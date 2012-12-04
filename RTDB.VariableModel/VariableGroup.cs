using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RTDB.VariableModel
{
    public class VariableGroup
    {
        #region 私有方法

        private readonly VariableGroup _parent;
        private  List<VariableGroup> _childGroups = new List<VariableGroup>();
        private  List<VariableBase> _childVariables = new List<VariableBase>();

        #endregion

        #region 属性

        /// <summary>
        /// 根组
        /// </summary>
        public static VariableGroup RootGroup { get; set; }

        /// <summary>
        /// 变量组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 变量组Id
        /// </summary>
        public string VariableGroupId
        {
            get { return Parent != null ? Parent.VariableGroupId + "." + GroupName : GroupName; }
        }

        /// <summary>
        /// 变量父祖Id
        /// </summary>
        public string ParentGroupId
        {
            get { return Parent != null ? Parent.VariableGroupId : null; }
        }

        /// <summary>
        /// 子组集合
        /// </summary>
        public List<VariableGroup> ChildGroups
        {
            get { return _childGroups; }
            set { _childGroups = value; }
        }

        /// <summary>
        /// 当前组的子组数量
        /// </summary>
        public int GroupsCount
        {
            get { return _childGroups.Count; }
        }

        /// <summary>
        /// 组变量集合
        /// </summary>
        public List<VariableBase> ChildVariables
        {
            get { return _childVariables; }
            set { _childVariables = value; }
        }

        /// <summary>
        /// 当前组的变量数量
        /// </summary>
        public int VariablesCount
        {
            get { return _childVariables.Count; }
        }

        /// <summary>
        /// 父节点
        /// </summary>
        public VariableGroup Parent
        {
            get { return _parent; }

        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 组默认构造函数
        /// </summary>
        /// <param name="parent">父组对象</param>
        public VariableGroup(VariableGroup parent)
        {
            _parent = parent;
        }

        /// <summary>
        /// 组构造函数
        /// </summary>
        /// <param name="groupName">组名称</param>
        /// <param name="parent">父组对象</param>
        public VariableGroup(string groupName, VariableGroup parent)
        {
            GroupName = groupName;
            _parent = parent;
        }

        static VariableGroup()
        {
            Debug.Assert(Resource1.VariableGroup__rootGroup_variableDictionary != null, 
                "Resource1.VariableGroup__rootGroup_variableDictionary != null");

            RootGroup = new VariableGroup(Resource1.VariableGroup__rootGroup_variableDictionary, null);
        }

        #endregion


    }
}
