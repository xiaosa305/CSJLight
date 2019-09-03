using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;

namespace LightController.Ast
{
	class JustTest
	{
		public static void Main(string[] args) {

			Console.WriteLine(Dns.GetHostName());

			foreach (IPAddress iPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
			{			
				Console.WriteLine(iPAddress.ToString());				
			}

		}


	}
}
