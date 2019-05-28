using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLAst.Test
{
	class BitCalc 
	{
		public static void Main()
		{
			//swap1();

			//Console.WriteLine( 17%16 );
			//Console.WriteLine( mod1(17,16) );
			//Console.WriteLine( mod1(15,8) == (15 % 8) );

			int jdz_x = 20;
			Console.WriteLine(juedui(jdz_x));
			Console.WriteLine( - jdz_x );

		}

		static void swap1()
		{
			int x = 10;
			int y = 20;
			x ^= y;
			y ^= x;
			x ^= y;
			Console.WriteLine(x);
			Console.WriteLine(y);
		}
		
		// y只为2的倍数时有效
		static int mod1(int x,int y)
		{
			return x & (y - 1);
		}

		// 求绝对值
		static int juedui(int x) {

			return (~x + 1 );
		}

	}
}
