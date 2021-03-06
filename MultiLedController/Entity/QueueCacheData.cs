using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.entity
{
    public class DebugQueueCacheData
    {
        public Dictionary<int, List<byte>> FieldDatas { get; set; }
        public int FramTime { get; set; }
        
        public DebugQueueCacheData(Dictionary<int,List<byte>> data,int framTime)
        {
            
            this.FieldDatas = new Dictionary<int, List<byte>>();
            foreach (int key in data.Keys)
            {
                this.FieldDatas.Add(key, data[key]);
            }
            this.FramTime = framTime;
        }
    }
}
