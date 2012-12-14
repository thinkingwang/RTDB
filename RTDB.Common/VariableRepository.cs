using System;
using System.Linq;
using RTDB.VariableModel;
using RTDB.EntityFramework;

namespace RTDB.Common
{
    public class VariableRepository : IVariableRepository
    {
        #region 私有字段

        private readonly IVariableContext _variableContext;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbNameOrConnectingString">数据库名称或数据库连接字符串</param>
        public VariableRepository(string dbNameOrConnectingString = "VariableDB")
        {
            if (string.IsNullOrEmpty(dbNameOrConnectingString))
            {
                dbNameOrConnectingString = "VariableDB";
            }
            _variableContext = new VariableContext(dbNameOrConnectingString);

        }

        #endregion
        
        #region 组公共方法

        /// <summary>
        /// 根据组Id获取组对象
        /// </summary>
        /// <param name="curGroupFullPath">组全路径，等于null或为空字符返回根组</param>
        /// <returns>返回组对象，未找到返回null</returns>
        public VariableGroup GetGroup(string curGroupFullPath)
        {
            //等于null或为空字符返回根组
            if (string.IsNullOrEmpty(curGroupFullPath))
            {
                return VariableGroup.RootGroup;
            }

            if (curGroupFullPath.Split('.')[0] == VariableGroup.RootGroup.GroupFullPath)
            {
                curGroupFullPath = curGroupFullPath.Substring(curGroupFullPath.IndexOf('.') + 1);
            }
            
            return  _variableContext.VariableGroupSet.Local.FirstOrDefault(curGroup => curGroup.GroupFullPath == curGroupFullPath);
        }

