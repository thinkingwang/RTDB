using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using SCADA.RTDB.Core.Alarm;
using SCADA.RTDB.Core.Variable;
using SCADA.RTDB.EntityFramework.DbConfig;

namespace SCADA.RTDB.EntityFramework.Context
{
    /// <summary>
    /// 实时数据的实体集合
    /// </summary>
    public class RealTimeDbContext : DbContext, IConvention
    {
        private static string _connectionStr;
        #region 变量集合和组集合

        /// <summary>
        /// 变量组集合
        /// </summary>
        public IDbSet<VariableGroup> VariableGroupSet { get; set; }
        
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
        public IDbSet<TextVariable> TextSet { get; set; }

        /// <summary>
        /// 报警组集合
        /// </summary>
        public IDbSet<AlarmGroup> AlarmGroupSet { get; set; }

        /// <summary>
        /// 变量报警集合
        /// </summary>
        public IDbSet<AlarmBase> AlarmSet { get; set; }


        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public RealTimeDbContext()
            : base(_connectionStr)
        {
        }

        /// <summary>
        /// 变量实体集构造函数
        /// </summary>
        /// <param name="variableRepositoryConfig">变量仓储配置信息类</param>
        internal RealTimeDbContext(RepositoryConfig variableRepositoryConfig)
            : base(variableRepositoryConfig.DbNameOrConnectingString)
        {
            _connectionStr = variableRepositoryConfig.DbNameOrConnectingString;
            switch (variableRepositoryConfig.DbType)
            {
                case DataBaseType.SqlCeConnectionFactory:
                    Database.DefaultConnectionFactory =
                        new SqlCeConnectionFactory(variableRepositoryConfig.ProviderInvariantName);
                    break;
                case DataBaseType.SqlConnectionFactory:
                    Database.DefaultConnectionFactory = new SqlConnectionFactory();
                    break;
                case DataBaseType.LocalDbConnectionFactory:
                    Database.DefaultConnectionFactory =
                        new LocalDbConnectionFactory(variableRepositoryConfig.LocalDbVersion);
                    break;
            }

           // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<RealTimeDbContext>());
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<RealTimeDbContext, MigrateDataBaseConfig<RealTimeDbContext>>());

            VariableGroup rootGroup = VariableGroupSet.FirstOrDefault(root => root.Parent == null);
            if (rootGroup == null)
            {
                rootGroup = new VariableGroup { Name = VariableGroup.RootGroup.Name, Parent = null };
                VariableGroupSet.Add(rootGroup);
                base.SaveChanges();
            }

        }

        #endregion

        #region 保存set集合到数据库

        /// <summary>
        /// 保存集合更改
        /// </summary>
        internal void SaveAllChanges()
        {
            base.SaveChanges();
        }
        
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlarmBase>().Ignore(m=>m.Variable);
            modelBuilder.Entity<VariableGroup>().Ignore(m => m.ChildVariables);
            modelBuilder.Entity<TextVariable>().Ignore(m => m.ParentGroup);
            modelBuilder.Entity<DigitalVariable>().Ignore(m => m.ParentGroup);
            modelBuilder.Entity<AnalogVariable>().Ignore(m => m.ParentGroup);
            base.OnModelCreating(modelBuilder);
        }
    }
}

