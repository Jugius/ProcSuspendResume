using System.Windows.Forms;

namespace ProcSuspendResume.Hotkeys
{
    public static class KeyboardHookExtention
    {
        private const ModifierKeys DefaultModifier = ModifierKeys.Control | ModifierKeys.Alt;
        private static readonly Keys[] DefaultKeys = new Keys[] 
            { Keys.F1, Keys.F2, Keys.F3, Keys.F4, Keys.F5, Keys.F6, Keys.F7, Keys.F8, Keys.F9, Keys.F10, Keys.F11, Keys.F12 };

        public static Hotkey RegisterDefaultHotkey(this KeyboardHook hook)
        {
            for (int i = DefaultKeys.Length; i > 0; i--)
            {
                var hotkey = new Hotkey(DefaultModifier, DefaultKeys[i - 1]);
                if (hook.TryRegisterHotKey(hotkey, out _))
                {
                    return hotkey;
                }
            }

            return null;
        }
    }
}
