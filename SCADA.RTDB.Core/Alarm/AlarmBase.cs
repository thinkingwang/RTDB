using System;
using SCADA.RTDB.Core.Variable;

namespace SCADA.RTDB.Core.Alarm
{
    /// <summary>
    /// 报警基类模型
    /// </summary>
    [Serializable]
    public class AlarmBase
    {
        private VariableBase _variable;

        /// <summary>
        /// 报警Id
        /// </summary>
        public int AlarmBaseId { get; set; }

        /// <summary>
        /// 报警名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 报警的变量
        /// </summary>
        public VariableBase Variable
        {
            get { return _variable; }
            set
            {
                _variable = value;
                if (_variable != null)
                {
                    Decription = _variable.Description;
                }
            }
        }

        /// <summary>
        /// 报警变量绝对路径
        /// </summary>
        public string AbsolutePath { get; set; }

        /// <summary>
        /// 报警级别
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 报警组别
        /// </summary>
        public AlarmGroup Group { get; set; }

        /// <summary>
        /// 报警描述
        /// </summary>
        public string Decription { get; set; }
    }

}