using ProcSuspendResume.Hotkeys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProcSuspendResume
{
    public partial class HotKeySettings: Form
    {
        private readonly KeyboardHook hook;
        private readonly Hotkey backupHotkey;
        private Hotkey currentHotkey;
        public Hotkey Hotkey => currentHotkey;
        public HotKeySettings()
        {
            InitializeComponent();
        }

        public HotKeySettings(KeyboardHook hook, Hotkey hotkey)
        {
            InitializeComponent();
            this.hook = hook;
            this.backupHotkey = hotkey;
            this.currentHotkey = new Hotkey (hotkey.Modifier, hotkey.Key);
            FillForm();
        }       

        private void FillForm()
        {
            chkAlt.Checked = currentHotkey.Modifier.HasFlag(Hotkeys.ModifierKeys.Alt);
            chkControl.Checked = currentHotkey.Modifier.HasFlag(Hotkeys.ModifierKeys.Control);
            chkShift.Checked = currentHotkey.Modifier.HasFlag(Hotkeys.ModifierKeys.Shift);
            txtKey.Text = currentHotkey.Key == Keys.None ? "" : currentHotkey.Key.ToString();
            lblHotKeyInfo.Text = currentHotkey.ToString();
        }
        private bool TryCombineOptions(out Hotkey hotkey)
        {
            try
            {
                var modInt = new List<CheckBox> { chkAlt, chkControl, chkShift }
                .Where(a => a.Checked)
                .Sum(a => int.Parse(a.Tag.ToString()));

                if (modInt == 0) throw new Exception("At least one parameter must be used");

                if (string.IsNullOrWhiteSpace(txtKey.Text)) throw new Exception("Key is required");
                if (!Enum.TryParse(txtKey.Text, out Keys key)) throw new Exception("Can not recognize Key");

                hotkey = new Hotkey((ModifierKeys)modInt, key);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                hotkey = new Hotkey();
                return false;
            }
        }
        private void txtKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                txtKey.Text = string.Empty;
                return;
            }
            e.SuppressKeyPress = true;
            txtKey.Text = e.KeyCode.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!TryCombineOptions(out var options)) return;

            if (options.Equals(backupHotkey))
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            hook.UnregisterAllHotKeys();

            if (hook.TryRegisterHotKey(options, out string error))
            {
                this.DialogResult = DialogResult.OK;
                this.currentHotkey = options;
                this.Close();
                return;
            }
            else
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                hook.RegisterHotKey(this.backupHotkey);
            }
        }        
    }
}
