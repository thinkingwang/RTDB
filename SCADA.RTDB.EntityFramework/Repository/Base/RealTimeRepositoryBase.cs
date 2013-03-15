using SCADA.RTDB.EntityFramework.Context;

namespace SCADA.RTDB.EntityFramework.Repository.Base
{
    /// <summary>
    /// 实时数据的实体集合
    /// </summary>
    public class RealTimeRepositoryBase
    {
        /// <summary>
        /// 实时数据库
        /// </summary>
        public static RealTimeDbContext RtDbContext { get; private set; }

        #region 构造函数

        /// <summary>
        /// 初始化对象
        /// </summary>
        /// <param name="realTimeDbContext">指定实时数据库</param>
        internal static void Initialize(RealTimeDbContext realTimeDbContext)
        {
            //实体存在不需要再次创建，直接返回
            if (RtDbContext != null)
            {
                return;
            }

            RtDbContext = realTimeDbContext;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        private RealTimeRepositoryBase()
        {
        }

        #endregion

        
    }
}