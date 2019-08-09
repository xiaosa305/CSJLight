using LightController.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools.CSJ.IMPL
{
    public class CSJ_Config:ICSJFile
    {
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

        public CSJ_Config(DBWrapper dBWrapper, string filePath)
        {

        }


        public byte[] GetData()
        {
            throw new NotImplementedException();
        }

        public void WriteToFile(string filepath)
        {
            throw new NotImplementedException();
        }
    }
}
