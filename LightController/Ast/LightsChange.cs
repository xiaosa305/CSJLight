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
        public int AddNum { get; set;  }

        /// <summary>
        /// 静态辅助方法：检查传入的changeList，是否有变动值；
        /// </summary>
        /// <param name="changeList"></param>
        /// <returns></returns>
        public static bool IsChanged(List<LightsChange> changeList) {

            if (changeList == null || changeList.Count == 0) {
                return false;
            }

            foreach (LightsChange change in changeList)
            {
                if (change.Operation != EnumOperation.NOCHANGE)
                {  //只要有一项发生了改变，就直接返回true，表示发生了改变
                    return true;                   
                }
            }
            return false;
        } 
     }
}
