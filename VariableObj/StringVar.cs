using System;
using System.Diagnostics;

namespace Variable
{
    public class StringVar : VariableBase
    {
        /// <summary>
        /// 变量初始值
        /// </summary>
        public string InitValue;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="groupPath">变量所属组别的名称,如果是根组，需要传递 "" 值</param>
        /// <param name="varName">变量名</param>
        public StringVar(string groupPath, string varName = "")
            : base(groupPath, varName, VARVALUETYPE.VarString)
        {
            InitValue = "";
        }

        /// <summary>
        /// 拷贝属性
        /// </summary>
        /// <param name="source">源变量对象实例</param>
        public override void CopyProperty(VariableBase source)
        {
            if (source == null)
            {
                Debug.Assert(Resource1.AnalogVar_CopyProperty_SourceObjIsNull != null, "Resource1.CopyProperty_SourceObjIsNull != null");
                throw new ArgumentNullException(Resource1.AnalogVar_CopyProperty_SourceObjIsNull);
            }
            base.CopyProperty(source);
            var variable = source as StringVar;
            if (variable != null)
            {
                InitValue = variable.InitValue;
            }
        }
    }
}