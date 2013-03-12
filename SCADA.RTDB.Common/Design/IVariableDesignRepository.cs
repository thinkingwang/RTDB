using System.Collections.Generic;
using SCADA.RTDB.Core.Variable;

namespace SCADA.RTDB.Common.Design
{
    /// <summary>
    /// 变量仓储公共接口
    /// </summary>
    public interface IVariableDesignRepository 
    {
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
        /// 向当前组添加子组
        /// </summary>
        /// <param name="name">子组名称</param>
        /// <param name="absolutePath">要添加的组全路径</param>
        VariableGroup AddGroup(string name, string absolutePath);

        /// <summary>
        /// 删除指定组
        /// </summary>
        /// <param name="absolutePath">要移除的组全路径</param>
        void RemoveGroup(string absolutePath);

        /// <summary>
        /// 修改当前变量组名称
        /// </summary>
        /// <param name="name">修改后的组名</param>
        /// <param name="absolutePath">要重命名的组全路径</param>
        VariableGroup RenameGroup(string name, string absolutePath);

        /// <summary>
        /// 移动当前组
        /// </summary>
        /// <param name="groupAbsolutePath">当前组路径</param>
        /// <param name="desAbsolutePath">目标路径</param>
        /// <returns>移动成功返回true</returns>
        bool MoveGroup(string groupAbsolutePath, string desAbsolutePath);

        /// <summary>
        /// 粘贴变量组
        /// </summary>
        /// <param name="source">需要粘贴的变量组</param>
        /// <param name="absolutePath">粘贴变量的目标组</param>
        /// <param name="isCopy">是否为复制，true为复制，false为剪切</param>
        /// <param name="pasteMode">粘贴模式，0：默认模式，重复则返回，1：如果重复则替换，2：如果重复则两个变量都保留，3：如果重复则放弃</param>
        /// <returns>如果默认模式下且有相同变量名称存在返回变量新名称，否则返回粘贴变量名称</returns>
        string PasteGroup(VariableGroup source, string absolutePath, bool isCopy,
                          uint pasteMode = 0);

        /// <summary>
        /// 向当前组添加变量
        /// </summary>
        /// <param name="variable">需要添加的变量</param>
        VariableBase AddVariable(VariableBase variable);

        /// <summary>
        /// 修改变量属性
        /// </summary>
        /// <param name="variable">修改前变量</param>
        /// <param name="variableStrings">修改后变量属性字符串序列</param>
        /// <returns>修改成功返回true，修改失败返回false</returns>
        VariableBase EditVariable(VariableBase variable, List<string> variableStrings);

        /// <summary>
        /// 修改变量属性
        /// </summary>
        /// <param name="variable">修改前变量</param>
        /// <param name="newVariable">修改后变量</param>
        /// <returns>修改成功返回true，修改失败返回false</returns>
        VariableBase EditVariable(VariableBase variable, VariableBase newVariable);

        /// <summary>
        /// 设置变量值
        /// </summary>
        /// <param name="variable">变量</param>
        /// <param name="value">变量值</param>
        /// <returns>设置成功返回true，失败返回false</returns>
        bool SetVariableValue(VariableBase variable, object value);

        /// <summary>
        /// 删除指定变量
        /// </summary>
        /// <param name="name">变量名称</param>
        /// <param name="absolutePath">移除变量所属组全路径</param>
        void RemoveVariable(string name, string absolutePath);

        /// <summary>
        /// 删除当前组的所有变量
        /// </summary>
        /// <param name="absolutePath">要清空的组全路径</param>
        void ClearVariable(string absolutePath);
        
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
        /// 移动当前变量
        /// </summary>
        /// <param name="variableAbsolutePath">当前变量路径</param>
        /// <param name="desAbsolutePath">目标路径</param>
        /// <returns>移动成功返回true</returns>
        bool MoveVariable(string variableAbsolutePath, string desAbsolutePath);
        
        /// <summary>
        /// 粘贴变量
        /// </summary>
        /// <param name="source">需要粘贴的变量</param>
        /// <param name="absolutePath">粘贴变量的目标组</param>
        /// <param name="isCopy">是否为复制，true为复制，false为剪切</param>
        /// <param name="pasteMode">粘贴模式，0：默认模式，重复则返回，1：如果重复则替换，2：如果重复则两个变量都保留，3：如果重复则放弃</param>
        /// <returns>如果默认模式下且有相同变量名称存在返回变量新名称，否则返回粘贴变量名称</returns>
        string PasteVariable(VariableBase source, string absolutePath, bool isCopy,
                             uint pasteMode = 0);

        /// <summary>
        /// 退出时保存变量当前以便程序复位
        /// </summary>
        void ExitWithSaving();

        /// <summary>
        /// 加载参数
        /// </summary>
        void Load();
    }
}
