using SCADA.RTDB.Core.Variable;

namespace SCADA.RTDB.Common
{
    /// <summary>
    /// 报警仓储公共接口
    /// </summary>
    public interface IAlarmRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="variable"></param>
        void AddAlarm(VariableBase variable);
    }
}