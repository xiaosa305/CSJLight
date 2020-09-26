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
        public static OldFileToNewFileUtils Instance { get; set; }
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
        }

        private void StartWorking()
        {
            this.WorkTimer.Start();
        }

        private void StopWorking(string msg)
        {
            this.WorkTimer.Stop();
            this.Error_Event(msg);
            this.InitParam();
        }


        public void ReadOldFile(string souceFilePath,string dirPath,string fileName,SceneConfig config,FileKind kind,Completed completed,Error error)
        {
            try
            {
                if (this.IsLeisure)
                {
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
                    this.ReadOldMusicFule();
                    break;
                case FileKind.Record_Basic:
                    this.ReadOldRecordBasicFile();
                    break;
                case FileKind.Record_Music:
                    this.ReadOldRecordMusicFile();
                    break;
            }
        }

        public void ReadOldBasicFile()
        {
            using (FileStream stream = new FileStream(this.SouceFilePath,FileMode.Open))
            {
                bool[] musicControlStatus = new bool[512];
                int isOpenMusic;
                int musicSetpMode;
                int isOpenMic;
                int micFrequentness;
                int micRunTime;
                int isRelay;
                int relayTime;
                int relayNextScene;
                Dictionary<int, List<byte>> framDatas = new Dictionary<int, List<byte>>();
                if (stream.Length > 520)
                {
                    bool isConfig = true;
                    byte[] buff = new byte[520];
                    List<byte> writeBuff = new List<byte>();
                    stream.Seek(520, SeekOrigin.Begin);
                    while (stream.Read(buff, 0, buff.Length) == 520)
                    {
                        if (framDatas[1].Count > 1024 * 4)
                        {
                            //写缓存数据
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
        }

        public void ReadOldMusicFule()
        {
            //using (FileStream stream = new FileStream())
            //{

            //}
        }

        public void ReadOldRecordBasicFile()
        {
            //写场景头

            //循环写dmx数据
        }

        public void ReadOldRecordMusicFile()
        {
            using (FileStream writeStream = new FileStream(this.DirPath + @"\" + this.FileName + @".bin",FileMode.OpenOrCreate))
            {
                //写音频头

                //循环写dmx数据
            }
        }

        private void WriteCacheDataToFile(int channelNo,List<byte> datas)
        {
            string cacheFileName = TransitionCacheFilePath + @"\" + channelNo + @".bin";
            using (FileStream stream = new FileStream(cacheFileName, FileMode.OpenOrCreate))
            {
                stream.Write(datas.ToArray(), 0, datas.Count);
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
            throw new NotImplementedException();
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

        public byte[] GetHeadData()
        {
            throw new NotImplementedException();
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
