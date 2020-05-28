﻿using MultiLedController.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.multidevice
{
    public interface ITransactionManager
    {
        /// <summary>
        /// 功能：搜索设备
        /// </summary>
        /// <param name="obj"></param>
        void SearchDevice(string localIp);
        /// <summary>
        /// 功能：获取设备列表
        /// </summary>
        /// <returns></returns>
        Dictionary<string, ControlDevice> GetControlDevicesList();
        /// <summary>
        /// 功能：清除设备列表
        /// </summary>
        void ClearControlDeviceList();

        void AddDevice(List<ControlDevice> devices, List<List<string>> ips, string serverIp);

        void StartReceiveDmxData();

        void StopReceiveDmxData();

        void StartDebug();

        void StopDebug();

        void StartRecode();

        void StopRecode();
    }
}
