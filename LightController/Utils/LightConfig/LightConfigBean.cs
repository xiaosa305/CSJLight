using CCWin.SkinClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Utils.LightConfig
{
    public class LightConfigBean
    {
        public int StepInc { get; set; }
        public SceneConfigBean[] SceneConfigs { get; set; }

        public static void WriteToFile(string path,LightConfigBean lightConfig)
        {
            List<byte> buff = new List<byte>();
            buff.Add(Convert.ToByte(lightConfig.StepInc));
            for (int sceneNo = 0; sceneNo < lightConfig.SceneConfigs.Length; sceneNo++)
            {
                for (int channelNo = 0; channelNo < lightConfig.SceneConfigs[sceneNo].ChannelConfigs.Length; channelNo++)
                {
                    buff.Add(Convert.ToByte((lightConfig.SceneConfigs[sceneNo].ChannelConfigs[channelNo].ChannelNo) & 0xFF));
                    buff.Add(Convert.ToByte((lightConfig.SceneConfigs[sceneNo].ChannelConfigs[channelNo].ChannelNo >> 8) & 0xFF));
                    buff.Add(Convert.ToByte((lightConfig.SceneConfigs[sceneNo].ChannelConfigs[channelNo].IsOpen ? 1 : 0) & 0xFF));
                    buff.Add(Convert.ToByte((lightConfig.SceneConfigs[sceneNo].ChannelConfigs[channelNo].DefaultValue) & 0xFF));
                }
            }
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.Create(path).Dispose();
            using (FileStream stream = new FileStream(path,FileMode.Create))
            {
                stream.Write(buff.ToArray(), 0, buff.Count);
            }
        }

        public static LightConfigBean GetTestData()
        {
            SceneConfigBean[] sceneConfigs = new SceneConfigBean[32];
            for (int sceneNo = 0; sceneNo < 32; sceneNo++)
            {
                ChannelConfigBean[] channelConfigs = new ChannelConfigBean[10];
                for (int channelNo = 1; channelNo < 11; channelNo++)
                {
                    channelConfigs[channelNo - 1] = new ChannelConfigBean() { ChannelNo = channelNo, IsOpen = (sceneNo % 2 == 0), DefaultValue = 100 };
                }
                sceneConfigs[sceneNo] = new SceneConfigBean() { ChannelConfigs = channelConfigs };
            }
            LightConfigBean lightConfig = new LightConfigBean() { SceneConfigs = sceneConfigs, StepInc = 10 };
            return lightConfig;
        }
    }
    public class SceneConfigBean
    {
        public ChannelConfigBean[] ChannelConfigs { get; set; }
    }
    public class ChannelConfigBean
    {
        public bool IsOpen { get; set; }
        public int ChannelNo { get; set; }
        public int DefaultValue { get; set; }
    }
}
