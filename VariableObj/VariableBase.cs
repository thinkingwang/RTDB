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
        ReadWrite,
        OnlyRead,
        OnlyWrite,
    }
    #endregion

    /// <summary>
    /// 变量基类
    /// </summary>
    public class VariableBase
    {
        private static UInt32 _varCount = 0;

        #region 变量属性

        #region 变量基本属性
        /// <summary>
        /// 变量名称
        /// </summary>
        public string VarName;
        /// <summary>
        /// 数据类型
        /// </summary>
        public VARVALUETYPE VarValueType = VARVALUETYPE.VarDouble;
        /// <summary>
        /// 变量类型
        /// </summary>
        public VARTYPE VarType;

        /// <summary>
        /// 变量描述
        /// </summary>
        public string VarDescription;


        /// <summary>
        /// 是否保存数值
        /// </summary>
        public bool IsValueSaved;

        /// <summary>
        /// 是否保存参数
        /// </summary>
        public bool IsParameterSaved;

        /// <summary>
        /// 是否允许外部程序访问
        /// </summary>
        public bool IsAddressable;

        /// <summary>
        /// 是否记录事件
        /// </summary>
        public bool IsRecordEvent;


        public Varoperateproperty OperateProperty;

        #endregion

        #region 变量报警属性

        #endregion

        #region 变量历史记录属性

        #endregion


        #region 变量分组属性
        /// <summary>
        /// 变量组名    
        /// </summary>
        public string GroupID;
        #endregion

        #endregion

        #region 变量集合

        /// <summary>
        /// 模拟变量集合
        /// </summary>
        protected static Dictionary<string, AnalogVar> analogSet =
           new System.Collections.Generic.Dictionary<string, AnalogVar>();

        /// <summary>
        /// 数字变量集合
        /// </summary>
        protected static Dictionary<string, DigitalVar> digitalSet =
            new System.Collections.Generic.Dictionary<string, DigitalVar>();

        /// <summary>
        /// 字符变量集合
        /// </summary>
        protected static Dictionary<string, StringVar> stringSet =
            new System.Collections.Generic.Dictionary<string, StringVar>();

        #endregion

        public VariableBase()
        {
            _varCount++;
            VarName = "Variable" + _varCount.ToString(CultureInfo.InvariantCulture);
            OperateProperty = Varoperateproperty.ReadWrite;
        }

        public VariableBase(string varName)
        {
            _varCount++;
            VarName = varName;
            OperateProperty = Varoperateproperty.ReadWrite;
        }

        #region 公有方法

        public static void AddVar(AnalogVar analogElement)
        {
            if (analogElement == null) throw new ArgumentNullException(Resource1.VariableBase_AddVar_analogElement_is_null);
            analogSet.Add(analogElement.VarName, analogElement);
        }

        public static void AddVar(DigitalVar digitalElement)
        {
            if (digitalElement == null) throw new ArgumentNullException(Resource1.VariableBase_AddVar_digitalElement_is_null);
            digitalSet.Add(digitalElement.VarName, digitalElement);
        }

        public static void AddVar(StringVar stringElement)
        {
            if (stringElement == null) throw new ArgumentNullException(Resource1.VariableBase_AddVar_stringElement_is_null);
            stringSet.Add(stringElement.VarName, stringElement);
        }

        public static void EditVar(VariableBase varObj, string newVarName)
        {
            if (varObj == null)
            {
                throw new ArgumentNullException(Resource1.VariableBase_EditVar_varObj_is_null);
            }
        }


        public void RemoveVar()
        {
            if (this.VarValueType == VARVALUETYPE.VarDouble)
            {
                analogSet.Remove(this.VarName);
            }
            else if (this.VarValueType == VARVALUETYPE.VarBool)
            {
                digitalSet.Remove(this.VarName);
            }
            else if (this.VarValueType == VARVALUETYPE.VarDouble)
            {
                stringSet.Remove(this.VarName);
            }
        }
        
        #endregion
    }

    public class DigitalVar : VariableBase
    {
        /// <summary>
        /// 变量初始值
        /// </summary>
        public bool InitValue;

    }

    public class AnalogVar : VariableBase
    {
        /// <summary>
        /// 死区,变量最小的变化幅度
        /// </summary>
        public double DeadArea;

        /// <summary>
        /// 变量初始值
        /// </summary>
        public double InitValue;

        /// <summary>
        /// 变量最小值
        /// </summary>
        public double MinValue;

        /// <summary>
        /// 变量最大值
        /// </summary>
        public double MaxValue;

        /// <summary>
        /// 工程单位
        /// </summary>
        public string ProjectUnit;

        public AnalogVar():base()
        {
            this.DeadArea = 0;
            this.GroupID = "";
            this.InitValue = 0;
            this.IsAddressable = false;
            this.IsParameterSaved = false;
            this.IsRecordEvent = false;
            this.IsValueSaved = true;
            this.MaxValue = 100;
            this.MinValue = 20;
            
            this.ProjectUnit = null;
            
        }

    }

    public class StringVar : VariableBase
    {
        /// <summary>
        /// 变量初始值
        /// </summary>
        public string InitValue;
    }

}
