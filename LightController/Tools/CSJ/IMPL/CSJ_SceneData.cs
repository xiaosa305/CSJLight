using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools.CSJ.IMPL
{
    class CSJ_SceneData
    {
        public int SceneNo { get; set; }
        public int ChannelCount { get; set; }
        public IList<CSJ_ChannelData> ChannelDatas { get; set; }
    }
}
