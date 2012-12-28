using System;
using System.Collections.Generic;

namespace SCADA.RTDB.VariableModel
{
    public sealed class VariableGroup
    {
        #region 私有字段

        private readonly List<VariableGroup> _childGroups = new List<VariableGroup>();
        private string _name;
        private List<DigitalVariable> _digitalVariables=new List<DigitalVariable>();
        private List<AnalogVariable> _analogVariables=new List<AnalogVariable>();
        private List<TextVariable> _textVariables=new List<TextVariable>();
        private readonly List<VariableBase> _childVariables = new List<VariableBase>();

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
            //get { return ParentGroup != null ? ParentGroup.fullPath + "." + Name : Name; } //带根节点
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
        public int VariableGroupId { get; set; }

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
        public List<DigitalVariable> DigitalVariables
        {
            get { return _digitalVariables; }
            private set { _digitalVariables = value; }
        }

        /// <summary>
        /// 组变量集合
        /// </summary>
        public List<AnalogVariable> AnalogVariables
        {
            get { return _analogVariables; }
            private set { _analogVariables = value; }
        }

        /// <summary>
        /// 组变量集合
        /// </summary>
        public List<TextVariable> TextVariables
        {
            get { return _textVariables; }
            private set { _textVariables = value; }
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
