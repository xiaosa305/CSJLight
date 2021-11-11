using LightController.Tools;
using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static LightController.Xiaosa.Entity.CallBackFunction;

namespace LightController.Utils
{
    public class FileUtils
    {
        private static string ProjectDataFilePath = Application.StartupPath + @"\DataCache\Project\CSJ";
        private static string ProjectDownloadDir = Application.StartupPath + @"\DataCache\Download\CSJ";
        public static readonly int MODE_PREVIEW = 11;
        public static readonly int MODE_MAKEFILE = 12;
        private FileUtils() { }
        public static void ClearProjectData()
        {
            try
            {
                if (Directory.Exists(ProjectDataFilePath))
                {
                    Directory.Delete(ProjectDataFilePath, true);
                }
            }
            catch (Exception)
            {
            }
        }
        public static bool CopyFileToDownloadDir(string dirPath)
        {
            try
            {
                if (!Directory.Exists(ProjectDownloadDir))
                {
                    Directory.CreateDirectory(ProjectDownloadDir);
                }
                else
                {
                    Directory.Delete(ProjectDownloadDir, true);
                    Directory.CreateDirectory(ProjectDownloadDir);
                }
                if (Directory.Exists(dirPath))
                {
					string[] strs = Directory.GetFileSystemEntries(dirPath);

					foreach (string filePath in Directory.GetFileSystemEntries(dirPath))
                    {
						if ( File.Exists(filePath)){
							FileInfo info = new FileInfo(filePath);
							info.CopyTo(ProjectDownloadDir + @"\" + info.Name, true);
						}							
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool CopyProjectFileToDownloadDir()
        {
            bool result = false;
            try
            {
                if (!Directory.Exists(ProjectDownloadDir))
                {
                    Directory.CreateDirectory(ProjectDownloadDir);
                }
                else
                {
                    Directory.Delete(ProjectDownloadDir, true);
                    Directory.CreateDirectory(ProjectDownloadDir);
                }
                if (Directory.Exists(ProjectDataFilePath))
                {
                    foreach (string filePath in Directory.GetFileSystemEntries(ProjectDataFilePath))
                    {
                        FileInfo info = new FileInfo(filePath);
                        info.CopyTo(ProjectDownloadDir + @"\" + info.Name, true);
                    }
                    result = true;
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
        public static bool ExportProjectFile(string exportPath)
        {
            bool result = false;
            string dirPath = exportPath;
            try
            {
                if (Directory.Exists(dirPath))
                {
                    Directory.Delete(dirPath, true);
                }
                Thread.Sleep(200);
                Directory.CreateDirectory(dirPath);
                if (Directory.Exists(ProjectDataFilePath))
                {
                    foreach (string filePath in Directory.GetFileSystemEntries(ProjectDataFilePath))
                    {
                        FileInfo info = new FileInfo(filePath);
                        info.CopyTo(dirPath + @"\" + info.Name, true);
                    }
                    result = true;
                }
            }
            catch (Exception)
            {
            }
          
            return result;
        }
    }
}
