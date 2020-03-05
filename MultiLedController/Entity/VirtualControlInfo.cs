using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.Entity
{
    public class VirtualControlInfo
    {
        public string IP { get; set; }
        public int SpaceNum { get; set; }
        /// <summary>
        /// 虚拟控制器构造函数
        /// </summary>
        /// <param name="ip">分配给虚拟控制器的IP地址</param>
        /// <param name="device">对应控制器的信息</param>
        public VirtualControlInfo(string ip, ControlDevice device)
        {
            this.IP = ip;
            this.SpaceNum = device.Led_space;
        }
    }
}
