using System;

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

        /// <summary>
        /// 获取变量值
        /// </summary>
        /// <returns>返回变量值</returns>
        public override object GetValue()
        {
            return Value;
        }

        /// <summary>
        /// 设置变量值
        /// </summary>
        /// <param name="obj">准备写入的值</param>
        /// <returns>是否写入成功</returns>
        public override bool SetValue(object obj)
        {
            if (!base.SetValue(obj))
            {
                return false;
            }
            bool value;
            if (!bool.TryParse(obj.ToString(), out value))
            {
                return false;
            }
            Value = value;
            return true;
        }

        /// <summary>
        /// 获取变量初始值
        /// </summary>
        /// <returns>返回变量初始值</returns>
        public override object GetInitValue()
        {
            return InitValue;
        }


        /// <summary>
        /// 设置变量初始值
        /// </summary>
        /// <param name="obj">准备写入的值</param>
        /// <returns>是否写入成功</returns>
        public override bool SetInitValue(object obj)
        {
            bool value;
            if (!bool.TryParse(obj.ToString(), out value))
            {
                return false;
            }
            InitValue = value;
            return true;
        }

    }
}