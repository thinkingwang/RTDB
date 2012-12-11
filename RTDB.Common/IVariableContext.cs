using System.Collections.Generic;
using System.Data.Entity;
using RTDB.VariableModel;

namespace RTDB.Common
{
    public interface IVariableContext
    {
        #region 变量集合

        /// <summary>
        /// 模拟变量集合
        /// </summary>
        IDbSet<AnalogVariable>  AnalogSet{ get; set; }

        /// <summary>
        /// 数字变量集合
        /// </summary>
        IDbSet<DigitalVariable>  DigitalSet{ get; set; }

        /// <summary>
        /// 字符变量集合
        /// </summary>
        IDbSet<StringVariable>  StringSet{ get; set; }

        /// <summary>
        /// 变量组集合
        /// </summary>
        IDbSet<VariableGroup> VariableGroupSet { get; set; }

        #endregion

        /// <summary>
        /// 保存变量到实体集
        /// </summary>
        void SaveVariable();
    }
}
