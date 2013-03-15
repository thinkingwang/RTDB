using System.Collections.Generic;
using SCADA.RTDB.Core.Variable;

namespace SCADA.RTDB.Common.Base
{
    /// <summary>
    /// 变量仓储类接口，负责加载和查询变量
    /// </summary>
    public interface IVariableRepository
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

        /// <summary>
        /// 查询组路径下面所有变量
        /// </summary>
        /// <param name="absolutePath">组路径</param>
        /// <returns>所有变量列表</returns>
        IEnumerable<VariableBase> FindVariables(string absolutePath);

        /// <summary>
        /// 根据组Id提供的路径信息，遍历树查找组节点
        /// </summary>
        /// <param name="absolutePath">组全路径，等于null或为空字符返回根组</param>
        /// <returns>返回组对象，未找到返回null</returns>
        VariableGroup FindGroupByPath(string absolutePath);

        /// <summary>
        /// 查询组路径下面所有子组
        /// </summary>
        /// <param name="absolutePath">组路径</param>
        /// <returns>所有子组列表</returns>
        IEnumerable<VariableGroup> FindGroups(string absolutePath);

        /// <summary>
        /// 退出时保存变量当前以便程序复位
        /// </summary>
        void ExitWithSaving();

    }
}