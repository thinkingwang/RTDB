using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SCADA.RTDB.StorageModel;
using SCADA.RTDB.VariableModel;

namespace SCADA.RTDB.EntityFramework
{
    public class EfVariableRepository : VariableRepositoryBase
    {
        private readonly Dictionary<string, VariableGroupStorage> _groupStorages = new Dictionary<string, VariableGroupStorage>();
        private readonly Dictionary<string, VariableBaseStorage> _variableBaseStorages = new Dictionary<string, VariableBaseStorage>();

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="variableRepositoryConfig">变量仓储配置信息类</param>
        public EfVariableRepository(VariableRepositoryConfig variableRepositoryConfig):base(variableRepositoryConfig)
        {
            
        }

        #endregion

        #region 组公共方法
        /// <summary>
        /// 向当前组添加子组
        /// </summary>
        /// <param name="name">子组名称</param>
        /// <param name="absolutePath">要添加的组全路径</param>
        public override VariableGroup AddGroup(string name, string absolutePath)
        {
            VariableGroup variableGroup = base.AddGroup(name, absolutePath);
            var variableGroupStoage = new VariableGroupStorage();
            variableGroupStoage.CopyProperty(variableGroup, _groupStorages[absolutePath].VariableGroupStorageId);
            VariableDbContext.VariableGroupSet.Add(variableGroupStoage);
            _groupStorages.Add(variableGroup.AbsolutePath, variableGroupStoage);
            VariableDbContext.SaveAllChanges();
            return variableGroup;
        }

        /// <summary>
        /// 删除指定组
        /// </summary>
        /// <param name="absolutePath">要移除的组全路径</param>
        public override void RemoveGroup(string absolutePath)
        {
            base.RemoveGroup(absolutePath);
            VariableDbContext.VariableGroupSet.Remove(_groupStorages[absolutePath]);
            _groupStorages.Remove(absolutePath);
            VariableDbContext.SaveAllChanges();
        }
        
        /// <summary>
        /// 修改当前变量组名称
        /// </summary>
        /// <param name="name">修改后的组名</param>
        /// <param name="absolutePath">要重命名的组全路径</param>
        public override VariableGroup RenameGroup(string name, string absolutePath)
        {
            VariableGroup variableGroup = base.RenameGroup(name, absolutePath);
            _groupStorages[absolutePath].Name = name;
            _groupStorages.Add(variableGroup.AbsolutePath, _groupStorages[absolutePath]);
            _groupStorages.Remove(absolutePath);
            VariableDbContext.SaveAllChanges();
            return variableGroup;
        }

        /// <summary>
        /// 移动当前组
        /// </summary>
        /// <param name="groupAbsolutePath">当前组路径</param>
        /// <param name="desAbsolutePath">目标路径</param>
        /// <returns>移动成功返回true</returns>
        public override bool MoveGroup(string groupAbsolutePath, string desAbsolutePath)
        {
            if (!base.MoveGroup(groupAbsolutePath, desAbsolutePath))
            {
                return false;
            }
            VariableGroup source = FindGroupByPath(groupAbsolutePath);
            VariableGroup desGroup = FindGroupByPath(desAbsolutePath);
            VariableGroupStorage variableGroupStorage = _groupStorages[groupAbsolutePath];

            source.Parent.ChildGroups.Remove(source);
            source.Parent = desGroup;
            desGroup.ChildGroups.Add(source);

            _groupStorages.Remove(groupAbsolutePath);
            _groupStorages.Add(source.AbsolutePath, variableGroupStorage);
            _groupStorages[source.AbsolutePath].ParentId = _groupStorages[desAbsolutePath].VariableGroupStorageId;
            VariableDbContext.SaveAllChanges();
            return true;
        }
        #endregion

        #region 组变量公共方法

