using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Ast
{
    public class StepWrapperCommon
    {                
        public string LightFullName { get; set; } // 不同的灯具全名必然不同(厂商名+型号），只需存储同一个变量就好
        public int StartNum { get; set; }  
    }
}
