using System.Collections.Generic;
using SCADA.RTDB.VariableModel;

namespace SCADA.RTDB.Repository
{
    /// <summary>
    /// 变量及变量组集合发生改变时触发事件
    /// </summary>
    public delegate void DataChangedEvent();

    public interface IVariableRepository
    {
        /// <summary>
        /// 变量及变量组集合发生改变时触发事件
        /// </summary>
        event DataChangedEvent DataChanged;

        /// <summary>
        /// Context是否有变化
        /// </summary>
        bool IsChanged { get; }

        /// <summary>
        /// 根据组Id获取组对象
        /// </summary>
        /// <param name="fullPath">组全路径，等于null或为空字符返回根组</param>
        /// <returns>返回组对象，未找到返回null</returns>
        VariableGroup FindGroupById(string fullPath);

        /// <summary>
        /// 根据组Id提供的路径信息，遍历树查找组节点
        /// </summary>
        /// <param name="fullPath">组全路径，等于null或为空字符返回根组</param>
        /// <returns>返回组对象，未找到返回null</returns>
        VariableGroup FindGroupByPath(string fullPath);

        /// <summary>
        /// 查询组路径下面所有子组
        /// </summary>
        /// <param name="fullPath">组路径</param>
        /// <returns>所有子组列表</returns>
        IEnumerable<VariableGroup> FindGroups(string fullPath);

        /// <summary>
        /// 向当前组添加子组
        /// </summary>
        /// <param name="name">子组名称</param>
        /// <param name="fullPath">要添加的组全路径</param>
        void AddGroup(string name, string fullPath);

        /// <summary>
        /// 删除指定组
        /// </summary>
        /// <param name="fullPath">要移除的组全路径</param>
        void RemoveGroup(string fullPath);

        /// <summary>
        /// 修改当前变量组名称
        /// </summary>
        /// <param name="name">修改后的组名</param>
        /// <param name="fullPath">要重命名的组全路径</param>
        void RenameGroup(string name, string fullPath);

        /// <summary>
        /// 粘贴变量组
        /// </summary>
        /// <param name="source">需要粘贴的变量组</param>
        /// <param name="fullPath">粘贴变量的目标组</param>
        /// <param name="isCopy">是否为复制，true为复制，false为剪切</param>
        /// <param name="pasteMode">粘贴模式，0：默认模式，重复则返回，1：如果重复则替换，2：如果重复则两个变量都保留，3：如果重复则放弃</param>
        /// <returns>如果默认模式下且有相同变量名称存在返回变量新名称，否则返回粘贴变量名称</returns>
        string PasteGroup(VariableGroup source, string fullPath, bool isCopy,
                          uint pasteMode = 0);

        /// <summary>
        /// 向当前组添加变量
        /// </summary>
        /// <param name="variable">需要添加的变量</param>
        void AddVariable(VariableBase variable);

        /// <summary>
        /// 删除指定变量
        /// </summary>
        /// <param name="name">变量名称</param>
        /// <param name="fullPath">移除变量所属组全路径</param>
        void RemoveVariable(string name, string fullPath);

        /// <summary>
        /// 删除当前组的所有变量
        /// </summary>
        /// <param name="fullPath">要清空的组全路径</param>
        void ClearVariable(string fullPath);

        /// <summary>
        /// 更新指定变量
        /// </summary>
        /// <param name="oldVariable">指定变量</param>
        /// <param name="newVariable">修改后的变量</param>
        void EditVariable(VariableBase oldVariable, VariableBase newVariable);

        /// <summary>
        /// 根据变量Id查找变量
        /// </summary>
        /// <param name="fullPath">变量全路径，如果路径指向组则返回组下所有节点，否则返回找到的变量或null</param>
        /// <returns>返回变量对象，未找到返回null</returns>
        VariableBase FindVariableById(string fullPath);

        /// <summary>
        /// 根据变量Id提供的路径信息，遍历树查找变量
        /// </summary>
        /// <param name="fullPath">变量全路径</param>
        /// <returns>返回变量对象，未找到返回null</returns>
        VariableBase FindVariableByPath(string fullPath);

        /// <summary>
        /// 查询组路径下面所有变量
        /// </summary>
        /// <param name="fullPath">组路径</param>
        /// <returns>所有变量列表</returns>
        IEnumerable<VariableBase> FindVariables(string fullPath);
        
        /// <summary>
        /// 粘贴变量
        /// </summary>
        /// <param name="source">需要粘贴的变量</param>
        /// <param name="fullPath">粘贴变量的目标组</param>
        /// <param name="isCopy">是否为复制，true为复制，false为剪切</param>
        /// <param name="pasteMode">粘贴模式，0：默认模式，重复则返回，1：如果重复则替换，2：如果重复则两个变量都保留，3：如果重复则放弃</param>
        /// <returns>如果默认模式下且有相同变量名称存在返回变量新名称，否则返回粘贴变量名称</returns>
        string PasteVariable(VariableBase source, string fullPath, bool isCopy,
                             uint pasteMode = 0);

        /// <summary>
        /// 提交数据库更改
        /// </summary>
        void Save();
    }
}
