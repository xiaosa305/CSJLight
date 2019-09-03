﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LighEditor.Tools
{
    public interface IReceiveCallBack
    {
        void SendCompleted(string ip,string order);
        void SendError(string ip,string order);
    }
}
