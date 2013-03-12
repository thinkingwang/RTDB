using System.Windows.Forms;

namespace SCADA.RTDB.Repository.Test
{
    /// <summary>
    /// DataGridView的单元格Button扩展类
    /// </summary>
    public sealed class DataGridViewCellButton : Button
    {
        /// <summary>
        /// 选择的单元格
        /// </summary>
        public DataGridViewCellEventArgs Cell { get; private set; }

        /// <summary>
        /// 选择的单元格标题
        /// </summary>
        public string HeaderText { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="e">选中的单元格</param>
        /// <param name="headerText">选择的单元格标题</param>
        public DataGridViewCellButton(DataGridViewCellEventArgs e, string headerText)
        {
            Cell = e;
            HeaderText = headerText;
            Text = "...";//设置button文字   
            Font = new System.Drawing.Font("Arial", 7);//设置文字格式   
            Visible = true;//设置控件允许显示
        }
    }
}