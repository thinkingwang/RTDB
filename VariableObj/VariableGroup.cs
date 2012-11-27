using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Variable
{
    public class VariableGroup
    {
        #region 私有方法

        private VariableGroup _parent;
        private readonly List<VariableGroup> _childGroups = new List<VariableGroup>();
        private readonly List<VariableBase> _childVariables = new List<VariableBase>();

        #endregion

        #region 属性

        /// <summary>
        /// 根组
        /// </summary>
        public static VariableGroup RootGroup { get; private set; }

        /// <summary>
        /// 变量组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 子组集合
        /// </summary>
        public VariableGroup[] ChildGroups
        {
            get { return _childGroups.ToArray(); }
        }

        /// <summary>
        /// 当前组的子组数量
        /// </summary>
        public int GroupsCount
        {
            get { return _childGroups.Count; }
        }

        /// <summary>
        /// 组变量集合
        /// </summary>
        public VariableBase[] ChildVariables
        {
            get { return _childVariables.ToArray(); }
        }

        /// <summary>
        /// 当前组的变量数量
        /// </summary>
        public int VariablesCount
        {
            get { return _childVariables.Count; }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 组默认构造函数
        /// </summary>
        public VariableGroup()
        {

        }

        /// <summary>
        /// 组构造函数
        /// </summary>
        /// <param name="groupName">组名称</param>
        public VariableGroup(string groupName)
        {
            GroupName = groupName;
        }

        static VariableGroup()
        {
            Debug.Assert(Resource1.VariableGroup__rootGroup_variableDictionary != null, 
                "Resource1.VariableGroup__rootGroup_variableDictionary != null");

            RootGroup = new VariableGroup(Resource1.VariableGroup__rootGroup_variableDictionary);
        }

        #endregion

        #region 公有方法

        #region 组公共方法

        /// <summary>
        /// 向当前组添加子组
        /// </summary>
        /// <param name="groupName">子组名称</param>
        public void AddGroup(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                Debug.Assert(Resource1.CVariableGroup_AddGroup_GroupNameIsNull != null,
                             "Resource1.CVariableGroup_AddGroup_GroupNameIsNull != null");
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupNameIsNull);
            }

            var newGroup = new VariableGroup {GroupName = groupName, _parent = this};

            if (IsExistGroupName(groupName))
            {
                Debug.Assert(Resource1.CVariableGroup_AddGroup_GroupeNameIsExist != null,
                             "Resource1.CVariableGroup_AddGroup_GroupeNameIsExist != null");
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupeNameIsExist);
            }

            if (_parent == null)
            {
                //根组
                RootGroup._childGroups.Add(newGroup);
            }
            else
            {
                _childGroups.Add(newGroup);
            }

        }

        /// <summary>
        /// 删除指定组
        /// </summary>
        public void RemoveGroup()
        {
            //删除该组下面的子组
            while (_childGroups.Count > 0)
            {
                _childGroups[0].RemoveGroup();
            }

            //删除该组下的变量
            ClearVariable();

            //删除该组
            if (_parent != null)
            {
                _parent._childGroups.Remove(this);
            }
            else
            {
                Debug.Assert(Resource1.VariableGroup_RemoveGroup_DeleteGroup_Is_RootGroup != null, 
                    "Resource1.VariableGroup_RemoveGroup_DeleteGroup_Is_RootGroup != null");
                throw new Exception(Resource1.VariableGroup_RemoveGroup_DeleteGroup_Is_RootGroup);
            }
        }
        
        /// <summary>
        /// 修改当前变量组名称
        /// </summary>
        /// <param name="groupName">修改后的组名</param>
        public void RenameGroup(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                Debug.Assert(Resource1.VariableGroup_ReGroupName_groupName_Is_Null != null,
                    "Resource1.VariableGroup_ReGroupName_groupName_Is_Null != null");
                throw new Exception(Resource1.VariableGroup_ReGroupName_groupName_Is_Null);
            }

            GroupName = groupName;
        }

        /// <summary>
        /// 获取当前组的绝对路径
        /// </summary>
        /// <param name="isHideRoot">是否隐藏根组</param>
        /// <returns>变量组绝对路径，以“.”作为间隔</returns>
        public string GetFullPath(bool isHideRoot = true)
        {
            VariableGroup varGroup = this;
            string path = varGroup.GroupName;
            while (varGroup._parent != null)
            {
                varGroup = varGroup._parent;
                path = varGroup.GroupName + "." + path;
            }

            if (isHideRoot)
            {
                //隐藏根组
                int index = path.IndexOf('.');
                path = index < 0 ? "" : path.Substring(index + 1);
            }
            return path;
        }

        /// <summary>
        /// 根据绝对路径获取组
        /// </summary>
        /// <param name="fullPath">绝对路径</param>
        /// <param name="isHideRoot">绝对路径是否包含根组</param>
        /// <returns>返回组</returns>
        public static VariableGroup GetGroup(string fullPath, bool isHideRoot = true)
        {
            if (isHideRoot)
            {
                fullPath = RootGroup.GroupName + "." + fullPath;
            }
            string[] paths = fullPath.Split('.');
            VariableGroup group = RootGroup;
            for (int i = 1; i < paths.Length; i++)
            {
                group = group._childGroups.Find(m => m.GroupName == paths[i]);
                if (group == null)
                    break;
            }

            return group;
        }

        #endregion

        #region 当前组的变量公共方法

        /// <summary>
        /// 向当前组添加变量
        /// </summary>
        /// <param name="variable">需要添加的变量</param>
        public void AddVariable(VariableBase variable)
        {
            if (variable == null)
            {
                Debug.Assert(Resource1.VariableGroup_AddVariable_variable_is_null != null, 
                    "Resource1.VariableGroup_AddVariable_variable_is_null != null");

                throw new ArgumentNullException(Resource1.VariableGroup_AddVariable_variable_is_null);
            }

            if (IsContainVariable(variable))
            {
                Debug.Assert(Resource1.VariableGroup_addVariable_variableName_is_Exist != null,
                             "Resource1.VariableGroup_addVariable_variableName_is_Exist != null");
                throw new Exception(Resource1.VariableGroup_addVariable_variableName_is_Exist);
            }

            variable.GroupID = GetFullPath();
            _childVariables.Add(variable);
        }

        /// <summary>
        /// 删除指定变量
        /// </summary>
        /// <param name="variableName">变量名称</param>
        public void RemoveVariable(string variableName)
        {
            for (int index = 0; index < _childVariables.Count; index++)
            {
                VariableBase curVariable = _childVariables[index];
                if (curVariable.VarName == variableName)
                {
                    _childVariables.Remove(curVariable);
                    curVariable.RemoveVar();
                    break;
                }
            }
        }

        /// <summary>
        /// 删除当前组的所有变量
        /// </summary>
        public void ClearVariable()
        {
            //删除该组下的变量
            while (_childVariables.Count > 0)
            {
                _childVariables[0].RemoveVar();
                _childVariables.RemoveAt(0);
            }
        }

        #endregion

        #endregion

        #region 私有方法
        /// <summary>
        /// 判断组名称groupNameId是否在parentGroup中存在
        /// </summary>
        /// <param name="groupNameId">组名称</param>
        /// <returns>true:存在，false：不存在</returns>
        private bool IsExistGroupName(string groupNameId)
        {
            //如果父组包含groupName相同的组，则返回不添加
            return _childGroups.Any(curGroup => curGroup.GroupName == groupNameId);
        }

        /// <summary>
        /// 变量列表是否包含指定变量
        /// </summary>
        /// <param name="variable">指定变量</param>
        /// <returns>true：存在，false：不存在</returns>
        private bool IsContainVariable(VariableBase variable)
        {
            return _childVariables.Any(curVariable => curVariable.VarName == variable.VarName);
        }

        #endregion
    }
}
