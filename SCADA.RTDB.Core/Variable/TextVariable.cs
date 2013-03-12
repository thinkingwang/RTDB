using System;

namespace SCADA.RTDB.Core.Variable
{
    /// <summary>
    /// 字符串变量
    /// </summary>
    [Serializable]
    public class TextVariable : VariableBase
    {
        #region 属性
        
        /// <summary>
        /// 变量值
        /// </summary>
        public string Value { get; set; }

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
            Value = Convert.ToString(obj);
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
            InitValue = Convert.ToString(obj);
            return true;
        }
    }
}