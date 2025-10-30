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
            richTextBoxLog = new RichTextBox();
            buttonClearLogs = new Button();
            toolStripAboutApp.SuspendLayout();
            SuspendLayout();
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(599, 401);
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
            portTextBox.Location = new Point(12, 401);
            portTextBox.Name = "portTextBox";
            portTextBox.Size = new Size(100, 23);
            portTextBox.TabIndex = 1;
            portTextBox.Text = "52000";
            // 
            // portLabel
            // 
            portLabel.AutoSize = true;
            portLabel.Location = new Point(13, 380);
            portLabel.Name = "portLabel";
            portLabel.Size = new Size(35, 15);
            portLabel.TabIndex = 2;
            portLabel.Text = "Порт";
            // 
            // buttonStop
            // 
            buttonStop.Location = new Point(693, 401);
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
            toolStripAboutApp.Size = new Size(800, 25);
            toolStripAboutApp.TabIndex = 0;
            // 
            // richTextBoxLog
            // 
            richTextBoxLog.Location = new Point(8, 31);
            richTextBoxLog.Name = "richTextBoxLog";
            richTextBoxLog.Size = new Size(780, 310);
            richTextBoxLog.TabIndex = 4;
            richTextBoxLog.Text = "";
            // 
            // buttonClearLogs
            // 
            buttonClearLogs.Location = new Point(12, 347);
            buttonClearLogs.Name = "buttonClearLogs";
            buttonClearLogs.Size = new Size(75, 23);
            buttonClearLogs.TabIndex = 5;
            buttonClearLogs.Text = "Очистить логи";
            buttonClearLogs.UseVisualStyleBackColor = true;
            buttonClearLogs.Click += ButtonClearLogs_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonClearLogs);
            Controls.Add(richTextBoxLog);
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
        private RichTextBox richTextBoxLog;
        private Button buttonClearLogs;
    }
}
