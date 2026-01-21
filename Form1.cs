using System;
using System.CodeDom.Compiler;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Renci.SshNet;

namespace GespantCouplerConfigurator
{
    public partial class Form1 : Form
    {
        private string selectedFilePath = "";
        private Timer connectionTimer;
        private bool isCheckingConnection = false;
        private bool deploymentSuccessful = false;

        public Form1()
        {
            InitializeComponent();
            LoadLogo();

            chkEnableLogger.Checked = true;

            // Status Timer Setup
            connectionTimer = new Timer();
            connectionTimer.Interval = 10000;
            connectionTimer.Tick += ConnectionTimer_Tick;
            connectionTimer.Start();

            // Create circular LED UI
            using (System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath())
            {
                gp.AddEllipse(0, 0, pnlStatusLed.Width, pnlStatusLed.Height);
                pnlStatusLed.Region = new Region(gp);
            }
        }

        private void LoadLogo()
        {
            try
            {
                // Ensure the resource name matches your project properties
                byte[] imageBytes = global::GespantCouplerConfigurator.Properties.Resources.Gespant_Logo;
                using (var ms = new MemoryStream(imageBytes))
                {
                    Image original = Image.FromStream(ms);
                    pbLogo.Image = InvertImage(original);
                }
            }
            catch { /* Logo optional */ }
        }

