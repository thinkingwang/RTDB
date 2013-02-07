using System;

namespace SCADA.RTDB.Core.Alarm
{
    /// <summary>
    /// 数字变量报警类型
    /// </summary>
    public enum DigitalAlarmType
    {
        /// <summary>
        /// 关报警
        /// </summary>
        OffAlarm,
        /// <summary>
        /// 开报警
        /// </summary>
        OnAlarm,
        /// <summary>
        /// 开或关报警
        /// </summary>
        OnOrOffAlarm
    }

    /// <summary>
    /// 数字变量报警
    /// </summary>
    [Serializable]
    public class DigitalAlarm : AlarmBase
    {
        /// <summary>
        /// 数字报警类型
        /// </summary>
        public DigitalAlarmType DigitalAlarmType { get; set; }

        /// <summary>
        /// 数字报警类型,int型
        /// </summary>
        public int DigitalAlarmIntType
        {
            get { return (int) DigitalAlarmType; }
            set { DigitalAlarmType = (DigitalAlarmType) value; }
        }

        /// <summary>
        /// 数字开报警文本
        /// </summary>
        public string OnAlarmContent { get; set; }

        /// <summary>
        /// 数字关报警文本
        /// </summary>
        public string OffAlarmContent { get; set; }

        /// <summary>
        /// 数字开到关报警文本
        /// </summary>
        public string OnToOffAlarmContent { get; set; }

        /// <summary>
        /// 数字关到开报警文本
        /// </summary>
        public string OffToOnAlarmContent { get; set; }
    }
}