using System;
using System.Windows.Forms;
using SCADA.RTDB.Common.Design;
using SCADA.RTDB.Core.Variable;
using SCADA.RTDB.EntityFramework.ExtendMethod;
using SCADA.RTDB.Repository.Test.Properties;

namespace SCADA.RTDB.Repository.Test
{
    /// <summary>
    /// 
    /// </summary>
    public class FormBase : Form
    {
        /// <summary>
        /// 上下文菜单
        /// </summary>
        public ContextMenuStrip _ContextMenuStrip;

        /// <summary>
        /// 
        /// </summary>
        public IVariableDesignRepository _iVariableDesignRepository;
        /// <summary>
        /// 
        /// </summary>
        protected TreeNode _currentNode = new TreeNode();
        /// <summary>
        /// 
        /// </summary>
        protected static VariableBase SelectVariable;
        /// <summary>
        /// 由变量组集合生成树
        /// </summary>
        /// <param name="treeNode">树</param>
        /// <param name="variableGroup">树节点所属组</param>
        protected void RefreshTreeView(TreeView treeNode, VariableGroup variableGroup)
        {
            treeNode.Nodes.Clear();
            var node = new TreeNode(variableGroup.Name)
                {
                    ContextMenuStrip = _ContextMenuStrip
                };
            treeNode.Nodes.Add(node);
            foreach (var variable in variableGroup.ChildGroups)
            {
                RefreshTreeView(node, variable);
            }
        }

        /// <summary>
        /// 由变量组集合生成树
        /// </summary>
        /// <param name="treeNode">树节点</param>
        /// <param name="variableGroup">树节点所属组</param>
        private void RefreshTreeView(TreeNode treeNode, VariableGroup variableGroup)
        {
            var node = new TreeNode(variableGroup.Name)
            {
                ContextMenuStrip = _ContextMenuStrip
            };
            if (treeNode != null) treeNode.Nodes.Add(node);
            foreach (var variable in variableGroup.ChildGroups)
            {
                RefreshTreeView(node, variable);
            }
        }

        /// <summary>
        /// 获取节点的组路径
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetVariableGroupPath(string source)
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
        /// 添加变量到DataGridView
        /// </summary>
        /// <param name="variable">变量</param>
        /// <param name="dataGridView"></param>
        protected void AddVarToListview(VariableBase variable, DataGridView dataGridView)
        {
            var row = new DataGridViewRow();
            row.CreateCells(dataGridView);
            var tableList = variable.VariableToDictionary();
            for (var i = 0; i < dataGridView.Columns.Count; i++)
            {
                row.Cells[i].Value = tableList.ContainsKey(dataGridView.Columns[i].Name.ToLower())
                                         ? tableList[dataGridView.Columns[i].Name.ToLower()]
                                         : "N/A";
            }
            dataGridView.Rows.Add(row); //增加一个新行
        }

    }
}