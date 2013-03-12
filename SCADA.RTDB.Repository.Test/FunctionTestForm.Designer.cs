using System.Windows.Forms;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView_FunctionTest = new System.Windows.Forms.TreeView();
            this.Cms_TreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
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
            this.Cms_VariableControl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RemoveAvariableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CMS_CopyVariable = new System.Windows.Forms.ToolStripMenuItem();
            this.CMS_CutVariable = new System.Windows.Forms.ToolStripMenuItem();
            this.CMS_PasteVariable = new System.Windows.Forms.ToolStripMenuItem();
            this.关联设备ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消关联设备ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Cms_VariableGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
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
            this.alarm_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alram_variable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alarm_config = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alarm_Level = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.alarm_Group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alarm_description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alarmGroup_description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alarmGroup_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cms_AlarmControl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新建报警ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除报警ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Cms_AlarmGroupControl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新建报警组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除报警组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.Cms_TreeView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Avaiable)).BeginInit();
            this.Cms_VariableControl.SuspendLayout();
            this.Cms_VariableGroup.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.Cms_AlarmControl.SuspendLayout();
            this.Cms_AlarmGroupControl.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(1227, 559);
            this.splitContainer1.SplitterDistance = 161;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView_FunctionTest
            // 
            this.treeView_FunctionTest.ContextMenuStrip = this.Cms_TreeView;
            this.treeView_FunctionTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_FunctionTest.Location = new System.Drawing.Point(0, 0);
            this.treeView_FunctionTest.Name = "treeView_FunctionTest";
            this.treeView_FunctionTest.Size = new System.Drawing.Size(161, 559);
            this.treeView_FunctionTest.TabIndex = 0;
            this.treeView_FunctionTest.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeViewFunctionTestAfterLabelEdit);
            this.treeView_FunctionTest.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeViewFunctionTestMouseDown);
            // 
            // Cms_TreeView
            // 
            this.Cms_TreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemRefresh});
            this.Cms_TreeView.Name = "contextMenuStrip_TreeView";
            this.Cms_TreeView.Size = new System.Drawing.Size(101, 26);
            // 
            // ToolStripMenuItemRefresh
            // 
            this.ToolStripMenuItemRefresh.Name = "ToolStripMenuItemRefresh";
            this.ToolStripMenuItemRefresh.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemRefresh.Text = "刷新";
            this.ToolStripMenuItemRefresh.Click += new System.EventHandler(this.ToolStripMenuItemRefreshClick);
            // 
            // dataGridView_Avaiable
            // 
            this.dataGridView_Avaiable.AllowUserToAddRows = false;
            this.dataGridView_Avaiable.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView_Avaiable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
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
            this.dataGridView_Avaiable.Size = new System.Drawing.Size(1062, 559);
            this.dataGridView_Avaiable.TabIndex = 1;
            this.dataGridView_Avaiable.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView_Avaiable_CellBeginEdit);
            this.dataGridView_Avaiable.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewAvaiableCellClick);
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
            // Cms_VariableControl
            // 
            this.Cms_VariableControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RemoveAvariableToolStripMenuItem,
            this.CMS_CopyVariable,
            this.CMS_CutVariable,
            this.CMS_PasteVariable,
            this.关联设备ToolStripMenuItem,
            this.取消关联设备ToolStripMenuItem,
            this.刷新数据ToolStripMenuItem});
            this.Cms_VariableControl.Name = "contextMenuStrip_Variable";
            this.Cms_VariableControl.Size = new System.Drawing.Size(149, 158);
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
            // Cms_VariableGroup
            // 
            this.Cms_VariableGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemCreateGroup,
            this.ToolStripMenuItemDeleteGroup,
            this.ToolStripMenuItemCreateVariable,
            this.ToolStripMenuItemRenameGroup,
            this.CMS_CopyVariableGroup,
            this.CMS_CutVariableGroup,
            this.CMS_PasteVariableGroup});
            this.Cms_VariableGroup.Name = "contextMenuStrip_TreeViewSub";
            this.Cms_VariableGroup.Size = new System.Drawing.Size(125, 158);
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
            this.menuStrip1.Size = new System.Drawing.Size(1227, 27);
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
            // alarm_name
            // 
            this.alarm_name.HeaderText = "报警名称";
            this.alarm_name.Name = "alarm_name";
            this.alarm_name.Width = 200;
            // 
            // alram_variable
            // 
            this.alram_variable.HeaderText = "报警变量";
            this.alram_variable.Name = "alram_variable";
            this.alram_variable.Width = 200;
            // 
            // alarm_config
            // 
            this.alarm_config.HeaderText = "报警配置";
            this.alarm_config.Name = "alarm_config";
            this.alarm_config.ReadOnly = true;
            this.alarm_config.Width = 400;
            // 
            // alarm_Level
            // 
            this.alarm_Level.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.alarm_Level.HeaderText = "报警等级";
            this.alarm_Level.Items.AddRange(new object[] {
            "特急",
            "紧急",
            "一般",
            "不急"});
            this.alarm_Level.Name = "alarm_Level";
            this.alarm_Level.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // alarm_Group
            // 
            this.alarm_Group.HeaderText = "报警组";
            this.alarm_Group.Name = "alarm_Group";
            this.alarm_Group.ReadOnly = true;
            this.alarm_Group.Width = 200;
            // 
            // alarm_description
            // 
            this.alarm_description.HeaderText = "报警描述";
            this.alarm_description.Name = "alarm_description";
            this.alarm_description.Width = 300;
            // 
            // alarmGroup_description
            // 
            this.alarmGroup_description.HeaderText = "报警组描述";
            this.alarmGroup_description.Name = "alarmGroup_description";
            this.alarmGroup_description.Width = 200;
            // 
            // alarmGroup_name
            // 
            this.alarmGroup_name.HeaderText = "报警组名称";
            this.alarmGroup_name.Name = "alarmGroup_name";
            this.alarmGroup_name.Width = 200;
            // 
            // Cms_AlarmControl
            // 
            this.Cms_AlarmControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建报警ToolStripMenuItem,
            this.删除报警ToolStripMenuItem});
            this.Cms_AlarmControl.Name = "Cms_AlarmControl";
            this.Cms_AlarmControl.Size = new System.Drawing.Size(125, 48);
            // 
            // 新建报警ToolStripMenuItem
            // 
            this.新建报警ToolStripMenuItem.Name = "新建报警ToolStripMenuItem";
            this.新建报警ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.新建报警ToolStripMenuItem.Text = "新建报警";
            this.新建报警ToolStripMenuItem.Click += new System.EventHandler(this.新建报警ToolStripMenuItemClick);
            // 
            // 删除报警ToolStripMenuItem
            // 
            this.删除报警ToolStripMenuItem.Name = "删除报警ToolStripMenuItem";
            this.删除报警ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.删除报警ToolStripMenuItem.Text = "删除报警";
            this.删除报警ToolStripMenuItem.Click += new System.EventHandler(this.删除报警ToolStripMenuItem_Click);
            // 
            // Cms_AlarmGroupControl
            // 
            this.Cms_AlarmGroupControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建报警组ToolStripMenuItem,
            this.删除报警组ToolStripMenuItem});
            this.Cms_AlarmGroupControl.Name = "Cms_AlarmGroupControl";
            this.Cms_AlarmGroupControl.Size = new System.Drawing.Size(153, 70);
            // 
            // 新建报警组ToolStripMenuItem
            // 
            this.新建报警组ToolStripMenuItem.Name = "新建报警组ToolStripMenuItem";
            this.新建报警组ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.新建报警组ToolStripMenuItem.Text = "新建报警组";
            this.新建报警组ToolStripMenuItem.Click += new System.EventHandler(this.新建报警组ToolStripMenuItem_Click);
            // 
            // 删除报警组ToolStripMenuItem
            // 
            this.删除报警组ToolStripMenuItem.Name = "删除报警组ToolStripMenuItem";
            this.删除报警组ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.删除报警组ToolStripMenuItem.Text = "删除报警组";
            this.删除报警组ToolStripMenuItem.Click += new System.EventHandler(this.删除报警组ToolStripMenuItem_Click);
            // 
            // FunctionTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 586);
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
            this.Cms_TreeView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Avaiable)).EndInit();
            this.Cms_VariableControl.ResumeLayout(false);
            this.Cms_VariableGroup.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.Cms_AlarmControl.ResumeLayout(false);
            this.Cms_AlarmGroupControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView_FunctionTest;
        private System.Windows.Forms.ContextMenuStrip Cms_TreeView;
        private System.Windows.Forms.ContextMenuStrip Cms_VariableGroup;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemRefresh;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCreateGroup;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDeleteGroup;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCreateVariable;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemRenameGroup;
        private System.Windows.Forms.ToolStripMenuItem Dig_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Ana_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Str_ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip Cms_VariableControl;
        private System.Windows.Forms.ToolStripMenuItem RemoveAvariableToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView_Avaiable;
        private System.Windows.Forms.ToolStripMenuItem CMS_CopyVariable;
        private System.Windows.Forms.ToolStripMenuItem CMS_CutVariable;
        private System.Windows.Forms.ToolStripMenuItem CMS_CopyVariableGroup;
        private System.Windows.Forms.ToolStripMenuItem CMS_PasteVariable;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn alarm_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn alram_variable;
        private System.Windows.Forms.DataGridViewTextBoxColumn alarm_config;
        private System.Windows.Forms.DataGridViewComboBoxColumn alarm_Level;
        private System.Windows.Forms.DataGridViewTextBoxColumn alarm_Group;
        private System.Windows.Forms.DataGridViewTextBoxColumn alarm_description;
        private System.Windows.Forms.DataGridViewTextBoxColumn alarmGroup_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn alarmGroup_description;
        private System.Windows.Forms.ContextMenuStrip Cms_AlarmControl;
        private System.Windows.Forms.ToolStripMenuItem 新建报警ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除报警ToolStripMenuItem;
        private ContextMenuStrip Cms_AlarmGroupControl;
        private ToolStripMenuItem 新建报警组ToolStripMenuItem;
        private ToolStripMenuItem 删除报警组ToolStripMenuItem;
    }
}

