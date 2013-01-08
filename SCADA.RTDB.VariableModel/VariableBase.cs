using System;

namespace SCADA.RTDB.VariableModel
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
    public enum Varoperateproperty
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
    public class VariableBase
    {
        private string _name;
        private int _variableType;
        private int _operateProperty;
        private int _valueType;

        /// <summary>
        /// 变量组
        /// </summary>
        public VariableGroup ParentGroup;
        
        #region 变量基本属性

        /// <summary>
        /// 唯一标示
        /// </summary>
        public int UniqueId { get;set; }

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
        /// 变量绝对路径
        /// </summary>
        public virtual string AbsolutePath { get; private set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public VarValuetype ValueType
        {
            get { return (VarValuetype)_valueType; }
            private set { _valueType = (int)value; }
        }

        /// <summary>
        /// 变量类型
        /// </summary>
        public int IntValueType
        {
            get { return _valueType; }
            set { _valueType = value; }
        }
        
        /// <summary>
        /// 变量类型
        /// </summary>
        public VarType VariableType
        {
            get { return (VarType)_variableType; }
            set { _variableType = (int)value; }
        }

        /// <summary>
        /// 变量类型
        /// </summary>
        public int IntVariableType
        {
            get { return _variableType; }
            set { _variableType = value; }
        }

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
        public Varoperateproperty OperateProperty
        {
            get { return (Varoperateproperty)_operateProperty; }
            set { _operateProperty = (int)value; }
        }

        /// <summary>
        /// 变量操作属性（可读写、只读、只写）,数据库存取
        /// </summary>
        public int IntOperateProperty
        {
            get { return _operateProperty; }
            set { _operateProperty = value; }
        }

        #endregion

        #region 构造函数

        protected VariableBase()
        {

        }

        /// <summary>
        /// 变量构造函数
        /// </summary>
        /// <param name="varName">变量名称</param>
        /// <param name="varValueType">变量类型</param>
        protected VariableBase(string varName = "",
                               VarValuetype varValueType = VarValuetype.VarDouble)
        {
            Name = varName;
            ValueType = varValueType;
            VariableType = VarType.VarNormal;
            OperateProperty = Varoperateproperty.ReadWrite;
            IsAddressable = false;
            IsInitValueSaved = false;
            IsRecordEvent = false;
            IsValueSaved = true;
        }

        #endregion

    }

}
