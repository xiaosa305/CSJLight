using LightController.Ast.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Ast
{
    public class LightListOperator
    {
        public EnumOperator Operator{ get; set;}
        public int OldAddr { get; set; }
        public int NewAddr { get; set; }

    }
}