        /// <summary>
        /// 向当前组添加变量
        /// </summary>
        /// <param name="variable">需要添加的变量</param>
        public override VariableBase AddVariable(VariableBase variable)
        {
            VariableBase variableBase = base.AddVariable(variable);
            VariableBaseStorage variableBaseStorage = null;
            switch (variableBase.ValueType)
            {
                    case VarValuetype.VarBool:
                    variableBaseStorage = new DigitalVariableStorage(true);
                    variableBaseStorage.PullCopyProperty(variableBase,
                                                         _groupStorages[variableBase.ParentGroup.AbsolutePath]
                                                             .VariableGroupStorageId);
                    VariableDbContext.DigitalSet.Add((DigitalVariableStorage)variableBaseStorage);
                    
                    break;
                    case VarValuetype.VarDouble:
                    variableBaseStorage = new AnalogVariableStorage(true);
                    variableBaseStorage.PullCopyProperty(variableBase,
                                                         _groupStorages[variableBase.ParentGroup.AbsolutePath]
                                                             .VariableGroupStorageId);
                    VariableDbContext.AnalogSet.Add((AnalogVariableStorage)variableBaseStorage);
                    break;
                    case VarValuetype.VarString:
                    variableBaseStorage = new TextVariableStorage(true);
                    variableBaseStorage.PullCopyProperty(variableBase,
                                                         _groupStorages[variableBase.ParentGroup.AbsolutePath]
                                                             .VariableGroupStorageId);
                    VariableDbContext.TextSet.Add((TextVariableStorage)variableBaseStorage);
                    break;
            }
            _variableBaseStorages.Add(variableBase.AbsolutePath, variableBaseStorage);
            VariableDbContext.SaveAllChanges();
            return variableBase;
        }

        /// <summary>
        /// 修改变量属性
        /// </summary>
        /// <param name="variable">修改前变量</param>
        /// <param name="variableStrings">修改后变量属性字符串序列</param>
        /// <returns>修改成功返回true，修改失败返回false</returns>
        public override VariableBase EditVariable(VariableBase variable, List<string> variableStrings)
        {
            VariableBaseStorage variableBaseStorage = _variableBaseStorages[variable.AbsolutePath];
            VariableBase variableBase = base.EditVariable(variable, variableStrings);
            variableBaseStorage.PullCopyProperty(variableBase,
                                                        _groupStorages[variableBase.ParentGroup.AbsolutePath]
                                                            .VariableGroupStorageId);
            _variableBaseStorages.Remove(variable.AbsolutePath);
            _variableBaseStorages.Add(variableBase.AbsolutePath, variableBaseStorage);
            VariableDbContext.SaveAllChanges();
            return variableBase;
        }

        /// <summary>
        /// 修改变量属性
        /// </summary>
        /// <param name="variable">修改前变量</param>
        /// <param name="newVariable">修改后变量</param>
        /// <returns>修改成功返回true，修改失败返回false</returns>
        public override VariableBase EditVariable(VariableBase variable, VariableBase newVariable)
        {
            VariableBaseStorage variableBaseStorage = _variableBaseStorages[variable.AbsolutePath];
            VariableBase variableBase = base.EditVariable(variable, newVariable);
            variableBaseStorage.PullCopyProperty(variableBase,
                                                        _groupStorages[variableBase.ParentGroup.AbsolutePath]
                                                            .VariableGroupStorageId);
            _variableBaseStorages.Remove(variable.AbsolutePath);
            _variableBaseStorages.Add(variableBase.AbsolutePath, variableBaseStorage);
            VariableDbContext.SaveAllChanges();
            return variableBase;
        }


        /// <summary>
        /// 删除指定变量
        /// </summary>
        /// <param name="name">变量名称</param>
        /// <param name="absolutePath">移除变量所属组全路径</param>
        public override void RemoveVariable(string name, string absolutePath)
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
            VariableBaseStorage variableBaseStorage = _variableBaseStorages[curVariable.AbsolutePath];
            switch (variableBaseStorage.ValueType)
            {
                case (int)VarValuetype.VarBool:
                    VariableDbContext.DigitalSet.Remove((DigitalVariableStorage)variableBaseStorage);

                    break;
                case (int)VarValuetype.VarDouble:
                    VariableDbContext.AnalogSet.Remove((AnalogVariableStorage)variableBaseStorage);
                    break;
                case (int)VarValuetype.VarString:
                    VariableDbContext.TextSet.Remove((TextVariableStorage)variableBaseStorage);
                    break;
            }
            _variableBaseStorages.Remove(curVariable.AbsolutePath);
            VariableDbContext.SaveAllChanges();
        }
        
        #endregion

        #region 保存改变

