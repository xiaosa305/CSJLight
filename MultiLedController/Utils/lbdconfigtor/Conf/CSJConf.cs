using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.utils.lbdconfigtor.Conf
{
    class CSJConf:IConf
    {
        private const byte Flag = 0xFF;//标记位
        private string MIA_HAO { get; set; }//密码
        private int Baud { get; set; }//(0-4)9600,19200,38400,57600,115200
        private bool IsSetBad { get; set; }//是否置设坏屏
        private int DiskFlag { get; set; }//磁盘标志
        private int Play_Mod { get; set; }//播放方式，（0-2）单播，循环播放，定时播放
    }
}
