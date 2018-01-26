namespace WindowsUpdateMonitor
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.TextBoxLog = new System.Windows.Forms.TextBox();
            this.NotifyIconMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.ContextMenuStripMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ExitApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ButtonCheckWindowsUpdateSettings = new System.Windows.Forms.Button();
            this.ContextMenuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBoxLog
            // 
            this.TextBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxLog.Location = new System.Drawing.Point(12, 12);
            this.TextBoxLog.Multiline = true;
            this.TextBoxLog.Name = "TextBoxLog";
            this.TextBoxLog.ReadOnly = true;
            this.TextBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TextBoxLog.Size = new System.Drawing.Size(505, 235);
            this.TextBoxLog.TabIndex = 0;
            this.TextBoxLog.WordWrap = false;
            // 
            // NotifyIconMain
            // 
            this.NotifyIconMain.ContextMenuStrip = this.ContextMenuStripMain;
            this.NotifyIconMain.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIconMain.Icon")));
            this.NotifyIconMain.Text = "Windows Update Monitor";
            this.NotifyIconMain.DoubleClick += new System.EventHandler(this.NotifyIconMain_DoubleClick);
            // 
            // ContextMenuStripMain
            // 
            this.ContextMenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitApplicationToolStripMenuItem});
            this.ContextMenuStripMain.Name = "ContextMenuStrip";
            this.ContextMenuStripMain.Size = new System.Drawing.Size(157, 26);
            // 
            // ExitApplicationToolStripMenuItem
            // 
            this.ExitApplicationToolStripMenuItem.Name = "ExitApplicationToolStripMenuItem";
            this.ExitApplicationToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.ExitApplicationToolStripMenuItem.Text = "Exit Application";
            this.ExitApplicationToolStripMenuItem.Click += new System.EventHandler(this.ExitApplicationToolStripMenuItem_Click);
            // 
            // ButtonCheckWindowsUpdateSettings
            // 
            this.ButtonCheckWindowsUpdateSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCheckWindowsUpdateSettings.Location = new System.Drawing.Point(12, 256);
            this.ButtonCheckWindowsUpdateSettings.Name = "ButtonCheckWindowsUpdateSettings";
            this.ButtonCheckWindowsUpdateSettings.Size = new System.Drawing.Size(505, 24);
            this.ButtonCheckWindowsUpdateSettings.TabIndex = 1;
            this.ButtonCheckWindowsUpdateSettings.Text = "Check Windows Update Settings";
            this.ButtonCheckWindowsUpdateSettings.UseVisualStyleBackColor = true;
            this.ButtonCheckWindowsUpdateSettings.Click += new System.EventHandler(this.ButtonCheckWindowsUpdateSettings_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 292);
            this.Controls.Add(this.ButtonCheckWindowsUpdateSettings);
            this.Controls.Add(this.TextBoxLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Windows Update Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.ContextMenuStripMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBoxLog;
        private System.Windows.Forms.NotifyIcon NotifyIconMain;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStripMain;
        private System.Windows.Forms.ToolStripMenuItem ExitApplicationToolStripMenuItem;
        private System.Windows.Forms.Button ButtonCheckWindowsUpdateSettings;
    }
}

