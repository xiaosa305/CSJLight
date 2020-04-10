using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LightController.Utils
{
    public class LogTools
    {
        private const string BORDER_UP = "";
        private const string BORDER_DOWN = "";
        private LogTools() { }

        public static void Debug(string tag,string debugInfo)
        {
            MethodBase method = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            Console.WriteLine(BORDER_UP);
            Console.WriteLine(tag + "：" + DateTime.Now.TimeOfDay + "    " + "调试信息");
            Console.WriteLine(tag + "：类名==>" + method.DeclaringType.FullName);
            Console.WriteLine(tag + "：方法名==>：" + method.Name);
            Console.WriteLine(tag + "：调试信息==>" + debugInfo);
            Console.WriteLine(BORDER_DOWN);
        }

        public static void Error(string tag ,string errorInfo, Exception exception)
        {
            MethodBase method = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            Console.WriteLine(BORDER_UP);
            Console.WriteLine(tag + "：" + DateTime.Now.TimeOfDay + "    " + "错误信息");
            Console.WriteLine(tag + "：类名==>" + method.DeclaringType.FullName);
            Console.WriteLine(tag + "：方法名==>：" + method.Name);
            Console.WriteLine(tag + "：错误类型==>" + exception.GetType());
            Console.WriteLine(tag + "：错误信息==>" + exception.Message);
            Console.WriteLine(BORDER_DOWN);
        }
        public static void Error(string tag, string errorInfo, Exception exception, bool isThrow, string throwInfo)
        {
            MethodBase method = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            Console.WriteLine(BORDER_UP);
            Console.WriteLine(tag + "：" + DateTime.Now.TimeOfDay + "    " + "错误信息");
            Console.WriteLine(tag + "：类名==>" + method.DeclaringType.FullName);
            Console.WriteLine(tag + "：方法名==>：" + method.Name);
            Console.WriteLine(tag + "：错误类型==>" + exception.GetType());
            Console.WriteLine(tag + "：错误信息==>" + exception.Message);
            Console.WriteLine(BORDER_DOWN);
            if (isThrow)
            {
                throw new Exception(throwInfo);
            }
        }
    }
}
