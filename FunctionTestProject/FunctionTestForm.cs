using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FunctionTestProject.Properties;
using RTDB.VariableModel;
using RTDB.Common;

namespace FunctionTestProject
{
    public partial class FunctionTestForm : Form
    {
        public FunctionTestForm()
        {
            InitializeComponent();
        }

        #region TreeView操作方法

        private TreeNode _currentNode = new TreeNode();
        private IVariableRepository _unitOfWork;

        private readonly List<VariableBase> _variablePasteBoard = new List<VariableBase>();
        private VariableGroup _groupPasteBoard = new VariableGroup();
        private bool _iscopy;

        /// <summary>
        /// 窗体初始化加载函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FunctionTestFormLoad(object sender, EventArgs e)
        {
            _unitOfWork =
                new VariableRepository(
                    "Data Source=cnwj6iapc006\\sqlexpress;Initial Catalog=VariableDB;User ID=sa;Password=666666");

            //listView_Variable.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            treeView_FunctionTest.LabelEdit = false;
            RefreshTree();
        }

        /// <summary>
        /// 由变量组集合生成树
        /// </summary>
        /// <param name="treeNode">树节点</param>
        /// <param name="variableGroup">树节点所属组</param>
        private void VairableGroupToTreeView(TreeNode treeNode, VariableGroup variableGroup)
        {
            var node = new TreeNode(variableGroup.Name)
                {
                    ContextMenuStrip = contextMenuStrip_TreeViewSub
                };
            if (treeNode != null) treeNode.Nodes.Add(node);
            foreach (var variable in variableGroup.ChildGroups)
            {
                VairableGroupToTreeView(node, variable);
            }
        }

        /// <summary>
        /// 由变量组集合生成树
        /// </summary>
        /// <param name="treeNode">树</param>
        /// <param name="variableGroup">树节点所属组</param>
        private void VairableGroupToTreeView(TreeView treeNode, VariableGroup variableGroup)
        {
            var node = new TreeNode(variableGroup.Name)
                {
                    ContextMenuStrip = contextMenuStrip_TreeViewSub
                };
            if (treeNode != null) treeNode.Nodes.Add(node);
            foreach (var variable in variableGroup.ChildGroups)
            {
                VairableGroupToTreeView(node, variable);
            }
        }

        /// <summary>
        /// 更新树
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemRefreshClick(object sender, EventArgs e)
        {
            RefreshTree();
        }

        /// <summary>
        /// 更新树方法体
        /// </summary>
        private void RefreshTree()
        {
            treeView_FunctionTest.Nodes.Clear();
            VairableGroupToTreeView(treeView_FunctionTest, _unitOfWork.GetGroup(null));
            _currentNode = treeView_FunctionTest.TopNode;

            RefreshDataGridView();
            treeView_FunctionTest.ExpandAll();
        }

        /// <summary>
        /// 增加组节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemCreateGroupClick(object sender, EventArgs e)
        {
            var node = new TreeNode(GetInitVarName(_currentNode))
                {
                    ContextMenuStrip = contextMenuStrip_TreeViewSub
                };
            _currentNode.Nodes.Add(node);
            _unitOfWork.AddGroup(node.Text, GetVariableGroupPath(node.Parent.FullPath));
            treeView_FunctionTest.ExpandAll();
        }

        /// <summary>
        /// 获取指定变量组的变量默认名称
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
        /// 获取节点的组路径
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private string GetVariableGroupPath(string source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(Resources.FunctionTestForm_GetVariableGroupPath_sourceIsNull);
            }
            string des = source.Replace('\\', '.');
            int index = des.IndexOf('.');
            des = index >= 0 ? des.Substring(index + 1) : "";
            return des;
        }

