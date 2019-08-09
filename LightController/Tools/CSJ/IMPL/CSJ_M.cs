using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools.CSJ.IMPL
{
    public class CSJ_M:ICSJFile
    {
        public int SceneNo { get; set; }//场景编号
        public int ChanelCount { get; set; }//通道总数
        public int FileSize { get; set; }//文件大小
        public int StepListCount { get; set; }//音频步数链表元素个数
        public int FrameTime { get; set; }//音频步时间
        public List<int> StepList { get; set; }//音频步数链表
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
