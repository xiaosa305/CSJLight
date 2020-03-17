using MultiLedController.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.Utils
{
    public class DataQueue
    {
        private static DataQueue Instance { get; set; }
        private Queue<DebugQueueCacheData> DebugQueue { get; set; }
        private Queue<SaveQueueCacheData> SaveQueue { get; set; }
        private DataQueue()
        {
            this.Reset();
        }
        public static DataQueue GetInstance()
        {
            if (Instance == null)
            {
                Instance = new DataQueue();
            }
            return Instance;
        }

        public void Reset()
        {
            this.DebugQueue = new Queue<DebugQueueCacheData>();
            this.SaveQueue = new Queue<SaveQueueCacheData>();
        }

        public void DebugEnqueue(Dictionary<int,List<byte>> data,int framTime)
        {
            Console.WriteLine("队列消息数量" + this.DebugQueue.Count);
            this.DebugQueue.Enqueue(new DebugQueueCacheData(data, framTime));
        }

        public void SaveEnqueue(Dictionary<int, List<byte>> data, int framTime,int led_Interface_num,int led_space)
        {
            this.SaveQueue.Enqueue(new SaveQueueCacheData(data, framTime,led_Interface_num,led_space));
        }

        public DebugQueueCacheData DebugDequeue()
        {
            if (this.DebugQueue.Count == 0)
            {
                return null;
            }
            else
            {
                //Console.WriteLine("队列大小" + this.DebugQueue.Count);
                return this.DebugQueue.Dequeue();
            }
        }

        public SaveQueueCacheData SaveDequeue()
        {
            if (this.SaveQueue.Count == 0)
            {
                return null;
            }
            else
            {
                return this.SaveQueue.Dequeue();
            }
        }
    }
}
