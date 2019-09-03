using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Tools.CSJ.IMPL
{
    public class CSJ_C:ICSJFile
    {
        public int SceneNo { get; set; }//场景编号}
        public int ChannelCount { get; set; }//场景通道总数
        public int MICSensor { get; set; }//摇麦功能
        public int SenseFreq { get; set; }//摇麦再次感应间隔时间
        public int RunTime { get; set; }//摇麦场景执行时间
        private int FileSize { get; set; }//场景文件大小
        public List<ChannelData> ChannelDatas { get; set; }//数据

        public byte[] GetData()
        {
            List<byte> fileData = new List<byte>();
            byte ram_Enabl = Convert.ToByte(MICSensor);
            byte[] ram_Response_Times = new byte[2];
            ram_Response_Times[0] = Convert.ToByte(((SenseFreq) * 60) & 0xFF);
            ram_Response_Times[1] = Convert.ToByte((((SenseFreq) * 60) >> 8) & 0xFF);
            byte[] ram_Play_Times = new byte[2];
            ram_Play_Times[0] = Convert.ToByte((RunTime) & 0xFF);
            ram_Play_Times[1] = Convert.ToByte(((RunTime) >> 8) & 0xFF);
            byte[] scene_Total_Count = new byte[2];
            scene_Total_Count[0] = (byte)(ChannelCount & 0xFF);
            scene_Total_Count[1] = (byte)((ChannelCount >> 8) & 0xFF);
            FileSize = 0;
            //文件大小预占位
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
            //摇麦开关
            fileData.Add(ram_Enabl);
            //摇麦感应时间
            fileData.AddRange(ram_Response_Times);
            //摇麦执行时间
            fileData.AddRange(ram_Play_Times);
            //通道总数
            fileData.Add(scene_Total_Count[0]);
            fileData.Add(scene_Total_Count[1]);
            //通道数据
            foreach (ChannelData c_Data in ChannelDatas)
            {
                //转换通道编号为byte
                byte[] chanelNo = new byte[2];
                chanelNo[0] = (byte)(c_Data.ChannelNo & 0xFF);
                chanelNo[1] = (byte)((c_Data.ChannelNo >> 8) & 0xFF);
                //添加两字节通道编号
                fileData.Add(chanelNo[0]);
                fileData.Add(chanelNo[1]);
                //添加数据长度
                byte[] dataSzie = new byte[2];
                dataSzie[0] = (byte)(c_Data.DataSize & 0xFF);
                dataSzie[1] = (byte)((c_Data.DataSize >> 8) & 0xFF);
                fileData.Add(dataSzie[0]);
                fileData.Add(dataSzie[1]);
                //添加起始数据偏移量
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

        public void WriteToFile(string filepath)
        {
            byte[] data = GetData();
            string path = filepath + @"\C" + (SceneNo + 1) + ".bin";
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
