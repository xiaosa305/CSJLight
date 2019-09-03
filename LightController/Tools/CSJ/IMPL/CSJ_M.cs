using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LighEditor.Tools.CSJ.IMPL
{
    public class CSJ_M:ICSJFile
    {
        public int SceneNo { get; set; }//场景编号
        public int ChannelCount { get; set; }//通道总数
        private int FileSize { get; set; }//文件大小
        public int StepListCount { get; set; }//音频步数链表元素个数
        public int FrameTime { get; set; }//音频步时间
        public int MusicIntervalTime { get; set; }//音频叠加后间隔时间
        public List<int> StepList { get; set; }//音频步数链表
        public List<ChannelData> ChannelDatas { get; set; }//数据

        public byte[] GetData()
        {
            IList<byte> fileData = new List<byte>();
            FileSize = 0;
            byte Music_Frame_Time = Convert.ToByte(FrameTime);
            byte[] Scene_Total_Count = new byte[2];
            byte MusicControlStepListCount = Convert.ToByte(StepListCount);
            Scene_Total_Count[0] = (byte)(ChannelCount & 0xFF);
            Scene_Total_Count[1] = (byte)((ChannelCount >> 8) & 0xFF);
            byte[] MusicIntervalTimeBuff = new byte[2];
            MusicIntervalTimeBuff[0] = Convert.ToByte(MusicIntervalTime & 0xFF);
            MusicIntervalTimeBuff[1] = Convert.ToByte((MusicIntervalTime >> 8) & 0xFF);
            //添加文件大小预占位
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
            //添加音频步时间
            fileData.Add(Music_Frame_Time);
            //添加音频叠加后间隔时间
            fileData.Add(MusicIntervalTimeBuff[0]);
            fileData.Add(MusicIntervalTimeBuff[1]);
            //添加音频步数列表成员个数
            fileData.Add(MusicControlStepListCount);
            //添加音频步数列表
            for (int i = 0; i < StepListCount; i++)
            {
                fileData.Add(Convert.ToByte(StepList[i]));
            }
            for (int i = StepListCount; i < 20; i++)
            {
                fileData.Add(Convert.ToByte(0x00));
            }
            //添加通道总数
            fileData.Add(Scene_Total_Count[0]);
            fileData.Add(Scene_Total_Count[1]);
            foreach (ChannelData m_Data in ChannelDatas)
            {
                //转换通道编号为byte
                byte[] chanelNo = new byte[2];
                chanelNo    [0] = (byte)(m_Data.ChannelNo & 0xFF);
                chanelNo[1] = (byte)((m_Data.ChannelNo >> 8) & 0xFF);
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
                //添加起始数据偏移量
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

        public void WriteToFile(string filepath)
        {
            byte[] data = GetData();
            string path = filepath + @"\M" + (SceneNo + 1) + ".bin";
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            FileStream fileStream = new FileStream(path, FileMode.Create);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }
    }
}
