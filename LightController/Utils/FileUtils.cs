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

namespace LightController.Utils
{
    public class FileUtils
    {
        private static string DataCacheFilePath = Application.StartupPath + @"\DataCache\Project\Cache";
        private static string ProjectDataFilePath = Application.StartupPath + @"\DataCache\Project\CSJ";
        private static string PreviewDataCachePath = Application.StartupPath + @"\DataCache\Preview\Cache";
        private static string PreviewDataFilePath = Application.StartupPath + @"\DataCache\Preview\CSJ";
        private static string ProjectDownloadDir = Application.StartupPath + @"\DataCache\Download\CSJ";

        private static readonly Object _Lock = new object();
        public static readonly int MODE_PREVIEW = 11;
        public static readonly int MODE_MAKEFILE = 12;
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
                if (!Directory.Exists(PreviewDataCachePath))
                {
                    Directory.CreateDirectory(PreviewDataCachePath);
                }
                if (!Directory.Exists(PreviewDataFilePath))
                {
                    Directory.CreateDirectory(PreviewDataFilePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void ClearCacheData()
        {
            try
            {
                if (Directory.Exists(DataCacheFilePath))
                {
                    Directory.Delete(DataCacheFilePath, true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void ClearProjectData()
        {
            try
            {
                if (Directory.Exists(ProjectDataFilePath))
                {
                    Directory.Delete(ProjectDataFilePath, true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void ClearPreviewCacheData()
        {
            try
            {
                if (Directory.Exists(PreviewDataCachePath))
                {
                    Directory.Delete(PreviewDataCachePath, true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void ClearPreviewProjectData()
        {
            try
            {
                if (Directory.Exists(PreviewDataFilePath))
                {
                    Directory.Delete(PreviewDataFilePath, true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void Write(byte[] datas,int length, string fileName,bool isMakeFile, bool isCreate, bool isCache)
        {
            InitFilePath();
            FileStream stream = null;
            try
            {
                string filePath;
                if (isCache)
                {
                    filePath = (isMakeFile ? DataCacheFilePath : PreviewDataCachePath)  + @"\" + fileName;
                }
                else
                {
                    filePath = (isMakeFile ? ProjectDataFilePath : PreviewDataFilePath) + @"\" + fileName;
                }
                //TODO 修改中
                //using (isCreate ? stream = new FileStream(filePath, FileMode.Create,FileAccess.Write) : stream = new FileStream(filePath, FileMode.Append,FileAccess.Write))
                using (stream = new FileStream(filePath, isCreate ? FileMode.Create : FileMode.Append))
                {
                    stream.Write(datas, 0, length);
                    stream.Flush();
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
        }
        public static void Write(byte data, string fileName,bool isMakeFile, bool isCreate, bool isCache)
        {
            InitFilePath();
            FileStream stream = null;
            try
            {
                string filePath;
                if (isCache)
                {
                    filePath = (isMakeFile ? DataCacheFilePath : PreviewDataCachePath) + @"/" + fileName;
                }
                else
                {
                    filePath = (isMakeFile ? ProjectDataFilePath : PreviewDataFilePath) + @"/" + fileName;
                }
                //TODO 修改中
                //using (isCreate ? stream = new FileStream(filePath, FileMode.Create,FileAccess.Write) : stream = new FileStream(filePath, FileMode.Append,FileAccess.Write))
                using (stream = new FileStream(filePath, isCreate ? FileMode.Create : FileMode.Append))
                {
                    stream.WriteByte(data);
                    stream.Flush();
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
                Console.WriteLine(ex.ToString());
            }
        }
        public static void Write(byte[] datas,int length, long seek, string fileName,bool isMakeFile, bool isCreate,bool isCache)
        {
            InitFilePath();
            FileStream stream = null;
            try
            {
                string filePath;
                if (isCache)
                {
                    filePath = (isMakeFile ? DataCacheFilePath : PreviewDataCachePath) + @"/" + fileName;
                }
                else
                {
                    filePath = (isMakeFile ? ProjectDataFilePath : PreviewDataFilePath) + @"/" + fileName;
                }
                //TODO 修改中
                //using (isCreate ? stream = new FileStream(filePath, FileMode.Create,FileAccess.Write) : stream = new FileStream(filePath, FileMode.Open, FileAccess.Write))
                using (stream = new FileStream(filePath, isCreate ? FileMode.Create : FileMode.Open, FileAccess.Write))
                {
                    stream.Seek(seek, SeekOrigin.Begin);
                    stream.Write(datas, 0, length);
                    stream.Flush();
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
                Console.WriteLine("******************" + Thread.CurrentThread.ManagedThreadId + "==============>" + ex.ToString());
            }
        }
        public static void MergeFile(int sceneNo, int mode, bool isMakeFile, bool isCompleted , ISaveProjectCallBack callBack)
        {
            
            string projectFilePath =  (isMakeFile ? ProjectDataFilePath : PreviewDataFilePath) + (mode == Constant.MODE_C ? @"\C" : @"\M") + (sceneNo + 1) + ".bin";
            FileInfo projectFileInfo = new FileInfo(projectFilePath);
            string projectCacheFilePath = (isMakeFile ? DataCacheFilePath : PreviewDataCachePath);
            long seek = projectFileInfo.Length;
            byte[] readBuff = new byte[1024 * 20];
            FileStream readStream = null;
            int readSize = -1;
            int fileCount = 0;
            try
            {
                if (Directory.Exists(projectCacheFilePath))
                {
                    foreach (string filePath in Directory.GetFileSystemEntries(projectCacheFilePath))
                    {
                        long channelDatasSize = 0;
                        string projectFileNPath = filePath.Substring(filePath.LastIndexOf('\\') + 1);
                        if (mode == Constant.MODE_C)
                        {
                            if (!projectFileNPath[0].Equals('C'))
                            {
                                continue;
                            }
                        }
                        else
                        {
                            if (!projectFileNPath[0].Equals('M'))
                            {
                                continue;
                            }
                        }
                        string[] strArray = projectFileNPath.Split('.');
                        string name = strArray[0];
                        string strChannel = name.Split('-')[1];
                        string strScene = name.Split('-')[0].Substring(1);
                        int.TryParse(strScene, out int intScneNo);
                        int.TryParse(strChannel, out int intChannelNo);

                        if (intScneNo == (sceneNo + 1))
                        {
                            fileCount++;
                            using (readStream = new FileStream(filePath, FileMode.Open))
                            {
                                //FileInfo fileInfo = new FileInfo(filePath);
                                channelDatasSize = readStream.Length;
                                seek = seek + 8;
                                //TODO 2.0版本
                                //seek = seek + 10;
                                byte[] channelDataHead = new byte[]
                                {
                                      Convert.ToByte(intChannelNo & 0xFF),
                            Convert.ToByte((intChannelNo >> 8) & 0xFF),
                            Convert.ToByte(channelDatasSize & 0xFF),
                            Convert.ToByte((channelDatasSize >> 8) & 0xFF),

                            //TODO 2.0版本
                            //Convert.ToByte((channelDatasSize >> 16) & 0xFF),
                            //Convert.ToByte((channelDatasSize >> 24) & 0xFF),

                            Convert.ToByte(seek & 0xFF),
                            Convert.ToByte((seek >> 8) & 0xFF),
                            Convert.ToByte((seek >> 16) & 0xFF),
                            Convert.ToByte((seek >> 24) & 0xFF),
                                };
                                Write(channelDataHead, channelDataHead.Length, projectFileInfo.Name, isMakeFile, false, false);
                                while ((readSize = readStream.Read(readBuff, 0, readBuff.Length)) != 0)
                                {
                                    Write(readBuff, readSize, projectFileInfo.Name, isMakeFile, false, false);
                                }
                                seek = seek + channelDatasSize;
                                //readStream.Close();
                            }
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
                    if (fileCount == 0)
                    {
                        File.Delete(projectFilePath);
                    }
                    else
                    {
                        Write(fileSize, fileSize.Length, 0, fileInfo.Name, isMakeFile, false, false);
                    }

                }
                Console.WriteLine(projectFileInfo.Name + "文件整合完成");
            }
            catch (Exception ex)
            {
                if (readStream != null)
                {
                    readStream.Close();
                }
                callBack.Error();
                Console.WriteLine("********************数据整合出错" + ex.StackTrace);
            }finally
            {
                if (isCompleted)
                {
                    if (isMakeFile)
                    {
                        CreateGradientData();
                    }
                    Console.WriteLine("XIAOSA：==>数据全部整合完毕");
                    callBack.Completed();
                }
                DataConvertUtils.Flag = true;
            }
        }
        public static bool CopyFileToDownloadDir(string dirPath)
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
                if (Directory.Exists(dirPath))
                {
                    foreach (string filePath in Directory.GetFileSystemEntries(dirPath))
                    {
                        FileInfo info = new FileInfo(filePath);
                        info.CopyTo(ProjectDownloadDir + @"\" + info.Name, true);
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("拷贝文件到下载目录报错" + ex.StackTrace);
            }
            return result;
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
            catch (Exception ex)
            {
                Console.WriteLine("拷贝工程文件到下载目录报错" + ex.StackTrace);
            }
            return result;
        }
        public static bool ExportProjectFile(string exportPath)
        {
            bool result = false;
            string dirPath = exportPath;
            try
            {
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                else
                {
                    Directory.Delete(dirPath, true);
                    Directory.CreateDirectory(dirPath);
                }
                if (Directory.Exists(ProjectDataFilePath))
                {
                    foreach (string filePath in Directory.GetFileSystemEntries(ProjectDataFilePath))
                    {

                        FileInfo info = new FileInfo(filePath);
                        Console.WriteLine("拷贝工程文件到指定目录：==>" + info.Name);
                        info.CopyTo(dirPath + @"\" + info.Name, true);
                    }
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("拷贝工程文件到指定目录" + ex.StackTrace);
            }
          
            return result;
        }
        public static List<PlayPoint> GetCPlayPoints()
        {
            FileStream readStream = null;
            int channelCount = 0;
            byte[] channelNumberBuff = new byte[2];
            long seek = 9;
            List<PlayPoint> playPoints = new List<PlayPoint>();
            using (readStream = new FileStream(PreviewDataFilePath + @"\C1.bin", FileMode.Open))
            {
                readStream.Seek(seek, SeekOrigin.Begin);
                readStream.Read(channelNumberBuff, 0, 2);
                channelCount = (channelNumberBuff[0] & 0xFF) | ((channelNumberBuff[1] & 0xFF) << 8);
                seek += 2;
                for (int channel = 0; channel < channelCount; channel++)
                {
                    int channelNo = 0;
                    int dataLength = 0;
                    long channelSeek = 0;
                    byte[] channelNoBuff = new byte[2];
                    byte[] seekBuff = new byte[4];
                    byte[] lengthBuff = new byte[4];
                    //读取通道编号
                    readStream.Seek(seek, SeekOrigin.Begin);
                    readStream.Read(channelNoBuff, 0, channelNoBuff.Length);
                    channelNo = (channelNoBuff[0] & 0xFF) | ((channelNoBuff[1] & 0xFF) << 8);
                    seek = seek + 2;
                    //读取数据长度
                    readStream.Seek(seek, SeekOrigin.Begin);
                    readStream.Read(lengthBuff, 0, lengthBuff.Length);

                    dataLength = (lengthBuff[0] & 0xFF) | ((lengthBuff[1] & 0xFF) << 8);
                    seek = seek + 2;
                    //TODO 2.0-1
                    //dataLength = (lengthBuff[0] & 0xFF) | ((lengthBuff[1] & 0xFF) << 8) | ((lengthBuff[2] & 0xFF) << 16) | ((lengthBuff[3] & 0xFF) << 24);
                    //seek = seek + 4;
                    //读取字节偏移量
                    readStream.Seek(seek, SeekOrigin.Begin);
                    readStream.Read(seekBuff, 0, seekBuff.Length);
                    channelSeek = (seekBuff[0] & 0xFF) | ((seekBuff[1] & 0xFF) << 8) | ((seekBuff[2] & 0xFF) << 16) | ((seekBuff[3] & 0xFF) << 24);
                    playPoints.Add(new PlayPoint(channelNo, channelSeek, dataLength,Constant.MODE_C));
                    seek = seek + dataLength + 4;
                }
            }
            foreach (PlayPoint item in playPoints)
            {
                item.Init();
            }
            return playPoints;
        }
        public static List<PlayPoint> GetMPlayPoints()
        {
            FileStream readStream = null;
            int channelCount = 0;
            byte[] channelNumberBuff = new byte[2];
            long seek = 28;
            List<PlayPoint> playPoints = new List<PlayPoint>();
            using (readStream = new FileStream(PreviewDataFilePath + @"\M1.bin", FileMode.Open))
            {
                readStream.Seek(seek, SeekOrigin.Begin);
                readStream.Read(channelNumberBuff, 0, 2);
                channelCount = (channelNumberBuff[0] & 0xFF) | ((channelNumberBuff[1] & 0xFF) << 8);
                seek += 2;
                for (int channel = 0; channel < channelCount; channel++)
                {
                    int channelNo = 0;
                    int dataLength = 0;
                    long channelSeek = 0;
                    byte[] channelNoBuff = new byte[2];
                    byte[] seekBuff = new byte[4];
                    byte[] lengthBuff = new byte[4];
                    //读取通道编号
                    readStream.Seek(seek, SeekOrigin.Begin);
                    readStream.Read(channelNoBuff, 0, channelNoBuff.Length);
                    channelNo = (channelNoBuff[0] & 0xFF) | ((channelNoBuff[1] & 0xFF) << 8);
                    seek = seek + 2;
                    //读取数据长度
                    readStream.Seek(seek, SeekOrigin.Begin);
                    readStream.Read(lengthBuff, 0, lengthBuff.Length);

                    dataLength = (lengthBuff[0] & 0xFF) | ((lengthBuff[1] & 0xFF) << 8);
                    seek = seek + 2;
                    //TODO 2.0-2
                    //dataLength = (lengthBuff[0] & 0xFF) | ((lengthBuff[1] & 0xFF) << 8) | ((lengthBuff[2] & 0xFF) << 16) | ((lengthBuff[3] & 0xFF) << 16);
                    //seek = seek + 4;
                    //读取字节偏移量
                    readStream.Seek(seek, SeekOrigin.Begin);
                    readStream.Read(seekBuff, 0, seekBuff.Length);
                    channelSeek = (seekBuff[0] & 0xFF) | ((seekBuff[1] & 0xFF) << 8) | ((seekBuff[2] & 0xFF) << 16) | ((seekBuff[3] & 0xFF) << 24);
                    playPoints.Add(new PlayPoint(channelNo, channelSeek, dataLength,Constant.MODE_M));
                    seek = seek + dataLength + 4;
                }
            }
            foreach (PlayPoint item in playPoints)
            {
                item.Init();
            }
            Thread.Sleep(50);
            return playPoints;
        }
        public static int GetMusicTime()
        {
            int result = 0;
            FileStream readStream = null;
            using (readStream = new FileStream(PreviewDataFilePath + @"\M1.bin", FileMode.Open))
            {
                readStream.Seek(4,SeekOrigin.Begin);
                result = readStream.ReadByte();
            }
            Thread.Sleep(50);
            return result;
        }
        public static int GetMusicIntervalTime()
        {
            int result = 0;
            byte[] readBuff = new byte[2];
            FileStream readStream = null;
            using (readStream = new FileStream(PreviewDataFilePath + @"\M1.bin", FileMode.Open))
            {
                readStream.Seek(5, SeekOrigin.Begin);
                readStream.Read(readBuff, 0, 2);
            }
            result = (readBuff[0] & 0xFF) | ((readBuff[1] & 0xFF) << 8);
            Thread.Sleep(50);
            return result;
        }
        public static int GetMusicStepCount()
        {
            int result = 0;
            FileStream readStream = null;
            using (readStream = new FileStream(PreviewDataFilePath + @"\M1.bin", FileMode.Open))
            {
                readStream.Seek(7, SeekOrigin.Begin);
                result = readStream.ReadByte();
            }
            Thread.Sleep(50);
            return result;
        }
        public static int[] GetMusicStepList()
        {
            FileStream readStream = null;
            int[] result = new int[20];
            byte[] readBuff = new byte[20];
            using (readStream = new FileStream(PreviewDataFilePath + @"\M1.bin", FileMode.Open))
            {
                readStream.Seek(8, SeekOrigin.Begin);
                readStream.Read(readBuff, 0, 20);
            }
            for (int i = 0; i < 20; i++)
            {
                result[i] = Convert.ToInt32(readBuff[i]);
            }
            Thread.Sleep(50);
            return result;
        }
        public static bool IsMusicFile()
        {
            return File.Exists(PreviewDataFilePath + @"\M1.bin") && File.Exists(PreviewDataFilePath + @"\C1.bin");
        }
        public static bool IsDefaultFIle()
        {
            return File.Exists(PreviewDataFilePath + @"\C1.bin");
        }
        public static void CreateGradientData()
        {
            string dirpath = Application.StartupPath + @"\DataCache\Project\CSJ";
            byte[][] gradientData = new byte[32][];
            FileStream readStream = null;
            int fileSize = 4;
            long seek = 9;
            //try
            //{
                for (int i = 0; i < 32; i++)
                {
                    gradientData[i] = Enumerable.Repeat(Convert.ToByte(0x00), 512).ToArray();
                }
                if (Directory.Exists(dirpath))
                {
                    foreach (string filePath in Directory.GetFileSystemEntries(dirpath))
                    {
                        string projectFileNPath = filePath.Substring(filePath.LastIndexOf('\\') + 1);
                        if (projectFileNPath.Equals("Config.bin"))
                        {
                            continue;
                        }
                        seek = 9;
                        if (!projectFileNPath[0].Equals('C'))
                        {
                            continue;
                        }
                        string[] strArray = projectFileNPath.Split('.');
                        string name = strArray[0];
                        string strScene = name.Length > 2 ? (name[1].ToString() + name[2].ToString()) : name[1].ToString();
                        int.TryParse(strScene, out int intScneNo);
                        readStream = new FileStream(filePath, FileMode.Open);
                        byte[] channelNumberBuff = new byte[2];
                        int channelCount = 0;
                        readStream.Seek(seek, SeekOrigin.Begin);
                        readStream.Read(channelNumberBuff, 0, channelNumberBuff.Count());
                        channelCount = (channelNumberBuff[0] & 0xFF) | ((channelNumberBuff[1] & 0xFF) << 8);
                        seek = seek + 2;
                        for (int i = 0; i < channelCount; i++)
                        {
                            int channelNo = 0;
                            int length = 0;
                            byte[] channelNoBuff = new byte[2];
                            byte[] seekBuff = new byte[4];
                            byte[] lengthBuff = new byte[2];
                            readStream.Seek(seek, SeekOrigin.Begin);
                            readStream.Read(channelNoBuff, 0, channelNoBuff.Length);
                            channelNo = (channelNoBuff[0] & 0xFF) | ((channelNoBuff[1] & 0xFF) << 8);
                            seek = seek + 2;
                            readStream.Seek(seek, SeekOrigin.Begin);
                            readStream.Read(lengthBuff, 0, lengthBuff.Length);
                            length = (lengthBuff[0] & 0xFF) | ((lengthBuff[1] & 0xFF) << 8);
                            //版本2.0
                            //length = (lengthBuff[0] & 0xFF) | ((lengthBuff[1] & 0xFF) << 8) | ((lengthBuff[2] & 0xFF) << 16) | ((lengthBuff[3] & 0xFF) << 24);
                            seek = seek + 2;
                            readStream.Seek(seek, SeekOrigin.Begin);
                            readStream.Read(seekBuff, 0, seekBuff.Length);
                            seek = (seekBuff[0] & 0xFF) | ((seekBuff[1] & 0xFF) << 8) | ((seekBuff[2] & 0xFF) << 16) | ((seekBuff[3] & 0xFF) << 24);
                            readStream.Seek(seek, SeekOrigin.Begin);
                            int value = readStream.ReadByte();
                            gradientData[intScneNo - 1][channelNo - 1] = Convert.ToByte(value);
                            seek = seek + length;
                        }
                    }
                }
                readStream.Close();
                for (int i = 0; i < 32; i++)
                {
                    fileSize = fileSize + gradientData[i].Count();
                }
                byte[] fileSizeBuff = new byte[] { Convert.ToByte(fileSize & 0xFF), Convert.ToByte((fileSize >> 8) & 0xFF), Convert.ToByte((fileSize >> 16) & 0xFF), Convert.ToByte((fileSize >> 24) & 0xFF) };
                List<byte> writeBuff = new List<byte>();
                writeBuff.AddRange(fileSizeBuff);
                for (int i = 0; i < 32; i++)
                {
                    writeBuff.AddRange(gradientData[i]);
                }
                Write(writeBuff.ToArray(), writeBuff.Count, "GradientData.bin", true, true, false);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("场景渐变数据生成报错" + ex.Message);
            //}
           
            
        }
        public static void CreateConfig(CSJ_Config config)
        {
            config.WriteToFile(ProjectDataFilePath);
        }
    }
}
