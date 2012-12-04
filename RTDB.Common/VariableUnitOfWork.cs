using System;
using System.Diagnostics;
using System.Linq;
using RTDB.VariableModel;

namespace RTDB.Common
{
    public class VariableUnitOfWork
    {
        private readonly IVariableContext _variableContext;

        public VariableUnitOfWork(IVariableContext variableContext)
        {
            _variableContext = variableContext;
            //将根组添加到集合
            _variableContext.VariableGroupSet.Add(VariableGroup.RootGroup);
        }

        #region 变量集合操作私有方法

        /// <summary>
        /// 增加指定变量
        /// </summary>
        /// <param name="variable">指定变量</param>
        private void AddVar(VariableBase variable)
        {
            if (variable == null)
            {
                Debug.Assert(Resource1.VariableRepository_AddVar_VariableIsNull != null,
                             "Resource1.VariableRepository_AddVar_VariableIsNull != null");
                throw new ArgumentNullException(Resource1.VariableRepository_AddVar_VariableIsNull);
            }
            if (variable.ValueType == Varvaluetype.VarBool)
            {
                if (_variableContext.DigitalSet.ContainsKey(variable.VariableBaseId))
                {
                    Debug.Assert(Resource1.VariableRepository_AddVar_VariableIsExist != null,
                                 "Resource1.VariableRepository_AddVar_VariableIsExist != null");
                    throw new Exception(Resource1.VariableRepository_AddVar_VariableIsExist);
                }
                _variableContext.DigitalSet.Add(variable.VariableBaseId, variable as DigitalVariable);
            }
            else if (variable.ValueType == Varvaluetype.VarDouble)
            {
                if (_variableContext.AnalogSet.ContainsKey(variable.VariableBaseId))
                {
                    Debug.Assert(Resource1.VariableRepository_AddVar_VariableIsExist != null,
                                 "Resource1.VariableRepository_AddVar_VariableIsExist != null");
                    throw new Exception(Resource1.VariableRepository_AddVar_VariableIsExist);
                }
                _variableContext.AnalogSet.Add(variable.VariableBaseId, variable as AnalogVariable);
            }
            else
            {
                if (_variableContext.StringSet.ContainsKey(variable.VariableBaseId))
                {
                    Debug.Assert(Resource1.VariableRepository_AddVar_VariableIsExist != null,
                                 "Resource1.VariableRepository_AddVar_VariableIsExist != null");
                    throw new Exception(Resource1.VariableRepository_AddVar_VariableIsExist);
                }
                _variableContext.StringSet.Add(variable.VariableBaseId, variable as StringVariable);
            }

        }

        /// <summary>
        /// 更新指定变量
        /// </summary>
        /// <param name="variable">指定变量</param>
        public void EditVar(VariableBase variable)
        {
            if (variable == null)
            {
                Debug.Assert(Resource1.VariableRepository_AddVar_VariableIsNull != null,
                             "Resource1.VariableRepository_AddVar_VariableIsNull != null");
                throw new ArgumentNullException(Resource1.VariableRepository_AddVar_VariableIsNull);
            }
            if (variable.ValueType == Varvaluetype.VarBool)
            {
                if (_variableContext.DigitalSet.ContainsKey(variable.VariableBaseId))
                {
                    _variableContext.DigitalSet[variable.VariableBaseId].CopyProperty(variable);
                }
            }
            else if (variable.ValueType == Varvaluetype.VarDouble)
            {
                if (_variableContext.AnalogSet.ContainsKey(variable.VariableBaseId))
                {
                    _variableContext.AnalogSet[variable.VariableBaseId].CopyProperty(variable);
                }
            }
            else
            {
                if (_variableContext.StringSet.ContainsKey(variable.VariableBaseId))
                {
                    _variableContext.StringSet[variable.VariableBaseId].CopyProperty(variable);
                }
            }
        }

