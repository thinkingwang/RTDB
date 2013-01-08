using System;

namespace SCADA.RTDB.VariableModel
{
    public class TextVariable : VariableBase
    {
        #region 属性

        /// <summary>
        /// 变量Id
        /// </summary>
        public int TextVariableId
        {
            get { return UniqueId & 0x0FFFFFFF; }
            set { UniqueId = (value | ((int)VarValuetype.VarString << 28)); }
        }

        /// <summary>
        /// 变量值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 变量初始值
        /// </summary>
        public string InitValue { get; set; }

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
        public override  string AbsolutePath
        {
            get
            {
                return ((ParentGroup == null) || (ParentGroup.Parent == null)) ? Name : (ParentGroup.FullPath + "." + Name);
            }
        }

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
            : base(varName, VarValuetype.VarString)
        {
            InitValue = "";
            Value = InitValue;
            Parent = group;
        }

        #endregion

    }
}