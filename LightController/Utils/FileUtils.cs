using LightController.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LightController.Utils
{
    public class FileUtils
    {
        private static string DataCacheFilePath = Application.StartupPath + @"\DataCache\Project\Cache";
        private static string ProjectDataFilePath = Application.StartupPath + @"\DataCache\Project\CSJ";
        private static readonly Object _Lock = new object();
        private FileUtils() { }
        private static void InitFilePath()
        {
            try
            {
                if (!Directory.Exists(DataCacheFilePath))
                {
                    Directory.CreateDirectory(DataCacheFilePath);
                }
                if (!Directory.Exists(ProjectDataFilePath))
                {
                    Directory.CreateDirectory(ProjectDataFilePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void ClearCacheData()
        {
            if (Directory.Exists(DataCacheFilePath))
            {
                Directory.Delete(DataCacheFilePath, true);
            }
        }
        public static void ClearProjectData()
        {
            if (Directory.Exists(ProjectDataFilePath))
            {
                Directory.Delete(ProjectDataFilePath, true);
            }
        }
        public static bool Write(byte[] datas,int length, string fileName, bool isCreate, bool isCache)
        {
            InitFilePath();
            try
            {
                string filePath;
                if (isCache)
                {
                    filePath = DataCacheFilePath + @"\" + fileName;
                }
                else
                {
                    filePath = ProjectDataFilePath + @"\" + fileName;
                }
                FileStream stream;
                using (isCreate ? stream = new FileStream(filePath, FileMode.Create) : stream = new FileStream(filePath, FileMode.Append))
                {
                    stream.Write(datas, 0, length);
                    stream.Flush();
                }
                return true;
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
                Console.WriteLine("******************线程" + Thread.CurrentThread.ManagedThreadId + ":" + ex.ToString());
                return false;
            }
        }
        public static bool Write(byte data, string fileName, bool isCreate, bool isCache)
        {
            InitFilePath();
            try
            {
                string filePath;
                if (isCache)
                {
                    filePath = DataCacheFilePath + @"/" + fileName;
                }
                else
                {
                    filePath = ProjectDataFilePath + @"/" + fileName;
                }
                FileStream stream;
                using (isCreate ? stream = new FileStream(filePath, FileMode.Create) : stream = new FileStream(filePath, FileMode.Append))
                {
                    stream.WriteByte(data);
                    stream.Flush();
                }
                return true;
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public static void Write(byte[] datas,int length, long seek, string fileName, bool isCreate,bool isCache)
        {
            InitFilePath();
            try
            {
                string filePath;
                if (isCache)
                {
                    filePath = DataCacheFilePath + @"/" + fileName;
                }
                else
                {
                    filePath = ProjectDataFilePath + @"/" + fileName;
                }
                FileStream stream;
                using (isCreate ? stream = new FileStream(filePath, FileMode.Create) : stream = new FileStream(filePath, FileMode.Open,FileAccess.Write))
                {
                    stream.Seek(seek, SeekOrigin.Begin);
                    stream.Write(datas, 0, length);
                    stream.Flush();
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
                Console.WriteLine(ex.ToString());
            }

        }
        public static void MergeFile(int sceneNo)
        {
            string projectFilePath = ProjectDataFilePath + @"\C" + (sceneNo + 1) + ".bin";
            FileInfo projectFileInfo = new FileInfo(projectFilePath);
            long seek = projectFileInfo.Length;
            byte[] readBuff = new byte[1024*20];
            int readSize = -1;
            if (Directory.Exists(DataCacheFilePath))
            {
                foreach (string filePath in Directory.GetFileSystemEntries(DataCacheFilePath))
                {
                    long channelDatasSize = 0;
                    string projectFileNPath = filePath.Substring(filePath.LastIndexOf('\\') + 1);
                    string[] strArray = projectFileNPath.Split('.');
                    string name = strArray[0];
                    string strChannel = name.Split('-')[1];
                    string strScene = name.Split('-')[0].Substring(1);
                    int.TryParse(strScene, out int intScneNo);
                    int.TryParse(strChannel, out int intChannelNo);
                   
                    if (intScneNo == (sceneNo + 1))
                    {
                        FileStream readStream = new FileStream(filePath, FileMode.Open);
                        //FileInfo fileInfo = new FileInfo(filePath);
                        channelDatasSize = readStream.Length;
                        seek = seek + 10;
                        byte[] channelDataHead = new byte[] 
                        {
                            Convert.ToByte(intChannelNo & 0xFF),
                            Convert.ToByte((intChannelNo >> 8) & 0xFF),
                            Convert.ToByte(channelDatasSize & 0xFF),
                            Convert.ToByte((channelDatasSize >> 8) & 0xFF),
                            Convert.ToByte((channelDatasSize >> 16) & 0xFF),
                            Convert.ToByte((channelDatasSize >> 24) & 0xFF),
                            Convert.ToByte(seek & 0xFF),
                            Convert.ToByte((seek >> 8) & 0xFF),
                            Convert.ToByte((seek >> 16) & 0xFF),
                            Convert.ToByte((seek >> 24) & 0xFF),
                        };
                        Write(channelDataHead,channelDataHead.Length, projectFileInfo.Name, false, false);
                        while ((readSize = readStream.Read(readBuff,0,readBuff.Length)) != 0)
                        {
                            Write(readBuff, readSize, projectFileInfo.Name, false, false);
                        }
                        readStream.Close();
                        seek = seek + channelDatasSize;
                    }
                }
                FileInfo fileInfo = new FileInfo(projectFilePath);
                byte[] fileSize = new byte[]
                {
                            Convert.ToByte(fileInfo.Length & 0xFF),
                            Convert.ToByte((fileInfo.Length >> 8) & 0xFF),
                            Convert.ToByte((fileInfo.Length >> 16) & 0xFF),
                            Convert.ToByte((fileInfo.Length >> 24) & 0xFF),
                };
                Write(fileSize, fileSize.Length, 0, fileInfo.Name, false, false);
            }
        }
    }
}
