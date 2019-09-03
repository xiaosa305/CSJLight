using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LighEditor.Tools.CSJ.IMPL
{
    public class ScenesInitData : ICSJFile
    {
        private int FileSize { get; set; }
        private int[][] ScenesData { get; set; }

        public ScenesInitData(CSJ_Project project)
        {
            ScenesData = new int[32][];
            int[] defaultDmx512Data = Enumerable.Repeat(0, Constant.DMX512).ToArray();
            for (int i = 0; i < 32; i++)
            {
                ScenesData[i] = defaultDmx512Data;
            }
            if (null != project)
            {
                if (null != project.CFiles)
                {
                    foreach (CSJ_C file in project.CFiles)
                    {
                        int[] data = defaultDmx512Data;
                        for (int channelNo = 0; channelNo < file.ChannelCount; channelNo++)
                        {
                            data[file.ChannelDatas[channelNo].ChannelNo - 1] = file.ChannelDatas[channelNo].Datas[0];
                        }
                        ScenesData[file.SceneNo] = data;
                    }
                }
            }
        }

        public byte[] GetData()
        {
            List<byte> dataBuff = new List<byte>();
            FileSize = 0;
            dataBuff.Add(Convert.ToByte(FileSize & 0xFF));
            dataBuff.Add(Convert.ToByte((FileSize >> 8) & 0xFF));
            dataBuff.Add(Convert.ToByte((FileSize >> 16) & 0xFF));
            dataBuff.Add(Convert.ToByte((FileSize >> 24) & 0xFF));
            for (int scene = 0; scene < Constant.SCENECOUNT; scene++)
            {
                for (int channel = 0; channel < Constant.DMX512; channel++)
                {
                    dataBuff.Add(Convert.ToByte(ScenesData[scene][channel]));
                }
            }
            FileSize = dataBuff.Count();
            dataBuff[0] = Convert.ToByte(FileSize & 0xFF);
            dataBuff[1] = Convert.ToByte((FileSize >> 8) & 0xFF);
            dataBuff[2] = Convert.ToByte((FileSize >> 16) & 0xFF);
            dataBuff[3] = Convert.ToByte((FileSize >> 24) & 0xFF);
            return dataBuff.ToArray();
        }

        public void WriteToFile(string filepath)
        {
            byte[] data = GetData();
            string path = filepath + @"\GradientData.bin";
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
