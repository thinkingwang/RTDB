using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SCADA.RTDB.Common;
using SCADA.RTDB.Common.Base;
using SCADA.RTDB.Core.Variable;
using SCADA.RTDB.EntityFramework.Context;
using SCADA.RTDB.EntityFramework.DbConfig;

namespace SCADA.RTDB.EntityFramework.Repository.Base
{
    /// <summary>
    /// 变量仓储类，负责加载和查询变量
    /// </summary>
    public class VariableRepository :  IVariableRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repositoryConfig"></param>
        public VariableRepository(RepositoryConfig repositoryConfig)
        {
            RealTimeRepositoryBase.Initialize(RealTimeRepositoryBase.RtDbContext ??
                                              new RealTimeDbContext(repositoryConfig ?? new RepositoryConfig()));
        }

        #region  加载参数
        
        /// <summary>
        /// 加载参数
        /// </summary>
        public void Load()
        {
            //遍历数据库变量组数据到set集合
            RealTimeRepositoryBase.RtDbContext.VariableGroupSet.Load();
            RealTimeRepositoryBase.RtDbContext.DigitalSet.Load();
            RealTimeRepositoryBase.RtDbContext.AnalogSet.Load();
            RealTimeRepositoryBase.RtDbContext.TextSet.Load();

            //同步组
            VariableGroup.RootGroup =
                RealTimeRepositoryBase.RtDbContext.VariableGroupSet.FirstOrDefault(root => root.Parent == null);

            var variables = new List<VariableBase>();
            variables.AddRange(RealTimeRepositoryBase.RtDbContext.DigitalSet.Local.ToArray());
            variables.AddRange(RealTimeRepositoryBase.RtDbContext.AnalogSet.Local.ToArray());
            variables.AddRange(RealTimeRepositoryBase.RtDbContext.TextSet.Local.ToArray());
            variables.Sort((x, y) => x.CreateTime.CompareTo(y.CreateTime));
            //关联组
            foreach (var variable in variables)
            {
                variable.ParentGroup = FindGroupByPath(variable.ParentGroupPath);
                variable.ParentGroup.ChildVariables.Add(variable);
            }
            
        }

        #endregion

        #region 查找变量及变量组

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
        
        #region 退出时保存变量初始值

        /// <summary>
        /// 退出时保存变量当前以便程序复位
        /// </summary>
        public virtual void ExitWithSaving()
        {
            foreach (var variable in RealTimeRepositoryBase.RtDbContext.DigitalSet.Local)
            {
                if (!variable.IsInitValueSaved)
                {
                    continue;
                }
                variable.InitValue = variable.Value;
            }
            foreach (var variable in RealTimeRepositoryBase.RtDbContext.AnalogSet.Local)
            {
                if (!variable.IsInitValueSaved)
                {
                    continue;
                }
                variable.InitValue = variable.Value;
            }
            foreach (var variable in RealTimeRepositoryBase.RtDbContext.TextSet.Local)
            {
                if (!variable.IsInitValueSaved)
                {
                    continue;
                }
                variable.InitValue = variable.Value;
            }
            RealTimeRepositoryBase.RtDbContext.SaveAllChanges();
        }

        #endregion

    }
}
