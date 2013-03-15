using System;
using System.ComponentModel.DataAnnotations;

namespace SCADA.RTDB.Core.Variable
{
    /// <summary>
    /// 数字变量
    /// </summary>
    [Serializable]
    public class DigitalVariable : VariableBase
    {
        #region 属性
        /// <summary>
        /// Id
        /// </summary>
        public int DigitalVariableId { get; set; }

        /// <summary>
        /// 变量值
        /// </summary>
        public bool Value { get; set; }

        /// <summary>
        /// 变量初始值
        /// </summary>
        public bool InitValue { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DigitalVariable()
        {
            ValueType = VarValuetype.VarBool;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="group">变量所属组别的名称,如果是根组，需要传递 "" 值</param>
        /// <param name="varName">变量名</param>
        public DigitalVariable(VariableGroup group, string varName = "")
            : base(group, varName, VarValuetype.VarBool)
        {
            InitValue = false;
            Value = InitValue;
        }

        #endregion

    }
}