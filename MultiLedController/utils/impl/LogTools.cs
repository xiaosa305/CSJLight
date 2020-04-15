using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MultiLedController.utils.impl
{
    public class LogTools
    {
        private static readonly string LogFileDirectPath = Application.StartupPath + @"\Log\";
        private const string FileKey = "TRANSJOY";
        private const string BORDER_UP = "";
        private const string BORDER_DOWN = "";
        private static readonly Object Key = new object();
        private LogTools() { }
        public static void Debug(string tag, string debugInfo)
        {
            MethodBase method = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(BORDER_UP);
            stringBuilder.AppendLine(tag + "：" + DateTime.Now.TimeOfDay + "    " + "<调试信息>");
            stringBuilder.AppendLine(tag + "：类名==>         " + method.DeclaringType.FullName);
            stringBuilder.AppendLine(tag + "：方法名==>       " + method.Name);
            stringBuilder.AppendLine(tag + "：线程编号==>     " + Thread.CurrentThread.ManagedThreadId);
            stringBuilder.AppendLine(tag + "：调试信息==>     " + debugInfo);
            stringBuilder.AppendLine(tag + "：" + DateTime.Now.TimeOfDay.ToString() + "    " + "</调试信息>");
            stringBuilder.AppendLine(BORDER_DOWN);
            stringBuilder.AppendLine(BORDER_DOWN);
            Console.WriteLine(stringBuilder);
            if (Constant.IsLogInFile)
            {
                WriteLogToFile(stringBuilder.ToString());
            }
        }
        public static void Error(string tag, string errorInfo, Exception exception)
        {
            MethodBase method = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(BORDER_UP);
            stringBuilder.AppendLine(tag + "：" + DateTime.Now.TimeOfDay + "    " + "<错误信息>");
            stringBuilder.AppendLine(tag + "：类名==>         " + method.DeclaringType.FullName);
            stringBuilder.AppendLine(tag + "：方法名==>       " + method.Name);
            stringBuilder.AppendLine(tag + "：线程编号==>     " + Thread.CurrentThread.ManagedThreadId);
            stringBuilder.AppendLine(tag + "：错误描述==>     " + errorInfo);
            stringBuilder.AppendLine(tag + "：错误类型==>     " + exception.GetType());
            stringBuilder.AppendLine(tag + "：错误信息==>     " + exception.Message);
            stringBuilder.AppendLine(tag + "：报错所在位置==> " + exception.StackTrace);
            stringBuilder.AppendLine(tag + "：" + DateTime.Now.TimeOfDay.ToString() + "    " + "</错误信息>");
            stringBuilder.AppendLine(BORDER_DOWN);
            stringBuilder.AppendLine(BORDER_DOWN);
            Console.WriteLine(stringBuilder);
            if (Constant.IsLogInFile)
            {
                WriteLogToFile(stringBuilder.ToString());
            }
        }
        public static void Error(string tag, string errorInfo, Exception exception, bool isThrow, string throwInfo)
        {
            MethodBase method = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(BORDER_UP);
            stringBuilder.AppendLine(tag + "：" + DateTime.Now.TimeOfDay + "    " + "<错误信息>");
            stringBuilder.AppendLine(tag + "：类名==>         " + method.DeclaringType.FullName);
            stringBuilder.AppendLine(tag + "：方法名==>       " + method.Name);
            stringBuilder.AppendLine(tag + "：线程编号==>     " + Thread.CurrentThread.ManagedThreadId);
            stringBuilder.AppendLine(tag + "：错误描述==>     " + errorInfo);
            stringBuilder.AppendLine(tag + "：错误类型==>     " + exception.GetType());
            stringBuilder.AppendLine(tag + "：错误信息==>     " + exception.Message);
            stringBuilder.AppendLine(tag + "：报错所在位置==> " + exception.StackTrace);
            stringBuilder.AppendLine(tag + "：" + DateTime.Now.TimeOfDay.ToString() + "    " + "</错误信息>");
            stringBuilder.AppendLine(BORDER_DOWN);
            stringBuilder.AppendLine(BORDER_DOWN);
            Console.WriteLine(stringBuilder);
            if (Constant.IsLogInFile)
            {
                WriteLogToFile(stringBuilder.ToString());
            }
            if (isThrow)
            {
                throw new Exception(throwInfo);
            }
        }
        private static void WriteLogToFile(string logInfo)
        {
            lock (Key)
            {
                try
                {
                    string dirPath = LogFileDirectPath + DateTime.Now.Month;
                    Directory.CreateDirectory(dirPath);
                    string logFilePath = dirPath + @"\" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + @".log";
                    using (FileStream fileStream = File.OpenWrite(logFilePath))
                    {
                        byte[] infoBuff = Encoding.Default.GetBytes(logInfo);
                        fileStream.Write(infoBuff, 0, infoBuff.Length);
                        fileStream.Flush();
                    }
                }
                catch (Exception exception)
                {
                    MethodBase method = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine(BORDER_UP);
                    stringBuilder.AppendLine(Constant.TAG_XIAOSA + "：" + DateTime.Now.TimeOfDay + "    " + "<错误信息>");
                    stringBuilder.AppendLine(Constant.TAG_XIAOSA + "：类名==>         " + method.DeclaringType.FullName);
                    stringBuilder.AppendLine(Constant.TAG_XIAOSA + "：方法名==>       " + method.Name);
                    stringBuilder.AppendLine(Constant.TAG_XIAOSA + "：线程编号==>     " + Thread.CurrentThread.ManagedThreadId);
                    stringBuilder.AppendLine(Constant.TAG_XIAOSA + "：错误描述==>     " + "日志信息写入日志文件出错");
                    stringBuilder.AppendLine(Constant.TAG_XIAOSA + "：错误类型==>     " + exception.GetType());
                    stringBuilder.AppendLine(Constant.TAG_XIAOSA + "：错误信息==>     " + exception.Message);
                    stringBuilder.AppendLine(Constant.TAG_XIAOSA + "：报错所在位置==> " + exception.StackTrace);
                    stringBuilder.AppendLine(Constant.TAG_XIAOSA + "：" + DateTime.Now.TimeOfDay.ToString() + "    " + "</错误信息>");
                    stringBuilder.AppendLine(BORDER_DOWN);
                    stringBuilder.AppendLine(BORDER_DOWN);
                    Console.WriteLine(stringBuilder);
                }
            }
        }
    }
}