        /// <summary>
        /// 删除指定组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemDeleteGroupClick(object sender, EventArgs e)
        {
            try
            {
                _unitOfWork.RemoveGroup(GetVariableGroupPath(_currentNode.FullPath));
                _currentNode.Remove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 重命名指定组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemRenameGroupClick(object sender, EventArgs e)
        {
            BiginEdit();
        }

        /// <summary>
        /// 组节点进入可编辑状态
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
        /// 组节点退出编辑状态
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
                                _unitOfWork.GetGroup(GetVariableGroupPath(_currentNode.FullPath));
                            _unitOfWork.RenameGroup(e.Label, GetVariableGroupPath(_currentNode.FullPath));
                            if (varGroup != null)
                            {
                                dataGridView_Avaiable.Rows.Clear();
                                foreach (VariableBase variable in varGroup.ChildVariables)
                                {
                                    AddVarToListview(variable);
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
        /// 树点击事件处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeViewFunctionTestMouseDown(object sender, MouseEventArgs e)
        {
            TreeNode node = treeView_FunctionTest.GetNodeAt(e.X, e.Y);
            if (node == null || node == _currentNode)
            {
                return;
            }
            _currentNode = node;
            RefreshDataGridView();
        }

        private void RefreshDataGridView()
        {
            treeView_FunctionTest.SelectedNode = _currentNode;
            VariableGroup varGroup = _unitOfWork.GetGroup(GetVariableGroupPath(_currentNode.FullPath));
            if (varGroup == null)
            {
                return;
            }
            dataGridView_Avaiable.Rows.Clear();
            foreach (VariableBase variable in varGroup.ChildVariables)
            {
                AddVarToListview(variable);
            }
        }

        /// <summary>
        /// 添加离散变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DigToolStripMenuItemClick(object sender, EventArgs e)
        {
            var digitalVariable =
                new DigitalVariable(_unitOfWork.GetGroup(GetVariableGroupPath(_currentNode.FullPath)));
            _unitOfWork.AddVariable(digitalVariable);
            AddVarToListview(digitalVariable);
        }

        /// <summary>
        /// 添加模拟变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnaToolStripMenuItemClick(object sender, EventArgs e)
        {
            var analogVariable =
                new AnalogVariable(_unitOfWork.GetGroup(GetVariableGroupPath(_currentNode.FullPath)));
            _unitOfWork.AddVariable(analogVariable);
            AddVarToListview(analogVariable);
        }

        /// <summary>
        /// 添加字符串变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StrToolStripMenuItemClick(object sender, EventArgs e)
        {
            var stringVariable =
                new TextVariable(_unitOfWork.GetGroup(GetVariableGroupPath(_currentNode.FullPath)));
            _unitOfWork.AddVariable(stringVariable);
            AddVarToListview(stringVariable);
        }

        private void TreeViewFunctionTestClick(object sender, EventArgs e)
        {
        }


        #endregion

        #region DataGridView操作方法

        /// <summary>
        /// 添加变量到DataGridView
        /// </summary>
        /// <param name="variable">变量</param>
        private void AddVarToListview(VariableBase variable)
        {
            var row = new DataGridViewRow();
            row.CreateCells(dataGridView_Avaiable);
            row.ContextMenuStrip = contextMenuStrip_Variable;
            if (variable is DigitalVariable)
            {
                row.Cells[0].Value = variable.Name;
                row.Cells[1].Value = variable.VariableBaseFullPath;
                row.Cells[2].Value =
                    variable.VariableType.ToString();
                row.Cells[3].Value =
                    variable.ValueType.ToString();
                row.Cells[4].Value =
                    (variable as DigitalVariable).InitValue;
                row.Cells[5].Value = "N/A";
                row.Cells[6].Value = "N/A";
                row.Cells[7].Value =
                    (variable as DigitalVariable).Value;
                row.Cells[8].Value = "N/A";
                row.Cells[9].Value =
                    variable.OperateProperty.ToString();
                row.Cells[10].Value = variable.IsValueSaved;
                row.Cells[11].Value =
                    variable.IsParameterSaved;
                row.Cells[12].Value = variable.IsAddressable;
                row.Cells[13].Value = variable.IsRecordEvent;
                row.Cells[14].Value = "N/A";
                row.Cells[15].Value = variable.Description;
            }
            else if (variable is AnalogVariable)
            {
                row.Cells[0].Value = variable.Name;
                row.Cells[1].Value = variable.VariableBaseFullPath;
                row.Cells[2].Value =
                    variable.VariableType.ToString();
                row.Cells[3].Value =
                    variable.ValueType.ToString();
                row.Cells[4].Value =
                    (variable as AnalogVariable).InitValue;
                row.Cells[5].Value =
                    (variable as AnalogVariable).MinValue;
                row.Cells[6].Value =
                    (variable as AnalogVariable).MaxValue;
                row.Cells[7].Value =
                    (variable as AnalogVariable).Value;
                row.Cells[8].Value =
                    (variable as AnalogVariable).DeadArea;
                row.Cells[9].Value =
                    variable.OperateProperty.ToString();
                row.Cells[10].Value = variable.IsValueSaved;
                row.Cells[11].Value =
                    variable.IsParameterSaved;
                row.Cells[12].Value = variable.IsAddressable;
                row.Cells[13].Value = variable.IsRecordEvent;
                row.Cells[14].Value =
                    (variable as AnalogVariable).ProjectUnit;
                row.Cells[15].Value = variable.Description;
            }
            else if (variable is TextVariable)
            {
                row.Cells[0].Value = variable.Name;
                row.Cells[1].Value = variable.VariableBaseFullPath;
                row.Cells[2].Value =
                    variable.VariableType.ToString();
                row.Cells[3].Value =
                    variable.ValueType.ToString();
                row.Cells[4].Value =
                    (variable as TextVariable).InitValue;
                row.Cells[5].Value = "N/A";
                row.Cells[6].Value = "N/A";
                row.Cells[7].Value =
                    (variable as TextVariable).Value;
                row.Cells[8].Value = "N/A";
                row.Cells[9].Value =
                    variable.OperateProperty.ToString();
                row.Cells[10].Value = variable.IsValueSaved;
                row.Cells[11].Value =
                    variable.IsParameterSaved;
                row.Cells[12].Value = variable.IsAddressable;
                row.Cells[13].Value = variable.IsRecordEvent;
                row.Cells[14].Value = "N/A";
                row.Cells[15].Value = variable.Description;
            }
            dataGridView_Avaiable.Rows.Add(row); //增加一个新行
        }

        /// <summary>
        /// 从DataGridView中移除指定变量
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
        /// 移除变量的方法体
        /// </summary>
        /// <param name="collenction"></param>
        private void RemoveAvariale(IEnumerable<DataGridViewRow> collenction)
        {
            foreach (DataGridViewRow row in collenction)
            {
                _unitOfWork.RemoveVariable(row.Cells[1].Value.ToString(),
                                           GetVariableGroupPath(_currentNode.FullPath));
                dataGridView_Avaiable.Rows.Remove(row);
            }
        }

        /// <summary>
        /// DataGridView鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridViewAvaiableMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }
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
                contextMenuStrip_Variable.Show(MousePosition);
            }
        }

        /// <summary>
        /// 单元格内容改变出发的事件处理函数
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
                DataGridViewCell currentCell = dataGridView_Avaiable.SelectedCells[0];
                DataGridViewRow currentRow = dataGridView_Avaiable.Rows[currentCell.RowIndex];
                VariableBase editVar;
                VariableBase oldVar = _unitOfWork.GetGroup(GetVariableGroupPath(_currentNode.FullPath))
                                                 .ChildVariables.Find(
                                                     m => m.VariableBaseFullPath == currentRow.Cells[1].Value.ToString());
                if (oldVar is DigitalVariable)
                {
                    editVar = new DigitalVariable(oldVar.Parent);
                    editVar.CopyProperty(oldVar);
                }
                else if (oldVar is AnalogVariable)
                {
                    editVar = new AnalogVariable(oldVar.Parent);
                    editVar.CopyProperty(oldVar);
                }
                else
                {
                    editVar = new TextVariable(oldVar.Parent);
                    editVar.CopyProperty(oldVar);
                }

                switch (currentCell.ColumnIndex)
                {
                    case 0:
                        editVar.Name = currentRow.Cells[currentCell.ColumnIndex].Value.ToString();
                        break;
                    case 2:
                        switch (currentRow.Cells[currentCell.ColumnIndex].Value.ToString())
                        {
                            case "VarNormal":
                                editVar.VariableType = VarType.VarNormal;
                                break;
                            case "VarStruct":
                                editVar.VariableType = VarType.VarStruct;
                                break;
                            case "VarRef":
                                editVar.VariableType = VarType.VarRef;
                                break;
                            default:
                                return;
                        }
                        break;
                    case 4:
                        if (editVar is AnalogVariable)
                        {
                            (editVar as AnalogVariable).InitValue =
                                double.Parse(currentRow.Cells[currentCell.ColumnIndex].Value.ToString());
                        }
                        else if (editVar is DigitalVariable)
                        {
                            (editVar as DigitalVariable).InitValue =
                                currentRow.Cells[currentCell.ColumnIndex].Value.ToString().Substring(0, 1).ToLower() ==
                                "t";
                        }
                        else
                        {
                            (editVar as TextVariable).InitValue =
                                currentRow.Cells[currentCell.ColumnIndex].Value.ToString();
                        }
                        break;
                    case 5:
                        if (editVar is AnalogVariable)
                        {
                            (editVar as AnalogVariable).MinValue =
                                double.Parse(currentRow.Cells[currentCell.ColumnIndex].Value.ToString());
                        }
                        break;
                    case 6:
                        if (editVar is AnalogVariable)
                        {
                            (editVar as AnalogVariable).MaxValue =
                                double.Parse(currentRow.Cells[currentCell.ColumnIndex].Value.ToString());
                        }
                        break;
                    case 7:
                        if (editVar is AnalogVariable)
                        {
                            (editVar as AnalogVariable).Value =
                                double.Parse(currentRow.Cells[currentCell.ColumnIndex].Value.ToString());
                        }
                        else if (editVar is DigitalVariable)
                        {
                            (editVar as DigitalVariable).Value =
                                currentRow.Cells[currentCell.ColumnIndex].Value.ToString().Substring(0, 1).ToLower() ==
                                "t";
                        }
                        else
                        {
                            (editVar as TextVariable).Value =
                                currentRow.Cells[currentCell.ColumnIndex].Value.ToString();
                        }
                        break;
                    case 8:
                        if (editVar is AnalogVariable)
                        {
                            (editVar as AnalogVariable).DeadArea =
                                double.Parse(currentRow.Cells[currentCell.ColumnIndex].Value.ToString());
                        }
                        break;
                    case 9:
                        switch (currentRow.Cells[currentCell.ColumnIndex].Value.ToString())
                        {
                            case "ReadWrite":
                                editVar.OperateProperty = Varoperateproperty.ReadWrite;
                                break;
                            case "OnlyRead":
                                editVar.OperateProperty = Varoperateproperty.ReadOnly;
                                break;
                            case "OnlyWrite":
                                editVar.OperateProperty = Varoperateproperty.WriteOnly;
                                break;
                            default:
                                return;
                        }
                        break;
                    case 10:
                        editVar.IsValueSaved =
                            currentRow.Cells[currentCell.ColumnIndex].Value.ToString().Substring(0, 1).ToLower() == "t";
                        break;
                    case 11:
                        editVar.IsParameterSaved =
                            currentRow.Cells[currentCell.ColumnIndex].Value.ToString().Substring(0, 1).ToLower() == "t";
                        break;
                    case 12:
                        editVar.IsAddressable =
                            currentRow.Cells[currentCell.ColumnIndex].Value.ToString().Substring(0, 1).ToLower() == "t";
                        break;
                    case 13:
                        editVar.IsRecordEvent =
                            currentRow.Cells[currentCell.ColumnIndex].Value.ToString().Substring(0, 1).ToLower() == "t";
                        break;
                    case 14:
                        if (editVar is AnalogVariable)
                        {
                            (editVar as AnalogVariable).ProjectUnit =
                                currentRow.Cells[currentCell.ColumnIndex].Value.ToString();
                        }
                        break;
                    case 15:
                        editVar.Description = currentRow.Cells[currentCell.ColumnIndex].Value.ToString();
                        break;
                    default:
                        return;
                }
                _unitOfWork.EditVariable(oldVar, editVar);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                RefreshDataGridView();
            }
        }

