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
        private Queue<QueueCacheData> DebugQueue { get; set; }
        private Queue<QueueCacheData> SaveQueue { get; set; }
        private DataQueue()
        {
            this.DebugQueue = new Queue<QueueCacheData>();
            this.SaveQueue = new Queue<QueueCacheData>();
        }
        public static DataQueue GetInstance()
        {
            if (Instance == null)
            {
                Instance = new DataQueue();
            }
            return Instance;
        }

        public void DebugEnqueue(Dictionary<int,List<byte>> data,int framTime)
        {
            this.DebugQueue.Enqueue(new QueueCacheData(data, framTime));
        }

        public void SaveEnqueue(Dictionary<int, List<byte>> data, int framTime)
        {
            this.SaveQueue.Enqueue(new QueueCacheData(data, framTime));
        }

        public QueueCacheData DebugDequeue()
        {
            if (this.DebugQueue.Count == 0)
            {
                return null;
            }
            else
            {
                return this.DebugQueue.Dequeue();
            }
        }

        public QueueCacheData SaveDequeue()
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
