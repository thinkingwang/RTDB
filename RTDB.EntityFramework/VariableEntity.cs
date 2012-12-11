using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Objects;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using RTDB.Common;
using RTDB.VariableModel;

namespace RTDB.EntityFramework
{
    internal sealed class ReportingDbMigrationsConfiguration : DbMigrationsConfiguration<VariableEntity>
    {
        public ReportingDbMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
    [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "IDisposable is specified by IEmployeeContext and the implementation is inherited from ObjectContext")]
    public class VariableEntity : DbContext, IVariableContext
    {

        #region 变量集合和组集合
        /// <summary>
        /// 数字变量集合
        /// </summary>
        public IDbSet<DigitalVariable> DigitalSet { get; set; }

        /// <summary>
        /// 模拟变量集合
        /// </summary>
        public IDbSet<AnalogVariable> AnalogSet { get; set; }

        /// <summary>
        /// 字符变量集合
        /// </summary>
        public IDbSet<StringVariable> StringSet { get; set; }

        /// <summary>
        /// 变量组集合
        /// </summary>
        public IDbSet<VariableGroup> VariableGroupSet { get; set; }

        #endregion

        /// <summary>
        /// 变量实体集构造函数
        /// </summary>
        /// <param name="dbNameOrConnectingString"></param>
        /// <param name="isLoadData"></param>
        public VariableEntity(string dbNameOrConnectingString = "VariableEntity", bool isLoadData = true)
            : base(dbNameOrConnectingString)
            //: base("Data Source=cnwj6iapc006\\sqlexpress;Initial Catalog=VariableEntity;User ID=sa;Password=666666")
        {
            Configuration.LazyLoadingEnabled = false;

            //加载现有集合及变量
            if (isLoadData)
            {
                LoadVariable();
            }
        }
        
        /// <summary>
        /// 数据库加载组信息以及变量信息
        /// </summary>
        public void LoadVariable()
        {
            //遍历数据库变量组数据到set集合
            VariableGroupSet.ToList();
            DigitalSet.ToList();
            AnalogSet.ToList();
            StringSet.ToList();

            VariableGroup rootGroup = VariableGroupSet.Local.FirstOrDefault(root => !root.ParentGroupId.HasValue);
            //没有找到根组则添加根组到数据库并返回
            if (rootGroup == null)
            {
                VariableGroupSet.Add(VariableGroup.RootGroup);
                SaveChanges();
                return;
            }

            VariableGroup.RootGroup.VariableGroupId = rootGroup.VariableGroupId;
            LoadchildGroups(VariableGroup.RootGroup);
        }


        private void LoadchildGroups(VariableGroup parentGroup)
        {
            parentGroup.ChildGroups.AddRange(VariableGroupSet.Local.Where(
                    childGroup => childGroup.ParentGroupId == parentGroup.VariableGroupId));
            parentGroup.ChildVariables.AddRange(
                    DigitalSet.Local.Where(childVariable => childVariable.GroupId == parentGroup.VariableGroupId));
            parentGroup.ChildVariables.AddRange(
                AnalogSet.Local.Where(childVariable => childVariable.GroupId == parentGroup.VariableGroupId));
            parentGroup.ChildVariables.AddRange(
                StringSet.Local.Where(childVariable => childVariable.GroupId == parentGroup.VariableGroupId));

            foreach (VariableBase variable in parentGroup.ChildVariables)
            {
                variable.Group = parentGroup;
            }

            foreach (VariableGroup variableGroup in parentGroup.ChildGroups)
            {
                variableGroup.Parent = parentGroup;
                LoadchildGroups(variableGroup);
                
            }
        }

        /// <summary>
        /// 保存集合更改
        /// </summary>
        public void SaveVariable()
        {
            base.SaveChanges();
        }
        
    }
}
