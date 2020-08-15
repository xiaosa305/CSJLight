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
        private static readonly string PROJECT_DIRECTORY_PATH = @"\DataCache\Project\CSJ";
        private static readonly string PROJECT_CHANNEL_CACHE_DIRECTORY_PATH = @"\DataCache\Project\Cache";
        private static readonly string PREVIEW_DIRECTORY_PATH = @"\DataCache\Preview\CSJ";
        private static readonly string PREVIEW_CHANNEL_CACHE_DIRECTORY_PATH = @"\DataCache\Preview\Cache";
        private static ChannelDataGeneratior Instance { get; set; }
        private string DirctoryPath { get; set; }


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

        public void CreateSceneChannelFile(ChannelDataBean dataBean,string dirPath)
        {
            this.DirctoryPath = dirPath;
            switch (dataBean.Mode)
            {
                case Mode.Basics:
                    this.CreateProjectBasicSceneCacheFile(dataBean);
                    break;
                case Mode.Music:
                    this.CreateProjectMusicSceneCacheFile(dataBean);
                    break;
            }
        }

        public void CreatePreviewSceneChannelFile(ChannelDataBean dataBean,string dirPath)
        {
            this.DirctoryPath = dirPath;
            switch (dataBean.Mode)
            {
                case Mode.Basics:
                    this.CreatePreviewBasicSceneCacheFile(dataBean);
                    break;
                case Mode.Music:
                    this.CreatePreviewMusicSceneCacheFile(dataBean);
                    break;
            }
        }

        /// <summary>
        /// 创建工程基础场景通道碎片文件
        /// </summary>
        private void CreateProjectBasicSceneCacheFile(ChannelDataBean dataBean)
        {
            string filePath = this.DirctoryPath + PROJECT_CHANNEL_CACHE_DIRECTORY_PATH + @"\C" + dataBean.SceneNo + "-" + dataBean.ChannelNo + ".bin";
            float inc;
            int stepValue;
            int stepMode;
            int startValue = dataBean.StepValues[0];
            int stepTime;
            float fValue;
            int iValue;
            List<byte> buff = new List<byte>();
            if (!Directory.Exists(this.DirctoryPath + PROJECT_CHANNEL_CACHE_DIRECTORY_PATH))
            {
                Directory.CreateDirectory(this.DirctoryPath + PROJECT_CHANNEL_CACHE_DIRECTORY_PATH);
            }
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
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
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                stream.Write(buff.ToArray(), 0, buff.Count);
            }
        }

        /// <summary>
        /// 创建工程音频场景通道碎片文件
        /// </summary>
        private void CreateProjectMusicSceneCacheFile(ChannelDataBean dataBean)
        {
            string filePath = this.DirctoryPath + PROJECT_CHANNEL_CACHE_DIRECTORY_PATH + @"\M" + dataBean.SceneNo + "-" + dataBean.ChannelNo + ".bin";
            List<byte> buff = new List<byte>();
            if (!Directory.Exists(this.DirctoryPath + PROJECT_CHANNEL_CACHE_DIRECTORY_PATH))
            {
                Directory.CreateDirectory(this.DirctoryPath + PROJECT_CHANNEL_CACHE_DIRECTORY_PATH);
            }
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
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
        /// 创建预览基础场景通道碎片文件
        /// </summary>
        private void CreatePreviewBasicSceneCacheFile(ChannelDataBean dataBean)
        {
            string filePath = this.DirctoryPath + PREVIEW_CHANNEL_CACHE_DIRECTORY_PATH + @"\C1" + "-" + dataBean.ChannelNo + ".bin";
            float inc;
            int stepValue;
            int stepMode;
            int startValue = dataBean.StepValues[0];
            int stepTime;
            float fValue;
            int iValue;
            List<byte> buff = new List<byte>();
            if (!Directory.Exists(this.DirctoryPath + PREVIEW_CHANNEL_CACHE_DIRECTORY_PATH))
            {
                Directory.CreateDirectory(this.DirctoryPath + PREVIEW_CHANNEL_CACHE_DIRECTORY_PATH);
            }
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
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
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                stream.Write(buff.ToArray(), 0, buff.Count);
            }
        }

        /// <summary>
        /// 创建预览音频场景通道碎片文件
        /// </summary>
        private void CreatePreviewMusicSceneCacheFile(ChannelDataBean dataBean)
        {
            string filePath = this.DirctoryPath + PREVIEW_CHANNEL_CACHE_DIRECTORY_PATH + @"\M1" + "-" + dataBean.ChannelNo + ".bin";
            List<byte> buff = new List<byte>();
            if (!Directory.Exists(this.DirctoryPath + PREVIEW_CHANNEL_CACHE_DIRECTORY_PATH))
            {
                Directory.CreateDirectory(this.DirctoryPath + PREVIEW_CHANNEL_CACHE_DIRECTORY_PATH);
            }
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
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
        /// 合成工程场景文件
        /// </summary>
        public void ProjectFileSynthesising(int sceneNo)
        {
            BasicProjectFileSynthesising(sceneNo);
            MusicProjectFileSynthesising(sceneNo);
        }

        /// <summary>
        /// 合成预览场景文件
        /// </summary>
        public void PreviewFileSynthesising()
        {
            BasicPreviewFileSynthesising();
            MusicPreviewFileSynthesising();
        }

        /// <summary>
        /// 生成工程基础场景文件
        /// </summary>
        /// <param name="sceneNo"></param>
        private void BasicProjectFileSynthesising(int sceneNo)
        {
            if (!Directory.Exists(this.DirctoryPath + PROJECT_DIRECTORY_PATH))
            {
                Directory.CreateDirectory(this.DirctoryPath + PROJECT_DIRECTORY_PATH);
            }
        }

        /// <summary>
        /// 生成工程音频场景文件
        /// </summary>
        /// <param name="sceneNo"></param>
        private void MusicProjectFileSynthesising(int sceneNo)
        {
            if (!Directory.Exists(this.DirctoryPath + PROJECT_DIRECTORY_PATH))
            {
                Directory.CreateDirectory(this.DirctoryPath + PROJECT_DIRECTORY_PATH);
            }
        }

        /// <summary>
        /// 生成预览基础场景文件
        /// </summary>
        /// <param name="sceneNo"></param>
        private void BasicPreviewFileSynthesising()
        {
            if (!Directory.Exists(this.DirctoryPath + PREVIEW_DIRECTORY_PATH))
            {
                Directory.CreateDirectory(this.DirctoryPath + PREVIEW_DIRECTORY_PATH);
            }
        }

        /// <summary>
        /// 生成预览音频场景文件
        /// </summary>
        /// <param name="sceneNo"></param>
        private void MusicPreviewFileSynthesising()
        {
            if (!Directory.Exists(this.DirctoryPath + PREVIEW_DIRECTORY_PATH))
            {
                Directory.CreateDirectory(this.DirctoryPath + PREVIEW_DIRECTORY_PATH);
            }
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
