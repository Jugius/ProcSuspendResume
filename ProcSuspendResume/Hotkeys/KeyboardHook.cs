using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ProcSuspendResume.Hotkeys
{
    public sealed class KeyboardHook : IDisposable
    {
        // Registers a hot key with Windows.
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        // Unregisters the hot key with Windows.
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// Represents the window that is used internally to get the messages.
        /// </summary>
        private class Window : NativeWindow, IDisposable
        {
            private static int WM_HOTKEY = 0x0312;

            public Window()
            {
                // create the handle for the window.
                this.CreateHandle(new CreateParams());
            }

            /// <summary>
            /// Overridden to get the notifications.
            /// </summary>
            /// <param name="m"></param>
            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                // check if we got a hot key pressed.
                if (m.Msg == WM_HOTKEY)
                {
                    // get the keys.
                    Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                    ModifierKeys modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);

                    // invoke the event to notify the parent.
                    if (KeyPressed != null)
                        KeyPressed(this, new KeyPressedEventArgs(modifier, key));
                }
            }

            public event EventHandler<KeyPressedEventArgs> KeyPressed;

            #region IDisposable Members

            public void Dispose()
            {
                this.DestroyHandle();
            }

            #endregion
        }

        private Window _window = new Window();
        private int _currentId;

        public KeyboardHook()
        {
            // register the event of the inner native window.
            _window.KeyPressed += delegate (object sender, KeyPressedEventArgs args)
            {
                if (KeyPressed != null)
                    KeyPressed(this, args);
            };
        }

        public HotkeyOptions RegisterDefaultOptions()
        {
            const ModifierKeys modifier = ModifierKeys.Control | ModifierKeys.Alt;

            int i = 12;
            do
            {
                var key = GetKeyF(i);
                if (TryRegisterHotKey(modifier, key, out _))
                {
                    return new HotkeyOptions { Modifier = modifier, Key = key };
                }
                i--;
            } 
            while (i > 0);

            return null;
        }

        private static Keys GetKeyF(int key)
        {
            switch (key)
            {
                case 1: return Keys.F1;
                case 2: return Keys.F2;
                case 3: return Keys.F3;
                case 4: return Keys.F4;
                case 5: return Keys.F5;
                case 6: return Keys.F6;
                case 7: return Keys.F7;
                case 8: return Keys.F8;
                case 9: return Keys.F9;
                case 10: return Keys.F10;
                case 11: return Keys.F11;
                case 12: return Keys.F12;
                default:
                    throw new ArgumentOutOfRangeException("key");
            }
        }

        /// <summary>
        /// Registers a hot key in the system.
        /// </summary>
        /// <param name="modifier">The modifiers that are associated with the hot key.</param>
        /// <param name="key">The key itself that is associated with the hot key.</param>
        public void RegisterHotKey(ModifierKeys modifier, Keys key)
        {
            // increment the counter.
            _currentId = _currentId + 1;

            // register the hot key.
            if (!RegisterHotKey(_window.Handle, _currentId, (uint)modifier, (uint)key))
                throw new InvalidOperationException("Couldn’t register the hot key.");
        }

        public bool TryRegisterHotKey(ModifierKeys modifier, Keys key, out string error)
        {
            // increment the counter.
            _currentId = _currentId + 1;
            try
            {
                if (!RegisterHotKey(_window.Handle, _currentId, (uint)modifier, (uint)key))
                {
                    error = "Couldn’t register the hot key.";
                    return false;
                }
                else
                {
                    error = string.Empty;
                    return true;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }

        }

        /// <summary>
        /// A hot key has been pressed.
        /// </summary>
        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        #region IDisposable Members

        public void Dispose()
        {
            // unregister all the registered hot keys.
            for (int i = _currentId; i > 0; i--)
            {
                UnregisterHotKey(_window.Handle, i);
            }

            // dispose the inner native window.
            _window.Dispose();
        }

        #endregion
    }
}
