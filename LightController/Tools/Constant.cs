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
        public static readonly string RECEIVE_ORDER_OK = "OK";
        public static readonly string RECEIVE_ORDER_DONE = "done";
        public static readonly string RECEIVE_ORDER_Send = "send@";
        public static readonly string RECEIVE_ORDER_ReSend = "resend@";
    }

    public enum ORDER
    {
        Put,Get,Send,Done,Resend
    }

    public enum RECEIVE
    {
        Send,Resend,Done
    }
}
