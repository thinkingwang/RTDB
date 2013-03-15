using System;
using System.Collections.Generic;
using System.Linq;
using SCADA.RTDB.Common.Base;
using SCADA.RTDB.Common.Design;
using SCADA.RTDB.Core.Variable;
using SCADA.RTDB.EntityFramework.DbConfig;
using SCADA.RTDB.EntityFramework.ExtendMethod;
using SCADA.RTDB.EntityFramework.Repository.Base;

namespace SCADA.RTDB.EntityFramework.Repository.Design
{
    /// <summary>
    /// 变量设计仓储类
    /// </summary>
    public class VariableDesignRepository :  IVariableDesignRepository
    {
        private readonly IVariableRepository VariableRepository;

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config">仓库配置信息</param>
        public VariableDesignRepository(RepositoryConfig config)
        {
            VariableRepository = new VariableRepository(config);
        }

        #endregion

        #region 变量仓储方法重载

        /// <summary>
        /// 加载参数
        /// </summary>
        public void Load()
        {
            VariableRepository.Load();
        }

        /// <summary>
        /// 根据变量Id提供的路径信息，遍历树查找变量
        /// </summary>
        /// <param name="absolutePath">变量全路径</param>
        /// <returns>返回变量对象，未找到返回null</returns>
        public VariableBase FindVariableByPath(string absolutePath)
        {
            return VariableRepository.FindVariableByPath(absolutePath);
        }

        /// <summary>
        /// 查询组路径下面所有变量
        /// </summary>
        /// <param name="absolutePath">组路径</param>
        /// <returns>所有变量列表</returns>
        public IEnumerable<VariableBase> FindVariables(string absolutePath)
        {
            return VariableRepository.FindVariables(absolutePath);
        }

        /// <summary>
        /// 根据组Id提供的路径信息，遍历树查找组节点
        /// </summary>
        /// <param name="absolutePath">组全路径，等于null或为空字符返回根组</param>
        /// <returns>返回组对象，未找到返回null</returns>
        public VariableGroup FindGroupByPath(string absolutePath)
        {
            return VariableRepository.FindGroupByPath(absolutePath);
        }

        /// <summary>
        /// 查询组路径下面所有子组
        /// </summary>
        /// <param name="absolutePath">组路径</param>
        /// <returns>所有子组列表</returns>
        public IEnumerable<VariableGroup> FindGroups(string absolutePath)
        {
            return VariableRepository.FindGroups(absolutePath);
        }

        /// <summary>
        /// 退出时保存变量当前以便程序复位
        /// </summary>
        public void ExitWithSaving()
        {
            VariableRepository.ExitWithSaving();
        }

        #endregion

        #region 组公共方法

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

            VariableGroup parentVariableGroup = VariableRepository.FindGroupByPath(absolutePath);
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
            RealTimeRepositoryBase.RtDbContext.VariableGroupSet.Add(newGroup);
            RealTimeRepositoryBase.RtDbContext.SaveAllChanges();
            return newGroup;
        }

        /// <summary>
        /// 删除指定组
        /// </summary>
        /// <param name="absolutePath">要移除的组全路径</param>
        public virtual void RemoveGroup(string absolutePath)
        {
            VariableGroup currentVariableGroup = VariableRepository.FindGroupByPath(absolutePath);
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
            RealTimeRepositoryBase.RtDbContext.VariableGroupSet.Remove(currentVariableGroup);
            RealTimeRepositoryBase.RtDbContext.SaveAllChanges();
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
            VariableGroup currentVariableGroup = VariableRepository.FindGroupByPath(absolutePath);
            if (currentVariableGroup == null)
            {
                throw new ArgumentNullException(Resource1.UnitofWork_AddGroup_currentVariableGroup);
            }
            if (IsExistName(name, currentVariableGroup.Parent))
            {
                throw new Exception(Resource1.CVariableGroup_AddGroup_GroupeNameIsExist);
            }
            currentVariableGroup.Name = name;
            RealTimeRepositoryBase.RtDbContext.SaveAllChanges();
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
            VariableGroup source = VariableRepository.FindGroupByPath(groupAbsolutePath);
            if (source == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_PasteVariable_sourceVariable);
            }
            //根组不能复制
            if (source.Parent == null)
            {
                throw new Exception("需要粘贴的组为根组，不能粘贴");
            }
            
