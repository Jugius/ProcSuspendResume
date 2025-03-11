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
            this.linkWebpage = new System.Windows.Forms.LinkLabel();
            this.linkHotkeySettings = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // txtProcessName
            // 
            this.txtProcessName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtProcessName.Location = new System.Drawing.Point(174, 19);
            this.txtProcessName.Margin = new System.Windows.Forms.Padding(2);
            this.txtProcessName.Name = "txtProcessName";
            this.txtProcessName.Size = new System.Drawing.Size(142, 29);
            this.txtProcessName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "ProcessName:";
            // 
            // btnSuspendResume
            // 
            this.btnSuspendResume.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSuspendResume.Location = new System.Drawing.Point(13, 63);
            this.btnSuspendResume.Margin = new System.Windows.Forms.Padding(2);
            this.btnSuspendResume.Name = "btnSuspendResume";
            this.btnSuspendResume.Size = new System.Drawing.Size(303, 37);
            this.btnSuspendResume.TabIndex = 2;
            this.btnSuspendResume.Text = "Pause";
            this.btnSuspendResume.UseVisualStyleBackColor = true;
            this.btnSuspendResume.Click += new System.EventHandler(this.SuspendResumePressed);
            // 
            // lblHotKeyInfo
            // 
            this.lblHotKeyInfo.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblHotKeyInfo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblHotKeyInfo.Location = new System.Drawing.Point(10, 115);
            this.lblHotKeyInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHotKeyInfo.Name = "lblHotKeyInfo";
            this.lblHotKeyInfo.Size = new System.Drawing.Size(305, 33);
            this.lblHotKeyInfo.TabIndex = 4;
            this.lblHotKeyInfo.Text = "Pause/Resume of rpocess hotkey";
            this.lblHotKeyInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // linkWebpage
            // 
            this.linkWebpage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkWebpage.AutoSize = true;
            this.linkWebpage.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.linkWebpage.Location = new System.Drawing.Point(10, 151);
            this.linkWebpage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkWebpage.Name = "linkWebpage";
            this.linkWebpage.Size = new System.Drawing.Size(66, 19);
            this.linkWebpage.TabIndex = 5;
            this.linkWebpage.TabStop = true;
            this.linkWebpage.Text = "Webpage";
            this.linkWebpage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Webpage_LinkClicked);
            // 
            // linkHotkeySettings
            // 
            this.linkHotkeySettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkHotkeySettings.AutoSize = true;
            this.linkHotkeySettings.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.linkHotkeySettings.Location = new System.Drawing.Point(210, 151);
            this.linkHotkeySettings.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkHotkeySettings.Name = "linkHotkeySettings";
            this.linkHotkeySettings.Size = new System.Drawing.Size(106, 19);
            this.linkHotkeySettings.TabIndex = 6;
            this.linkHotkeySettings.TabStop = true;
            this.linkHotkeySettings.Text = "Hotkey Settings";
            this.linkHotkeySettings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HotkeySettings_LinkClicked);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 180);
            this.Controls.Add(this.linkHotkeySettings);
            this.Controls.Add(this.linkWebpage);
            this.Controls.Add(this.lblHotKeyInfo);
            this.Controls.Add(this.btnSuspendResume);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtProcessName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
        private System.Windows.Forms.LinkLabel linkWebpage;
        private System.Windows.Forms.LinkLabel linkHotkeySettings;
    }
}

