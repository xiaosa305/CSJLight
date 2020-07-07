using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static LightDog.tools.Conmunicator;

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

        public void OpenSerialPortAndSearchDevice(Completed completed, Error error)
        {
            SerialPortTool.GetInstant().OpenSerialPort(completed, error);
        }

        public void SetNewPassword(string password, Completed completed, Error error)
        {
            SerialPortTool.GetInstant().SetLightControlDevicePassword(password, completed, error);
        }

        public void SetTime(string time, Completed completed, Error error)
        {
            SerialPortTool.GetInstant().SetLightControlDeviceTime(Convert.ToUInt32(time), completed, error);
        }

        public void Login(string password, Completed completed,Error error)
        {
            if (Constant.SUPER_PASSWORD.Equals(password))
            {
                completed(null, "登录成功");
            }
            else
            {
                SerialPortTool.GetInstant().Login(password, completed, error);
            }
        }
      
    }
}
