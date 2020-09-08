using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Utils.Ver2
{
    class ChannelDataBean
    {
        public const int MODE_C = 0;
        public const int MODE_M = 1;
        public const int MODE_C_GRADUAL = 1;
        public const int MODE_C_JUMP = 0;
        public const int MODE_C_HIDDEN = 2;
        public const int MODE_M_GRADUAL = 2;
        public const int MODE_M_JUMP = 1;
        public const int MODE_M_HIDDEN = 0;
        public int ChannelNo { get; set; }
        public int StepCount { get; set; }
        public int SceneNo { get; set; }
        public Mode Mode { get; set; }
        public ChannelFlag ChannelFlag { get; set; }
        public int MaxValue { get; set; }
        public List<int> StepValues { get; set; }
        public List<int> StepTime { get; set; }
        public List<int> StepMode { get; set; }
    }
}
