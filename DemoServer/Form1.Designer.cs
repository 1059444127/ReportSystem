namespace DemoServer
{
    partial class Form1
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
            this.btnStopReportServer = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbxLog = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Preview = new System.Windows.Forms.DataGridViewButtonColumn();
            this.patientIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.callingIPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reportPathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isOverwriteDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.reportStructBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportStructBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStopReportServer
            // 
            this.btnStopReportServer.Location = new System.Drawing.Point(565, 12);
            this.btnStopReportServer.Name = "btnStopReportServer";
            this.btnStopReportServer.Size = new System.Drawing.Size(125, 33);
            this.btnStopReportServer.TabIndex = 0;
            this.btnStopReportServer.Text = "Stop Report Server";
            this.btnStopReportServer.UseVisualStyleBackColor = true;
            this.btnStopReportServer.Click += new System.EventHandler(this.btnStopReportServer_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 48);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(678, 431);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(670, 405);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Received Reports";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbxLog);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(670, 405);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbxLog
            // 
            this.tbxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxLog.Location = new System.Drawing.Point(3, 3);
            this.tbxLog.Multiline = true;
            this.tbxLog.Name = "tbxLog";
            this.tbxLog.Size = new System.Drawing.Size(664, 399);
            this.tbxLog.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.patientIdDataGridViewTextBoxColumn,
            this.callingIPDataGridViewTextBoxColumn,
            this.reportPathDataGridViewTextBoxColumn,
            this.isOverwriteDataGridViewCheckBoxColumn,
            this.Preview});
            this.dataGridView1.DataSource = this.reportStructBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(664, 399);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Preview
            // 
            this.Preview.HeaderText = "Preview";
            this.Preview.Name = "Preview";
            this.Preview.ReadOnly = true;
            this.Preview.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Preview.Text = "Preview";
            this.Preview.UseColumnTextForButtonValue = true;
            // 
            // patientIdDataGridViewTextBoxColumn
            // 
            this.patientIdDataGridViewTextBoxColumn.DataPropertyName = "PatientId";
            this.patientIdDataGridViewTextBoxColumn.HeaderText = "PatientId";
            this.patientIdDataGridViewTextBoxColumn.Name = "patientIdDataGridViewTextBoxColumn";
            this.patientIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // callingIPDataGridViewTextBoxColumn
            // 
            this.callingIPDataGridViewTextBoxColumn.DataPropertyName = "CallingIP";
            this.callingIPDataGridViewTextBoxColumn.HeaderText = "CallingIP";
            this.callingIPDataGridViewTextBoxColumn.Name = "callingIPDataGridViewTextBoxColumn";
            this.callingIPDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // reportPathDataGridViewTextBoxColumn
            // 
            this.reportPathDataGridViewTextBoxColumn.DataPropertyName = "ReportPath";
            this.reportPathDataGridViewTextBoxColumn.HeaderText = "ReportPath";
            this.reportPathDataGridViewTextBoxColumn.Name = "reportPathDataGridViewTextBoxColumn";
            this.reportPathDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isOverwriteDataGridViewCheckBoxColumn
            // 
            this.isOverwriteDataGridViewCheckBoxColumn.DataPropertyName = "IsOverwrite";
            this.isOverwriteDataGridViewCheckBoxColumn.HeaderText = "IsOverwrite";
            this.isOverwriteDataGridViewCheckBoxColumn.Name = "isOverwriteDataGridViewCheckBoxColumn";
            this.isOverwriteDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // reportStructBindingSource
            // 
            this.reportStructBindingSource.DataSource = typeof(DemoServer.ReportStruct);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 491);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnStopReportServer);
            this.Name = "Form1";
            this.Text = "报告服务端演示";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportStructBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStopReportServer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tbxLog;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource reportStructBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn patientIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn callingIPDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn reportPathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isOverwriteDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn Preview;
    }
}

