using System;
using System.Collections.Generic;
using System.Linq;
using SCADA.RTDB.VariableModel;

namespace SCADA.RTDB.StorageModel
{
    public sealed class VariableGroupStorage
    {
        #region 属性

        /// <summary>
        /// 变量组ID
        /// </summary>
        public int VariableGroupStorageId { get; set; }

        /// <summary>
        /// 变量组名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public int? ParentId { get; set; }
        
        #endregion

        public void CopyProperty(VariableGroup variableGroup, int? parentId)
        {
            Name = variableGroup.Name;
            ParentId = parentId;
        }
        
    }
}
