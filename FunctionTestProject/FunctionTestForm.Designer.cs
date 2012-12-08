namespace FunctionTestProject
{
    partial class FunctionTestForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView_FunctionTest = new System.Windows.Forms.TreeView();
            this.contextMenuStrip_TreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView_Avaiable = new System.Windows.Forms.DataGridView();
            this.Column_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_VariableID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_VarType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column_VarValueType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_InitValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_MinValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_MaxValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_DeadArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_OperateType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column_IsValueSaved = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column_IsParamentSaved = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column_IsAddressable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column_IsEventSaved = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column_ProjectUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip_Variable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RemoveAvariableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_TreeViewSub = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemCreateGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDeleteGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCreateVariable = new System.Windows.Forms.ToolStripMenuItem();
            this.Dig_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Ana_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Str_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemRenameGroup = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip_TreeView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Avaiable)).BeginInit();
            this.contextMenuStrip_Variable.SuspendLayout();
            this.contextMenuStrip_TreeViewSub.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView_FunctionTest);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView_Avaiable);
            this.splitContainer1.Size = new System.Drawing.Size(1777, 586);
            this.splitContainer1.SplitterDistance = 234;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView_FunctionTest
            // 
            this.treeView_FunctionTest.ContextMenuStrip = this.contextMenuStrip_TreeView;
            this.treeView_FunctionTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_FunctionTest.Location = new System.Drawing.Point(0, 0);
            this.treeView_FunctionTest.Name = "treeView_FunctionTest";
            this.treeView_FunctionTest.Size = new System.Drawing.Size(234, 586);
            this.treeView_FunctionTest.TabIndex = 0;
            this.treeView_FunctionTest.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeViewFunctionTestAfterLabelEdit);
            this.treeView_FunctionTest.Click += new System.EventHandler(this.TreeViewFunctionTestClick);
            this.treeView_FunctionTest.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeViewFunctionTestMouseDown);
            // 
            // contextMenuStrip_TreeView
            // 
            this.contextMenuStrip_TreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemRefresh});
            this.contextMenuStrip_TreeView.Name = "contextMenuStrip_TreeView";
            this.contextMenuStrip_TreeView.Size = new System.Drawing.Size(101, 26);
            // 
            // ToolStripMenuItemRefresh
            // 
            this.ToolStripMenuItemRefresh.Name = "ToolStripMenuItemRefresh";
            this.ToolStripMenuItemRefresh.Size = new System.Drawing.Size(100, 22);
            this.ToolStripMenuItemRefresh.Text = "刷新";
            this.ToolStripMenuItemRefresh.Click += new System.EventHandler(this.ToolStripMenuItemRefreshClick);
            // 
            // dataGridView_Avaiable
            // 
            this.dataGridView_Avaiable.AllowUserToAddRows = false;
            this.dataGridView_Avaiable.AllowUserToDeleteRows = false;
            this.dataGridView_Avaiable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Avaiable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_Name,
            this.Column_VariableID,
            this.Column_VarType,
            this.Column_VarValueType,
            this.Column_InitValue,
            this.Column_MinValue,
            this.Column_MaxValue,
            this.Column_Value,
            this.Column_DeadArea,
            this.Column_OperateType,
            this.Column_IsValueSaved,
            this.Column_IsParamentSaved,
            this.Column_IsAddressable,
            this.Column_IsEventSaved,
            this.Column_ProjectUnit,
            this.Column_Description});
            this.dataGridView_Avaiable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Avaiable.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_Avaiable.Name = "dataGridView_Avaiable";
            this.dataGridView_Avaiable.RowTemplate.Height = 23;
            this.dataGridView_Avaiable.Size = new System.Drawing.Size(1539, 586);
            this.dataGridView_Avaiable.TabIndex = 1;
            this.dataGridView_Avaiable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewAvaiableCellValueChanged);
            this.dataGridView_Avaiable.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataGridViewAvaiableMouseDown);
            // 
            // Column_Name
            // 
            this.Column_Name.HeaderText = "变量名";
            this.Column_Name.Name = "Column_Name";
            // 
            // Column_VariableID
            // 
            this.Column_VariableID.HeaderText = "变量ID";
            this.Column_VariableID.Name = "Column_VariableID";
            this.Column_VariableID.ReadOnly = true;
            // 
            // Column_VarType
            // 
            this.Column_VarType.HeaderText = "变量类型";
            this.Column_VarType.Items.AddRange(new object[] {
            "VarNormal",
            "VarStruct",
            "VarRef"});
            this.Column_VarType.Name = "Column_VarType";
            // 
            // Column_VarValueType
            // 
            this.Column_VarValueType.HeaderText = "数据类型";
            this.Column_VarValueType.Name = "Column_VarValueType";
            this.Column_VarValueType.ReadOnly = true;
            this.Column_VarValueType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column_VarValueType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column_InitValue
            // 
            this.Column_InitValue.HeaderText = "初始值";
            this.Column_InitValue.Name = "Column_InitValue";
            // 
            // Column_MinValue
            // 
            this.Column_MinValue.HeaderText = "最小值";
            this.Column_MinValue.Name = "Column_MinValue";
            // 
            // Column_MaxValue
            // 
            this.Column_MaxValue.HeaderText = "最大值";
            this.Column_MaxValue.Name = "Column_MaxValue";
            // 
            // Column_Value
            // 
            this.Column_Value.HeaderText = "值";
            this.Column_Value.Name = "Column_Value";
            // 
            // Column_DeadArea
            // 
            this.Column_DeadArea.HeaderText = "死区";
            this.Column_DeadArea.Name = "Column_DeadArea";
            // 
            // Column_OperateType
            // 
            this.Column_OperateType.HeaderText = "操作类型";
            this.Column_OperateType.Items.AddRange(new object[] {
            "ReadWrite",
            "OnlyRead",
            "OnlyWrite"});
            this.Column_OperateType.Name = "Column_OperateType";
            this.Column_OperateType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column_OperateType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column_IsValueSaved
            // 
            this.Column_IsValueSaved.HeaderText = "是否保存值";
            this.Column_IsValueSaved.Name = "Column_IsValueSaved";
            this.Column_IsValueSaved.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column_IsValueSaved.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column_IsParamentSaved
            // 
            this.Column_IsParamentSaved.HeaderText = "是否保存参数";
            this.Column_IsParamentSaved.Name = "Column_IsParamentSaved";
            this.Column_IsParamentSaved.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column_IsParamentSaved.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column_IsAddressable
            // 
            this.Column_IsAddressable.HeaderText = "是否可以访问";
            this.Column_IsAddressable.Name = "Column_IsAddressable";
            this.Column_IsAddressable.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column_IsAddressable.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column_IsEventSaved
            // 
            this.Column_IsEventSaved.HeaderText = "是否保存事件";
            this.Column_IsEventSaved.Name = "Column_IsEventSaved";
            this.Column_IsEventSaved.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column_IsEventSaved.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column_ProjectUnit
            // 
            this.Column_ProjectUnit.HeaderText = "工程单位";
            this.Column_ProjectUnit.Name = "Column_ProjectUnit";
            // 
            // Column_Description
            // 
            this.Column_Description.HeaderText = "描述";
            this.Column_Description.Name = "Column_Description";
            // 
            // contextMenuStrip_Variable
            // 
            this.contextMenuStrip_Variable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RemoveAvariableToolStripMenuItem});
            this.contextMenuStrip_Variable.Name = "contextMenuStrip_Variable";
            this.contextMenuStrip_Variable.Size = new System.Drawing.Size(125, 26);
            // 
            // RemoveAvariableToolStripMenuItem
            // 
            this.RemoveAvariableToolStripMenuItem.Name = "RemoveAvariableToolStripMenuItem";
            this.RemoveAvariableToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.RemoveAvariableToolStripMenuItem.Text = "移除变量";
            this.RemoveAvariableToolStripMenuItem.Click += new System.EventHandler(this.RemoveAvariableToolStripMenuItemClick);
            // 
            // contextMenuStrip_TreeViewSub
            // 
            this.contextMenuStrip_TreeViewSub.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemCreateGroup,
            this.ToolStripMenuItemDeleteGroup,
            this.ToolStripMenuItemCreateVariable,
            this.ToolStripMenuItemRenameGroup});
            this.contextMenuStrip_TreeViewSub.Name = "contextMenuStrip_TreeViewSub";
            this.contextMenuStrip_TreeViewSub.Size = new System.Drawing.Size(125, 92);
            // 
            // ToolStripMenuItemCreateGroup
            // 
            this.ToolStripMenuItemCreateGroup.Name = "ToolStripMenuItemCreateGroup";
            this.ToolStripMenuItemCreateGroup.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItemCreateGroup.Text = "新建组";
            this.ToolStripMenuItemCreateGroup.Click += new System.EventHandler(this.ToolStripMenuItemCreateGroupClick);
            // 
            // ToolStripMenuItemDeleteGroup
            // 
            this.ToolStripMenuItemDeleteGroup.Name = "ToolStripMenuItemDeleteGroup";
            this.ToolStripMenuItemDeleteGroup.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItemDeleteGroup.Text = "删除组";
            this.ToolStripMenuItemDeleteGroup.Click += new System.EventHandler(this.ToolStripMenuItemDeleteGroupClick);
            // 
            // ToolStripMenuItemCreateVariable
            // 
            this.ToolStripMenuItemCreateVariable.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Dig_ToolStripMenuItem,
            this.Ana_ToolStripMenuItem,
            this.Str_ToolStripMenuItem});
            this.ToolStripMenuItemCreateVariable.Name = "ToolStripMenuItemCreateVariable";
            this.ToolStripMenuItemCreateVariable.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItemCreateVariable.Text = "新建变量";
            // 
            // Dig_ToolStripMenuItem
            // 
            this.Dig_ToolStripMenuItem.Name = "Dig_ToolStripMenuItem";
            this.Dig_ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.Dig_ToolStripMenuItem.Text = "开关变量";
            this.Dig_ToolStripMenuItem.Click += new System.EventHandler(this.DigToolStripMenuItemClick);
            // 
            // Ana_ToolStripMenuItem
            // 
            this.Ana_ToolStripMenuItem.Name = "Ana_ToolStripMenuItem";
            this.Ana_ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.Ana_ToolStripMenuItem.Text = "数字变量";
            this.Ana_ToolStripMenuItem.Click += new System.EventHandler(this.AnaToolStripMenuItemClick);
            // 
            // Str_ToolStripMenuItem
            // 
            this.Str_ToolStripMenuItem.Name = "Str_ToolStripMenuItem";
            this.Str_ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.Str_ToolStripMenuItem.Text = "字符变量";
            this.Str_ToolStripMenuItem.Click += new System.EventHandler(this.StrToolStripMenuItemClick);
            // 
            // ToolStripMenuItemRenameGroup
            // 
            this.ToolStripMenuItemRenameGroup.Name = "ToolStripMenuItemRenameGroup";
            this.ToolStripMenuItemRenameGroup.Size = new System.Drawing.Size(124, 22);
            this.ToolStripMenuItemRenameGroup.Text = "重命名";
            this.ToolStripMenuItemRenameGroup.Click += new System.EventHandler(this.ToolStripMenuItemRenameGroupClick);
            // 
            // FunctionTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1777, 586);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FunctionTestForm";
            this.Text = "功能测试";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FunctionTestFormLoad);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip_TreeView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Avaiable)).EndInit();
            this.contextMenuStrip_Variable.ResumeLayout(false);
            this.contextMenuStrip_TreeViewSub.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView_FunctionTest;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_TreeView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_TreeViewSub;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemRefresh;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCreateGroup;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDeleteGroup;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCreateVariable;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemRenameGroup;
        private System.Windows.Forms.ToolStripMenuItem Dig_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Ana_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Str_ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Variable;
        private System.Windows.Forms.ToolStripMenuItem RemoveAvariableToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView_Avaiable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_VariableID;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column_VarType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_VarValueType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_InitValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_MinValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_MaxValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_DeadArea;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column_OperateType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column_IsValueSaved;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column_IsParamentSaved;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column_IsAddressable;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column_IsEventSaved;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_ProjectUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Description;
    }
}

