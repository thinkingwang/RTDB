using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SCADA.RTDB.Adaptation;
using SCADA.RTDB.Common.Design;
using SCADA.RTDB.Core.Alarm;
using SCADA.RTDB.Core.Variable;
using SCADA.RTDB.EntityFramework.DbConfig;
using SCADA.RTDB.EntityFramework.Repository.Design;
using SCADA.RTDB.Repository.Test.Properties;
using SCADA.RTDB.SimulationDevice;
using SCADA.RTDB.SimulationDeviceBase;

namespace SCADA.RTDB.Repository.Test
{
    internal enum Displaytype
    {
        Variable,
        Alarm,
        AlarmGroup
    }

    /// <summary>
    /// </summary>
    public partial class FunctionTestForm : FormBase
    {
        #region 私有变量

        // private const string Connectstring = "Data Source=cnwj6iapc006\\sqlexpress;Initial Catalog=VariableDB;User ID=sa;Password=666666";
        private const string Connectstring = "data source=VariableDB.sdf;Password=666666";
        private readonly List<string> AddButtonColumList = new List<string>();


        private readonly List<VariableBase> _variablePasteBoard = new List<VariableBase>();

        /// <summary>
        ///     控制datagridview显示内容状态标志
        /// </summary>
        private Displaytype _displayState = Displaytype.Variable;

        private VariableGroup _groupPasteBoard = new VariableGroup();
        private IAlarmDesignRepository _iAalarmDesignRepository;
        
        private bool _iscopy;
        private string _nameTemp; //临时存放修改名称前的名称

        #endregion

        #region 界面相关

        /// <summary>
        /// </summary>
        public FunctionTestForm()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     窗体初始化加载函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FunctionTestFormLoad(object sender, EventArgs e)
        {
            //初始化实时数据库
            var config = new RepositoryConfig
                {
                    DbNameOrConnectingString = Connectstring,
                    DbType = DataBaseType.SqlCeConnectionFactory
                };

            //初始化变量字典
            _iVariableDesignRepository =
                new VariableDesignRepository(config);
            _iVariableDesignRepository.Load();

            //初始化变量报警
            _iAalarmDesignRepository = new AlarmDesignRepository(config);
            _iAalarmDesignRepository.Load();

            //初始化界面
            treeView_FunctionTest.LabelEdit = false;

            //初始化datagridView需要添加按钮的列
            AddButtonColumList.Add("报警变量");
            AddButtonColumList.Add("报警配置");
            AddButtonColumList.Add("报警组");
            _ContextMenuStrip = Cms_VariableGroup;
            RefreshTree();
        }

        private void FunctionTestFormFormClosing(object sender, FormClosingEventArgs e)
        {
            //关闭所有线程
            DeviceAdapter.Close();
            //保存变量最后值
            _iVariableDesignRepository.ExitWithSaving();
        }

        private void Timer1Tick(object sender, EventArgs e)
        {
            UpdateVariableValue();
        }

