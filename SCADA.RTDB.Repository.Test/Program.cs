using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SCADA.RTDB.Repository.Test;

namespace SCADA.RTDB.Repository.Test
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FunctionTestForm());
            }
            catch(ArgumentException es)
            {
                MessageBox.Show(es.ToString());
            }
            
        }
    }
}
