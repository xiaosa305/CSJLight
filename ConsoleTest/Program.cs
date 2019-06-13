using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			IniFileAst ini = new IniFileAst(@"C:\Temp\test.ini");
			Console.WriteLine(ini.ReadInt("data","age",0));
			ini.WriteInt("data", "age", 30);




		}
	}
}
