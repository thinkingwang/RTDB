using SCADA.RTDB.Core.Variable;

namespace SCADA.RTDB.Common.Runtime
{
    /// <summary>
    /// 变量运行仓储接口
    /// </summary>
    public interface IVariableRunTimeRepository 
    {
        /// <summary>
        /// 加载参数
        /// </summary>
        void Load();


        /// <summary>
        /// 根据变量Id提供的路径信息，遍历树查找变量
        /// </summary>
        /// <param name="absolutePath">变量全路径</param>
        /// <returns>返回变量对象，未找到返回null</returns>
        VariableBase FindVariableByPath(string absolutePath);
    }
}