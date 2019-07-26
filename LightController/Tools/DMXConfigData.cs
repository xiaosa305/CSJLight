using DMX512;
using LightController.Ast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    public class DMXConfigData
    {
        //文件大小
        public int FileSize { get; set; }
        //灯具总数
        public int Light_Total_Count { get; set; }
        //DMX512通道总数
        public int DMX512_Chanel_Count { get; set; }
        //开机自动播放场景
        public int Default_Scene_Number { get; set; }
        //音频功能开启
        public IList<int> Music_Control_Enable { get; set; }
        //场景切换模式
        public int Scene_Change_Mode { get; set; }
        //时间因子
        public int TimeFactory { get; set; }
        //场景组合播放数据
        public Config_Combine_Scene[] Combine_Scenes { get; set; }
        //灯具数据
        public IList<Config_Light> Lights { get; set; }

        private IList<DMX_C_File> C_Files { get; set; }
        private IList<DMX_M_File> M_Files { get; set; }
        private StreamReader Reader { get; set; }
        private IList<DB_Light> DB_Lights { get; set; }
        private string FilePath { get; set; }

        private int[] Ram_Enabl { get; set; }
        private int[] Ram_Response_Times { get; set; }
        private int[] Ram_Play_Times { get; set; }

        public DMXConfigData(DBWrapper dBWrapper, string filePath)
        {
            C_Files = DMXTools.GetInstance().Get_C_Files(FormatTools.GetInstance().GetC_SceneDatas(dBWrapper), filePath);
            M_Files = DMXTools.GetInstance().Get_M_Files(FormatTools.GetInstance().GetM_SceneDatas(dBWrapper), filePath);
            Combine_Scenes = new Config_Combine_Scene[9];
            DB_Lights = dBWrapper.lightList;
            Music_Control_Enable = new List<int>();
            FilePath = filePath;
            ReadFromFile();
            Ram_Enabl = new int[24];
            Ram_Response_Times = new int[24];
            Ram_Play_Times = new int[24];
        }

        public void WriteToFile(string path)
        {
            string filePath = path + @"\Config.bin";
            byte[] Data = GetConfigData();
            FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
            fileStream.Write(Data, 0, Data.Length);
            fileStream.Close();
        }

        public byte[] GetConfigData()
        {
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
            for (int i = 0; i < Music_Control_Enable.Count(); i++)
            {
                int value = (Music_Control_Enable[i] == 0) ? 0 : 1;
                music_control_enable = music_control_enable + value;
                if ((i + 1) % 8 == 0)
                {
                    data.Add(Convert.ToByte(music_control_enable, 2));
                    music_control_enable = "";
                }
            }
            data.Add(Convert.ToByte(0x00));
            //添加场景切换模式
            data.Add(Convert.ToByte(Scene_Change_Mode));
            //添加时间因子
            data.Add(Convert.ToByte(TimeFactory));
            //添加摇麦信息
            for (int i = 0; i < 24; i++)
            {
                byte ram_Enabl = Convert.ToByte(Ram_Enabl[i]);
                byte[] ram_Response_Times = new byte[2];
                ram_Response_Times[0] = Convert.ToByte(((Ram_Response_Times[i] + 1) * 60) & 0xFF);
                ram_Response_Times[1] = Convert.ToByte((((Ram_Response_Times[i] + 1) * 60) >> 8) & 0xFF);
                byte[] ram_Play_Times = new byte[2];
                ram_Play_Times[0] = Convert.ToByte((Ram_Play_Times[i] + 1) & 0xFF);
                ram_Play_Times[1] = Convert.ToByte(((Ram_Play_Times[i] + 1) >> 8) & 0xFF);
                data.Add(ram_Enabl);
                data.Add(ram_Response_Times[0]);
                data.Add(ram_Response_Times[1]);
                data.Add(ram_Play_Times[0]);
                data.Add(ram_Play_Times[1]);
            }
            //添加场景组合播放数据
            foreach (Config_Combine_Scene value in Combine_Scenes)
            {
                //添加组合场景播放功能开启状态
                data.Add(Convert.ToByte(value.Combine_Scene_Enable));
                //添加连播次数
                data.Add(Convert.ToByte(value.Play_Count & 0xFF));
                data.Add(Convert.ToByte((value.Play_Count >> 8) & 0xFF));
                //添加主场景编号
                data.Add(Convert.ToByte(value.Scene_Main_Number));
                //添加主场景播放模式
                //data.Add(Convert.ToByte(value.Play_Mode_Main));
                //添加主场景播放时间
                data.Add(Convert.ToByte(value.Play_Time_Main_Scene & 0xFF));
                data.Add(Convert.ToByte((value.Play_Time_Main_Scene >> 8) & 0xFF));
                //添加副场景1编号
                data.Add(Convert.ToByte(value.Scene_One_Number));
                //添加副场景1播放模式
                //data.Add(Convert.ToByte(value.Play_Mode_One));
                //添加副场景1播放时间
                data.Add(Convert.ToByte(value.Play_Time_Scene_One & 0xFF));
                data.Add(Convert.ToByte((value.Play_Time_Scene_One >> 8) & 0xFF));
                //添加副场景2编号
                data.Add(Convert.ToByte(value.Scene_Two_Number));
                //添加副场景2播放模式
                //data.Add(Convert.ToByte(value.Play_Mode_Two));
                //添加副场景2播放时间
                data.Add(Convert.ToByte(value.Play_Time_Scene_Two & 0xFF));
                data.Add(Convert.ToByte((value.Play_Time_Scene_Two >> 8) & 0xFF));
                //添加副场景3编号
                data.Add(Convert.ToByte(value.Scene_Three_Number));
                //添加副场景3播放模式
                //data.Add(Convert.ToByte(value.Play_Mode_Three));
                //添加副场景3播放时间
                data.Add(Convert.ToByte(value.Play_Time_Scene_Three & 0xFF));
                data.Add(Convert.ToByte((value.Play_Time_Scene_Three >> 8) & 0xFF));
                //添加副场景4编号
                data.Add(Convert.ToByte(value.Scene_Four_Number));
                //添加副场景4播放模式
                //data.Add(Convert.ToByte(value.Play_Mode_Four));
                //添加副场景4播放时间
                data.Add(Convert.ToByte(value.Play_Time_Scene_Four & 0xFF));
                data.Add(Convert.ToByte((value.Play_Time_Scene_Four >> 8) & 0xFF));
            }
            //添加灯具信息
            foreach (Config_Light value in Lights)
            {
                //添加灯具编号
                data.Add(Convert.ToByte(value.Light_Number & 0xFF));
                data.Add(Convert.ToByte((value.Light_Number >> 8) & 0xFF));
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

            return data.ToArray();
        }



        private void ReadFromFile()
        {
            string lineStr = "";
            string strValue;
            int intValue;
            Lights = new List<Config_Light>();
            try
            {
                using (Reader = new StreamReader(FilePath))
                {
                    lineStr = Reader.ReadLine();
                    if (lineStr.Equals("[QD]"))
                    {
                        for (int i = 0; i < 24; i++)
                        {
                            Reader.ReadLine();
                        }

                        lineStr = Reader.ReadLine();
                    }
                    if (lineStr.Equals("[Set]"))
                    {
                        strValue = (Reader.ReadLine().Split('='))[1];
                        int.TryParse(strValue, out intValue);
                        DMX512_Chanel_Count = intValue;
                        strValue = (Reader.ReadLine().Split('='))[1];
                        int.TryParse(strValue, out intValue);
                        Default_Scene_Number = intValue;
                        strValue = (Reader.ReadLine().Split('='))[1];
                        int.TryParse(strValue, out intValue);
                        TimeFactory = intValue;
                        strValue = (Reader.ReadLine().Split('='))[1];
                        int.TryParse(strValue, out intValue);
                        Scene_Change_Mode = intValue;
                        lineStr = Reader.ReadLine();
                    }
                    if (lineStr.Equals("[Multiple]"))
                    {
                        lineStr = Reader.ReadLine();
                        Config_Combine_Scene combine_Scene = new Config_Combine_Scene();
                        //获取主场景编号
                        strValue = lineStr[0] + "";
                        int.TryParse(strValue, out intValue);
                        combine_Scene.Scene_Main_Number = intValue;
                        //获取场景组合播放开启状态
                        strValue = (lineStr.Split('='))[1];
                        int.TryParse(strValue, out intValue);
                        combine_Scene.Combine_Scene_Enable = intValue;
                        //获取连播次数
                        strValue = (Reader.ReadLine().Split('='))[1];
                        int.TryParse(strValue, out intValue);
                        combine_Scene.Play_Count = intValue;
                        //获取主场景播放模式
                        strValue = (Reader.ReadLine().Split('='))[1];
                        int.TryParse(strValue, out intValue);
                        combine_Scene.Play_Mode_Main = intValue;
                        //获取主场景播放时间
                        strValue = (Reader.ReadLine().Split('='))[1];
                        int.TryParse(strValue, out intValue);
                        combine_Scene.Play_Time_Main_Scene = intValue;
                        //获取副场景1编号
                        strValue = (Reader.ReadLine().Split('='))[1];
                        int.TryParse(strValue, out intValue);
                        combine_Scene.Scene_One_Number = intValue;
                        //获取副场景1播放模式
                        strValue = (Reader.ReadLine().Split('='))[1];
                        int.TryParse(strValue, out intValue);
                        combine_Scene.Play_Mode_One = intValue;
                        //获取副场景1播放时间
                        strValue = (Reader.ReadLine().Split('='))[1];
                        int.TryParse(strValue, out intValue);
                        combine_Scene.Play_Time_Scene_One = intValue;
                        //获取副场景2编号
                        strValue = (Reader.ReadLine().Split('='))[1];
                        int.TryParse(strValue, out intValue);
                        combine_Scene.Scene_Two_Number = intValue;
                        //获取副场景2播放模式
                        strValue = (Reader.ReadLine().Split('='))[1];
                        int.TryParse(strValue, out intValue);
                        combine_Scene.Play_Mode_Two = intValue;
                        //获取副场景2播放时间
                        strValue = (Reader.ReadLine().Split('='))[1];
                        int.TryParse(strValue, out intValue);
                        combine_Scene.Play_Time_Scene_Two = intValue;
                        //获取副场景3编号
                        strValue = (Reader.ReadLine().Split('='))[1];
                        int.TryParse(strValue, out intValue);
                        combine_Scene.Scene_Three_Number = intValue;
                        //获取副场景3播放模式
                        strValue = (Reader.ReadLine().Split('='))[1];
                        int.TryParse(strValue, out intValue);
                        combine_Scene.Play_Mode_Three = intValue;
                        //获取副场景3播放时间
                        strValue = (Reader.ReadLine().Split('='))[1];
                        int.TryParse(strValue, out intValue);
                        combine_Scene.Play_Time_Scene_Three = intValue;
                        //获取副场景4编号
                        strValue = (Reader.ReadLine().Split('='))[1];
                        int.TryParse(strValue, out intValue);
                        combine_Scene.Scene_Four_Number = intValue;
                        //获取副场景4播放模式
                        strValue = (Reader.ReadLine().Split('='))[1];
                        int.TryParse(strValue, out intValue);
                        combine_Scene.Play_Mode_Four = intValue;
                        //获取副场景4播放时间
                        strValue = (Reader.ReadLine().Split('='))[1];
                        int.TryParse(strValue, out intValue);
                        combine_Scene.Play_Time_Scene_Four = intValue;
                        //将场景组合播放数据进行存放
                        Combine_Scenes[combine_Scene.Scene_Main_Number] = combine_Scene;
                        //读取下一条是否为下一个场景组合数据，不是则结束循环
                        lineStr = Reader.ReadLine();
                        if (lineStr.Equals("[SK]"))
                        {
                            //添加音频功能开启状态
                            for (int i = 0; i < 24; i++)
                            {
                                lineStr = Reader.ReadLine();
                                strValue = lineStr.Split('=')[1];
                                int.TryParse(strValue, out intValue);
                                Music_Control_Enable.Add(intValue);
                            }
                        }
                        if (lineStr.Equals("[YM]"))
                        {
                            for (int i = 0; i < 24; i++)
                            {
                                lineStr = Reader.ReadLine();
                                strValue = lineStr.Split('=')[1];
                                int.TryParse(lineStr, out Ram_Enabl[i]);

                                lineStr = Reader.ReadLine();
                                strValue = lineStr.Split('=')[1];
                                int.TryParse(lineStr, out Ram_Response_Times[i]);

                                lineStr = Reader.ReadLine();
                                strValue = lineStr.Split('=')[1];
                                int.TryParse(lineStr, out Ram_Play_Times[i]);
                            }
                        }
                        Config_Combine_Scene nullData = new Config_Combine_Scene
                        {
                            Combine_Scene_Enable = 0,
                            Play_Count = 0,
                            Scene_Main_Number = 0,
                            Play_Mode_Main = 0,
                            Play_Time_Main_Scene = 0,
                            Scene_One_Number = 0,
                            Play_Mode_One = 0,
                            Play_Time_Scene_One = 0,
                            Scene_Two_Number = 0,
                            Play_Mode_Two = 0,
                            Play_Time_Scene_Two = 0,
                            Scene_Three_Number = 0,
                            Play_Mode_Three = 0,
                            Play_Time_Scene_Three = 0,
                            Scene_Four_Number = 0,
                            Play_Mode_Four = 0,
                            Play_Time_Scene_Four = 0
                        };
                        for (int i = 0; i < 9; i++)
                        {
                            if (Combine_Scenes[i] == null)
                            {
                                Combine_Scenes[i] = nullData;
                                nullData.Scene_Main_Number = i + 1;
                            }
                        }
                        //读取灯具数据
                        foreach (DB_Light value in DB_Lights)
                        {
                            Config_Light config_Light = new Config_Light
                            {
                                Light_Number = value.LightNo,
                                Start_Address = value.StartID,
                                Chanel_Count = value.Count
                            };
                            Lights.Add(config_Light);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("全局配置文件读取失败:" + ex.Message);
            }
        }
    }
    public class Config_Combine_Scene
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
        public int Play_Mode_Main { get; set; }
        public int Play_Mode_One { get; set; }
        public int Play_Mode_Two { get; set; }
        public int Play_Mode_Three { get; set; }
        public int Play_Mode_Four { get; set; }

    }
    public class Config_Light
    {
        public int Light_Number { get; set; }
        public int Start_Address { get; set; }
        public int Chanel_Count { get; set; }
    }

}
