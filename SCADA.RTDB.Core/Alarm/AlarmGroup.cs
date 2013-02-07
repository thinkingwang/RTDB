using System;
using System.Collections.Generic;

namespace SCADA.RTDB.Core.Alarm
{
    /// <summary>
    /// 报警组模型
    /// </summary>
    [Serializable]
    public class AlarmGroup
    {
        /// <summary>
        /// 报警组Id
        /// </summary>
        public int AlarmGroupId { get; set; }

        /// <summary>
        /// 报警组名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 报警组描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 报警组报警列表
        /// </summary>
        public List<AlarmBase> Alarms { get; set; }
    }
}
