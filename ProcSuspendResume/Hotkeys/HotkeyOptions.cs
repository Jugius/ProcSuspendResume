using System;
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
            var all = Enum.GetValues(typeof(ModifierKeys))
                .Cast<ModifierKeys>().Where(a => Modifier.HasFlag(a)).ToList();

            List<string> values = new List<string>(2);
            if (all.Count > 0) values.Add(string.Join("+", all));
            if (Key != Keys.None) values.Add(Key.ToString());

            return values.Count > 0 ? string.Join("+", values) : "None";
        }
    }
}
