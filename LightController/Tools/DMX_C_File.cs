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

        public string Print()
        {
            string str = "C" + SceneNo + "==>文件大小==>" + GetByteData().Length + ";摇麦功能开启==>" + Data.HeadData.MICSensor + ";摇麦感应间隔时间==>" + Data.HeadData.SenseFreq + ";摇麦场景执行时间" + Data.HeadData.RunTime;
            return str;
        }

        public byte[] GetByteData()
        {
            List<byte> fileData = new List<byte>();
            byte Ram_Enabl = Convert.ToByte(Data.HeadData.MICSensor);
            byte[] Ram_Response_Times = new byte[2];
            Ram_Response_Times[0] = Convert.ToByte(((Data.HeadData.SenseFreq) * 60) & 0xFF);
            Ram_Response_Times[1] = Convert.ToByte((((Data.HeadData.SenseFreq) * 60) >> 8) & 0xFF);
            byte[] Ram_Play_Times = new byte[2];
            Ram_Play_Times[0] = Convert.ToByte((Data.HeadData.RunTime) & 0xFF);
            Ram_Play_Times[1] = Convert.ToByte(((Data.HeadData.RunTime) >> 8) & 0xFF);
            byte[] Scene_Total_Count = new byte[2];
            int test = 0;
            foreach (C_Data item in Data.Datas)
            {
                if (item.ChanelNo > 512)
                {
                    test++;
                }
            }
            Scene_Total_Count[0] = (byte)((Data.HeadData.ChanelCount - test) & 0xFF);
            Scene_Total_Count[1] = (byte)(((Data.HeadData.ChanelCount - test) >> 8) & 0xFF);
            int FileSize = 0;
            //文件大小
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
            //摇麦开关
            fileData.Add(Ram_Enabl);
            //摇麦感应时间
            fileData.AddRange(Ram_Response_Times);
            //摇麦执行时间
            fileData.AddRange(Ram_Play_Times);
            //通道总数
            fileData.Add(Scene_Total_Count[0]);
            fileData.Add(Scene_Total_Count[1]);
            //通道数据
            foreach (C_Data c_Data in Data.Datas)
            {
                if (c_Data.ChanelNo > 512)
                {
                    continue;
                }
                //转换通道编号为byte
                byte[] chanelNo = new byte[2];
                chanelNo[0] = (byte)(c_Data.ChanelNo & 0xFF);
                chanelNo[1] = (byte)((c_Data.ChanelNo >> 8) & 0xFF);
                //添加两字节通道编号
                fileData.Add(chanelNo[0]);
                fileData.Add(chanelNo[1]);
                //获取数据长度
                byte[] dataSzie = new byte[2];
                dataSzie[0] = (byte)(c_Data.DataSize & 0xFF);
                dataSzie[1] = (byte)((c_Data.DataSize >> 8) & 0xFF);
                //添加数据长度
                fileData.Add(dataSzie[0]);
                fileData.Add(dataSzie[1]);
                //**************添加起始数据偏移量
                int length = fileData.Count + 4;
                fileData.Add(Convert.ToByte(length & 0xFF));
                fileData.Add(Convert.ToByte((length >> 8) & 0xFF));
                fileData.Add(Convert.ToByte((length >> 16) & 0xFF));
                fileData.Add(Convert.ToByte((length >> 24) & 0xFF));
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
            filePath = path + @"\C" + (SceneNo + 1) + ".bin";
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }
    }
}
