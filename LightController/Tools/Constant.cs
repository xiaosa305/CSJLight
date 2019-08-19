using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    class Constant
    {
        public const int SCENECOUNT = 24;
        public const int MODE_C = 0;
        public const int MODE_M = 1;
        public const int MODE_GRADUAL = 1;
        public const int MODE_JUMP = 0;
        public const int DMX512 = 512;
        public const int UDPADDR = 255;
        public const int PACKAGE_SIZE_1K = 1016;
        public const int PACKAGE_SIZE_2K = 2040;
        public const int PACKAGE_SIZE_512 = 508;
        public const int PACKAGE_SIZE_DEFAULT = 1016;
        public const int PACKAGEHEAD_SIZE = 8;
        public const int TIMEOUT = 9999999;
        public const int HIDDEN = 2;
        public const int MUSIC_CONTROL_ON = 1;
        public const int MUSIC_CONTROL_OFF = 0;
        public const string UDP_ORDER = "UdpBroadCast";
        public const string RECEIVE_ORDER_BEGIN_OK = "Ok";
        public const string RECEIVE_ORDER_BEGIN_ERROR = "Error";
        public const string RECEIVE_ORDER_BEGIN_ERROR_DISK = "Error:DiskUnmount";
        public const string RECEIVE_ORDER_ENDSEND_OK = "Ok";
        public const string RECEIVE_ORDER_ENDSEND_ERROR = "Error";
        public const string RECEIVE_ORDER_SENDNEXT = "SendNext";
        public const string RECEIVE_ORDER_DONE = "Done";
        public const string RECEIVE_ORDER_PUT = "Ok:Decode";
        public const string RECEIVE_ORDER_PUT_PARA = "Ok:Decode";
        public const string RECEIVE_ORDER_OTHER_OK = "OK";
        public const string RECEIVE_ORDER_OTHER_ERROR = "Error";
        public const string RECEIVE_OK = "Ok";
        public const string RECEIVE_ERROR = "Error";
        public const string ORDER_PUT = "Put";
        public const string ORDER_PUT_PARAM = "PutParam";
        public const string ORDER_BEGIN_SEND = "BeginSend";
        public const string ORDER_END_SEND = "EndSend";
        public const string ORDER_GET_PARAM = "GetParam";
        public const string MARK_ORDER_TAKE_DATA = "00000101";
        public const string MARK_ORDER_NO_TAKE_DATA = "00000001";
        public const string MARK_DATA_NO_END = "00000110";
        public const string MARK_DATA_END = "00000010";

    }

    public enum ORDER
    {
        Put,Test
    }
    public delegate void DownloadProgressDelegate(string filename, int Progress);
    public delegate void GetParamDelegate(CSJ_Hardware hardware);
}
