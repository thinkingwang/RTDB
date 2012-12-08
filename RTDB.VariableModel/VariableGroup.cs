using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RTDB.VariableModel
{
    public class VariableGroup
    {
        #region 私有方法

        private readonly VariableGroup _parent;
        private readonly List<VariableGroup> _childGroups = new List<VariableGroup>();
        private  List<VariableBase> _childVariables = new List<VariableBase>();
        private string _groupName;

        #endregion

        #region 属性

        /// <summary>
        /// 根组
        /// </summary>
        public static VariableGroup RootGroup { get; private set; }

        /// <summary>
        /// 变量组名称
        /// </summary>
        public string GroupName
        {
            get { return _groupName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _groupName = value;
                }
            }
        }

        /// <summary>
        /// 变量组Id
        /// </summary>
        public string VariableGroupId
        {
            //get { return Parent != null ? Parent.VariableGroupId + "." + GroupName : GroupName; } //带根节点
            get //不带根节点
            {
                if (Parent == null)
                {
                    return "";
                }
                if (string.IsNullOrEmpty(Parent.VariableGroupId))
                {
                    return GroupName;
                }
                return Parent.VariableGroupId + "." + GroupName;
            }
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
            //set { _childGroups = value; }
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
        /// 组构造函数
        /// </summary>
        /// <param name="groupName">组名称</param>
        /// <param name="parent">父组对象, 为null表示根组</param>
        public VariableGroup(string groupName, VariableGroup parent)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                throw new ArgumentNullException(Resource1.VariableGroup_VariableGroup_groupNameIsNull);
            }
            _groupName = groupName;
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
