using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    public interface IReceiveCallBack
    {
        void SendCompleted(string ip);
        void SendError(string ip,string order);
    }
}
