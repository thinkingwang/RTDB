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
        public VariableGroup ParentGroup { get; set; }

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
        }
        
        #endregion


        /// <summary>
        /// 获取变量值
        /// </summary>
        /// <returns>返回变量值</returns>
        public virtual object GetValue()
        {
            return null;
        }

       
        /// <summary>
        /// 设置变量值
        /// </summary>
        /// <param name="obj">准备写入的值</param>
        /// <returns>是否写入成功</returns>
        public virtual bool SetValue(object obj)
        {
            //只读变量不允许修改
            if (OperateProperty == VarOperateProperty.ReadOnly)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取变量初始值
        /// </summary>
        /// <returns>返回变量初始值</returns>
        public virtual object GetInitValue()
        {
            return null;
        }


        /// <summary>
        /// 设置变量初始值
        /// </summary>
        /// <param name="obj">准备写入的值</param>
        /// <returns>是否写入成功</returns>
        public virtual bool SetInitValue(object obj)
        {
            return true;
        }
    }

}
