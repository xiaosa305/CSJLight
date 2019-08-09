using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    class DMXDataConvert
    {
        private static DMXDataConvert Instance { get; set; }
        private DMXDataConvert()
        {

        }
        public DMXDataConvert GetInstance()
        {
            if (Instance == null)
            {
                Instance = new DMXDataConvert();
            }
            return Instance;
        }

    }
}
