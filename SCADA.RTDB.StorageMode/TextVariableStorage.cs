using System;
using SCADA.RTDB.VariableModel;

namespace SCADA.RTDB.StorageModel
{
    public class TextVariableStorage : VariableBaseStorage
    {
        #region 属性

        /// <summary>
        /// 变量Id
        /// </summary>
        public int TextVariableStorageId { get; set; }

        /// <summary>
        /// 变量初始值
        /// </summary>
        public string InitValue { get; set; }
        
        #endregion
        public TextVariableStorage()
        {
            
        }

        public TextVariableStorage(bool isAdd = false)
            : base(isAdd)
        {
            
        }

        public override void PullCopyProperty(VariableBase variable, int parentGroupId)
        {
            base.PullCopyProperty(variable, parentGroupId);
            InitValue = ((TextVariable)variable).InitValue;
        }
        public override void PushCopyProperty(VariableBase variable)
        {
            base.PushCopyProperty(variable);
            ((TextVariable)variable).InitValue = InitValue;
            ((TextVariable)variable).Value = InitValue;
        }
    }
}