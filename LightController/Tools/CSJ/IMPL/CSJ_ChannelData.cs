using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools.CSJ.IMPL
{
    public class CSJ_ChannelData
    {
        public int ChannelNo { get; set; }
        public int StepCount { get; set; }
        public IList<int> IsGradualChange { get; set; }
        public IList<int> StepTimes { get; set; }
        public IList<int> StepValues { get; set; }
    }
}
