using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Wbs.Everdigm.Desktop
{
    static class Program
    {
        /// <summary>
        /// 程序启动与否
        /// </summary>
        public static EventWaitHandle ProgramStarted;
        private static string ewhName = "Wbs.Everdigm.Desktop.WaitHandle";
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 尝试创建一个命名事件  
            bool createNew;
            ProgramStarted = new EventWaitHandle(false, EventResetMode.AutoReset, ewhName, out createNew);
            // 如果该命名事件已经存在(存在有前一个运行实例)，则发事件通知并退出  
            if (!createNew)
            {
                MessageBox.Show("There has another instance still running.\nIf that one is not opened by you, please contact the administrator.", 
                    "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ProgramStarted.Set();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
