using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Xiaosa.Entity
{
    public class CallBackFunction
    {
        public delegate void Completed_TakeMsg(string msg);
        public delegate void Completed_TakeMsgAndObj(Object obj, string message);
        public delegate void Completed();
        public delegate void Error(string errorMessage);
        public delegate void UpdateProgress(string name);
        public delegate void KeyPressClick(Object obj);
        public delegate void CopyListener(Object obj);
    }
}
