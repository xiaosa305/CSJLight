using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RecordTools.Entity
{
    public class MusicSceneConfig
    {
		public int StepTime { get; set; }
        public int StepWaitTIme { get; set; }
        public string MusicStepList { get; set; }
        public HashSet<int> MusicChannelNoList { get; set; }

        public bool WriteToFile(string dirPath,string fileName)
        {
            try
            {
                List<int> steps = new List<int>();
                List<byte> writeBuff = new List<byte>();
                writeBuff.Add(0x00);
                writeBuff.Add(0x00);
                writeBuff.Add(0x00);
                writeBuff.Add(0x00);
                if (this.StepTime >= 0 && this.StepTime <= 255)
                {
                    writeBuff.Add(Convert.ToByte(this.StepTime & 0xFF));
                }
                else
                {
                    writeBuff.Add(0x1E);
                }
                if (this.StepWaitTIme >= 0)
                {
                    writeBuff.Add(Convert.ToByte(this.StepWaitTIme & 0xFF));
                    writeBuff.Add(Convert.ToByte((this.StepWaitTIme >> 8) & 0xFF));
                }
                else
                {
                    writeBuff.Add(0x00);
                }
                for (int index = 0; index < this.MusicStepList.Length; index++)
                {
                    steps.Add(int.Parse("" + this.MusicStepList[index]));
                }

                if (steps.Count >= 0)
                {
                    if (steps.Count > 20)
                    {
                        writeBuff.Add(0x14);
                    }
                    else
                    {
                        writeBuff.Add(Convert.ToByte(steps.Count & 0xFF));
                    }
                }
                else
                {
                    writeBuff.Add(0x00);
                }
                for (int index = 0; index < 20; index++)
                {
                    if (index >= steps.Count)
                    {
                        writeBuff.Add(0x00);
                    }
                    else
                    {
                        if (steps[index] > 255)
                        {
                            writeBuff.Add(0xFF);
                        }
                        else if (steps[index] < 0)
                        {
                            writeBuff.Add(0x00);
                        }
                        else
                        {
                            writeBuff.Add(Convert.ToByte(steps[index] & 0xFF));
                        }
                    }
                }
                writeBuff.Add(Convert.ToByte(this.MusicChannelNoList.Count & 0xFF));
                writeBuff.Add(Convert.ToByte((this.MusicChannelNoList.Count >> 8) & 0xFF));
                int seek = writeBuff.Count;
                foreach (int channelNo in this.MusicChannelNoList)
                {
                    seek += 8;
                    writeBuff.Add(Convert.ToByte(channelNo & 0xFF));
                    writeBuff.Add(Convert.ToByte((channelNo >> 8) & 0xFF));
                    writeBuff.Add(0x01);
                    writeBuff.Add(0x00);
                    writeBuff.Add(Convert.ToByte(seek & 0xFF));
                    writeBuff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                    writeBuff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                    writeBuff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                    writeBuff.Add(0x00);
                    seek += 1;
                }
                writeBuff[0] = Convert.ToByte(writeBuff.Count & 0xFF);
                writeBuff[1] = Convert.ToByte((writeBuff.Count >> 8) & 0xFF);
                writeBuff[2] = Convert.ToByte((writeBuff.Count >> 16) & 0xFF);
                writeBuff[3] = Convert.ToByte((writeBuff.Count >> 24) & 0xFF);
                if (this.MusicChannelNoList.Count > 0)
                {
                    string filePath = dirPath + @"\" + fileName;
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        stream.Write(writeBuff.ToArray(), 0, writeBuff.Count);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public static MusicSceneConfig ReadFromFile(string filePath)
        {
            try
            {
                MusicSceneConfig config = new MusicSceneConfig();
                FileInfo file = new FileInfo(filePath);
                byte[] readBuff = new byte[file.Length];
                using (FileStream stream = file.OpenRead())
                {
                    stream.Read(readBuff, 0, readBuff.Length);
                }
                config.StepTime = (int)(readBuff[4] & 0xFF);
                config.StepWaitTIme = (int)((readBuff[5] & 0xFF) | ((readBuff[6] << 8) & 0xFF));

                config.StepWaitTIme = (int)((readBuff[5] & 0xFF) | ((readBuff[6] & 0xFF) << 8));

                int stepListCount = (int)(readBuff[7] & 0xFF);
                config.MusicStepList = "";
                for (int index = 0; index < stepListCount; index++)
                {
                    config.MusicStepList += ((int)(readBuff[8 + index] & 0xFF)).ToString();
                }
                int stepChannelCount = (int)((readBuff[28] & 0xFF) | ((readBuff[29] & 0xFF) << 8));
                config.MusicChannelNoList = new HashSet<int>();
                for (int index = 0; index < stepChannelCount; index++)
                {
                    int channelNo = (int)((readBuff[30 + index * 9] & 0xFF) | ((readBuff[31 + index * 9] & 0xFF) << 8));
                    config.MusicChannelNoList.Add(channelNo);
                }
                return config;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return null;
            }
           
        }
    }
}
