﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    public class SceneData
    {
        public int SceneNo { get; set; }
        public int ChanelCount { get; set; }
        public IList<ChanelData> ChanelDatas { get; set; }
    }
}