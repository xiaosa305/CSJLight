using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools.CSJ.IMPL
{
    public class CSJ_C:ICSJFile
    {
        public int SceneNo { get; set; }//场景编号
        public int MICSensor { get; set; }//摇麦功能
        public int SenseFreq { get; set; }//摇麦再次感应间隔时间
        public int RunTime { get; set; }//摇麦场景执行时间
        public int ChanelCount { get; set; }//场景通道总数
        public int FileSize { get; set; }//场景文件大小
        public List<ChannelData> ChannelDatas { get; set; }//数据

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
