using System;
using System.Collections.Generic;
using System.Linq;
using SCADA.RTDB.EntityFramework;
using SCADA.RTDB.VariableModel;

namespace SCADA.RTDB.Repository
{
    public class VariableRepository : IVariableRepository
    {
        public static IVariableContext VariableDbContext { get; private set; }
        
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbNameOrConnectingString">数据库名称或数据库连接字符串</param>
        public VariableRepository(string dbNameOrConnectingString = "VariableDB")
        {
            //实体存在不需要再次创建，直接返回
            if (VariableDbContext != null)
            {
                return;
            }
            if (String.IsNullOrEmpty(dbNameOrConnectingString))
            {
                dbNameOrConnectingString = "VariableDB";
            }
            VariableDbContext = new VariableContext(dbNameOrConnectingString);
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
            if (String.IsNullOrEmpty(fullPath))
            {
                return VariableGroup.RootGroup;
            }

            return VariableDbContext.VariableGroupSet.Local.FirstOrDefault(curGroup => curGroup.FullPath == fullPath);

        }

        /// <summary>
        /// 根据组Id提供的路径信息，遍历树查找组节点
        /// </summary>
        /// <param name="fullPath">组全路径，等于null或为空字符返回根组</param>
        /// <returns>返回组对象，未找到返回null</returns>
        public VariableGroup FindGroupByPath(string fullPath)
        {
            //等于null或为空字符返回根组
            if (String.IsNullOrEmpty(fullPath))
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
            VariableGroup variableGroup = FindGroupByPath(fullPath);
            return variableGroup == null ? null : variableGroup.ChildGroups;
        }

