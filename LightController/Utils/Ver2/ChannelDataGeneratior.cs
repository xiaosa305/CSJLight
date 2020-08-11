using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.Utils.Ver2
{
    public class ChannelDataGeneratior
    {
        public static ChannelDataGeneratior Instance { get; set; }
        private ChannelDataGeneratior()
        {
            this.InitParam();
        }
        public static ChannelDataGeneratior GetInstance()
        {
            if (null == Instance)
            {
                Instance = new ChannelDataGeneratior();
            }
            return Instance;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void InitParam()
        {
            ;
        }

        public void CreateSceneChannelFile(ChannelDataBean dataBean)
        {
            switch (dataBean.Mode)
            {
                case Mode.Basics:
                    this.CreateBasicSceneCacheFile(dataBean);
                    break;
                case Mode.Music:
                    this.CreateMusicSceneCacheFile(dataBean);
                    break;
            }
        }
        /// <summary>
        /// 创建基础场景通道碎片文件
        /// </summary>
        private void CreateBasicSceneCacheFile(ChannelDataBean dataBean)
        {
            string filePath = Application.StartupPath + @"\DataCache\Project\Cache\" + "C" + dataBean.SceneNo + "-" + dataBean.ChannelNo + ".bin";
            float inc;
            int stepValue;
            int stepMode;
            int startValue = dataBean.StepValues[0];
            int stepTime;
            float fValue;
            int iValue;
            List<byte> buff = new List<byte>();
            buff.Add(Convert.ToByte(dataBean.ChannelFlag == ChannelFlag.FineTune ? 0 : dataBean.StepValues[0]));
            for (int stepIndex = 0; stepIndex < dataBean.StepValues.Count + 1; stepIndex++)
            {

                int index = stepIndex == dataBean.StepCount ? 0 : stepIndex;
                stepValue = dataBean.StepValues[index];
                stepMode = dataBean.StepMode[index];
                stepTime = dataBean.StepTime[index];
                inc = (stepValue - startValue) / (float)stepTime;
                for (int fram = 0; fram < stepTime; fram++)
                {
                    if (stepIndex == dataBean.StepValues.Count && fram == stepTime - 1)
                    {
                        break;
                    }
                    else
                    {
                        if (stepMode == ChannelDataBean.MODE_C_GRADUAL)
                        {
                            fValue = startValue + inc * (fram + 1);
                            if (inc < 0)
                            {
                                fValue = fValue < 0 ? 0 : fValue;
                            }
                            else
                            {
                                fValue = fValue > 255 ? 255 : fValue;
                            }
                            iValue = (int)Math.Floor(fValue * 256);
                            if (dataBean.ChannelFlag == ChannelFlag.FineTune)
                            {
                                iValue = (int)((iValue & 0xFF) / (255.0 / dataBean.MaxValue));
                            }
                            else
                            {
                                iValue = (int)((iValue >> 8) & 0xFF);
                            }
                            buff.Add(Convert.ToByte(iValue));
                        }
                        else if (stepMode == ChannelDataBean.MODE_C_JUMP)
                        {
                            buff.Add(Convert.ToByte(dataBean.ChannelFlag == ChannelFlag.FineTune ? 0 : stepValue));

                        }
                    }
                }
            }
            using (FileStream stream = new FileStream(filePath,FileMode.Create))
            {
                stream.Write(buff.ToArray(), 0, buff.Count);
            }
        }

        /// <summary>
        /// 创建音频场景通道碎片文件
        /// </summary>
        private void CreateMusicSceneCacheFile(ChannelDataBean dataBean)
        {
            string filePath = Application.StartupPath + @"\DataCache\Project\Cache\" + "C" + dataBean.SceneNo + "-" + dataBean.ChannelNo + ".bin";
            List<byte> buff = new List<byte>();
            for (int stepIndex = 0; stepIndex < dataBean.StepValues.Count; stepIndex++)
            {
                if (dataBean.StepMode[stepIndex] == ChannelDataBean.MODE_M_JUMP)
                {
                    buff.Add(Convert.ToByte(dataBean.StepValues[stepIndex]));
                }
            }
            using (FileStream stream = new FileStream(filePath, FileMode.Create)) 
            {
                stream.Write(buff.ToArray(), 0, buff.Count);
            }
        }

        /// <summary>
        /// 合成工程文件
        /// </summary>
        public static void ProjectFileSynthesising()
        {
            ;
        }
    }

    public enum Mode
    {
        Basics,Music
    }

    public enum ChannelFlag
    {
        MainTune,FineTune,Null
    }
}
