using System;
using System.Diagnostics;
using System.Linq;
using RTDB.VariableModel;

namespace RTDB.Common
{
    public class VariableRepository
    {
        private readonly IVariableContext _variableContext;

        public VariableRepository(IVariableContext variableContext)
        {
            if (variableContext == null)
            {
                Debug.Assert(Resource1.VariableUnitOfWork_VariableUnitOfWork_variableContextIsNull != null,
                    "Resource1.VariableUnitOfWork_VariableUnitOfWork_variableContextIsNull != null");
                throw new ArgumentNullException(Resource1.VariableUnitOfWork_VariableUnitOfWork_variableContextIsNull);
            }
            _variableContext = variableContext;

        }
        
        #region 组公共方法

        /// <summary>
        /// 根据组Id获取组对象
        /// </summary>
        /// <param name="groupId">组Id，等于null或为空字符返回根组</param>
        /// <returns>返回组对象，未找到返回null</returns>
        public VariableGroup GetGroupById(string groupId)
        {
            //等于null或为空字符返回根组
            return string.IsNullOrEmpty(groupId)
                       ? VariableGroup.RootGroup
                       : _variableContext.VariableGroupSet.Local.FirstOrDefault(curGroup => curGroup.GroupFullPath == groupId);
        }

        /// <summary>
        /// 向当前组添加子组
        /// </summary>
        /// <param name="groupName">子组名称</param>
        /// <param name="parentVariableGroupId">要添加的组Id</param>
        public void AddGroup(string groupName,string parentVariableGroupId)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                Debug.Assert(Resource1.CVariableGroup_AddGroup_GroupNameIsNull != null,
                             "Resource1.CVariableGroup_AddGroup_GroupNameIsNull != null");
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupNameIsNull);
            }

            VariableGroup parentVariableGroup = GetGroupById(parentVariableGroupId);
            if (parentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            var newGroup = new VariableGroup(groupName, parentVariableGroup);

            if (IsExistName(groupName, parentVariableGroup))
            {
                Debug.Assert(Resource1.CVariableGroup_AddGroup_GroupeNameIsExist != null,
                             "Resource1.CVariableGroup_AddGroup_GroupeNameIsExist != null");
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupeNameIsExist);
            }

            parentVariableGroup.ChildGroups.Add(newGroup);

            _variableContext.VariableGroupSet.Add(newGroup);
            _variableContext.SaveVariable();
        }

        /// <summary>
        /// 删除指定组
        /// </summary>
        /// <param name="curVariableGroupId">要移除的组Id</param>
        public void RemoveGroup(string curVariableGroupId)
        {
            if (string.IsNullOrEmpty(curVariableGroupId))
            {
                Debug.Assert(Resource1.VariableUnitOfWork_RemoveGroup_curVariableGroupIdIsNullOrEmpty != null, 
                    "Resource1.VariableUnitOfWork_RemoveGroup_curVariableGroupIdIsNullOrEmpty != null");
                throw new ArgumentNullException(Resource1.VariableUnitOfWork_RemoveGroup_curVariableGroupIdIsNullOrEmpty);
            }
            VariableGroup currentVariableGroup = GetGroupById(curVariableGroupId);
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            //删除该组下面的子组
            while (currentVariableGroup.ChildGroups.Count > 0)
            {
                RemoveGroup(currentVariableGroup.ChildGroups[0].GroupFullPath);
            }

            //删除该组下的变量
            ClearVariable(currentVariableGroup);

            //删除该组
            if (currentVariableGroup.ParentGroupId <= 0)
            {
                //根组不允许
                Debug.Assert(Resource1.VariableGroup_RemoveGroup_DeleteGroup_Is_RootGroup != null,
                    "Resource1.VariableGroup_RemoveGroup_DeleteGroup_Is_RootGroup != null");
                throw new Exception(Resource1.VariableGroup_RemoveGroup_DeleteGroup_Is_RootGroup);
            }
            currentVariableGroup.Parent.ChildGroups.Remove(currentVariableGroup);
            _variableContext.VariableGroupSet.Remove(currentVariableGroup);
            _variableContext.SaveVariable();
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
            if (string.IsNullOrEmpty(curVariableGroupId))
            {
                Debug.Assert(Resource1.VariableUnitOfWork_RemoveGroup_curVariableGroupIdIsNullOrEmpty != null,
                    "Resource1.VariableUnitOfWork_RemoveGroup_curVariableGroupIdIsNullOrEmpty != null");
                throw new ArgumentNullException(Resource1.VariableUnitOfWork_RemoveGroup_curVariableGroupIdIsNullOrEmpty);
            }
            VariableGroup currentVariableGroup = GetGroupById(curVariableGroupId);
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            if (IsExistName(groupName, currentVariableGroup.Parent))
            {
                Debug.Assert(Resource1.CVariableGroup_AddGroup_GroupeNameIsExist != null,
                             "Resource1.CVariableGroup_AddGroup_GroupeNameIsExist != null");
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupeNameIsExist);
            }
            currentVariableGroup.GroupName = groupName;
            _variableContext.SaveVariable();
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

            if (IsExistName(variable.Name, variable.Group))
            {
                Debug.Assert(Resource1.VariableGroup_addVariable_variableName_is_Exist != null,
                             "Resource1.VariableGroup_addVariable_variableName_is_Exist != null");
                throw new Exception(Resource1.VariableGroup_addVariable_variableName_is_Exist);
            }

            variable.Group.ChildVariables.Add(variable);
            
            //添加到仓库集合
            AddVar(variable);

            _variableContext.SaveVariable();
        }

        /// <summary>
        /// 删除指定变量
        /// </summary>
        /// <param name="variableName">变量名称</param>
        /// <param name="curVariableGroupId">移除变量所属组</param>
        public void RemoveVariable(string variableName, string curVariableGroupId)
        {
            if (curVariableGroupId==null)
            {
                Debug.Assert(Resource1.VariableUnitOfWork_RemoveGroup_curVariableGroupIdIsNullOrEmpty != null,
                    "Resource1.VariableUnitOfWork_RemoveGroup_curVariableGroupIdIsNullOrEmpty != null");
                throw new ArgumentNullException(Resource1.VariableUnitOfWork_RemoveGroup_curVariableGroupIdIsNullOrEmpty);
            }
            VariableGroup currentVariableGroup = GetGroupById(curVariableGroupId);
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            for (int index = 0; index < currentVariableGroup.ChildVariables.Count; index++)
            {
                VariableBase curVariable = currentVariableGroup.ChildVariables[index];
                if (curVariable.VariableBaseFullPath == variableName)
                {
                    currentVariableGroup.ChildVariables.Remove(curVariable);

                    //移除仓库集合中的变量
                    RemoveVar(curVariable);
                    break;
                }
            }

            _variableContext.SaveVariable();
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
            _variableContext.SaveVariable();
        }

        /// <summary>
        /// 更新指定变量
        /// </summary>
        /// <param name="variable">指定变量</param>
        /// <param name="newVariable">修改后的变量</param>
        public void EditVariable(VariableBase variable, VariableBase newVariable)
        {
            if (variable == null || newVariable == null)
            {
                Debug.Assert(Resource1.VariableRepository_AddVar_VariableIsNull != null,
                             "Resource1.VariableRepository_AddVar_VariableIsNull != null");
                throw new ArgumentNullException(Resource1.VariableRepository_AddVar_VariableIsNull);
            }

            if ((variable.Name != newVariable.Name) && (IsExistName(newVariable.Name, variable.Group)))
            {
                throw new Exception(Resource1.VariableUnitOfWork_EditVariable_AvarialeNameExist);
            }

            if (variable.ValueType == Varvaluetype.VarBool)
            {
                if (_variableContext.DigitalSet.Local.Any(m=>m.VariableBaseFullPath==variable.VariableBaseFullPath))
                {
                    _variableContext.DigitalSet.Local.First(m=>m.VariableBaseFullPath==variable.VariableBaseFullPath).CopyProperty(newVariable);
                }
            }
            else if (variable.ValueType == Varvaluetype.VarDouble)
            {
                if (_variableContext.AnalogSet.Local.Any(m => m.VariableBaseFullPath == variable.VariableBaseFullPath))
                {
                    _variableContext.AnalogSet.Local.First(m=>m.VariableBaseFullPath==variable.VariableBaseFullPath).CopyProperty(newVariable);
                }
            }
            else
            {
                if (_variableContext.StringSet.Local.Any(m=>m.VariableBaseFullPath==variable.VariableBaseFullPath))
                {
                    _variableContext.StringSet.Local.First(m=>m.VariableBaseFullPath==variable.VariableBaseFullPath).CopyProperty(newVariable);
                }
            }
            _variableContext.SaveVariable();
        }

        #endregion

        #region 私有方法

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
                if (_variableContext.DigitalSet.Local.Any(m => m.VariableBaseFullPath == variable.VariableBaseFullPath))
                {
                    Debug.Assert(Resource1.VariableRepository_AddVar_VariableIsExist != null,
                                 "Resource1.VariableRepository_AddVar_VariableIsExist != null");
                    throw new Exception(Resource1.VariableRepository_AddVar_VariableIsExist);
                }
                _variableContext.DigitalSet.Add(variable as DigitalVariable);
            }
            else if (variable.ValueType == Varvaluetype.VarDouble)
            {
                if (_variableContext.AnalogSet.Local.Any(m => m.VariableBaseFullPath == variable.VariableBaseFullPath))
                {
                    Debug.Assert(Resource1.VariableRepository_AddVar_VariableIsExist != null,
                                 "Resource1.VariableRepository_AddVar_VariableIsExist != null");
                    throw new Exception(Resource1.VariableRepository_AddVar_VariableIsExist);
                }
                _variableContext.AnalogSet.Add(variable as AnalogVariable);
            }
            else
            {
                if (_variableContext.StringSet.Local.Any(m => m.VariableBaseFullPath == variable.VariableBaseFullPath))
                {
                    Debug.Assert(Resource1.VariableRepository_AddVar_VariableIsExist != null,
                                 "Resource1.VariableRepository_AddVar_VariableIsExist != null");
                    throw new Exception(Resource1.VariableRepository_AddVar_VariableIsExist);
                }
                _variableContext.StringSet.Add(variable as TextVariable);
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
                _variableContext.AnalogSet.Remove(variable as AnalogVariable);
            }
            else if (variable.ValueType == Varvaluetype.VarBool)
            {
                _variableContext.DigitalSet.Remove(variable as DigitalVariable);
            }
            else if (variable.ValueType == Varvaluetype.VarString)
            {
                _variableContext.StringSet.Remove(variable as TextVariable);
            }
        }

        /// <summary>
        /// 获取指定变量组的变量默认名称
        /// </summary>
        /// <param name="currentVariableGroup">指定变量组</param>
        /// <returns>返回指定变量组的变量默认名称</returns>
        private static string GetInitVarName(VariableGroup currentVariableGroup)
        {
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            int cnt = 1;

            while (currentVariableGroup.ChildVariables.Any(curVar => curVar.Name == "Variable" + cnt))
            {
                cnt++;
            }
            return "Variable" + cnt;
        }

        /// <summary>
        /// 判断组或者变量的名称name是否在currentVariableGroup中存在
        /// </summary>
        /// <param name="name">组名称</param>
        /// <param name="currentVariableGroup">组对象</param>
        /// <returns>true:存在，false：不存在</returns>
        private bool IsExistName(string name, VariableGroup currentVariableGroup)
        {
            if (name == null)
            {
                Debug.Assert(Resource1.VariableUnitOfWork_IsExistName_nameIsNullOrEmpty != null,
                    "Resource1.VariableUnitOfWork_IsExistName_nameIsNullOrEmpty != null");
                throw new ArgumentNullException(Resource1.VariableUnitOfWork_IsExistName_nameIsNullOrEmpty);
            }
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            //如果父组包含groupName相同的组或者相同的变量，则返回不添加
            return currentVariableGroup.ChildGroups.Any(curGroup => curGroup.GroupName == name) 
                || currentVariableGroup.ChildVariables.Any(curVariable => curVariable.Name == name);
        }

        #endregion

    }
}
