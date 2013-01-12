using System;
using System.Reflection;
using SCADA.RTDB.VariableModel;

namespace SCADA.RTDB.StorageModel
{
    /// <summary>
    /// 变量基类
    /// </summary>
    public class VariableBaseStorage
    {
        /// <summary>
        /// 初始化变量顺序ID
        /// </summary>
        private static int InitOrderId { get; set; }

        #region 变量基本属性

        /// <summary>
        /// 变量名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 变量建立顺序
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 变量类型
        /// </summary>
        public int ValueType { get; set; }

        /// <summary>
        /// 变量类型
        /// </summary>
        public int VariableType { get; set; }

        /// <summary>
        /// 变量描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否保存数值
        /// </summary>
        public bool IsValueSaved { get; set; }

        /// <summary>
        /// 是否保存参数
        /// </summary>
        public bool IsInitValueSaved { get; set; }

        /// <summary>
        /// 是否允许外部程序访问
        /// </summary>
        public bool IsAddressable { get; set; }

        /// <summary>
        /// 是否记录事件
        /// </summary>
        public bool IsRecordEvent { get; set; }

        /// <summary>
        /// 变量操作属性（可读写、只读、只写）,数据库存取
        /// </summary>
        public int OperateProperty { get; set; }
        
        /// <summary>
        /// 变量组
        /// </summary>
        public int ParentId { get; set; }

        #endregion

        protected VariableBaseStorage(bool isAdd = false)
        {
            if (isAdd)
                OrderId = ++InitOrderId;
        }

        public virtual void PullCopyProperty(VariableBase variable, int parentGroupId)
        {
            Name = variable.Name;
            Description = variable.Description;
            IsAddressable = variable.IsAddressable;
            IsInitValueSaved = variable.IsInitValueSaved;
            IsRecordEvent = variable.IsRecordEvent;
            IsValueSaved = variable.IsValueSaved;
            ValueType = (int)variable.ValueType;
            VariableType = (int)variable.VariableType;
            ParentId = parentGroupId;
            OperateProperty = (int) variable.OperateProperty;
        }

        public virtual void PushCopyProperty(VariableBase variable)
        {
            variable.Name = Name;
            variable.Description = Description;
            variable.IsAddressable = IsAddressable;
            variable.IsInitValueSaved = IsInitValueSaved;
            variable.IsRecordEvent = IsRecordEvent;
            variable.IsValueSaved = IsValueSaved;
            variable.ValueType = (VarValuetype)ValueType;
            variable.VariableType = (VarType)VariableType;
            variable.OperateProperty = (VarOperateProperty) OperateProperty;
            if (OrderId > InitOrderId)
            {
                InitOrderId = OrderId;
            }
        }
    }

}
