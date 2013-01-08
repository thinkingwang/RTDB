using System;
using System.Windows.Forms;

namespace SCADA.RTDB.Repository.Test
{
    public partial class PasteMessage : Form
    {
        public PasteMessage(string name)
        {
            InitializeComponent();
            label3.Text = "会将正在移动的变量重命名为 \"" + name + "\"";
        }


        private void PasteMessage_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;//移动并替换
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;//请勿移动
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;//移动，但保留两个文件
            Close();
        }
    }
}
