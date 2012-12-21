﻿using System;
using System.Collections.Generic;

namespace SCADA.RTDB.VariableModel
{
    public sealed class VariableGroup
    {
        #region 私有字段

        private readonly List<VariableGroup> _childGroups = new List<VariableGroup>();
        private readonly List<VariableBase> _childVariables = new List<VariableBase>();
        private string _name;
        private int? _parentGroupId;

        #endregion

        #region 属性

        /// <summary>
        /// 根组
        /// </summary>
        public static VariableGroup RootGroup { get; set; }

        /// <summary>
        /// 变量组名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _name = value;
                }
            }
        }

        /// <summary>
        /// 变量组全路径
        /// </summary>
        public string FullPath
        {
            //get { return Parent != null ? Parent.fullPath + "." + Name : Name; } //带根节点
            get //不带根节点
            {
                if (Parent == null)
                {
                    return null;
                }
                if (string.IsNullOrEmpty(Parent.FullPath))
                {
                    return Name;
                }
                return Parent.FullPath + "." + Name;
            }
        }

        /// <summary>
        /// 变量组ID
        /// </summary>
// ReSharper disable UnusedAutoPropertyAccessor.Global
        public int VariableGroupId { get; set; }
// ReSharper restore UnusedAutoPropertyAccessor.Global

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
        public List<VariableGroup> ChildGroups
        {
            get { return _childGroups; }
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
        public VariableGroup Parent { get; set; }


        #endregion

        #region 构造函数

        public VariableGroup()
        {

        }

        /// <summary>
        /// 组构造函数
        /// </summary>
        /// <param name="name">组名称</param>
        /// <param name="parent">父组对象, 为null表示根组</param>
        public VariableGroup(string name, VariableGroup parent)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(Resource1.VariableGroup_VariableGroup_groupNameIsNull);
            }
            _name = name;
            Parent = parent;
        }

        static VariableGroup()
        {
            RootGroup = new VariableGroup(Resource1.VariableGroup__rootGroup_variableDictionary, null);
        }

        #endregion


    }
}
