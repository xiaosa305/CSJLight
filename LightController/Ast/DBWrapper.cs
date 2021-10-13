﻿using LightController.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Ast
{
	public class DBWrapper
	{
        public IList<DB_Light> lightList { get; set; }
        public IList<DB_FineTune> fineTuneList { get; set; }
        public IList<DB_Channel> channelList { get; set; }        
    }
}
