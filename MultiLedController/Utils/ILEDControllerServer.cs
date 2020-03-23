using MultiLedController.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.Utils
{
    public interface ILEDControllerServer
    {
        /// <summary>
        /// 功能：设置管理器
        /// </summary>
        /// <param name="manager"></param>
        void SetManager(IArt_Net_Manager manager);
        /// <summary>
        /// 功能：配置本地服务器IP并启动服务器
        /// </summary>
        /// <param name="ip"></param>
        void StartServer(string ip);
        /// <summary>
        /// 功能：网络发送模块
        /// </summary>
        /// <param name="data"></param>
        void SendDebugData(List<byte> data);
        /// <summary>
        /// 功能：搜索设备
        /// </summary>
        /// <param name="data"></param>
        void SearchDevice(List<byte> data);
        /// <summary>
        /// 功能：初始化设备存储缓存区
        /// </summary>
        void InitDeviceList();
        /// <summary>
        /// 功能：获取设备列表
        /// </summary>
        /// <returns></returns>
        Dictionary<string, ControlDevice> GetControlDevices();
    }
}