        /// <summary>
        /// 向当前组添加子组
        /// </summary>
        /// <param name="name">子组名称</param>
        /// <param name="fullPath">要添加的组全路径</param>
        public void AddGroup(string name, string fullPath)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupNameIsNull);
            }

            VariableGroup parentVariableGroup = FindGroupByPath(fullPath);
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
            SaveAllChanges();

        }

        /// <summary>
        /// 删除指定组
        /// </summary>
        /// <param name="fullPath">要移除的组全路径</param>
        public void RemoveGroup(string fullPath)
        {
            VariableGroup currentVariableGroup = FindGroupByPath(fullPath);
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
            VariableDbContext.VariableGroupSet.Remove(currentVariableGroup);
            SaveAllChanges();
        }

        /// <summary>
        /// 修改当前变量组名称
        /// </summary>
        /// <param name="name">修改后的组名</param>
        /// <param name="fullPath">要重命名的组全路径</param>
        public void RenameGroup(string name, string fullPath)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new Exception(Resource1.VariableGroup_ReGroupName_groupName_Is_Null);
            }
            VariableGroup currentVariableGroup = FindGroupByPath(fullPath);
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            if (IsExistName(name, currentVariableGroup.Parent))
            {
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupeNameIsExist);
            }
            currentVariableGroup.Name = name;
            SaveAllChanges();
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
            //根组不能复制
            if (source.Parent == null)
            {
                throw new Exception("需要粘贴的组为根组，不能粘贴");
            }
            if (source == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_PasteVariable_sourceVariable);
            }

            //目标组是源文件的子文件组，不允许粘贴
            if ((source.FullPath == null) || (fullPath != null && fullPath.Contains(source.FullPath)))
            {
                throw new Exception(Resource1.VariableRepository_PasteGroup_SourceGroupContainDesGroup);
            }

            VariableGroup desGroup = FindGroupByPath(fullPath);
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
                    if (desGroup.FullPath == source.Parent.FullPath) //如果源与目标位置相同，无需替换
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
                    source.Parent.ChildGroups.Remove(source);
                    source.Parent = desGroup;
                    if (pasteMode == 2) //同时保留两个
                    {
                        source.Name = GetDefaultName(desGroup, source.Name);
                    }
                    desGroup.ChildGroups.Add(source);
                }
            }
            SaveAllChanges();
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

            variable.Name = (String.IsNullOrEmpty(variable.Name)) ? GetDefaultName(variable.ParentGroup) : variable.Name;

            if (IsExistName(variable.Name, variable.ParentGroup))
            {
                throw new Exception(Resource1.VariableGroup_addVariable_variableName_is_Exist);
            }
            variable.ParentGroup.AddVariableToGroup(variable);
            SaveAllChanges();

        }

        /// <summary>
        /// 修改变量属性
        /// </summary>
        /// <param name="variable">修改前变量</param>
        /// <param name="variableStrings">修改后变量属性字符串序列</param>
        /// <returns>修改成功返回true，修改失败返回false</returns>
        public bool EditVariable(VariableBase variable, List<string> variableStrings)
        {
            if (variable.Name != variableStrings[0] && IsExistName(variableStrings[0], variable.ParentGroup))
            {
                throw new Exception(Resource1.VariableUnitOfWork_EditVariable_AvarialeNameExist);
            }
            bool result = variable.EditVariable(variableStrings);
            SaveAllChanges();
            return result;
        }

        /// <summary>
        /// 修改变量属性
        /// </summary>
        /// <param name="variable">修改前变量</param>
        /// <param name="newVariable">修改后变量</param>
        /// <returns>修改成功返回true，修改失败返回false</returns>
        public bool EditVariable(VariableBase variable, VariableBase newVariable)
        {
            if (variable.Name != newVariable.Name && IsExistName(newVariable.Name, variable.ParentGroup))
            {
                throw new Exception(Resource1.VariableUnitOfWork_EditVariable_AvarialeNameExist);
            }
            bool result = variable.EditVariable(newVariable);
            SaveAllChanges();
            return result;
        }

        /// <summary>
        /// 设置变量值
        /// </summary>
        /// <param name="variable">变量</param>
        /// <param name="value">变量值</param>
        /// <returns>设置成功返回true，失败返回false</returns>
        public bool SetVariableValue(VariableBase variable, object value)
        {
            if (!variable.SetValue(value))
            {
                return false;
            }
            //是否需要保存历史记录
            //if (variable.IsInitValueSaved)
            //{
            //    SaveAllChanges();
            //}
            return true;
        }

        /// <summary>
        /// 删除指定变量
        /// </summary>
        /// <param name="name">变量名称</param>
        /// <param name="fullPath">移除变量所属组全路径</param>
        public void RemoveVariable(string name, string fullPath)
        {
            VariableGroup currentVariableGroup = FindGroupByPath(fullPath);
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            var curVariable = currentVariableGroup.ChildVariables.FirstOrDefault(p => p.Name == name);
            if (curVariable == null)
            {
                return;
            }
            //从仓储中移除变量
            VariableDbContext.Remove(curVariable);
            SaveAllChanges();
        }

        /// <summary>
        /// 删除当前组的所有变量
        /// </summary>
        /// <param name="fullPath">要清空的组全路径</param>
        public void ClearVariable(string fullPath)
        {
            VariableGroup currentVariableGroup = FindGroupByPath(fullPath);
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            //删除该组下的变量
            while (currentVariableGroup.ChildVariables.Count > 0)
            {
                VariableDbContext.Remove(currentVariableGroup.ChildVariables[0]);
            }
            SaveAllChanges();
        }

        /// <summary>
        /// 根据变量Id查找变量
        /// </summary>
        /// <param name="fullPath">变量全路径，如果路径指向组则返回组下所有节点，否则返回找到的变量或null</param>
        /// <returns>返回变量对象，未找到返回null</returns>
        public VariableBase FindVariableById(string fullPath)
        {
            if (String.IsNullOrEmpty(fullPath))
            {
                return null;
            }
            VariableBase variable =
                VariableDbContext.AnalogSet.Local.FirstOrDefault(m => m.AbsolutePath == fullPath) ??
                (VariableBase)VariableDbContext.DigitalSet.Local.FirstOrDefault(m => m.AbsolutePath == fullPath);
            return variable ??
                   (VariableDbContext.TextSet.Local.FirstOrDefault(m => m.AbsolutePath == fullPath));
        }

        /// <summary>
        /// 根据变量Id提供的路径信息，遍历树查找变量
        /// </summary>
        /// <param name="fullPath">变量全路径</param>
        /// <returns>返回变量对象，未找到返回null</returns>
        public VariableBase FindVariableByPath(string fullPath)
        {
            if (String.IsNullOrEmpty(fullPath))
            {
                return null;
            }

            if (!fullPath.Contains('.'))
            {
                return VariableGroup.RootGroup.ChildVariables.Find(m => m.AbsolutePath == fullPath);
            }
            var variableGroup = findRecursion(VariableGroup.RootGroup,
                                                        fullPath.Substring(0, fullPath.LastIndexOf('.')));

            return variableGroup == null
                       ? null
                       : variableGroup.ChildVariables.Find(m => m.AbsolutePath == fullPath);
        }
        
        /// <summary>
        /// 查询组路径下面所有变量
        /// </summary>
        /// <param name="fullPath">组路径</param>
        /// <returns>所有变量列表</returns>
        public IEnumerable<VariableBase> FindVariables(string fullPath)
        {
            VariableGroup variableGroup = FindGroupByPath(fullPath);
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

            VariableGroup desGroup = FindGroupByPath(fullPath);
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
                    //如果变量的位置与目标位置相同，无需替换，直接返回
                    if (source.ParentGroup.FullPath == fullPath)
                    {
                        return source.Name;
                    }
                    RemoveVariable(source.Name, desGroup.FullPath);
                }
                if (isCopy)
                {
                    VariableBase var = source.CreatVariable(desGroup);
                    var.EditVariable(source);

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
            SaveAllChanges();
            return source.Name;
        }

        #endregion

        #region 保存改变

        /// <summary>
        /// 保存变量
        /// </summary>
        public void SaveAllChanges()
        {
            VariableDbContext.SaveAllChanges();
        }

        /// <summary>
        /// 退出时保存变量当前以便程序复位
        /// </summary>
        public void ExitWithSaving()
        {
            VariableDbContext.ExitWithSaving();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 复制变量组
        /// </summary>
        /// <param name="sourse">源</param>
        /// <param name="destination">目标</param>
        /// <param name="pasteMode">粘贴模式，0：默认模式，重复则返回，1：如果重复则替换，2：如果重复则两个变量都保留，3：如果重复则放弃</param>
        private void CopyGroup(VariableGroup sourse, VariableGroup destination, uint pasteMode)
        {
            if (sourse == null)
            {
                return;
            }
            string groupName = sourse.Name;
            if ((pasteMode == 2) && IsExistName(sourse.Name, destination))//同时保留两个
            {
                groupName = GetDefaultName(destination, sourse.Name);
            }
            AddGroup(groupName, destination.FullPath);

            VariableGroup var =
                FindGroupByPath(destination.FullPath == null ? groupName : destination.FullPath + "." + groupName);

            if (var == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_CopyGroup_destinationIsNotExist);
            }

            foreach (var childVariable in sourse.ChildVariables)
            {
                var varVariable = new AnalogVariable(var);
                varVariable.EditVariable(childVariable);
                AddVariable(varVariable);
            }

            foreach (var variableGroup in sourse.ChildGroups)
            {
                CopyGroup(variableGroup, var, pasteMode);
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

            if (String.IsNullOrEmpty(defaultName))
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
