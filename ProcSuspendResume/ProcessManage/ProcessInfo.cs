
using System;
using System.IO;

namespace ProcSuspendResume.ProcessManage
{
    internal class ProcessInfo : IEquatable<ProcessInfo>
    {
        public string Name { get; }
        public ProcessState State { get; set; }
        public ProcessInfo(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public bool Equals(ProcessInfo other)
        {
            return other != null & string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }
        public override bool Equals(object obj)
        {
            return obj is ProcessInfo info && Equals(info);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }


        private const string FileName = "process";
        public static ProcessInfo LoadFromFile()
        {
            string path = Path.Combine(Environment.CurrentDirectory, FileName);
            if (!File.Exists(path)) return null;
            string name = File.ReadAllText(path);
            return new ProcessInfo(name) { State = ProcessState.Running };
        }
        public static void SaveToFile(ProcessInfo process)
        {
            if (process == null) return;
            string path = Path.Combine(Environment.CurrentDirectory, FileName);
            File.WriteAllText(path, process.Name);
        }
    }
}
