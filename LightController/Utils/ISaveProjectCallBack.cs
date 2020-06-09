using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Utils
{
    public interface ISaveProjectCallBack
    {
        void Completed();
        void Error(string message);
        void UpdateProgress(string name);
    }
}
