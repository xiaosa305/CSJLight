using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    public class DMX_M_Data
    {
        public M_Head HeadData { get; set; }//数据头
        public IList<M_Data> Datas { get; set; }//数据块
    }
    public class M_Head
    {
        public int ChanelCount { get; set; }//通道总数
        public int FileSize { get; set; }//文件大小
        public int StepListCount { get; set; }//音频步数链表元素个数
        public int FrameTime { get; set; }//音频步时间
        public IList<int> StepList { get; set; }//音频步数链表
    }
    public class M_Data
    {
        public int ChanelNo { get; set; }//通道编号
        public int DataSize { get; set; }//数据大小
        public IList<int> Datas { get; set; }//通道所有采样数据
    }
}
