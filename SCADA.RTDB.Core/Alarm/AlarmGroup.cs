using System;
using System.Collections.Generic;

namespace SCADA.RTDB.Core.Alarm
{
    /// <summary>
    /// 保证报警变量组名为的委托
    /// </summary>
    /// <param name="name"></param>
    public delegate bool AlarmGroupVerifyTheUniquenessEventHandler(string name);
    /// <summary>
    /// 报警组模型
    /// </summary>
    [Serializable]
    public class AlarmGroup
    {
        private string _name;

        /// <summary>
        /// 报警变量组名改变之前触发的事件
        /// </summary>
        public static event AlarmGroupVerifyTheUniquenessEventHandler VerifyTheUniqueness;
        /// <summary>
        /// 报警组Id
        /// </summary>
        public int AlarmGroupId { get; set; }

        /// <summary>
        /// 报警组名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (VerifyTheUniqueness(value))
                {
                    _name = value;
                }
                else
                {
                    throw new Exception("该报警组名称已经存在");
                }
            }
        }

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
