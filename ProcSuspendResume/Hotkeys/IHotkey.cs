
using System.Windows.Forms;

namespace ProcSuspendResume.Hotkeys
{
    public interface IHotkey
    {
        ModifierKeys Modifier { get; }
        Keys Key { get; }        
    }
}
