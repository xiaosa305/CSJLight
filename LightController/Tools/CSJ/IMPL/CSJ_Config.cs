using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools.CSJ.IMPL
{
    public class CSJ_Config
    {
        public int FileSize { get; set; }
        public int Light_Total_Count { get; set; }
        public int DMX512_Chanel_Count { get; set; }
        public int Default_Scene_Number { get; set; }
        public List<int> Music_Control_Enable { get; set; }
        public int Scene_Change_Mode { get; set; }
        public int TimeFactory { get; set; }

    }
}
