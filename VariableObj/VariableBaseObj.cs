using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml.Linq;
using System.Diagnostics;

namespace VariableObj
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
    public enum VAROPERATEPROPERTY
    {
        ReadWrite,
        OnlyRead,
        OnlyWrite,
    }
    #endregion

    /// <summary>
    /// 变量基类
    /// </summary>
    public class VariableBaseObj
    {
        #region 变量属性

        #region 变量基本属性
        /// <summary>
        /// 变量名称
        /// </summary>
        public string VarName;
        /// <summary>
        /// 数据类型
        /// </summary>
        public VARVALUETYPE VarValueType;
        /// <summary>
        /// 变量类型
        /// </summary>
        public VARTYPE VarType;

        /// <summary>
        /// 变量描述
        /// </summary>
        public string VarDescription;

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

        public VAROPERATEPROPERTY OperateProperty;

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
        public static Dictionary<string, AnalogVar> analogSet =
           new System.Collections.Generic.Dictionary<string, AnalogVar>();

        /// <summary>
        /// 数字变量集合
        /// </summary>
        public static Dictionary<string, DigitalVar> digitalSet =
            new System.Collections.Generic.Dictionary<string, DigitalVar>();

        #endregion

       


        #region 公有方法

        public virtual void AddVar(string groupID, string varName)
        {

        }


        public static void EditVar(VariableBaseObj varObj, string newVarName)
        {
        }

        public static void DeleteVar(string groupID, string varName)
        {

        }

        public static void DeleteAllVarByGroup(string groupId)
        {
        }

        public static List<VariableBaseObj> FindAllVarByGroup(string groupId)
        {
            return null;
        }

        #endregion
    }

    public class DigitalVar : VariableBaseObj
    {

    }

    public class AnalogVar : VariableBaseObj
    {

    }

    public class Alarm
    {
        
    }

}
