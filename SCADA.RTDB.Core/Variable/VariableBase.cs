using System;

namespace SCADA.RTDB.Core.Variable
{
    #region 变量枚举类型
    /// <summary>
    /// 变量类型
    /// </summary>
    public enum VarType
    {
        /// <summary>
        /// 基本类型
        /// </summary>
        VarNormal,
        /// <summary>
        /// 结构体类型
        /// </summary>
        VarStruct,
        /// <summary>
        /// 引用类型
        /// </summary>
        VarRef
    }

    /// <summary>
    /// 变量数据类型
    /// </summary>
    public enum VarValuetype
    {
        /// <summary>
        /// 开关量
        /// </summary>
        VarBool,
        /// <summary>
        /// 模拟量
        /// </summary>
        VarDouble,
        /// <summary>
        /// 字符量
        /// </summary>
        VarString
    }

    /// <summary>
    /// 变量操作属性，可读写，只读、只写
    /// </summary>
    public enum VarOperateProperty
    {
        /// <summary>
        /// 可读写
        /// </summary>
        ReadWrite,
        /// <summary>
        /// 只读
        /// </summary>
        ReadOnly,
        /// <summary>
        /// 只写
        /// </summary>
        WriteOnly,
    }
    #endregion

    /// <summary>
    /// 变量基类
    /// </summary>
    [Serializable]
    public class VariableBase
    {
        private string _name;
        private VariableGroup _parentGroup;
        private string _parentGroupPath;
        private DateTime _createTime;

        #region 变量基本属性
        
        /// <summary>
        /// 变量名称
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
        /// 变量组
        /// </summary>
        public VariableGroup ParentGroup
        {
            get { return _parentGroup; }
            set
            {
                _parentGroup = value;
                ParentGroupPath = _parentGroup != null ? _parentGroup.AbsolutePath : string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ParentGroupPath
        {
            get { return _parentGroup == null ? _parentGroupPath : _parentGroup.AbsolutePath; }
            set { _parentGroupPath = value; }
        }

        /// <summary>
        /// 变量绝对路径
        /// </summary>
        public string AbsolutePath
        {
            get
            {
                return ((ParentGroup == null) || (ParentGroup.Parent == null)) ? Name : (ParentGroup.AbsolutePath + "." + Name);
            }
        }
        
        /// <summary>
        /// 数据类型
        /// </summary>
        public VarValuetype ValueType { get; set; }
        
        /// <summary>
        /// 变量类型
        /// </summary>
        public VarType VariableType{ get; set; }

        /// <summary>
        /// 变量描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否保存数值
        /// </summary>
        public bool IsValueSaved { get; set; }

        /// <summary>
        /// 是否保存参数
        /// </summary>
        public bool IsInitValueSaved { get; set; }

        /// <summary>
        /// 是否允许外部程序访问
        /// </summary>
        public bool IsAddressable { get; set; }

        /// <summary>
        /// 是否记录事件
        /// </summary>
        public bool IsRecordEvent { get; set; }

        /// <summary>
        /// 变量操作属性（可读写、只读、只写）
        /// </summary>
        public VarOperateProperty OperateProperty { get; set; }
        
        /// <summary>
        /// 变量建立时间
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
        protected VariableBase()
        {
        }

        /// <summary>
        /// 变量构造函数
        /// </summary>
        /// <param name="group">父祖</param>
        /// <param name="varName">变量名称</param>
        /// <param name="varValueType">变量类型</param>
        protected VariableBase(VariableGroup group, string varName = "",
                               VarValuetype varValueType = VarValuetype.VarDouble)
        {
            Name = varName;
            ValueType = varValueType;
            VariableType = VarType.VarNormal;
            OperateProperty = VarOperateProperty.ReadWrite;
            IsAddressable = false;
            IsInitValueSaved = false;
            IsRecordEvent = false;
            IsValueSaved = true;
            ParentGroup = group;
            CreateTime = DateTime.Now;
        }
        
        #endregion

    }

}
