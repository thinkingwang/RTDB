using System;
using System.Diagnostics;

namespace Variable
{
    public class AnalogVar : VariableBase
    {
        /// <summary>
        /// 死区,变量最小的变化幅度
        /// </summary>
        public double DeadArea { get; set; }

        /// <summary>
        /// 变量初始值
        /// </summary>
        public double InitValue { get; set; }

        /// <summary>
        /// 变量最小值
        /// </summary>
        public double MinValue { get; set; }

        /// <summary>
        /// 变量最大值
        /// </summary>
        public double MaxValue { get; set; }

        /// <summary>
        /// 工程单位
        /// </summary>
        public string ProjectUnit { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="groupPath">变量所属组别的名称,如果是根组，需要传递 "" 值</param>
        /// <param name="varName">变量名</param>
        public AnalogVar(string groupPath, string varName = "")
            : base(groupPath, varName, VARVALUETYPE.VarDouble)
        {
            DeadArea = 0;
            InitValue = 0;
            MaxValue = 100;
            MinValue = 20;
            ProjectUnit = "";

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
            var variable = source as AnalogVar;
            if (variable != null)
            {
                DeadArea = variable.DeadArea;
                InitValue = variable.InitValue;
                MinValue = variable.MinValue;
                MaxValue = variable.MaxValue;
                ProjectUnit = variable.ProjectUnit;
            }
        }
    }
}