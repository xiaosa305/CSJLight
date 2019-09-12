using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools.CSJ.IMPL
{
    public class CSJ_Project
    {
        public List<ICSJFile> CFiles { get; set; }
        public List<ICSJFile> MFiles { get; set; }
        public CSJ_Config ConfigFile { get; set; }

        public int GetProjectFileSize()
        {
            int size = 0;
            if (CFiles != null)
            {
                foreach (ICSJFile item in CFiles)
                {
                    size += item.GetData().Length;
                }
            }
            if (MFiles != null)
            {
                foreach (ICSJFile item in MFiles)
                {
                    size += item.GetData().Length;
                }
            }
            if (ConfigFile != null)
            {
                size += ConfigFile.GetData().Length;
            }
            return size;
        }
    }
}
