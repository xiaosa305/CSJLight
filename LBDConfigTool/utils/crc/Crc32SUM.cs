using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBDConfigTool.utils.crc
{
    public class Crc32SUM
    {
        public static uint GetSumCRC(byte[] data)
        {
            uint crc = 0;
            for (int i = 0; i < data.Length; i++)
            {
                crc += data[i];
            }
            return crc;
        }
    }
}
