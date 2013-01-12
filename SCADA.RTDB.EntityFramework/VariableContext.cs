using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using SCADA.RTDB.StorageModel;
using SCADA.RTDB.VariableModel;

namespace SCADA.RTDB.EntityFramework
{
    public class VariableContext : DbContext, IConvention
    {
        #region 变量集合和组集合

        /// <summary>
        /// 变量组集合
        /// </summary>
        public IDbSet<VariableGroupStorage> VariableGroupSet { get; set; }
        
        /// <summary>
        /// 数字变量集合
        /// </summary>
        public IDbSet<DigitalVariableStorage> DigitalSet { get; set; }

        /// <summary>
        /// 模拟变量集合
        /// </summary>
        public IDbSet<AnalogVariableStorage> AnalogSet { get; set; }

        /// <summary>
        /// 字符变量集合
        /// </summary>
        public IDbSet<TextVariableStorage> TextSet { get; set; }


        #endregion

        #region 构造函数

        /// <summary>
        /// 变量实体集构造函数
        /// </summary>
        /// <param name="variableRepositoryConfig">变量仓储配置信息类</param>
        public VariableContext(VariableRepositoryConfig variableRepositoryConfig)
            : base(variableRepositoryConfig.DbNameOrConnectingString)
        {
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

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<VariableContext>());

            VariableGroupStorage rootGroup = VariableGroupSet.FirstOrDefault(root => root.ParentId == null);
            if (rootGroup == null)
            {
                rootGroup = new VariableGroupStorage();
                rootGroup.Name = VariableGroup.RootGroup.Name;
                rootGroup.ParentId = null;
                VariableGroupSet.Add(rootGroup);
                base.SaveChanges();
            }

        }

        #endregion

        #region 加载数据库资料到set集合


        #endregion

        #region 保存set集合到数据库

        /// <summary>
        /// 保存集合更改
        /// </summary>
        public void SaveAllChanges()
        {
            base.SaveChanges();
        }
        
        #endregion
    }
}

