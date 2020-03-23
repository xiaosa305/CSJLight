using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MultiLedController.Utils.IMPL
{
    public class CalMD5Value : ICalMD5Value
    {
        private static CalMD5Value Instance { get; set; }
        private CalMD5Value()
        {

        }
        public static ICalMD5Value GetInstance()
        {
            if (Instance == null)
            {
                Instance = new CalMD5Value();
            }
            return Instance;
        }
        public string GetMD5Value(byte[] data)
        {
            MD5 mD5 = MD5.Create();
            byte[] md5Value = mD5.ComputeHash(data);
            mD5.Clear();
            return this.BytesToString(md5Value);
        }

        /// <summary>
        /// 将得到的 MD5 字节数组转成 字符串
        /// </summary>
        /// <param name="md5Bytes"></param>
        /// <returns></returns>
        private string BytesToString(byte[] md5Bytes)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < md5Bytes.Length; i++)
            {
                sb.Append(md5Bytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
