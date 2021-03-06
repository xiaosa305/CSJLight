using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RecordTools.Entity
{
    public class GlobalConfig
    {
        public int DefaultScene { get; set; }

        public GlobalConfig(int defaultScene)
        {
            this.DefaultScene = defaultScene;
        }

        public bool WriteToFile(string dirPath,string fileName)
        {
            bool result = false;
            try
            {
                List<byte> writeBuff = new List<byte>();
                //文件大小
                writeBuff.Add(0x00);
                writeBuff.Add(0x00);
                writeBuff.Add(0x00);
                writeBuff.Add(0x00);

                //灯具总数
                writeBuff.Add(0x01);
                writeBuff.Add(0x00);

                //通道总数
                writeBuff.Add(0x00);
                writeBuff.Add(0x02);

                //开机场景
                writeBuff.Add(Convert.ToByte(this.DefaultScene));

                //音频功能
                writeBuff.Add(0xFF);
                writeBuff.Add(0xFF);
                writeBuff.Add(0xFF);
                writeBuff.Add(0xFF);

                //场景切换
                writeBuff.Add(0x00);

                //时间因子
                writeBuff.Add(0x28);

                //场景接力
                for (int index = 0; index < 9; index++)
                {
                    writeBuff.Add(0x00);

                    writeBuff.Add(0x00);
                    writeBuff.Add(0x00);

                    writeBuff.Add(0x00);

                    writeBuff.Add(0x00);
                    writeBuff.Add(0x00);

                    writeBuff.Add(0x00);

                    writeBuff.Add(0x00);
                    writeBuff.Add(0x00);

                    writeBuff.Add(0x00);

                    writeBuff.Add(0x00);
                    writeBuff.Add(0x00);

                    writeBuff.Add(0x00);

                    writeBuff.Add(0x00);
                    writeBuff.Add(0x00);

                    writeBuff.Add(0x00);

                    writeBuff.Add(0x00);
                    writeBuff.Add(0x00);
                }

                //灯
                writeBuff.Add(0x01);
                writeBuff.Add(0x00);

                writeBuff.Add(0x01);
                writeBuff.Add(0x00);

                writeBuff.Add(0x0E);
                writeBuff.Add(0x00);

                writeBuff[0] = (Convert.ToByte(writeBuff.Count & 0xFF));
                writeBuff[1] = Convert.ToByte((writeBuff.Count >> 8) & 0xFF);
                writeBuff[2] = Convert.ToByte((writeBuff.Count >> 16) & 0xFF);
                writeBuff[3] = Convert.ToByte((writeBuff.Count >> 24) & 0xFF);

                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                string filePath = dirPath + @"\" + fileName;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    stream.Write(writeBuff.ToArray(), 0, writeBuff.Count);
                }
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return result;
        }
    }
}
