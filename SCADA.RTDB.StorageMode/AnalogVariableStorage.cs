using System;
using SCADA.RTDB.VariableModel;

namespace SCADA.RTDB.StorageModel
{
    public class AnalogVariableStorage : VariableBaseStorage
    {
        #region 属性

        /// <summary>
        /// 变量Id
        /// </summary>
        public int AnalogVariableStorageId{ get; set; }
        
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

        public AnalogVariableStorage()
        {
            
        }

        public AnalogVariableStorage(bool isAdd = false)
            : base(isAdd)
        {
            
        }

        public override void PullCopyProperty(VariableBase variable, int parentGroupId)
        {
            base.PullCopyProperty(variable, parentGroupId);
            InitValue = ((AnalogVariable)variable).InitValue;
            MaxValue = ((AnalogVariable)variable).MaxValue;
            MinValue = ((AnalogVariable)variable).MinValue;
            DeadBand = ((AnalogVariable)variable).DeadBand;
            EngineeringUnit = ((AnalogVariable)variable).EngineeringUnit;
        }
        public override void PushCopyProperty(VariableBase variable)
        {
            base.PushCopyProperty(variable);
            ((AnalogVariable)variable).InitValue = InitValue;
            ((AnalogVariable)variable).MaxValue = MaxValue;
            ((AnalogVariable)variable).MinValue = MinValue;
            ((AnalogVariable)variable).DeadBand = DeadBand;
            ((AnalogVariable)variable).EngineeringUnit = EngineeringUnit;
            ((AnalogVariable)variable).Value = InitValue;
        }
    }
}