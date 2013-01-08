using System;

namespace SCADA.RTDB.VariableModel
{
    public class DigitalVariable : VariableBase
    {
        #region 属性

        /// <summary>
        /// 变量Id
        /// </summary>
        public int DigitalVariableId
        {
            get { return UniqueId & 0x0FFFFFFF; }
            set { UniqueId = (value | ((int)VarValuetype.VarBool << 28)); }
        }
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
        /// 变量绝对路径
        /// </summary>
        public override string AbsolutePath
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
            : base(varName, VarValuetype.VarBool)
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

    }
}