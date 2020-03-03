using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MultiLedController.Utils
{
    public  class CalMD5Value
    {
        public static byte[] GetMD5Value(byte[] data)
        {
            MD5 mD5 = MD5.Create();
            byte[] md5Value = mD5.ComputeHash(data);
            mD5.Clear();
            return md5Value;
        }
    }
}
