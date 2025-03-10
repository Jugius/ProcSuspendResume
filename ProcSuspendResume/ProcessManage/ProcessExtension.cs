using ProcSuspendResume.ProcessManage;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ProcSuspendResume
{
    public static class ProcessExtension
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);

        private static void Suspend(this Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero)
                {
                    break;
                }
                _ = SuspendThread(pOpenThread);
            }
        }
        private static void Resume(this Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero)
                {
                    break;
                }
                _ = ResumeThread(pOpenThread);
            }
        }
        internal static ProcessInfo Suspend(this ProcessInfo process)
        {
            if (process.State == ProcessState.Suspended) return process;

            Process[] vsProcs = Process.GetProcessesByName(process.Name);
            if (vsProcs == null || vsProcs.Length == 0)
                throw new Exception("Не найден процесс с названием: " + process.Name);

            vsProcs[0].Suspend();
            process.State = ProcessState.Suspended;

            return process;
        }

        internal static ProcessInfo Resume(this ProcessInfo process)
        {
            if (process.State == ProcessState.Running) return process;

            Process[] vsProcs = Process.GetProcessesByName(process.Name);
            if (vsProcs == null || vsProcs.Length == 0)
                throw new Exception("Не найден процесс с названием: " + process.Name);

            vsProcs[0].Resume();
            process.State = ProcessState.Running;

            return process;
        }

        private static void Print(this Process process)
        {
            Console.WriteLine("{0,8}    {1}", process.Id, process.ProcessName);
        }
    }
}
