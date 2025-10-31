namespace BarcodeScannerServer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            buttonStart = new Button();
            notifyIconTray = new NotifyIcon(components);
            toolStripButton1 = new ToolStripButton();
            portTextBox = new TextBox();
            portLabel = new Label();
            buttonStop = new Button();
            toolStripButton2 = new ToolStripButton();
            toolStripAboutApp = new ToolStrip();
            logsPage = new TabPage();
            scannerPortLabel = new Label();
            comboBoxParity = new ComboBox();
            comboBoxStopBits = new ComboBox();
            comboBoxDataBits = new ComboBox();
            comboBoxBaudRate = new ComboBox();
            buttonRefreshPorts = new Button();
            comboBoxComPort = new ComboBox();
            richTextBoxLog = new RichTextBox();
            buttonClearLogs = new Button();
            tabControl1 = new TabControl();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            toolStripAboutApp.SuspendLayout();
            logsPage.SuspendLayout();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // buttonStart
            // 
            buttonStart.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonStart.Location = new Point(611, 452);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(75, 23);
            buttonStart.TabIndex = 0;
            buttonStart.Text = "Пуск";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += ButtonStart_Click;
            // 
            // notifyIconTray
            // 
            notifyIconTray.Icon = (Icon)resources.GetObject("notifyIconTray.Icon");
            notifyIconTray.Text = "notifyIconTray";
            notifyIconTray.Visible = true;
            notifyIconTray.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // toolStripButton1
            // 
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(23, 23);
            // 
            // portTextBox
            // 
            portTextBox.Anchor = AnchorStyles.Bottom;
            portTextBox.Location = new Point(17, 454);
            portTextBox.Name = "portTextBox";
            portTextBox.Size = new Size(100, 23);
            portTextBox.TabIndex = 1;
            portTextBox.Text = "52000";
            // 
            // portLabel
            // 
            portLabel.Anchor = AnchorStyles.Bottom;
            portLabel.AutoSize = true;
            portLabel.Location = new Point(17, 432);
            portLabel.Name = "portLabel";
            portLabel.Size = new Size(82, 15);
            portLabel.TabIndex = 2;
            portLabel.Text = "Порт сервера";
            // 
            // buttonStop
            // 
            buttonStop.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonStop.Location = new Point(703, 453);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(75, 23);
            buttonStop.TabIndex = 3;
            buttonStop.Text = "Стоп";
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Click += ButtonStop_Click;
            // 
            // toolStripButton2
            // 
            toolStripButton2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton2.Image = (Image)resources.GetObject("toolStripButton2.Image");
            toolStripButton2.ImageTransparentColor = Color.Magenta;
            toolStripButton2.Name = "toolStripButton2";
            toolStripButton2.Size = new Size(86, 22);
            toolStripButton2.Text = "О программе";
            // 
            // toolStripAboutApp
            // 
            toolStripAboutApp.Items.AddRange(new ToolStripItem[] { toolStripButton2 });
            toolStripAboutApp.Location = new Point(0, 0);
            toolStripAboutApp.Name = "toolStripAboutApp";
            toolStripAboutApp.Size = new Size(810, 25);
            toolStripAboutApp.TabIndex = 0;
            // 
            // logsPage
            // 
            logsPage.Controls.Add(label4);
            logsPage.Controls.Add(label3);
            logsPage.Controls.Add(label2);
            logsPage.Controls.Add(label1);
            logsPage.Controls.Add(scannerPortLabel);
            logsPage.Controls.Add(comboBoxParity);
            logsPage.Controls.Add(comboBoxStopBits);
            logsPage.Controls.Add(comboBoxDataBits);
            logsPage.Controls.Add(comboBoxBaudRate);
            logsPage.Controls.Add(buttonRefreshPorts);
            logsPage.Controls.Add(comboBoxComPort);
            logsPage.Controls.Add(richTextBoxLog);
            logsPage.Controls.Add(buttonClearLogs);
            logsPage.Location = new Point(4, 24);
            logsPage.Name = "logsPage";
            logsPage.Padding = new Padding(3);
            logsPage.Size = new Size(790, 372);
            logsPage.TabIndex = 0;
            logsPage.Text = "Логи";
            logsPage.UseVisualStyleBackColor = true;
            // 
            // scannerPortLabel
            // 
            scannerPortLabel.AutoSize = true;
            scannerPortLabel.Location = new Point(644, 9);
            scannerPortLabel.Name = "scannerPortLabel";
            scannerPortLabel.Size = new Size(38, 15);
            scannerPortLabel.TabIndex = 16;
            scannerPortLabel.Text = "label1";
            // 
            // comboBoxParity
            // 
            comboBoxParity.AutoCompleteCustomSource.AddRange(new string[] { "None", "Even", "Odd", "Mark", "Space" });
            comboBoxParity.FormattingEnabled = true;
            comboBoxParity.Items.AddRange(new object[] { "None", "Even", "Odd", "Mark", "Space" });
            comboBoxParity.Location = new Point(644, 204);
            comboBoxParity.Name = "comboBoxParity";
            comboBoxParity.Size = new Size(121, 23);
            comboBoxParity.TabIndex = 13;
            // 
            // comboBoxStopBits
            // 
            comboBoxStopBits.AutoCompleteCustomSource.AddRange(new string[] { "One", "Two", "OnePointFive" });
            comboBoxStopBits.FormattingEnabled = true;
            comboBoxStopBits.Items.AddRange(new object[] { "One", "Two", "OnePointFive" });
            comboBoxStopBits.Location = new Point(644, 161);
            comboBoxStopBits.Name = "comboBoxStopBits";
            comboBoxStopBits.Size = new Size(121, 23);
            comboBoxStopBits.TabIndex = 12;
            // 
            // comboBoxDataBits
            // 
            comboBoxDataBits.AutoCompleteCustomSource.AddRange(new string[] { "7", "8" });
            comboBoxDataBits.FormattingEnabled = true;
            comboBoxDataBits.Items.AddRange(new object[] { "7", "8" });
            comboBoxDataBits.Location = new Point(644, 115);
            comboBoxDataBits.Name = "comboBoxDataBits";
            comboBoxDataBits.Size = new Size(121, 23);
            comboBoxDataBits.TabIndex = 11;
            // 
            // comboBoxBaudRate
            // 
            comboBoxBaudRate.FormattingEnabled = true;
            comboBoxBaudRate.Items.AddRange(new object[] { "9600", "19200", "38400", "57600", "115200" });
            comboBoxBaudRate.Location = new Point(644, 70);
            comboBoxBaudRate.Name = "comboBoxBaudRate";
            comboBoxBaudRate.Size = new Size(121, 23);
            comboBoxBaudRate.TabIndex = 10;
            // 
            // buttonRefreshPorts
            // 
            buttonRefreshPorts.Location = new Point(644, 249);
            buttonRefreshPorts.Name = "buttonRefreshPorts";
            buttonRefreshPorts.Size = new Size(112, 23);
            buttonRefreshPorts.TabIndex = 9;
            buttonRefreshPorts.Text = "Обновить порты";
            buttonRefreshPorts.UseVisualStyleBackColor = true;
            buttonRefreshPorts.Click += ButtonRefreshPorts_Click;
            // 
            // comboBoxComPort
            // 
            comboBoxComPort.FormattingEnabled = true;
            comboBoxComPort.Location = new Point(644, 25);
            comboBoxComPort.Name = "comboBoxComPort";
            comboBoxComPort.Size = new Size(121, 23);
            comboBoxComPort.TabIndex = 8;
            // 
            // richTextBoxLog
            // 
            richTextBoxLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            richTextBoxLog.Location = new Point(3, 6);
            richTextBoxLog.Name = "richTextBoxLog";
            richTextBoxLog.Size = new Size(612, 324);
            richTextBoxLog.TabIndex = 4;
            richTextBoxLog.Text = "";
            // 
            // buttonClearLogs
            // 
            buttonClearLogs.Anchor = AnchorStyles.Bottom;
            buttonClearLogs.Location = new Point(7, 335);
            buttonClearLogs.Name = "buttonClearLogs";
            buttonClearLogs.Size = new Size(75, 23);
            buttonClearLogs.TabIndex = 5;
            buttonClearLogs.Text = "Очистить логи";
            buttonClearLogs.UseVisualStyleBackColor = true;
            buttonClearLogs.Click += ButtonClearLogs_Click;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(logsPage);
            tabControl1.Location = new Point(12, 28);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(798, 400);
            tabControl1.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(644, 52);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 17;
            label1.Text = "label1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(644, 96);
            label2.Name = "label2";
            label2.Size = new Size(38, 15);
            label2.TabIndex = 18;
            label2.Text = "label2";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(644, 143);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 19;
            label3.Text = "label3";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(644, 187);
            label4.Name = "label4";
            label4.Size = new Size(38, 15);
            label4.TabIndex = 20;
            label4.Text = "label4";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(810, 496);
            Controls.Add(tabControl1);
            Controls.Add(buttonStop);
            Controls.Add(portLabel);
            Controls.Add(portTextBox);
            Controls.Add(toolStripAboutApp);
            Controls.Add(buttonStart);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Сервер работы с драйвером сканера штрихкода";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            Resize += MainForm_Resize;
            toolStripAboutApp.ResumeLayout(false);
            toolStripAboutApp.PerformLayout();
            logsPage.ResumeLayout(false);
            logsPage.PerformLayout();
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonStart;
        private NotifyIcon notifyIconTray;
        private ToolStripButton toolStripButton1;
        private TextBox portTextBox;
        private Label portLabel;
        private Button buttonStop;
        private ToolStripButton toolStripButton2;
        private ToolStrip toolStripAboutApp;
        private TabPage logsPage;
        private RichTextBox richTextBoxLog;
        private Button buttonClearLogs;
        private TabControl tabControl1;
        private ComboBox comboBoxParity;
        private ComboBox comboBoxStopBits;
        private ComboBox comboBoxDataBits;
        private ComboBox comboBoxBaudRate;
        private Button buttonRefreshPorts;
        private ComboBox comboBoxComPort;
        private Label scannerPortLabel;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}
