using System;
using SCADA.RTDB.Core.Variable;

namespace SCADA.RTDB.Core.Alarm
{
    /// <summary>
    /// 保证报警变量名为的委托
    /// </summary>
    /// <param name="name"></param>
    public delegate bool AlarmVerifyTheUniquenessEventHandler(string name);


    /// <summary>
    /// 报警基类模型
    /// </summary>
    [Serializable]
    public class AlarmBase
    {
        /// <summary>
        /// 报警变量名改变之前触发的事件
        /// </summary>
        public static event AlarmVerifyTheUniquenessEventHandler VerifyTheUniqueness;

        private VariableBase _variable;
        private string _name;
        private string _absolutePath;

        /// <summary>
        /// 报警Id
        /// </summary>
        public int AlarmBaseId { get; set; }

        /// <summary>
        /// 报警名称
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
                    throw new Exception("该报警名称已经存在");
                }
                
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //public int Test { get; set; }

        /// <summary>
        /// 报警的变量
        /// </summary>
        public VariableBase Variable
        {
            get { return _variable; }
            set
            {
                _variable = value;
                AbsolutePath = _variable != null ? _variable.AbsolutePath : string.Empty;
            }
        }

        /// <summary>
        /// 报警变量绝对路径
        /// </summary>
        public string AbsolutePath
        {
            get { return _variable == null ? _absolutePath : _variable.AbsolutePath; }
            set { _absolutePath = value; }
        }

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
        public string Description { get; set; }

        protected AlarmBase()
        {
        }
    }

}