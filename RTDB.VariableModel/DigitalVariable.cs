using System;

namespace SCADA.RTDB.VariableModel
{
    public class DigitalVariable : VariableBase
    {
        #region 属性

        /// <summary>
        /// 变量Id
        /// </summary>
        public int DigitalVariableId { get; set; }

        /// <summary>
        /// 变量值
        /// </summary>
        public bool Value { get; set; }

        /// <summary>
        /// 变量初始值
        /// </summary>
        public bool InitValue { get; set; }

        /// <summary>
        /// 变量组
        /// </summary>
        private VariableGroup Parent
        {
            get { return ParentGroup; }
            set { ParentGroup = value; }
        }

        /// <summary>
        /// 变量全名
        /// </summary>
        public override string FullPath
        {
            get
            {
                return ((ParentGroup == null) || (ParentGroup.Parent == null)) ? Name : (ParentGroup.FullPath + "." + Name);
            }
        }

        #endregion

        #region 构造函数

        public DigitalVariable()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="group">变量所属组别的名称,如果是根组，需要传递 "" 值</param>
        /// <param name="varName">变量名</param>
        public DigitalVariable(VariableGroup group, string varName = "")
            : base(varName, Varvaluetype.VarBool)
        {
            if (group == null)
            {
                throw new ArgumentNullException(Resource1.VariableBase_VariableBase_groupIsNull);
            }
            InitValue = false;
            Value = InitValue;
            Parent = group;
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
            var variable = source as DigitalVariable;
            if (variable != null)
            {
                Value = variable.Value;
                InitValue = variable.InitValue;
                //Parent = source.ParentGroup;
            }
        }

        #endregion

    }
}