        /// <summary>
        /// 删除指定变量
        /// </summary>
        /// <param name="variable">指定变量</param>
        private void RemoveVar(VariableBase variable)
        {
            if (variable == null)
            {
                Debug.Assert(Resource1.VariableRepository_AddVar_VariableIsNull != null,
                             "Resource1.VariableRepository_AddVar_VariableIsNull != null");
                throw new ArgumentNullException(Resource1.VariableRepository_AddVar_VariableIsNull);
            }
            if (variable.ValueType == Varvaluetype.VarDouble)
            {
                _variableContext.AnalogSet.Remove(variable.Name);
            }
            else if (variable.ValueType == Varvaluetype.VarBool)
            {
                _variableContext.DigitalSet.Remove(variable.Name);
            }
            else if (variable.ValueType == Varvaluetype.VarDouble)
            {
                _variableContext.StringSet.Remove(variable.Name);
            }
        }

        #endregion

        #region 组公共方法

        public void InitVariableGroup( )
        {
            VariableGroup.RootGroup = _variableContext.VariableGroupSet.Find(m => m.ParentGroupId == null);
            AddGroupMethod(VariableGroup.RootGroup);

        }
        private void AddGroupMethod(VariableGroup currentNode)
        {
            foreach (VariableGroup variableGroup in _variableContext.VariableGroupSet.FindAll(m => m.ParentGroupId == currentNode.VariableGroupId))
            {
                AddGroupMethod(variableGroup);
                currentNode.ChildGroups.Add(variableGroup);
            }
        }

        /// <summary>
        /// 根据组Id获取组对象
        /// </summary>
        /// <param name="groupId">组Id，等于null或为空字符返回根组</param>
        /// <returns>返回组对象，未找到返回null</returns>
        public VariableGroup GetGroupById(string groupId)
        {
            return string.IsNullOrEmpty(groupId) ? VariableGroup.RootGroup :  //等于null或为空字符返回根组
                _variableContext.VariableGroupSet.FirstOrDefault(variableGroup
                    => variableGroup.VariableGroupId == groupId);
        }

