using System;
using System.Windows.Forms;

namespace SCADA.RTDB.Repository.Test
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PasteMessage : Form
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public PasteMessage(string name)
        {
            InitializeComponent();
            label3.Text = "会将正在移动的变量重命名为 \"" + name + "\"";
        }
        
        private void Button1Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;//移动并替换
            Close();
        }

        private void Button2Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;//请勿移动
            Close();
        }

        private void Button3Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;//移动，但保留两个文件
            Close();
        }
    }
}
