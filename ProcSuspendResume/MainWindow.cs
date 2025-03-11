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
        private HotkeyOptions hotkeyOptions = null;

        public MainWindow()
        {
            InitializeComponent();

            // register the event that is fired after the key press.
            hook.KeyPressed += SuspendResumePressed;
                new EventHandler<KeyPressedEventArgs>(SuspendResumePressed);

            hotkeyOptions = HotkeyOptions.LoadFromFile() ?? hook.RegisterDefaultOptions();
            UpdateHotkeyDescription();

            currentProcess = ProcessInfo.LoadFromFile();
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
        private void UpdateHotkeyDescription()
        {
            if (this.hotkeyOptions == null)
            {
                lblHotKeyInfo.ForeColor = System.Drawing.Color.Firebrick;
                lblHotKeyInfo.Text = "Hotkey not registered";
            }
            else
            {
                lblHotKeyInfo.Text = $"Pause/Resume hotkey: {hotkeyOptions}";
            }
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

        
        private static ProcessInfo GetLastUsedProcess()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "last");
            if (!File.Exists(path)) return null;
            string name = File.ReadAllText(path);
            return new ProcessInfo(name) { State = ProcessState.Running };
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            ProcessInfo.SaveToFile(this.currentProcess);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo processStartInfo
                = new ProcessStartInfo(@"https://github.com/Jugius/ProcSuspendResume/releases");
            processStartInfo.UseShellExecute = true;
            Process.Start(processStartInfo);
        }

        private void btnHotkeySettings_Click(object sender, EventArgs e)
        {            
            var dlg = new HotKeySettings(this.hook, this.hotkeyOptions);
            if (dlg.ShowDialog(owner: this) == DialogResult.OK)
            {
                this.hotkeyOptions = dlg.Options;
                UpdateHotkeyDescription();
                HotkeyOptions.SaveToFile(this.hotkeyOptions);
            }
        }
    }
}
