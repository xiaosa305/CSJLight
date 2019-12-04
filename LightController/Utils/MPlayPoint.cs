using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LightController.Utils
{
    public class MPlayPoint
    {
        private static string M_DIRPATH = Application.StartupPath + @"\DataCache\Preview\CSJ\M1.bin";
        private static int BUFFSIZE = 512;
        public int ChannelNo { get; set; }
        private long DefaultSeek { get; set; }
        private long CurrentSeek { get; set; }
        private int DataLength { get; set; }
        private int ReadSize { get; set; }
        private int BuffPoint { get; set; }
        private byte[] ChannelDataBuff1 { get; set; }
        private byte[] ChannelDataBuff2 { get; set; }

        public MPlayPoint(int channelNo, long seek, int datalength)
        {
            this.ChannelNo = channelNo;
            this.DefaultSeek = seek;
            this.CurrentSeek = seek;
            this.DataLength = datalength;
            this.ChannelDataBuff1 = Enumerable.Repeat(Convert.ToByte(0x00), BUFFSIZE).ToArray();
            this.ChannelDataBuff2 = Enumerable.Repeat(Convert.ToByte(0x00), BUFFSIZE).ToArray();
            this.BuffPoint = 0;
            this.ReadSize = 0;
        }

        public void Init()
        {
            this.ReadCBytesFromPreviewFile(null);
            ChannelDataBuff2.CopyTo(ChannelDataBuff1, 0);
            this.ReadCBytesFromPreviewFile(null);
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
            using (readStream = new FileStream(M_DIRPATH, FileMode.Open))
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