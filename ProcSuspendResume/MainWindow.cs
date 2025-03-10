using ProcSuspendResume.Hotkeys;
using ProcSuspendResume.ProcessManage;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ProcSuspendResume
{
    public partial class MainWindow : Form
    {
        private ProcessInfo currentProcess;
        private readonly KeyboardHook hook = new KeyboardHook();

        public MainWindow()
        {
            InitializeComponent();

            // register the event that is fired after the key press.
            hook.KeyPressed +=
                new EventHandler<KeyPressedEventArgs>(SuspendResumePressed);

            // register the control + alt + F(i) combination as hot key.            
            int i = 12;
            do
            {
                var key = GetKeyF(i);
                if (hook.TryRegisterHotKey(Hotkeys.ModifierKeys.Control | Hotkeys.ModifierKeys.Alt, key, out _))
                {
                    break;
                }

                i--;
            } while (i > 0);

            if (i > 0)
                lblHotKeyInfo.Text = "Pause/Resume of process hotkey: Ctrl+Alt+F" + i;
            else
            {
                lblHotKeyInfo.ForeColor = System.Drawing.Color.Firebrick;
                lblHotKeyInfo.Text = "Hotkey not registered";
            }

            currentProcess = GetLastUsedProcess();
            UpdateWindow();
        }
        private void SuspendResumePressed(object sender, EventArgs e)
        {
            string errorCaption = "Error";
            try
            {
                var proc = GetCurrentProcess();
                switch (proc.State)
                {
                    case ProcessState.Running:
                        errorCaption = "Pause error";
                        this.currentProcess = proc.Suspend();
                        break;
                    case ProcessState.Suspended:
                        errorCaption = "Resume error";
                        this.currentProcess = proc.Resume();
                        break;
                    default:
                        break;
                }
                UpdateWindow();
            }
            catch (Exception ex)
            {
                ShowException(ex.Message, errorCaption);
            }
        }       
        private void UpdateWindow()
        {
            if (currentProcess == null) return;
            switch (currentProcess.State)
            {
                case ProcessState.Running:
                    btnSuspendResume.Text = "Pause";
                    break;
                case ProcessState.Suspended:
                    btnSuspendResume.Text = "Resume";
                    break;
                default:
                    break;
            }

            if (!string.Equals(currentProcess.Name, txtProcessName.Text, StringComparison.OrdinalIgnoreCase))
                txtProcessName.Text = currentProcess.Name;
        }
        private ProcessInfo GetCurrentProcess()
        {
            if (string.IsNullOrWhiteSpace(txtProcessName.Text))
                throw new Exception("There is no process name in textbox!");

            string name = txtProcessName.Text.Trim().ToLower();
            if (name.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                name = name.Replace(".exe", "");

            if (currentProcess != null && string.Equals(currentProcess.Name, name))
                return currentProcess;

            Process[] vsProcs = Process.GetProcessesByName(name);
            if (vsProcs == null || vsProcs.Length == 0)
                throw new Exception("Not found process: " + name);

            return new ProcessInfo(name) { State = ProcessState.Running };
        }
        private void ShowException(string message, string caption) =>
            MessageBox.Show(owner: this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

        private static Keys GetKeyF(int key)
        {
            switch (key)
            {
                case 1:return Keys.F1;
                case 2: return Keys.F2;
                case 3: return Keys.F3;
                case 4: return Keys.F4;
                case 5: return Keys.F5;
                case 6: return Keys.F6;
                case 7: return Keys.F7;
                case 8: return Keys.F8;
                case 9: return Keys.F9;
                case 10: return Keys.F10;
                case 11: return Keys.F11;
                case 12: return Keys.F12;
                default:
                        throw new ArgumentOutOfRangeException("key");
            }
        }
        private static ProcessInfo GetLastUsedProcess()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "last");
            if (!File.Exists(path)) return null;
            string name = File.ReadAllText(path);
            return new ProcessInfo(name) { State = ProcessState.Running };
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentProcess == null) return;
            string path = Path.Combine(Environment.CurrentDirectory, "last");
            File.WriteAllText(path, currentProcess.Name);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo processStartInfo
                = new ProcessStartInfo(@"https://github.com/Jugius/ProcSuspendResume/releases");
            processStartInfo.UseShellExecute = true;
            Process.Start(processStartInfo);
        }
    }
}
