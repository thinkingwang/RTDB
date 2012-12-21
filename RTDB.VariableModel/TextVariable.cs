using System;

namespace SCADA.RTDB.VariableModel
{
    public class TextVariable : VariableBase
    {
        #region 属性

        /// <summary>
        /// 变量Id
        /// </summary>
        public int TextVariableId { get; set; }

        /// <summary>
        /// 变量值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 变量初始值
        /// </summary>
        public string InitValue { get; set; }

        #endregion

        #region 构造函数

        public TextVariable()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="group">变量所属组别的名称,如果是根组，需要传递 "" 值</param>
        /// <param name="varName">变量名</param>
        public TextVariable(VariableGroup group, string varName = "")
            : base(group, varName, Varvaluetype.VarString)
        {
            InitValue = "";
            Value = InitValue;
        }

        #endregion

        #region 复制变量

        /// <summary>
        /// 拷贝属性
        /// </summary>
        /// <param name="source">源变量对象实例</param>
        public override void CopyProperty(VariableBase source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(Resource1.CopyProperty_SourceObjIsNull);
            }
            base.CopyProperty(source);
            var variable = source as TextVariable;
            if (variable != null)
            {
                Value = variable.Value;
                InitValue = variable.InitValue;
            }
        }

        #endregion

    }
}