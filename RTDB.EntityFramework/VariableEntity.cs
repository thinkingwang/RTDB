using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using RTDB.Common;
using RTDB.VariableModel;

namespace RTDB.EntityFramework
{
    public class VariableEntity : IVariableContext
    {
        /// <summary>
        /// 模拟变量集合
        /// </summary>
        private static readonly Dictionary<string, AnalogVariable> AnalogVarSet = new Dictionary<string, AnalogVariable>();

        /// <summary>
        /// 数字变量集合
        /// </summary>
        private static readonly Dictionary<string, DigitalVariable> DigitalVarSet = new Dictionary<string, DigitalVariable>();

        /// <summary>
        /// 字符变量集合
        /// </summary>
        private static readonly Dictionary<string, StringVariable> StringVarSet = new Dictionary<string, StringVariable>();

        /// <summary>
        /// 变量组集合
        /// </summary>
        private static readonly List<VariableGroup> VarGroupColletion = new List<VariableGroup>();


        public Dictionary<string, AnalogVariable> AnalogSet
        {
            get { return AnalogVarSet; }
        }

        public Dictionary<string, DigitalVariable> DigitalSet
        {
            get { return DigitalVarSet; }
        }

        public Dictionary<string, StringVariable> StringSet
        {
            get { return StringVarSet; }
        }

        public List<VariableGroup> VariableGroupSet
        {
            get { return VarGroupColletion; }
        }
    }
}
