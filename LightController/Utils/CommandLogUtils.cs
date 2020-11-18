using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;

namespace LightController.Utils
{
    public class CommandLogUtils
    {
        private readonly bool LOG_STATUS = true;
        private readonly string FILEPATH = Application.StartupPath + @"\command.log";
        private static CommandLogUtils Instance { get; set; }
        private ConcurrentQueue<string> MsgQueue { get; set; }
        private System.Timers.Timer ReadWorking { get; set; }

        private CommandLogUtils()
        {
            this.MsgQueue = new ConcurrentQueue<string>();
            this.Init();
            this.ReadWorking = new System.Timers.Timer(30) { AutoReset = true};
            this.ReadWorking.Elapsed += this.ReadMsg;
            this.ReadWorking.Start();
        }

        private void Init()
        {
            if (File.Exists(FILEPATH))
            {
                File.Delete(FILEPATH);
            }
            File.Create(FILEPATH).Dispose();
        }

        public static CommandLogUtils GetInstance()
        {
            if (Instance == null)
            {
                Instance = new CommandLogUtils();
            }
            return Instance;
        }

        public bool Enqueue(string msg)
        {
            bool result = false;
            try
            {
                lock (this.MsgQueue)
                {
                    this.MsgQueue.Enqueue(msg);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return result;
        }

        private void ReadMsg(object sender, ElapsedEventArgs e)
        {
            string msg = "";
            bool flag = false;
            lock (this.MsgQueue)
            {
                if (this.MsgQueue.Count > 0)
                {
                    this.MsgQueue.TryDequeue(out msg);
                    flag = true;
                }
            }
            if (flag)
            {
                using (FileStream stream = new FileStream(FILEPATH, FileMode.Append))
                {
                    msg = DateTime.Now.Date + "::::" + msg;
                    byte[] buff = Encoding.Default.GetBytes(msg);
                    stream.Write(buff, 0, buff.Length);
                }
            }
        }
    }
}
