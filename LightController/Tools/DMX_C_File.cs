using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    public class DMX_C_File
    {
        public int SenceNo { get; set; }
        public DMX_C_Data Data { get; set; }

        public byte[] GetByteData()
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
                    //添加采样数据
                    fileData.Add(Convert.ToByte(value));
                }
            }
            //获取文件大小
            FileSize = fileData.Count();
            //添加文件大小
            fileData[0] = (byte)(FileSize & 0xFF);
            fileData[1] = (byte)((FileSize >> 8) & 0xFF);
            fileData[2] = (byte)((FileSize >> 16) & 0xFF);
            fileData[3] = (byte)((FileSize >> 24) & 0xFF);
            return fileData.ToArray();
        }

        public void WriteFile(string path)
        {
            byte[] data = GetByteData();
           
            string filePath;
            if (SenceNo < 9)
            {
                filePath = path + @"\C0" + (SenceNo + 1) + ".bin";
            }
            else
            {
                filePath = path + @"C\" + (SenceNo + 1) + ".bin";
            }
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }
    }
}
