using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Xiaosa.Entity
{
    public class RecordMusicFile
    {
        public int MusicStepTime { get; set; } // U8
        public int MusicIntervalTime { get; set; }  // U16
        public int MusicListCount { get; set; } // U8
        public List<int> MusicList { get; set; } // U8[20]
        public int ChannelCount { get; set; } // U16
        public int DmxDataFrameCount { get; set; } // 原始录制文件dmx数据总帧数ps
        public List<byte[]> DmxDatas { get; set; }  //分页读取dmx帧数据
        public List<int> Channels { get; set; }

        private RecordMusicFile()
        {
           
        }

        public static RecordMusicFile BuildFromFile(string sourceFilePath)
        {
            try
            {
                RecordMusicFile musicFile = new RecordMusicFile();

                using (FileStream read = new FileStream(sourceFilePath, FileMode.Open))
                {
                    int index = 4;
                    byte[] readBuff = new byte[30];
                    //读取文件头
                    read.Read(readBuff, 0, readBuff.Length);
                    musicFile.MusicStepTime = readBuff[index++];
                    musicFile.MusicIntervalTime = (readBuff[index++] & 0xFF) | ((readBuff[index++] & 0xFF) << 8);
                    musicFile.MusicListCount = readBuff[index++];
                    musicFile.MusicList = new List<int>();
                    for (int i = 0; i < musicFile.MusicListCount; i++)
                    {
                        musicFile.MusicList.Add(readBuff[index++]);
                    }
                    index += 20 - musicFile.MusicListCount;
                    musicFile.ChannelCount = (readBuff[index++] & 0xFF) | ((readBuff[index++] & 0xFF) << 8);
                    readBuff = new byte[8 * musicFile.ChannelCount];
                    read.Read(readBuff, 0, readBuff.Length);
                    musicFile.Channels = new List<int>();
                    for (int i = 0; i < musicFile.ChannelCount; i += 8)
                    {
                        musicFile.Channels.Add((readBuff[i] & 0xFF) | ((readBuff[i + 1] & 0xFF) << 8));
                    }
                    readBuff = new byte[520];
                    int frameCount = 0;
                    int length = 0;
                    musicFile.DmxDatas = new List<byte[]>();
                    while ((length = read.Read(readBuff,0,readBuff.Length)) == 520)
                    {
                        frameCount++;
                        musicFile.DmxDatas.Add(readBuff);
                        readBuff = new byte[520];
                    }
                    musicFile.DmxDataFrameCount = frameCount;
                }
                return musicFile;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public bool SaveToFile(string destinationFile)
        {
            try
            {
                List<byte> writeBuff = new List<byte>();
                writeBuff.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x00 });
                writeBuff.Add(Convert.ToByte(MusicStepTime));
                writeBuff.Add(Convert.ToByte((MusicIntervalTime) & 0xFF));
                writeBuff.Add(Convert.ToByte((MusicIntervalTime >> 8) & 0xFF));
                MusicListCount = MusicList.Count;
                writeBuff.Add(Convert.ToByte(MusicListCount));
                for (int i = 0; i < MusicListCount; i++)
                {
                    writeBuff.Add(Convert.ToByte(MusicList[i]));
                }
                writeBuff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 20 - MusicListCount).ToArray());
                writeBuff.Add(Convert.ToByte((ChannelCount) & 0xFF));
                writeBuff.Add(Convert.ToByte((ChannelCount >> 8) & 0xFF));
                for (int i = 0; i < ChannelCount; i++)
                {
                    writeBuff.Add(Convert.ToByte((MusicList[i]) & 0xFF));
                    writeBuff.Add(Convert.ToByte((MusicList[i] >> 8) & 0xFF));
                    writeBuff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 6).ToArray());
                }
                for (int i = 0; i < DmxDatas.Count; i++)
                {
                    writeBuff.AddRange(DmxDatas[i]);
                }
                using (FileStream write = new FileStream(destinationFile,FileMode.OpenOrCreate))
                {
                    write.Write(writeBuff.ToArray(), 0, writeBuff.Count);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
