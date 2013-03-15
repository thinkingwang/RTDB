using System;
using System.ComponentModel.DataAnnotations;

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
        /// Id
        /// </summary>
        public int AnalogVariableId { get; set; }

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
            ValueType = VarValuetype.VarDouble;
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

       
    }
}