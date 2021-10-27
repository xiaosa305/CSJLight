using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Ast
{
    public class TongdaoWrapperCommon
    {
        public string TongdaoName { get; set; }  //通道名称
        public int Address { get; set; }     // 通道地址
        public string Remark { get; set; } //备注,主要用以显示各个子属性，在渲染时要写进去
    }
}
