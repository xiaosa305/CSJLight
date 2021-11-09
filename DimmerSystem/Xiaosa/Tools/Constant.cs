using LightController.Tools.CSJ.IMPL;

namespace DimmerSystem.Xiaosa.Tools
{
    class Constant
    {
        public const string RECEIVE_ORDER_BEGIN_OK = "Ok";
        public const string RECEIVE_ORDER_BEGIN_ERROR = "Error";
        public const string RECEIVE_ORDER_BEGIN_ERROR_DISK_Old = "Error:DiskUnmount";
        public const string RECEIVE_ORDER_BEGIN_ERROR_DISK = "DiskUnmount";
        public const string RECEIVE_ORDER_ENDSEND_OK = "Ok";
        public const string RECEIVE_ORDER_ENDSEND_ERROR = "Error";
        public const string RECEIVE_ORDER_SENDNEXT = "SendNext";
        public const string RECEIVE_ORDER_ACK = "ack\r\n";
        public const string RECEIVE_ORDER_DONE = "Done";
        public const string RECEIVE_ORDER_PUT = "Ok:Decode";
        public const string RECEIVE_ORDER_PUT_PARAM = "Ok:Decode";
        public const string RECEIVE_ORDER_GET_PARAM = "Error:Get_Param";
        public const string RECEIVE_ORDER_GET_FIRMWARE_VERSION = "Error:FirmwareVersion";
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
        public const string UDP_ORDER = "UdpBroadCast";
        public const string ORDER_PUT = "Put";
        public const string ORDER_PUT_PARAM = "PutParam";
        public const string ORDER_BEGIN_SEND = "BeginSend";
        public const string ORDER_END_SEND = "EndSend";
        public const string ORDER_GET_PARAM = "GetParam";
        public const string ORDER_OPEN_SCENE = "OpenScene";
        public const string ORDER_CLOSE_SCENE = "CloseScene";
        public const string ORDER_SEARCH = "Search";
        public const string ORDER_UPDATE = "UpDate";
        public const string ORDER_START_DEBUG = "StartDebug";
        public const string ORDER_END_DEBUG = "EndDebug";
        public const string ORDER_SEND_DEBUG_DATA = "SendPreviewData";
        public const string ORDER_GET_FIRMWARE_VERSION = "FirmwareVersion";
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
    }
}
