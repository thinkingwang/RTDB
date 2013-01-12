using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCADA.RTDB.EntityFramework
{
    /// <summary>
    /// 变量仓储模型
    /// </summary>
    public enum VariableRepositoryMode
    {
        EntityFrameworkMode,
        XmlMode
    }

    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DataBaseType
    {
        SqlCeConnectionFactory,
        SqlConnectionFactory,
        LocalDbConnectionFactory
    }

    /// <summary>
    /// 变量仓储配置信息类
    /// </summary>
    public class VariableRepositoryConfig
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


        public VariableRepositoryConfig()
        {
            DbNameOrConnectingString = "VariableDataBase";
            RepositoryMode = VariableRepositoryMode.EntityFrameworkMode;
            ProviderInvariantName = "System.Data.SqlServerCe.4.0";
            DbType = DataBaseType.SqlCeConnectionFactory;
        }

        public VariableRepositoryConfig(string dbNameOrConnectingString, VariableRepositoryMode variableRepositoryMode,
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
