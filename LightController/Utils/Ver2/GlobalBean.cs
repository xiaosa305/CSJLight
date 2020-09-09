using DMX512;
using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Utils.Ver2
{
    public class GlobalBean
    {
        private const int SCENECOUNTMAX = 32;
        public int ChannelCount { get; set; }//DMX通道总数
        public int StartUpScene { get; set; }//开机场景
        public int FrameIntervalTime { get; set; }//帧间隔时间
        public int SequencerMode { get; set; }//场景切换模式
        public List<Multiple> Multiples { get; set; }//组合场景数据
        public List<MusicSceneSet> MusicSceneSets { get; set; }//音频步数数据
        public List<ShakeMic> ShakeMics { get; set; }//摇麦数据
        public List<LightInfo> LightInfos { get; set; }//灯具数据

        public GlobalBean(string configPath,IList<DB_Light> dB_Lights)
        {
            string strValue = string.Empty;
            int iValue = 0;
            this.Multiples = new List<Multiple>();
            this.MusicSceneSets = new List<MusicSceneSet>();
            this.ShakeMics = new List<ShakeMic>();
            this.LightInfos = new List<LightInfo>();
            #region 读取配置文件
            using (StreamReader reader = new StreamReader(configPath))
            {
                while (!reader.ReadLine().Equals("[Set]"))
                {
                    continue;
                }
                strValue = reader.ReadLine();
                int.TryParse(strValue.Split('=')[1], out  iValue);
                switch (iValue)
                {
                    case 1:
                        this.ChannelCount = 384;
                        break;
                    case 2:
                        this.ChannelCount = 256;
                        break;
                    case 3:this.ChannelCount = 128;
                        break;
                    case 0:
                    default:
                        this.ChannelCount = 512;
                        break;
                }

                strValue = reader.ReadLine();
                int.TryParse(strValue.Split('=')[1], out  iValue);
                this.StartUpScene = iValue;

                strValue = reader.ReadLine();
                int.TryParse(strValue.Split('=')[1], out iValue);
                this.FrameIntervalTime = iValue;


                strValue = reader.ReadLine();
                int.TryParse(strValue.Split('=')[1], out iValue);
                this.SequencerMode = iValue;
              

                strValue = reader.ReadLine();
                if (strValue.Equals("[Multiple]"))
                {
                    strValue = reader.ReadLine();
                    do
                    {
                        Multiple multiple = new Multiple();
                        int.TryParse(strValue.Split('=')[1], out iValue);
                        multiple.IsOpen = iValue != 0;
                        if (strValue[1] >= '0' && strValue[1] <= '9')
                        {
                            int.TryParse(strValue[0].ToString() + strValue[1].ToString(), out iValue);
                        }
                        else
                        {
                            int.TryParse(strValue[0].ToString(), out iValue);
                        }
                        multiple.MainScenario = iValue;

                        strValue = reader.ReadLine();
                        int.TryParse(strValue.Split('=')[1], out iValue);
                        multiple.Repetitions = iValue;

                        strValue = reader.ReadLine();
                        int.TryParse(strValue.Split('=')[1], out iValue);
                        multiple.MainScenarioPlayTime = iValue;

                        strValue = reader.ReadLine();
                        int.TryParse(strValue.Split('=')[1], out iValue);
                        multiple.DeputyScenario1 = iValue;

                        strValue = reader.ReadLine();
                        int.TryParse(strValue.Split('=')[1], out iValue);
                        multiple.DeputyScenarioPlayTime1 = iValue;

                        strValue = reader.ReadLine();
                        int.TryParse(strValue.Split('=')[1], out iValue);
                        multiple.DeputyScenario2 = iValue;

                        strValue = reader.ReadLine();
                        int.TryParse(strValue.Split('=')[1], out iValue);
                        multiple.DeputyScenarioPlayTime2 = iValue;

                        strValue = reader.ReadLine();
                        int.TryParse(strValue.Split('=')[1], out iValue);
                        multiple.DeputyScenario3 = iValue;

                        strValue = reader.ReadLine();
                        int.TryParse(strValue.Split('=')[1], out iValue);
                        multiple.DeputyScenarioPlayTime3 = iValue;

                        strValue = reader.ReadLine();
                        int.TryParse(strValue.Split('=')[1], out iValue);
                        multiple.DeputyScenario4 = iValue;

                        strValue = reader.ReadLine();
                        int.TryParse(strValue.Split('=')[1], out iValue);
                        multiple.DeputyScenarioPlayTime4 = iValue;
                        this.Multiples.Add(multiple);
                        strValue = reader.ReadLine();
                    } while (!strValue.Equals("[SK]"));
                    strValue = reader.ReadLine();
                    for (int index = 0; index < SCENECOUNTMAX; index++)
                    {
                        MusicSceneSet musicSceneSet = new MusicSceneSet()
                        {
                            SceneNo = index,
                            MusicStepList = new List<int>()
                        };
                        strValue = strValue.Split('=')[1];
                        for (int strIndex = 0; strIndex < strValue.Length; strIndex++)
                        {
                            int.TryParse(strValue[strIndex].ToString(), out iValue);
                            if (iValue != 0)
                            {
                                musicSceneSet.MusicStepList.Add(iValue);
                            }
                        }

                        strValue = reader.ReadLine();
                        int.TryParse(strValue.Split('=')[1], out iValue);
                        musicSceneSet.MusicStepTime = iValue;

                        strValue = reader.ReadLine();
                        int.TryParse(strValue.Split('=')[1], out iValue);
                        musicSceneSet.MusicIntervalTime = iValue;
                        this.MusicSceneSets.Add(musicSceneSet);

                        strValue = reader.ReadLine();
                        if (strValue.Equals("[YM]"))
                        {
                            break;
                        }
                    }
                    for (int index = 0; index < SCENECOUNTMAX; index++)
                    {
                        ShakeMic shakeMic = new ShakeMic()
                        {
                            SceneNo = index
                        };
                        strValue = reader.ReadLine();
                        int.TryParse(strValue.Split('=')[1], out iValue);
                        shakeMic.IsOpen = iValue != 0;

                        strValue = reader.ReadLine();
                        int.TryParse(strValue.Split('=')[1], out iValue);
                        shakeMic.IntervalTime = iValue;

                        strValue = reader.ReadLine();
                        int.TryParse(strValue.Split('=')[1], out iValue);
                        shakeMic.RunTime = iValue;

                        this.ShakeMics.Add(shakeMic);
                    }
                }
            }
            #endregion
            #region 读取灯具数据
            foreach (DB_Light item in dB_Lights)
            {
                LightInfo light = new LightInfo()
                {
                    LightNumber = item.LightNo,
                    StartAddress = item.StartID,
                    ChannelCount = item.Count
                };
                this.LightInfos.Add(light);
            }
            #endregion
            #region 写入文件
            ;
            #endregion
        }
    }
    public class Multiple
    {
        public bool IsOpen { get; set; }//开关状态
        public int Repetitions { get; set; }//重复次数
        public int MainScenario { get; set; }//主场景
        public int MainScenarioPlayTime { get; set; }//主场景播放时间
        public int DeputyScenario1 { get; set; }//组合场景1
        public int DeputyScenarioPlayTime1 { get; set; }//组合场景1播放时间
        public int DeputyScenario2 { get; set; }//组合场景2
        public int DeputyScenarioPlayTime2 { get; set; }//组合场景2播放时间
        public int DeputyScenario3 { get; set; }//组合场景3
        public int DeputyScenarioPlayTime3 { get; set; }//组合场景3播放时间
        public int DeputyScenario4 { get; set; }//组合场景4
        public int DeputyScenarioPlayTime4 { get; set; }//组合场景4播放时间
    }
    public class MusicSceneSet
    {
        public int SceneNo { get; set; }
        public List<int> MusicStepList { get; set; }
        public int MusicStepTime { get; set; }
        public int MusicIntervalTime { get; set; }
    }
    public class ShakeMic
    {
        public int SceneNo { get; set; }
        public bool IsOpen { get; set; }
        public int IntervalTime { get; set; }
        public int RunTime { get; set; }
    }
    public class LightInfo
    {
        public int LightNumber { get; set; }
        public int StartAddress { get; set; }
        public int ChannelCount { get; set; }
    }
}
