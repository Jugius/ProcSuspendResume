using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace GTAProcSuspendResume
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSuspend_Click(object sender, EventArgs e)
        {
            try
            {
                var proc = GetProcess(txtProcessName.Text);
                proc.Suspend();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка остановки", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnResume_Click(object sender, EventArgs e)
        {
            try
            {
                var proc = GetProcess(txtProcessName.Text);
                proc.Resume();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка возобновления", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Process GetProcess(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Не указано название процесса!");

            Process[] vsProcs = Process.GetProcessesByName(name);
            if (vsProcs == null || vsProcs.Length == 0)
                throw new Exception("Не найден процесс с названием: " + name);
            return vsProcs[0];
        }
        
    }
}
