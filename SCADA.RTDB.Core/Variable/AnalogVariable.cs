using System;

namespace SCADA.RTDB.Core.Variable
{
    /// <summary>
    /// 模拟变量
    /// </summary>
    [Serializable]
    public class AnalogVariable : VariableBase
    {
        #region 属性

        /// <summary>
        /// 变量值
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// 死区,变量最小的变化幅度
        /// </summary>
        public double DeadBand { get; set; }

        /// <summary>
        /// 变量初始值
        /// </summary>
        public double InitValue { get; set; }

        /// <summary>
        /// 变量最小值
        /// </summary>
        public double MinValue { get; set; }

        /// <summary>
        /// 变量最大值
        /// </summary>
        public double MaxValue { get; set; }

        /// <summary> 
        /// 工程单位
        /// </summary>
        public string EngineeringUnit { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AnalogVariable()
        {
            
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="group">变量所属组别的名称,如果是根组，需要传递 "" 值</param>
        /// <param name="varName">变量名</param>
        public AnalogVariable(VariableGroup group, string varName = "")
            : base(group, varName, VarValuetype.VarDouble)
        {
            DeadBand = 0;
            InitValue = 0;
            Value = InitValue;
            MaxValue = 80;
            MinValue = 20;
            EngineeringUnit = "";
            ParentGroup = group;

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
            double value;
            if (!double.TryParse(obj.ToString(), out value))
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
            double value;
            if (!double.TryParse(obj.ToString(), out value))
            {
                return false;
            }
            InitValue = value;
            return true;
        }
        
    }
}