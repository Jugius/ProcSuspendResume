
using System;

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
    }
}
