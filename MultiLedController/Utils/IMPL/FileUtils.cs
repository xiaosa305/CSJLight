using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace MultiLedController.Utils.IMPL
{
    public class FileUtils : IFileUtils
    {
        private static FileUtils Instance { get; set; }
        private FileUtils() { }
        public static IFileUtils GetInstance()
        {
            if (Instance == null)
            {
                Instance = new FileUtils();
            }
            return Instance;
        }
        public  void WriteToFile(List<byte> data,string filePath)
        {
            using (FileStream stream = new FileStream(filePath,FileMode.Append))
            {
                stream.Write(data.ToArray(), 0, data.Count);
                stream.Flush();
            }
        }
        public  void WriteToFileByCreate(List<byte> data, string filePath)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                stream.Write(data.ToArray(), 0, data.Count);
                stream.Flush();
            }
        }
        public  void WriteToFileBySeek(List<byte> data,string filePath, long seek)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                stream.Seek(seek, SeekOrigin.Begin);
                stream.Write(data.ToArray(), 0, data.Count);
                stream.Flush();
            }
        }
    }
}
