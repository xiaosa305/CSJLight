using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightDog.tools
{
    public class Test
    {
        private static Test Instance { get; set; }
        private Test() { SerialPortTool.GetInstant().SetOldPassword("88888888"); }

        public static Test GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Test();
            }
            return Instance;
        }

        public void OpenSerialPortAndSearchDevice()
        {
            SerialPortTool.GetInstant().OpenSerialPort(this.Completed,this.Error);
        }

        public void SetNewPassword(string password)
        {
            SerialPortTool.GetInstant().SetLightControlDevicePassword(password, this.Completed, this.Error);
        }

        public void SetTime(string time)
        {
            SerialPortTool.GetInstant().SetLightControlDeviceTime(Convert.ToUInt32(time), this.Completed, this.Error);
        }

        public void Login(string password)
        {
            if (Constant.SUPER_PASSWORD.Equals(password))
            {
                this.Completed(null, "登录成功");
            }
            else
            {
                SerialPortTool.GetInstant().Login(password, this.Completed, this.Error);
            }
            SerialPortTool.GetInstant().Login(password, this.Completed, this.Error);
        }

        public void Completed(Object obj,string msg)
        {
            Console.WriteLine("完成：" + msg);
        }

        public void Error(Object obj,string msg)
        {
            Console.WriteLine("失败：" + msg);
        }
    }
}
