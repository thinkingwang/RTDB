namespace SCADA.RTDB.Repository.Test
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
            this.contextMenuStrip_Variable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RemoveAvariableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CMS_CopyVariable = new System.Windows.Forms.ToolStripMenuItem();
            this.CMS_CutVariable = new System.Windows.Forms.ToolStripMenuItem();
            this.CMS_PasteVariable = new System.Windows.Forms.ToolStripMenuItem();
            this.关联设备ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消关联设备ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_TreeViewSub = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemCreateGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDeleteGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCreateVariable = new System.Windows.Forms.ToolStripMenuItem();
            this.Dig_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Ana_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Str_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemRenameGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.CMS_CopyVariableGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.CMS_CutVariableGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.CMS_PasteVariableGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.搜索ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.按组搜索ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.搜索组的所有变量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.同步数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.停止同步数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip_TreeView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Avaiable)).BeginInit();
            this.contextMenuStrip_Variable.SuspendLayout();
            this.contextMenuStrip_TreeViewSub.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView_FunctionTest);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView_Avaiable);
            this.splitContainer1.Size = new System.Drawing.Size(1777, 559);
            this.splitContainer1.SplitterDistance = 234;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView_FunctionTest
            // 
            this.treeView_FunctionTest.ContextMenuStrip = this.contextMenuStrip_TreeView;
            this.treeView_FunctionTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_FunctionTest.Location = new System.Drawing.Point(0, 0);
            this.treeView_FunctionTest.Name = "treeView_FunctionTest";
            this.treeView_FunctionTest.Size = new System.Drawing.Size(234, 559);
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
            this.dataGridView_Avaiable.Size = new System.Drawing.Size(1539, 559);
            this.dataGridView_Avaiable.TabIndex = 1;
            this.dataGridView_Avaiable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewAvaiableCellValueChanged);
            this.dataGridView_Avaiable.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataGridViewAvaiableMouseDown);
            // 
            // contextMenuStrip_Variable
            // 
            this.contextMenuStrip_Variable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RemoveAvariableToolStripMenuItem,
            this.CMS_CopyVariable,
            this.CMS_CutVariable,
            this.CMS_PasteVariable,
            this.关联设备ToolStripMenuItem,
            this.取消关联设备ToolStripMenuItem,
            this.刷新数据ToolStripMenuItem});
            this.contextMenuStrip_Variable.Name = "contextMenuStrip_Variable";
            this.contextMenuStrip_Variable.Size = new System.Drawing.Size(149, 158);
            // 
            // RemoveAvariableToolStripMenuItem
            // 
            this.RemoveAvariableToolStripMenuItem.Name = "RemoveAvariableToolStripMenuItem";
            this.RemoveAvariableToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.RemoveAvariableToolStripMenuItem.Text = "移除变量";
            this.RemoveAvariableToolStripMenuItem.Click += new System.EventHandler(this.RemoveAvariableToolStripMenuItemClick);
            // 
            // CMS_CopyVariable
            // 
            this.CMS_CopyVariable.Name = "CMS_CopyVariable";
            this.CMS_CopyVariable.Size = new System.Drawing.Size(148, 22);
            this.CMS_CopyVariable.Text = "复制变量";
            this.CMS_CopyVariable.Click += new System.EventHandler(this.CmsCopyVariableClick);
            // 
            // CMS_CutVariable
            // 
            this.CMS_CutVariable.Name = "CMS_CutVariable";
            this.CMS_CutVariable.Size = new System.Drawing.Size(148, 22);
            this.CMS_CutVariable.Text = "剪切变量";
            this.CMS_CutVariable.Click += new System.EventHandler(this.CmsCutVariableClick);
            // 
            // CMS_PasteVariable
            // 
            this.CMS_PasteVariable.Enabled = false;
            this.CMS_PasteVariable.Name = "CMS_PasteVariable";
            this.CMS_PasteVariable.Size = new System.Drawing.Size(148, 22);
            this.CMS_PasteVariable.Text = "粘贴变量";
            this.CMS_PasteVariable.Click += new System.EventHandler(this.CmsPasteVariableClick);
            // 
            // 关联设备ToolStripMenuItem
            // 
            this.关联设备ToolStripMenuItem.Name = "关联设备ToolStripMenuItem";
            this.关联设备ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.关联设备ToolStripMenuItem.Text = "关联设备";
            this.关联设备ToolStripMenuItem.Click += new System.EventHandler(this.关联设备ToolStripMenuItemClick);
            // 
            // 取消关联设备ToolStripMenuItem
            // 
            this.取消关联设备ToolStripMenuItem.Name = "取消关联设备ToolStripMenuItem";
            this.取消关联设备ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.取消关联设备ToolStripMenuItem.Text = "取消关联设备";
            this.取消关联设备ToolStripMenuItem.Click += new System.EventHandler(this.取消关联设备ToolStripMenuItemClick);
            // 
            // 刷新数据ToolStripMenuItem
            // 
            this.刷新数据ToolStripMenuItem.Name = "刷新数据ToolStripMenuItem";
            this.刷新数据ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.刷新数据ToolStripMenuItem.Text = "刷新数据";
            this.刷新数据ToolStripMenuItem.Click += new System.EventHandler(this.刷新数据ToolStripMenuItemClick);
            // 
            // contextMenuStrip_TreeViewSub
            // 
            this.contextMenuStrip_TreeViewSub.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemCreateGroup,
            this.ToolStripMenuItemDeleteGroup,
            this.ToolStripMenuItemCreateVariable,
            this.ToolStripMenuItemRenameGroup,
            this.CMS_CopyVariableGroup,
            this.CMS_CutVariableGroup,
            this.CMS_PasteVariableGroup});
            this.contextMenuStrip_TreeViewSub.Name = "contextMenuStrip_TreeViewSub";
            this.contextMenuStrip_TreeViewSub.Size = new System.Drawing.Size(125, 158);
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
            this.Ana_ToolStripMenuItem.Text = "模拟变量";
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
            // CMS_CopyVariableGroup
            // 
            this.CMS_CopyVariableGroup.Name = "CMS_CopyVariableGroup";
            this.CMS_CopyVariableGroup.Size = new System.Drawing.Size(124, 22);
            this.CMS_CopyVariableGroup.Text = "复制组";
            this.CMS_CopyVariableGroup.Click += new System.EventHandler(this.CmsCopyVariableGroupClick);
            // 
            // CMS_CutVariableGroup
            // 
            this.CMS_CutVariableGroup.Name = "CMS_CutVariableGroup";
            this.CMS_CutVariableGroup.Size = new System.Drawing.Size(124, 22);
            this.CMS_CutVariableGroup.Text = "剪切组";
            this.CMS_CutVariableGroup.Click += new System.EventHandler(this.CmsCutVariableGroupClick);
            // 
            // CMS_PasteVariableGroup
            // 
            this.CMS_PasteVariableGroup.Enabled = false;
            this.CMS_PasteVariableGroup.Name = "CMS_PasteVariableGroup";
            this.CMS_PasteVariableGroup.Size = new System.Drawing.Size(124, 22);
            this.CMS_PasteVariableGroup.Text = "粘贴组";
            this.CMS_PasteVariableGroup.Click += new System.EventHandler(this.CmsPasteVariableGroupClick);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(300, 23);
            // 
            // 搜索ToolStripMenuItem
            // 
            this.搜索ToolStripMenuItem.Name = "搜索ToolStripMenuItem";
            this.搜索ToolStripMenuItem.Size = new System.Drawing.Size(68, 23);
            this.搜索ToolStripMenuItem.Text = "按Id搜索";
            this.搜索ToolStripMenuItem.Click += new System.EventHandler(this.搜索ToolStripMenuItemClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.搜索ToolStripMenuItem,
            this.按组搜索ToolStripMenuItem,
            this.搜索组的所有变量ToolStripMenuItem,
            this.同步数据ToolStripMenuItem,
            this.停止同步数据ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1777, 27);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 按组搜索ToolStripMenuItem
            // 
            this.按组搜索ToolStripMenuItem.Name = "按组搜索ToolStripMenuItem";
            this.按组搜索ToolStripMenuItem.Size = new System.Drawing.Size(68, 23);
            this.按组搜索ToolStripMenuItem.Text = "按组搜索";
            this.按组搜索ToolStripMenuItem.Click += new System.EventHandler(this.按组搜索ToolStripMenuItemClick);
            // 
            // 搜索组的所有变量ToolStripMenuItem
            // 
            this.搜索组的所有变量ToolStripMenuItem.Name = "搜索组的所有变量ToolStripMenuItem";
            this.搜索组的所有变量ToolStripMenuItem.Size = new System.Drawing.Size(116, 23);
            this.搜索组的所有变量ToolStripMenuItem.Text = "搜索组的所有变量";
            this.搜索组的所有变量ToolStripMenuItem.Click += new System.EventHandler(this.搜索组的所有变量ToolStripMenuItemClick);
            // 
            // 同步数据ToolStripMenuItem
            // 
            this.同步数据ToolStripMenuItem.Name = "同步数据ToolStripMenuItem";
            this.同步数据ToolStripMenuItem.Size = new System.Drawing.Size(68, 23);
            this.同步数据ToolStripMenuItem.Text = "同步数据";
            this.同步数据ToolStripMenuItem.Click += new System.EventHandler(this.同步数据ToolStripMenuItemClick);
            // 
            // 停止同步数据ToolStripMenuItem
            // 
            this.停止同步数据ToolStripMenuItem.Enabled = false;
            this.停止同步数据ToolStripMenuItem.Name = "停止同步数据ToolStripMenuItem";
            this.停止同步数据ToolStripMenuItem.Size = new System.Drawing.Size(92, 23);
            this.停止同步数据ToolStripMenuItem.Text = "停止同步数据";
            this.停止同步数据ToolStripMenuItem.Click += new System.EventHandler(this.停止同步数据ToolStripMenuItemClick);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
            // 
            // Column_Name
            // 
            this.Column_Name.HeaderText = "变量名";
            this.Column_Name.Name = "Column_Name";
            // 
            // Column_VariableID
            // 
            this.Column_VariableID.HeaderText = "变量绝对路径";
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
            this.Column_Value.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
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
            "ReadOnly",
            "WriteOnly"});
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
            // FunctionTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1777, 586);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FunctionTestForm";
            this.Text = "功能测试";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FunctionTestFormFormClosing);
            this.Load += new System.EventHandler(this.FunctionTestFormLoad);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip_TreeView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Avaiable)).EndInit();
            this.contextMenuStrip_Variable.ResumeLayout(false);
            this.contextMenuStrip_TreeViewSub.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.ToolStripMenuItem CMS_CopyVariable;
        private System.Windows.Forms.ToolStripMenuItem CMS_CutVariable;
        private System.Windows.Forms.ToolStripMenuItem CMS_PasteVariable;
        private System.Windows.Forms.ToolStripMenuItem CMS_CopyVariableGroup;
        private System.Windows.Forms.ToolStripMenuItem CMS_CutVariableGroup;
        private System.Windows.Forms.ToolStripMenuItem CMS_PasteVariableGroup;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem 搜索ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 按组搜索ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 搜索组的所有变量ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关联设备ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消关联设备ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 同步数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止同步数据ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem 刷新数据ToolStripMenuItem;
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

