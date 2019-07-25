using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    class Constant
    {
        public static readonly int SCENECOUNT = 24;
        public static readonly int MODE_C = 0;
        public static readonly int MODE_M = 1;
        public static readonly int MODE_GRADUAL = 1;
        public static readonly int MODE_JUMP = 0;
        public static readonly int DMX512 = 512;

        public const string RECEIVE_ORDER_BEGIN_OK = "Ok";//
        public const string RECEIVE_ORDER_BEGIN_ERROR = "Error";//Error:BeginSend
        public const string RECEIVE_ORDER_BEGIN_ERROR_DISK = "Error:DiskUnmount";

        public const string RECEIVE_ORDER_END_OK = "Ok";
        public const string RECEIVE_ORDER_END_ERROR = "Error";

        public const string RECEIVE_ORDER_SENDNEXT = "SendNext";
        public const string RECEIVE_ORDER_DONE = "Done";
        public const string RECEIVE_ORDER_PUT = "Ok:Decode";
        public const string RECEIVE_ORDER_PUT_PARA = "Ok:Decode";

        public const string RECEIVE_ORDER_OTHER_OK = "OK";
        public const string RECEIVE_ORDER_OTHER_ERROR = "Error";

        public const string ORDER_PUT = "Put";
        public const string ORDER_PUT_PARA = "PutParam";
        public const string ORDER_BEGIN_SEND = "BeginSend";
        public const string ORDER_END_SEND = "EndSend";

        public const string RECEIVE_OK = "Ok";
        public const string RECEIVE_ERROR = "Error";

        public const string UDP_ORDER = "UdpBroadCast";

        public static readonly string MARK_ORDER_TAKE_DATA = "00000101";
        public static readonly string MARK_ORDER_NO_TAKE_DATA = "00000001";
        public static readonly string MARK_DATA_NO_END = "00000110";
        public static readonly string MARK_DATA_END = "00000010";
    }

    public enum ORDER
    {
        Put,Test
    }
}
