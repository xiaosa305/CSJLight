using LightController.Ast;
using LightController.Tools.CSJ.IMPL;
using LightController.Tools.CSJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
        public void ProjectToFile(DBWrapper wrapper,string configPath,string savePath)
        {
            this.DelectDir(savePath);
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
                ScenesInitData scenesInitData = new ScenesInitData(project);
                if (null != scenesInitData)
                {
                    scenesInitData.WriteToFile(savePath);
                }
            }
           
        }
        public void HardwareToFile(string hardwarePath,string savePath)
        {
            CSJ_Hardware hardware = new CSJ_Hardware(hardwarePath);
            hardware.WriteToFile(savePath);
        }
        private void DelectDir(string path)
        {
            try
            {
                DirectoryInfo info = new DirectoryInfo(path);
                FileSystemInfo[] fileInfo = info.GetFileSystemInfos();
                foreach (FileSystemInfo item in fileInfo)
                {
                    if (item is DirectoryInfo)
                    {
                        DirectoryInfo subdir = new DirectoryInfo(item.FullName);
                        subdir.Delete(true);
                    }
                    else
                    {
                        File.Delete(item.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
        }
    }
}
