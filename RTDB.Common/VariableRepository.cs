using System;
using System.Collections.Generic;
using System.Linq;
using SCADA.RTDB.EntityFramework;
using SCADA.RTDB.VariableModel;

namespace SCADA.RTDB.Repository
{
    public class VariableRepository : IVariableRepository
    {
        #region 私有字段

        private readonly IVariableContext _variableContext;
        private bool _isChanged;

        #endregion

        /// <summary>
        /// 变量及变量组集合发生改变时触发事件
        /// </summary>
        public event DataChangedEvent DataChanged;

        /// <summary>
        /// 变量及变量组集合是否改变
        /// </summary>
        public bool IsChanged
        {
            get { return _isChanged; }
            private set
            {
                _isChanged = value;
                if (_isChanged)
                {
                    if (DataChanged != null) DataChanged();
                }
            }
        }

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
        /// <param name="fullPath">组全路径，等于null或为空字符返回根组</param>
        /// <returns>返回组对象，未找到返回null</returns>
        public VariableGroup FindGroupById(string fullPath)
        {
            //等于null或为空字符返回根组
            if (string.IsNullOrEmpty(fullPath))
            {
                return VariableGroup.RootGroup;
            }

            return _variableContext.VariableGroupSet.Local.FirstOrDefault(curGroup => curGroup.FullPath == fullPath);

        }
        /// <summary>
        /// 根据组Id提供的路径信息，遍历树查找组节点
        /// </summary>
        /// <param name="fullPath">组全路径，等于null或为空字符返回根组</param>
        /// <returns>返回组对象，未找到返回null</returns>
        public VariableGroup FindGroupByPath(string fullPath)
        {
            //等于null或为空字符返回根组
            if (string.IsNullOrEmpty(fullPath))
            {
                return VariableGroup.RootGroup;
            }

            return findRecursion(VariableGroup.RootGroup, fullPath);

        }

        /// <summary>
        /// 查询组路径下面所有子组
        /// </summary>
        /// <param name="fullPath">组路径</param>
        /// <returns>所有子组列表</returns>
        public IEnumerable<VariableGroup> FindGroups(string fullPath)
        {
            VariableGroup variableGroup = FindGroupById(fullPath);
            return variableGroup == null ? null : variableGroup.ChildGroups;
        }

