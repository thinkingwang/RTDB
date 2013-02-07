using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SCADA.RTDB.Adaptation;
using SCADA.RTDB.Core.Variable;
using SCADA.RTDB.EntityFramework.DbConfig;
using SCADA.RTDB.EntityFramework.ExtendMethod;
using SCADA.RTDB.EntityFramework.Repository;
using SCADA.RTDB.Repository.Test.Properties;
using SCADA.RTDB.SimulationDevice;
using SCADA.RTDB.SimulationDeviceBase;
using SCADA.RTDB.Common;

namespace SCADA.RTDB.Repository.Test
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FunctionTestForm : Form
    {
       // private const string Connectstring = "Data Source=cnwj6iapc006\\sqlexpress;Initial Catalog=VariableDB;User ID=sa;Password=666666";
        private const string Connectstring = "data source=VariableDB.sdf;Password=666666";

        /// <summary>
        /// 
        /// </summary>
        public FunctionTestForm()
        {
            InitializeComponent();
        }

        private TreeNode _currentNode = new TreeNode();
        private IVariableRepository _iVariableRepository;

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
            var config = new RepositoryConfig
                {
                    DbNameOrConnectingString = Connectstring,
                    DbType = DataBaseType.SqlCeConnectionFactory
                };
            _iVariableRepository =
                new EfVariableRepository(config);
            _iVariableRepository.Load();
            
            treeView_FunctionTest.LabelEdit = false;
            RefreshTree();

        }

        #region TreeView操作方法

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
            VairableGroupToTreeView(treeView_FunctionTest, _iVariableRepository.FindGroupByPath(null));
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
            _iVariableRepository.AddGroup(node.Text, GetVariableGroupPath(node.Parent.FullPath));
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
            des = index >= 0 ? des.Substring(index + 1) : string.Empty;
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
                _iVariableRepository.RemoveGroup(GetVariableGroupPath(_currentNode.FullPath));
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
                    if (e.Label.IndexOfAny(new[] { '@', '.', ',', '!' }) == -1)
                    {
                        try
                        {
                            VariableGroup varGroup =
                                _iVariableRepository.FindGroupByPath(GetVariableGroupPath(_currentNode.FullPath));
                            _iVariableRepository.RenameGroup(e.Label, GetVariableGroupPath(_currentNode.FullPath));
                            if (varGroup != null)
                            {
                                dataGridView_Avaiable.Rows.Clear();
                                foreach (var variable in varGroup.ChildVariables)
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
            VariableGroup varGroup = _iVariableRepository.FindGroupByPath(GetVariableGroupPath(_currentNode.FullPath));
            if (varGroup == null)
            {
                return;
            }
            //dataGridView_Avaiable.DataSource = null;
            //dataGridView_Avaiable.DataSource = _iVariableRepository.FindVariables(varGroup.AbsolutePath);
            dataGridView_Avaiable.Rows.Clear();
            foreach (VariableBase variable in _iVariableRepository.FindVariables(varGroup.AbsolutePath))
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
                new DigitalVariable(_iVariableRepository.FindGroupByPath(GetVariableGroupPath(_currentNode.FullPath)));
            _iVariableRepository.AddVariable(digitalVariable);
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
                new AnalogVariable(_iVariableRepository.FindGroupByPath(GetVariableGroupPath(_currentNode.FullPath)));
            _iVariableRepository.AddVariable(analogVariable);
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
                new TextVariable(_iVariableRepository.FindGroupByPath(GetVariableGroupPath(_currentNode.FullPath)));
            _iVariableRepository.AddVariable(stringVariable);
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
            //row.ContextMenuStrip = contextMenuStrip_Variable;
            List<string> tableList = variable.VariableToStrings();
            for (int i = 0; i < 16; i++)
            {
                row.Cells[i].Value = tableList[i];
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
                _iVariableRepository.RemoveVariable(row.Cells[0].Value.ToString(),
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
                //VariableBase editVar;
                var variableStrings = new List<string>();
                for (int i = 0; i < 16; i++)
                {
                    variableStrings.Add(currentRow.Cells[i].Value == null ?  null : currentRow.Cells[i].Value.ToString());
                }

                VariableBase oldVar = (_iVariableRepository.FindGroupByPath(GetVariableGroupPath(_currentNode.FullPath))
                                                           .ChildVariables.Find(
                                                               m => m.AbsolutePath == currentRow.Cells[1].Value.ToString()));
                //修改变量值
                if (!timer1.Enabled && e.ColumnIndex == 7)
                {
                    if (!oldVar.SetValue(variableStrings[7]))
                    {
                        MessageBox.Show("变量值不合法，变量值设置失败");
                    }
                    return;
                }

                if (_iVariableRepository.EditVariable(oldVar, variableStrings) == null)
                {
                    MessageBox.Show("修改参数不合法，变量修改失败");
                }

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
            CopyVariables(true);
        }

        private void CopyVariables(bool isCopy)
        {
            _variablePasteBoard.Clear();
            foreach (DataGridViewRow c in dataGridView_Avaiable.SelectedRows)
            {
                VariableGroup variableGroup =
                    _iVariableRepository.FindGroupByPath(GetVariableGroupPath(_currentNode.FullPath));
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
        /// 剪切变量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmsCutVariableClick(object sender, EventArgs e)
        {
            CopyVariables(false);
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

                string str = _iVariableRepository.PasteVariable(variableBase, GetVariableGroupPath(_currentNode.FullPath),
                                                       _iscopy);
                if (str != variableBase.Name)
                {
                    switch ((new PasteMessage(str)).ShowDialog())
                    {
                        case DialogResult.Yes: //移动并替换
                            _iVariableRepository.PasteVariable(variableBase, GetVariableGroupPath(_currentNode.FullPath), _iscopy,
                                                      1);
                            break;
                        case DialogResult.OK: //移动，但保留两个文件
                            _iVariableRepository.PasteVariable(variableBase, GetVariableGroupPath(_currentNode.FullPath), _iscopy,
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
            _groupPasteBoard = _iVariableRepository.FindGroupByPath(GetVariableGroupPath(_currentNode.FullPath));

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
            _groupPasteBoard = _iVariableRepository.FindGroupByPath(GetVariableGroupPath(_currentNode.FullPath));

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
            try
            {

                string str = _iVariableRepository.PasteGroup(_groupPasteBoard, GetVariableGroupPath(_currentNode.FullPath),
                                                    _iscopy);

                if (str != _groupPasteBoard.Name)
                {
                    switch ((new PasteMessage(str)).ShowDialog())
                    {
                        case DialogResult.Yes: //移动并替换
                            _iVariableRepository.PasteGroup(_groupPasteBoard, GetVariableGroupPath(_currentNode.FullPath),
                                                   _iscopy, 1);
                            break;
                        case DialogResult.OK: //移动，但保留两个文件
                            _iVariableRepository.PasteGroup(_groupPasteBoard, GetVariableGroupPath(_currentNode.FullPath),
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

        private void 搜索ToolStripMenuItemClick(object sender, EventArgs e)
        {
            VariableBase digitalVariable = _iVariableRepository.FindVariableByPath(toolStripTextBox1.Text);
            dataGridView_Avaiable.Rows.Clear();
            if (digitalVariable != null)
            {
                AddVarToListview(digitalVariable);
            }

        }

        private void 按组搜索ToolStripMenuItemClick(object sender, EventArgs e)
        {
            VariableBase digitalVariable = _iVariableRepository.FindVariableByPath(toolStripTextBox1.Text);
            dataGridView_Avaiable.Rows.Clear();
            if (digitalVariable != null)
            {
                AddVarToListview(digitalVariable);
            }
        }

        private void 搜索组的所有变量ToolStripMenuItemClick(object sender, EventArgs e)
        {
            IEnumerable<VariableBase> digitalVariable = _iVariableRepository.FindVariables(toolStripTextBox1.Text);
            if (digitalVariable == null) return;

            dataGridView_Avaiable.Rows.Clear();
            foreach (VariableBase variableBase in digitalVariable)
            {
                AddVarToListview(variableBase);
            }

        }

        private void FunctionTestFormFormClosing(object sender, FormClosingEventArgs e)
        {
            //关闭所有线程
            DeviceAdapter.Close();
            //保存变量最后值
            _iVariableRepository.ExitWithSaving();
        }

        private void 关联设备ToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (dataGridView_Avaiable.SelectedRows.Count <= 0)
            {
                MessageBox.Show("关联变量必须选中变量");
                return;
            }
            DeviceBase deviceBase = new RandomDevice();
            DeviceAdapter.AddMap(_iVariableRepository.FindVariableByPath(dataGridView_Avaiable.SelectedRows[0].Cells[1].Value.ToString()),
                                 deviceBase);
        }

        private void 取消关联设备ToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (dataGridView_Avaiable.SelectedRows.Count <= 0)
            {
                MessageBox.Show("关联变量必须选中变量");
                return;
            }
            DeviceAdapter.RemoveMap(_iVariableRepository.FindVariableByPath(dataGridView_Avaiable.SelectedRows[0].Cells[1].Value.ToString()));
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
                    var variable =
                        _iVariableRepository.FindVariableByPath(dataGridView_Avaiable.Rows[i].Cells[1].Value.ToString());
                    dataGridView_Avaiable.Rows[i].Cells[7].Value = variable.GetValue();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void 刷新数据ToolStripMenuItemClick(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }
    }


}
