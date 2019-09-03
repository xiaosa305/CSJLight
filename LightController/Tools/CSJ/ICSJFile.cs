using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LighEditor.Tools.CSJ
{
    public interface ICSJFile
    {
        byte[] GetData();
        void WriteToFile(string filepath);
    }
}
