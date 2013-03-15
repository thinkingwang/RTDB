using System;
using System.ComponentModel.DataAnnotations;

namespace SCADA.RTDB.Core.Variable
{
    /// <summary>
    /// 字符串变量
    /// </summary>
    [Serializable]
    public class TextVariable : VariableBase
    {
        private string _value;

        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int TextVariableId { get; set; }

        /// <summary>
        /// 变量值
        /// </summary>
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// 变量初始值
        /// </summary>
        public string InitValue { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public TextVariable()
        {
            ValueType = VarValuetype.VarString;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="group">变量所属组别的名称,如果是根组，需要传递 "" 值</param>
        /// <param name="varName">变量名</param>
        public TextVariable(VariableGroup group, string varName = "")
            : base(group, varName, VarValuetype.VarString)
        {
            InitValue = "";
            Value = InitValue;
        }

        #endregion

    }
}