        /// <summary>
        /// 退出时保存变量当前以便程序复位
        /// </summary>
        public override void ExitWithSaving()
        {
            foreach (var variableBaseStorage in _variableBaseStorages)
            {
                if (!variableBaseStorage.Value.IsInitValueSaved)
                {
                    continue;
                }
                switch (variableBaseStorage.Value.ValueType)
                {
                    case (int) VarValuetype.VarBool:
                        ((DigitalVariableStorage) variableBaseStorage.Value).InitValue =
                            ((DigitalVariable) FindVariableByPath(variableBaseStorage.Key)).Value;
                        break;
                    case (int) VarValuetype.VarDouble:
                        ((AnalogVariableStorage)variableBaseStorage.Value).InitValue =
                            ((AnalogVariable)FindVariableByPath(variableBaseStorage.Key)).Value;
                        break;
                    case (int) VarValuetype.VarString:
                        ((TextVariableStorage)variableBaseStorage.Value).InitValue =
                            ((TextVariable)FindVariableByPath(variableBaseStorage.Key)).Value;
                        break;
                }
            }
            VariableDbContext.SaveAllChanges();
        }

        
        /// <summary>
        /// 加载参数
        /// </summary>
        public override void Load()
        {
            //遍历数据库变量组数据到set集合
            VariableDbContext.VariableGroupSet.Load();
            VariableDbContext.DigitalSet.Load();
            VariableDbContext.AnalogSet.Load();
            VariableDbContext.TextSet.Load();

            //同步组
            VariableGroupStorage rootGroup =
                VariableDbContext.VariableGroupSet.FirstOrDefault(root => root.ParentId == null);

            if (rootGroup != null)
            {
                VariableGroup.RootGroup.Parent = null;
                VariableGroup.RootGroup.Name = rootGroup.Name;
                _groupStorages.Add(VariableGroup.RootGroup.AbsolutePath, rootGroup);
            }
            LoadVariable(VariableGroup.RootGroup, rootGroup);
        }

        #endregion


        /// <summary>
        /// 数据库加载组信息以及变量信息
        /// </summary>
        /// <param name="variablegroup">变量组</param>
        /// <param name="parentGroupStorage">变量存储模型父祖</param>
        private void LoadVariable(VariableGroup variablegroup, VariableGroupStorage parentGroupStorage)
        {
            #region 加载变量

            var analogVariables = VariableDbContext.AnalogSet.Local.ToList()
                                                   .FindAll(m => m.ParentId == parentGroupStorage.VariableGroupStorageId);
            //同步变量集合
            foreach (var variableStorage in analogVariables)
            {
                var variable = new AnalogVariable {ParentGroup = variablegroup};
                variableStorage.PushCopyProperty(variable);
                variablegroup.ChildVariables.Add(variable);
                _variableBaseStorages.Add(variable.AbsolutePath, variableStorage);
            }
            var digitalVariables = VariableDbContext.DigitalSet.Local.ToList()
                                                   .FindAll(m => m.ParentId == parentGroupStorage.VariableGroupStorageId);
            foreach (var variableStorage in digitalVariables)
            {
                var variable = new DigitalVariable {ParentGroup = variablegroup};
                variableStorage.PushCopyProperty(variable);
                variablegroup.ChildVariables.Add(variable);
                _variableBaseStorages.Add(variable.AbsolutePath, variableStorage);
            }
            var textVariables = VariableDbContext.TextSet.Local.ToList()
                                                   .FindAll(m => m.ParentId == parentGroupStorage.VariableGroupStorageId);
            foreach (var variableStorage in textVariables)
            {
                var variable = new TextVariable {ParentGroup = variablegroup};
                variableStorage.PushCopyProperty(variable);
                variablegroup.ChildVariables.Add(variable);
                _variableBaseStorages.Add(variable.AbsolutePath, variableStorage);
            }

            //对变量进行排序
            variablegroup.ChildVariables.Sort(
                (x, y) =>
                _variableBaseStorages[x.AbsolutePath].OrderId.CompareTo(_variableBaseStorages[y.AbsolutePath].OrderId));

            #endregion

            var groupStorages = VariableDbContext.VariableGroupSet.Local.ToList()
                                                 .FindAll(m => m.ParentId == parentGroupStorage.VariableGroupStorageId);
            //同步子组
            foreach (var groupStorage in groupStorages)
            {
                var element = new VariableGroup(groupStorage.Name, variablegroup);
                variablegroup.ChildGroups.Add(element);
                _groupStorages.Add(element.AbsolutePath, groupStorage);
                LoadVariable(element, groupStorage);
            }
        }
        
    }
}
