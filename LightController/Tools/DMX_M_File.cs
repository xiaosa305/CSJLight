using System;
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

        public byte[] GetByteData()
        {
            IList<byte> fileData = new List<byte>();
            int FileSize = 0;
            byte Music_Step_Times = Convert.ToByte(Data.HeadData.StepTimes);
            byte Music_Frame_Time = Convert.ToByte(Data.HeadData.FrameTime);
            byte[] Scene_Total_Count = new byte[2];
            Scene_Total_Count[0] = (byte)(Data.HeadData.ChanelCount & 0xFF);
            Scene_Total_Count[1] = (byte)((Data.HeadData.ChanelCount >> 8) & 0xFF);
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Convert.ToByte(FileSize));
            fileData.Add(Music_Step_Times);
            fileData.Add(Music_Frame_Time);
            fileData.Add(Scene_Total_Count[0]);
            fileData.Add(Scene_Total_Count[1]);
            foreach (M_Data m_Data in Data.Datas)
            {
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
            if (SceneNo < 9)
            {
                filePath = path + @"\M0" + (SceneNo + 1) + ".bin";
            }
            else
            {
                filePath = path + @"\M" + (SceneNo + 1) + ".bin";
            }
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }
    }
}
