using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.Tools
{
    class Constant
    {
        public static int[] DMX512ChannelNos;
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
        public const int TIMEOUT = 5000;
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
        public const string RECEIVE_ORDER_ACK = "ack\r\n";
        public const string RECEIVE_ORDER_DONE = "Done";
        public const string RECEIVE_ORDER_PUT = "Ok:Decode";
        public const string RECEIVE_ORDER_PUT_PARAM = "Ok:Decode";
        public const string RECEIVE_ORDER_GET_PARAM = "Error:Get_Param";
        public const string RECEIVE_ORDER_OTHER_OK = "OK";
        public const string RECEIVE_ORDER_OTHER_ERROR = "Error";
        public const string RECEIVE_ORDER_UPDATE_OK = "Ok:Decode";
        public const string RECEIVE_ORDER_UPDATE_ERROR = "Error:UpDate";
        public const string RECEIVE_ORDER_START_DEBUG_OK = "Ok:StartDebug";
        public const string RECEIVE_ORDER_START_DEBUG_ERROR = "Error:StartDebug";
        public const string RECEIVE_ORDER_END_DEBUG_OK = "Ok:EndDebug";
        public const string RECEIVE_ORDER_END_DEBUG_ERROR = "Error:EndDebug";
        public const string RECEIVE_OK = "Ok";
        public const string RECEIVE_ERROR = "Error";
        public const string ORDER_PUT = "Put";
        public const string ORDER_PUT_PARAM = "PutParam";
        public const string ORDER_BEGIN_SEND = "BeginSend";
        public const string ORDER_END_SEND = "EndSend";
        public const string ORDER_GET_PARAM = "GetParam";
        public const string ORDER_SEARCH = "Search";
        public const string ORDER_UPDATE = "UpDate";
        public const string ORDER_START_DEBUG = "StartDebug";
        public const string ORDER_END_DEBUG = "EndDebug";
        public const string ORDER_SEND_DEBUG_DATA = "SendPreviewData";
        public const string MARK_ORDER_TAKE_DATA = "00000101";
        public const string MARK_ORDER_NO_TAKE_DATA = "00000001";
        public const string MARK_DATA_NO_END = "00000110";
        public const string MARK_DATA_END = "00000010";

        public const string NEW_DEVICE_LIGHTCONTROL = "Relay";
        public const string NEW_DEVICE_CENTRALCONTROL = "CenterControl";
        public const string NEW_DEVICE_PASSTHROUGH = "PassThrough";

        public const string OLD_DEVICE_LIGHTCONTROL_READ = "rg";
        public const string OLD_DEVICE_LIGHTCONTROL_DOWNLOAD = "dg";
        public const string OLD_DEVICE_LIGHTCONTROL_CONNECT = "zg";
        public const string OLD_DEVICE_LIGHTCONTROL_DEBUG = "yg";

        public const string OLD_DEVICE_CENTRALCONTROL_CONNECT = "lk";
        public const string OLD_DEVICE_CENTRALCONTROL_DOWNLOAD = "dk";
        public const string OLD_DEVICE_CENTRALCONTROL_START_STUDY = "cp";
        public const string OLD_DEVICE_CENTRALCONTROL_STOP_STUDY = "xp";

        public const string OLD_DEVICE_KEYPRESS_CONNECT = "zc";
        public const string OLD_DEVICE_KEYPRESS_READ = "rc";
        public const string OLD_DEVICE_KEYPRESS_DOWNLOAD = "dc";

        public const int SCENE_BOOING = 23;
        public const int SCENE_ACCLAIM = 22;
        public const int SCENE_SHAKE_MIC = 21;
        public const int SCENE_ALL_ON_AND_OFF = 20;
        public const int SCENE_ALL_ON = 19;
        public const int SCENE_ALL_OFF = 18;
        public const int SCENE_PAUSE = 17;
        public const int SCENE_ALL_ON_NO = 30;
        public const int SCENE_ALL_OFF_NO = 29;

        public static int GetNumber(int number)
        {
            if (DMX512ChannelNos == null)
            {
                DMX512ChannelNos = new int[1001];
                for (int i = 0; i < 1001; i++)
                {
                    DMX512ChannelNos[i] = i;
                }
            }
            return DMX512ChannelNos[number];
        }

    }

    public enum ORDER
    {
        Put,Test
    }
    public delegate void DownloadProgressDelegate(string filename, int Progress);
    public delegate void GetParamDelegate(CSJ_Hardware hardware);
}
