using LightController.Tools.CSJ.IMPL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Utils
{
    public interface ICommunicatorCallBack
    {
        void Completed(string deviceTag);
        void Error(string deviceTag, string errorMessage);
        void UpdateProgress(string deviceTag, string fileName, int newProgress);
        void GetParam(CSJ_Hardware hardware);
    }
}
