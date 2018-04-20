using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Can_Test
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);// ThreadException 这个异常事件，注册方法Application_ThreadException
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());            
        }

        //static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        //{
        //    MessageBox.Show("b");
        //    LogUnhandledException(e.ExceptionObject);
        //}

        //static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        //{
        //    MessageBox.Show("a");
        //    LogUnhandledException(e.Exception);
        //}

        static void LogUnhandledException(object exceptionobj)
        {
            //Log the exception here or report it to developer
            MessageBox.Show(exceptionobj.ToString());
        }
    }
}

