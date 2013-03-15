using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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
        public static Dictionary<string, string> VariableToDictionary(this VariableBase variable)
        {
            var t = variable.GetType();
            var props = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            return props.ToDictionary(prop => prop.Name.ToLower(), prop => prop.GetValue(variable, null) != null ? prop.GetValue(variable, null).ToString() : string.Empty);
        }
    }

}