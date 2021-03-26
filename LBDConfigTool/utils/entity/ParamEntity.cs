using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBDConfigTool.utils.entity
{
    public class ParamEntity
    {
        public int PacketSize { get; set; }
        public int PacketIntervalTime { get; set; }
        public int PacketIntervalTimeByPartitionIndex { get; set; }
        public int PartitionIndex { get; set; }
        public int FirstPacketIntervalTime { get; set; }
        public int FPGAUpdateCompletedIntervalTime { get; set; }
    }
}
