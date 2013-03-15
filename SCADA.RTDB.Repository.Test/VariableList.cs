using System;
using System.Windows.Forms;
using SCADA.RTDB.Common.Design;
using SCADA.RTDB.Core.Variable;

namespace SCADA.RTDB.Repository.Test
{
    /// <summary>
    /// </summary>
    public partial class VariableList : FormBase
    {
        /// <summary>
        ///     变量列表界面构造函数
        /// </summary>
        public VariableList(IVariableDesignRepository iRepository)
        {
            InitializeComponent();
            _iVariableDesignRepository = iRepository;
        }

        /// <summary>
        ///     变量列表界面加载函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VariableListLoad(object sender, EventArgs e)
        {
            _ContextMenuStrip = null;
            RefreshTreeView(treeView_VariableList, _iVariableDesignRepository.FindGroupByPath(null));
            treeView_VariableList.ExpandAll();
        }

        private void TreeViewVariableListMouseDown(object sender, MouseEventArgs e)
        {
            _currentNode = treeView_VariableList.GetNodeAt(e.X, e.Y);

            if (_currentNode == null)
            {
                return;
            }
            treeView_VariableList.SelectedNode = _currentNode;
            VariableGroup varGroup =
                _iVariableDesignRepository.FindGroupByPath(GetVariableGroupPath(_currentNode.FullPath));
            if (varGroup == null)
            {
                return;
            }
            dataGridView_VariableList.Rows.Clear();
            foreach (VariableBase variable in _iVariableDesignRepository.FindVariables(varGroup.AbsolutePath))
            {
                AddVarToListview(variable, dataGridView_VariableList);
            }
            if (dataGridView_VariableList.Rows.Count > 0)
            {
                dataGridView_VariableList.Rows[0].Selected = false;
            }
        }

        private void btn_VariableOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            SelectVariable = dataGridView_VariableList.SelectedRows.Count > 0
                                 ? _iVariableDesignRepository.FindVariableByPath(
                                     dataGridView_VariableList.SelectedRows[0].Cells[1].Value.ToString())
                                 : null;
        }

        private void btn_VariableCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}