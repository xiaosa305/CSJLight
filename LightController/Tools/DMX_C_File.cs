using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    public class DMX_C_File
    {
        public int SenceNo { get; set; }
        public DMX_C_Data Data { get; set; }

        private byte[] GetByteData()
        {
            IList<byte> fileData = new List<byte>();
            byte Ram_Enabl = Convert.ToByte(Data.HeadData.MICSensor);
            byte Ram_Response_Time = Convert.ToByte(Data.HeadData.SenseFreq);
            byte Ram_Play_Time = Convert.ToByte(Data.HeadData.RunTime);
            byte[] Scene_Total_Count = new byte[2];
            Scene_Total_Count[0] = (byte)(Data.HeadData.ChanelCount & 0xFF);
            Scene_Total_Count[1] = (byte)((Data.HeadData.ChanelCount >> 8) & 0xFF);
            int FileSize = 0;
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Ram_Enabl);
            fileData.Add(Ram_Response_Time);
            fileData.Add(Ram_Play_Time);
            fileData.Add(Scene_Total_Count[0]);
            fileData.Add(Scene_Total_Count[1]);
            foreach (C_Data c_Data in Data.Datas)
            {
                //转换通道编号为byte
                byte[] chanelNo = new byte[2];
                chanelNo[0] = (byte)(c_Data.ChanelNo & 0xFF);
                chanelNo[1] = (byte)((c_Data.ChanelNo >> 8) & 0xFF);
                //添加两字节通道编号
                fileData.Add(chanelNo[0]);
                fileData.Add(chanelNo[1]);
                //添加所有采样数据
                foreach (int value in c_Data.Datas)
                {
                    //转换采样数据为byte
                    byte bValue = Convert.ToByte(value);
                    //添加采样数据
                    fileData.Add(bValue);
                }
            }
            FileSize = fileData.Count();
            fileData[0] = (byte)(FileSize & 0xFF);
            fileData[1] = (byte)((FileSize >> 8) & 0xFF);
            fileData[2] = (byte)((FileSize >> 16) & 0xFF);
            fileData[3] = (byte)((FileSize >> 24) & 0xFF);
            Console.WriteLine("文件大小为：" + FileSize);
            return fileData.ToArray();
        }

        public void Test()
        {
            if (Data != null)
            {
                byte[] data = GetByteData();
                for (int i = 0; i < data.Length; i++)
                {
                    Console.Write(data[i] + " "); 
                    if ((i + 1) % 16 == 0)
                    {
                        Console.WriteLine("");
                    }
                }
            }
        }
    }
}
