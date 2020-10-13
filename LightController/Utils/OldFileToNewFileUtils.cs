using LightController.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;

namespace LightController.Utils
{
    public class OldFileToNewFileUtils
    {
        private readonly string TransitionCacheFilePath = Application.StartupPath + @"\TransitionCacheFile";
        private static OldFileToNewFileUtils Instance { get; set; }
        public delegate void Completed();
        public delegate void Error(string msg);
        private bool IsLeisure { get; set; }
        private System.Timers.Timer WorkTimer { get; set; }

        private Completed Completed_Event { get; set; }
        private Error Error_Event { get; set; }

        private string SouceFilePath { get; set; }
        private string DirPath { get; set; }
        private string FileName { get; set; }
        private SceneConfig Config { get; set; }
        private FileKind Kind { get; set; }

        private OldFileToNewFileUtils()
        {
            this.WorkTimer = new System.Timers.Timer() { AutoReset = false };
            this.WorkTimer.Elapsed += this.WorkTranslation;
            this.InitParam();
        }

        public static OldFileToNewFileUtils GetInstance()
        {
            if (Instance == null)
            {
                Instance = new OldFileToNewFileUtils();
            }
            return Instance;
        }


        private void InitParam()
        {
            this.IsLeisure = true;
            if (!Directory.Exists(TransitionCacheFilePath))
            {
                Directory.CreateDirectory(TransitionCacheFilePath);
            }
        }

        private void StartWorking()
        {
            this.WorkTimer.Start();
        }

        private void StopWorking(string msg)
        {
            this.Error_Event(msg);
            this.InitParam();
            this.WorkTimer.Stop();
        }


