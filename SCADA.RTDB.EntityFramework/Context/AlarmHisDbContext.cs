using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SCADA.RTDB.EntityFramework.Context
{
    /// <summary>
    /// 历史数据的实体集合
    /// </summary>
    public class AlarmHisDbContext : DbContext, IConvention
    {
         
    }
}