using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RecordTools.utils
{
    public class EditMusicRecordFile
    {
        private static EditMusicRecordFile Instance { get; set; }
        private int Seek { get; set; }
        public int MusicStepTime { get; set; }
        public int MusicStepWaitTime { get; set; }
        public int MusicStepListCount { get; set; }
        public List<int> MusicStepList { get; set; }
        public int ChannelCount { get; set; }
        public List<int> ChannelList { get; set; }

        private EditMusicRecordFile()
        {
            this.Seek = 0;
            this.MusicStepTime = 0;
            this.MusicStepWaitTime = 0;
            this.MusicStepListCount = 0;
            this.MusicStepList = new List<int>();
            this.ChannelList = new List<int>();
            this.ChannelCount = 0;
        }

        public static EditMusicRecordFile GetInstance()
        {
            if (Instance == null)
            {
                Instance = new EditMusicRecordFile();
            }
            return Instance;
        }

        public void ReadRecordFile(String filePath)
        {
            byte[] buff = new byte[1024 * 2];
            using (FileStream stream = new FileStream(filePath,FileMode.Open))
            {
                if (stream.Length > 1024 * 2)
                {
                    stream.Read(buff, 0, 1024 * 2);
                }
                else
                {
                    stream.Read(buff,0, (int)stream.Length);
                }
                //读取音频步时间
                this.MusicStepTime = buff[4] & 0xFF;
                //读取音频等待时间
                this.MusicStepWaitTime = (int)((buff[5] & 0xFF) | ((buff[6] & 0xFF) << 8));
                //读取音频声控步数个数
                this.MusicStepListCount = buff[7] & 0xFF;
                //读取音频声控步数链表
                this.MusicStepList = new List<int>();
                for (int i = 0; i < 20; i++)
                {
                    int step = buff[8 + i] & 0xFF;
                    if (step != 0)
                    {
                        this.MusicStepList.Add(step);
                    }
                }
                //读取通道总数
                this.ChannelCount = (int)((buff[28] & 0xFF) | ((buff[29] & 0xFF) << 8));
                //读取通道编号
                if (this.ChannelCount > 0)
                {
                    for (int i = 0; i < this.ChannelCount; i++)
                    {
                        int channel = (int)((buff[30 + 10 * i] & 0xFF) | ((buff[31 + 10 * i] & 0xFF) << 8));
                        this.ChannelList.Add(channel);
                    }
                }
            }
        }

        public void WriteRecordDataToFile(string srcFile,string desFile)
        {
            if (File.Exists(desFile))
            {
                File.Delete(desFile);
            }
            using (FileStream writeStream = new FileStream(desFile, FileMode.Create))
            {
                byte[] buff = new byte[520];
                int dataSize = 0;
                //写数据头
                List<byte> headBuff = new List<byte>();
                headBuff.Add(0x00);
                headBuff.Add(0x00);
                headBuff.Add(0x00);
                headBuff.Add(0x00);

                headBuff.Add(Convert.ToByte(this.MusicStepTime));

                headBuff.Add(Convert.ToByte(this.MusicStepWaitTime));
                headBuff.Add(Convert.ToByte(Convert.ToByte((this.MusicStepWaitTime >> 8) & 0xFF)));

                headBuff.Add(Convert.ToByte(this.MusicStepList.Count));

                for (int i = 0; i < this.MusicStepList.Count; i++)
                {
                    headBuff.Add(Convert.ToByte(this.MusicStepList[i]));
                }
                for (int i = this.MusicStepList.Count; i < 20; i++)
                {
                    headBuff.Add(0x00);
                }

                headBuff.Add(Convert.ToByte(this.ChannelList.Count));
                headBuff.Add(Convert.ToByte(Convert.ToByte((this.ChannelList.Count >> 8) & 0xFF)));

                for (int i = 0; i < this.ChannelList.Count; i++)
                {
                    headBuff.Add(Convert.ToByte(this.ChannelList[i]));
                    headBuff.Add(Convert.ToByte(Convert.ToByte((this.ChannelList[i] >> 8) & 0xFF)));
                    headBuff.Add(0x00);
                    headBuff.Add(0x00);
                    headBuff.Add(0x00);
                    headBuff.Add(0x00);
                    headBuff.Add(0x00);
                    headBuff.Add(0x00);
                    headBuff.Add(0x00);
                    headBuff.Add(0x00);
                }

                dataSize += headBuff.Count;
                writeStream.Write(headBuff.ToArray(), 0, headBuff.Count);

                //写帧数据
                using (FileStream readStream = new FileStream(srcFile, FileMode.Open))
                {
                    readStream.Seek(30 + 10 * (ChannelCount + 1), SeekOrigin.Begin);
                    int length = 0;
                    while ((length = readStream.Read(buff, 0, buff.Length)) != 0)
                    {
                        dataSize += length;
                        writeStream.Write(buff, 0, length);
                    }
                }
                writeStream.Seek(0, SeekOrigin.Begin);
                headBuff.Clear();
                headBuff.Add(Convert.ToByte(dataSize));
                headBuff.Add(Convert.ToByte(Convert.ToByte((dataSize >> 8) & 0xFF)));
                headBuff.Add(Convert.ToByte(Convert.ToByte((dataSize >> 16) & 0xFF)));
                headBuff.Add(Convert.ToByte(Convert.ToByte((dataSize >> 24) & 0xFF)));
                writeStream.Write(headBuff.ToArray(), 0, headBuff.Count);
            }
        }
    }
}
