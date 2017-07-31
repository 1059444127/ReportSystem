namespace Report.Client
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.iconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.iconMenuShow = new System.Windows.Forms.ToolStripMenuItem();
            this.iconMenuHide = new System.Windows.Forms.ToolStripMenuItem();
            this.iconMenuProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.iconMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.iconMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(593, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 421);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(593, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipText = "Report Client";
            this.notifyIcon.BalloonTipTitle = "Report Client";
            this.notifyIcon.ContextMenuStrip = this.iconMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Report Client";
            this.notifyIcon.Visible = true;
            // 
            // iconMenu
            // 
            this.iconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iconMenuShow,
            this.iconMenuHide,
            this.iconMenuProperties,
            this.iconMenuExit});
            this.iconMenu.Name = "iconMenu";
            this.iconMenu.Size = new System.Drawing.Size(128, 92);
            // 
            // iconMenuShow
            // 
            this.iconMenuShow.Name = "iconMenuShow";
            this.iconMenuShow.Size = new System.Drawing.Size(127, 22);
            this.iconMenuShow.Text = "Show";
            this.iconMenuShow.Click += new System.EventHandler(this.iconMenuShow_Click);
            // 
            // iconMenuHide
            // 
            this.iconMenuHide.Name = "iconMenuHide";
            this.iconMenuHide.Size = new System.Drawing.Size(127, 22);
            this.iconMenuHide.Text = "Hide";
            this.iconMenuHide.Click += new System.EventHandler(this.iconMenuHide_Click);
            // 
            // iconMenuProperties
            // 
            this.iconMenuProperties.Name = "iconMenuProperties";
            this.iconMenuProperties.Size = new System.Drawing.Size(127, 22);
            this.iconMenuProperties.Text = "Properties";
            this.iconMenuProperties.Click += new System.EventHandler(this.iconMenuProperties_Click);
            // 
            // iconMenuExit
            // 
            this.iconMenuExit.Name = "iconMenuExit";
            this.iconMenuExit.Size = new System.Drawing.Size(127, 22);
            this.iconMenuExit.Text = "Exit";
            this.iconMenuExit.Click += new System.EventHandler(this.iconMenuExit_Click);
            // 
            // timer
            // 
            this.timer.Interval = 500;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 443);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "Report Client";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.iconMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip iconMenu;
        private System.Windows.Forms.ToolStripMenuItem iconMenuShow;
        private System.Windows.Forms.ToolStripMenuItem iconMenuHide;
        private System.Windows.Forms.ToolStripMenuItem iconMenuProperties;
        private System.Windows.Forms.ToolStripMenuItem iconMenuExit;
        private System.Windows.Forms.Timer timer;
    }
}