        private void UpdateVariableValue()
        {
            if (!DeviceAdapter.IsRunning)
            {
                return;
            }
            try
            {
                for (int i = 0; i < dataGridView_Avaiable.Rows.Count; i++)
                {
                    VariableBase variable =
                        _iVariableDesignRepository.FindVariableByPath(
                            dataGridView_Avaiable.Rows[i].Cells[1].Value.ToString());
                    if (variable.ValueType == VarValuetype.VarBool)
                    {
                        dataGridView_Avaiable.Rows[i].Cells[7].Value = ((DigitalVariable)variable).Value;
                    }
                    else if (variable.ValueType == VarValuetype.VarDouble)
                    {
                        dataGridView_Avaiable.Rows[i].Cells[7].Value = ((AnalogVariable)variable).Value;
                    }
                    else
                    {
                        dataGridView_Avaiable.Rows[i].Cells[7].Value = ((TextVariable)variable).Value;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region TreeView操作方法

        #region 变量组右键菜单

        /// <summary>
        ///     增加组节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemCreateGroupClick(object sender, EventArgs e)
        {
            var node = new TreeNode(GetInitVarName(_currentNode))
                {
                    ContextMenuStrip = Cms_VariableGroup
                };
            _currentNode.Nodes.Add(node);
            _iVariableDesignRepository.AddGroup(node.Text, GetVariableGroupPath(node.Parent.FullPath));
            treeView_FunctionTest.ExpandAll();
        }

        /// <summary>
        ///     获取指定变量组的变量默认名称
        /// </summary>
        /// <param name="curNode">指定变量组</param>
        /// <returns>返回指定变量组的变量默认名称</returns>
        private string GetInitVarName(TreeNode curNode)
        {
            int cnt = 1;
            while (true)
            {
                bool isFind =
                    curNode.Nodes.Cast<TreeNode>()
                           .Any(var => var.Text == Resources.FunctionTestForm_GetInitVarName_node + cnt);
                if (isFind)
                {
                    cnt++;
                }
                else
                {
                    return Resources.FunctionTestForm_GetInitVarName_node + cnt;
                }
            }
        }


        /// <summary>
        ///     删除指定组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemDeleteGroupClick(object sender, EventArgs e)
        {
            try
            {
                _iVariableDesignRepository.RemoveGroup(GetVariableGroupPath(_currentNode.FullPath));
                _currentNode.Remove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        ///     重命名指定组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemRenameGroupClick(object sender, EventArgs e)
        {
            BiginEdit();
        }

        /// <summary>
        ///     添加离散变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DigToolStripMenuItemClick(object sender, EventArgs e)
        {
            var digitalVariable =
                new DigitalVariable(
                    _iVariableDesignRepository.FindGroupByPath(GetVariableGroupPath(_currentNode.FullPath)));
            _iVariableDesignRepository.AddVariable(digitalVariable);
            AddVarToListview(digitalVariable, dataGridView_Avaiable);
        }

        /// <summary>
        ///     添加模拟变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnaToolStripMenuItemClick(object sender, EventArgs e)
        {
            var analogVariable =
                new AnalogVariable(
                    _iVariableDesignRepository.FindGroupByPath(GetVariableGroupPath(_currentNode.FullPath)));
            _iVariableDesignRepository.AddVariable(analogVariable);
            AddVarToListview(analogVariable, dataGridView_Avaiable);
        }

        /// <summary>
        ///     添加字符串变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StrToolStripMenuItemClick(object sender, EventArgs e)
        {
            var stringVariable =
                new TextVariable(_iVariableDesignRepository.FindGroupByPath(GetVariableGroupPath(_currentNode.FullPath)));
            _iVariableDesignRepository.AddVariable(stringVariable);
            AddVarToListview(stringVariable, dataGridView_Avaiable);
        }

        /// <summary>
        ///     复制变量组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmsCopyVariableGroupClick(object sender, EventArgs e)
        {
            _groupPasteBoard = null;
            if (_currentNode == null)
            {
                return;
            }
            _groupPasteBoard = _iVariableDesignRepository.FindGroupByPath(GetVariableGroupPath(_currentNode.FullPath));

            if (_groupPasteBoard == null)
            {
                return;
            }
            _iscopy = true;
            CMS_PasteVariableGroup.Enabled = true;
        }

        /// <summary>
        ///     剪切变量组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmsCutVariableGroupClick(object sender, EventArgs e)
        {
            _groupPasteBoard = null;
            if (_currentNode == null)
            {
                return;
            }
            _groupPasteBoard = _iVariableDesignRepository.FindGroupByPath(GetVariableGroupPath(_currentNode.FullPath));

            if (_groupPasteBoard == null)
            {
                return;
            }
            _iscopy = false;
            CMS_PasteVariableGroup.Enabled = true;
        }

        #endregion

        /// <summary>
        ///     组节点进入可编辑状态
        /// </summary>
        private void BiginEdit()
        {
            if (_currentNode != null && _currentNode.Parent != null)
            {
                treeView_FunctionTest.SelectedNode = _currentNode;
                treeView_FunctionTest.LabelEdit = true;
                if (!_currentNode.IsEditing)
                {
                    _currentNode.BeginEdit();
                }
            }
            else
            {
                MessageBox.Show(Resources.FunctionTestForm_ToolStripMenuItemRenameGroupClick_,
                                Resources.FunctionTestForm_BiginEdit_Invalid_selection);
            }
        }

        /// <summary>
        ///     组节点退出编辑状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeViewFunctionTestAfterLabelEdit(object sender,
                                                        NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (e.Label.Length > 0)
                {
                    if (e.Label.IndexOfAny(new[] {'@', '.', ',', '!'}) == -1)
                    {
                        try
                        {
                            VariableGroup varGroup =
                                _iVariableDesignRepository.FindGroupByPath(GetVariableGroupPath(_currentNode.FullPath));
                            _iVariableDesignRepository.RenameGroup(e.Label, GetVariableGroupPath(_currentNode.FullPath));
                            if (varGroup != null)
                            {
                                dataGridView_Avaiable.Rows.Clear();
                                foreach (VariableBase variable in varGroup.ChildVariables)
                                {
                                    AddVarToListview(variable, dataGridView_Avaiable);
                                }
                            }

                            // Stop editing without canceling the label change.
                            e.Node.EndEdit(false);
                        }
                        catch
                        {
                            e.CancelEdit = true;
                            MessageBox.Show(
                                Resources.FunctionTestForm_TreeViewFunctionTestAfterLabelEdit_NodeNameIsExist,
                                Resources.FunctionTestForm_TreeViewFunctionTestAfterLabelEdit_Node_Label_Edit);
                            e.Node.BeginEdit();
                        }
                    }
                    else
                    {
                        /* Cancel the label edit action, inform the user, and 
                           place the node in edit mode again. */
                        e.CancelEdit = true;
                        MessageBox.Show(Resources.FunctionTestForm_TreeViewFunctionTest_AfterLabelEdit_,
                                        Resources.FunctionTestForm_TreeViewFunctionTestAfterLabelEdit_Node_Label_Edit);
                        e.Node.BeginEdit();
                    }
                }
                else
                {
                    /* Cancel the label edit action, inform the user, and 
                       place the node in edit mode again. */
                    e.CancelEdit = true;
                    MessageBox.Show(Resources.FunctionTestForm_TreeViewFunctionTest_AfterLabelEdit_ContentIsBlank,
                                    Resources.FunctionTestForm_TreeViewFunctionTestAfterLabelEdit_Node_Label_Edit);
                    e.Node.BeginEdit();
                }

                treeView_FunctionTest.LabelEdit = true;
            }
        }

        /// <summary>
        ///     树点击事件处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeViewFunctionTestMouseDown(object sender, MouseEventArgs e)
        {
            TreeNode node = treeView_FunctionTest.GetNodeAt(e.X, e.Y);

            if (node == null)
            {
                return;
            }

            if (node.Text == "变量报警")
            {
                InitVariableUi(Displaytype.Alarm);
            }
            else if (node.Text == "变量报警组")
            {
                InitVariableUi(Displaytype.AlarmGroup);
            }
            else
            {
                InitVariableUi(Displaytype.Variable);
                _currentNode = node;
            }

            RefreshDataGridView();
        }

        private void RefreshDataGridView()
        {
            switch (_displayState)
            {
                case Displaytype.Alarm:
                    dataGridView_Avaiable.Rows.Clear();
                    foreach (AlarmBase alarmBase in _iAalarmDesignRepository.FindAlarms())
                    {
                        var alarmGridViewRow = new DataGridViewRow();
                        alarmGridViewRow.CreateCells(dataGridView_Avaiable);
                        alarmGridViewRow.Cells[0].Value = alarmBase.Name;
                        alarmGridViewRow.Cells[1].Value = alarmBase.Variable == null ? null : alarmBase.Variable.Name;
                        alarmGridViewRow.Cells[2].Value = alarmBase;
                        alarmGridViewRow.Cells[3].Value = alarm_Level.Items[alarmBase.Level];
                        alarmGridViewRow.Cells[4].Value = alarmBase.Group == null ? null : alarmBase.Group.Name;
                        alarmGridViewRow.Cells[5].Value = alarmBase.Description;
                        dataGridView_Avaiable.Rows.Add(alarmGridViewRow);
                    }
                    break;
                case Displaytype.AlarmGroup:
                    dataGridView_Avaiable.Rows.Clear();
                    foreach (AlarmGroup alarmGroup in _iAalarmDesignRepository.FindAlarmGroups())
                    {
                        var alarmGridViewRow = new DataGridViewRow();
                        alarmGridViewRow.CreateCells(dataGridView_Avaiable);
                        alarmGridViewRow.Cells[0].Value = alarmGroup.Name;
                        alarmGridViewRow.Cells[1].Value = alarmGroup.Description;
                        dataGridView_Avaiable.Rows.Add(alarmGridViewRow);
                    }
                    break;
                case Displaytype.Variable:
                    treeView_FunctionTest.SelectedNode = _currentNode;
                    VariableGroup varGroup =
                        _iVariableDesignRepository.FindGroupByPath(GetVariableGroupPath(_currentNode.FullPath));
                    if (varGroup == null)
                    {
                        return;
                    }
                    dataGridView_Avaiable.Rows.Clear();
                    foreach (VariableBase variable in _iVariableDesignRepository.FindVariables(varGroup.AbsolutePath))
                    {
                        AddVarToListview(variable, dataGridView_Avaiable);
                    }
                    break;
            }
        }

        #region 树形控件右键菜单

        /// <summary>
        ///     更新树
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemRefreshClick(object sender, EventArgs e)
        {
            RefreshTree();
        }

        /// <summary>
        ///     更新树方法体
        /// </summary>
        private void RefreshTree()
        {
            //添加变量组
            RefreshTreeView(treeView_FunctionTest, _iVariableDesignRepository.FindGroupByPath(null));
            _currentNode = treeView_FunctionTest.TopNode;

            RefreshDataGridView();

            //添加变量报警
            treeView_FunctionTest.Nodes.Add("变量报警");
            //添加变量报警组
            treeView_FunctionTest.Nodes.Add("变量报警组");
            treeView_FunctionTest.ExpandAll();
        }

        #endregion

        #endregion

        #region DataGridView操作变量的方法

        private void InitVariableUi(Displaytype state)
        {
            if (_displayState == state)
            {
                return;
            }
            _displayState = state;
            dataGridView_Avaiable.Columns.Clear();
            switch (state)
            {
                case Displaytype.Alarm:
                    dataGridView_Avaiable.Columns.AddRange(new DataGridViewColumn[]
                        {
                            alarm_name,
                            alram_variable,
                            alarm_config,
                            alarm_Level,
                            alarm_Group,
                            alarm_description
                        });
                    break;
                case Displaytype.AlarmGroup:
                    dataGridView_Avaiable.Columns.AddRange(new DataGridViewColumn[]
                        {
                            alarmGroup_name,
                            alarmGroup_description
                        });
                    break;
                case Displaytype.Variable:
                    dataGridView_Avaiable.Columns.AddRange(new DataGridViewColumn[]
                        {
                            name,
                            AbsolutePath,
                            VariableType,
                            ValueType,
                            InitValue,
                            MinValue,
                            MaxValue,
                            Value,
                            DeadBand,
                            OperateProperty,
                            IsValueSaved,
                            IsInitValueSaved,
                            IsAddressable,
                            IsRecordEvent,
                            EngineeringUnit,
                            Description
                        });
                    break;
            }
        }


        /// <summary>
        ///     DataGridView鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewAvaiableMouseDown(object sender, MouseEventArgs e)
        {
            dataGridView_Avaiable.Controls.Clear(); //移除所有控件   
            if (e.Button != MouseButtons.Right)
            {
                return;
            }
            //报警的相关操作
            if (_displayState == Displaytype.Alarm)
            {
                Cms_AlarmControl.Show(MousePosition);
                return;
            }
            //报警组的相关操作
            if (_displayState == Displaytype.AlarmGroup)
            {
                Cms_AlarmGroupControl.Show(MousePosition);
                return;
            }

            //变量的相关操作
            try
            {
                DataGridViewRow rowSelected =
                    dataGridView_Avaiable.Rows[dataGridView_Avaiable.HitTest(e.X, e.Y).RowIndex];
                if (!dataGridView_Avaiable.SelectedRows.Contains(rowSelected))
                {
                    foreach (DataGridViewRow row in dataGridView_Avaiable.SelectedRows)
                    {
                        row.Selected = false;
                    }
                }
                rowSelected.Selected = true;
                CMS_CopyVariable.Enabled = true;
                CMS_CutVariable.Enabled = true;
                RemoveAvariableToolStripMenuItem.Enabled = true;
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch
                // ReSharper restore EmptyGeneralCatchClause
            {
                CMS_CopyVariable.Enabled = false;
                CMS_CutVariable.Enabled = false;
                RemoveAvariableToolStripMenuItem.Enabled = false;
            }
            finally
            {
                Cms_VariableControl.Show(MousePosition);
            }
        }

        /// <summary>
        ///     单元格内容改变出发的事件处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewAvaiableCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_Avaiable.SelectedCells.Count <= 0)
            {
                return;
            }
            try
            {
                switch (_displayState)
                {
                    case Displaytype.Alarm:
                        if (ChangeAlarmProperty(e)) return;
                        break;
                    case Displaytype.AlarmGroup:
                        if (ChangeAlarmGroupProperty(e)) return;
                        break;
                    case Displaytype.Variable:
                        if (ChangeVariableProperty(e)) return;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            RefreshDataGridView();
        }


        /// <summary>
        ///     修改变量报警属性
        /// </summary>
        /// <param name="e">需要修改的单元</param>
        /// <returns>不需要刷新界面dataGridView返回true，需要刷新界面dataGridView返回false</returns>
        private bool ChangeAlarmGroupProperty(DataGridViewCellEventArgs e)
        {
            DataGridViewCell currentCell = dataGridView_Avaiable.SelectedCells[0];
            DataGridViewRow currentRow = dataGridView_Avaiable.Rows[currentCell.RowIndex];

            try
            {
                switch (e.ColumnIndex)
                {
                    case 0: //name
                        _iAalarmDesignRepository.EditAlarmGroup(_nameTemp, "name",
                                                                currentRow.Cells[e.ColumnIndex].Value.ToString());
                        break;
                    case 1: //descript
                        _iAalarmDesignRepository.EditAlarmGroup(currentRow.Cells[0].Value.ToString(), "Description",
                                                                currentRow.Cells[e.ColumnIndex].Value.ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return false;
        }

        /// <summary>
        ///     修改变量报警属性
        /// </summary>
        /// <param name="e">需要修改的单元</param>
        /// <returns>不需要刷新界面dataGridView返回true，需要刷新界面dataGridView返回false</returns>
        private bool ChangeAlarmProperty(DataGridViewCellEventArgs e)
        {
            DataGridViewCell currentCell = dataGridView_Avaiable.SelectedCells[0];
            DataGridViewRow currentRow = dataGridView_Avaiable.Rows[currentCell.RowIndex];

            try
            {
                switch (e.ColumnIndex)
                {
                    case 0: //name
                        _iAalarmDesignRepository.EditAlarm(_nameTemp, "name",
                                                           currentRow.Cells[e.ColumnIndex].Value.ToString());
                        break;
                    case 1: //variable
                        object variable = currentRow.Cells[e.ColumnIndex].Value;
                        if (variable == null)
                        {
                            _iAalarmDesignRepository.EditAlarm(currentRow.Cells[0].Value.ToString(), "Variable", null);
                            return false;
                        }
                        VariableBase alarm = _iVariableDesignRepository.FindVariableByPath(variable.ToString());
                        if (alarm == null)
                        {
                            MessageBox.Show("当前变量不存在");
                            return false;
                        }
                        _iAalarmDesignRepository.EditAlarm(currentRow.Cells[0].Value.ToString(), "Variable", alarm);

                        break;
                    case 2: //config
                        break;
                    case 3: //level
                        _iAalarmDesignRepository.EditAlarm(currentRow.Cells[0].Value.ToString(), "Level",
                                                           alarm_Level.Items.IndexOf(
                                                               currentRow.Cells[e.ColumnIndex].Value.ToString()));
                        break;
                    case 4: //group
                        break;
                    case 5: //descript
                        _iAalarmDesignRepository.EditAlarm(currentRow.Cells[0].Value.ToString(), "Description",
                                                           currentRow.Cells[e.ColumnIndex].Value.ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return false;
        }

        /// <summary>
        ///     修改变量属性
        /// </summary>
        /// <param name="e">需要修改的单元</param>
        /// <returns>不需要刷新界面dataGridView返回true，需要刷新界面dataGridView返回false</returns>
        private bool ChangeVariableProperty(DataGridViewCellEventArgs e)
        {
            DataGridViewCell currentCell = dataGridView_Avaiable.SelectedCells[0];
            DataGridViewRow currentRow = dataGridView_Avaiable.Rows[currentCell.RowIndex];
            switch (e.ColumnIndex)
            {
                case 0: //name
                    _iVariableDesignRepository.EditVariable(currentRow.Cells[1].Value.ToString(), "Name",
                                                            currentRow.Cells[e.ColumnIndex].Value.ToString());
                    break;
                case 2: //variableType
                    _iVariableDesignRepository.EditVariable(currentRow.Cells[1].Value.ToString(), "VariableType",
                                                            VariableType.Items.IndexOf(
                                                                currentRow.Cells[e.ColumnIndex].Value.ToString()));
                    break;
                case 3: //valueType
                    break;
                case 4: //initValue
                    _iVariableDesignRepository.EditVariable(currentRow.Cells[1].Value.ToString(), "InitValue",
                                                            currentRow.Cells[e.ColumnIndex].Value.ToString());
                    break;
                case 5: //minValue
                    _iVariableDesignRepository.EditVariable(currentRow.Cells[1].Value.ToString(), "MinValue",
                                                            currentRow.Cells[e.ColumnIndex].Value.ToString());
                    break;
                case 6: //maxValue
                    _iVariableDesignRepository.EditVariable(currentRow.Cells[1].Value.ToString(), "MaxValue",
                                                            currentRow.Cells[e.ColumnIndex].Value.ToString());
                    break;
                case 7: //value
                    _iVariableDesignRepository.EditVariable(currentRow.Cells[1].Value.ToString(), "Value",
                                                            currentRow.Cells[e.ColumnIndex].Value.ToString());
                    break;
                case 8: //deadbaud
                    _iVariableDesignRepository.EditVariable(currentRow.Cells[1].Value.ToString(), "DeadBand",
                                                            currentRow.Cells[e.ColumnIndex].Value.ToString());
                    break;
                case 9: //operateType
                    _iVariableDesignRepository.EditVariable(currentRow.Cells[1].Value.ToString(), "OperateProperty",
                                                            OperateProperty.Items.IndexOf(
                                                                currentRow.Cells[e.ColumnIndex].Value.ToString()));
                    break;
                case 10: //IsValueSaved
                    _iVariableDesignRepository.EditVariable(currentRow.Cells[1].Value.ToString(), "IsValueSaved",
                                                            currentRow.Cells[e.ColumnIndex].Value.ToString());
                    break;
                case 11: //IsInitValueSaved
                    _iVariableDesignRepository.EditVariable(currentRow.Cells[1].Value.ToString(), "IsInitValueSaved",
                                                            currentRow.Cells[e.ColumnIndex].Value.ToString());
                    break;
                case 12: //是否允许访问
                    _iVariableDesignRepository.EditVariable(currentRow.Cells[1].Value.ToString(), "IsAddressable",
                                                            currentRow.Cells[e.ColumnIndex].Value.ToString());
                    break;
                case 13: //是否保存事件
                    _iVariableDesignRepository.EditVariable(currentRow.Cells[1].Value.ToString(), "IsRecordEvent",
                                                            currentRow.Cells[e.ColumnIndex].Value.ToString());
                    break;
                case 14: //工程单位
                    _iVariableDesignRepository.EditVariable(currentRow.Cells[1].Value.ToString(), "EngineeringUnit",
                                                            currentRow.Cells[e.ColumnIndex].Value.ToString());
                    break;
                case 15: //描述
                    _iVariableDesignRepository.EditVariable(currentRow.Cells[1].Value.ToString(), "Description",
                                                            currentRow.Cells[e.ColumnIndex].Value.ToString());
                    break;
            }
            
            return false;
        }

        private void DataGridViewAvaiableCellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView_Avaiable.Controls.Clear(); //移除所有控件   

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 &&
                AddButtonColumList.Contains(dataGridView_Avaiable.Columns[e.ColumnIndex].HeaderText))
            {
                var btn = new DataGridViewCellButton(e, dataGridView_Avaiable.Columns[e.ColumnIndex].HeaderText)
                    {
                        Width = dataGridView_Avaiable.GetCellDisplayRectangle(e.ColumnIndex,
                                                                              e.RowIndex, true).Height,
                        Height = dataGridView_Avaiable.GetCellDisplayRectangle(e.ColumnIndex,
                                                                               e.RowIndex, true).Height
                    }; //创建Buttonbtn   

                btn.Click += BtnClick; //为btn添加单击事件   
                dataGridView_Avaiable.Controls.Add(btn); //dataGridView中添加控件btn   

                //设置btn显示位置 
                btn.Location =
                    new Point(
                        ((dataGridView_Avaiable.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Right) -
                         (btn.Width)), dataGridView_Avaiable.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Y);
            }
        }

        private void dataGridView_Avaiable_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewCell currentCell = dataGridView_Avaiable.SelectedCells[0];
            DataGridViewRow currentRow = dataGridView_Avaiable.Rows[currentCell.RowIndex];
            _nameTemp = currentRow.Cells[0].Value.ToString();
        }

        #region 变量操作右键菜单

        /// <summary>
        ///     从DataGridView中移除指定变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveAvariableToolStripMenuItemClick(object sender, EventArgs e)
        {
            var collenction = new DataGridViewRow[dataGridView_Avaiable.SelectedRows.Count];
            dataGridView_Avaiable.SelectedRows.CopyTo(collenction, 0);
            RemoveAvariale(collenction);
        }

        /// <summary>
        ///     移除变量的方法体
        /// </summary>
        /// <param name="collenction"></param>
        private void RemoveAvariale(IEnumerable<DataGridViewRow> collenction)
        {
            foreach (DataGridViewRow row in collenction)
            {
                _iVariableDesignRepository.RemoveVariable(row.Cells[0].Value.ToString(),
                                                          GetVariableGroupPath(_currentNode.FullPath));
                dataGridView_Avaiable.Rows.Remove(row);
            }
        }

        /// <summary>
        ///     复制变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmsCopyVariableClick(object sender, EventArgs e)
        {
            CopyVariables(true);
        }

        private void CopyVariables(bool isCopy)
        {
            _variablePasteBoard.Clear();
            foreach (DataGridViewRow c in dataGridView_Avaiable.SelectedRows)
            {
                VariableGroup variableGroup =
                    _iVariableDesignRepository.FindGroupByPath(GetVariableGroupPath(_currentNode.FullPath));
                VariableBase variable = variableGroup.ChildVariables.Find(
                    m =>
                    m.AbsolutePath == c.Cells[1].Value.ToString());
                if (variable != null)
                {
                    _variablePasteBoard.Add(variable);
                }
            }

            if (_variablePasteBoard == null)
            {
                return;
            }
            _iscopy = isCopy;
            CMS_PasteVariable.Enabled = true;
        }

        /// <summary>
        ///     剪切变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmsCutVariableClick(object sender, EventArgs e)
        {
            CopyVariables(false);
        }

        /// <summary>
        ///     粘贴变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmsPasteVariableClick(object sender, EventArgs e)
        {
            foreach (VariableBase variableBase in _variablePasteBoard)
            {
                string str = _iVariableDesignRepository.PasteVariable(variableBase,
                                                                      GetVariableGroupPath(_currentNode.FullPath),
                                                                      _iscopy);
                if (str != variableBase.Name)
                {
                    switch ((new PasteMessage(str)).ShowDialog())
                    {
                        case DialogResult.Yes: //移动并替换
                            _iVariableDesignRepository.PasteVariable(variableBase,
                                                                     GetVariableGroupPath(_currentNode.FullPath),
                                                                     _iscopy,
                                                                     1);
                            break;
                        case DialogResult.OK: //移动，但保留两个文件
                            _iVariableDesignRepository.PasteVariable(variableBase,
                                                                     GetVariableGroupPath(_currentNode.FullPath),
                                                                     _iscopy,
                                                                     2);
                            break;
                    }
                }
            }
            if (_iscopy == false)
            {
                _variablePasteBoard.Clear();
            }
            if (_variablePasteBoard.Count <= 0)
            {
                CMS_PasteVariable.Enabled = false;
            }
            RefreshDataGridView();
        }

        /// <summary>
        ///     粘贴变量组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmsPasteVariableGroupClick(object sender, EventArgs e)
        {
            if (_groupPasteBoard == null)
            {
                return;
            }
            try
            {
                string str = _iVariableDesignRepository.PasteGroup(_groupPasteBoard,
                                                                   GetVariableGroupPath(_currentNode.FullPath),
                                                                   _iscopy);

                if (str != _groupPasteBoard.Name)
                {
                    switch ((new PasteMessage(str)).ShowDialog())
                    {
                        case DialogResult.Yes: //移动并替换
                            _iVariableDesignRepository.PasteGroup(_groupPasteBoard,
                                                                  GetVariableGroupPath(_currentNode.FullPath),
                                                                  _iscopy, 1);
                            break;
                        case DialogResult.OK: //移动，但保留两个文件
                            _iVariableDesignRepository.PasteGroup(_groupPasteBoard,
                                                                  GetVariableGroupPath(_currentNode.FullPath),
                                                                  _iscopy, 2);
                            break;
                    }
                }
                RefreshTree();
                if (_iscopy == false)
                {
                    _groupPasteBoard = null;
                }
                if (_groupPasteBoard == null)
                {
                    CMS_PasteVariableGroup.Enabled = false;
                }
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 关联设备ToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (dataGridView_Avaiable.SelectedRows.Count <= 0)
            {
                MessageBox.Show("关联变量必须选中变量");
                return;
            }
            DeviceBase deviceBase = new RandomDevice();
            DeviceAdapter.AddMap(
                _iVariableDesignRepository.FindVariableByPath(
                    dataGridView_Avaiable.SelectedRows[0].Cells[1].Value.ToString()),
                deviceBase);
        }

        private void 取消关联设备ToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (dataGridView_Avaiable.SelectedRows.Count <= 0)
            {
                MessageBox.Show("关联变量必须选中变量");
                return;
            }
            DeviceAdapter.RemoveMap(
                _iVariableDesignRepository.FindVariableByPath(
                    dataGridView_Avaiable.SelectedRows[0].Cells[1].Value.ToString()));
        }

        private void 刷新数据ToolStripMenuItemClick(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        #endregion

        #endregion

        #region 菜单按钮

        private void 搜索ToolStripMenuItemClick(object sender, EventArgs e)
        {
            InitVariableUi(Displaytype.Variable);
            VariableBase digitalVariable = _iVariableDesignRepository.FindVariableByPath(toolStripTextBox1.Text);
            dataGridView_Avaiable.Rows.Clear();
            if (digitalVariable != null)
            {
                AddVarToListview(digitalVariable, dataGridView_Avaiable);
            }
        }

        private void 按组搜索ToolStripMenuItemClick(object sender, EventArgs e)
        {
            InitVariableUi(Displaytype.Variable);
            VariableBase digitalVariable = _iVariableDesignRepository.FindVariableByPath(toolStripTextBox1.Text);
            dataGridView_Avaiable.Rows.Clear();
            if (digitalVariable != null)
            {
                AddVarToListview(digitalVariable, dataGridView_Avaiable);
            }
        }

        private void 搜索组的所有变量ToolStripMenuItemClick(object sender, EventArgs e)
        {
            InitVariableUi(Displaytype.Variable);
            IEnumerable<VariableBase> digitalVariable = _iVariableDesignRepository.FindVariables(toolStripTextBox1.Text);
            if (digitalVariable == null) return;

            dataGridView_Avaiable.Rows.Clear();
            foreach (VariableBase variableBase in digitalVariable)
            {
                AddVarToListview(variableBase, dataGridView_Avaiable);
            }
        }

        private void 同步数据ToolStripMenuItemClick(object sender, EventArgs e)
        {
            DeviceAdapter.Start();
            同步数据ToolStripMenuItem.Enabled = false;
            停止同步数据ToolStripMenuItem.Enabled = true;
            timer1.Enabled = true;
        }

        private void 停止同步数据ToolStripMenuItemClick(object sender, EventArgs e)
        {
            DeviceAdapter.Stop();
            同步数据ToolStripMenuItem.Enabled = true;
            停止同步数据ToolStripMenuItem.Enabled = false;
            timer1.Enabled = false;
        }

        #endregion

        private void 新建报警ToolStripMenuItemClick(object sender, EventArgs e)
        {
            var alarmGridViewRow = new DataGridViewRow();
            alarmGridViewRow.CreateCells(dataGridView_Avaiable);
            string value;
            for (int i = 1;; i++)
            {
                if (!_iAalarmDesignRepository.IsExistAlarmName("变量报警" + i))
                {
                    value = "变量报警" + i;
                    break;
                }
            }
            alarmGridViewRow.Cells[0].Value = value;
            alarmGridViewRow.Cells[3].Value = "一般";
            dataGridView_Avaiable.Rows.Add(alarmGridViewRow);
            _iAalarmDesignRepository.AddAlarm(new AnalogAlarm
                {
                    Name = alarmGridViewRow.Cells[0].Value.ToString(),
                    Level = alarm_Level.Items.IndexOf(alarmGridViewRow.Cells[3].Value)
                });
        }

        private void 删除报警ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView_Avaiable.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择需要删除的报警");
                return;
            }
            foreach (DataGridViewRow row in dataGridView_Avaiable.SelectedRows)
            {
                _iAalarmDesignRepository.RemoveAlarm(row.Cells[0].Value.ToString());
            }
            RefreshDataGridView();
        }

        /// <summary>
        ///     dataGridView的按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClick(object sender, EventArgs e)
        {
            DialogResult dialog;
            switch (((DataGridViewCellButton) sender).HeaderText)
            {
                case "报警变量":
                    dialog = (new VariableList(_iVariableDesignRepository)).ShowDialog();
                    if (dialog != DialogResult.OK || SelectVariable == null)
                    {
                        return;
                    }
                    _iAalarmDesignRepository.EditAlarm(
                        dataGridView_Avaiable.Rows[((DataGridViewCellButton)sender).Cell.RowIndex].Cells[0]
                            .Value
                            .ToString
                            (), "Variable",SelectVariable);
                    RefreshDataGridView();
                    break;
                case "报警配置":
                    break;
                case "报警组":
                    string selectAlarmName =
                        dataGridView_Avaiable.Rows[((DataGridViewCellButton) sender).Cell.RowIndex].Cells[0].Value
                                                                                                            .ToString();
                    dialog = (new AlarmGroupList(_iAalarmDesignRepository, selectAlarmName)).ShowDialog();
                    if (dialog == DialogResult.OK)
                    {
                        AlarmBase alarm = _iAalarmDesignRepository.FindAlarmByName(selectAlarmName);
                        if (alarm != null)
                        {
                            dataGridView_Avaiable.Rows[((DataGridViewCellButton) sender).Cell.RowIndex].Cells[4]
                                .Value = alarm.Group == null ? "" : alarm.Group.Name;
                        }
                    }
                    break;
            }
        }


        private void 新建报警组ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var alarmGridViewRow = new DataGridViewRow();
            alarmGridViewRow.CreateCells(dataGridView_Avaiable);
            string value;
            for (int i = 1;; i++)
            {
                if (!_iAalarmDesignRepository.IsExistAlarmGroupName("变量报警组" + i))
                {
                    value = "变量报警组" + i;
                    break;
                }
            }
            alarmGridViewRow.Cells[0].Value = value;
            alarmGridViewRow.Cells[1].Value = "";
            dataGridView_Avaiable.Rows.Add(alarmGridViewRow);
            _iAalarmDesignRepository.AddAlarmGroup(new AlarmGroup
                {
                    Name = alarmGridViewRow.Cells[0].Value.ToString(),
                    Description = alarmGridViewRow.Cells[1].Value.ToString()
                });
        }

        private void 删除报警组ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView_Avaiable.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择需要删除的报警");
                return;
            }
            foreach (DataGridViewRow row in dataGridView_Avaiable.SelectedRows)
            {
                _iAalarmDesignRepository.RemoveAlarmGroup(row.Cells[0].Value.ToString());
            }
            RefreshDataGridView();
        }
    }
}