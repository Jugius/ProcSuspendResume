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
            // register the control + alt + F12 combination as hot key.
            hook.RegisterHotKey(Hotkeys.ModifierKeys.Control | Hotkeys.ModifierKeys.Alt,
                Keys.F12);
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
        
    }
}
