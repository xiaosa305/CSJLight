using MultiLedController.entity;
using MultiLedController.utils;
using MultiLedController.utils.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.multidevice.impl
{
    public class RecodeDmxDataQueue
    {
        private Queue<RecodeDmxData> RecodeDmxDatas { get; set; }

        public RecodeDmxDataQueue()
        {
            this.InitParameter();
        }
        public void InitParameter()
        {
            this.RecodeDmxDatas = new Queue<RecodeDmxData>();
        }
        public void Reset()
        {
            this.RecodeDmxDatas.Clear();
        }
        public void Enqueue(Dictionary<int,List<byte>> dmxData, int frameIntervalTime, ControlDevice controlDevice)
        {
            lock (this.RecodeDmxDatas)
            {
                this.RecodeDmxDatas.Enqueue(new RecodeDmxData(dmxData, frameIntervalTime, controlDevice));
            }
        }
        public RecodeDmxData Dequeue()
        {
            try
            {
                lock (this.RecodeDmxDatas)
                {
                    if (this.RecodeDmxDatas.Count != 0)
                    {
                        return this.RecodeDmxDatas.Dequeue();
                    }
                }
            }
            catch (Exception ex)
            {
                LogTools.Error(Constant.TAG_XIAOSA, "提取录制数据失败", ex);
            }
            return null;
        }
    }
}
