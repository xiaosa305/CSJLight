using MultiLedController.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static MultiLedController.utils.impl.Art_Net_Manager;

namespace MultiLedController.utils
{
    public interface IArt_Net_Manager
    {
        /// <summary>
        /// 功能：启动虚拟控制器对接麦爵士
        /// </summary>
        /// <param name="virtuals">虚拟控制器信息，包含虚拟控制器使用的Ip地址以及虚拟控制器空间数量</param>
        /// <param name="serverIp">麦爵士所在的服务器IP</param>
        /// <param name="currentMainIP">本地主IP</param>
        void Start(List<VirtualControlInfo> virtuals, string currentIP, string serverIp,ControlDevice device);
        /// <summary>
        /// 功能：关闭服务器
        /// </summary>
        void Close();
        /// <summary>
        /// 功能：发送启动实时调试命令
        /// </summary>
        void StartDebug(GetPlayFrameCount frameCount);
        /// <summary>
        /// 功能：启动实时调试
        /// </summary>
        void StartDebugMode();
        /// <summary>
        /// 功能：关闭实时调试
        /// </summary>
        void StopDebug();
        /// <summary>
        /// 功能：搜索设备
        /// </summary>
        /// <param name="currentMainIp">本机主要IP</param>
        void SearchDevice(string currentMainIp);
        /// <summary>
        /// 功能：获取控制器列表
        /// </summary>
        /// <returns>搜索到的设备信息字典，KEY为控制器MAC，VALUE为控制器信息</returns>
        Dictionary<string, ControlDevice> GetLedControlDevices();
        /// <summary>
        /// 功能：修改存储文件路径
        /// </summary>
        /// <param name="dirPath">新的文件存储路径</param>
        void SetSaveFilePath(string filePath);
        /// <summary>
        /// 功能：启动数据存储至文件
        /// </summary>
        void StartSaveToFile(GetRecodeFrameCount frameCount);
        /// <summary>
        /// 功能：关闭数据存储至文件
        /// </summary>
        void StopSaveToFile();
    }
}
