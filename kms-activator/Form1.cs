using KMSEmulator;
using KMSEmulator.Logging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace kms_activator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            form1 = this;
        }

        private static Form1 form1 = null;
        private static DebugInfo debugInfo = null;

        class DebugLogger : ILogger
        {
            public void LogMessage(string message, bool timestamp = false)
            {
                if(debugInfo != null)
                {
                    if (timestamp)
                        debugInfo.WriteLine(DateTime.Now.ToString("s") + "\t" + message);
                    else
                        debugInfo.WriteLine(message);
                }
            }
        }

        public static void uncheck_debug_option()
        {
            form1.debug_option.Checked = false;
        }

        private string ReadOutput(Process proc)
        {
            string output = "";
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                if (debugInfo != null)
                    debugInfo.WriteLine(line);
                output += line;
            }
            return output;
        }

        private void StartKMSServer()
        {
            // Set KMS Server Settings
            KMSServerSettings kmsSettings = new KMSServerSettings
            {
                KillProcessOnPort = true,
                GenerateRandomKMSPID = true,
                DefaultKMSHWID = "364F463A8863D35F"
            };

            DebugLogger debugLogger = new DebugLogger();

            // Start KMS Server
            KMSEmulator.KMSServer.Start(debugLogger, kmsSettings);
        }

        private void StopKMSServer()
        {
            KMSEmulator.KMSServer.Stop();
        }

        private void KMSActivate()
        {
            if (windows_option.Checked)
            {
                Thread win = new Thread(() =>
                {
                    WinActivate();
                });
                win.Start();
            }
            else if (office_option.Checked)
            {
                Thread office = new Thread(() =>
                {
                    OfficeActivate();
                });
                office.Start();
            }
        }

        private void CheckActivateState(string status)
        {
            if (status.Contains("successful"))
            {
                Activate.Text = "Done! Click to return";
                Activate.Enabled = true;
                windows_option.Enabled = true;
                office_option.Enabled = true;
                debug_option.Enabled = true;
            }
            else
            {
                Activate.Text = "Retry";
                Activate.Enabled = true;
                windows_option.Enabled = true;
                office_option.Enabled = true;
                debug_option.Enabled = true;
            }
        }

        private void WinActivate()
        {
            // VOL keys
            Dictionary<string, string> winKeys = new Dictionary<string, string>()
            {
                {"Windows 10 Professional", "W269N-WFGWX-YVC9B-4J6C9-T83GX" },
                {"Windows 10 Enterprise", "NPPR9-FWDCX-D2C8J-H872K-2YT43" },
                {"Windows 8.1 Professional", "GCRJD-8NW9H-F2CDX-CCM8D-9D6T9" },
                {"Windows 8.1 Enterprise", "MHF9N-XY6XB-WVXMC-BTDCT-MKKG7" },
                {"Windows 7 Professional", "FJ82H-XT6CR-J8D7P-XQJJ2-GPDD4" },
                {"Windows 7 Enterprise", "33PXH-7Y6KF-2VJC9-XBBR8-HVTHH" },
                {"Windows Server 2016 Standard", "WC2BQ-8NRM3-FDDYY-2BFGV-KHKQY" },
                {"Windows Server 2016 Datacenter", "CB7KF-BWN84-R7R2Y-793K2-8XDDG" },
                {"Windows Server 2012 R2 Server Standard", "D2N9P-3P6X9-2R39C-7RTCD-MDVJX" },
                {"Windows Server 2012 R2 Datacenter", "W3GGN-FT8W3-Y4M27-J84CP-Q3VJ9"},
                {"Windows Server 2008 R2 Standard", "YC6KT-GKW9T-YTKYR-T4X34-R7VHC" },
                {"Windows Server 2008 R2 Enterprise", "489J6-VHDMP-X63PK-3K798-CPX3Y" }
            };

            // which version to activate?
            string key = "";
            string productName = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName", "").ToString();
            if (productName.ToLower().Contains("ultimate"))
            // in case you want to activate Windows 7 Ultimate (it's a retail version, which doesn't support VOL at all)
            {
                MessageBox.Show("Not supported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                foreach (string winversion in winKeys.Keys)
                {
                    if (winversion.Contains(productName))
                    {
                        key = winKeys[winversion];
                        Activate.Text = "Activating " + winversion + "...";
                        break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Windows version not supported", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // make vol
            Process makeVol = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cscript.exe",
                WorkingDirectory = System.Environment.GetEnvironmentVariable("SystemRoot") + @"\System32",
                Arguments = @"//Nologo slmgr.vbs /ipk " + key,

                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            makeVol.StartInfo = startInfo;

            try
            {
                makeVol.Start();
                ReadOutput(makeVol);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Exception caught", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // change KMS server
            startInfo.Arguments = "//Nologo slmgr.vbs /skms " + KMSServer.Text;

            Process kmsServer = new Process
            {
                StartInfo = startInfo
            };
            kmsServer.Start();

            ReadOutput(kmsServer);

            // apply
            startInfo.Arguments = "//Nologo slmgr.vbs /ato";
            Process activate = new Process
            {
                StartInfo = startInfo
            };
            activate.Start();

            CheckActivateState(ReadOutput(activate));
        }

        public void OfficeActivate()
        {
            Process makeVol = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cscript.exe",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            // change KMS server
            startInfo.WorkingDirectory = OsppPath.Text;
            startInfo.Arguments = "//Nologo ospp.vbs /sethst:" + KMSServer.Text;

            Process kmsServer = new Process
            {
                StartInfo = startInfo
            };

            try
            {
                kmsServer.Start();
                ReadOutput(kmsServer);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Exception caught", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // apply
            startInfo.Arguments = "//Nologo ospp.vbs /act";
            Process activate = new Process
            {
                StartInfo = startInfo
            };
            activate.Start();

            CheckActivateState(ReadOutput(activate));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            windows_option.Checked = true;
        }

        private void Office_option_CheckedChanged(object sender, EventArgs e)
        {
            // look for Office's install path, where OSPP.VBS can be found
            try
            {
                Activate.Text = "Activate ";
                RegistryKey localKey;
                if (Environment.Is64BitOperatingSystem)
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                else
                    localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);

                string officepath = "";
                RegistryKey officeBaseKey = localKey.OpenSubKey(@"SOFTWARE\Microsoft\Office");
                if (officeBaseKey.OpenSubKey(@"16.0", false) != null)
                {
                    officepath = officeBaseKey.OpenSubKey(@"16.0\Word\InstallRoot").GetValue("Path").ToString();

                    if (officepath.Contains("root"))
                    // Office 2019 can only be installed via Click-To-Run, therefore we get "C:\Program Files\Microsoft Office\root\Office16\",
                    // otherwise we get "C:\Program Files\Microsoft Office\Office16\"
                    {
                        // OSPP.VBS is still in "C:\Program Files\Microsoft Office\Office16\"
                        officepath = officepath.Replace("root", "");
                        Activate.Text += "Office 2019/2016";
                    }
                    else
                    {
                        Activate.Text += "Office 2016";
                    }
                }
                else if (officeBaseKey.OpenSubKey(@"15.0", false) != null)
                {
                    officepath = officeBaseKey.OpenSubKey(@"15.0\Word\InstallRoot").GetValue("Path").ToString();
                    Activate.Text += "Office 2013";
                }
                else if (officeBaseKey.OpenSubKey(@"14.0", false) != null)
                {
                    officepath = officeBaseKey.OpenSubKey(@"14.0\Word\InstallRoot").GetValue("Path").ToString();
                    Activate.Text += "Office 2010";
                }
                else
                {
                    MessageBox.Show("Only works with Office 2010 and/or above", "Unsupported version", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Activate.Text = "Unsupported version";
                    windows_option.Checked = true;
                    return;
                }
                OsppPath.Text = officepath;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Error detecting Office path", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Debug_option_CheckedChanged(object sender, EventArgs e)
        {
            if(debug_option.Checked)
            {
                if (debugInfo == null)
                {
                    debugInfo = new DebugInfo();
                    debugInfo.Show();
                }
            }
            else
            {
                if(debugInfo != null)
                {
                    debugInfo.Close();
                    debugInfo = null;
                }
            }
        }

        private void Kms_option_CheckedChanged(object sender, EventArgs e)
        {
            if (kms_option.Checked)
                StartKMSServer();
            else
                StopKMSServer();
        }

        private void Activate_Click(object sender, EventArgs e)
        {
            if(KMSServer.Text == "")
            {
                KMSServer.Focus();
                return;
            }

            if (Activate.Text == "Done! Click to return")
            {
                Activate.Text = "Activate";
                windows_option.Enabled = true;
                office_option.Enabled = true;
                debug_option.Enabled = true;
                return;
            }

            if (office_option.Checked)
            {
                DialogResult response = MessageBox.Show("Make sure you are using VOL version", "Proceed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (response == DialogResult.No)
                    return;
            }

            Activate.Text = "Please wait...";
            Activate.Enabled = false;
            windows_option.Enabled = false;
            office_option.Enabled = false;
            debug_option.Enabled = false;

            KMSActivate();
        }        
    }
}