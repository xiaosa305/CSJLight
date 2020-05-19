using MultiLedController.entity;
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
        List<ControlDevice> GetControlDevicesList();
        /// <summary>
        /// 功能：清除设备列表
        /// </summary>
        void ClearControlDeviceList();
    }
}