        /// <summary>
        /// 向当前组添加子组
        /// </summary>
        /// <param name="groupName">子组名称</param>
        /// <param name="curGroupFullPath">要添加的组全路径</param>
        public void AddGroup(string groupName,string curGroupFullPath)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupNameIsNull);
            }

            VariableGroup parentVariableGroup = GetGroup(curGroupFullPath);
            if (parentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            var newGroup = new VariableGroup(groupName, parentVariableGroup);

            if (IsExistName(groupName, parentVariableGroup))
            {
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupeNameIsExist);
            }

            parentVariableGroup.ChildGroups.Add(newGroup);

            _variableContext.VariableGroupSet.Add(newGroup);
            _variableContext.Save();
        }

        /// <summary>
        /// 删除指定组
        /// </summary>
        /// <param name="curGroupFullPath">要移除的组全路径</param>
        public void RemoveGroup(string curGroupFullPath)
        {
            if (string.IsNullOrEmpty(curGroupFullPath))
            {
                throw new ArgumentNullException(Resource1.VariableUnitOfWork_RemoveGroup_curVariableGroupIdIsNullOrEmpty);
            }
            VariableGroup currentVariableGroup = GetGroup(curGroupFullPath);
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
            ClearVariable(curGroupFullPath);

            //删除该组
            if (currentVariableGroup.ParentGroupId <= 0)
            {
                //根组不允许
                throw new Exception(Resource1.VariableGroup_RemoveGroup_DeleteGroup_Is_RootGroup);
            }
            currentVariableGroup.Parent.ChildGroups.Remove(currentVariableGroup);
            _variableContext.VariableGroupSet.Remove(currentVariableGroup);
            _variableContext.Save();
        }

        /// <summary>
        /// 修改当前变量组名称
        /// </summary>
        /// <param name="groupName">修改后的组名</param>
        /// <param name="curGroupFullPath">要重命名的组全路径</param>
        public void RenameGroup(string groupName, string curGroupFullPath)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                throw new Exception(Resource1.VariableGroup_ReGroupName_groupName_Is_Null);
            }
            if (string.IsNullOrEmpty(curGroupFullPath))
            {
                throw new ArgumentNullException(Resource1.VariableUnitOfWork_RemoveGroup_curVariableGroupIdIsNullOrEmpty);
            }
            VariableGroup currentVariableGroup = GetGroup(curGroupFullPath);
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            if (IsExistName(groupName, currentVariableGroup.Parent))
            {
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupeNameIsExist);
            }
            currentVariableGroup.Name = groupName;
            _variableContext.Save();
        }

        /// <summary>
        /// 粘贴变量组
        /// </summary>
        /// <param name="sourceGroup">需要粘贴的变量组</param>
        /// <param name="curGroupFullPath">粘贴变量的目标组</param>
        /// <param name="isCopy">是否为复制，true为复制，false为剪切</param>
        /// <param name="pasteMode">粘贴模式，0：默认模式，重复则返回，1：如果重复则替换，2：如果重复则两个变量都保留，3：如果重复则放弃</param>
        /// <returns>如果默认模式下且有相同变量名称存在返回变量新名称，否则返回粘贴变量名称</returns>
        public string PasteGroup(VariableGroup sourceGroup, string curGroupFullPath, bool isCopy,
                                  uint pasteMode = 0)
        {
            if (sourceGroup == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_PasteVariable_sourceVariable);
            }

            if (curGroupFullPath == null)
            {
                throw new ArgumentNullException(Resource1.VariableUnitOfWork_RemoveGroup_curVariableGroupIdIsNullOrEmpty);
            }

            VariableGroup desGroup = GetGroup(curGroupFullPath);
            if (desGroup == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_PasteVariable_desGroup);
            }

            if (IsExistName(sourceGroup.Name, desGroup) && (pasteMode == 0))
            {
                return GetInitVarName(desGroup, sourceGroup.Name); //保留两个变量后新的变量名
            }

            if (pasteMode <= 2)
            {
                //替换
                if (pasteMode == 1) //替换
                {
                    RemoveGroup(desGroup.GroupFullPath + "." + sourceGroup.Name);
                }

                if (isCopy)
                {
                    CopyGroup(sourceGroup, desGroup, pasteMode);
                }
                else
                {
                    sourceGroup.Parent.ChildGroups.Remove(sourceGroup);
                    sourceGroup.Parent = desGroup;
                    if (pasteMode == 2) //同时保留两个
                    {
                        sourceGroup.Name = GetInitVarName(desGroup, sourceGroup.Name);
                    }
                    desGroup.ChildGroups.Add(sourceGroup);
                }

                _variableContext.Save();
            }

            return sourceGroup.Name;
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
                throw new ArgumentNullException(Resource1.VariableGroup_AddVariable_variable_is_null);
            }
            if (variable.Parent == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            
            variable.Name = (string.IsNullOrEmpty(variable.Name)) ? GetInitVarName(variable.Parent) : variable.Name;

            if (IsExistName(variable.Name, variable.Parent))
            {
                throw new Exception(Resource1.VariableGroup_addVariable_variableName_is_Exist);
            }

            variable.Parent.ChildVariables.Add(variable);
            
            //添加到仓库集合
            AddVar(variable);

            _variableContext.Save();
        }

        /// <summary>
        /// 删除指定变量
        /// </summary>
        /// <param name="variableName">变量名称</param>
        /// <param name="curGroupFullPath">移除变量所属组全路径</param>
        public void RemoveVariable(string variableName, string curGroupFullPath)
        {
            if (curGroupFullPath==null)
            {
                throw new ArgumentNullException(Resource1.VariableUnitOfWork_RemoveGroup_curVariableGroupIdIsNullOrEmpty);
            }
            VariableGroup currentVariableGroup = GetGroup(curGroupFullPath);
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

            _variableContext.Save();
        }

        /// <summary>
        /// 删除当前组的所有变量
        /// </summary>
        /// <param name="curGroupFullPath">要清空的组全路径</param>
        public void ClearVariable(string curGroupFullPath)
        {
            if (curGroupFullPath == null)
            {
                throw new ArgumentNullException(Resource1.VariableUnitOfWork_RemoveGroup_curVariableGroupIdIsNullOrEmpty);
            }
            VariableGroup currentVariableGroup = GetGroup(curGroupFullPath);
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
            _variableContext.Save();
        }

        /// <summary>
        /// 更新指定变量
        /// </summary>
        /// <param name="oldVariable">指定变量</param>
        /// <param name="newVariable">修改后的变量</param>
        public void EditVariable(VariableBase oldVariable, VariableBase newVariable)
        {
            if (oldVariable == null || newVariable == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_AddVar_VariableIsNull);
            }

            if ((oldVariable.Name != newVariable.Name) && (IsExistName(newVariable.Name, oldVariable.Parent)))
            {
                throw new Exception(Resource1.VariableUnitOfWork_EditVariable_AvarialeNameExist);
            }

            if (oldVariable.ValueType == Varvaluetype.VarBool)
            {
                if (_variableContext.DigitalSet.Local.Any(m=>m.VariableBaseFullPath==oldVariable.VariableBaseFullPath))
                {
                    _variableContext.DigitalSet.Local.First(m=>m.VariableBaseFullPath==oldVariable.VariableBaseFullPath).CopyProperty(newVariable);
                }
            }
            else if (oldVariable.ValueType == Varvaluetype.VarDouble)
            {
                if (_variableContext.AnalogSet.Local.Any(m => m.VariableBaseFullPath == oldVariable.VariableBaseFullPath))
                {
                    _variableContext.AnalogSet.Local.First(m=>m.VariableBaseFullPath==oldVariable.VariableBaseFullPath).CopyProperty(newVariable);
                }
            }
            else
            {
                if (_variableContext.StringSet.Local.Any(m=>m.VariableBaseFullPath==oldVariable.VariableBaseFullPath))
                {
                    _variableContext.StringSet.Local.First(m=>m.VariableBaseFullPath==oldVariable.VariableBaseFullPath).CopyProperty(newVariable);
                }
            }
            _variableContext.Save();
        }
        
        /// <summary>
        /// 粘贴变量
        /// </summary>
        /// <param name="sourceVariable">需要粘贴的变量</param>
        /// <param name="curGroupFullPath">粘贴变量的目标组</param>
        /// <param name="isCopy">是否为复制，true为复制，false为剪切</param>
        /// <param name="pasteMode">粘贴模式，0：默认模式，重复则返回，1：如果重复则替换，2：如果重复则两个变量都保留，3：如果重复则放弃</param>
        /// <returns>如果默认模式下且有相同变量名称存在返回变量新名称，否则返回粘贴变量名称</returns>
        public string PasteVariable(VariableBase sourceVariable, string curGroupFullPath, bool isCopy,
                                    uint pasteMode = 0)
        {
            if (sourceVariable == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_PasteVariable_sourceVariable);
            }

            if (curGroupFullPath == null)
            {
                throw new ArgumentNullException(Resource1.VariableUnitOfWork_RemoveGroup_curVariableGroupIdIsNullOrEmpty);
            }

            VariableGroup desGroup = GetGroup(curGroupFullPath);
            if (desGroup == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_PasteVariable_desGroup);
            }

            if (IsExistName(sourceVariable.Name, desGroup) && (pasteMode == 0))
            {
                return GetInitVarName(desGroup, sourceVariable.Name); //保留两个变量后新的变量名
            }

            if (pasteMode <= 2)
            {
                //替换
                if (pasteMode == 1) //替换
                {
                    RemoveVariable(sourceVariable.Name, desGroup.GroupFullPath);
                }

                if (isCopy)
                {
                    VariableBase var;
                    if (sourceVariable is AnalogVariable)
                    {
                        var = new AnalogVariable(desGroup);
                    }
                    else if (sourceVariable is DigitalVariable)
                    {
                        var = new DigitalVariable(desGroup);
                    }
                    else
                    {
                        var = new TextVariable(desGroup);
                    }
                    var.CopyProperty(sourceVariable);
                    if (pasteMode == 2) //同时保留两个
                    {
                        var.Name = GetInitVarName(desGroup, sourceVariable.Name);
                    }
                    AddVariable(var);
                }
                else
                {
                    sourceVariable.Parent.ChildVariables.Remove(sourceVariable);
                    sourceVariable.Parent = desGroup;
                    if (pasteMode == 2) //同时保留两个
                    {
                        sourceVariable.Name = GetInitVarName(desGroup, sourceVariable.Name);
                    }
                    desGroup.ChildVariables.Add(sourceVariable);
                }

                _variableContext.Save();
            }

            return sourceVariable.Name;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 复制变量组
        /// </summary>
        /// <param name="sourse"></param>
        /// <param name="desGroup"></param>
        /// <param name="pasteMode"></param>
        private void CopyGroup(VariableGroup sourse, VariableGroup desGroup, uint pasteMode)
        {
            if (sourse == null)
            {
                return;
            }
            string groupName = sourse.Name;
            if (IsExistName(sourse.Name, desGroup) && (pasteMode == 2))//同时保留两个
            {
                groupName = GetInitVarName(desGroup, sourse.Name);
            }
            AddGroup(groupName, desGroup.GroupFullPath);

            VariableGroup var = GetGroup(desGroup.GroupFullPath + "." + groupName);
            foreach (var childVariable in sourse.ChildVariables)
            {
                VariableBase varVariable;
                if (childVariable is DigitalVariable)
                {
                    varVariable = new DigitalVariable(var);
                    varVariable.CopyProperty(childVariable);
                    AddVariable(varVariable);
                }
                if (childVariable is AnalogVariable)
                {
                    varVariable = new AnalogVariable(var);
                    varVariable.CopyProperty(childVariable);
                    AddVariable(varVariable);
                }
                if (childVariable is TextVariable)
                {
                    varVariable = new TextVariable(var);
                    varVariable.CopyProperty(childVariable);
                    AddVariable(varVariable);
                }
            }

            foreach (var variableGroup in sourse.ChildGroups)
            {
                CopyGroup(variableGroup, var, pasteMode);
            }
        }

        /// <summary>
        /// 增加指定变量
        /// </summary>
        /// <param name="variable">指定变量</param>
        private void AddVar(VariableBase variable)
        {
            if (variable == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_AddVar_VariableIsNull);
            }
            if (variable.ValueType == Varvaluetype.VarBool)
            {
                if (_variableContext.DigitalSet.Local.Any(m => m.VariableBaseFullPath == variable.VariableBaseFullPath))
                {
                    throw new Exception(Resource1.VariableRepository_AddVar_VariableIsExist);
                }
                _variableContext.DigitalSet.Add(variable as DigitalVariable);
            }
            else if (variable.ValueType == Varvaluetype.VarDouble)
            {
                if (_variableContext.AnalogSet.Local.Any(m => m.VariableBaseFullPath == variable.VariableBaseFullPath))
                {
                    throw new Exception(Resource1.VariableRepository_AddVar_VariableIsExist);
                }
                _variableContext.AnalogSet.Add(variable as AnalogVariable);
            }
            else
            {
                if (_variableContext.StringSet.Local.Any(m => m.VariableBaseFullPath == variable.VariableBaseFullPath))
                {
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
        /// <param name="defaultName">默认名称前缀</param>
        /// <returns>返回指定变量组的变量默认名称</returns>
        private string GetInitVarName(VariableGroup currentVariableGroup, string defaultName = "Variable")
        {
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }

            if (string.IsNullOrEmpty(defaultName))
            {
                defaultName = "Variable";
            }
            int cnt = 1;

            while (currentVariableGroup.ChildVariables.Any(curVar => curVar.Name == defaultName + cnt) ||
                   currentVariableGroup.ChildGroups.Any(curVar => curVar.Name == defaultName + cnt))
            {
                cnt++;
            }
            return defaultName + cnt;
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
                throw new ArgumentNullException(Resource1.VariableUnitOfWork_IsExistName_nameIsNullOrEmpty);
            }
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            //如果父组包含groupName相同的组或者相同的变量，则返回不添加
            return currentVariableGroup.ChildGroups.Any(curGroup => curGroup.Name == name) 
                || currentVariableGroup.ChildVariables.Any(curVariable => curVariable.Name == name);
        }

        #endregion

    }
}
