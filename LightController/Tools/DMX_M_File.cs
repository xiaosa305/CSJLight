﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    public class DMX_M_File
    {
        public int SceneNo { get; set; }
        public DMX_M_Data Data { get; set; }

        public string Print()
        {
            string str = "M"+SceneNo+"==>文件大小==>" + GetByteData().Length + "; 通道总数==>" + Data.HeadData.ChanelCount + ";音频声控步时长==>" + Data.HeadData.FrameTime
            + ";音频叠加后间隔时间==>" + Data.HeadData.MusicIntervalTime + ";音频声控步数个数==>" + Data.HeadData.StepListCount;
            return str;
        }

        public byte[] GetByteData()
        {
            IList<byte> fileData = new List<byte>();
            int FileSize = 0;
            byte Music_Frame_Time = Convert.ToByte(Data.HeadData.FrameTime);
            byte[] Scene_Total_Count = new byte[2];
            byte MusicControlStepListCount = Convert.ToByte(Data.HeadData.StepListCount);
            int test = 0;
            foreach (M_Data item in Data.Datas)
            {
                if (item.ChanelNo > 512)
                {
                    test++;
                }
            }
            Scene_Total_Count[0] = (byte)((Data.HeadData.ChanelCount - test) & 0xFF);
            Scene_Total_Count[1] = (byte)(((Data.HeadData.ChanelCount -test) >> 8) & 0xFF);
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
           
            fileData.Add(Music_Frame_Time);
            byte[] MusicIntervalTimeBuff = new byte[2];
            MusicIntervalTimeBuff[0] = Convert.ToByte(Data.HeadData.MusicIntervalTime & 0xFF);
            MusicIntervalTimeBuff[1] = Convert.ToByte((Data.HeadData.MusicIntervalTime >> 8) & 0xFF);
            fileData.Add(MusicIntervalTimeBuff[0]);
            fileData.Add(MusicIntervalTimeBuff[1]);
            fileData.Add(MusicControlStepListCount);
            foreach (int step in Data.HeadData.StepList)
            {
                fileData.Add(Convert.ToByte(step));
            }
            for (int i = Data.HeadData.StepListCount; i < 20; i++)
            {
                fileData.Add(Convert.ToByte(0x00));
            }
            fileData.Add(Scene_Total_Count[0]);
            fileData.Add(Scene_Total_Count[1]);
            foreach (M_Data m_Data in Data.Datas)
            {
                int test1 = m_Data.ChanelNo;
                if (m_Data.ChanelNo > 512)
                {
                    continue;
                }
                //转换通道编号为byte
                byte[] chanelNo = new byte[2];
                chanelNo[0] = (byte)(m_Data.ChanelNo & 0xFF);
                chanelNo[1] = (byte)((m_Data.ChanelNo >> 8) & 0xFF);
                //添加两字节通道编号
                fileData.Add(chanelNo[0]);
                fileData.Add(chanelNo[1]);
                //获取数据长度
                byte[] dataSzie = new byte[2];
                dataSzie[0] = (byte)(m_Data.DataSize & 0xFF);
                dataSzie[1] = (byte)((m_Data.DataSize >> 8) & 0xFF);
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
                foreach (int value in m_Data.Datas)
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
            filePath = path + @"\M" + (SceneNo + 1) + ".bin";
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }
    }
}
