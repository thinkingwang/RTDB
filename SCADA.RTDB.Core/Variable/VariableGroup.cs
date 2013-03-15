using System;
using System.Collections.Generic;

namespace SCADA.RTDB.Core.Variable
{
    /// <summary>
    /// 变量分组
    /// </summary>
    [Serializable]
    public sealed class VariableGroup
    {
        #region 私有字段

        private List<VariableGroup> _childGroups = new List<VariableGroup>();
        private string _name;
        private readonly List<VariableBase> _childVariables = new List<VariableBase>();
        private DateTime _createTime;

        #endregion
        
        /// <summary>
        /// 根组
        /// </summary>
        public static VariableGroup RootGroup { get; set; }

        #region 属性

        /// <summary>
        /// Id
        /// </summary>
        public int VariableGroupId { get; set; }

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
        public string AbsolutePath
        {
            get //不带根节点
            {
                if (Parent == null)
                {
                    return string.Empty;
                }
                if (string.IsNullOrEmpty(Parent.AbsolutePath))
                {
                    return Name;
                }
                return Parent.AbsolutePath + "." + Name;
            }
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
        /// 组变量集合
        /// </summary>
        public List<VariableBase> ChildVariables
        {
            get { return _childVariables; }
        }

        /// <summary>
        /// 父节点
        /// </summary>
        public VariableGroup Parent { get; set; }

        /// <summary>
        /// 变量组建立时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return _createTime; }
            set
            {
                if (_createTime == new DateTime())
                {
                    _createTime = value;
                }
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
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
            CreateTime = DateTime.Now;

        }

        static VariableGroup()
        {
            RootGroup = new VariableGroup(Resource1.VariableGroup__rootGroup_variableDictionary, null);
        }

        #endregion

    }
}
