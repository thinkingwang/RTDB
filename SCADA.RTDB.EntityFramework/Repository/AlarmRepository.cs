using System.Collections.Generic;
using SCADA.RTDB.Common;
using SCADA.RTDB.Core.Alarm;
using SCADA.RTDB.Core.Variable;

namespace SCADA.RTDB.EntityFramework.Repository
{
    /// <summary>
    ///     报警仓储
    /// </summary>
    public class AlarmRepository : IAlarmRepository
    {
        private readonly IVariableRepository _iVariableRepository;

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="variableRepository">变量仓储</param>
        protected AlarmRepository(IVariableRepository variableRepository)
        {
            _iVariableRepository = variableRepository;
        }

        /// <summary>
        ///     添加变量报警
        /// </summary>
        /// <param name="variable"></param>
        public void AddAlarm(VariableBase variable)
        {
        }

        /// <summary>
        ///     报警组数据加载
        /// </summary>
        public void Load()
        {
            foreach (AlarmBase alarm in RealTimeRepositoryBase.RtDbContext.AlarmSet.Local)
            {
                alarm.Variable = _iVariableRepository.FindVariableByPath(alarm.AbsolutePath);
            }
        }

        /// <summary>
        ///     根据报警名称，删除报警
        /// </summary>
        /// <param name="name"></param>
        public void RemoveAlarm(string name)
        {
        }

        /// <summary>
        ///     删除指定报警组
        /// </summary>
        /// <param name="alarmGroup"></param>
        public void RemoveAlarm(AlarmGroup alarmGroup)
        {
        }

        /// <summary>
        ///     清除报警
        /// </summary>
        public void Clear()
        {
        }

        /// <summary>
        ///     修改报警信息
        /// </summary>
        public void EditAlarm()
        {
        }

        /// <summary>
        ///     根据变量绝对路径，查找指定变量的报警对象
        /// </summary>
        /// <param name="variablePath"></param>
        /// <returns></returns>
        public AlarmBase FindAlarmByVariable(string variablePath)
        {
            return null;
        }

        /// <summary>
        ///     根据报警名查找抱紧对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AlarmBase FindAlarmByName(string name)
        {
            return null;
        }

        /// <summary>
        ///     查找所有报警对象
        /// </summary>
        /// <returns></returns>
        public List<AlarmBase> FindAll()
        {
            return null;
        }
    }
}