using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Variable
{

    public class VariableRepository
    {
        #region 变量集合

        /// <summary>
        /// 模拟变量集合
        /// </summary>
        private static readonly Dictionary<string, AnalogVar> AnalogSet = new Dictionary<string, AnalogVar>();

        /// <summary>
        /// 数字变量集合
        /// </summary>
        private static readonly Dictionary<string, DigitalVar> DigitalSet = new Dictionary<string, DigitalVar>();

        /// <summary>
        /// 字符变量集合
        /// </summary>
        private static readonly Dictionary<string, StringVar> StringSet = new Dictionary<string, StringVar>();

        #endregion

        /// <summary>
        /// 增加指定变量
        /// </summary>
        /// <param name="variable">指定变量</param>
        public static void AddVar(VariableBase variable)
        {
            if (variable == null)
            {
                Debug.Assert(Resource1.VariableRepository_AddVar_VariableIsNull != null, 
                    "Resource1.VariableRepository_AddVar_VariableIsNull != null");
                throw new ArgumentNullException(Resource1.VariableRepository_AddVar_VariableIsNull);
            }
            if (variable.VarValueType == VARVALUETYPE.VarBool)
            {
                if (DigitalSet.ContainsKey(variable.VariableFullName))
                {
                    Debug.Assert(Resource1.VariableRepository_AddVar_VariableIsExist != null, 
                        "Resource1.VariableRepository_AddVar_VariableIsExist != null");
                    throw new Exception(Resource1.VariableRepository_AddVar_VariableIsExist);
                }
                DigitalSet.Add(variable.VariableFullName, variable as DigitalVar);
            }
            else if (variable.VarValueType == VARVALUETYPE.VarDouble)
            {
                if (AnalogSet.ContainsKey(variable.VariableFullName))
                {
                    Debug.Assert(Resource1.VariableRepository_AddVar_VariableIsExist != null,
                        "Resource1.VariableRepository_AddVar_VariableIsExist != null");
                    throw new Exception(Resource1.VariableRepository_AddVar_VariableIsExist);
                }
                AnalogSet.Add(variable.VariableFullName, variable as AnalogVar);
            }
            else
            {
                if (StringSet.ContainsKey(variable.VariableFullName))
                {
                    Debug.Assert(Resource1.VariableRepository_AddVar_VariableIsExist != null,
                        "Resource1.VariableRepository_AddVar_VariableIsExist != null");
                    throw new Exception(Resource1.VariableRepository_AddVar_VariableIsExist);
                }
                StringSet.Add(variable.VariableFullName, variable as StringVar);
            }

        }

        /// <summary>
        /// 更新指定变量
        /// </summary>
        /// <param name="varObj">指定变量</param>
        public static void EditVar(VariableBase variable)
        {
            if (variable == null)
            {
                Debug.Assert(Resource1.VariableRepository_AddVar_VariableIsNull != null,
                    "Resource1.VariableRepository_AddVar_VariableIsNull != null");
                throw new ArgumentNullException(Resource1.VariableRepository_AddVar_VariableIsNull);
            }
            if (variable.VarValueType == VARVALUETYPE.VarBool)
            {
                if (DigitalSet.ContainsKey(variable.VariableFullName))
                {
                    DigitalSet[variable.VariableFullName].CopyProperty(variable);
                }
            }
            else if (variable.VarValueType == VARVALUETYPE.VarDouble)
            {
                if (AnalogSet.ContainsKey(variable.VariableFullName))
                {
                    AnalogSet[variable.VariableFullName].CopyProperty(variable);
                }
            }
            else
            {
                if (StringSet.ContainsKey(variable.VariableFullName))
                {
                    StringSet[variable.VariableFullName].CopyProperty(variable);
                }
            }
            
        }

        /// <summary>
        /// 删除指定变量
        /// </summary>
        /// <param name="variable">指定变量</param>
        public static void RemoveVar(VariableBase variable)
        {
            if (variable == null)
            {
                Debug.Assert(Resource1.VariableRepository_AddVar_VariableIsNull != null,
                    "Resource1.VariableRepository_AddVar_VariableIsNull != null");
                throw new ArgumentNullException(Resource1.VariableRepository_AddVar_VariableIsNull);
            }
            if (variable.VarValueType == VARVALUETYPE.VarDouble)
            {
                AnalogSet.Remove(variable.VarName);
            }
            else if (variable.VarValueType == VARVALUETYPE.VarBool)
            {
                DigitalSet.Remove(variable.VarName);
            }
            else if (variable.VarValueType == VARVALUETYPE.VarDouble)
            {
                StringSet.Remove(variable.VarName);
            }

            //删除变量组中的变量
            VariableGroup varGroup = VariableGroup.GetGroup(variable.GroupID);
            varGroup.RemoveVariable(variable.VarName);
        }
        
    }
}