        #endregion

        /// <summary>
        /// 复制变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmsCopyVariableClick(object sender, EventArgs e)
        {
            _variablePasteBoard.Clear();
            foreach (DataGridViewRow c in dataGridView_Avaiable.SelectedRows)
            {
                _variablePasteBoard.Add(_unitOfWork.GetGroup(GetVariableGroupPath(_currentNode.FullPath))
                                                   .ChildVariables.Find(
                                                       m =>
                                                       m.VariableBaseFullPath == c.Cells[1].Value.ToString()));
            }

            if (_variablePasteBoard == null)
            {
                return;
            }
            _iscopy = true;
            CMS_PasteVariable.Enabled = true;
        }

        /// <summary>
        /// 剪切变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmsCutVariableClick(object sender, EventArgs e)
        {
            _variablePasteBoard.Clear();
            foreach (DataGridViewRow c in dataGridView_Avaiable.SelectedRows)
            {
                _variablePasteBoard.Add(_unitOfWork.GetGroup(GetVariableGroupPath(_currentNode.FullPath))
                                                   .ChildVariables.Find(
                                                       m =>
                                                       m.VariableBaseFullPath == c.Cells[1].Value.ToString()));
            }
            if (_variablePasteBoard == null)
            {
                return;
            }
            _iscopy = false;
            CMS_PasteVariable.Enabled = true;
        }

