using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace LightController.Tools
{
    public class CSJLogs
    {
        private static CSJLogs Instance { get; set; }
        private string FileName { get; set; }
        private const string LogFileDirector = @"C:\Temp\LightLog";
        private FileStream Stream { get; set; }

        private CSJLogs()
        {
            if (!Directory.Exists(LogFileDirector))
            {
                Directory.CreateDirectory(LogFileDirector);
            }
            string year = DateTime.Now.Year.ToString();
            string month = string.Empty;
            string day = string.Empty;
            if (DateTime.Now.Month < 10)
            {
                month = "0";
            }
            month += DateTime.Now.Month.ToString(); ;
            if (DateTime.Now.Day < 10)
            {
                day = "0";
            }
            day += DateTime.Now.Day.ToString();
            FileName = year + month + day + ".ini";
            Stream = new FileStream(LogFileDirector + @"\" + FileName, FileMode.Append);

        }

        public static CSJLogs GetInstance()
        {
            if (Instance == null)
            {
                Instance = new CSJLogs();
            }
            return Instance;
        }

        public void ErrorLog(Exception ex)
        {
            string message = ex.StackTrace + ":" + ex.Message + "\r\n";
            byte[] data = Encoding.Default.GetBytes(@"[ERROR]" + DateTime.Now + ":" + message);
            Stream.Write(data, 0, data.Length);
            Stream.Flush();
        }

        public void ErrorLog(Exception ex,string errorStr)
        {
            string message = ex.StackTrace + ":" + ex.Message + "\r\n" + errorStr + "\r\n";
            byte[] data = Encoding.Default.GetBytes(@"[ERROR]" + DateTime.Now + ":" + message);
            Stream.Write(data, 0, data.Length);
            Stream.Flush();
        }

        public void DebugLog(string debugStr)
        {
            byte[] data = Encoding.Default.GetBytes(@"[DEBUG]" + DateTime.Now + ":" + debugStr + "\r\n");
            Stream.Write(data, 0, data.Length);
            Stream.Flush();
        }
    }
}
