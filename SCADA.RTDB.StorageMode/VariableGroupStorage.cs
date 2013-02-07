using System;
using System.Collections.Generic;
using System.Linq;
using SCADA.RTDB.Core.Variable;

namespace SCADA.RTDB.StorageModel
{
    /// <summary>
    /// 变量组存储模型
    /// </summary>
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

        /// <summary>
        /// 更新变量组存储模型属性
        /// </summary>
        /// <param name="variableGroup">变量组</param>
        /// <param name="parentId">变量组父Id</param>
        public void CopyProperty(VariableGroup variableGroup, int? parentId)
        {
            Name = variableGroup.Name;
            ParentId = parentId;
        }
        
    }
}