        /// <summary>
        /// 向当前组添加子组
        /// </summary>
        /// <param name="name">子组名称</param>
        /// <param name="fullPath">要添加的组全路径</param>
        public void AddGroup(string name, string fullPath)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupNameIsNull);
            }

            VariableGroup parentVariableGroup = FindGroupById(fullPath);
            if (parentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            var newGroup = new VariableGroup(name, parentVariableGroup);

            if (IsExistName(name, parentVariableGroup))
            {
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupeNameIsExist);
            }

            parentVariableGroup.ChildGroups.Add(newGroup);
            IsChanged = true;

        }

        /// <summary>
        /// 删除指定组
        /// </summary>
        /// <param name="fullPath">要移除的组全路径</param>
        public void RemoveGroup(string fullPath)
        {
            VariableGroup currentVariableGroup = FindGroupById(fullPath);
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            //删除该组下面的子组
            while (currentVariableGroup.ChildGroups.Count > 0)
            {
                RemoveGroup(currentVariableGroup.ChildGroups[0].FullPath);
            }

            //删除该组下的变量
            ClearVariable(fullPath);

            //删除该组
            if (currentVariableGroup.Parent == null)
            {
                //根组不允许
                throw new Exception(Resource1.VariableGroup_RemoveGroup_DeleteGroup_Is_RootGroup);
            }
            currentVariableGroup.Parent.ChildGroups.Remove(currentVariableGroup);
            _variableContext.VariableGroupSet.Remove(currentVariableGroup);
            IsChanged = true;
        }

        /// <summary>
        /// 修改当前变量组名称
        /// </summary>
        /// <param name="name">修改后的组名</param>
        /// <param name="fullPath">要重命名的组全路径</param>
        public void RenameGroup(string name, string fullPath)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception(Resource1.VariableGroup_ReGroupName_groupName_Is_Null);
            }
            VariableGroup currentVariableGroup = FindGroupById(fullPath);
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            if (IsExistName(name, currentVariableGroup.Parent))
            {
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupeNameIsExist);
            }
            currentVariableGroup.Name = name;
            IsChanged = true;
        }

        /// <summary>
        /// 粘贴变量组
        /// </summary>
        /// <param name="source">需要粘贴的变量组</param>
        /// <param name="fullPath">粘贴变量的目标组,null为根组</param>
        /// <param name="isCopy">是否为复制，true为复制，false为剪切</param>
        /// <param name="pasteMode">粘贴模式，0：默认模式，重复则返回，1：如果重复则替换，2：如果重复则两个变量都保留，3：如果重复则放弃</param>
        /// <returns>如果默认模式下且有相同变量名称存在返回变量新名称，否则返回粘贴变量名称</returns>
        public string PasteGroup(VariableGroup source, string fullPath, bool isCopy,
                                  uint pasteMode = 0)
        {
            if (source == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_PasteVariable_sourceVariable);
            }

            //目标组是源文件的子文件组，不允许粘贴
            if ((source.FullPath == null) || (fullPath != null && fullPath.Contains(source.FullPath)))
            {
                throw new Exception(Resource1.VariableRepository_PasteGroup_SourceGroupContainDesGroup);
            }

            VariableGroup desGroup = FindGroupById(fullPath);
            if (desGroup == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_PasteVariable_desGroup);
            }

            if (IsExistName(source.Name, desGroup) && (pasteMode == 0))
            {
                return GetDefaultName(desGroup, source.Name); //保留两个变量后新的变量名
            }

            if (pasteMode <= 2)
            {
                //替换
                if (pasteMode == 1) //替换
                {
                    if (source.Parent != null && desGroup.FullPath == source.Parent.FullPath) //如果源与目标位置相同，无需替换
                    {
                        return source.Name;
                    }
                    RemoveGroup(desGroup.FullPath + "." + source.Name);
                }

                if (isCopy)
                {
                    CopyGroup(source, desGroup, pasteMode);
                }
                else
                {
                    if (source.Parent == null)
                    {
                        return null;
                    }
                    source.Parent.ChildGroups.Remove(source);
                    source.Parent = desGroup;
                    if (pasteMode == 2) //同时保留两个
                    {
                        source.Name = GetDefaultName(desGroup, source.Name);
                    }
                    desGroup.ChildGroups.Add(source);
                }
            }
            IsChanged = true;
            return source.Name;
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
            if (variable.ParentGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }

            variable.Name = (string.IsNullOrEmpty(variable.Name)) ? GetDefaultName(variable.ParentGroup) : variable.Name;

            if (IsExistName(variable.Name, variable.ParentGroup))
            {
                throw new Exception(Resource1.VariableGroup_addVariable_variableName_is_Exist);
            }
            if (variable is AnalogVariable)
            {
                variable.ParentGroup.AnalogVariables.Add(variable as AnalogVariable);
            }
            if (variable is DigitalVariable)
            {
                variable.ParentGroup.DigitalVariables.Add(variable as DigitalVariable);
            }
            if (variable is TextVariable)
            {
                variable.ParentGroup.TextVariables.Add(variable as TextVariable);
            }
            variable.ParentGroup.ChildVariables.Add(variable);
            IsChanged = true;

        }

        /// <summary>
        /// 删除指定变量
        /// </summary>
        /// <param name="name">变量名称</param>
        /// <param name="fullPath">移除变量所属组全路径</param>
        public void RemoveVariable(string name, string fullPath)
        {
            VariableGroup currentVariableGroup = FindGroupById(fullPath);
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }

            for (int index = 0; index < currentVariableGroup.ChildVariables.Count; index++)
            {
                VariableBase curVariable = currentVariableGroup.ChildVariables[index];
                if (curVariable.Name == name)
                {
                    currentVariableGroup.ChildVariables.RemoveAt(index);
                    //移除仓库集合中的变量
                    RemoveVar(curVariable);
                    IsChanged = true;
                    return;
                }
            }
            IsChanged = false;
        }

        /// <summary>
        /// 删除当前组的所有变量
        /// </summary>
        /// <param name="fullPath">要清空的组全路径</param>
        public void ClearVariable(string fullPath)
        {
            VariableGroup currentVariableGroup = FindGroupById(fullPath);
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
            IsChanged = true;
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

            if ((oldVariable.Name != newVariable.Name) && (IsExistName(newVariable.Name, oldVariable.ParentGroup)))
            {
                throw new Exception(Resource1.VariableUnitOfWork_EditVariable_AvarialeNameExist);
            }

            if (oldVariable.ValueType == Varvaluetype.VarBool)
            {
                if (_variableContext.DigitalSet.Local.Any(m => m.FullPath == oldVariable.FullPath))
                {
                    _variableContext.DigitalSet.Local.First(m => m.FullPath == oldVariable.FullPath).CopyProperty(newVariable);
                }
            }
            else if (oldVariable.ValueType == Varvaluetype.VarDouble)
            {
                if (_variableContext.AnalogSet.Local.Any(m => m.FullPath == oldVariable.FullPath))
                {
                    _variableContext.AnalogSet.Local.First(m => m.FullPath == oldVariable.FullPath).CopyProperty(newVariable);
                }
            }
            else
            {
                if (_variableContext.TextSet.Local.Any(m => m.FullPath == oldVariable.FullPath))
                {
                    _variableContext.TextSet.Local.First(m => m.FullPath == oldVariable.FullPath).CopyProperty(newVariable);
                }
            }
            IsChanged = true;
        }

        /// <summary>
        /// 根据变量Id查找变量
        /// </summary>
        /// <param name="fullPath">变量全路径，如果路径指向组则返回组下所有节点，否则返回找到的变量或null</param>
        /// <returns>返回变量对象，未找到返回null</returns>
        public VariableBase FindVariableById(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath))
            {
                return null;
            }
            VariableBase variable =
                _variableContext.AnalogSet.Local.FirstOrDefault(m => m.FullPath == fullPath) ??
                (VariableBase)_variableContext.DigitalSet.Local.FirstOrDefault(m => m.FullPath == fullPath);
            return variable ??
                   (_variableContext.TextSet.Local.FirstOrDefault(m => m.FullPath == fullPath));
        }

        /// <summary>
        /// 根据变量Id提供的路径信息，遍历树查找变量
        /// </summary>
        /// <param name="fullPath">变量全路径</param>
        /// <returns>返回变量对象，未找到返回null</returns>
        public VariableBase FindVariableByPath(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath))
            {
                return null;
            }

            if (!fullPath.Contains('.'))
            {
                return VariableGroup.RootGroup.ChildVariables.Find(m => m.FullPath == fullPath);
            }
            var variableGroup = findRecursion(VariableGroup.RootGroup,
                                                        fullPath.Substring(0, fullPath.LastIndexOf('.')));

            return variableGroup == null
                       ? null
                       : VariableGroup.RootGroup.ChildVariables.Find(m => m.FullPath == fullPath);
        }

        /// <summary>
        /// 查询组路径下面所有变量
        /// </summary>
        /// <param name="fullPath">组路径</param>
        /// <returns>所有变量列表</returns>
        public IEnumerable<VariableBase> FindVariables(string fullPath)
        {
            VariableGroup variableGroup = FindGroupById(fullPath);
            if (variableGroup == null)
            {
                return null;
            }
            return variableGroup.ChildVariables;
        }

        /// <summary>
        /// 粘贴变量
        /// </summary>
        /// <param name="source">需要粘贴的变量</param>
        /// <param name="fullPath">粘贴变量的目标组</param>
        /// <param name="isCopy">是否为复制，true为复制，false为剪切</param>
        /// <param name="pasteMode">粘贴模式，0：默认模式，重复则返回，1：如果重复则替换，2：如果重复则两个变量都保留，3：如果重复则放弃</param>
        /// <returns>如果默认模式下且有相同变量名称存在返回变量新名称，否则返回粘贴变量名称</returns>
        public string PasteVariable(VariableBase source, string fullPath, bool isCopy,
                                    uint pasteMode = 0)
        {
            if (source == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_PasteVariable_sourceVariable);
            }

            VariableGroup desGroup = FindGroupById(fullPath);
            if (desGroup == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_PasteVariable_desGroup);
            }

            if (IsExistName(source.Name, desGroup) && (pasteMode == 0))
            {
                return GetDefaultName(desGroup, source.Name); //保留两个变量后新的变量名
            }

            if (pasteMode <= 2)
            {
                if (pasteMode == 1) //替换
                {
                    RemoveVariable(source.Name, desGroup.FullPath);
                }
                if (isCopy)
                {
                    VariableBase var;
                    if (source is AnalogVariable)
                    {
                        var = new AnalogVariable(desGroup);
                    }
                    else if (source is DigitalVariable)
                    {
                        var = new DigitalVariable(desGroup);
                    }
                    else
                    {
                        var = new TextVariable(desGroup);
                    }
                    var.CopyProperty(source);

                    if (pasteMode == 2) //同时保留两个
                    {
                        var.Name = GetDefaultName(desGroup, source.Name);
                    }
                    AddVariable(var);
                }
                else
                {
                    source.ParentGroup.ChildVariables.Remove(source);
                    source.ParentGroup = desGroup;
                    if (pasteMode == 2) //同时保留两个
                    {
                        source.Name = GetDefaultName(desGroup, source.Name);
                    }
                    desGroup.ChildVariables.Add(source);

                }
            }
            IsChanged = true;
            return source.Name;
        }

        /// <summary>
        /// 保存变量
        /// </summary>
        public void Save()
        {
            _variableContext.Save();
            IsChanged = false;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 复制变量组
        /// </summary>
        /// <param name="sourse">源</param>
        /// <param name="group">目标</param>
        /// <param name="pasteMode">粘贴模式，0：默认模式，重复则返回，1：如果重复则替换，2：如果重复则两个变量都保留，3：如果重复则放弃</param>
        private void CopyGroup(VariableGroup sourse, VariableGroup group, uint pasteMode)
        {
            if (sourse == null)
            {
                return;
            }
            string groupName = sourse.Name;
            if ((pasteMode == 2) && IsExistName(sourse.Name, group))//同时保留两个
            {
                groupName = GetDefaultName(group, sourse.Name);
            }
            AddGroup(groupName, group.FullPath);

            VariableGroup var =
                FindGroupById(group.FullPath == null ? groupName : group.FullPath + "." + groupName);

            foreach (var childVariable in sourse.ChildVariables)
            {
                var varVariable = new AnalogVariable(var);
                varVariable.CopyProperty(childVariable);
                AddVariable(varVariable);
            }

            foreach (var variableGroup in sourse.ChildGroups)
            {
                CopyGroup(variableGroup, var, pasteMode);
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
                _variableContext.TextSet.Remove(variable as TextVariable);
            }
        }

        /// <summary>
        /// 获取指定变量组的变量默认名称
        /// </summary>
        /// <param name="group">指定变量组</param>
        /// <param name="defaultName">默认名称前缀</param>
        /// <returns>返回指定变量组的变量默认名称</returns>
        private string GetDefaultName(VariableGroup group, string defaultName = "Variable")
        {
            if (group == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }

            if (string.IsNullOrEmpty(defaultName))
            {
                defaultName = "Variable";
            }
            int cnt = 1;

            while (group.ChildVariables.Any(curVar => curVar.Name == defaultName + cnt) ||
                   group.ChildGroups.Any(curVar => curVar.Name == defaultName + cnt))
            {
                cnt++;
            }
            return defaultName + cnt;
        }

        /// <summary>
        /// 判断组或者变量的名称name是否在currentVariableGroup中存在
        /// </summary>
        /// <param name="name">组名称</param>
        /// <param name="group">组对象</param>
        /// <returns>true:存在，false：不存在</returns>
        private bool IsExistName(string name, VariableGroup group)
        {
            if (name == null)
            {
                throw new ArgumentNullException(Resource1.VariableUnitOfWork_IsExistName_nameIsNullOrEmpty);
            }
            if (group == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            //如果父组包含groupName相同的组或者相同的变量，则返回不添加
            return group.ChildGroups.Any(curGroup => curGroup.Name == name)
                   || group.ChildVariables.Any(curVariable => curVariable.Name == name);
        }

        /// <summary>
        /// 递归查找组内部方法
        /// </summary>
        /// <param name="group"></param>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        private VariableGroup findRecursion(VariableGroup group, string fullPath)
        {
            if (group == null)
            {
                return null;
            }
            if (!fullPath.Contains('.'))
            {
                return group.ChildGroups.FirstOrDefault(m => m.Name == fullPath);
            }
            return findRecursion(group.ChildGroups.FirstOrDefault(m => m.Name == fullPath.Split('.')[0]),
                     fullPath.Substring(fullPath.IndexOf('.') + 1));
        }

        #endregion

    }
}
