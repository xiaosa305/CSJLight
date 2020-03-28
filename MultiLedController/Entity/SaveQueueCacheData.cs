using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.entity
{
    public class SaveQueueCacheData
    {
        public Dictionary<int, List<byte>> FieldDatas { get; set; }
        public int FramTime { get; set; }
        public int Led_Interface_num { get; set; }
        public int Led_space { get; set; }

        public SaveQueueCacheData(Dictionary<int, List<byte>> data, int framTime,int led_Interface_num,int led_space)
        {
            this.FieldDatas = new Dictionary<int, List<byte>>();
            this.Led_Interface_num = led_Interface_num;
            this.Led_space = led_space;
            foreach (int key in data.Keys)
            {
                this.FieldDatas.Add(key, data[key]);
            }
            this.FramTime = framTime;
        }
    }
}
