using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crc32C;
using LBDConfigTool.utils.crc;

namespace LBDConfigTool.utils.conf
{
    [Serializable]
    public class CSJConf:IConf
    {
        private const byte Flag = 0xEA;//标记位 u8
        public string MIA_HAO { get; set; }//密码  u8[6]
        public int Addr { get; set; }//地址 u16
        public int Baud { get; set; }//(0-4)9600,19200,38400,57600,115200 u8
        public bool IsSetBad { get; set; }//是否置设坏屏 u8
        public int DiskFlag { get; set; }//磁盘标志 u8
        public int Play_Mod { get; set; }//播放方式，（0-2）单播，循环播放，定时播放  u8
        public int PlayScene { get; set; }//开机播放场景
        public string LedName { get; set; }//显示屏标识 u8[16]
        public string Ver { get; set; } //u8[16]
        public int Max_scan_dot { get; set; }//最大扫描点  u16
        public int CardType { get; set; }//卡类型，0-主卡，2-副卡 u8
        public int Led_out_type { get; set; }//245,485   u8
        public int Led_fx { get; set; }//数据取反 u8
        public int RGB_Type { get; set; }//RGB RGBW  u8
        public int IC_Type { get; set; }//芯片类型   u8
        public int Play_hz { get; set; }//播放速度10ms---200  单步时间 u8
        public int Clk_shzhong { get; set; }//时钟频率  u16
        public int Led_gam { get; set; }// Led_gam  u8
        public int Led_ld { get; set; }//全局亮度  u8
        public int R_LD { get; set; }//红亮度   u8
        public int G_LD { get; set; }//绿亮度   u8
        public int B_LD { get; set; }//蓝亮度   u8
        public int W_LD { get; set; }//白亮度   u8
        //ArtNetConf
        public string Mac { get; set; }//MAC地址   u8[6]
        public string Ip { get; set; }//IP地址   u32
        public int Fk_lushu { get; set; }//分控路数  u8
        public int Jl_fk_num { get; set; }//级联分控数   u8
        public int Art_Net_Start_Space { get; set; }//ArtNet起始空间   u16
        public int Art_Net_Pre { get; set; }//一路通道数  u8
        public int Art_Net_td_len { get; set; }//通道长度  u16
        public int Art_Net_fk_id { get; set; }//分控号   u8
        public int SumUseTimes { get; set; }//总使用次数   u32
        public int CurrUseTimes { get; set; }//当前使用次数  不参与CRC校验

        public string OLD_MIA_HAO { get; set; }// 旧密码 u8[6]
        public CSJConf()
        {
            this.Max_scan_dot = 1024;
            this.Baud = 3;
            this.Led_gam = 1;
            this.DiskFlag = 0;
            this.LedName = "";
        }