        public void ReadOldFile(string souceFilePath,string dirPath,string fileName,SceneConfig config,FileKind kind,Completed completed,Error error)
        {
            try
            {
                if (this.IsLeisure)
                {
                    this.IsLeisure = false;
                    this.Completed_Event = completed;
                    this.Error_Event = error;
                    this.SouceFilePath = souceFilePath;
                    this.DirPath = dirPath;
                    this.FileName = fileName;
                    this.Config = config;
                    this.Kind = kind;
                    this.StartWorking();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("XIAOSA ：启动文件转换失败");
                Console.WriteLine("XIAOSA ：" + ex.Message);
                Console.WriteLine("XIAOSA ：" + ex.StackTrace);
            }
        }

        private void WorkTranslation(object sender, ElapsedEventArgs e)
        {
            switch (this.Kind)
            {
                case FileKind.Project_Basic:
                    this.ReadOldBasicFile();
                    break;
                case FileKind.Project_Music:
                    this.ReadOldMusicFile();
                    break;
                case FileKind.Record_Basic:
                    this.ReadOldRecordBasicFile();
                    break;
                case FileKind.Record_Music:
                    this.ReadOldRecordMusicFile();
                    break;
            }
        }

        private void ReadOldBasicFile()
        {
            //清楚缓存文件
            for (int channelNo = 1; channelNo < 513; channelNo++)
            {
                string cacheFileName = TransitionCacheFilePath + @"\C-" + channelNo + @".bin";
                if (File.Exists(cacheFileName))
                {
                    File.Delete(cacheFileName);
                }
            }
            //读取旧文件数据
            using (FileStream stream = new FileStream(this.SouceFilePath,FileMode.Open))
            {
                Dictionary<int, List<byte>> framDatas = new Dictionary<int, List<byte>>();
                if (stream.Length >= 1040)
                {
                    byte[] buff = new byte[520];
                    stream.Seek(520, SeekOrigin.Begin);
                    while (stream.Read(buff, 0, buff.Length) == 520)
                    {
                        if (framDatas[1].Count > 1024 * 4)
                        {
                            //写缓存数据
                            for (int channelIndex = 0; channelIndex < 513; channelIndex++)
                            {
                                this.WriteBasicCacheDataToFile(channelIndex + 0, framDatas[channelIndex]);
                            }
                            //清空缓存
                            framDatas = new Dictionary<int, List<byte>>();
                        }
                        for (int index = 2; index < 514; index++)
                        {
                            if (!framDatas.ContainsKey(index))
                            {
                                framDatas.Add(index - 1, new List<byte>());
                            }
                            else
                            {
                                framDatas[index - 1].Add(buff[index]);
                            }
                        }
                    }
                }
            }
            //缓存写入完成，整合缓存文件
            string filePath = this.DirPath + @"\" + this.FileName + @".bin";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            bool flag = false;
            File.Create(filePath).Dispose();
            using (FileStream writeStream = new FileStream(filePath,FileMode.Append))
            {
                List<byte> writeBuff = new List<byte>();
                long dataSize = 0;
                #region 写新文件的文件头
                writeBuff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00),4));
                writeBuff.AddRange(Config.GetHeadData());
                dataSize += writeBuff.Count;
                #endregion
                #region 读取缓存文件数据写到新文件中
                for (int channelNo = 1; channelNo < 513; channelNo++)
                {
                    string cacheFileName = TransitionCacheFilePath + @"\C-" + channelNo + @".bin";
                    if (File.Exists(cacheFileName))
                    {
                        using (FileStream readStream = new FileStream(cacheFileName,FileMode.Open))
                        {
                            flag = true;
                            long fileSize = readStream.Length;
                            byte[] readBuff = new byte[fileSize];
                            readStream.Read(readBuff, 0, readBuff.Length);
                            writeBuff.Add(Convert.ToByte((channelNo) & 0xFF));
                            writeBuff.Add(Convert.ToByte((channelNo >> 8) & 0xFF));
                            writeBuff.Add(Convert.ToByte((fileSize) & 0xFF));
                            writeBuff.Add(Convert.ToByte((fileSize >> 8) & 0xFF));
                            writeBuff.Add(Convert.ToByte((dataSize + 8) & 0xFF));
                            writeBuff.Add(Convert.ToByte(((dataSize + 8) >> 8) & 0xFF));
                            writeBuff.Add(Convert.ToByte(((dataSize + 8) >> 16) & 0xFF));
                            writeBuff.Add(Convert.ToByte(((dataSize + 8) >> 24) & 0xFF));
                            dataSize += fileSize + 8;
                            writeBuff.AddRange(readBuff);
                        }
                    }
                    writeStream.Write(writeBuff.ToArray(), 0, writeBuff.Count);
                    writeBuff.Clear();
                }
                #endregion
                #region 写文件大小
                writeBuff.Clear();
                writeBuff.Add(Convert.ToByte((dataSize) & 0xFF));
                writeBuff.Add(Convert.ToByte((dataSize >> 8) & 0xFF));
                writeBuff.Add(Convert.ToByte((dataSize >> 16) & 0xFF));
                writeBuff.Add(Convert.ToByte((dataSize >> 24) & 0xFF));
                writeStream.Seek(0, SeekOrigin.Begin);
                writeStream.Write(writeBuff.ToArray(), 0, writeBuff.Count);
                #endregion
            }
            if (!flag)
            {
                File.Delete(filePath);
                this.WorkError("没有调光数据");
            }
            this.WorkCompleted();
        }

        private void ReadOldMusicFile()
        {
            //清楚缓存文件
            for (int channelNo = 1; channelNo < 513; channelNo++)
            {
                string cacheFileName = TransitionCacheFilePath + @"\M-" + channelNo + @".bin";
                if (File.Exists(cacheFileName))
                {
                    File.Delete(cacheFileName);
                }
            }
            //读取旧文件数据
            using (FileStream stream = new FileStream(this.SouceFilePath, FileMode.Open))
            {
                Dictionary<int, List<byte>> framDatas = new Dictionary<int, List<byte>>();
                if (stream.Length > 520)
                {
                    byte[] buff = new byte[520];
                    stream.Seek(520, SeekOrigin.Begin);
                    while (stream.Read(buff, 0, buff.Length) == 520)
                    {
                        if (framDatas[1].Count > 1024 * 4)
                        {
                            //写缓存数据
                            for (int channelIndex = 0; channelIndex < 513; channelIndex++)
                            {
                                this.WriteMusicCacheDataToFile(channelIndex + 0, framDatas[channelIndex]);
                            }
                            //清空缓存
                            framDatas = new Dictionary<int, List<byte>>();
                        }
                        for (int index = 2; index < 514; index++)
                        {
                            if (!framDatas.ContainsKey(index))
                            {
                                framDatas.Add(index - 1, new List<byte>());
                            }
                            else
                            {
                                framDatas[index - 1].Add(buff[index]);
                            }
                        }
                    }
                }
            }
            //缓存写入完成，整合缓存文件
            string filePath = this.DirPath + @"\" + this.FileName + @".bin";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            bool flag = false;
            File.Create(filePath).Dispose();
            using (FileStream writeStream = new FileStream(filePath, FileMode.Append))
            {
                List<byte> writeBuff = new List<byte>();
                long dataSize = 0;
                #region 写新文件的文件头
                writeBuff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 4));
                writeBuff.AddRange(Config.GetHeadData());
                dataSize += writeBuff.Count;
                #endregion
                #region 读取缓存文件数据写到新文件中
                for (int channelNo = 1; channelNo < 513; channelNo++)
                {
                    if (((MusicSceneConfig)this.Config).MusicChannelStatus[channelNo - 1])
                    {
                        string cacheFileName = TransitionCacheFilePath + @"\M-" + channelNo + @".bin";
                        if (File.Exists(cacheFileName))
                        {
                            using (FileStream readStream = new FileStream(cacheFileName, FileMode.Open))
                            {
                                flag = true;
                                long fileSize = readStream.Length;
                                byte[] readBuff = new byte[fileSize];
                                readStream.Read(readBuff, 0, readBuff.Length);
                                writeBuff.Add(Convert.ToByte((channelNo) & 0xFF));
                                writeBuff.Add(Convert.ToByte((channelNo >> 8) & 0xFF));
                                writeBuff.Add(Convert.ToByte((fileSize) & 0xFF));
                                writeBuff.Add(Convert.ToByte((fileSize >> 8) & 0xFF));
                                writeBuff.Add(Convert.ToByte((dataSize + 8) & 0xFF));
                                writeBuff.Add(Convert.ToByte(((dataSize + 8) >> 8) & 0xFF));
                                writeBuff.Add(Convert.ToByte(((dataSize + 8) >> 16) & 0xFF));
                                writeBuff.Add(Convert.ToByte(((dataSize + 8) >> 24) & 0xFF));
                                dataSize += fileSize + 8;
                                writeBuff.AddRange(readBuff);
                            }
                        }
                        writeStream.Write(writeBuff.ToArray(), 0, writeBuff.Count);
                        writeBuff.Clear();
                    }
                }
                #endregion
                #region 写文件大小
                writeBuff.Clear();
                writeBuff.Add(Convert.ToByte((dataSize) & 0xFF));
                writeBuff.Add(Convert.ToByte((dataSize >> 8) & 0xFF));
                writeBuff.Add(Convert.ToByte((dataSize >> 16) & 0xFF));
                writeBuff.Add(Convert.ToByte((dataSize >> 24) & 0xFF));
                writeStream.Seek(0, SeekOrigin.Begin);
                writeStream.Write(writeBuff.ToArray(), 0, writeBuff.Count);
                #endregion
            }
            if (!flag)
            {
                File.Delete(filePath);
                this.WorkError("没有调光数据");
            }
            this.WorkCompleted();
        }

        private void ReadOldRecordBasicFile()
        {
            bool flag = false;
            if (File.Exists(this.DirPath + @"\" + this.FileName + @".bin"))
            {
                File.Delete(this.DirPath + @"\" + this.FileName + @".bin");
            }
            File.Create(this.DirPath + @"\" + this.FileName + @".bin").Dispose();
            using (FileStream writeStream = new FileStream(this.DirPath + @"\" + this.FileName + @".bin",FileMode.Append))
            {
                using (FileStream readStream = new FileStream(this.SouceFilePath, FileMode.Open))
                {
                    if (readStream.Length >= 1040)
                    {
                        flag = true;
                        long dataSize = 0;
                        List<byte> writeBuff = new List<byte>();
                        writeBuff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 4));
                        writeBuff.AddRange(this.Config.GetHeadData());
                        //for (int index = 0; index < 512; index++)
                        //{
                        //    if ((this.Config as MusicSceneConfig).MusicChannelStatus[index])
                        //    {
                        //        writeBuff.Add(Convert.ToByte((index + 1) & 0xFF));
                        //        writeBuff.Add(Convert.ToByte((index + 1 >> 8) & 0xFF));
                        //        writeBuff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 6));
                        //    }
                        //}
                        dataSize += writeBuff.Count;
                        writeStream.Write(writeBuff.ToArray(), 0, writeBuff.Count);
                        writeBuff.Clear();
                        byte[] readBuff = new byte[520];
                        readStream.Seek(520, SeekOrigin.Begin);
                        while (readStream.Read(readBuff, 0, readBuff.Length) == 520)
                        {
                            dataSize += readBuff.Length;
                            writeStream.Write(readBuff, 0, readBuff.Length);
                        }
                        writeStream.Seek(0, SeekOrigin.Begin);
                        writeBuff.Add(Convert.ToByte((dataSize) & 0xFF));
                        writeBuff.Add(Convert.ToByte(((dataSize) >> 8) & 0xFF));
                        writeBuff.Add(Convert.ToByte(((dataSize) >> 16) & 0xFF));
                        writeBuff.Add(Convert.ToByte(((dataSize) >> 24) & 0xFF));
                    }
                }
            }
            if (!flag)
            {
                File.Delete(this.DirPath + @"\" + this.FileName + @".bin");
                this.WorkError("没有调光数据");
            }
            this.WorkCompleted();
        }
        private void ReadOldRecordMusicFile()
        {
            bool flag = false;
            if (File.Exists(this.DirPath + @"\" + this.FileName + @".bin"))
            {
                File.Delete(this.DirPath + @"\" + this.FileName + @".bin");
            }
            File.Create(this.DirPath + @"\" + this.FileName + @".bin").Dispose();
            using (FileStream writeStream = new FileStream(this.DirPath + @"\" + this.FileName + @".bin", FileMode.Append))
            {
                using (FileStream readStream = new FileStream(this.SouceFilePath, FileMode.Open))
                {
                    if (readStream.Length >= 1040)
                    {
                        flag = true;
                        long dataSize = 0;
                        List<byte> writeBuff = new List<byte>();
                        writeBuff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 4));
                        writeBuff.AddRange((this.Config).GetHeadData());
                        for (int index = 0; index < 512; index++)
                        {
                            if ((this.Config as MusicSceneConfig).MusicChannelStatus[index])
                            {
                                writeBuff.Add(Convert.ToByte((index + 1) & 0xFF));
                                writeBuff.Add(Convert.ToByte((index + 1 >> 8) & 0xFF));
                                writeBuff.AddRange(Enumerable.Repeat(Convert.ToByte(0x00), 6));
                            }
                        }
                        dataSize += writeBuff.Count;
                        writeStream.Write(writeBuff.ToArray(), 0, writeBuff.Count);
                        writeBuff.Clear();
                        byte[] readBuff = new byte[520];
                        readStream.Seek(520, SeekOrigin.Begin);
                        while (readStream.Read(readBuff, 0, readBuff.Length) == 520)
                        {
                            dataSize += readBuff.Length;
                            writeStream.Write(readBuff, 0, readBuff.Length);
                        }
                        writeStream.Seek(0, SeekOrigin.Begin);
                        writeBuff.Add(Convert.ToByte((dataSize) & 0xFF));
                        writeBuff.Add(Convert.ToByte(((dataSize) >> 8) & 0xFF));
                        writeBuff.Add(Convert.ToByte(((dataSize) >> 16) & 0xFF));
                        writeBuff.Add(Convert.ToByte(((dataSize) >> 24) & 0xFF));
                    }
                }
            }
            if (!flag)
            {
                File.Delete(this.DirPath + @"\" + this.FileName + @".bin");
                this.WorkError("没有调光数据");
            }
            this.WorkCompleted();
        }

        private void WorkCompleted()
        {
            this.InitParam();
            this.Completed_Event();
        }

        private void WorkError(string msg)
        {
            this.StopWorking(msg);
        }

        private void WriteBasicCacheDataToFile(int channelNo,List<byte> datas)
        {
            string cacheFileName = TransitionCacheFilePath + @"\C-" + channelNo + @".bin";
            using (FileStream stream = new FileStream(cacheFileName, FileMode.OpenOrCreate))
            {
                stream.Write(datas.ToArray(), 1, datas.Count);
            }
        }

        private void WriteMusicCacheDataToFile(int channelNo, List<byte> datas)
        {
            string cacheFileName = TransitionCacheFilePath + @"\M-" + channelNo + @".bin";
            using (FileStream stream = new FileStream(cacheFileName, FileMode.OpenOrCreate))
            {
                stream.Write(datas.ToArray(), 1, datas.Count);
            }
        }
    }

    public class BasicSceneConfig : SceneConfig
    {
        public bool YMOpenStatus { get; set; }
        public int YMIntervalTime { get; set; }
        public int YMRunTime { get; set; }
        public int BasicChannelCount { get; set; }
        public BasicSceneConfig(bool ymOpenStatus,int ymIntervalTime,int ymRunTime)
        {
            this.YMOpenStatus = ymOpenStatus;
            this.YMIntervalTime = ymIntervalTime;
            this.YMRunTime = ymRunTime;
            this.BasicChannelCount = 512;
        }
        public byte[] GetHeadData()
        {
            List<byte> dataBuff = new List<byte>();
            dataBuff.Add(Convert.ToByte(YMOpenStatus ? 1 : 0));
            dataBuff.Add(Convert.ToByte(YMIntervalTime & 0xFF));
            dataBuff.Add(Convert.ToByte((YMIntervalTime >> 8) & 0xFF));
            dataBuff.Add(Convert.ToByte(YMRunTime & 0xFF));
            dataBuff.Add(Convert.ToByte((YMRunTime >> 8) & 0xFF));
            dataBuff.Add(Convert.ToByte(BasicChannelCount & 0xFF));
            dataBuff.Add(Convert.ToByte((BasicChannelCount >> 8) & 0xFF));
            return dataBuff.ToArray();
        }
    }

    public class MusicSceneConfig : SceneConfig
    {
        public bool[] MusicChannelStatus { get; set; }
        public int MusicStepTime { get; set; }
        public int MusicIntervalTime { get; set; }
        public int MusicSteoListCount { get; set; }
        public int[] MusitStepList { get; set; }
        public int MusicChannelCount { get; set; }
        public MusicSceneConfig(bool[] channelStatus,int stepTime,int intervalTime,int[] stepList)
        {
            this.MusicChannelStatus = channelStatus;
            this.MusicStepTime = stepTime;
            this.MusicIntervalTime = intervalTime;
            this.MusitStepList = stepList;
            this.MusicSteoListCount = stepList.Count();
            this.MusicChannelCount = 0;
            foreach (bool item in channelStatus)
            {
                if (item)
                {
                    this.MusicChannelCount++;
                }
            }
        }

        public MusicSceneConfig(string basicPath)
        {
            this.MusicChannelStatus = Enumerable.Repeat(false, 512).ToArray();
            Console.WriteLine("文件是否存在：" + File.Exists(basicPath));
            using (FileStream stream = new FileStream(basicPath,FileMode.Open))
            {
                byte[] readBuff = new byte[520];
                stream.Read(readBuff, 0, readBuff.Length);
                for (int index = 1; index < 65; index++)
                {
                    string bitStr = StringHelper.DecimalStringToBitBinary(readBuff[index].ToString(), 8);
                    for (int bitIndex = 0; bitIndex < 8; bitIndex++)
                    {
                        this.MusicChannelStatus[(index - 1) * 8 + bitIndex] = bitStr[bitIndex].Equals("1");
                    }
                }
            }
        }

        public byte[] GetHeadData()
        {
            List<byte> dataBuff = new List<byte>();
            dataBuff.Add(Convert.ToByte(MusicStepTime & 0xFF));
            dataBuff.Add(Convert.ToByte((MusicIntervalTime) & 0xFF));
            dataBuff.Add(Convert.ToByte((MusicIntervalTime >> 8) & 0xFF));
            dataBuff.Add(Convert.ToByte((MusicSteoListCount) & 0xFF));
            for (int index = 0; index < 20; index++)
            {
                if (index + 1 >= MusicSteoListCount)
                {
                    dataBuff.Add(Convert.ToByte(0 & 0xFF));
                }
                else
                {
                    dataBuff.Add(Convert.ToByte(MusitStepList[index] & 0xFF));
                }
            }
            dataBuff.Add(Convert.ToByte((MusicChannelCount) & 0xFF));
            dataBuff.Add(Convert.ToByte((MusicChannelCount >> 8) & 0xFF));
            return dataBuff.ToArray();
        }
    }

    public enum FileKind
    {
        Project_Basic,Project_Music,Record_Basic,Record_Music
    }

    public interface SceneConfig
    {
        byte[] GetHeadData();
        //void WriteHeadDataToFile();
    }
}
