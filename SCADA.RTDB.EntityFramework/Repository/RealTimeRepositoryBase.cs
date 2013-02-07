using SCADA.RTDB.EntityFramework.Context;

namespace SCADA.RTDB.EntityFramework.Repository
{
    /// <summary>
    /// 实时数据的实体集合
    /// </summary>
    public class RealTimeRepositoryBase
    {
        /// <summary>
        /// 实时数据库
        /// </summary>
        internal static RealTimeDbContext RtDbContext { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="realTimeDbContext">指定实时数据库</param>
        protected RealTimeRepositoryBase(RealTimeDbContext realTimeDbContext)
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
        protected RealTimeRepositoryBase()
        {
        }
    }
}