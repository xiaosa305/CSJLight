using MultiLedController.entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace MultiLedController.utils
{
    public static class XiaosaTest
    {
        private static int Temp = 0;

        /// <summary>
        /// 1.调用Art_Net_Manager:SearchDevice()方法传入本机主IP进行搜索设备
        /// 2.搜索成功才允许软件的其他操作，调用Art_Net_Manager:GetLedControlDevices()获取搜索到的设备目录
        /// 3.根据搜索到的设备数量，获取相应数量的子IP，将设备信息中的SpaceNum与配置的IP实例化一个对应的VirtualControlInfo
        /// 4.将List<VirtualControlInfo>以及麦爵士软件所在设备IP作为入参调用Art_Net_Manager:Start()启动虚拟客户端用于接收麦爵士数据
        /// 5.启动实时调试功能：调用Art_Net_Manager:SendStartDebugOrder()方法进行启动，调用Art_Net_Manager:EndDebug()方法关闭实时调试
        /// </summary>
        public static void Test1()
        {
            switch (Temp)
            {
                case 0:
                case 1:
                default:
                    Thread thread = new Thread(FileTest)
                    {
                        IsBackground = true
                    };
                    thread.Start();
                    break;
            }
            Temp++;
        }

        public static void FileTest()
        {
            using (FileStream fileStream = new FileStream(@"C:\WorkSpace\新建文件夹\SC00.bin", FileMode.Open))
            {
                List<byte> data = new List<byte>();
                Console.WriteLine(fileStream.Length);

                List<byte> Temp = new List<byte>();
                List<byte> Temp1 = new List<byte>();
                for (int i = 0; i < fileStream.Length; i++)
                {
                    data.Add(Convert.ToByte(fileStream.ReadByte()));
                }
                for (int i = 35; i < data.Count; i = i + 3)
                {
                    Temp1.Add(data[i]);
                    Temp1.Add(data[i + 1]);
                    Temp1.Add(data[i + 2]);

                    if (Temp1.Count == 450)
                    {
                        if (i < 500)
                        {
                            Temp.AddRange(Temp1);
                            Temp1.Clear();
                        }
                        else
                        {
                            for (int j = 0; j < 450; j++)
                            {
                                if (Temp[j] != Temp1[j])
                                {
                                    Console.WriteLine("数据不同" + j);
                                }
                            }
                            Temp.Clear();
                            Temp.AddRange(Temp1);
                            Temp1.Clear();
                        }
                    }
                    //Console.WriteLine("XIAOSA Index Is" + i + "             :G:" + data[i] + ",R:" + data[i + 1] + ",B:" + data[i + 2]);
                }
                Console.WriteLine("结束");
            }
        }
    }
}