        public static CSJConf BuildParamEmtity(byte[] data)
        {
            try
            {
                int index = 6;
                CSJConf conf = new CSJConf();
                List<byte> buff = new List<byte>();
                for (int j = 0; j < 6; j++)
                {
                    buff.Add(data[index++]);
                }
                conf.MIA_HAO = Encoding.Default.GetString(buff.ToArray());
                buff.Clear();
                for (int i = 0; i < 6; i++)
                {
                    buff.Add(data[index++]);
                }
                conf.OLD_MIA_HAO = Encoding.Default.GetString(buff.ToArray());
                buff.Clear();
                conf.Addr = (int)((data[index++]) & 0xFF | ((data[index++]) << 8) & 0xFF);
                conf.Baud = (int)(data[index++] & 0xFF);
                conf.IsSetBad = ((int)(data[index++] & 0xFF)) == 1;
                conf.DiskFlag = (int)(data[index++] & 0xFF);
                conf.Play_Mod = (int)(data[index++] & 0xFF);
                conf.PlayScene = (int)(data[index++] & 0xFF);
                for (int i = 0; i < 16; i++)
                {
                    buff.Add(data[index++]);
                }
                conf.LedName = Encoding.Default.GetString(buff.ToArray());
                buff.Clear();
                for (int i = 0; i < 16; i++)
                {
                    buff.Add(data[index++]);
                }
                conf.Ver = Encoding.Default.GetString(buff.ToArray());
                buff.Clear();
                conf.Max_scan_dot = (int)((data[index++] & 0xFF) | ((data[index++] << 8) & 0xFF));
                conf.CardType = (int)(data[index++] & 0xFF);
                conf.Led_out_type = (int)(data[index++] & 0xFF);
                conf.Led_fx = (int)(data[index++] & 0xFF);
                conf.RGB_Type = (int)(data[index++] & 0xFF);
                conf.IC_Type = (int)(data[index++] & 0xFF);
                conf.Play_hz = (int)(data[index++] & 0xFF);
                conf.Clk_shzhong = (int)((data[index++] & 0xFF) | ((data[index++] << 8) & 0xFF));
                conf.Led_gam = (int)(data[index++] & 0xFF);
                conf.Led_ld = (int)(data[index++] & 0xFF);
                conf.R_LD = (int)(data[index++] & 0xFF);
                conf.G_LD = (int)(data[index++] & 0xFF);
                conf.B_LD = (int)(data[index++] & 0xFF);
                conf.W_LD = (int)(data[index++] & 0xFF);
                conf.Mac = data[index++].ToString("X2") + "-" + data[index++].ToString("X2") + "-" + data[index++].ToString("X2") + "-" + data[index++].ToString("X2") + "-" + data[index++].ToString("X2") + "-" + data[index++].ToString("X2");
                conf.Ip = data[index++].ToString("X2") + "." + data[index++].ToString("X2") + "." + data[index++].ToString("X2") + "." + data[index++].ToString("X2");
                conf.Fk_lushu = (int)(data[index++] & 0xFF);
                conf.Jl_fk_num = (int)(data[index++] & 0xFF);
                conf.Art_Net_Start_Space = (int)((data[index++] & 0xFF) | ((data[index++] << 8) & 0xFF));
                conf.Art_Net_Pre = (int)(data[index++] & 0xFF);
                conf.Art_Net_td_len = (int)((data[index++] & 0xFF) | ((data[78] << 8) & 0xFF));
                conf.Art_Net_fk_id = (int)(data[index++] & 0xFF);
                conf.SumUseTimes = (int)((data[index++] & 0xFF) | ((data[index++] << 8) & 0xFF) | ((data[index++] << 16) & 0xFF) | ((data[index++] << 24) & 0xFF));
                conf.CurrUseTimes = (int)((data[index++] & 0xFF) | ((data[index++] << 8) & 0xFF) | ((data[index++] << 16) & 0xFF) | ((data[index++] << 24) & 0xFF));
                return conf;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return null;
        }

        public static CSJConf Build(byte[] data)
        {
            CSJConf conf = null;
            try
            {
                conf = new CSJConf();
                List<byte> buff = new List<byte>();
                for (int i = 1; i < 7; i++)
                {
                    buff.Add(data[i]);
                }
                conf.MIA_HAO = Encoding.Default.GetString(buff.ToArray());
                buff.Clear();
                conf.Addr = (int)((data[7] & 0xFF) | ((data[8] << 8) & 0xFF));
                conf.Baud = (int)(data[9] & 0xFF);
                conf.IsSetBad = ((int)(data[10] & 0xFF)) == 1;
                conf.DiskFlag = (int)(data[11] & 0xFF);
                conf.Play_Mod = (int)(data[12] & 0xFF);
                conf.PlayScene = (int)(data[13] & 0xFF);
                for (int i = 0; i < 16; i++)
                {
                    buff.Add(data[14 + i]);
                }
                conf.LedName = Encoding.Default.GetString(buff.ToArray());
                buff.Clear();
                for (int i = 0; i < 16; i++)
                {
                    buff.Add(data[30 + i]);
                }
                conf.Ver = Encoding.Default.GetString(buff.ToArray());
                buff.Clear();
                conf.Max_scan_dot = (int)((data[46] & 0xFF) | ((data[47] << 8) & 0xFF));
                conf.CardType = (int)(data[48] & 0xFF);
                conf.Led_out_type = (int)(data[49] & 0xFF);
                conf.Led_fx = (int)(data[50] & 0xFF);
                conf.RGB_Type = (int)(data[51] & 0xFF);
                conf.IC_Type = (int)(data[52] & 0xFF);
                conf.Play_hz = (int)(data[53] & 0xFF);
                conf.Clk_shzhong = (int)((data[54] & 0xFF) | ((data[55] << 8) & 0xFF));
                conf.Led_gam = (int)(data[56] & 0xFF);
                conf.Led_ld = (int)(data[57] & 0xFF);
                conf.R_LD = (int)(data[58] & 0xFF);
                conf.G_LD = (int)(data[59] & 0xFF);
                conf.B_LD = (int)(data[60] & 0xFF);
                conf.W_LD = (int)(data[61] & 0xFF);
                conf.Mac = data[62].ToString("X2") + "-" + data[63].ToString("X2") + "-" + data[64].ToString("X2") + "-" + data[65].ToString("X2") + "-" + data[66].ToString("X2") + "-" + data[67].ToString("X2");
                conf.Ip = data[68].ToString("X2") + "." + data[69].ToString("X2") + "." + data[70].ToString("X2") + "." + data[71].ToString("X2");
                conf.Fk_lushu = (int)(data[72] & 0xFF);
                conf.Jl_fk_num = (int)(data[73] & 0xFF);
                conf.Art_Net_Start_Space = (int)((data[74] & 0xFF) | ((data[75] << 8) & 0xFF));
                conf.Art_Net_Pre = (int)(data[76] & 0xFF);
                conf.Art_Net_td_len = (int)((data[77] & 0xFF) | ((data[78] << 8) & 0xFF));
                conf.Art_Net_fk_id = (int)(data[79] & 0xFF);
                conf.SumUseTimes = (int)((data[80] & 0xFF) | ((data[81] << 8) & 0xFF) | ((data[82] << 16) & 0xFF) | ((data[83] << 24) & 0xFF));
                conf.CurrUseTimes = (int)((data[84] & 0xFF) | ((data[85] << 8) & 0xFF) | ((data[86] << 16) & 0xFF) | ((data[87] << 24) & 0xFF));
                return conf;
            }
            catch (Exception ex)
            {
                Console.WriteLine("读取CSJ配置参数失败");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return conf;
        }

        public byte[] GetData()
        {
            byte[] data = null;
            try
            {
                List<byte> buff = new List<byte>();
                //Flag
                buff.Add(Flag);
                //MIA_HAO
                for (int i = 0; i < (this.MIA_HAO.Length <= 6 ? this.MIA_HAO.Length : 6) ; i++)
                {
                    buff.Add(Convert.ToByte(this.MIA_HAO[i]));
                }
                if (this.MIA_HAO.Length < 6)
                {
                    for (int i = this.MIA_HAO.Length; i < 6; i++)
                    {
                        buff.Add(0x00);
                    }
                }

                //OLD_MIA_HAO
                for (int i = 0; i < (this.OLD_MIA_HAO.Length <= 6 ? this.OLD_MIA_HAO.Length : 6); i++)
                {
                    buff.Add(Convert.ToByte(this.OLD_MIA_HAO[i]));
                }
                if (this.OLD_MIA_HAO.Length < 6)
                {
                    for (int i = this.OLD_MIA_HAO.Length; i < 6; i++)
                    {
                        buff.Add(0x00);
                    }
                }

                //Addr
                buff.Add(Convert.ToByte(this.Addr & 0xFF));//LO
                buff.Add(Convert.ToByte((this.Addr >> 8) & 0xFF));//HI
                //Baud
                buff.Add(Convert.ToByte(this.Baud));
                //IsSetBad
                buff.Add(Convert.ToByte(this.IsSetBad ? 0x01 : 0x00));
                //DiskFlag
                buff.Add(Convert.ToByte(this.DiskFlag));
                //Play_Mod
                buff.Add(Convert.ToByte(this.Play_Mod));
                //Play_Scene
                buff.Add(Convert.ToByte(this.PlayScene));
                //LedName
                for (int i = 0; i < (this.LedName.Length <= 16 ? this.LedName.Length : 16); i++)
                {
                    buff.Add(Convert.ToByte(this.LedName[i]));
                }
                if (this.LedName.Length < 16)
                {
                    for (int i = this.LedName.Length; i < 16; i++)
                    {
                        buff.Add(0x00);
                    }
                }
                //Ver
                for (int i = 0; i < (this.Ver.Length <= 16 ? this.Ver.Length : 16); i++)
                {
                    buff.Add(Convert.ToByte(this.Ver[i]));
                }
                if (this.Ver.Length < 16)
                {
                    for (int i = this.Ver.Length; i < 16; i++)
                    {
                        buff.Add(0x00);
                    }
                }
                //Max_Scan_Dot
                buff.Add(Convert.ToByte(this.Max_scan_dot & 0xFF));//LO
                buff.Add(Convert.ToByte((this.Max_scan_dot >> 8) & 0xFF));//HI
                //CardType
                buff.Add(Convert.ToByte(this.CardType & 0xFF));
                //Led_Out_Type
                buff.Add(Convert.ToByte(this.Led_out_type & 0xFF));
                //Led_Fx
                buff.Add(Convert.ToByte(this.Led_fx & 0xFF));
                //RGB_Type
                buff.Add(Convert.ToByte(this.RGB_Type & 0xFF));
                //IC_Type
                buff.Add(Convert.ToByte(this.IC_Type & 0xFF));
                //Play_Hz
                buff.Add(Convert.ToByte(this.Play_hz & 0xFF));
                //Clk_shzhong
                buff.Add(Convert.ToByte(this.Clk_shzhong & 0xFF));//LO
                buff.Add(Convert.ToByte((this.Clk_shzhong >> 8) & 0xFF));//HI
                //Led_gam
                buff.Add(Convert.ToByte(this.Led_gam));
                //Led_LD
                buff.Add(Convert.ToByte(this.Led_ld));
                //R_LD;
                buff.Add(Convert.ToByte(this.R_LD));
                //G_LD;
                buff.Add(Convert.ToByte(this.G_LD));
                //B_LD;
                buff.Add(Convert.ToByte(this.B_LD));
                //W_LD;
                buff.Add(Convert.ToByte(this.W_LD));
                /*ArtNet*/
                //MAC
                string[] macbuff = this.Mac.Split('-');
                for (int i = 0; i < 6; i++)
                {
                    try
                    {
                        int value = int.Parse(macbuff[i], System.Globalization.NumberStyles.HexNumber);
                        buff.Add(Convert.ToByte(value));
                    }
                    catch (Exception)
                    {
                        buff.Add(0x00);
                    }
                }
                //IP
                string[] ipBuff = this.Ip.Split('.');
                for (int i = 0; i < 4; i++)
                {
                    try
                    {
                        int value = int.Parse(ipBuff[i]);
                        buff.Add(Convert.ToByte(value));
                    }
                    catch (Exception)
                    {
                        buff.Add(0x00);
                    }
                }
                //fk_lushu
                buff.Add(Convert.ToByte(this.Fk_lushu));
                //jl_fk_num
                buff.Add(Convert.ToByte(this.Jl_fk_num));
                //ArtNetStartSpace
                buff.Add(Convert.ToByte(this.Art_Net_Start_Space & 0xFF));
                buff.Add(Convert.ToByte((this.Art_Net_Start_Space >> 8) & 0xFF));
                //ArtNetPre
                buff.Add(Convert.ToByte(this.Art_Net_Pre));
                //ArtNetTdLen
                buff.Add(Convert.ToByte(this.Art_Net_td_len & 0xFF));
                buff.Add(Convert.ToByte((this.Art_Net_td_len >> 8 )& 0xFF));
                //ArtNetFkId
                buff.Add(Convert.ToByte(this.Art_Net_fk_id));
                //SumUseTimes
                buff.Add(Convert.ToByte(this.SumUseTimes & 0xFF));
                buff.Add(Convert.ToByte((this.SumUseTimes >> 8) & 0xFF));
                buff.Add(Convert.ToByte((this.SumUseTimes >> 16) & 0xFF));
                buff.Add(Convert.ToByte((this.SumUseTimes >> 24) & 0xFF));
                //CurrUseTimes
                buff.Add(Convert.ToByte(this.CurrUseTimes & 0xFF));
                buff.Add(Convert.ToByte((this.CurrUseTimes >> 8) & 0xFF));
                buff.Add(Convert.ToByte((this.CurrUseTimes >> 16) & 0xFF));
                buff.Add(Convert.ToByte((this.CurrUseTimes >> 24) & 0xFF));
                //crc
                //uint crcValue = Crc32CAlgorithm.Compute(buff.ToArray());
                uint crcValue = Crc32SUM.GetSumCRC(buff.ToArray());
                buff.Add(Convert.ToByte(crcValue & 0xFF));
                buff.Add(Convert.ToByte((crcValue >> 8) & 0xFF));
                buff.Add(Convert.ToByte((crcValue >> 16) & 0xFF));
                buff.Add(Convert.ToByte((crcValue >> 24) & 0xFF));
                data = buff.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine("生成配置参数数据");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return data;
        }
    }
}
