using SCADA.RTDB.Core.Variable;

namespace SCADA.RTDB.StorageModel
{
    /// <summary>
    /// 数字变量存储模型
    /// </summary>
    public class DigitalVariableStorage : VariableBaseStorage
    {
        #region 属性

        /// <summary>
        /// 变量Id
        /// </summary>
        public int DigitalVariableStorageId { get; set; }
        
        /// <summary>
        /// 变量初始值
        /// </summary>
        public bool InitValue { get; set; }

       
        #endregion
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DigitalVariableStorage()
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isAdd">变量OrderId是否自增，从数据库加载为false，添加变量为true</param>
        public DigitalVariableStorage(bool isAdd = false)
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
            InitValue = ((DigitalVariable)variable).InitValue;
        }

        /// <summary>
        /// 更新变量的属性
        /// </summary>
        /// <param name="variable">需要更新的变量</param>
        public override void PushCopyProperty(VariableBase variable)
        {
            base.PushCopyProperty(variable);
            ((DigitalVariable)variable).InitValue = InitValue;
            ((DigitalVariable)variable).Value = InitValue;
        }
    }
}