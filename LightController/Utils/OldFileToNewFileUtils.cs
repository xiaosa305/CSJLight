using LightController.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.Utils
{
    public class OldFileToNewFileUtils
    {
        private readonly string TransitionCacheFilePath = Application.StartupPath + @"\TransitionCacheFile";
        private readonly string TransitionFilePath = Application.StartupPath + @"\TransitionFile";
        public static OldFileToNewFileUtils Instance { get; set; }

        private OldFileToNewFileUtils()
        {

        }

        public static OldFileToNewFileUtils GetInstance()
        {
            if (Instance == null)
            {
                Instance = new OldFileToNewFileUtils();
            }
            return Instance;
        }

        public void ReadOldBasicFile(string filePath)
        {
            using (FileStream stream = new FileStream(filePath,FileMode.Open))
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
                    while (stream.Read(buff, 0, buff.Length) == 520)
                    {
                        if (isConfig)
                        {
                            //读取音频受控通道
                            for (int index = 1; index < 65; index++)
                            {
                                string value = StringHelper.DecimalStringToBitBinary(buff[index].ToString(), 8);
                                for (int valueIndex = 0; valueIndex < 8; valueIndex++)
                                {
                                    musicControlStatus[(index - 1) * 8 + valueIndex] = value[0].Equals('1');
                                }
                            }
                            //音频功能是否启用
                            isOpenMusic = buff[65];
                            musicSetpMode = buff[66];
                            isOpenMic = buff[67];
                            micFrequentness = buff[68];
                            micRunTime = buff[69];
                            isRelay = buff[70];
                            relayTime = buff[71];
                            relayNextScene = buff[72];
                            isConfig = false;
                            //写参数头
                            using (FileStream fileStream = new FileStream(TransitionFilePath + @"\C1.bin",FileMode.Create))
                            {
                                fileStream.Write(writeBuff.ToArray(), 0, writeBuff.Count);
                            }
                        }
                        else
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
        }

        public void ReadOldMusicFule(string filePath)
        {
            //using (FileStream stream = new FileStream())
            //{

            //}
        }

        private void WriteCacheDataToFile(int channelNo,List<byte> datas)
        {
          
        }
    }
}