            VariableGroup desGroup = VariableRepository.FindGroupByPath(desAbsolutePath);
            if (desGroup == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_PasteVariable_desGroup);
            }

            if (IsExistName(source.Name, desGroup))
            {
                 return false; 
            }

            source.Parent.ChildGroups.Remove(source);
            source.Parent = desGroup;
            desGroup.ChildGroups.Add(source);

            RealTimeRepositoryBase.RtDbContext.SaveAllChanges();
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

            VariableGroup desGroup = VariableRepository.FindGroupByPath(absolutePath);
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
                    if (desGroup.AbsolutePath == "")
                    {
                        RemoveGroup(desGroup.AbsolutePath + "." + source.Name);
                    }
                    else
                    {
                        RemoveGroup(source.Name);
                    }
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
            if (variable.CreateTime == new DateTime())
            {
                variable.CreateTime = DateTime.Now;
            }
            variable.ParentGroup.ChildVariables.Add(variable);
            switch (variable.ValueType)
            {
                case VarValuetype.VarBool:
                    RealTimeRepositoryBase.RtDbContext.DigitalSet.Add((DigitalVariable)variable);
                    break;
                case VarValuetype.VarDouble:
                    RealTimeRepositoryBase.RtDbContext.AnalogSet.Add((AnalogVariable)variable);
                    break;
                case VarValuetype.VarString:
                    RealTimeRepositoryBase.RtDbContext.TextSet.Add((TextVariable)variable);
                    break;
            }
            RealTimeRepositoryBase.RtDbContext.SaveAllChanges();
            return variable;
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
            ObjectCopier.CopyProperties(variable,newVariable);
            RealTimeRepositoryBase.RtDbContext.SaveAllChanges();
            return variable;
        }

        /// <summary>
        /// 根据变量的属性名称修改变量的该项属性
        /// </summary>
        /// <param name="absolutePath">变量绝对路径名</param>
        /// <param name="propertyName">要被修改的变量的属性名</param>
        /// <param name="value">修改的值</param>
        public virtual VariableBase EditVariable(string absolutePath, string propertyName, object value)
        {
            VariableBase variable = VariableRepository.FindVariableByPath(absolutePath);
            if (variable == null)
            {
                throw new Exception("需要修改的变量不存在");
            }
            if (propertyName == "Name")
            {
                if (IsExistName(propertyName, variable.ParentGroup))
                {
                    throw new Exception("变量名称已存在，不能修改");
                }
            }
            ObjectCopier.CopyProperty(variable, propertyName, value);
            RealTimeRepositoryBase.RtDbContext.SaveAllChanges();
            return variable;
        }
        
        /// <summary>
        /// 删除指定变量
        /// </summary>
        /// <param name="name">变量名称</param>
        /// <param name="absolutePath">移除变量所属组全路径</param>
        public virtual void RemoveVariable(string name, string absolutePath)
        {
            VariableGroup currentVariableGroup = VariableRepository.FindGroupByPath(absolutePath);
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
            switch (curVariable.ValueType)
            {
                case VarValuetype.VarBool:
                    RealTimeRepositoryBase.RtDbContext.DigitalSet.Remove((DigitalVariable)curVariable);
                    break;
                case VarValuetype.VarDouble:
                    RealTimeRepositoryBase.RtDbContext.AnalogSet.Remove((AnalogVariable)curVariable);
                    break;
                case VarValuetype.VarString:
                    RealTimeRepositoryBase.RtDbContext.TextSet.Remove((TextVariable)curVariable);
                    break;
            }
            RealTimeRepositoryBase.RtDbContext.SaveAllChanges();
        }

        /// <summary>
        /// 删除当前组的所有变量
        /// </summary>
        /// <param name="absolutePath">要清空的组全路径</param>
        public virtual void ClearVariable(string absolutePath)
        {
            VariableGroup currentVariableGroup = VariableRepository.FindGroupByPath(absolutePath);
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
        /// 移动当前变量
        /// </summary>
        /// <param name="variableAbsolutePath">当前变量路径</param>
        /// <param name="desAbsolutePath">目标路径</param>
        /// <returns>移动成功返回true</returns>
        public virtual bool MoveVariable(string variableAbsolutePath, string desAbsolutePath)
        {
            VariableBase source = VariableRepository.FindVariableByPath(variableAbsolutePath);
            if (source == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_PasteVariable_sourceVariable);
            }
            VariableGroup desGroup = VariableRepository.FindGroupByPath(desAbsolutePath);
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

            VariableGroup desGroup = VariableRepository.FindGroupByPath(absolutePath);
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
                VariableRepository.FindGroupByPath(destination.AbsolutePath == null ? groupName : destination.AbsolutePath + "." + groupName);

            if (var == null)
            {
                throw new ArgumentNullException(Resource1.VariableRepository_CopyGroup_destinationIsNotExist);
            }

            foreach (var childVariable in sourse.ChildVariables)
            {
                var varVariable = new AnalogVariable(var);
                ObjectCopier.CopyProperties(varVariable, childVariable);
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

        #endregion
    }
}
