using MultiLedController.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.entity
{
    public class ControlDevice
    {
        private const byte Flag = 0xEB;// 参数标志位固定0xEB
        private const byte Head = 0x55;// 参数头标识   0x55
        public string LedName { get; set; }//控制器标识byte[8]
        public int Addr { get; set; }// 硬件地址 byte[2]
        public int LinkMode { get; set; } // 网络连接类型 0-TCP 1-UDP
        public string IP { get; set; } // IP地址byte[4]
        public int LinkPort { get; set; } // 连接端口byte[2]
        public string Mac { get; set; }//MAC地址 byte[6]
        public int Baud { get; set; }//波特率
        public int Led_space { get; set; }//每路空间数
        public int Frame_time { get; set; }//设置播放速度MS
        public int Led_interface_num { get; set; }//接口数
        public int Led_max_len { get; set; }//灯带最大长度byte[2]

        public ControlDevice(byte[] data)
        {
            byte[] LedNameBuff = new byte[16];
            Array.Copy(data, 2, LedNameBuff, 0, 16);
            this.LedName = Encoding.Default.GetString(LedNameBuff);
            this.Addr = (int)((data[18] & 0xFF) | ((data[19] & 0xFF) << 8));
            this.LinkMode = Convert.ToInt16(data[20]);
            this.IP = Convert.ToInt16(data[24]).ToString()
                     + "."
                     + Convert.ToInt16(data[23]).ToString()
                     + "."
                     + Convert.ToInt16(data[22]).ToString()
                     + "."
                     + Convert.ToInt16(data[21]).ToString();
            this.LinkPort = (int)((data[25] & 0xFF) | ((data[26] & 0xFF) << 8));
            this.Mac = StringHelper.DecimalStringToBitHex(Convert.ToInt16(data[27]).ToString(),2)   
                     + "-"
                     + StringHelper.DecimalStringToBitHex(Convert.ToInt16(data[28]).ToString(), 2)
                     + "-"
                     + StringHelper.DecimalStringToBitHex(Convert.ToInt16(data[29]).ToString(), 2)
                     + "-"
                     + StringHelper.DecimalStringToBitHex(Convert.ToInt16(data[30]).ToString(), 2)
                     + "-"
                     + StringHelper.DecimalStringToBitHex(Convert.ToInt16(data[31]).ToString(), 2)
                     + "-"
                     + StringHelper.DecimalStringToBitHex(Convert.ToInt16(data[32]).ToString(), 2);
            this.Baud = Convert.ToInt16(data[33]);
            this.Led_space = Convert.ToInt16(data[34]);
            this.Frame_time = Convert.ToInt16(data[35]);
            this.Led_interface_num = Convert.ToInt16(data[36]) + 1;
            this.Led_max_len = (int)((data[37] & 0xFF) | ((data[38] & 0xFF) << 8));
        }
    }
}
