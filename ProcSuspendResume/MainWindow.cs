using ProcSuspendResume.Hotkeys;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProcSuspendResume
{
    public partial class MainWindow : Form
    {
        private bool IsProcessSuspended = false;
        KeyboardHook hook = new KeyboardHook();
        public MainWindow()
        {
            InitializeComponent();
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

            // show the keys pressed in a label.
            //label1.Text = e.Modifier.ToString() + " + " + e.Key.ToString();
        }
        private void ShowException(string message, string caption) =>
            MessageBox.Show(owner: this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);

        private Process GetProcess(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("There is no process name in textbox!");

            Process[] vsProcs = Process.GetProcessesByName(name);
            if (vsProcs == null || vsProcs.Length == 0)
                throw new Exception("Not found process: " + name);
            return vsProcs[0];
        }
        private static System.Windows.Forms.Keys GetKeyF(int key)
        {
            switch (key)
            {
                case 1:return System.Windows.Forms.Keys.F1;
                case 2: return System.Windows.Forms.Keys.F2;
                case 3: return System.Windows.Forms.Keys.F3;
                case 4: return System.Windows.Forms.Keys.F4;
                case 5: return System.Windows.Forms.Keys.F5;
                case 6: return System.Windows.Forms.Keys.F6;
                case 7: return System.Windows.Forms.Keys.F7;
                case 8: return System.Windows.Forms.Keys.F8;
                case 9: return System.Windows.Forms.Keys.F9;
                case 10: return System.Windows.Forms.Keys.F10;
                case 11: return System.Windows.Forms.Keys.F11;
                case 12: return System.Windows.Forms.Keys.F12;
                default:
                        throw new ArgumentOutOfRangeException("key");
            }
        }
    }
}
