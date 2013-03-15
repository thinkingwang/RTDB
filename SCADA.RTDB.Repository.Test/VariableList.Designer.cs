namespace SCADA.RTDB.Repository.Test
{
    partial class VariableList
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView_VariableList = new System.Windows.Forms.TreeView();
            this.dataGridView_VariableList = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AbsolutePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VariableType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_VariableOK = new System.Windows.Forms.Button();
            this.btn_VariableCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_VariableList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(754, 441);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "变量列表";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 17);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView_VariableList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView_VariableList);
            this.splitContainer1.Size = new System.Drawing.Size(748, 421);
            this.splitContainer1.SplitterDistance = 199;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView_VariableList
            // 
            this.treeView_VariableList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_VariableList.Location = new System.Drawing.Point(0, 0);
            this.treeView_VariableList.Name = "treeView_VariableList";
            this.treeView_VariableList.Size = new System.Drawing.Size(199, 421);
            this.treeView_VariableList.TabIndex = 0;
            this.treeView_VariableList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeViewVariableListMouseDown);
            // 
            // dataGridView_VariableList
            // 
            this.dataGridView_VariableList.AllowUserToAddRows = false;
            this.dataGridView_VariableList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_VariableList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.AbsolutePath,
            this.VariableType,
            this.Value,
            this.Description});
            this.dataGridView_VariableList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_VariableList.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_VariableList.MultiSelect = false;
            this.dataGridView_VariableList.Name = "dataGridView_VariableList";
            this.dataGridView_VariableList.RowTemplate.Height = 23;
            this.dataGridView_VariableList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_VariableList.Size = new System.Drawing.Size(545, 421);
            this.dataGridView_VariableList.TabIndex = 0;
            // 
            // name
            // 
            this.name.HeaderText = "变量名称";
            this.name.Name = "Name";
            this.name.ReadOnly = true;
            // 
            // AbsolutePath
            // 
            this.AbsolutePath.HeaderText = "变量绝对路径";
            this.AbsolutePath.Name = "AbsolutePath";
            this.AbsolutePath.ReadOnly = true;
            // 
            // VariableType
            // 
            this.VariableType.HeaderText = "变量类型";
            this.VariableType.Name = "VariableType";
            this.VariableType.ReadOnly = true;
            // 
            // Value
            // 
            this.Value.HeaderText = "变量值";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.HeaderText = "变量描述";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // btn_VariableOK
            // 
            this.btn_VariableOK.Location = new System.Drawing.Point(570, 464);
            this.btn_VariableOK.Name = "btn_VariableOK";
            this.btn_VariableOK.Size = new System.Drawing.Size(75, 39);
            this.btn_VariableOK.TabIndex = 1;
            this.btn_VariableOK.Text = "OK";
            this.btn_VariableOK.UseVisualStyleBackColor = true;
            this.btn_VariableOK.Click += new System.EventHandler(this.btn_VariableOK_Click);
            // 
            // btn_VariableCancel
            // 
            this.btn_VariableCancel.Location = new System.Drawing.Point(673, 464);
            this.btn_VariableCancel.Name = "btn_VariableCancel";
            this.btn_VariableCancel.Size = new System.Drawing.Size(75, 39);
            this.btn_VariableCancel.TabIndex = 2;
            this.btn_VariableCancel.Text = "Cancel";
            this.btn_VariableCancel.UseVisualStyleBackColor = true;
            this.btn_VariableCancel.Click += new System.EventHandler(this.btn_VariableCancel_Click);
            // 
            // VariableList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 515);
            this.Controls.Add(this.btn_VariableCancel);
            this.Controls.Add(this.btn_VariableOK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VariableList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "变量浏览器";
            this.Load += new System.EventHandler(this.VariableListLoad);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_VariableList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView_VariableList;
        private System.Windows.Forms.Button btn_VariableOK;
        private System.Windows.Forms.Button btn_VariableCancel;
        private System.Windows.Forms.DataGridView dataGridView_VariableList;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn AbsolutePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn VariableType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}