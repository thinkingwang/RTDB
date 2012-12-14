using RTDB.VariableModel;

namespace RTDB.Common
{
    public interface IVariableRepository
    {
        /// <summary>
        /// 根据组Id获取组对象
        /// </summary>
        /// <param name="curGroupFullPath">组全路径，等于null或为空字符返回根组</param>
        /// <returns>返回组对象，未找到返回null</returns>
        VariableGroup GetGroup(string curGroupFullPath);

        /// <summary>
        /// 向当前组添加子组
        /// </summary>
        /// <param name="groupName">子组名称</param>
        /// <param name="curGroupFullPath">要添加的组全路径</param>
        void AddGroup(string groupName, string curGroupFullPath);

        /// <summary>
        /// 删除指定组
        /// </summary>
        /// <param name="curGroupFullPath">要移除的组全路径</param>
        void RemoveGroup(string curGroupFullPath);

        /// <summary>
        /// 修改当前变量组名称
        /// </summary>
        /// <param name="groupName">修改后的组名</param>
        /// <param name="curGroupFullPath">要重命名的组全路径</param>
        void RenameGroup(string groupName, string curGroupFullPath);

        /// <summary>
        /// 粘贴变量组
        /// </summary>
        /// <param name="sourceGroup">需要粘贴的变量组</param>
        /// <param name="curGroupFullPath">粘贴变量的目标组</param>
        /// <param name="isCopy">是否为复制，true为复制，false为剪切</param>
        /// <param name="pasteMode">粘贴模式，0：默认模式，重复则返回，1：如果重复则替换，2：如果重复则两个变量都保留，3：如果重复则放弃</param>
        /// <returns>如果默认模式下且有相同变量名称存在返回变量新名称，否则返回粘贴变量名称</returns>
        string PasteGroup(VariableGroup sourceGroup, string curGroupFullPath, bool isCopy,
                          uint pasteMode = 0);

        /// <summary>
        /// 向当前组添加变量
        /// </summary>
        /// <param name="variable">需要添加的变量</param>
        void AddVariable(VariableBase variable);

        /// <summary>
        /// 删除指定变量
        /// </summary>
        /// <param name="variableName">变量名称</param>
        /// <param name="curGroupFullPath">移除变量所属组全路径</param>
        void RemoveVariable(string variableName, string curGroupFullPath);

        /// <summary>
        /// 删除当前组的所有变量
        /// </summary>
        /// <param name="curGroupFullPath">要清空的组全路径</param>
        void ClearVariable(string curGroupFullPath);

        /// <summary>
        /// 更新指定变量
        /// </summary>
        /// <param name="oldVariable">指定变量</param>
        /// <param name="newVariable">修改后的变量</param>
        void EditVariable(VariableBase oldVariable, VariableBase newVariable);

        /// <summary>
        /// 粘贴变量
        /// </summary>
        /// <param name="sourceVariable">需要粘贴的变量</param>
        /// <param name="curGroupFullPath">粘贴变量的目标组</param>
        /// <param name="isCopy">是否为复制，true为复制，false为剪切</param>
        /// <param name="pasteMode">粘贴模式，0：默认模式，重复则返回，1：如果重复则替换，2：如果重复则两个变量都保留，3：如果重复则放弃</param>
        /// <returns>如果默认模式下且有相同变量名称存在返回变量新名称，否则返回粘贴变量名称</returns>
        string PasteVariable(VariableBase sourceVariable, string curGroupFullPath, bool isCopy,
                             uint pasteMode = 0);
    }
}