        private Image InvertImage(Image source)
        {
            Bitmap bmp = new Bitmap(source);
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color c = bmp.GetPixel(x, y);
                    if (c.A > 0) bmp.SetPixel(x, y, Color.FromArgb(c.A, 255 - c.R, 255 - c.G, 255 - c.B));
                }
            }
            return bmp;
        }

        private void ExecuteAndLog(SshClient client, string command)
        {
            var cmd = client.CreateCommand(command);
            string result = cmd.Execute();

            // Log normal output (like "Inflating: file.py")
            if (!string.IsNullOrWhiteSpace(result))
                UpdateStatus("SSH Output: " + result.Trim(), Color.Gray);

            // Log errors (like "unzip: command not found")
            if (!string.IsNullOrWhiteSpace(cmd.Error))
                UpdateStatus("SSH Error: " + cmd.Error.Trim(), Color.Red);
        }
        private void UpdateStatus(string message, Color? color = null)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateStatus(message, color)));
                return;
            }
            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.SelectionColor = color ?? Color.LimeGreen;
            rtbLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\n");
            rtbLog.ScrollToCaret();
        }

        private async Task<bool> ValidateSshConnection()
        {
            string host = txtIpAddress.Text.Trim();
            string pass = txtPassword.Text;
            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(pass)) return false;

            return await Task.Run(() =>
            {
                try
                {
                    using (var client = new SshClient(host, "root", pass))
                    {
                        client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(3);
                        client.Connect();
                        bool connected = client.IsConnected;
                        client.Disconnect();
                        return connected;
                    }
                }
                catch { return false; }
            });
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e) => txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;

        private async void btnCheckConn_Click(object sender, EventArgs e)
        {
            if (isCheckingConnection) return;
            string rawIp = txtIpAddress.Text.Trim();

            if (!System.Net.IPAddress.TryParse(rawIp, out _))
            {
                UpdateStatus("VALIDATION ERROR: Invalid IP format.", Color.Orange);
                pnlStatusLed.BackColor = Color.Orange;
                lblStatusText.Text = "INVALID IP";
                return;
            }

            isCheckingConnection = true;
            btnCheckSSH.Enabled = false;
            UpdateStatus($"Testing SSH connection to {rawIp}...", Color.White);

            bool isConnected = await ValidateSshConnection();

            if (isConnected)
            {
                UpdateStatus("CONNECTION SUCCESS: Authorized.", Color.LimeGreen);
                pnlStatusLed.BackColor = Color.LimeGreen;
                lblStatusText.Text = "SSH ONLINE";
                lblStatusText.ForeColor = Color.LimeGreen;
            }
            else
            {
                UpdateStatus("CONNECTION FAILED.", Color.Red);
                pnlStatusLed.BackColor = Color.Red;
                lblStatusText.Text = "SSH OFFLINE";
                lblStatusText.ForeColor = Color.Red;
            }

            btnCheckSSH.Enabled = true;
            isCheckingConnection = false;
        }

        private async void ConnectionTimer_Tick(object sender, EventArgs e)
        {
            if (isCheckingConnection) return;
            string rawIp = txtIpAddress.Text.Trim();
            if (string.IsNullOrEmpty(rawIp) || !System.Net.IPAddress.TryParse(rawIp, out _)) return;

            bool isConnected = await ValidateSshConnection();

            this.Invoke(new Action(() => {
                pnlStatusLed.BackColor = isConnected ? Color.LimeGreen : Color.Red;
                lblStatusText.Text = isConnected ? "SSH ONLINE" : "SSH OFFLINE";
                lblStatusText.ForeColor = isConnected ? Color.LimeGreen : Color.Red;
            }));
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                // 1. Restrict to .zip only in the explorer
                ofd.Filter = "Zip Archives (*.zip)|*.zip";
                ofd.Title = "Select Firmware Update Package";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fileInfo = new FileInfo(ofd.FileName);
                    string ext = fileInfo.Extension.ToLower();

                    // 2. Define max size (e.g., 50MB)
                    long maxSizeBytes = 50 * 1024 * 1024;

                    if (ext != ".zip")
                    {
                        MessageBox.Show("Only .zip files are supported.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (fileInfo.Length > maxSizeBytes)
                    {
                        double sizeInMb = (double)fileInfo.Length / (1024 * 1024);
                        MessageBox.Show($"File is too large ({sizeInMb:F2} MB). Max limit is 50 MB.",
                                        "File Size Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        UpdateStatus($"REJECTED: {fileInfo.Name} is too large.", Color.Red);
                        return;
                    }

                    // If all checks pass
                    selectedFilePath = ofd.FileName;
                    UpdateStatus($"File ready: {fileInfo.Name} ({(fileInfo.Length / 1024.0 / 1024.0):F2} MB)", Color.Cyan);
                }
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFilePath) || cmbProtocol.SelectedItem == null)
            {
                MessageBox.Show("Select file and protocol.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            deploymentSuccessful = false;
            btnUpdate.Enabled = false;
            btnReboot.Enabled = false; // Disable reboot until this specific update finishes

            btnUpdate.Text = "UPLOADING...";
            btnUpdate.BackColor = Color.FromArgb(45, 45, 48); // Dim it out

            string host = txtIpAddress.Text.Trim();
            string pass = txtPassword.Text;
            string protocol = cmbProtocol.SelectedItem.ToString();
            string fileName = Path.GetFileName(selectedFilePath);
            string destDir = "/home/Gespant/gespant-io-coupler-webserver/src/canopen_master";
            string Webserver_Service = "gespant_webserver.service";
            string ENIP_Service = "gespant_ENIP.service";
            string ModbusTCP_Service = "gespant_modbus.service";
            string ENIP_APP_PREFIX = "GSP_COUPLER_ENIP";

            UpdateStatus($"Starting {protocol} deployment...", Color.Yellow);

            await Task.Run(() =>
            {
                try
                {
                    // Upload via SFTP
                    using (var sftp = new SftpClient(host, "root", pass))
                    {
                        sftp.Connect();
                        using (var fs = File.OpenRead(selectedFilePath))
                        {
                            sftp.UploadFile(fs, "/tmp/" + fileName);
                        }
                    }

                    // Install via SSH
                    using (var ssh = new SshClient(host, "root", pass))
                    {
                        string cmd_webserver = "";
                        ssh.Connect();

                        cmd_webserver = "systemctl stop " + Webserver_Service;
                        // Stop current services
                        ssh.RunCommand(cmd_webserver);
                        string service = protocol.Contains("EtherNet/IP") ? ENIP_Service : ModbusTCP_Service;
                        ssh.RunCommand($"systemctl stop {service}");

                        // Protocol Specific Logic
                        if (protocol.Contains("Modbus"))
                        {
                            UpdateStatus("Stopping EtherNet/IP services...", Color.Yellow);
                            ssh.RunCommand($"systemctl stop {ENIP_Service} && systemctl disable {ENIP_Service}");

                            // STAGING: Extract to a separate temp folder first
                            string tempExtractPath = "/tmp/modbus_staging";
                            ssh.RunCommand($"rm -rf {tempExtractPath} && mkdir -p {tempExtractPath}");

                            UpdateStatus("Extracting to staging area...", Color.White);
                            ExecuteAndLog(ssh, $"unzip -oq /tmp/{fileName} -d {tempExtractPath}");

                            // TARGETED CLEANUP (The Safest Way)
                            // Instead of using 'find', we delete specific Python-related items only.
                            // This guarantees 'Master' and 'logs' stay safe because we don't mention them.
                            UpdateStatus("Removing old Modbus scripts...", Color.White);
                            ssh.RunCommand($"rm -rf {destDir}/*.py {destDir}/venv {destDir}/__pycache__ {destDir}/frontend {destDir}/data");

                            // DEPLOY: Move from staging to destination
                            // This logic handles both: zip with a folder inside, or zip with files at root.
                            UpdateStatus("Moving new files to destination...", Color.White);
                            string moveCmd = $"cd {tempExtractPath} && " +
                                             "IF_DIR=$(ls -d */ 2>/dev/null | head -1); " +
                                             "if [ -n \"$IF_DIR\" ]; then " +
                                             $"  mv \"$IF_DIR\"* {destDir}/; " + // Move contents of subfolder
                                             "else " +
                                             $"  mv * {destDir}/; " +             // Move files from root
                                             "fi";
                            ssh.RunCommand(moveCmd);

                            // PERMISSIONS
                            UpdateStatus("Setting permissions...", Color.White);
                            ssh.RunCommand($"chmod +x {destDir}/*.py");
                            ssh.RunCommand($"[ -d {destDir}/venv/bin ] && chmod -R +x {destDir}/venv/bin/*");

                            // FINAL CLEANUP
                            ssh.RunCommand($"rm -rf {tempExtractPath} /tmp/{fileName}");
                        }
                        else if (protocol.Contains("EtherNet/IP"))
                        {
                            UpdateStatus("Stopping ModbusTCP/IP services for EtherNet/IP deployment...", Color.Yellow);
                            ssh.RunCommand($"systemctl stop {ModbusTCP_Service} && systemctl disable {ModbusTCP_Service}");

                            // CLEANUP: Delete old versioned files to prevent confusion
                            // We keep 'Master' (the link) but delete 'Master_v1', 'Master_Alpha', etc.
                            UpdateStatus("Cleaning up old binaries...", Color.Yellow);
                            ssh.RunCommand($"find {destDir} -maxdepth 1 -type f -name '{ENIP_APP_PREFIX}_*' -delete");

                            ExecuteAndLog(ssh, $"unzip -oj /tmp/{fileName} -d {destDir}");

                            // IDENTIFY & LINK
                            // Since we deleted the old ones, the ONLY 'Master_*' file now is the new one
                            string linkCmd = $"cd {destDir} && " +
                                             $"NEW_BIN=$(ls -t {ENIP_APP_PREFIX}_* 2>/dev/null | head -1) && " +
                                             "if [ -n \"$NEW_BIN\" ]; then " +
                                             "  rm -f Master && ln -s \"$NEW_BIN\" Master && echo \"Linked Master to $NEW_BIN\"; " +
                                             "else " +
                                             "  echo \"Error: Extraction failed, no file with prefix {ENIP_APP_PREFIX} found!\"; " +
                                             "fi";

                            ExecuteAndLog(ssh, linkCmd);

                            // PERMISSIONS
                            ssh.RunCommand($"chmod +x {destDir}/Master*");
                        }

                        if (chkEnableLogger.Checked)
                        {
                            // We use backticks `` so the Linux shell executes the command inside them.
                            string linuxLogCommand = $"mkdir -p {destDir}/logs && echo \"Updated with {fileName} on `date`\" >> {destDir}/logs/update.log";
                            ExecuteAndLog(ssh, linuxLogCommand);

                            // We use DateTime.Now to show the local computer time in your app's log.
                            string uiLogMessage = $"Updated with {fileName} on {DateTime.Now:G}";
                            UpdateStatus("Device Log Entry: " + uiLogMessage, Color.Gray);
                        }
                        
                        // Cleanup installer and Start services
                        ssh.RunCommand($"rm /tmp/{fileName}");

                        cmd_webserver = "systemctl restart " + Webserver_Service;
                        ssh.RunCommand($"systemctl enable {service}");
                        ssh.RunCommand($"systemctl restart {service} && " + cmd_webserver);

                        deploymentSuccessful = true;
                    }
                }
                catch (Exception ex)
                {
                    UpdateStatus("Deployment Error: " + ex.Message, Color.Red);
                }
            });

            // UI UPDATES AFTER TASK COMPLETION
            if (deploymentSuccessful)
            {
                UpdateStatus("SUCCESS: Update complete. Reboot recommended.", Color.LimeGreen);
                this.Invoke(new Action(() => {
                    btnReboot.Enabled = true;
                    btnReboot.BackColor = Color.FromArgb(60, 30, 30); // Give it a subtle "ready" glow
                    UpdateStatus("Reboot now available.", Color.Tomato);
                }));
            }

            btnUpdate.Enabled = true;
            btnUpdate.Text = "START UPDATE";
            btnUpdate.BackColor = Color.FromArgb(0, 122, 204);
        }

        private async void btnReboot_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Reboot Coupler? Connection will drop.", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string host = txtIpAddress.Text.Trim();
                string pass = txtPassword.Text;

                UpdateStatus("Rebooting...", Color.Orange);
                btnReboot.Enabled = false;

                await Task.Run(() =>
                {
                    try
                    {
                        using (var ssh = new SshClient(host, "root", pass))
                        {
                            ssh.Connect();
                            ssh.RunCommand("reboot");
                            ssh.Disconnect();
                        }
                        UpdateStatus("Reboot signal sent successfully.", Color.Yellow);
                    }
                    catch (Exception ex)
                    {
                        UpdateStatus("Reboot failed: " + ex.Message, Color.Red);
                        this.Invoke(new Action(() => btnReboot.Enabled = true));
                    }
                });
            }
        }

        private async void btnViewLogs_Click(object sender, EventArgs e)
        {
            string host = txtIpAddress.Text.Trim();
            string pass = txtPassword.Text;
            string logPath = "/home/Gespant/gespant-io-coupler-webserver/src/canopen_master/logs/update.log";

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Please enter IP and Password first.", "Missing Credentials");
                return;
            }

            UpdateStatus("Fetching update logs...", Color.White);

            await Task.Run(() =>
            {
                try
                {
                    using (var ssh = new SshClient(host, "root", pass))
                    {
                        ssh.Connect();

                        // 'tail -n 5' gets the last 5 lines of the file
                        var cmd = ssh.RunCommand($"tail -n 5 {logPath}");

                        string logContent = cmd.Result;

                        this.Invoke(new Action(() =>
                        {
                            if (string.IsNullOrWhiteSpace(logContent))
                            {
                                MessageBox.Show("The log file is empty or does not exist yet.", "Empty Log");
                            }
                            else
                            {
                                MessageBox.Show(logContent, "Last 5 Updates", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }));
                    }
                }
                catch (Exception ex)
                {
                    UpdateStatus("Failed to read logs: " + ex.Message, Color.Red);
                }
            });
        }

        private async void btnReadDeviceInfo_Click(object sender, EventArgs e)
        {
            string host = txtIpAddress.Text.Trim();
            string pass = txtPassword.Text;

            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Please enter IP and Password first.", "Missing Credentials");
                return;
            }

            UpdateStatus("Fetching Device Information...", Color.White);

            await Task.Run(() =>
            {
                try
                {
                    using (var ssh = new SshClient(host, "root", pass))
                    {
                        ssh.Connect();

                        // 1. Fetch Serial, Revision, and Model in one go
                        // This command grabs the lines and formats them slightly for easier reading
                        var cmd = ssh.RunCommand("cat /proc/cpuinfo | grep -E 'Serial|Revision|Model' | sed 's/\t//g'");
                        string rawContent = cmd.Result.Trim();

                        this.Invoke(new Action(() =>
                        {
                            if (string.IsNullOrWhiteSpace(rawContent))
                            {
                                UpdateStatus("Failed to retrieve hardware info.", Color.Yellow);
                                MessageBox.Show("Hardware info could not be read from /proc/cpuinfo.", "Error");
                            }
                            else
                            {
                                UpdateStatus("Device information retrieved.", Color.Lime);

                                // Displaying the multi-line result in a formatted box
                                MessageBox.Show(rawContent,
                                                "Device Identity",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Information);
                            }
                        }));

                        ssh.Disconnect();
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() => {
                        UpdateStatus("Connection Error: " + ex.Message, Color.Red);
                    }));
                }
            });
        }
    }
}