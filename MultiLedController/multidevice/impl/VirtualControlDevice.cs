using MultiLedController.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.multidevice.impl
{
    public class VirtualControlDevice
    {
        private List<VirtualClient> VirtualClients { get; set; }
        private int VirtualDeviceIndex { get; set; }

        public VirtualControlDevice(int index, int startLedSpace, ControlDevice device, List<string> ips,string serverIp)
        {
            this.VirtualDeviceIndex = index;
            this.InitParameter();
            this.CreateVirtualClient(startLedSpace, device, ips,serverIp);
        }

        private void InitParameter()
        {
            this.VirtualClients = new List<VirtualClient>();
        }

        private void CreateVirtualClient(int startLedSpace, ControlDevice device,List<string> ips, string serverIp)
        {
            for (int virtualClientIndex = 0; virtualClientIndex < device.Led_interface_num; virtualClientIndex++)
            {
                //新建虚拟客户端
                VirtualClient virtualClient = new VirtualClient(startLedSpace, device, ips[virtualClientIndex], serverIp);
                //将虚拟客户端添加到虚拟客户端池中
                this.VirtualClients.Add(virtualClient);
                //调整下一个虚拟客户端首个空间编号
                startLedSpace += device.Led_space;
            }
        }
    }
}
