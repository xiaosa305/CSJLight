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

        /// <summary>
        /// 文件写入
        /// </summary>
        /// <param name="datas">写入数据</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="isCreate">是否建立新文件</param>
        public static bool Write(byte[] datas, string fileName, bool isCreate, bool isCache)
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
                    stream.Write(datas, 0, datas.Length);
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

        /// <summary>
        /// 文件写入
        /// </summary>
        /// <param name="data">写入一个BYTE数据</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="isCreate">是否建立新文件</param>
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

        /// <summary>
        ///  文件写入
        /// </summary>
        /// <param name="datas">写入数据</param>
        /// <param name="seek">写入位置</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="isCreate">是否建立新文件</param>
        public static void Write(byte[] datas, long seek, string fileName, bool isCreate)
        {
            InitFilePath();
            try
            {
                string filePath = DataCacheFilePath + @"/" + fileName;
                FileStream stream;
                using (isCreate ? stream = new FileStream(filePath, FileMode.Create) : stream = new FileStream(filePath, FileMode.Append))
                {
                    stream.Seek(seek, SeekOrigin.Begin);
                    stream.Write(datas, 0, datas.Length);
                    stream.Flush();
                }
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
