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
        public List<int> MusicStepList { get; set; }
        public List<int> MusicChannelNoList { get; set; }

        public void WriteToFile(string dirPath,string fileName)
        {
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
            if (this.MusicStepList.Count >= 0)
            {
                if (this.MusicStepList.Count > 20)
                {
                    writeBuff.Add(0x14);
                }
                else
                {
                    writeBuff.Add(Convert.ToByte(this.MusicStepList.Count & 0xFF));
                }
            }
            else
            {
                writeBuff.Add(0x00);
            }
            for (int index = 0; index < 20; index++)
            {
                if (index >= this.MusicStepList.Count)
                {
                    writeBuff.Add(0x00);
                }
                else
                {
                    if (this.MusicStepList[index] >  255)
                    {
                        writeBuff.Add(0xFF);
                    }
                    else if (this.MusicStepList[index] < 0)
                    {
                        writeBuff.Add(0x00);
                    }
                    else
                    {
                        writeBuff.Add(Convert.ToByte(this.MusicStepList[index] & 0xFF));
                    }
                }
            }
            for (int index = 0; index < this.MusicChannelNoList.Count; index++)
            {
                int channelNo = this.MusicChannelNoList[index];
                int seek = writeBuff.Count + 8;
                writeBuff.Add(Convert.ToByte(channelNo & 0xFF));
                writeBuff.Add(Convert.ToByte((channelNo >> 8) & 0xFF));
                writeBuff.Add(0x01);
                writeBuff.Add(0x00);
                writeBuff.Add(Convert.ToByte(seek & 0xFF));
                writeBuff.Add(Convert.ToByte((seek >> 8) & 0xFF));
                writeBuff.Add(Convert.ToByte((seek >> 16) & 0xFF));
                writeBuff.Add(Convert.ToByte((seek >> 24) & 0xFF));
                writeBuff.Add(0x00);
            }
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
        }
    }
}
