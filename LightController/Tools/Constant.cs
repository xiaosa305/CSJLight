using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    class Constant
    {
        public static readonly int SENCECOUNT = 24;
        public static readonly int MODE_C = 0;
        public static readonly int MODE_M = 1;
        public static readonly int MODE_GRADUAL = 1;
        public static readonly int MODE_JUMP = 0;
        public static readonly int DMX512 = 512;
        public static readonly string RECEIVE_ORDER_OK = "Ok";
        public static readonly string RECEIVE_ORDER_DONE = "Done";
        public static readonly string RECEIVE_ORDER_Send = "SendNext";
        public static readonly string RECEIVE_ORDER_ReSend = "Resend@";
    }

    public enum ORDER
    {
        Put,Get,Send,Done,Resend
    }

    public enum RECEIVE
    {
        Send,Resend,Done,Ok
    }
}
