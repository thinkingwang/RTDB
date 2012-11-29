using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml.Linq;
using System.Diagnostics;

namespace Variable
{
    #region 变量枚举类型
    /// <summary>
    /// 变量类型
    /// </summary>
    public enum VARTYPE
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
    public enum VARVALUETYPE
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
        OnlyRead,
        /// <summary>
        /// 只写
        /// </summary>
        OnlyWrite,
    }
    #endregion

    /// <summary>
    /// 变量基类
    /// </summary>
    public class VariableBase
    {
        #region 变量基本属性

        /// <summary>
        /// 变量名称
        /// </summary>
        public string VarName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public VARVALUETYPE VarValueType { get; set; }

        /// <summary>
        /// 变量类型
        /// </summary>
        public VARTYPE VarType { get; set; }

        /// <summary>
        /// 变量描述
        /// </summary>
        public string VarDescription { get; set; }

        /// <summary>
        /// 变量组名    
        /// </summary>
        public string GroupID { get; set; }

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
        public Varoperateproperty OperateProperty { get; set; }

        /// <summary>
        /// 变量全名
        /// </summary>
        public string VariableFullName
        {
            get { return (GroupID == string.Empty || GroupID == "") ? VarName : (GroupID + "." + VarName); }
        }

        #endregion

        /// <summary>
        /// 变量构造函数
        /// </summary>
        /// <param name="groupPath">变量隶属于组别名称</param>
        /// <param name="varName">变量名称</param>
        /// <param name="varValueType">变量类型</param>
        protected VariableBase(string groupPath, string varName = "", VARVALUETYPE varValueType = VARVALUETYPE.VarDouble)
        {
            VarName = (varName == "") ? VariableGroup.GetInitVarName(groupPath) : varName;

            VarValueType = varValueType;
            OperateProperty = Varoperateproperty.ReadWrite;
            GroupID = groupPath;
            IsAddressable = false;
            IsParameterSaved = false;
            IsRecordEvent = false;
            IsValueSaved = true;
        }

        /// <summary>
        /// 拷贝属性
        /// </summary>
        /// <param name="source">源变量对象实例</param>
        public virtual void CopyProperty(VariableBase source)
        {
            if (source == null)
            {
                Debug.Assert(Resource1.AnalogVar_CopyProperty_SourceObjIsNull != null, "Resource1.CopyProperty_SourceObjIsNull != null");
                throw new ArgumentNullException(Resource1.AnalogVar_CopyProperty_SourceObjIsNull);
            }
            VarValueType = source.VarValueType;
            OperateProperty = source.OperateProperty;
            IsAddressable = source.IsAddressable;
            IsParameterSaved = source.IsParameterSaved;
            IsRecordEvent = source.IsRecordEvent;
            IsValueSaved = source.IsValueSaved;
        }
    }
}
