using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    class Constant
    {
        public const int DMX512BAUD = 250000;
        public const int SCENECOUNTMAX = 32;
        public const int STEPLISTMAX = 20;
        public const int TIMEMAXCOUNT = 0;
        public const int MODE_C = 0;
        public const int MODE_M = 1;
        public const int MODE_C_GRADUAL = 1;
        public const int MODE_C_JUMP = 0;
        public const int MODE_C_HIDDEN = 2;
        public const int MODE_M_GRADUAL = 2;
        public const int MODE_M_JUMP = 1;
        public const int MODE_M_HIDDEN = 0;
        public const int DMX512 = 512;
        public const int UDPADDR = 255;
        public const int PACKAGE_SIZE_1K = 1016;
        public const int PACKAGE_SIZE_2K = 2040;
        public const int PACKAGE_SIZE_512 = 504;
        public const int PACKAGE_SIZE_DEFAULT = 1016;
        public const int PACKAGEHEAD_SIZE = 8;
        public const int TIMEOUT = 10000;
        public const int MUSIC_CONTROL_GRADUAL = 1;
        public const int MUSIC_CONTROL_JUMP = 0;
        public const int MUSIC_CONTROL_OFF = 2;
        public const string UDP_ORDER = "UdpBroadCast";
        public const string RECEIVE_ORDER_BEGIN_OK = "Ok";
        public const string RECEIVE_ORDER_BEGIN_ERROR = "Error";
        public const string RECEIVE_ORDER_BEGIN_ERROR_DISK = "Error:DiskUnmount";
        public const string RECEIVE_ORDER_ENDSEND_OK = "Ok";
        public const string RECEIVE_ORDER_ENDSEND_ERROR = "Error";
        public const string RECEIVE_ORDER_SENDNEXT = "SendNext";
        public const string RECEIVE_ORDER_DONE = "Done";
        public const string RECEIVE_ORDER_PUT = "Ok:Decode";
        public const string RECEIVE_ORDER_PUT_PARAM = "Ok:Decode";
        public const string RECEIVE_ORDER_GET_PARAM = "Error:Get_Param";
        public const string RECEIVE_ORDER_OTHER_OK = "OK";
        public const string RECEIVE_ORDER_OTHER_ERROR = "Error";
        public const string RECEIVE_ORDER_UPDATE_OK = "Ok:Decode";
        public const string RECEIVE_ORDER_UPDATE_ERROR = "Error:UpDate";
        public const string RECEIVE_OK = "Ok";
        public const string RECEIVE_ERROR = "Error";
        public const string ORDER_PUT = "Put";
        public const string ORDER_PUT_PARAM = "PutParam";
        public const string ORDER_BEGIN_SEND = "BeginSend";
        public const string ORDER_END_SEND = "EndSend";
        public const string ORDER_GET_PARAM = "GetParam";
        public const string ORDER_SEARCH = "Search";
        public const string ORDER_UPDATE = "UpDate";

        public const string ORDER_STARTPREVIEW = "StartDebug";
        public const string ORDER_STOPPREVIEW = "StopDebug";
        public const string ORDER_SENDPREVIEWDATA = "SendPreviewData";

        public const string MARK_ORDER_TAKE_DATA = "00000101";
        public const string MARK_ORDER_NO_TAKE_DATA = "00000001";
        public const string MARK_DATA_NO_END = "00000110";
        public const string MARK_DATA_END = "00000010";
        public const int SCENE_BOOING = 23;
        public const int SCENE_ACCLAIM = 22;
        public const int SCENE_SHAKE_MIC = 21;
        public const int SCENE_ALL_ON_AND_OFF = 20;
        public const int SCENE_ALL_ON = 19;
        public const int SCENE_ALL_OFF = 18;
        public const int SCENE_PAUSE = 17;
        public const int SCENE_ALL_ON_NO = 30;
        public const int SCENE_ALL_OFF_NO = 29;
    }

    public enum ORDER
    {
        Put,Test
    }
    public delegate void DownloadProgressDelegate(string filename, int Progress);
    public delegate void GetParamDelegate(CSJ_Hardware hardware);
}
