using System;

namespace SCADA.RTDB.Core.Alarm
{
    /// <summary>
    /// 模拟变量报警模型
    /// </summary>
    [Serializable]
    public class AnalogAlarm : AlarmBase
    {
        #region 使能位标志

        /// <summary>
        /// 低低报警使能位
        /// </summary>
        public const int LowLowEnabled = 0x0001;

        /// <summary>
        /// 低报警使能位
        /// </summary>
        public const int LowEnabled = 0x0002;

        /// <summary>
        /// 高报警使能位
        /// </summary>
        public const int HighEnabled = 0x0004;

        /// <summary>
        /// 高高报警使能位
        /// </summary>
        public const int HighHighEnabled = 0x0008;

        /// <summary>
        /// 偏差下限报警使能位
        /// </summary>
        public const int LowerDeviationEnabled = 0x0010;

        /// <summary>
        /// 偏差上限报警使能位
        /// </summary>
        public const int UpperDeviationEnabled = 0x0020;

        /// <summary>
        /// 变化率报警使能位
        /// </summary>
        public const int RateOfChangeEnabled = 0x0040;

        /// <summary>
        /// 延迟报警使能位
        /// </summary>
        public const int DelayEnabled = 0x0080;

        /// <summary>
        /// 报警死区使能位
        /// </summary>
        public const int DeadBandEnabled = 0x0080;

        #endregion

        #region 模拟量报警属性

        /// <summary>
        /// 模拟量报警类型
        /// </summary>
        public int AnalogAlarmType { get; set; }
        
        /// <summary>
        /// 模拟量偏差或越限延迟报警时间，单位秒
        /// </summary>
        public int DelayValue { get; set; }

        #region 上下限报警

        /// <summary>
        /// 模拟量低低报警值
        /// </summary>
        public double LowLowValue { get; set; }

        /// <summary>
        /// 模拟量低低报警文本
        /// </summary>
        public string LowLowContent { get; set; }

        /// <summary>
        /// 模拟量低报警值
        /// </summary>
        public double LowValue { get; set; }

        /// <summary>
        /// 模拟量低报警文本
        /// </summary>
        public string LowContent { get; set; }

        /// <summary>
        /// 模拟量高报警值
        /// </summary>
        public double HighValue { get; set; }

        /// <summary>
        /// 模拟量高报警文本
        /// </summary>
        public string HighContent { get; set; }

        /// <summary>
        /// 模拟量高高报警值
        /// </summary>
        public double HighHighValue { get; set; }

        /// <summary>
        /// 模拟量高高报警文本
        /// </summary>
        public string HighHighContent { get; set; }

        #endregion

        #region 偏差报警

        /// <summary>
        /// 模拟量偏差下限报警值
        /// </summary>
        public double LowerDeviationValue { get; set; }

        /// <summary>
        /// 模拟量偏差下限报警文本
        /// </summary>
        public string LowerDeviationContent { get; set; }

        /// <summary>
        /// 模拟量偏差上限报警值
        /// </summary>
        public double UpperDeviationValue { get; set; }

        /// <summary>
        /// 模拟量偏差上限报警文本
        /// </summary>
        public string UpperDeviationContent { get; set; }

        /// <summary>
        /// 模拟量偏差报警目标值
        /// </summary>
        public double DeviationTargetValue { get; set; }

        /// <summary>
        /// 模拟量报警死区值
        /// </summary>
        public double DeadBandValue { get; set; }

        #endregion

        #region 变化率报警

        /// <summary>
        /// 模拟量变化率报警值
        /// </summary>
        public double RateOfChangeValue { get; set; }

        /// <summary>
        /// 模拟量变化率报警文本
        /// </summary>
        public string RateOfChangeContent { get; set; }

        #endregion
        
        #endregion
        
    }
}