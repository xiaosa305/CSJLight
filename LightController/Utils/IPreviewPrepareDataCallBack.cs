using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Utils
{
    public interface IPreviewPrepareDataCallBack
    {
        void Completed();
        void Error();
        void UpdateProgress(string name);
    }
}
