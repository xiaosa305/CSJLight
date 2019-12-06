using LightController.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LightController.Utils
{
    public class PlayPoint
    {
        private static string C_DIRPATH = Application.StartupPath + @"\DataCache\Preview\CSJ\C1.bin";
        private static string M_DIRPATH = Application.StartupPath + @"\DataCache\Preview\CSJ\M1.bin";
        private static int BUFFSIZE = 512;
        public int ChannelNo { get; set; }
        private long DefaultSeek { get; set; }
        private long CurrentSeek { get; set; }
        private int DataLength { get; set; } 
        private int ReadSize { get; set; }
        private int BuffPoint { get; set; }
        private string FilePath { get; set; }
        private byte[] ChannelDataBuff1 { get; set; }
        private byte[] ChannelDataBuff2 { get; set; }

        

        public PlayPoint(int channelNo,long seek,int datalength,int mode)
        {
            this.ChannelNo = channelNo;
            this.DefaultSeek = seek;
            this.CurrentSeek = seek;
            this.DataLength = datalength;
            this.ChannelDataBuff1 = Enumerable.Repeat(Convert.ToByte(0x00), BUFFSIZE).ToArray();
            this.ChannelDataBuff2 = Enumerable.Repeat(Convert.ToByte(0x00), BUFFSIZE).ToArray();
            this.BuffPoint = 0;
            this.ReadSize = 0;
            switch (mode)
            {
                case Constant.MODE_M:
                    FilePath = M_DIRPATH;
                    break;
                case Constant.MODE_C:
                default:
                    FilePath = C_DIRPATH;
                    break;
            }
        }
        public void Init()
        {
            this.ReadCBytesFromPreviewFile(null);
            ChannelDataBuff2.CopyTo(ChannelDataBuff1, 0);
            this.ReadCBytesFromPreviewFile(null);
            Console.WriteLine("通道" + ChannelNo + "预览数据预加载完成");
        }

        public byte Read()
        {
            if (this.BuffPoint == BUFFSIZE)
            {
                this.BuffPoint = 0;
                ChannelDataBuff2.CopyTo(ChannelDataBuff1, 0);
                this.ReadCBytesFromPreviewFile(null);
                Console.WriteLine("");
            }
            return ChannelDataBuff1[BuffPoint++];
        }

        private void ReadCBytesFromPreviewFile(Object obj)
        {
            FileStream readStream = null;
            int loadedSize = 0;
            long unLoadDataSize;
            int readSize;
            using (readStream = new FileStream(FilePath, FileMode.Open))
            {
                while (loadedSize < BUFFSIZE)
                {
                    readStream.Seek(this.CurrentSeek, SeekOrigin.Begin);
                    unLoadDataSize = Convert.ToInt64(DataLength) + DefaultSeek - CurrentSeek;
                    if (unLoadDataSize < (BUFFSIZE - loadedSize))
                    {
                        readSize = readStream.Read(ChannelDataBuff2, loadedSize, Convert.ToInt32(unLoadDataSize));
                        loadedSize += readSize;
                        CurrentSeek = DefaultSeek;
                    }
                    else
                    {
                        readSize = readStream.Read(ChannelDataBuff2, loadedSize, BUFFSIZE - loadedSize);
                        loadedSize += readSize;
                        CurrentSeek = (CurrentSeek + loadedSize) == (DefaultSeek + DataLength) ? DefaultSeek : (CurrentSeek + readSize);
                        if (readSize == 0 || loadedSize == BUFFSIZE)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
