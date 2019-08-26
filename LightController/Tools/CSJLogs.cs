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

        private CSJLogs() { }

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
            string message = ex.StackTrace + ":" + ex.Message + @"\r\n";
            if (!Directory.Exists(LogFileDirector))
            {
                Directory.CreateDirectory(LogFileDirector);
            }
            if (!File.Exists(LogFilePath))
            {
                File.Create(LogFilePath);
            }
            FileStream stream = new FileStream(LogFilePath, FileMode.Append);
            byte[] data = Encoding.Default.GetBytes(@"[ERROR]:\\" + message);
            stream.Write(data, 0, data.Length);
            stream.Close();
        }

        public void DebugLog(string debugStr)
        {
            if (!Directory.Exists(LogFileDirector))
            {
                Directory.CreateDirectory(LogFileDirector);
            }
            if (!File.Exists(LogFilePath))
            {
                File.Create(LogFilePath);
            }
            FileStream stream = new FileStream(LogFilePath, FileMode.Append);
            byte[] data = Encoding.Default.GetBytes(@"[DEBUG]:\\" + debugStr);
            stream.Write(data, 0, data.Length);
            stream.Close();
        }
    }
}
