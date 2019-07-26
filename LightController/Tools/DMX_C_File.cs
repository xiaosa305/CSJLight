using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    public class DMX_C_File
    {
        public int SceneNo { get; set; }
        public DMX_C_Data Data { get; set; }

        public byte[] GetByteData()
        {
            List<byte> fileData = new List<byte>();
            //byte Ram_Enabl = Convert.ToByte(Data.HeadData.MICSensor);
            //byte[] Ram_Response_Times = new byte[4];
            //Ram_Response_Times[0] = Convert.ToByte(((Data.HeadData.SenseFreq + 1) * 60000) & 0xFF);
            //Ram_Response_Times[1] = Convert.ToByte((((Data.HeadData.SenseFreq + 1) * 60000) >> 8) & 0xFF);
            //Ram_Response_Times[2] = Convert.ToByte((((Data.HeadData.SenseFreq + 1) * 60000) >> 16) & 0xFF);
            //Ram_Response_Times[3] = Convert.ToByte((((Data.HeadData.SenseFreq + 1) * 60000) >> 24) & 0xFF);
            //byte[] Ram_Play_Times = new byte[4];

            //Ram_Play_Times[0] = Convert.ToByte(((Data.HeadData.RunTime + 1) * 1000) & 0xFF);
            //Ram_Play_Times[1] = Convert.ToByte((((Data.HeadData.RunTime + 1) * 1000) >> 8) & 0xFF);
            //Ram_Play_Times[2] = Convert.ToByte((((Data.HeadData.RunTime + 1) * 1000) >> 16) & 0xFF);
            //Ram_Play_Times[3] = Convert.ToByte((((Data.HeadData.RunTime + 1) * 1000) >> 24) & 0xFF);

            byte[] Scene_Total_Count = new byte[2];
            Scene_Total_Count[0] = (byte)(Data.HeadData.ChanelCount & 0xFF);
            Scene_Total_Count[1] = (byte)((Data.HeadData.ChanelCount >> 8) & 0xFF);
            int FileSize = 0;
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
            //fileData.Add(Ram_Enabl);
            //fileData.AddRange(Ram_Response_Times);
            //fileData.AddRange(Ram_Play_Times);
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
            if (SceneNo < 9)
            {
                filePath = path + @"\C0" + (SceneNo + 1) + ".bin";
            }
            else
            {
                filePath = path + @"C\" + (SceneNo + 1) + ".bin";
            }
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }
    }
}
