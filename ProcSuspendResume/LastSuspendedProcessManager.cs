
namespace ProcSuspendResume
{
    public class LastSuspendedProcessManager
    {
        private const string File = "last";
        private string _processName;
        public string ProcessName
        {
            get => _processName ?? string.Empty;
            set {
                if (string.IsNullOrEmpty(value))
                {
                    _processName = null;
                    if (System.IO.File.Exists(File))
                        System.IO.File.Delete(File);
                }
                else if (value != _processName)
                {
                    _processName = value;
                    System.IO.File.WriteAllText(File, value);
                }
            }
        }
        public LastSuspendedProcessManager()
        {
            if (System.IO.File.Exists(File))
                _processName = System.IO.File.ReadAllText(File);
        }
    }
}
