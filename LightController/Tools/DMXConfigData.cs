using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    public class DMXConfigData
    {
        public int FileSize { get; set; }
        public int Light_Total_Count { get; set; }
        public int DMX512_Chanel_Count { get; set; }
        public int Default_Scene_Number { get; set; }
        public int[] Music_Control_Enable { get; set; }
        public int Scene_Change_Mode { get; set; }
        public int Time_Factor { get; set; }
        public int[] C_Sence_Speed { get; set; }
        public Config_Combine_Scenes[] Combine_Scenes { get; set; }
        public Config_Light[] Lights { get; set; }

        public DMXConfigData()
        {

        }

        public void WriteToFile(string path)
        {
            string filePath = path + "Config.bin";
            int FileSize = 0;
            IList<byte> data = new List<byte>();
            //预填充文件大小
            data.Add(Convert.ToByte(FileSize));
            data.Add(Convert.ToByte(FileSize));
            data.Add(Convert.ToByte(FileSize));
            data.Add(Convert.ToByte(FileSize));
            //添加灯具总数
            data.Add(Convert.ToByte(Light_Total_Count & 0xFF));
            data.Add(Convert.ToByte((Light_Total_Count >> 8) & 0xFF));
            //添加DMX512通道总数
            data.Add(Convert.ToByte(DMX512_Chanel_Count));
            //添加开机自动播放场景
            data.Add(Convert.ToByte(Default_Scene_Number));
            //添加音频功能开启状态
            string music_control_enable = "";
            for (int i = 0; i < Music_Control_Enable.Length; i++)
            {
                music_control_enable = music_control_enable + Music_Control_Enable[i];
                if ((i + 1) % 8 == 0)
                {
                    data.Add(Convert.ToByte(music_control_enable, 2));
                    music_control_enable = "";
                }
            }
            //添加场景切换模式
            data.Add(Convert.ToByte(Scene_Change_Mode));
            //添加时间因子
            data.Add(Convert.ToByte(Time_Factor));
            //添加场景1声控模式常规程序速度
            foreach (int value in C_Sence_Speed)
            {
                data.Add(Convert.ToByte(value));

            }
            //添加场景组合播放数据
            foreach (Config_Combine_Scenes value in Combine_Scenes)
            {
                //添加主场景编号
                data.Add(Convert.ToByte(value.Scene_Main_Number));
                //添加组合场景播放功能开启状态
                data.Add(Convert.ToByte(value.Combine_Scene_Enable));
                //添加连播次数
                data.Add(Convert.ToByte(value.Play_Count & 0xFF));
                data.Add(Convert.ToByte((value.Play_Count >> 8) & 0xFF));
                //添加主场景播放时间
                data.Add(Convert.ToByte(value.Play_Time_Main_Scene & 0xFF));
                data.Add(Convert.ToByte((value.Play_Time_Main_Scene >> 8) & 0xFF));
                //添加副场景1编号
                data.Add(Convert.ToByte(value.Scene_One_Number));
                //添加副场景1播放时间
                data.Add(Convert.ToByte(value.Play_Time_Scene_One & 0xFF));
                data.Add(Convert.ToByte((value.Play_Time_Scene_One >> 8) & 0xFF));
                //添加副场景2编号
                data.Add(Convert.ToByte(value.Scene_Two_Number));
                //添加副场景2播放时间
                data.Add(Convert.ToByte(value.Play_Time_Scene_Two & 0xFF));
                data.Add(Convert.ToByte((value.Play_Time_Scene_Two >> 8) & 0xFF));
                //添加副场景3编号
                data.Add(Convert.ToByte(value.Scene_Three_Number));
                //添加副场景3播放时间
                data.Add(Convert.ToByte(value.Play_Time_Scene_Three & 0xFF));
                data.Add(Convert.ToByte((value.Play_Time_Scene_Three >> 8) & 0xFF));
                //添加副场景4编号
                data.Add(Convert.ToByte(value.Scene_Four_Number));
                //添加副场景4播放时间
                data.Add(Convert.ToByte(value.Play_Time_Scene_Four & 0xFF));
                data.Add(Convert.ToByte((value.Play_Time_Scene_Four >> 8) & 0xFF));
            }
            //添加灯具信息
            foreach (Config_Light value in Lights)
            {
                //添加灯具编号
                data.Add(Convert.ToByte(value.Chanel_Number & 0xFF));
                data.Add(Convert.ToByte((value.Chanel_Number >> 8) & 0xFF));
                //添加灯具起始地址
                data.Add(Convert.ToByte(value.Start_Address & 0xFF));
                data.Add(Convert.ToByte((value.Start_Address >> 8) & 0xFF));
                //添加灯具通道数
                data.Add(Convert.ToByte(value.Chanel_Count & 0xFF));
                data.Add(Convert.ToByte((value.Chanel_Count >> 8) & 0xFF));
            }
            FileSize = data.Count();
            data[0] = Convert.ToByte(FileSize & 0xFF);
            data[1] = Convert.ToByte((FileSize >> 8) & 0xFF);
            data[2] = Convert.ToByte((FileSize >> 16) & 0xFF);
            data[3] = Convert.ToByte((FileSize >> 24) & 0xFF);
            FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
            fileStream.Write(data.ToArray(), 0, data.ToArray().Length);
            fileStream.Close();
        }
    }
    public class Config_Combine_Scenes
    {
        public int Scene_Main_Number { get; set; }
        public int Combine_Scene_Enable { get; set; }
        public int Play_Count { get; set; }
        public int Play_Time_Main_Scene { get; set; }
        public int Scene_One_Number { get; set; }
        public int Play_Time_Scene_One { get; set; }
        public int Scene_Two_Number { get; set; }
        public int Play_Time_Scene_Two { get; set; }
        public int Scene_Three_Number { get; set; }
        public int Play_Time_Scene_Three { get; set; }
        public int Scene_Four_Number { get; set; }
        public int Play_Time_Scene_Four { get; set; }
    }
    public class Config_Light
    {
        public int Chanel_Number { get; set; }
        public int Start_Address { get; set; }
        public int Chanel_Count { get; set; }
    }

}
