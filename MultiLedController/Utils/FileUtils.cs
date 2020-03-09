using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace MultiLedController.Utils
{
    public class FileUtils
    {
        //private static string ProjectDownloadDir = Application.StartupPath + @"\DataCache\Download\CSJ";
        private static string ProjectDownloadDir = @"C:\WorkSpace\Save";

        public static void WriteToFile(List<byte> data,string fileName)
        {
            string filePath = ProjectDownloadDir + @"\" + fileName;
            using (FileStream stream = new FileStream(filePath,FileMode.Append))
            {
                stream.Write(data.ToArray(), 0, data.Count);
                stream.Flush();
            }
        }

        public static void WriteToFileByCreate(List<byte> data, string fileName)
        {
            string filePath = ProjectDownloadDir + @"\" + fileName;
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                stream.Write(data.ToArray(), 0, data.Count);
                stream.Flush();
            }
        }

        public static void WriteToFileBySeek(List<byte> data,string fileName,long seek)
        {
            string filePath = ProjectDownloadDir + @"\" + fileName;
            using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                stream.Seek(seek, SeekOrigin.Begin);
                stream.Write(data.ToArray(), 0, data.Count);
                stream.Flush();
            }
        }

        public static void SetSaveDirPath(string dirPath)
        {
            ProjectDownloadDir = dirPath;
        }
    }
}
