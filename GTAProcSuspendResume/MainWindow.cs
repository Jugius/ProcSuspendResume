using GTAProcSuspendResume.Hotkeys;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace GTAProcSuspendResume
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
                lblHotKeyInfo.Text = "Остановка/продолжение процесса висит на хоткее: Ctrl+Alt+F" + i;
            else
            {
                lblHotKeyInfo.ForeColor = System.Drawing.Color.Firebrick;
                lblHotKeyInfo.Text = "Не удалось зарегистрировать хоткей";
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
                MessageBox.Show(ex.Message, "Ошибка остановки", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ex.Message, "Ошибка возобновления", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(ex.Message, "Ошибка возобновления", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // show the keys pressed in a label.
            //label1.Text = e.Modifier.ToString() + " + " + e.Key.ToString();
        }

        private Process GetProcess(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Не указано название процесса!");

            Process[] vsProcs = Process.GetProcessesByName(name);
            if (vsProcs == null || vsProcs.Length == 0)
                throw new Exception("Не найден процесс с названием: " + name);
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
