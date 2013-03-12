using System;
using System.Collections.Generic;
using System.Linq;
using SCADA.RTDB.Common;
using SCADA.RTDB.Common.Design;
using SCADA.RTDB.Core.Variable;
using SCADA.RTDB.EntityFramework.Context;
using SCADA.RTDB.EntityFramework.DbConfig;
using SCADA.RTDB.EntityFramework.ExtendMethod;

namespace SCADA.RTDB.EntityFramework.Repository
{
    /// <summary>
    /// 变量仓储基类
    /// </summary>
    public class VariableDesignRepository : RealTimeRepositoryBase, IVariableDesignRepository
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryConfig">仓储配置信息类</param>
        protected VariableDesignRepository(RepositoryConfig repositoryConfig)
            : base(new RealTimeDbContext(repositoryConfig ?? new RepositoryConfig()))
        {
        }

        #endregion

        #region 组公共方法

        /// <summary>
        /// 根据组Id提供的路径信息，遍历树查找组节点
        /// </summary>
        /// <param name="absolutePath">组全路径，等于null或为空字符返回根组</param>
        /// <returns>返回组对象，未找到返回null</returns>
        public virtual VariableGroup FindGroupByPath(string absolutePath)
        {
            //等于null或为空字符返回根组
            if (String.IsNullOrEmpty(absolutePath))
            {
                return VariableGroup.RootGroup;
            }

            return findRecursion(VariableGroup.RootGroup, absolutePath);

        }

        /// <summary>
        /// 查询组路径下面所有子组
        /// </summary>
        /// <param name="absolutePath">组路径</param>
        /// <returns>所有子组列表</returns>
        public virtual IEnumerable<VariableGroup> FindGroups(string absolutePath)
        {
            VariableGroup variableGroup = FindGroupByPath(absolutePath);
            return variableGroup == null ? null : variableGroup.ChildGroups;
        }

