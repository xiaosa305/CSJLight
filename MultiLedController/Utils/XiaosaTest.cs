using MultiLedController.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MultiLedController.Utils
{
    public static class XiaosaTest
    {
        private static int Temp = 0;

        /// <summary>
        /// 1.调用Art_Net_Manager:SearchDevice()方法传入本机主IP进行搜索设备
        /// 2.搜索成功才允许软件的其他操作，调用Art_Net_Manager:GetLedControlDevices()获取搜索到的设备目录
        /// 3.根据搜索到的设备数量，获取相应数量的子IP，将设备信息中的SpaceNum与配置的IP实例化一个对应的VirtualControlInfo
        /// 4.将List<VirtualControlInfo>以及麦爵士软件所在设备IP作为入参调用Art_Net_Manager:Start()启动虚拟客户端用于接收麦爵士数据
        /// 5.启动实时调试功能：调用Art_Net_Manager:SendStartDebugOrder()方法进行启动，调用Art_Net_Manager:EndDebug()方法关闭实时调试
        /// </summary>
        public static void Test1()
        {
            switch (Temp)
            {
                case 0:
                    Art_Net_Manager.GetInstance().TestStart();
                    break;
                case 1:
                default:
                    //Art_Net_Manager.GetInstance().SearchDevice("192.168.1.21");
                    Art_Net_Manager.GetInstance().SendStartDebugOrder();
                    break;
            }
            Temp++;
        }
    }
}
