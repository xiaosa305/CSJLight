using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Ast
{
    public class TongdaoWrapperCommon
    {
        /// <summary>
        /// 通道名称
        /// </summary>
        public string TongdaoName { get; set; }  
        
        /// <summary>
        /// 通道地址（从1开始)
        /// </summary>
        public int Address { get; set; }     

        /// <summary>
        /// 备注,主要用以显示各个子属性，在渲染时要写进去
        /// </summary>
        public string Remark { get; set; } 

        /// <summary>
        /// 此通道的初始值
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public int InitValue { get; set; }
    }
}
