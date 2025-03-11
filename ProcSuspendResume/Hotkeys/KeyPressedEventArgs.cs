using System;
using System.Windows.Forms;

namespace ProcSuspendResume.Hotkeys
{
    /// <summary>
    /// Event Args for the event that is fired after the hot key has been pressed.
    /// </summary>
    public class KeyPressedEventArgs : EventArgs, IHotkey
    { 
        public ModifierKeys Modifier { get; }
        public Keys Key { get; }

        internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
        {
            Modifier = modifier;
            Key = key;
        }
    }
}
