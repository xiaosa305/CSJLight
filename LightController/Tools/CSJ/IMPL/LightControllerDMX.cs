using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools.CSJ.IMPL
{
    public class LightControllerDMX
    {
        public int[][] DmxData { get; set; }
        public static LightControllerDMX Build(byte[] data)
        {
            try
            {
                if (data.Length < 467)
                {
                    return null;
                }
                LightControllerDMX light = new LightControllerDMX();
                light.DmxData = new int[17][];
                int point = 400;
                for (int sceneIndex = 0; sceneIndex < 17; sceneIndex++)
                {
                    light.DmxData[sceneIndex][0] = data[point + sceneIndex * 4 + 0];
                    light.DmxData[sceneIndex][1] = data[point + sceneIndex * 4 + 1];
                    light.DmxData[sceneIndex][2] = data[point + sceneIndex * 4 + 2];
                    light.DmxData[sceneIndex][3] = data[point + sceneIndex * 4 + 3];
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
                if (DmxData == null )
                {
                    return null;
                }
                List<byte> data = new List<byte>();
                for (int sceneIndex = 0; sceneIndex < 17; sceneIndex++)
                {
                    data.Add(Convert.ToByte(DmxData[sceneIndex][0]));
                    data.Add(Convert.ToByte(DmxData[sceneIndex][1]));
                    data.Add(Convert.ToByte(DmxData[sceneIndex][2]));
                    data.Add(Convert.ToByte(DmxData[sceneIndex][3]));
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
