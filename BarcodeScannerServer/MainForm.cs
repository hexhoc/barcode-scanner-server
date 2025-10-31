using System;
using System.IO.Ports;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BarcodeScannerServer
{
    public partial class MainForm : Form
    {
        private HttpListener httpListener;
        private CancellationTokenSource cancellationTokenSource;
        private bool isRunning = false;
        private SerialPort barcodeScanner;
        private bool isScannerListening = false;

        // Colors for different log types
        private readonly Color ColorNormal = Color.Black;
        private readonly Color ColorError = Color.Red;
        private readonly Color ColorSuccess = Color.Green;
        private readonly Color ColorWarning = Color.Orange;
        private readonly Color ColorInfo = Color.Blue;
        private readonly Color ColorReceived = Color.Purple;
        private readonly Color ColorSent = Color.DarkGreen;

        // Store connected WebSocket clients
        private readonly List<WebSocket> connectedClients = new List<WebSocket>();
        private readonly object clientsLock = new object();

        public MainForm()
        {
            InitializeComponent();
            InitializeScannerControls();
        }

        private void InitializeScannerControls()
        {
            // Populate COM ports dropdown
            PopulateComPorts();

            // Set default baud rate
            comboBoxBaudRate.SelectedItem = "9600";

            // Set default data bits
            comboBoxDataBits.SelectedItem = "8";

            // Set default stop bits
            comboBoxStopBits.SelectedItem = "One";

            // Set default parity
            comboBoxParity.SelectedItem = "None";
        }

        private void PopulateComPorts()
        {
            comboBoxComPort.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            comboBoxComPort.Items.AddRange(ports);

            if (comboBoxComPort.Items.Count > 0)
            {
                comboBoxComPort.SelectedIndex = 0;
            }
        }

        private void ButtonRefreshPorts_Click(object sender, EventArgs e)
        {
            PopulateComPorts();
            LogMessage("COM ports refreshed", ColorInfo);
        }

        private void ButtonCheckScanner_Click(object sender, EventArgs e)
        {
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Update UI to reflect initial state
            UpdateUIState();

            // Add initial log message
            LogMessage("Application started. Click 'Start' to begin WebSocket server.", ColorInfo);
        }

        private async void ButtonStart_Click(object sender, EventArgs e)
        {
            try
            {
                // Start the WebSocket server
                await StartWebSocketServer();
            }
            catch (Exception ex)
            {
                LogMessage($"Failed to start server: {ex.Message}", ColorError);
                MessageBox.Show($"Failed to start server: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            StopWebSocketServer();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // we check our window, and if it has been minimized, we make an event       
            if (WindowState == FormWindowState.Minimized)
            {
                // hiding our window from the panel
                this.ShowInTaskbar = false;
                // making our tray icon active
                notifyIconTray.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowInTaskbar = true;
            notifyIconTray.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private Task StartWebSocketServer()
        {
            if (isRunning)
            {
                return Task.CompletedTask;
            }

            string serverUrl = $"http://localhost:{portTextBox.Text}/";

            // Configure the server to listen on localhost port 
            httpListener = new HttpListener();
            httpListener.Prefixes.Add(serverUrl);

            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                httpListener.Start();
                isRunning = true;
                UpdateUIState();

                // Log server start
                LogMessage($"WebSocket server started on ws://localhost:{portTextBox.Text}/", ColorSuccess);
                LogMessage("Server is listening for incoming connections...", ColorInfo);

                // Start listening for connections in background
                _ = Task.Run(async () => await ListenForConnections());
            }
            catch (Exception ex)
            {
                LogMessage($"ERROR starting server: {ex.Message}", ColorError);
                isRunning = false;
                UpdateUIState();
                throw;
            }

            return Task.CompletedTask;
        }

        private async Task ListenForConnections()
        {
            var token = cancellationTokenSource.Token;

            while (!token.IsCancellationRequested && httpListener.IsListening)
            {
                try
                {
                    // Wait for incoming connection
                    var context = await httpListener.GetContextAsync();

                    if (context.Request.IsWebSocketRequest)
                    {
                        // Accept the WebSocket connection
                        var webSocketContext = await context.AcceptWebSocketAsync(null);
                        var webSocket = webSocketContext.WebSocket;

                        LogMessage($"Client connected from {context.Request.RemoteEndPoint}", ColorSuccess);

                        // Add client to connected clients list
                        lock (clientsLock)
                        {
                            connectedClients.Add(webSocket);
                        }

                        // Handle the WebSocket connection in background
                        _ = Task.Run(async () => await HandleWebSocketConnection(webSocket));
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                        context.Response.Close();
                        LogMessage($"Rejected non-WebSocket request from {context.Request.RemoteEndPoint}", ColorWarning);
                    }
                }
                catch (HttpListenerException)
                {
                    // This is expected when stopping the listener
                    break;
                }
                catch (Exception ex)
                {
                    LogMessage($"Error handling connection: {ex.Message}", ColorError);
                }
            }
        }


        private async Task HandleWebSocketConnection(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var token = cancellationTokenSource.Token;

            try
            {
                while (webSocket.State == WebSocketState.Open && !token.IsCancellationRequested)
                {
                    // Wait for message from client
                    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), token);

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        // Process received message
                        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                        LogMessage($"Received: {message}", ColorReceived);

                        // Echo the message back to client
                        var response = $"Echo: {message}";
                        var responseBytes = Encoding.UTF8.GetBytes(response);
                        await webSocket.SendAsync(
                            new ArraySegment<byte>(responseBytes),
                            WebSocketMessageType.Text,
                            true,
                            token);

                        LogMessage($"Sent: {response}", ColorSent);
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure,
                            "Closed by client", token);
                        LogMessage("Client disconnected gracefully", ColorInfo);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Expected when server is stopping
                LogMessage("Client connection terminated (server shutdown)", ColorWarning);
            }
            catch (Exception ex)
            {
                LogMessage($"WebSocket error: {ex.Message}", ColorError);
            }
            finally
            {
                // Remove client from connected clients list
                lock (clientsLock)
                {
                    connectedClients.Remove(webSocket);
                }
                webSocket?.Dispose();
            }
        }

        private void StartBarcodeScanner()
        {
            if (isScannerListening)
            {
                LogMessage("Barcode scanner is already listening", ColorWarning);
                return;
            }

            if (comboBoxComPort.SelectedItem == null)
            {
                LogMessage("Please select a COM port", ColorError);
                return;
            }

            try
            {
                // Configure serial port
                barcodeScanner = new SerialPort
                {
                    PortName = comboBoxComPort.SelectedItem.ToString(),
                    BaudRate = int.Parse(comboBoxBaudRate.SelectedItem.ToString()),
                    DataBits = int.Parse(comboBoxDataBits.SelectedItem.ToString()),
                    StopBits = GetStopBits(comboBoxStopBits.SelectedItem.ToString()),
                    Parity = GetParity(comboBoxParity.SelectedItem.ToString()),
                    Handshake = Handshake.None,
                    ReadTimeout = 1000,
                    WriteTimeout = 1000
                };

                // Subscribe to data received event
                barcodeScanner.DataReceived += BarcodeScanner_DataReceived;

                // Open the serial port
                barcodeScanner.Open();
                isScannerListening = true;
                UpdateUIState();

                LogMessage($"Barcode scanner started on {barcodeScanner.PortName}", ColorSuccess);
            }
            catch (Exception ex)
            {
                LogMessage($"Failed to start barcode scanner: {ex.Message}", ColorError);
                MessageBox.Show($"Failed to start barcode scanner: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StopBarcodeScanner()
        {
            if (!isScannerListening || barcodeScanner == null)
            {
                return;
            }

            try
            {
                barcodeScanner.DataReceived -= BarcodeScanner_DataReceived;
                barcodeScanner.Close();
                barcodeScanner.Dispose();
                barcodeScanner = null;
                isScannerListening = false;
                UpdateUIState();

                LogMessage("Barcode scanner stopped", ColorSuccess);
            }
            catch (Exception ex)
            {
                LogMessage($"Error stopping barcode scanner: {ex.Message}", ColorError);
            }
        }

        private void BarcodeScanner_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;
                string barcodeData = sp.ReadExisting().Trim();

                if (!string.IsNullOrEmpty(barcodeData))
                {
                    // Log the barcode data
                    LogMessage($"Barcode scanned: {barcodeData}", ColorNormal);

                    // Broadcast to all connected WebSocket clients
                    BroadcastToClients(barcodeData);
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Error reading barcode data: {ex.Message}", ColorError);
            }
        }

        private async void BroadcastToClients(string message)
        {
            if (connectedClients.Count == 0)
            {
                return;
            }

            var token = cancellationTokenSource.Token;
            var messageBytes = Encoding.UTF8.GetBytes(message);
            var messageSegment = new ArraySegment<byte>(messageBytes);

            List<WebSocket> clientsToRemove = new List<WebSocket>();

            lock (clientsLock)
            {
                foreach (var client in connectedClients)
                {
                    if (client.State == WebSocketState.Open)
                    {
                        try
                        {
                            client.SendAsync(
                                messageSegment,
                                WebSocketMessageType.Text,
                                true,
                                token);

                            LogMessage($"Broadcasted barcode to client: {message}", ColorSent);
                        }
                        catch (Exception ex)
                        {
                            LogMessage($"Error sending to client: {ex.Message}", ColorWarning);
                            clientsToRemove.Add(client);
                        }
                    }
                    else
                    {
                        clientsToRemove.Add(client);
                    }
                }

                // Remove disconnected clients
                foreach (var client in clientsToRemove)
                {
                    connectedClients.Remove(client);
                    client.Dispose();
                }
            }
        }

        private StopBits GetStopBits(string stopBitsText)
        {
            return stopBitsText switch
            {
                "One" => StopBits.One,
                "Two" => StopBits.Two,
                "OnePointFive" => StopBits.OnePointFive,
                _ => StopBits.One
            };
        }

        private Parity GetParity(string parityText)
        {
            return parityText switch
            {
                "None" => Parity.None,
                "Even" => Parity.Even,
                "Odd" => Parity.Odd,
                "Mark" => Parity.Mark,
                "Space" => Parity.Space,
                _ => Parity.None
            };
        }

        private void StopWebSocketServer()
        {
            if (!isRunning) return;

            try
            {
                LogMessage("Stopping WebSocket server...", ColorWarning);

                // Stop barcode scanner first
                StopBarcodeScanner();

                cancellationTokenSource?.Cancel();
                httpListener?.Stop();

                // Close all client connections
                lock (clientsLock)
                {
                    foreach (var client in connectedClients)
                    {
                        try
                        {
                            client.Dispose();
                        }
                        catch { }
                    }
                    connectedClients.Clear();
                }

                isRunning = false;
                UpdateUIState();
                LogMessage("WebSocket server stopped successfully", ColorSuccess);
            }
            catch (Exception ex)
            {
                LogMessage($"Error stopping server: {ex.Message}", ColorError);
            }
        }

        // Updating the form and its elements
        private void UpdateUIState()
        {
            // Update UI elements based on server state
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateUIState));
                return;
            }

            // Setting the accessibility of the buttons
            buttonStart.Enabled = !isRunning;
            buttonStop.Enabled = isRunning;
            buttonStartScanner.Enabled = isRunning && !isScannerListening;
            buttonStopScanner.Enabled = isScannerListening;
            buttonRefreshPorts.Enabled = !isScannerListening;

            // COM port controls
            comboBoxComPort.Enabled = !isScannerListening;
            comboBoxBaudRate.Enabled = !isScannerListening;
            comboBoxDataBits.Enabled = !isScannerListening;
            comboBoxStopBits.Enabled = !isScannerListening;
            comboBoxParity.Enabled = !isScannerListening;

            // Updating the form title
            this.Text = $"Barcode Scanner Server - {(isRunning ? "RUNNING" : "STOPPED")} - Scanner: {(isScannerListening ? "LISTENING" : "STOPPED")}";

            // Updating the form title
            this.Text = $"Barcode Scanner Server - {(isRunning ? "RUNNING" : "STOPPED")}";
        }

        // Logging the event in the RichTextBox on the form
        private void LogMessage(string message, Color color)
        {
            // Add timestamp and log to RichTextBox
            if (InvokeRequired)
            {
                Invoke(new Action<string, Color>(LogMessage), message, color);
                return;
            }

            int maxLines = 10000; // Keep last 10,000 lines
            string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            string logEntry = $"[{timestamp}] {message}\n";

            // Check if we need to trim old lines
            if (richTextBoxLog.Lines.Length >= maxLines)
            {
                TrimOldLines(maxLines);
            }

            // Append to RichTextBox with color and auto-scroll to bottom
            richTextBoxLog.SelectionStart = richTextBoxLog.TextLength;
            richTextBoxLog.SelectionLength = 0;
            richTextBoxLog.SelectionColor = color;
            richTextBoxLog.AppendText(logEntry);
            richTextBoxLog.SelectionColor = richTextBoxLog.ForeColor; // Reset to default color

            // Auto-scroll to the bottom
            richTextBoxLog.ScrollToCaret();
        }

        // Truncating the logs. RichTextBox has a limit of ~2.1 billion characters.
        private void TrimOldLines(int maxLines)
        {
            try
            {
                // Keep only the most recent lines
                var lines = richTextBoxLog.Lines;
                if (lines.Length > maxLines)
                {
                    int removeCount = lines.Length - maxLines;
                    int removeLength = 0;

                    // Calculate length of lines to remove
                    for (int i = 0; i < removeCount; i++)
                    {
                        removeLength += lines[i].Length + 1; // +1 for newline
                    }

                    // Remove old lines
                    richTextBoxLog.Select(0, removeLength);
                    richTextBoxLog.SelectedText = "";
                }
            }
            catch (Exception ex)
            {
                // Fallback: clear and add message
                richTextBoxLog.Clear();
                richTextBoxLog.AppendText($"Log cleared due to size management. Previous logs trimmed.\n");
            }
        }

        // The log cleanup button
        private void ButtonClearLogs_Click(object sender, EventArgs e)
        {
            richTextBoxLog.Clear();
        }

        // Actions when closing the form
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopWebSocketServer(); // Останавливаем сервер
            cancellationTokenSource?.Dispose();
            httpListener?.Close();
        }
    }

}
