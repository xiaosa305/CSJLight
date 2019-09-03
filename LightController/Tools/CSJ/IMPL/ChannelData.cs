using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools.CSJ.IMPL
{
    public class ChannelData
    {
        public int ChannelNo { get; set; }//通道编号
        public int DataSize { get; set; }//数据大小
        public List<int> Datas { get; set; }//采样数据
    }
}
