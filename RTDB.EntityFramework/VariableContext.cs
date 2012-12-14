using System.Data.Entity;
using System.Linq;
using RTDB.VariableModel;

namespace RTDB.EntityFramework
{
    public class VariableContext : DbContext, IVariableContext
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
        public IDbSet<TextVariable> StringSet { get; set; }

        /// <summary>
        /// 变量组集合
        /// </summary>
        public IDbSet<VariableGroup> VariableGroupSet { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 变量实体集构造函数
        /// </summary>
        /// <param name="dbNameOrConnectingString">数据库名称或者数据库连接字符串</param>
        /// <param name="isLoadData">是否添加数据库值，默认添加</param>
        public VariableContext(string dbNameOrConnectingString = "VariableDB", bool isLoadData = true)
            : base(dbNameOrConnectingString)
        {
            Configuration.LazyLoadingEnabled = false;

            //加载现有集合及变量
            if (isLoadData)
            {
                LoadVariable();
            }
        }

        #endregion

        #region 加载数据库资料到set集合

        /// <summary>
        /// 数据库加载组信息以及变量信息
        /// </summary>
        private void LoadVariable()
        {
            //遍历数据库变量组数据到set集合
// ReSharper disable ReturnValueOfPureMethodIsNotUsed
            VariableGroupSet.ToList();
            DigitalSet.ToList();
            AnalogSet.ToList();
            StringSet.ToList();
// ReSharper restore ReturnValueOfPureMethodIsNotUsed

            VariableGroup rootGroup = VariableGroupSet.Local.FirstOrDefault(root => !root.ParentGroupId.HasValue);
            //没有找到根组则添加根组到数据库并返回
            if (rootGroup == null)
            {
                VariableGroupSet.Add(VariableGroup.RootGroup);
                SaveChanges();
                return;
            }

            VariableGroup.RootGroup = VariableGroupSet.Local[0];
            LoadchildGroups(rootGroup);
        }

        /// <summary>
        /// 加载指定变量组的子组及变量
        /// </summary>
        /// <param name="parentGroup">指定变量组</param>
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
                variable.Parent = parentGroup;
            }

            foreach (VariableGroup variableGroup in parentGroup.ChildGroups)
            {
                variableGroup.Parent = parentGroup;
                LoadchildGroups(variableGroup);

            }
        }

        #endregion

        #region 保存set集合到数据库

        /// <summary>
        /// 保存集合更改
        /// </summary>
        public void Save()
        {
            base.SaveChanges();
        }

        #endregion

        #region 数据库表模型重定义

        /// <summary>
        /// 数据库表模型重定义
        /// </summary>
        /// <param name="modelBuilder">数据库表模型</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            VariableGroupTable(modelBuilder);
            TextVariableTable(modelBuilder);
            AnalogVariableTable(modelBuilder);
            DigitalVariableTable(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 变量组数据库表模型定义
        /// </summary>
        /// <param name="modelBuilder">数据库表模型</param>
        private static void VariableGroupTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VariableGroup>().Ignore(p => p.GroupFullPath);
            modelBuilder.Entity<VariableGroup>().Ignore(p => p.ChildVariables);
            modelBuilder.Entity<VariableGroup>().Ignore(p => p.ChildGroups);
            modelBuilder.Entity<VariableGroup>().HasKey(p => p.VariableGroupId);
            modelBuilder.Entity<VariableGroup>().Property(p => p.ParentGroupId);
            modelBuilder.Entity<VariableGroup>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<VariableGroup>().Ignore(p => p.GroupsCount);
            modelBuilder.Entity<VariableGroup>().Ignore(p => p.VariablesCount);
            modelBuilder.Entity<VariableGroup>().Ignore(p => p.Parent);
        }

        /// <summary>
        /// 字符变量数据库表模型定义
        /// </summary>
        /// <param name="modelBuilder">数据库表模型</param>
        private static void TextVariableTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TextVariable>().Ignore(p => p.VariableBaseFullPath);
            modelBuilder.Entity<TextVariable>().Ignore(p => p.OperateProperty);
            modelBuilder.Entity<TextVariable>().Ignore(p => p.ValueType);
            modelBuilder.Entity<TextVariable>().Ignore(p => p.VariableType);
            modelBuilder.Entity<TextVariable>().Ignore(p => p.Parent);
            modelBuilder.Entity<TextVariable>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        }

        /// <summary>
        /// 模拟变量数据库表模型定义
        /// </summary>
        /// <param name="modelBuilder">数据库表模型</param>
        private static void AnalogVariableTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnalogVariable>().Ignore(p => p.VariableBaseFullPath);
            modelBuilder.Entity<AnalogVariable>().Ignore(p => p.OperateProperty);
            modelBuilder.Entity<AnalogVariable>().Ignore(p => p.ValueType);
            modelBuilder.Entity<AnalogVariable>().Ignore(p => p.VariableType);
            modelBuilder.Entity<AnalogVariable>().Ignore(p => p.Parent);
            modelBuilder.Entity<AnalogVariable>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        }

        /// <summary>
        /// 数字变量数据库表模型定义
        /// </summary>
        /// <param name="modelBuilder">数据库表模型</param>
        private static void DigitalVariableTable(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DigitalVariable>().Ignore(p => p.VariableBaseFullPath);
            modelBuilder.Entity<DigitalVariable>().Ignore(p => p.OperateProperty);
            modelBuilder.Entity<DigitalVariable>().Ignore(p => p.ValueType);
            modelBuilder.Entity<DigitalVariable>().Ignore(p => p.VariableType);
            modelBuilder.Entity<DigitalVariable>().Ignore(p => p.Parent);
            modelBuilder.Entity<DigitalVariable>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        }

        #endregion

    }
}
