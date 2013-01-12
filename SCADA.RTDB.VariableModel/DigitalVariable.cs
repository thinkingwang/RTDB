using System;

namespace SCADA.RTDB.VariableModel
{
    public class DigitalVariable : VariableBase
    {
        #region 属性

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

        public DigitalVariable()
        {
            
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