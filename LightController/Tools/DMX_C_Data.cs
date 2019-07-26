using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    public class DMX_C_Data
    {
        public C_Head HeadData { get; set; }//数据头
        public IList<C_Data> Datas { get; set; }//数据
    }

    public class C_Head
    {
        //public int MICSensor { get; set; }//摇麦功能
        //public int SenseFreq { get; set; }//摇麦再次感应间隔时间
        //public int RunTime { get; set; }//摇麦场景执行时间
        public int ChanelCount { get; set; }//场景通道总数
        public int FileSize { get; set; }//场景文件大小
    }
    public class C_Data
    {
        public int ChanelNo { get; set; }//通道编号
        public int DataSize { get; set; }//采样数据长度
        public IList<int> Datas { get; set; }//采样数据
    }
}
