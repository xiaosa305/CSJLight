using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    public class CSJLogs
    {
        private static CSJLogs Instance { get; set; }
        private const string LogFilePath = @"C:\Temp\LightLog\CSJLOG.ini";
        private const string LogFileDirector = @"C:\Temp\LightLog";
        private FileStream Stream { get; set; }

        private CSJLogs()
        {
            if (!Directory.Exists(LogFileDirector))
            {
                Directory.CreateDirectory(LogFileDirector);
            }
            if (!File.Exists(LogFilePath))
            {
                File.Create(LogFilePath);
            }
            Stream = new FileStream(LogFilePath, FileMode.Append);
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
            byte[] data = Encoding.Default.GetBytes(@"[ERROR]:" + message);
            Stream.Write(data, 0, data.Length);
        }

        public void DebugLog(string debugStr)
        {
            byte[] data = Encoding.Default.GetBytes(@"[DEBUG]:" + debugStr + "\r\n");
            Stream.Write(data, 0, data.Length);
        }
    }
}
