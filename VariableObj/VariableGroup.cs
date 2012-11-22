using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace VariableObj
{
   
    public class VariableGroup : TreeNode
    {
        /// <summary>
        /// 变量组集合
        /// </summary>
        public static TreeNode RootGroup = new TreeNode("Variable");

        #region 属性
        /// <summary>
        /// 组的ID号，与变量关联
        /// </summary>
        public string GroupId
        {
            get
            {
                return ToolTipText;
            }
            set
            {
                ToolTipText = value;
            }
        }

        /// <summary>
        /// 变量组根节点名称
        /// </summary>
        public static string RootGroupName
        {
            get
            {
                return RootGroup.Text;
            }
            set
            {
                RootGroup.Text = value;
            }
        }
        #endregion

        #region 公有方法

        /// <summary>
        /// 添加组
        /// </summary>
        /// <param name="groupName">组名称</param>
        /// <param name="parentGroup">组的上一级组</param>
        /// <exception cref="Exception"></exception>
        /// <returns>返回新建组节点</returns>
        public static TreeNode AddGroup(string groupName, TreeNode parentGroup)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                Debug.Assert(Resource1.CVariableGroup_AddGroup_GroupNameIsNull != null, "Resource1.CVariableGroup_AddGroup_GroupNameIsNull != null");
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupNameIsNull);
            }

            if(parentGroup == null)
            {
                throw new Exception(Resource1.VariableGroup_AddGroup_parentNodeIsNull);
            }

            VariableGroup newGroup = new VariableGroup();
            newGroup.GroupId = (parentGroup.Text == RootGroupName) ? (groupName) : (parentGroup.ToolTipText + "." + groupName);
            newGroup.Text = groupName;

            //赋值变量组ID
            if (IsExistGroupName(newGroup.Text, parentGroup))
            {
                Debug.Assert(Resource1.CVariableGroup_AddGroup_GroupeNameIsExist != null, "Resource1.CVariableGroup_AddGroup_GroupeNameIsExist != null");
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupeNameIsExist);
            }

            if (parentGroup.Text == RootGroupName)
            {
                //根节点
                RootGroup.Nodes.Add(newGroup);
            }
            else
            {
                parentGroup.Nodes.Add(newGroup);
            }
            return newGroup;
        }

        /// <summary>
        /// 删除指定组
        /// </summary>
        /// <param name="delelteGroupObj">需要删除的组对象</param>
        public static void DeleteGroup(TreeNode delelteGroupObj)
        {
            if (delelteGroupObj == null)
            {
                return;
            }
            //删除该组下面的子组
            while (delelteGroupObj.Nodes.Count > 0)
            {
                DeleteGroup(delelteGroupObj.Nodes[0]);
            }

            //删除该组下的变量
            VariableBaseObj.DeleteAllVarByGroup(delelteGroupObj.ToolTipText);

            //删除该组
            delelteGroupObj.Remove();
        }

        /// <summary>
        /// 编辑指定组的组名称
        /// </summary>
        /// <param name="editGroup">需要编辑的组对象</param>
        /// <param name="groupName">修改后的组名</param>
        public static void EditGroupName(TreeNode editGroup, string groupName)
        {

            if (editGroup == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(groupName))
            {
                return;
            }

            //修改组自身信息
            editGroup.Text = groupName;

            //修改节点Id值
            string newGroupName = editGroup.ToolTipText.Substring(0, editGroup.ToolTipText.LastIndexOf('.') + 1) + groupName;
            ReplaceGroupName(editGroup, editGroup.ToolTipText, newGroupName);
        }

        /// <summary>
        /// 获取指定变量组的所有变量
        /// </summary>
        /// <param name="id">变量组ID</param>
        /// <returns>变量列表</returns>
        public static List<VariableBaseObj> GetVars(string id)
        {
            return VariableBaseObj.FindAllVarByGroup(id);
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 替换组节点下所有子组和变量Id值
        /// </summary>
        /// <param name="editGroup">需要修改的组</param>
        /// <param name="oldGroupName">修改前的组名称</param>
        /// <param name="newGroupName">修改后的组名称</param>
        private static void ReplaceGroupName(TreeNode editGroup, string oldGroupName, string newGroupName)
        {
            if (editGroup == null)
            {
                throw new ArgumentNullException(Resource1.VariableGroup_ReplaceGroupName_editGroup_is_null);
            }
            editGroup.ToolTipText = newGroupName + editGroup.ToolTipText.Substring(oldGroupName.Length);

            //修改组的所有变量Id
            List<VariableBaseObj> curVarList = GetVars(oldGroupName);
            if (curVarList != null)
            {
                foreach (var variableBaseObj in curVarList)
                {
                    variableBaseObj.GroupID = newGroupName + variableBaseObj.GroupID.Substring(oldGroupName.Length);
                }
            }
            for (int i = 0; i < editGroup.Nodes.Count; i++)
            {
                ReplaceGroupName(editGroup.Nodes[i], oldGroupName, newGroupName);
            }
        }

        /// <summary>
        /// 判断组名称groupNameId是否在parentGroup中存在
        /// </summary>
        /// <param name="groupNameId">组名称</param>
        /// <param name="parentGroup">父组节点</param>
        /// <returns>true:存在，false：不存在</returns>
        private static bool IsExistGroupName(string groupNameId, TreeNode parentGroup)
        {
            if (parentGroup == null)
            {
                throw new ArgumentNullException(Resource1.VariableGroup_IsExistGroupName_parentGroup_is_null);
            }
            //如果父节点包含groupName相同的节点，则返回不添加
            return parentGroup.Nodes.Cast<VariableGroup>().Any(curGroup => curGroup.Text == groupNameId);
        }
        #endregion
    }
}
