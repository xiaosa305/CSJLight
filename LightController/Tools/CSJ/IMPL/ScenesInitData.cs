using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Tools.CSJ.IMPL
{
    public class ScenesInitData : ICSJFile
    {
        private int FileSize { get; set; }
        private int[][] ScenesData { get; set; }

        public ScenesInitData(CSJ_Project project)
        {
            this.ScenesData = new int[Constant.SCENECOUNTMAX][];
            int[] defaultDmx512Data = Enumerable.Repeat(0, Constant.DMX512).ToArray();
            for (int i = 0; i < Constant.SCENECOUNTMAX; i++)
            {
                ScenesData[i] = defaultDmx512Data;
            }
            if (null != project)
            {
                if (null != project.CFiles)
                {
                    for (int scene = 0; scene < project.CFiles.Count; scene++)
                    {
                        CSJ_C file = project.CFiles[scene] as CSJ_C;
                        int[] data = Enumerable.Repeat(0, Constant.DMX512).ToArray();
                        for (int channel = 0; channel < file.ChannelCount; channel++)
                        {
                            data[file.ChannelDatas[channel].ChannelNo - 1] = file.ChannelDatas[channel].Datas[0];
                        }
                        this.ScenesData[file.SceneNo] = data;
                    }
                }
            }
        }
        public byte[] GetData()
        {
            List<byte> dataBuff = new List<byte>();
            this.FileSize = 0;
            dataBuff.Add(Convert.ToByte(FileSize & 0xFF));
            dataBuff.Add(Convert.ToByte((FileSize >> 8) & 0xFF));
            dataBuff.Add(Convert.ToByte((FileSize >> 16) & 0xFF));
            dataBuff.Add(Convert.ToByte((FileSize >> 24) & 0xFF));
            for (int scene = 0; scene < Constant.SCENECOUNTMAX; scene++)
            {
                for (int channel = 0; channel < Constant.DMX512; channel++)
                {
                    dataBuff.Add(Convert.ToByte(ScenesData[scene][channel]));
                }
            }
            this.FileSize = dataBuff.Count();
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
