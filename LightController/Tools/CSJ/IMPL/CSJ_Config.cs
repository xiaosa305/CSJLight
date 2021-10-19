using LightController.Ast;
using LightController.Entity;
using LightController.MyForm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Tools.CSJ.IMPL
{
    public class CSJ_Config : ICSJFile
    {
        private const string CONFIGNAME = "Config.bin";
        public int FileSize { get; set; }//文件大小
        public int Light_Total_Count { get; set; }//灯具总数
        public int DMX512_Chanel_Count { get; set; } //DMX512通道总数
        public int Default_Scene_Number { get; set; }//开机自动播放场景
        public List<int> Music_Control_Enable { get; set; }//音频功能开启
        public int Scene_Change_Mode { get; set; }//场景切换模式
        public int TimeFactory { get; set; }//时间因子
        public List<CombineScene> CombineScenes { get; set; }//场景组合播放数据
        public List<LightInfo> LightInfos { get; set; } //灯具数据
        private string FilePath { get; set; }//全局配置文件路径
        private IList<DB_Light> DB_Lights { get; set; }//数据库灯具信息
        

        public CSJ_Config(IList<DB_Light> lights, string filePath)
        {
            FilePath = filePath;
            Music_Control_Enable = new List<int>();
            CombineScenes = new List<CombineScene>();
            LightInfos = new List<LightInfo>();
            DB_Lights = lights;
            ReadData();
        }

        public byte[] GetData()
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
            int Chanel_Count = 0;
            switch (DMX512_Chanel_Count)
            {
                case 0:
                    Chanel_Count = 512;
                    break;
                case 1:
                    Chanel_Count = 384;
                    break;
                case 2:
                    Chanel_Count = 256;
                    break;
                case 3:
                default:
                    Chanel_Count = 128;
                    break;
            }
            data.Add(Convert.ToByte(Chanel_Count & 0xFF));
            data.Add(Convert.ToByte((Chanel_Count >> 8) & 0xFF));
            //添加开机自动播放场景
            data.Add(Convert.ToByte(Default_Scene_Number));
            //添加音频功能开启状态
            //每增加8个场景，协议添加一个字节描述音频状态
            string music_control_enable = "";
            for (int i = 0; i < Music_Control_Enable.Count(); i++)
            {
                int value = (Music_Control_Enable[i] == 0) ? 0 : 1;
                music_control_enable = value + music_control_enable;
                if ((i + 1) % 8 == 0)
                {
                    data.Add(Convert.ToByte(music_control_enable, 2));
                    music_control_enable = "";
                }
            }
            //添加场景切换模式
            switch (Scene_Change_Mode)
            {
                case 1:
                    data.Add(Convert.ToByte(5));
                    break;
                case 2:
                    data.Add(Convert.ToByte(10));
                    break;
                case 3:
                    data.Add(Convert.ToByte(15));
                    break;
                case 0:
                default:
                    data.Add(Convert.ToByte(0));
                    break;
            }
            //添加时间因子
            data.Add(Convert.ToByte(TimeFactory));
            //添加场景组合播放数据
            foreach (CombineScene value in CombineScenes)
            {
                //添加组合场景播放功能开启状态
                data.Add(Convert.ToByte(value.Combine_Scene_Enable));
                //添加连播次数
                data.Add(Convert.ToByte(value.Play_Count & 0xFF));
                data.Add(Convert.ToByte((value.Play_Count >> 8) & 0xFF));
                //添加主场景编号
                data.Add(Convert.ToByte(value.Scene_Main_Number + 1));
                //添加主场景播放时间
                data.Add(Convert.ToByte(value.Play_Time_Main_Scene & 0xFF));
                data.Add(Convert.ToByte((value.Play_Time_Main_Scene >> 8) & 0xFF));
                //添加副场景1编号
                data.Add(Convert.ToByte(value.Scene_One_Number + 1));
                //添加副场景1播放时间
                data.Add(Convert.ToByte(value.Play_Time_Scene_One & 0xFF));
                data.Add(Convert.ToByte((value.Play_Time_Scene_One >> 8) & 0xFF));
                //添加副场景2编号
                data.Add(Convert.ToByte(value.Scene_Two_Number + 1));
                //添加副场景2播放时间
                data.Add(Convert.ToByte(value.Play_Time_Scene_Two & 0xFF));
                data.Add(Convert.ToByte((value.Play_Time_Scene_Two >> 8) & 0xFF));
                //添加副场景3编号
                data.Add(Convert.ToByte(value.Scene_Three_Number + 1));
                //添加副场景3播放时间
                data.Add(Convert.ToByte(value.Play_Time_Scene_Three & 0xFF));
                data.Add(Convert.ToByte((value.Play_Time_Scene_Three >> 8) & 0xFF));
                //添加副场景4编号
                data.Add(Convert.ToByte(value.Scene_Four_Number + 1));
                //添加副场景4播放时间
                data.Add(Convert.ToByte(value.Play_Time_Scene_Four & 0xFF));
                data.Add(Convert.ToByte((value.Play_Time_Scene_Four >> 8) & 0xFF));
            }
            //添加灯具信息
            foreach (LightInfo value in LightInfos)
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

        public void WriteToFile(string filepath)
        {
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            string path = filepath + @"\" + CONFIGNAME;
            byte[] Data = GetData();
            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
            fileStream.Write(Data, 0, Data.Length);
            fileStream.Close();
        }

        private void ReadData()
        {
            string lineStr = "";
            string strValue;
            int intValue;
            for (int i = 0; i < GlobalSetForm.MULTI_SCENE_COUNT; i++)
            {
                CombineScene Data = new CombineScene
                {
                    Combine_Scene_Enable = 0,
                    Play_Count = 0,
                    Scene_Main_Number = i,
                    Play_Time_Main_Scene = 0,
                    Scene_One_Number = i,
                    Play_Time_Scene_One = 0,
                    Scene_Two_Number = i,
                    Play_Time_Scene_Two = 0,
                    Scene_Three_Number = i,
                    Play_Time_Scene_Three = 0,
                    Scene_Four_Number = i,
                    Play_Time_Scene_Four = 0
                };
                CombineScenes.Add(Data);
            }
            StreamReader Reader = StreamReader.Null;
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
                        do
                        {
                            CombineScene combine_Scene = new CombineScene();
                            //获取主场景编号
                            if (lineStr[1] >= '0' && lineStr[1] <= '9')
                            {
                                strValue = lineStr[0].ToString() + lineStr[1].ToString() + "";
                            }
                            else
                            {
                                strValue = lineStr[0] + "";
                            }
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
                            //获取主场景播放时间
                            strValue = (Reader.ReadLine().Split('='))[1];
                            int.TryParse(strValue, out intValue);
                            combine_Scene.Play_Time_Main_Scene = intValue;
                            //获取副场景1编号
                            strValue = (Reader.ReadLine().Split('='))[1];
                            int.TryParse(strValue, out intValue);
                            combine_Scene.Scene_One_Number = intValue;
                            //获取副场景1播放时间
                            strValue = (Reader.ReadLine().Split('='))[1];
                            int.TryParse(strValue, out intValue);
                            combine_Scene.Play_Time_Scene_One = intValue;
                            //获取副场景2编号
                            strValue = (Reader.ReadLine().Split('='))[1];
                            int.TryParse(strValue, out intValue);
                            combine_Scene.Scene_Two_Number = intValue;
                            //获取副场景2播放时间
                            strValue = (Reader.ReadLine().Split('='))[1];
                            int.TryParse(strValue, out intValue);
                            combine_Scene.Play_Time_Scene_Two = intValue;
                            //获取副场景3编号
                            strValue = (Reader.ReadLine().Split('='))[1];
                            int.TryParse(strValue, out intValue);
                            combine_Scene.Scene_Three_Number = intValue;
                            //获取副场景3播放时间
                            strValue = (Reader.ReadLine().Split('='))[1];
                            int.TryParse(strValue, out intValue);
                            combine_Scene.Play_Time_Scene_Three = intValue;
                            //获取副场景4编号
                            strValue = (Reader.ReadLine().Split('='))[1];
                            int.TryParse(strValue, out intValue);
                            combine_Scene.Scene_Four_Number = intValue;
                            //获取副场景4播放时间
                            strValue = (Reader.ReadLine().Split('='))[1];
                            int.TryParse(strValue, out intValue);
                            combine_Scene.Play_Time_Scene_Four = intValue;
                            //将场景组合播放数据进行存放
                            if (combine_Scene.Scene_Main_Number < GlobalSetForm.MULTI_SCENE_COUNT && combine_Scene.Scene_Main_Number >= 0)
                            {
                                CombineScenes[combine_Scene.Scene_Main_Number] = combine_Scene;
                            }
                            //读取下一条是否为下一个场景组合数据，不是则结束循环
                            lineStr = Reader.ReadLine();
                        } while (!lineStr.Equals("[SK]"));
                        //添加音频功能开启状态
                        for (int i = 0; i < Constant.SCENECOUNTMAX; i++)
                        {
                            lineStr = Reader.ReadLine();
                            strValue = lineStr.Split('=')[1];
                            if (strValue.Length > 0)
                            {
                                Music_Control_Enable.Add(1);
                            }
                            else
                            {
                                Music_Control_Enable.Add(0);
                            }
                            lineStr = Reader.ReadLine();
                            lineStr = Reader.ReadLine();
                        }
                        //读取灯具数据
                        foreach (DB_Light value in DB_Lights)
                        {
                            LightInfo config_Light = new LightInfo
                            {
                                Light_Number = value.LightID,
                                Start_Address = value.LightID,
                                Chanel_Count = value.Count
                            };
                            LightInfos.Add(config_Light);
                        }
                        Light_Total_Count = LightInfos.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex, "全局配置文件读取失败");
            }
        }
    }
}
