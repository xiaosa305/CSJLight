using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBDConfigTool.utils.conf
{
    public class HCXConf : IConf
    {
        public string MAC { get; set; }//MAC地址              u8[6]
        public int IP_4 { get; set; }//IP最后一个字节         u8
        public int DevNum { get; set; }//级联分控数           u16
        public int ChipType { get; set; }//芯片类型           u16
        public int ChLength { get; set; }//通道长度           u8
        public int DevNo { get; set; }//分控号                u8
        public string DevName { get; set; }//分控名称         u8[6]
        public int ArtNetSpace { get; set; }//ArtNet起始空间  u16
        public string VerNo { get; set; }//版本号             u8[4]
        private string Other { get; set; }//预留               u8[2]

        public static HCXConf Build(byte[] data)
        {
            HCXConf conf = new HCXConf();
            try
            {
                conf.MAC = data[7].ToString("X2") + "-" + data[8].ToString("X2") + "-" + data[9].ToString("X2") + "-" + data[10].ToString("X2") + "-" + data[11].ToString("X2") + "-" + data[12].ToString("X2");
                conf.DevNum = (data[13] & 0xFF) | ((data[14] & 0xFF) << 8);
                conf.IP_4 = data[15] & 0xFF;
                conf.ChipType = (data[16] & 0xFF) | ((data[17] & 0xFF) << 8);
                conf.ArtNetSpace = (data[18] & 0xFF) | ((data[21] & 0xFF) << 8);
                conf.ChLength = data[19] & 0xFF;
                conf.DevNo = data[20] & 0xFF;
                List<byte> DevNameBuff = new List<byte>();
                for (int i =22; i < 28; i++)
                {
                    DevNameBuff.Add(data[i]);
                }
                conf.DevName = Encoding.Default.GetString(DevNameBuff.ToArray());
                return conf;
            }
            catch (Exception ex)
            {
                Console.WriteLine("读取HCX参数失败" + ex.Message);
            }
            return null;
        }

        public byte[] GetData()
        {
            try
            {
                List<byte> buff = new List<byte>();
                buff.Add(0x57);
                buff.Add(0x52);
                buff.Add(0x49);
                buff.Add(0x46);
                string[] macstrs = this.MAC.Split('-');
                for (int i = 0; i < macstrs.Length; i++)
                {
                    int value = int.Parse(macstrs[i], System.Globalization.NumberStyles.HexNumber);
                    buff.Add(Convert.ToByte(value));
                }
                buff.Add(buff[9]);
                buff.Add(Convert.ToByte(this.IP_4));
                buff.Add(Convert.ToByte(this.DevNum & 0xFF));
                buff.Add(Convert.ToByte((this.DevNum >> 8) & 0xFF));
                buff.Add(Convert.ToByte(this.ChipType & 0xFF));
                buff.Add(Convert.ToByte((this.ChipType >> 8) & 0xFF));
                buff.Add(Convert.ToByte(this.ChLength & 0xFF));
                buff.Add(Convert.ToByte(this.DevNo & 0xFF));
                buff.Add(Convert.ToByte(this.ArtNetSpace & 0xFF));
                buff.Add(Convert.ToByte((this.ArtNetSpace >> 8) & 0xFF));
                byte[] devNameBuff = Encoding.Default.GetBytes(this.DevName);
                for (int i = 0; i < 6; i++)
                {
                    if (i < devNameBuff.Length)
                    {
                        buff.Add(devNameBuff[i]);
                    }
                    else
                    {
                        buff.Add(0x00);
                    }
                }
                buff.Add(0x00);
                buff.Add(0x00);
                return buff.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine("生成HCX参数失败" + ex.Message);
            }
            return null;
        }
    }
}
