using ProcSuspendResume.Hotkeys;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProcSuspendResume
{
    public partial class MainWindow : Form
    {
        private readonly LastSuspendedProcessManager lastProcessManager;
        private readonly KeyboardHook hook = new KeyboardHook();
        private bool IsProcessSuspended = false;
        
        public MainWindow()
        {
            InitializeComponent();

            //try load last process.
            lastProcessManager = new LastSuspendedProcessManager();
            txtProcessName.Text = lastProcessManager.ProcessName;

            // register the event that is fired after the key press.
            hook.KeyPressed +=
                new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);

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
        }

        private void btnSuspend_Click(object sender, EventArgs e)
        {
            try
            {
                var proc = GetProcess(txtProcessName.Text);
                proc.Suspend();
                IsProcessSuspended = true;
                lastProcessManager.ProcessName = proc.ProcessName;
            }
            catch (Exception ex)
            {
                ShowException(ex.Message, "Error");
            }
        }
        private void btnResume_Click(object sender, EventArgs e)
        {
            try
            {
                var proc = GetProcess(txtProcessName.Text);
                proc.Resume();
                IsProcessSuspended = false;
            }
            catch (Exception ex)
            {
                ShowException(ex.Message, "Error");
            }
        }
        void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            try
            {
                if (IsProcessSuspended)
                {
                    btnResume_Click(null, null);
                }
                else
                {
                    btnSuspend_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex.Message, "Error");
            }
        }
        private void ShowException(string message, string caption) =>
            MessageBox.Show(owner: this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

        private Process GetProcess(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("There is no process name in textbox!");

            Process[] vsProcs = Process.GetProcessesByName(name);
            if (vsProcs == null || vsProcs.Length == 0)
                throw new Exception("Not found process: " + name);
            return vsProcs[0];
        }
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo processStartInfo
                = new ProcessStartInfo(@"https://github.com/Jugius/ProcSuspendResume/releases");
            processStartInfo.UseShellExecute = true;
            Process.Start(processStartInfo);
        }
    }
}
