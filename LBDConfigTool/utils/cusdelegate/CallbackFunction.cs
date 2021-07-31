using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBDConfigTool.utils.cusdelegate
{
    public class CallbackFunction
    {
        public delegate void CompletedByObjAndMsg(Object obj, string msg);
        public delegate void ErrorByMsg(string errorMsg);
        public delegate void TaskProgress(int progress);
    }
}
