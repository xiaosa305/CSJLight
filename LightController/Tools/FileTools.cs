using LightController.Ast;
using LightController.Tools.CSJ.IMPL;
using LightController.Tools.CSJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    public class FileTools
    {
        private static FileTools Instance { get; set; }

        private FileTools()
        {
        }
        public static FileTools GetInstance()
        {
            if (Instance == null)
            {
                Instance = new FileTools();
            }
            return Instance;
        }
        /// <summary>
        /// 生成工程文件
        /// </summary>
        /// <param name="wrapper">数据库数据</param>
        /// <param name="configPath">全局配置文件路径</param>
        /// <param name="savePath">工程文件存储路径</param>
        public void ProjectToFile(DBWrapper wrapper,string configPath,string savePath)
        {
            CSJ_Project project = DmxDataConvert.GetInstance().GetCSJProjectFiles(wrapper, configPath);
            if (project.CFiles != null)
            {
                foreach (ICSJFile file in project.CFiles)
                {
                    file.WriteToFile(savePath);
                }
            }
            if (project.MFiles != null)
            {
                foreach (ICSJFile file in project.MFiles)
                {
                    file.WriteToFile(savePath);
                }
            }
            if (project.ConfigFile != null)
            {
                project.ConfigFile.WriteToFile(savePath);
            }
        }
        /// <summary>
        /// 生成硬件配置文件
        /// </summary>
        /// <param name="hardwarePath">硬件配置数据文件路径</param>
        /// <param name="savePath">硬件配置文件存储路径</param>
        public void HardwareToFile(string hardwarePath,string savePath)
        {
            CSJ_Hardware hardware = new CSJ_Hardware(hardwarePath);
            hardware.WriteToFile(savePath);
        }
    }
}
