using System;
using SCADA.RTDB.VariableModel;

namespace SCADA.RTDB.StorageModel
{
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

        public DigitalVariableStorage()
        {
            
        }

        public DigitalVariableStorage(bool isAdd = false)
            : base(isAdd)
        {

        }

        public override void PullCopyProperty(VariableBase variable, int parentGroupId)
        {
            base.PullCopyProperty(variable, parentGroupId);
            InitValue = ((DigitalVariable)variable).InitValue;
        }
        public override void PushCopyProperty(VariableBase variable)
        {
            base.PushCopyProperty(variable);
            ((DigitalVariable)variable).InitValue = InitValue;
            ((DigitalVariable)variable).Value = InitValue;
        }
    }
}