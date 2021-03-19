using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBDConfigTool
{
	[Serializable]
	public class User
	{
		public string Name { get; set; }
		public int Age { get; set; }
		public string Num { get; set; }

		public User(string name, int age, string num)
		{
			Name = name;
			Age = age;
			Num = num;
		}

		public void Say()
		{
			Console.WriteLine("名字：{0}，年龄{1}，编号{2}。", Name, Age,Num);
		}
	}
}
