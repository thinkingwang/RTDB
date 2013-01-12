using System;
using System.Collections.Generic;
using System.Globalization;
using SCADA.RTDB.VariableModel;

namespace SCADA.RTDB.EntityFramework
{
    public static class VariableExtend
    {
        /// <summary>
        /// 创建变量，变量基类扩展方法
        /// </summary>
        /// <param name="variable">变量基类</param>
        /// <param name="variableGroup">新建变量的变量组</param>
        /// <returns>返回新建的变量，失败则返回null</returns>
        public static VariableBase CreatVariable(this VariableBase variable, VariableGroup variableGroup)
        {
            switch (variable.ValueType)
            {
                case VarValuetype.VarBool:
                    return new DigitalVariable(variableGroup);
                case VarValuetype.VarDouble:
                    return new AnalogVariable(variableGroup);
                case VarValuetype.VarString:
                    return new TextVariable(variableGroup);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取变量初始值，变量基类扩展方法
        /// </summary>
        /// <param name="variable">变量基类</param>
        /// <returns>返回变量初始值，获取失败则返回null</returns>
        public static object GetInitValue(this VariableBase variable)
        {
            switch (variable.ValueType)
            {
                case VarValuetype.VarBool:
                    return ((DigitalVariable)variable).InitValue;
                case VarValuetype.VarDouble:
                    return ((AnalogVariable) variable).InitValue;
                case VarValuetype.VarString:
                    return ((TextVariable)variable).InitValue;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取变量值，变量基类扩展方法
        /// </summary>
        /// <param name="variable">变量基类</param>
        /// <returns>返回变量值，获取失败则返回null</returns>
        public static object GetValue(this VariableBase variable)
        {
            switch (variable.ValueType)
            {
                case VarValuetype.VarBool:
                    return ((DigitalVariable)variable).Value;
                case VarValuetype.VarDouble:
                    return ((AnalogVariable)variable).Value;
                case VarValuetype.VarString:
                    return ((TextVariable)variable).Value;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 设置变量值
        /// </summary>
        /// <param name="variable">变量</param>
        /// <param name="value">变量值</param>
        /// <returns>设置成功返回ture，失败返回false，失败原因为数据类型不匹配</returns>
        public static bool SetValue(this VariableBase variable, object value)
        {
            try
            {
                //变量为只读时，不允许写入
                if (variable.OperateProperty == VarOperateProperty.ReadOnly)
                {
                    return true;
                }
                switch (variable.ValueType)
                {
                    case VarValuetype.VarBool:
                        ((DigitalVariable)variable).Value = Convert.ToBoolean(value);
                        break;
                    case VarValuetype.VarDouble:
                        ((AnalogVariable)variable).Value = Convert.ToDouble(value);
                        break;
                    case VarValuetype.VarString:
                        ((TextVariable)variable).Value = Convert.ToString(value);
                        break;
                }
                return true;
            }
            catch
            {
                return false;
            }
            
        }

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
        public static bool EditVariable(this VariableBase variable, List<string> str)
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
        public static bool EditVariable(this VariableBase variable, VariableBase newVariable)
        {
            return variable.EditVariable(newVariable.VariableToStrings());
        }

    }
}