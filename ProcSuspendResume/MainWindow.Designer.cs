namespace ProcSuspendResume
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.txtProcessName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSuspendResume = new System.Windows.Forms.Button();
            this.lblHotKeyInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtProcessName
            // 
            this.txtProcessName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtProcessName.Location = new System.Drawing.Point(178, 10);
            this.txtProcessName.Margin = new System.Windows.Forms.Padding(2);
            this.txtProcessName.Name = "txtProcessName";
            this.txtProcessName.Size = new System.Drawing.Size(88, 29);
            this.txtProcessName.TabIndex = 0;
            this.txtProcessName.Text = "GTA5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Название процесса:";
            // 
            // btnSuspendResume
            // 
            this.btnSuspendResume.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSuspendResume.Location = new System.Drawing.Point(13, 50);
            this.btnSuspendResume.Margin = new System.Windows.Forms.Padding(2);
            this.btnSuspendResume.Name = "btnSuspendResume";
            this.btnSuspendResume.Size = new System.Drawing.Size(252, 37);
            this.btnSuspendResume.TabIndex = 2;
            this.btnSuspendResume.Text = "Приостановить";
            this.btnSuspendResume.UseVisualStyleBackColor = true;
            this.btnSuspendResume.Click += new System.EventHandler(this.SuspendResumePressed);
            // 
            // lblHotKeyInfo
            // 
            this.lblHotKeyInfo.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblHotKeyInfo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblHotKeyInfo.Location = new System.Drawing.Point(10, 132);
            this.lblHotKeyInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHotKeyInfo.Name = "lblHotKeyInfo";
            this.lblHotKeyInfo.Size = new System.Drawing.Size(255, 54);
            this.lblHotKeyInfo.TabIndex = 4;
            this.lblHotKeyInfo.Text = "Остановка/продолжение процесса висит на хоткее: Ctrl+Alt+F12";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 184);
            this.Controls.Add(this.lblHotKeyInfo);
            this.Controls.Add(this.btnSuspendResume);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtProcessName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Process Suspend and Resume";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtProcessName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSuspendResume;
        private System.Windows.Forms.Label lblHotKeyInfo;
    }
}

