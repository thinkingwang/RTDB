using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Globalization;

namespace RTDB.VariableModel
{
    public class VariableGroup
    {
        #region 私有方法

        private readonly List<VariableGroup> _childGroups = new List<VariableGroup>();
        private readonly List<VariableBase> _childVariables = new List<VariableBase>();
        private string _groupName;
        private int? _parentGroupId;

        #endregion

        #region 属性

        /// <summary>
        /// 根组
        /// </summary>
        [NotMapped]
        public static VariableGroup RootGroup { get; private set; }

        /// <summary>
        /// 变量组名称
        /// </summary>
         [Required, MaxLength(50)]
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
        /// 变量组全路径
        /// </summary>
        [NotMapped]
        public string GroupFullPath
        {
            //get { return Parent != null ? Parent.GroupFullPath + "." + GroupName : GroupName; } //带根节点
            get //不带根节点
            {
                if (Parent == null)
                {
                    return "_RootGroup";
                }
                if (string.IsNullOrEmpty(Parent.GroupFullPath) 
                    || Parent.GroupFullPath == "_RootGroup")
                {
                    return GroupName;
                }
                return Parent.GroupFullPath + "." + GroupName;
            }
        }

        /// <summary>
        /// 变量组ID
        /// </summary>
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VariableGroupId { get; set; }

        /// <summary>
        /// 变量父祖Id
        /// </summary>
        public int? ParentGroupId
        {
            get
            {
                //如果父祖存在，直接返回父祖Id
                if (Parent != null)
                {
                    return Parent.VariableGroupId;
                }
                //如果是根组，返回null
                if (Equals(RootGroup))
                {
                    return null;
                }
                //如果父祖为null且不是根组，说明是从数据库加载，返回数据库值
                return _parentGroupId;
            }
            set { _parentGroupId = value; }
        }

        /// <summary>
        /// 子组集合
        /// </summary>
        [NotMapped]
        public virtual List<VariableGroup> ChildGroups
        {
            get { return _childGroups; }
        }

        /// <summary>
        /// 当前组的子组数量
        /// </summary>
        [NotMapped]
        public int GroupsCount
        {
            get { return _childGroups.Count; }
        }

        /// <summary>
        /// 组变量集合
        /// </summary>
        [NotMapped]
        public virtual List<VariableBase> ChildVariables
        {
            get { return _childVariables; }
        }

        /// <summary>
        /// 当前组的变量数量
        /// </summary>
        [NotMapped]
        public int VariablesCount
        {
            get { return _childVariables.Count; }
        }

        /// <summary>
        /// 父节点
        /// </summary>
        [NotMapped]
        public VariableGroup Parent { get; set; }

        //public string test { get; set; }

        #endregion

        #region 构造函数

        public VariableGroup()
        {
            
        }
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
            Parent = parent;
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
