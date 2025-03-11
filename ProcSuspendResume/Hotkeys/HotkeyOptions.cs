using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProcSuspendResume.Hotkeys
{
    public class HotkeyOptions
    {
        public ModifierKeys Modifier { get; set; }
        public Keys Key { get; set; }

        public override string ToString()
        {
            uint modIint = (uint)Modifier;

            if (modIint == 0 && Key == Keys.None) return "None";
            if (modIint == 0) return Key.ToString();

            var all = new List<ModifierKeys> { ModifierKeys.Control, ModifierKeys.Alt, ModifierKeys.Shift }
                .Where(a => Modifier.HasFlag(a));

            string modStr = string.Join("+", all);

            if (Key == Keys.None) return modStr;

            return $"{modStr}+{Key}";
        }
    }
}
