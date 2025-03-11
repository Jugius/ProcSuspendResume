using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ProcSuspendResume.Hotkeys
{
    public class Hotkey : IEquatable<Hotkey>
    {
        public Hotkey(ModifierKeys modifier, Keys key)
        {
            Modifier = modifier;
            Key = key;
        }
        public Hotkey() { }

        public ModifierKeys Modifier { get; }
        public Keys Key { get; }

        public bool Equals(Hotkey other)
        {
            return other != null
                && this.Modifier == other.Modifier
                && this.Key == other.Key;
        }

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


        private const string FileName = "hotkey";
        public static void SaveToFile(Hotkey hotkey)
        {
            string path = Path.Combine(Environment.CurrentDirectory, FileName);
            string[] lines = new string[] { hotkey.Modifier.ToString(), hotkey.Key.ToString() };
            File.WriteAllLines(path, lines);
        }
        public static Hotkey LoadFromFile()
        {
            string path = Path.Combine(Environment.CurrentDirectory, FileName);
            if (!File.Exists(path)) return null;
            var lines = File.ReadAllLines(path);
            var modifier = (ModifierKeys)Enum.Parse(typeof(ModifierKeys), lines[0]);
            var key = (Keys)Enum.Parse(typeof(Keys), lines[1]);
            return new Hotkey(modifier, key);
        }
    }
}
