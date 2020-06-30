using MultiLedController.entity;
using MultiLedController.utils;
using MultiLedController.utils.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.multidevice.impl
{
    public class DebugDmxDataQueue
    {
        private Queue<DebugDmxData> DebugDmxDatas { get; set; }
        public DebugDmxDataQueue()
        {
            this.InitParameter();
        }
        private void InitParameter()
        {
            this.DebugDmxDatas = new Queue<DebugDmxData>();
        }
        public void Reset()
        {
            this.DebugDmxDatas.Clear();
        }
        public void Enqueue(Dictionary<int,List<byte>> dmxData,int frameIntervalTime,ControlDevice controlDevice)
        {
            lock (this.DebugDmxDatas)
            {
                this.DebugDmxDatas.Enqueue(new DebugDmxData(dmxData, frameIntervalTime, controlDevice));
            }
        }
        public DebugDmxData Dequeue()
        {
            lock (this.DebugDmxDatas)
            {
                try
                {
                    //Console.WriteLine("queue count" + this.DebugDmxDatas.Count);
                    if (this.DebugDmxDatas.Count != 0)
                    {
                        return this.DebugDmxDatas.Dequeue();
                    }
                }
                catch (Exception ex)
                {
                    LogTools.Error(Constant.TAG_XIAOSA, "提取实时调试数据失败", ex);
                }
                return null;
            }
        }
    }
}
