namespace SCADA.RTDB.EntityFramework.DbConfig
{
    /// <summary>
    /// 变量仓储模型
    /// </summary>
    public enum VariableRepositoryMode
    {
        /// <summary>
        /// 数据库存储模型
        /// </summary>
        EntityFrameworkMode,
        /// <summary>
        /// XML存储模型
        /// </summary>
        XmlMode
    }

    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DataBaseType
    {
        /// <summary>
        /// Compact
        /// </summary>
        SqlCeConnectionFactory,
        /// <summary>
        /// sql
        /// </summary>
        SqlConnectionFactory,
        /// <summary>
        /// local db
        /// </summary>
        LocalDbConnectionFactory
    }

    /// <summary>
    /// 变量仓储配置信息类
    /// </summary>
    public class RepositoryConfig
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DataBaseType DbType { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string DbNameOrConnectingString { get; set; }

        /// <summary>
        /// 本地数据库版本，只对LocalDbConnectionFactory有效
        /// </summary>
        public string LocalDbVersion { get; set; }

        /// <summary>
        /// SqlCe数据库自身不变信息
        /// </summary>
        public string ProviderInvariantName { get; set; }

        /// <summary>
        /// 变量仓储存储模型
        /// </summary>
        public VariableRepositoryMode RepositoryMode { get; set; }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public RepositoryConfig()
        {
            DbNameOrConnectingString = "VariableDataBase";
            RepositoryMode = VariableRepositoryMode.EntityFrameworkMode;
            ProviderInvariantName = "System.Data.SqlServerCe.4.0";
            DbType = DataBaseType.SqlCeConnectionFactory;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbNameOrConnectingString">变量仓储数据库连接字符串</param>
        /// <param name="variableRepositoryMode">变量存储方式（XML或EF）</param>
        /// <param name="dbType">变量仓储数据库类型</param>
        /// <param name="localDbVersion">本地数据库版本信息</param>
        /// <param name="providerInvariantName">Compact有效信息名称</param>
        public RepositoryConfig(string dbNameOrConnectingString, VariableRepositoryMode variableRepositoryMode,
                                        DataBaseType dbType, string localDbVersion,
                                        string providerInvariantName = "System.Data.SqlServerCe.4.0")
        {
            DbNameOrConnectingString = dbNameOrConnectingString;
            RepositoryMode = variableRepositoryMode;
            DbType = dbType;
            LocalDbVersion = localDbVersion;
            ProviderInvariantName = providerInvariantName;
        }
    }
}
