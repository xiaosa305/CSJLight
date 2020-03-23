using MultiLedController.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultiLedController.Utils.IMPL
{
    public class DataQueue : IDataQueue
    {
        private static DataQueue Instance { get; set; }
        private Queue<DebugQueueCacheData> DebugQueue { get; set; }
        private Queue<SaveQueueCacheData> SaveQueue { get; set; }
        private long TestTime { get; set; }
        private long TestTime2 { get; set; }
        private DataQueue()
        {
            this.Reset();
            TestTime = -1;
            TestTime2 = -1;
        }
        /// <summary>
        /// 功能：获取队列实例
        /// </summary>
        /// <returns></returns>
        public static IDataQueue GetInstance()
        {
            if (Instance == null)
            {
                Instance = new DataQueue();
            }
            return Instance;
        }
        /// <summary>
        /// 功能：复位队列缓存区
        /// </summary>
        public void Reset()
        {
            this.DebugQueue = new Queue<DebugQueueCacheData>();
            this.SaveQueue = new Queue<SaveQueueCacheData>();
        }
        /// <summary>
        /// 功能：推送实时调试数据到队列
        /// </summary>
        /// <param name="data">实时调试数据</param>
        /// <param name="framTime">帧间隔时间</param>
        public void DebugEnqueue(Dictionary<int,List<byte>> data,int framTime)
        {
            Console.WriteLine("队列大小 :" + this.DebugQueue.Count);
            this.DebugQueue.Enqueue(new DebugQueueCacheData(data, framTime));
        }
        /// <summary>
        /// 功能：推送文件存储数据到队列
        /// </summary>
        /// <param name="data">存储数据</param>
        /// <param name="framTime">帧间隔时间</param>
        /// <param name="led_Interface_num">控制卡线路数量</param>
        /// <param name="led_space">控制卡空间数量</param>
        public void SaveEnqueue(Dictionary<int, List<byte>> data, int framTime,int led_Interface_num,int led_space)
        {
            this.SaveQueue.Enqueue(new SaveQueueCacheData(data, framTime,led_Interface_num,led_space));
        }
        /// <summary>
        /// 功能：提取队列实时调试数据
        /// </summary>
        /// <returns>实时调试数据</returns>
        public DebugQueueCacheData DebugDequeue()
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
        /// <summary>
        /// 功能：提取队列存储数据
        /// </summary>
        /// <returns>存储数据</returns>
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
