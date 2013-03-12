namespace SCADA.RTDB.EntityFramework.DbConfig
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// 数据库迁移配置信息类
    /// </summary>
    /// <typeparam name="T">需要迁移的数据库上下文</typeparam>
    public  class MigrateDataBaseConfig<T> : DbMigrationsConfiguration<T> where T : DbContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MigrateDataBaseConfig()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

       
    }
}