        /// <summary>
        /// 向当前组添加子组
        /// </summary>
        /// <param name="name">子组名称</param>
        /// <param name="absolutePath">要添加的组全路径</param>
        public virtual VariableGroup AddGroup(string name, string absolutePath)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupNameIsNull);
            }

            VariableGroup parentVariableGroup = FindGroupByPath(absolutePath);
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
            return newGroup;
        }

        /// <summary>
        /// 删除指定组
        /// </summary>
        /// <param name="absolutePath">要移除的组全路径</param>
        public virtual void RemoveGroup(string absolutePath)
        {
            VariableGroup currentVariableGroup = FindGroupByPath(absolutePath);
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            //删除该组下面的子组
            while (currentVariableGroup.ChildGroups.Count > 0)
            {
                RemoveGroup(currentVariableGroup.ChildGroups[0].AbsolutePath);
            }

            //删除该组下的变量
            ClearVariable(absolutePath);

            //删除该组
            if (currentVariableGroup.Parent == null)
            {
                //根组不允许
                throw new Exception(Resource1.VariableGroup_RemoveGroup_DeleteGroup_Is_RootGroup);
            }
            currentVariableGroup.Parent.ChildGroups.Remove(currentVariableGroup);
        }

        /// <summary>
        /// 修改当前变量组名称
        /// </summary>
        /// <param name="name">修改后的组名</param>
        /// <param name="absolutePath">要重命名的组全路径</param>
        public virtual VariableGroup RenameGroup(string name, string absolutePath)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new Exception(Resource1.VariableGroup_ReGroupName_groupName_Is_Null);
            }
            VariableGroup currentVariableGroup = FindGroupByPath(absolutePath);
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            if (IsExistName(name, currentVariableGroup.Parent))
            {
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupeNameIsExist);
            }
            currentVariableGroup.Name = name;
            return currentVariableGroup;
        }

        /// <summary>
        /// 移动当前组
        /// </summary>
        /// <param name="groupAbsolutePath">当前组路径</param>
        /// <param name="desAbsolutePath">目标路径</param>
        /// <returns>移动成功返回true</returns>
        public virtual bool MoveGroup(string groupAbsolutePath, string desAbsolutePath)
        {
            //目标组是源文件的子文件组，不允许粘贴
            if ((groupAbsolutePath == null) || (desAbsolutePath != null && desAbsolutePath.Contains(groupAbsolutePath)))
            {
                throw new Exception(Resource1.VariableRepository_PasteGroup_SourceGroupContainDesGroup);
            }
            VariableGroup source = FindGroupByPath(groupAbsolutePath);
            if (source == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_PasteVariable_sourceVariable);
            }
            //根组不能复制
            if (source.Parent == null)
            {
                throw new Exception("需要粘贴的组为根组，不能粘贴");
            }
            
            VariableGroup desGroup = FindGroupByPath(desAbsolutePath);
            if (desGroup == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_PasteVariable_desGroup);
            }

            if (IsExistName(source.Name, desGroup))
            {
                 return false; 
            }

            return true;
        }

        /// <summary>
        /// 粘贴变量组
        /// </summary>
        /// <param name="source">需要粘贴的变量组</param>
        /// <param name="absolutePath">粘贴变量的目标组,null为根组</param>
        /// <param name="isCopy">是否为复制，true为复制，false为剪切</param>
        /// <param name="pasteMode">粘贴模式，0：默认模式，重复则返回，1：如果重复则替换，2：如果重复则两个变量都保留，3：如果重复则放弃</param>
        /// <returns>如果默认模式下且有相同变量名称存在返回变量新名称，否则返回粘贴变量名称</returns>
        public virtual string PasteGroup(VariableGroup source, string absolutePath, bool isCopy,
                                  uint pasteMode = 0)
        {
            if (source == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_PasteVariable_sourceVariable);
            }

            //根组不能复制
            if (source.Parent == null)
            {
                throw new Exception("需要粘贴的组为根组，不能粘贴");
            }
           
            //目标组是源文件的子文件组，不允许粘贴
            if ((source.AbsolutePath == null) || (absolutePath != null && absolutePath.Contains(source.AbsolutePath)))
            {
                throw new Exception(Resource1.VariableRepository_PasteGroup_SourceGroupContainDesGroup);
            }

            VariableGroup desGroup = FindGroupByPath(absolutePath);
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
                    if (desGroup.AbsolutePath == source.Parent.AbsolutePath) //如果源与目标位置相同，无需替换
                    {
                        return source.Name;
                    }
                    RemoveGroup(desGroup.AbsolutePath + "." + source.Name);
                }

                if (isCopy)
                {
                    CopyGroup(source, desGroup, pasteMode);
                }
                else
                {
                    if (pasteMode == 2) //同时保留两个
                    {
                        source.Name = GetDefaultName(desGroup, source.Name);
                    }
                    MoveGroup(source.AbsolutePath, desGroup.AbsolutePath);

                }
            }
            return source.Name;
        }

        #endregion

        #region 组变量公共方法

        /// <summary>
        /// 向当前组添加变量
        /// </summary>
        /// <param name="variable">需要添加的变量</param>
        public virtual VariableBase AddVariable(VariableBase variable)
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
            variable.ParentGroup.ChildVariables.Add(variable);
            return variable;
        }

        /// <summary>
        /// 修改变量属性
        /// </summary>
        /// <param name="variable">修改前变量</param>
        /// <param name="variableStrings">修改后变量属性字符串序列</param>
        /// <returns>修改后的变量</returns>
        public virtual VariableBase EditVariable(VariableBase variable, List<string> variableStrings)
        {
            if (variable.Name != variableStrings[0] && IsExistName(variableStrings[0], variable.ParentGroup))
            {
                throw new Exception(Resource1.VariableUnitOfWork_EditVariable_AvarialeNameExist);
            }
            return variable.EditVariable(variableStrings) ? variable : null;

        }

        /// <summary>
        /// 修改变量属性
        /// </summary>
        /// <param name="variable">修改前变量</param>
        /// <param name="newVariable">修改后变量</param>
        /// <returns>修改后的变量,修改失败返回null</returns>
        public virtual VariableBase EditVariable(VariableBase variable, VariableBase newVariable)
        {
            if (variable.Name != newVariable.Name && IsExistName(newVariable.Name, variable.ParentGroup))
            {
                throw new Exception(Resource1.VariableUnitOfWork_EditVariable_AvarialeNameExist);
            }
            return variable.EditVariable(newVariable) ? variable : null;
        }

        /// <summary>
        /// 设置变量值
        /// </summary>
        /// <param name="variable">变量</param>
        /// <param name="value">变量值</param>
        /// <returns>设置成功返回true，失败返回false</returns>
        public virtual bool SetVariableValue(VariableBase variable, object value)
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
        /// <param name="absolutePath">移除变量所属组全路径</param>
        public virtual void RemoveVariable(string name, string absolutePath)
        {
            VariableGroup currentVariableGroup = FindGroupByPath(absolutePath);
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            var curVariable = currentVariableGroup.ChildVariables.FirstOrDefault(p => p.Name == name);
            if (curVariable == null)
            {
                return;
            }
            curVariable.ParentGroup.ChildVariables.Remove(curVariable);
        }

        /// <summary>
        /// 删除当前组的所有变量
        /// </summary>
        /// <param name="absolutePath">要清空的组全路径</param>
        public virtual void ClearVariable(string absolutePath)
        {
            VariableGroup currentVariableGroup = FindGroupByPath(absolutePath);
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            //删除该组下的变量
            while (currentVariableGroup.ChildVariables.Count > 0)
            {
                RemoveVariable(currentVariableGroup.ChildVariables[0].Name, absolutePath);
            }
        }

        /// <summary>
        /// 根据变量Id提供的路径信息，遍历树查找变量
        /// </summary>
        /// <param name="absolutePath">变量全路径</param>
        /// <returns>返回变量对象，未找到返回null</returns>
        public virtual VariableBase FindVariableByPath(string absolutePath)
        {
            if (String.IsNullOrEmpty(absolutePath))
            {
                return null;
            }

            if (!absolutePath.Contains('.'))
            {
                return VariableGroup.RootGroup.ChildVariables.Find(m => m.AbsolutePath == absolutePath);
            }
            var variableGroup = findRecursion(VariableGroup.RootGroup,
                                                        absolutePath.Substring(0, absolutePath.LastIndexOf('.')));

            return variableGroup == null
                       ? null
                       : variableGroup.ChildVariables.Find(m => m.AbsolutePath == absolutePath);
        }
        
        /// <summary>
        /// 查询组路径下面所有变量
        /// </summary>
        /// <param name="absolutePath">组路径</param>
        /// <returns>所有变量列表</returns>
        public virtual IEnumerable<VariableBase> FindVariables(string absolutePath)
        {
            VariableGroup variableGroup = FindGroupByPath(absolutePath);
            if (variableGroup == null)
            {
                return null;
            }
            return variableGroup.ChildVariables;
        }

        /// <summary>
        /// 移动当前变量
        /// </summary>
        /// <param name="variableAbsolutePath">当前变量路径</param>
        /// <param name="desAbsolutePath">目标路径</param>
        /// <returns>移动成功返回true</returns>
        public virtual bool MoveVariable(string variableAbsolutePath, string desAbsolutePath)
        {
            VariableBase source = FindVariableByPath(variableAbsolutePath);
            if (source == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_PasteVariable_sourceVariable);
            }
            VariableGroup desGroup = FindGroupByPath(desAbsolutePath);
            if (desGroup == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_PasteVariable_desGroup);
            }
            if (IsExistName(source.Name, desGroup))
            {
                return false;
            }
            RemoveVariable(source.Name, source.ParentGroup.AbsolutePath);
            source.ParentGroup = desGroup;
            AddVariable(source);
            return true;
        }

        /// <summary>
        /// 粘贴变量
        /// </summary>
        /// <param name="source">需要粘贴的变量</param>
        /// <param name="absolutePath">粘贴变量的目标组</param>
        /// <param name="isCopy">是否为复制，true为复制，false为剪切</param>
        /// <param name="pasteMode">粘贴模式，0：默认模式，重复则返回，1：如果重复则替换，2：如果重复则两个变量都保留，3：如果重复则放弃</param>
        /// <returns>如果默认模式下且有相同变量名称存在返回变量新名称，否则返回粘贴变量名称</returns>
        public virtual string PasteVariable(VariableBase source, string absolutePath, bool isCopy,
                                    uint pasteMode = 0)
        {
            if (source == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_PasteVariable_sourceVariable);
            }

            VariableGroup desGroup = FindGroupByPath(absolutePath);
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
                    if (source.ParentGroup.AbsolutePath == absolutePath)
                    {
                        return source.Name;
                    }
                    RemoveVariable(source.Name, desGroup.AbsolutePath);
                }
                if (isCopy)
                {
                    VariableBase var = ObjectCopier.Clone(source);
                    var.ParentGroup = desGroup;

                    if (pasteMode == 2) //同时保留两个
                    {
                        var.Name = GetDefaultName(desGroup, source.Name);
                    }
                    AddVariable(var);
                }
                else
                {
                    if (pasteMode == 2) //同时保留两个
                    {
                        source.Name = GetDefaultName(desGroup, source.Name);
                    }
                    MoveVariable(source.AbsolutePath, desGroup.AbsolutePath);
                }
            }
            return source.Name;
        }

        #endregion

        #region 保存改变
        
        /// <summary>
        /// 退出时保存变量当前以便程序复位
        /// </summary>
        public virtual void ExitWithSaving()
        {
            RtDbContext.SaveAllChanges();
        }

        /// <summary>
        /// 加载参数
        /// </summary>
        public virtual void Load()
        {
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
            AddGroup(groupName, destination.AbsolutePath);

            VariableGroup var =
                FindGroupByPath(destination.AbsolutePath == null ? groupName : destination.AbsolutePath + "." + groupName);

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
        /// <param name="absolutePath"></param>
        /// <returns></returns>
        private VariableGroup findRecursion(VariableGroup group, string absolutePath)
        {
            if (group == null)
            {
                return null;
            }
            if (!absolutePath.Contains('.'))
            {
                return group.ChildGroups.FirstOrDefault(m => m.Name == absolutePath);
            }
            return findRecursion(group.ChildGroups.FirstOrDefault(m => m.Name == absolutePath.Split('.')[0]),
                     absolutePath.Substring(absolutePath.IndexOf('.') + 1));
        }

        #endregion
    }
}
