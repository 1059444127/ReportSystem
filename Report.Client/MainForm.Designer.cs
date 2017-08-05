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
            this.groupSettings = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxReportPath = new System.Windows.Forms.TextBox();
            this.btnBrowsePath = new System.Windows.Forms.Button();
            this.tbxServerIP = new System.Windows.Forms.TextBox();
            this.tbxServerPort = new System.Windows.Forms.TextBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbxConfirmOKReport = new System.Windows.Forms.CheckBox();
            this.iconMenu.SuspendLayout();
            this.groupSettings.SuspendLayout();
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
            this.notifyIcon.BalloonTipText = "报告客户端";
            this.notifyIcon.BalloonTipTitle = "V1.0.0";
            this.notifyIcon.ContextMenuStrip = this.iconMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "报告客户端";
            this.notifyIcon.Visible = true;
            this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.NotifyIcon_BalloonTipClicked);
            this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            this.notifyIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseMove);
            // 
            // iconMenu
            // 
            this.iconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iconMenuShow,
            this.iconMenuHide,
            this.iconMenuProperties,
            this.iconMenuExit});
            this.iconMenu.Name = "iconMenu";
            this.iconMenu.Size = new System.Drawing.Size(99, 92);
            // 
            // iconMenuShow
            // 
            this.iconMenuShow.Name = "iconMenuShow";
            this.iconMenuShow.Size = new System.Drawing.Size(98, 22);
            this.iconMenuShow.Text = "显示";
            this.iconMenuShow.Click += new System.EventHandler(this.iconMenuShow_Click);
            // 
            // iconMenuHide
            // 
            this.iconMenuHide.Name = "iconMenuHide";
            this.iconMenuHide.Size = new System.Drawing.Size(98, 22);
            this.iconMenuHide.Text = "隐藏";
            this.iconMenuHide.Click += new System.EventHandler(this.iconMenuHide_Click);
            // 
            // iconMenuProperties
            // 
            this.iconMenuProperties.Name = "iconMenuProperties";
            this.iconMenuProperties.Size = new System.Drawing.Size(98, 22);
            this.iconMenuProperties.Text = "配置";
            this.iconMenuProperties.Click += new System.EventHandler(this.iconMenuProperties_Click);
            // 
            // iconMenuExit
            // 
            this.iconMenuExit.Name = "iconMenuExit";
            this.iconMenuExit.Size = new System.Drawing.Size(98, 22);
            this.iconMenuExit.Text = "退出";
            this.iconMenuExit.Click += new System.EventHandler(this.iconMenuExit_Click);
            // 
            // timer
            // 
            this.timer.Interval = 500;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // groupSettings
            // 
            this.groupSettings.Controls.Add(this.cbxConfirmOKReport);
            this.groupSettings.Controls.Add(this.btnCancel);
            this.groupSettings.Controls.Add(this.btnApply);
            this.groupSettings.Controls.Add(this.btnBrowsePath);
            this.groupSettings.Controls.Add(this.tbxServerPort);
            this.groupSettings.Controls.Add(this.tbxServerIP);
            this.groupSettings.Controls.Add(this.tbxReportPath);
            this.groupSettings.Controls.Add(this.label3);
            this.groupSettings.Controls.Add(this.label2);
            this.groupSettings.Controls.Add(this.label1);
            this.groupSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupSettings.Location = new System.Drawing.Point(0, 24);
            this.groupSettings.Name = "groupSettings";
            this.groupSettings.Size = new System.Drawing.Size(593, 397);
            this.groupSettings.TabIndex = 2;
            this.groupSettings.TabStop = false;
            this.groupSettings.Text = "设置";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "报告(PDF)临时存储路径:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = " 服务器IP地址:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(84, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = " 服务器端口:";
            // 
            // tbxReportPath
            // 
            this.tbxReportPath.Location = new System.Drawing.Point(163, 62);
            this.tbxReportPath.Name = "tbxReportPath";
            this.tbxReportPath.Size = new System.Drawing.Size(330, 20);
            this.tbxReportPath.TabIndex = 0;
            // 
            // btnBrowsePath
            // 
            this.btnBrowsePath.Location = new System.Drawing.Point(499, 60);
            this.btnBrowsePath.Name = "btnBrowsePath";
            this.btnBrowsePath.Size = new System.Drawing.Size(53, 23);
            this.btnBrowsePath.TabIndex = 1;
            this.btnBrowsePath.Text = "...";
            this.btnBrowsePath.UseVisualStyleBackColor = true;
            // 
            // tbxServerIP
            // 
            this.tbxServerIP.Location = new System.Drawing.Point(163, 94);
            this.tbxServerIP.Name = "tbxServerIP";
            this.tbxServerIP.Size = new System.Drawing.Size(127, 20);
            this.tbxServerIP.TabIndex = 2;
            this.tbxServerIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxServerIP_KeyPress);
            // 
            // tbxServerPort
            // 
            this.tbxServerPort.Location = new System.Drawing.Point(163, 126);
            this.tbxServerPort.Name = "tbxServerPort";
            this.tbxServerPort.Size = new System.Drawing.Size(127, 20);
            this.tbxServerPort.TabIndex = 3;
            this.tbxServerPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxServerPort_KeyPress);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(477, 358);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 6;
            this.btnApply.Text = "应用";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(396, 358);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbxConfirmOKReport
            // 
            this.cbxConfirmOKReport.AutoSize = true;
            this.cbxConfirmOKReport.Location = new System.Drawing.Point(163, 172);
            this.cbxConfirmOKReport.Name = "cbxConfirmOKReport";
            this.cbxConfirmOKReport.Size = new System.Drawing.Size(230, 17);
            this.cbxConfirmOKReport.TabIndex = 4;
            this.cbxConfirmOKReport.Text = "显示并确认服务端成功自动解析的报告";
            this.cbxConfirmOKReport.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(593, 443);
            this.Controls.Add(this.groupSettings);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "报告客户端";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.iconMenu.ResumeLayout(false);
            this.groupSettings.ResumeLayout(false);
            this.groupSettings.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBrowsePath;
        private System.Windows.Forms.TextBox tbxServerPort;
        private System.Windows.Forms.TextBox tbxServerIP;
        private System.Windows.Forms.TextBox tbxReportPath;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.CheckBox cbxConfirmOKReport;
    }
}

