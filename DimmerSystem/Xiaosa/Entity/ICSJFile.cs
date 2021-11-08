using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DimmerSystem.Xiaosa.Entity
{
    public interface ICSJFile
    {
        byte[] GetData();
        void WriteToFile(string filepath);
    }
}
