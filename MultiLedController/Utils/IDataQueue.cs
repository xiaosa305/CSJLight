using MultiLedController.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.Utils
{
    public interface IDataQueue
    {
        /// <summary>
        /// 功能：复位队列缓存区
        /// </summary>
        void Reset();
        /// <summary>
        /// 功能：推送实时调试数据到队列
        /// </summary>
        /// <param name="data">实时调试数据</param>
        /// <param name="framTime">帧间隔时间</param>
        void DebugEnqueue(Dictionary<int, List<byte>> data, int framTime);
        /// <summary>
        /// 功能：推送文件存储数据到队列
        /// </summary>
        /// <param name="data">存储数据</param>
        /// <param name="framTime">帧间隔时间</param>
        /// <param name="led_Interface_num">控制卡线路数量</param>
        /// <param name="led_space">控制卡空间数量</param>
        void SaveEnqueue(Dictionary<int, List<byte>> data, int framTime, int led_Interface_num, int led_space);
        /// <summary>
        /// 功能：提取队列实时调试数据
        /// </summary>
        /// <returns>实时调试数据</returns>
        DebugQueueCacheData DebugDequeue();
        /// <summary>
        /// 功能：提取队列存储数据
        /// </summary>
        /// <returns>存储数据</returns>
        SaveQueueCacheData SaveDequeue();
    }
}
