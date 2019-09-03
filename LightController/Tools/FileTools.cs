using LighEditor.Ast;
using LighEditor.Tools.CSJ.IMPL;
using LighEditor.Tools.CSJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LighEditor.Tools
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
                    if ((file as CSJ_C).SceneNo ==Constant.SCENE_ALL_ON)
                    {
                        (file as CSJ_C).SceneNo = Constant.SCENE_ALL_ON_NO;
                        file.WriteToFile(savePath);
                    }
                    else if ((file as CSJ_C).SceneNo == Constant.SCENE_ALL_OFF)
                    {
                        (file as CSJ_C).SceneNo = Constant.SCENE_ALL_OFF_NO;
                        file.WriteToFile(savePath);
                    }
                }
            }
            if (project.MFiles != null)
            {
                foreach (ICSJFile file in project.MFiles)
                {
                    file.WriteToFile(savePath);
                    if ((file as CSJ_M).SceneNo == Constant.SCENE_ALL_ON)
                    {
                        (file as CSJ_M).SceneNo = Constant.SCENE_ALL_ON_NO;
                        file.WriteToFile(savePath);
                    }
                    else if ((file as CSJ_M).SceneNo == Constant.SCENE_ALL_OFF)
                    {
                        (file as CSJ_M).SceneNo = Constant.SCENE_ALL_OFF_NO;
                        file.WriteToFile(savePath);
                    }
                }
            }
            if (project.ConfigFile != null)
            {
                project.ConfigFile.WriteToFile(savePath);
            }
            ScenesInitData scenesInitData = new ScenesInitData(project);
            if (null != scenesInitData)
            {
                scenesInitData.WriteToFile(savePath);
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
