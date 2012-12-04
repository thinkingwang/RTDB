using System.Collections.Generic;
using RTDB.VariableModel;

namespace RTDB.Common
{
    public interface IVariableContext
    {
        #region 变量集合

        /// <summary>
        /// 模拟变量集合
        /// </summary>
        Dictionary<string, AnalogVariable> AnalogSet { get; }

        /// <summary>
        /// 数字变量集合
        /// </summary>
        Dictionary<string, DigitalVariable> DigitalSet { get; }

        /// <summary>
        /// 字符变量集合
        /// </summary>
        Dictionary<string, StringVariable> StringSet { get; }

        /// <summary>
        /// 变量组集合
        /// </summary>
        List<VariableGroup> VariableGroupSet { get; }

        #endregion
    }
}
