using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.utils
{
    public interface ICalMD5Value
    {
        string GetMD5Value(byte[] data);
    }
}
