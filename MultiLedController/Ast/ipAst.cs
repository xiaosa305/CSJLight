using MultiLedController.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;

namespace MultiLedController.Ast
{
	public class IPAst
	{
		public string[] IpArray { get; set; }
		public string[] SubmaskArray { get; set; }
		public string[] GatewayArray { get; set; }
		public string[] DnsArray { get; set; }

		/// <summary>
		/// 以ManagementObject为形参的构造函数，在此函数内做相应的处理(去除ipv6的ip)
		/// </summary>
		/// <param name="mo"></param>
		public IPAst(ManagementObject mo) {
			
			string[] ipArray = (string[])mo["IPAddress"];
			IList<string> ipList = new List<string>();
			foreach (string ip in ipArray) {
				if (IPHelper.IsIPV4(ip)) {
					ipList.Add(ip);
				}
			}
			IpArray = ipList.ToArray();

			string[] submaskArray = (string[])mo["IPSubnet"];
			IList<string> submaskList = new List<string>();
			foreach (string submask in submaskArray)
			{
				if (IPHelper.IsSubmask(submask))
				{
					submaskList.Add(submask);
				}
			}
			SubmaskArray = submaskList.ToArray();
			GatewayArray = (string[])mo["DefaultIPGateway"];
			DnsArray = (string[])mo["DNSServerSearchOrder"];
		}

	}
}
