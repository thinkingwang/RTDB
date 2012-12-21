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
    public enum Varvaluetype
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
        #region 私有字段

        private string _name;
        private int _groupId;
        private int _variableType;
        private int _operateProperty;
        private int _valueType;

        #endregion

        #region 变量基本属性
        
        /// <summary>
        /// 变量全名
        /// </summary>
        public string fullPath
        {
            get
            {
                return ((Parent == null) || (Parent.Parent == null)) ? Name : (Parent.FullPath + "." + Name);
            }
        }

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
        /// 变量的组Id
        /// </summary>
        public int GroupId
        {
            get
            {
                if (Parent == null)
                {
                    return _groupId;
                }
                return Parent.VariableGroupId;
            }
            set { _groupId = value; }
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public Varvaluetype ValueType
        {
            get { return (Varvaluetype)_valueType; }
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
        /// 变量组
        /// </summary>
        public VariableGroup Parent { get; set; }

        /// <summary>
        /// 是否保存数值
        /// </summary>
        public bool IsValueSaved { get; set; }

        /// <summary>
        /// 是否保存参数
        /// </summary>
        public bool IsParameterSaved { get; set; }

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
        /// <param name="group">变量隶属于组别名称</param>
        /// <param name="varName">变量名称</param>
        /// <param name="varValueType">变量类型</param>
        protected VariableBase(VariableGroup group, string varName = "",
                               Varvaluetype varValueType = Varvaluetype.VarDouble)
        {
            if (group == null)
            {
                throw new ArgumentNullException(Resource1.VariableBase_VariableBase_groupIsNull);
            }
            Name = varName;
            ValueType = varValueType;
            VariableType = VarType.VarNormal;
            OperateProperty = Varoperateproperty.ReadWrite;
            Parent = group;
            IsAddressable = false;
            IsParameterSaved = false;
            IsRecordEvent = false;
            IsValueSaved = true;
        }

        #endregion

        #region 复制变量

        /// <summary>
        /// 拷贝属性
        /// </summary>
        /// <param name="source">源变量对象实例</param>
        public virtual void CopyProperty(VariableBase source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(Resource1.CopyProperty_SourceObjIsNull);
            }
            _name = source.Name;
            VariableType = source.VariableType;
            OperateProperty = source.OperateProperty;
            Description = source.Description;
            IsAddressable = source.IsAddressable;
            IsParameterSaved = source.IsParameterSaved;
            IsRecordEvent = source.IsRecordEvent;
            IsValueSaved = source.IsValueSaved;
        }

        #endregion

    }
}
