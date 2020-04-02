using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.utils
{
    public interface IFileUtils
    {
        /// <summary>
        /// 功能：写数据到文件末尾
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filePath"></param>
        void WriteToFile(List<byte> data, string filePath);
        /// <summary>
        /// 功能：新建文件并写数据到文件
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filePath"></param>
        void WriteToFileByCreate(List<byte> data, string filePath);
        /// <summary>
        /// 功能：从指定位置开始写数据到文件并覆盖
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filePath"></param>
        /// <param name="seek"></param>
        void WriteToFileBySeek(List<byte> data, string filePath, long seek);
    }
}
