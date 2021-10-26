using LightController.Ast.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Ast
{
    public class LightsChange
    {
        public EnumOperation Operation { get; set; }
        public LightAst NewLightAst { get; set;}
        public int LightIndex { get; set; }
        //public int OldAddr { get; set; }
        //public int NewAddr { get; set; }
    }
}
