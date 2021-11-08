using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DimmerSystem.Xiaosa.Entity
{
    public class LightControllerSCR
    {
        public int[,] ScrData { get; set; }
        public bool IsStartSCR { get; set; }
        public static LightControllerSCR Build(byte[] data)
        {
            try
            {
                if (data.Length < 467)
                {
                    return null;
                }
                LightControllerSCR light = new LightControllerSCR();
                light.IsStartSCR = data[61] != 0;
                light.ScrData = new int[17,4];
                int point = 400;
                for (int sceneIndex = 0; sceneIndex < 17; sceneIndex++)
                {
                    light.ScrData[sceneIndex,0] = data[point + sceneIndex * 4 + 0] == Convert.ToByte(0xFF) ? 0 : data[point + sceneIndex * 4 + 0];
                    light.ScrData[sceneIndex,1] = data[point + sceneIndex * 4 + 1] == Convert.ToByte(0xFF) ? 0 : data[point + sceneIndex * 4 + 1];
                    light.ScrData[sceneIndex,2] = data[point + sceneIndex * 4 + 2] == Convert.ToByte(0xFF) ? 0 : data[point + sceneIndex * 4 + 2];
                    light.ScrData[sceneIndex,3] = data[point + sceneIndex * 4 + 3] == Convert.ToByte(0xFF) ? 0 : data[point + sceneIndex * 4 + 3];
                }
                return light;
            }
            catch (Exception ex)
            {
                Console.WriteLine("读取灯控调光数据失败\r\n" + ex.StackTrace + "\r\n" + ex.Message);
            }
            return null;
        }
        public byte[] GetData()
        {
            try
            {
                if (ScrData == null )
                {
                    return null;
                }
                List<byte> data = new List<byte>();
                for (int sceneIndex = 0; sceneIndex < 17; sceneIndex++)
                {
                    data.Add(Convert.ToByte(ScrData[sceneIndex,0]));
                    data.Add(Convert.ToByte(ScrData[sceneIndex,1]));
                    data.Add(Convert.ToByte(ScrData[sceneIndex,2]));
                    data.Add(Convert.ToByte(ScrData[sceneIndex,3]));
                }
                return data.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine("生成灯控调光数据失败\r\n" + ex.StackTrace + "\r\n" + ex.Message);
            }
            return new byte[68];
        }
    }
}