        /// <summary>
        /// 向当前组添加子组
        /// </summary>
        /// <param name="groupName">子组名称</param>
        /// <param name="curVariableGroupId">要添加的组Id</param>
        public void AddGroup(string groupName,string curVariableGroupId)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                Debug.Assert(Resource1.CVariableGroup_AddGroup_GroupNameIsNull != null,
                             "Resource1.CVariableGroup_AddGroup_GroupNameIsNull != null");
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupNameIsNull);
            }

            VariableGroup currentVariableGroup = GetGroupById(curVariableGroupId);
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            var newGroup = new VariableGroup(groupName, currentVariableGroup);

            if (IsExistGroupName(groupName, currentVariableGroup))
            {
                Debug.Assert(Resource1.CVariableGroup_AddGroup_GroupeNameIsExist != null,
                             "Resource1.CVariableGroup_AddGroup_GroupeNameIsExist != null");
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupeNameIsExist);
            }

            currentVariableGroup.ChildGroups.Add(newGroup);

            _variableContext.VariableGroupSet.Add(newGroup);
        }

        /// <summary>
        /// 删除指定组
        /// </summary>
        /// <param name="curVariableGroupId">要移除的组Id</param>
        public void RemoveGroup(string curVariableGroupId)
        {
            VariableGroup currentVariableGroup = GetGroupById(curVariableGroupId);
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            //删除该组下面的子组
            while (currentVariableGroup.ChildGroups.Count > 0)
            {
                RemoveGroup(currentVariableGroup.ChildGroups[0].VariableGroupId);
            }

            //删除该组下的变量
            ClearVariable(currentVariableGroup);

            //删除该组
            if (currentVariableGroup.ParentGroupId != null)
            {
                currentVariableGroup.Parent.ChildGroups.Remove(currentVariableGroup);
                _variableContext.VariableGroupSet.Remove(currentVariableGroup);
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
        /// <param name="curVariableGroupId">要重命名的组Id</param>
        public void RenameGroup(string groupName, string curVariableGroupId)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                Debug.Assert(Resource1.VariableGroup_ReGroupName_groupName_Is_Null != null,
                    "Resource1.VariableGroup_ReGroupName_groupName_Is_Null != null");
                throw new Exception(Resource1.VariableGroup_ReGroupName_groupName_Is_Null);
            }
            VariableGroup currentVariableGroup = GetGroupById(curVariableGroupId);
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            if (IsExistGroupName(groupName, currentVariableGroup))
            {
                Debug.Assert(Resource1.CVariableGroup_AddGroup_GroupeNameIsExist != null,
                             "Resource1.CVariableGroup_AddGroup_GroupeNameIsExist != null");
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupeNameIsExist);
            }
            currentVariableGroup.GroupName = groupName;
        }


        #endregion

        #region 组变量公共方法

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
            if (variable.Group == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            
            variable.Name = (string.IsNullOrEmpty(variable.Name)) ? GetInitVarName(variable.Group) : variable.Name;

            if (IsContainVariable(variable))
            {
                Debug.Assert(Resource1.VariableGroup_addVariable_variableName_is_Exist != null,
                             "Resource1.VariableGroup_addVariable_variableName_is_Exist != null");
                throw new Exception(Resource1.VariableGroup_addVariable_variableName_is_Exist);
            }

            variable.Group.ChildVariables.Add(variable);
            
            //添加到仓库集合
            AddVar(variable);
        }

        /// <summary>
        /// 删除指定变量
        /// </summary>
        /// <param name="variableName">变量名称</param>
        /// <param name="curVariableGroupId">移除变量所属组</param>
        public void RemoveVariable(string variableName, string curVariableGroupId)
        {
            VariableGroup currentVariableGroup = GetGroupById(curVariableGroupId);
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            for (int index = 0; index < currentVariableGroup.ChildVariables.Count; index++)
            {
                VariableBase curVariable = currentVariableGroup.ChildVariables[index];
                if (curVariable.Name == variableName)
                {
                    currentVariableGroup.ChildVariables.Remove(curVariable);

                    //移除仓库集合中的变量
                    RemoveVar(curVariable);
                    break;
                }
            }
        }

        /// <summary>
        /// 删除当前组的所有变量
        /// </summary>
        /// <param name="currentVariableGroup">要清空的组</param>
        public void ClearVariable(VariableGroup currentVariableGroup)
        {
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            //删除该组下的变量
            while (currentVariableGroup.ChildVariables.Count > 0)
            {
                RemoveVar(currentVariableGroup.ChildVariables[0]);
                currentVariableGroup.ChildVariables.RemoveAt(0);
            }
        }

        /// <summary>
        /// 获取指定变量组的变量默认名称
        /// </summary>
        /// <param name="group">指定变量组</param>
        /// <returns>返回指定变量组的变量默认名称</returns>
        public static string GetInitVarName(VariableGroup group)
        {
            int cnt = 1;

            while (group.ChildVariables.Any(curVar => curVar.Name == "Variable" + cnt))
            {
                cnt++;
            }
            return "Variable" + cnt;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 判断组名称groupNameId是否在currentVariableGroup中存在
        /// </summary>
        /// <param name="groupNameId">组名称</param>
        /// <param name="currentVariableGroup">组对象</param>
        /// <returns>true:存在，false：不存在</returns>
        private bool IsExistGroupName(string groupNameId, VariableGroup currentVariableGroup)
        {
            //如果父组包含groupName相同的组，则返回不添加
            return currentVariableGroup.ChildGroups.Any(curGroup => curGroup.GroupName == groupNameId);
        }

        /// <summary>
        /// 变量列表是否包含指定变量
        /// </summary>
        /// <param name="variable">指定变量</param>
        /// <returns>true：存在，false：不存在</returns>
        private bool IsContainVariable(VariableBase variable)
        {
            return variable.Group.ChildVariables.Any(curVariable => curVariable.Name == variable.Name);
        }

        #endregion

    }
}
