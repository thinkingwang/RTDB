using System.Linq;
using SCADA.RTDB.Common;
using SCADA.RTDB.Core.Variable;
using SCADA.RTDB.StorageModel;

namespace SCADA.RTDB.EntityFramework.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class RepositoryDecoratorAlarm:RepositoryDecorator,IAlarmRepository
    {

        public override void Load()
        {
            base.Load();
            LoadAlarm();
        }
        private void LoadAlarm()
        {
            foreach (var alarm in RtDbContext.AlarmSet.Local)
            {
                alarm.Variable = FindVariableByPath(alarm.AbsolutePath);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="variable"></param>
        public void AddAlarm(VariableBase variable)
        {

        }
    }
}