using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using SCADA.RTDB.Core.Variable;

namespace SCADA.RTDB.EntityFramework.ExtendMethod
{
    /// <summary>
    /// 变量扩展方法
    /// </summary>
    public static class VariableExtend
    {
        /// <summary>
        /// 获取变量属性字符串列表，变量基类扩展方法
        /// 列表顺序：
        ///     0、变量名
        ///     1、变量绝对路径
        ///     2、变量类型
        ///     3、变量值类型
        ///     4、变量初始值
        ///     5、变量最小值
        ///     6、变量最大值
        ///     7、变量值
        ///     8、变量死区值
        ///     9、变量读写类型
        ///     10、是否保存变量值
        ///     11、是否保存初始值
        ///     12、是否允许变量外部访问
        ///     13、是否记录变量事件
        ///     14、变量工程单位
        ///     15、变量描述
        /// </summary>
        /// <param name="variable">变量基类</param>
        /// <returns>返回变量属性字符串列表</returns>
        public static List<string> VariableToStrings(this VariableBase variable)
        {
            var tableValueList= new List<string>();
            switch (variable.ValueType)
            {
                case VarValuetype.VarBool:
                case VarValuetype.VarString:
                    tableValueList.AddRange(new[]
                        {
                            variable.Name,
                            variable.AbsolutePath,
                            variable.VariableType.ToString(),
                            variable.ValueType.ToString(),
                            variable.GetInitValue().ToString(),
                            "N/A",
                            "N/A",
                            variable.GetValue().ToString(),
                            "N/A",
                            variable.OperateProperty.ToString(),
                            variable.IsValueSaved.ToString(),
                            variable.IsInitValueSaved.ToString(),
                            variable.IsAddressable.ToString(),
                            variable.IsRecordEvent.ToString(),
                            "N/A",
                            variable.Description
                        });
                    return tableValueList;
                case VarValuetype.VarDouble:
                     tableValueList.AddRange(new[]
                        {
                            variable.Name,
                            variable.AbsolutePath,
                            variable.VariableType.ToString(),
                            variable.ValueType.ToString(),
                            variable.GetInitValue().ToString(),
                            ((AnalogVariable)variable).MinValue.ToString(CultureInfo.InvariantCulture),
                            ((AnalogVariable)variable).MaxValue.ToString(CultureInfo.InvariantCulture),
                            variable.GetValue().ToString(),
                            ((AnalogVariable)variable).DeadBand.ToString(CultureInfo.InvariantCulture),
                            variable.OperateProperty.ToString(),
                            variable.IsValueSaved.ToString(),
                            variable.IsInitValueSaved.ToString(),
                            variable.IsAddressable.ToString(),
                            variable.IsRecordEvent.ToString(),
                            ((AnalogVariable)variable).EngineeringUnit,
                            variable.Description
                        });
                    return tableValueList;
                
                default:
                    return null;
            }
        }

        /// <summary>
        /// 修改变量属性，变量基类扩展方法
        /// </summary>
        /// <param name="variable">变量</param>
        /// <param name="str">修改后的属性列表</param>
        /// <returns>修改成功返回true，修改失败返回false</returns>
        internal static bool EditVariable(this VariableBase variable, List<string> str)
        {
            switch (variable.ValueType)
            {
                case VarValuetype.VarBool:
                    ((DigitalVariable) variable).InitValue = str[4].Substring(0, 1).ToLower() == "t";
                    break;
                case VarValuetype.VarDouble:
                    double deadArea;
                    double initValue;
                    double minValue;
                    double maxValue;
                    if (!double.TryParse(str[4], out initValue) ||
                        !double.TryParse(str[5], out minValue) ||
                        !double.TryParse(str[6], out maxValue) ||
                        !double.TryParse(str[8], out deadArea))
                    {
                        return false;
                    }
                    ((AnalogVariable) variable).InitValue = initValue;
                    ((AnalogVariable) variable).MinValue = minValue;
                    ((AnalogVariable) variable).MaxValue = maxValue;
                    ((AnalogVariable) variable).DeadBand = deadArea;
                    ((AnalogVariable) variable).EngineeringUnit = str[14];
                    break;
                case VarValuetype.VarString:
                    ((TextVariable) variable).InitValue = str[4];
                    break;
            }
            variable.Name = str[0];
            switch (str[2])
            {
                case "VarNormal":
                    variable.VariableType = VarType.VarNormal;
                    break;
                case "VarStruct":
                    variable.VariableType = VarType.VarStruct;
                    break;
                case "VarRef":
                    variable.VariableType = VarType.VarRef;
                    break;
            }
            switch (str[9])
            {
                case "ReadWrite":
                    variable.OperateProperty = VarOperateProperty.ReadWrite;
                    break;
                case "ReadOnly":
                    variable.OperateProperty = VarOperateProperty.ReadOnly;
                    break;
                case "WriteOnly":
                    variable.OperateProperty = VarOperateProperty.WriteOnly;
                    break;
            }
            variable.IsValueSaved = str[10].Substring(0, 1).ToLower() == "t";
            variable.IsInitValueSaved = str[11].Substring(0, 1).ToLower() == "t";
            variable.IsAddressable = str[12].Substring(0, 1).ToLower() == "t";
            variable.IsRecordEvent = str[13].Substring(0, 1).ToLower() == "t";
            variable.Description = str[15];

            return true;

        }

        /// <summary>
        /// 修改变量属性，变量基类扩展方法
        /// </summary>
        /// <param name="variable">变量</param>
        /// <param name="newVariable">修改后的变量属性</param>
        /// <returns>修改成功返回true，修改失败返回false</returns>
        internal static bool EditVariable(this VariableBase variable, VariableBase newVariable)
        {
            return variable.EditVariable(newVariable.VariableToStrings());
        }

    }

    /// <summary>
    /// 对象深拷贝
    /// </summary>
    public static class ObjectCopier
    {
        /// <summary>
        /// Perform a deep Copy of the object.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException(@"The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}