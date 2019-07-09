using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    public interface IReceiveCallBack
    {
        void SendCompleted();
        void SendError();
        void Resend();
    }
}
