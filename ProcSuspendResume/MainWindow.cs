﻿using ProcSuspendResume.Hotkeys;
using ProcSuspendResume.ProcessManage;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ProcSuspendResume
{
    public partial class MainWindow : Form
    {
        private readonly KeyboardHook hook;
        private Hotkey hotkey;
        private ProcessInfo currentProcess;

        public MainWindow()
        {
            InitializeComponent();

            hook = new KeyboardHook();                        
            hook.KeyPressed += SuspendResumePressed;

            var key = Hotkey.LoadFromFile();
            if (key != null && hook.TryRegisterHotKey(key, out _))
            {
                hotkey = key;
            }
            else
            { 
                hotkey = hook.RegisterDefaultHotkey();
            }
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
            if (this.hotkey == null)
            {
                lblHotKeyInfo.ForeColor = System.Drawing.Color.Firebrick;
                lblHotKeyInfo.Text = "Hotkey not registered";
            }
            else
            {
                lblHotKeyInfo.ForeColor = System.Drawing.Color.FromArgb(105, 105, 105);
                lblHotKeyInfo.Text = $"Pause/Resume hotkey: {hotkey}";
            }
        }     

        private void Webpage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo processStartInfo
                = new ProcessStartInfo(@"https://oohelp.net/software/processsuspendresume")
                {
                    UseShellExecute = true
                };
            Process.Start(processStartInfo);
        }
        private void HotkeySettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var dlg = new HotKeySettings(this.hook, this.hotkey);
            if (dlg.ShowDialog(owner: this) == DialogResult.OK)
            {
                this.hotkey = dlg.Hotkey;
                UpdateHotkeyDescription();
                Hotkey.SaveToFile(this.hotkey);
            }
        }
        private void ShowException(string message, string caption) =>
            MessageBox.Show(owner: this, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            ProcessInfo.SaveToFile(this.currentProcess);
        }
    }
}
