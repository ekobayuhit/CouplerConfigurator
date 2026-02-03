using System.Windows.Forms;

namespace GespantCouplerConfigurator
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnCheckSSH = new System.Windows.Forms.Button();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.lblProtocol = new System.Windows.Forms.Label();
            this.cmbProtocol = new System.Windows.Forms.ComboBox();
            this.chkEnableLogger = new System.Windows.Forms.CheckBox();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.pnlStatusLed = new System.Windows.Forms.Panel();
            this.chkShowPassword = new System.Windows.Forms.CheckBox();
            this.lblStatusText = new System.Windows.Forms.Label();
            this.btnReboot = new System.Windows.Forms.Button();
            this.btnViewLogs = new System.Windows.Forms.Button();
            this.btnReadDeviceInfo = new System.Windows.Forms.Button();
            this.btnCanMonitor = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(40, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP Address";
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.txtIpAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIpAddress.ForeColor = System.Drawing.Color.White;
            this.txtIpAddress.Location = new System.Drawing.Point(40, 138);
            this.txtIpAddress.MaxLength = 15;
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(310, 22);
            this.txtIpAddress.TabIndex = 3;
            this.txtIpAddress.Text = "192.168.1.101";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(40, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "SSH Password";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.ForeColor = System.Drawing.Color.White;
            this.txtPassword.Location = new System.Drawing.Point(40, 198);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(310, 22);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnCheckSSH
            // 
            this.btnCheckSSH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.btnCheckSSH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckSSH.ForeColor = System.Drawing.Color.White;
            this.btnCheckSSH.Location = new System.Drawing.Point(40, 250);
            this.btnCheckSSH.Name = "btnCheckSSH";
            this.btnCheckSSH.Size = new System.Drawing.Size(310, 30);
            this.btnCheckSSH.TabIndex = 6;
            this.btnCheckSSH.Text = "🔍 CHECK SSH CONNECTION";
            this.btnCheckSSH.UseVisualStyleBackColor = false;
            this.btnCheckSSH.Click += new System.EventHandler(this.btnCheckConn_Click);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.btnSelectFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectFile.ForeColor = System.Drawing.Color.White;
            this.btnSelectFile.Location = new System.Drawing.Point(40, 355);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(310, 30);
            this.btnSelectFile.TabIndex = 9;
            this.btnSelectFile.Text = "📂 BROWSE PROGRAM";
            this.btnSelectFile.UseVisualStyleBackColor = false;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.label3.Location = new System.Drawing.Point(186, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(328, 41);
            this.label3.TabIndex = 1;
            this.label3.Text = "Protocol Configurator";
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(80)))), ((int)(((byte)(150)))));
            this.btnUpdate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(250)))));
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(40, 420);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(310, 50);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "START UPDATE";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.rtbLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.rtbLog.ForeColor = System.Drawing.Color.LimeGreen;
            this.rtbLog.Location = new System.Drawing.Point(380, 115);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(390, 405);
            this.rtbLog.TabIndex = 12;
            this.rtbLog.Text = "";
            // 
            // lblProtocol
            // 
            this.lblProtocol.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblProtocol.Location = new System.Drawing.Point(40, 295);
            this.lblProtocol.Name = "lblProtocol";
            this.lblProtocol.Size = new System.Drawing.Size(200, 23);
            this.lblProtocol.TabIndex = 7;
            this.lblProtocol.Text = "Communication Protocol";
            // 
            // cmbProtocol
            // 
            this.cmbProtocol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.cmbProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProtocol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbProtocol.ForeColor = System.Drawing.Color.White;
            this.cmbProtocol.Items.AddRange(new object[] {
            "Coupler-EtherNet/IP",
            "Coupler-ModbusTCP/IP"});
            this.cmbProtocol.Location = new System.Drawing.Point(40, 318);
            this.cmbProtocol.Name = "cmbProtocol";
            this.cmbProtocol.Size = new System.Drawing.Size(310, 24);
            this.cmbProtocol.TabIndex = 8;
            // 
            // chkEnableLogger
            // 
            this.chkEnableLogger.AutoSize = true;
            this.chkEnableLogger.ForeColor = System.Drawing.Color.Gray;
            this.chkEnableLogger.Location = new System.Drawing.Point(40, 395);
            this.chkEnableLogger.Name = "chkEnableLogger";
            this.chkEnableLogger.Size = new System.Drawing.Size(194, 20);
            this.chkEnableLogger.TabIndex = 10;
            this.chkEnableLogger.Text = "Enable deployment logging";
            // 
            // pbLogo
            // 
            this.pbLogo.Location = new System.Drawing.Point(30, 15);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(150, 100);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // pnlStatusLed
            // 
            this.pnlStatusLed.BackColor = System.Drawing.Color.DimGray;
            this.pnlStatusLed.Location = new System.Drawing.Point(335, 118);
            this.pnlStatusLed.Name = "pnlStatusLed";
            this.pnlStatusLed.Size = new System.Drawing.Size(12, 12);
            this.pnlStatusLed.TabIndex = 13;
            // 
            // chkShowPassword
            // 
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.ForeColor = System.Drawing.Color.Gray;
            this.chkShowPassword.Location = new System.Drawing.Point(40, 225);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Size = new System.Drawing.Size(125, 20);
            this.chkShowPassword.TabIndex = 15;
            this.chkShowPassword.Text = "Show Password";
            this.chkShowPassword.UseVisualStyleBackColor = true;
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);
            // 
            // lblStatusText
            // 
            this.lblStatusText.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Bold);
            this.lblStatusText.ForeColor = System.Drawing.Color.DimGray;
            this.lblStatusText.Location = new System.Drawing.Point(240, 116);
            this.lblStatusText.Name = "lblStatusText";
            this.lblStatusText.Size = new System.Drawing.Size(90, 20);
            this.lblStatusText.TabIndex = 14;
            this.lblStatusText.Text = "SSH OFFLINE";
            this.lblStatusText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnReboot
            // 
            this.btnReboot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnReboot.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReboot.Enabled = false;
            this.btnReboot.FlatAppearance.BorderColor = System.Drawing.Color.Tomato;
            this.btnReboot.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.btnReboot.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnReboot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReboot.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnReboot.ForeColor = System.Drawing.Color.Tomato;
            this.btnReboot.Location = new System.Drawing.Point(40, 480);
            this.btnReboot.Name = "btnReboot";
            this.btnReboot.Size = new System.Drawing.Size(310, 40);
            this.btnReboot.TabIndex = 16;
            this.btnReboot.Text = "🔄  REBOOT COUPLER";
            this.btnReboot.UseVisualStyleBackColor = false;
            this.btnReboot.Click += new System.EventHandler(this.btnReboot_Click);
            // 
            // btnViewLogs
            // 
            this.btnViewLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnViewLogs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewLogs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnViewLogs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.btnViewLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewLogs.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnViewLogs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnViewLogs.Location = new System.Drawing.Point(594, 526);
            this.btnViewLogs.Name = "btnViewLogs";
            this.btnViewLogs.Size = new System.Drawing.Size(176, 35);
            this.btnViewLogs.TabIndex = 17;
            this.btnViewLogs.Text = "📋  VIEW LOGS";
            this.btnViewLogs.UseVisualStyleBackColor = false;
            this.btnViewLogs.Click += new System.EventHandler(this.btnViewLogs_Click);
            // 
            // btnReadDeviceInfo
            // 
            this.btnReadDeviceInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnReadDeviceInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReadDeviceInfo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnReadDeviceInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.btnReadDeviceInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReadDeviceInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnReadDeviceInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnReadDeviceInfo.Location = new System.Drawing.Point(594, 567);
            this.btnReadDeviceInfo.Name = "btnReadDeviceInfo";
            this.btnReadDeviceInfo.Size = new System.Drawing.Size(176, 35);
            this.btnReadDeviceInfo.TabIndex = 17;
            this.btnReadDeviceInfo.Text = "🔍 READ DEVICE";
            this.btnReadDeviceInfo.UseVisualStyleBackColor = false;
            this.btnReadDeviceInfo.Click += new System.EventHandler(this.btnReadDeviceInfo_Click);
            // 
            // btnCanMonitor
            // 
            this.btnCanMonitor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnCanMonitor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCanMonitor.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.btnCanMonitor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(60)))), ((int)(((byte)(0)))));
            this.btnCanMonitor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCanMonitor.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnCanMonitor.ForeColor = System.Drawing.Color.LimeGreen;
            this.btnCanMonitor.Location = new System.Drawing.Point(412, 526);
            this.btnCanMonitor.Name = "btnCanMonitor";
            this.btnCanMonitor.Size = new System.Drawing.Size(176, 35);
            this.btnCanMonitor.TabIndex = 18;
            this.btnCanMonitor.Text = "📡 LIVE IO BUS MONITOR";
            this.btnCanMonitor.UseVisualStyleBackColor = false;
            this.btnCanMonitor.Click += new System.EventHandler(this.btnCanMonitor_Click);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(800, 623);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIpAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.chkShowPassword);
            this.Controls.Add(this.btnCheckSSH);
            this.Controls.Add(this.lblProtocol);
            this.Controls.Add(this.cmbProtocol);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.chkEnableLogger);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.pnlStatusLed);
            this.Controls.Add(this.lblStatusText);
            this.Controls.Add(this.btnReboot);
            this.Controls.Add(this.btnViewLogs);
            this.Controls.Add(this.btnReadDeviceInfo);
            this.Controls.Add(this.btnCanMonitor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "GespantCoupler Configurator v1.3";
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnCheckSSH;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Label lblProtocol;
        private System.Windows.Forms.ComboBox cmbProtocol;
        private System.Windows.Forms.CheckBox chkEnableLogger;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Panel pnlStatusLed;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Label lblStatusText;
        private System.Windows.Forms.Button btnReboot;
        private System.Windows.Forms.Button btnViewLogs;
        private System.Windows.Forms.Button btnReadDeviceInfo;
        private System.Windows.Forms.Button btnCanMonitor;
    }
}