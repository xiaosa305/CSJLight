using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    public class SenceData
    {
        public int SenceNo { get; set; }
        public int ChanelCount { get; set; }
        public IList<ChanelData> ChanelDatas { get; set; }
    }
}
