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
        private readonly HotkeyOptions backupOptions;
        private HotkeyOptions currentOptions;
        public HotkeyOptions Options => currentOptions;
        public HotKeySettings()
        {
            InitializeComponent();
        }

        public HotKeySettings(KeyboardHook hook, HotkeyOptions hotkeyOptions)
        {
            InitializeComponent();
            this.hook = hook;
            this.backupOptions = hotkeyOptions;
            this.currentOptions = new HotkeyOptions (hotkeyOptions.Modifier, hotkeyOptions.Key);
            FillForm();
        }       

        private void FillForm()
        {
            chkAlt.Checked = currentOptions.Modifier.HasFlag(Hotkeys.ModifierKeys.Alt);
            chkControl.Checked = currentOptions.Modifier.HasFlag(Hotkeys.ModifierKeys.Control);
            chkShift.Checked = currentOptions.Modifier.HasFlag(Hotkeys.ModifierKeys.Shift);
            txtKey.Text = currentOptions.Key == Keys.None ? "" : currentOptions.Key.ToString();
            lblHotKeyInfo.Text = currentOptions.ToString();
        }
        private bool TryCombineOptions(out HotkeyOptions options)
        {
            try
            {
                var modInt = new List<CheckBox> { chkAlt, chkControl, chkShift }
                .Where(a => a.Checked)
                .Sum(a => int.Parse(a.Tag.ToString()));

                if (modInt == 0) throw new Exception("At least one parameter must be used");

                if (string.IsNullOrWhiteSpace(txtKey.Text)) throw new Exception("Key is required");
                if (!Enum.TryParse(txtKey.Text, out Keys key)) throw new Exception("Can not recognize Key");

                options = new HotkeyOptions((ModifierKeys)modInt, key);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                options = new HotkeyOptions();
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

            if (options.Equals(backupOptions))
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            hook.UnregisterAllHotKeys();

            if (hook.TryRegisterHotKey(options, out string error))
            {
                this.DialogResult = DialogResult.OK;
                this.currentOptions = options;
                this.Close();
                return;
            }
            else
            {
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                hook.RegisterHotKey(this.backupOptions);
            }
        }        
    }
}