        /// <summary>
        /// 粘贴变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmsPasteVariableClick(object sender, EventArgs e)
        {
            foreach (VariableBase variableBase in _variablePasteBoard)
            {

                string str = _unitOfWork.PasteVariable(variableBase, GetVariableGroupPath(_currentNode.FullPath),
                                                       _iscopy);
                if (str != variableBase.Name)
                {
                    switch ((new PasteMessage(str)).ShowDialog())
                    {
                        case DialogResult.Yes: //移动并替换
                            _unitOfWork.PasteVariable(variableBase, GetVariableGroupPath(_currentNode.FullPath), _iscopy,
                                                      1);
                            break;
                        case DialogResult.OK: //移动，但保留两个文件
                            _unitOfWork.PasteVariable(variableBase, GetVariableGroupPath(_currentNode.FullPath), _iscopy,
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
        /// 复制变量组
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
            _groupPasteBoard = _unitOfWork.GetGroup(GetVariableGroupPath(_currentNode.FullPath));

            if (_groupPasteBoard == null)
            {
                return;
            }
            _iscopy = true;
            CMS_PasteVariableGroup.Enabled = true;
        }

        /// <summary>
        /// 剪切变量组
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
            _groupPasteBoard = _unitOfWork.GetGroup(GetVariableGroupPath(_currentNode.FullPath));

            if (_groupPasteBoard == null)
            {
                return;
            }
            _iscopy = false;
            CMS_PasteVariableGroup.Enabled = true;
        }

        /// <summary>
        /// 粘贴变量组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmsPasteVariableGroupClick(object sender, EventArgs e)
        {
            if (_groupPasteBoard == null)
            {
                return;
            }
            string str = _unitOfWork.PasteGroup(_groupPasteBoard, GetVariableGroupPath(_currentNode.FullPath),
                                                   _iscopy);

            if (str != _groupPasteBoard.Name)
            {
                switch ((new PasteMessage(str)).ShowDialog())
                {
                    case DialogResult.Yes: //移动并替换
                        _unitOfWork.PasteGroup(_groupPasteBoard, GetVariableGroupPath(_currentNode.FullPath), _iscopy, 1);
                        break;
                    case DialogResult.OK: //移动，但保留两个文件
                        _unitOfWork.PasteGroup(_groupPasteBoard, GetVariableGroupPath(_currentNode.FullPath), _iscopy, 2);
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

    }


}
