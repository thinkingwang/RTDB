using SCADA.RTDB.Core.Variable;

namespace SCADA.RTDB.StorageModel
{
    /// <summary>
    /// 模拟变量存储模型
    /// </summary>
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

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AnalogVariableStorage()
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isAdd">变量OrderId是否自增，从数据库加载为false，添加变量为true</param>
        public AnalogVariableStorage(bool isAdd = false)
            : base(isAdd)
        {
            
        }
        /// <summary>
        /// 更新变量存储模型属性
        /// </summary>
        /// <param name="variable">变量</param>
        /// <param name="parentGroupId">父祖Id</param>
        public override void PullCopyProperty(VariableBase variable, int parentGroupId)
        {
            base.PullCopyProperty(variable, parentGroupId);
            InitValue = ((AnalogVariable)variable).InitValue;
            MaxValue = ((AnalogVariable)variable).MaxValue;
            MinValue = ((AnalogVariable)variable).MinValue;
            DeadBand = ((AnalogVariable)variable).DeadBand;
            EngineeringUnit = ((AnalogVariable)variable).EngineeringUnit;
        }

        /// <summary>
        /// 更新变量的属性
        /// </summary>
        /// <param name="variable">需要更新的变量</param>
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