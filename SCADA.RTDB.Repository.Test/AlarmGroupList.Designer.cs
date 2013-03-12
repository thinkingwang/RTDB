namespace SCADA.RTDB.Repository.Test
{
    partial class AlarmGroupList
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.alarmGroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alarmGroupDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_AlarmGroupListOK = new System.Windows.Forms.Button();
            this.btn_AlarmGroupListCancel = new System.Windows.Forms.Button();
            this.btn_AlarmGroupListClear = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(471, 331);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "报警组列表";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.alarmGroupName,
            this.alarmGroupDescription});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(465, 311);
            this.dataGridView1.TabIndex = 1;
            // 
            // alarmGroupName
            // 
            this.alarmGroupName.HeaderText = "报警组名称";
            this.alarmGroupName.Name = "alarmGroupName";
            this.alarmGroupName.Width = 150;
            // 
            // alarmGroupDescription
            // 
            this.alarmGroupDescription.HeaderText = "报警组描述";
            this.alarmGroupDescription.Name = "alarmGroupDescription";
            this.alarmGroupDescription.Width = 300;
            // 
            // btn_AlarmGroupListOK
            // 
            this.btn_AlarmGroupListOK.Location = new System.Drawing.Point(295, 351);
            this.btn_AlarmGroupListOK.Name = "btn_AlarmGroupListOK";
            this.btn_AlarmGroupListOK.Size = new System.Drawing.Size(75, 36);
            this.btn_AlarmGroupListOK.TabIndex = 2;
            this.btn_AlarmGroupListOK.Text = "OK";
            this.btn_AlarmGroupListOK.UseVisualStyleBackColor = true;
            this.btn_AlarmGroupListOK.Click += new System.EventHandler(this.btn_AlarmGroupListOK_Click);
            // 
            // btn_AlarmGroupListCancel
            // 
            this.btn_AlarmGroupListCancel.Location = new System.Drawing.Point(391, 351);
            this.btn_AlarmGroupListCancel.Name = "btn_AlarmGroupListCancel";
            this.btn_AlarmGroupListCancel.Size = new System.Drawing.Size(75, 36);
            this.btn_AlarmGroupListCancel.TabIndex = 3;
            this.btn_AlarmGroupListCancel.Text = "Cancel";
            this.btn_AlarmGroupListCancel.UseVisualStyleBackColor = true;
            this.btn_AlarmGroupListCancel.Click += new System.EventHandler(this.btn_AlarmGroupListCancel_Click);
            // 
            // btn_AlarmGroupListClear
            // 
            this.btn_AlarmGroupListClear.Location = new System.Drawing.Point(15, 351);
            this.btn_AlarmGroupListClear.Name = "btn_AlarmGroupListClear";
            this.btn_AlarmGroupListClear.Size = new System.Drawing.Size(75, 36);
            this.btn_AlarmGroupListClear.TabIndex = 4;
            this.btn_AlarmGroupListClear.Text = "Clear";
            this.btn_AlarmGroupListClear.UseVisualStyleBackColor = true;
            this.btn_AlarmGroupListClear.Click += new System.EventHandler(this.btn_AlarmGroupListClear_Click);
            // 
            // AlarmGroupList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 397);
            this.Controls.Add(this.btn_AlarmGroupListClear);
            this.Controls.Add(this.btn_AlarmGroupListCancel);
            this.Controls.Add(this.btn_AlarmGroupListOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AlarmGroupList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择报警组";
            this.Load += new System.EventHandler(this.AlarmGroupList_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn alarmGroupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn alarmGroupDescription;
        private System.Windows.Forms.Button btn_AlarmGroupListOK;
        private System.Windows.Forms.Button btn_AlarmGroupListCancel;
        private System.Windows.Forms.Button btn_AlarmGroupListClear;

    }
}