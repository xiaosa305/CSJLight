using MultiLedController.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace MultiLedController.Common
{
	public class IPHelper
	{
		/// <summary>
		/// 分解字符串以判断是否ipv4
		/// </summary>
		/// <param name="ip"></param>
		/// <returns></returns>
		public static bool IsIPV4(string ip)
		{

			int num;
			//分割成四段
			string[] ip_4 = ip.Split('.');
			if (ip_4.Length != 4) return false;

			for (int i = 0; i < 4; i++)
			{
				if (!int.TryParse(ip_4[i], out num)) return false;
				if (num < 0 && num > 255) return false;
			}
			//全部检查完毕 无错误 
			return true;
		}

		/// <summary>
		/// 用正则表达式来判断是否ipv4
		/// </summary>
		/// <param name="ip"></param>
		/// <returns></returns>
		public static bool IsIPV4Regex(string ip)
		{
			return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
		}

		/// <summary>
		/// 判断传入字符串是否子网掩码
		/// </summary>
		/// <param name="mask"></param>
		/// <returns></returns>
		public static bool IsSubmask(string mask)
		{
			//11111 0000  是否分割 标志位
			bool zero = false;
			//分割成四段
			string[] ip_4 = mask.Split('.');
			if (ip_4.Length != 4) return false;
			for (int i = 0; i < 4; i++)
			{
				//如果是255
				if (ip_4[i].Equals("255"))
				{
					if (zero) return false;
				}
				//其它网段 前面全为1 后面全为0
				else if (ip_4[i].Equals("128") ||
						 ip_4[i].Equals("192") || ip_4[i].Equals("224") ||
						 ip_4[i].Equals("240") || ip_4[i].Equals("248") ||
						 ip_4[i].Equals("252") || ip_4[i].Equals("254"))
				{
					//标志位为1 后面应该全为0 退出 掩码格式错误
					if (zero) return false;
					zero = true;
				}
				//全是0
				else if (ip_4[i].Equals("0") || ip_4[i].Equals("00") || ip_4[i].Equals("000"))
				{
					//还没有分割 则标志位为1 分割 1 0
					if (!zero) zero = true;
				}
				//其它数字或者字符
				else return false;
			}
			//全部检查完毕 无错误 
			return true;
		}

		/// <summary>
		/// 将子网掩码字符串形式转换为位数
		/// </summary>
		/// <param name="submask"></param>
		/// <returns></returns>
		public static int CalcSubmask(string submask)
		{

			int totalBits = 0;
			foreach (string octet in submask.Split('.'))
			{
				byte octetByte = byte.Parse(octet);
				while (octetByte != 0)
				{
					totalBits += octetByte & 1;     // logical AND on the LSB
					octetByte >>= 1;            // do a bitwise shift to the right to create a new LSB
				}
			}
			return totalBits;
		}

		/// <summary>
		/// 静态辅助方法：通过ipAst来设置本地ip
		/// </summary>
		/// <param name="ipAst"></param>
		public static bool SetIPAddress(ManagementObject mo, IPAst ipAst)
		{
			return SetIPAddress(mo, ipAst.IpArray, ipAst.SubmaskArray, ipAst.GatewayArray, ipAst.DnsArray);
		}

		/// <summary>
		/// 静态辅助方法： 设置IP地址，掩码，网关和DNS
		/// </summary>
		/// <returns>设置成功true；设置失败则恢复原始设置（mo中取）并设为false</returns>
		/// <param name="ipArray">ip列表：要设置的ip列表</param>
		/// <param name="submaskArray">子网掩码列表：有多少个ip，就要对应多少个子网掩码</param>
		/// <param name="gateway">网关列表：与ip独立，只需设置一次即可</param>
		/// <param name="dns">dns列表：有多少个DNS，就设置几个</param>
		public static bool SetIPAddress(ManagementObject mo, string[] ipArray, string[] submaskArray, string[] gatewayArray, string[] dnsArray)
		{
			ManagementClass wmi = new ManagementClass("Win32_NetworkAdapterConfiguration");
			ManagementObjectCollection moc = wmi.GetInstances();
			ManagementBaseObject inPar = null;
			ManagementBaseObject outPar = null;
			IPAst tempIPAst = new IPAst(mo);

			//设置IP地址和掩码
			if (ipArray != null && submaskArray != null)
			{
				inPar = mo.GetMethodParameters("EnableStatic");
				inPar["IPAddress"] = ipArray;
				inPar["SubnetMask"] = submaskArray;
				outPar = mo.InvokeMethod("EnableStatic", inPar, null);
			}

			//设置网关地址
			if (gatewayArray != null)
			{
				inPar = mo.GetMethodParameters("SetGateways");
				inPar["DefaultIPGateway"] = gatewayArray;
				outPar = mo.InvokeMethod("SetGateways", inPar, null);
			}

			//设置DNS地址
			if (dnsArray != null)
			{
				inPar = mo.GetMethodParameters("SetDNSServerSearchOrder");
				inPar["DNSServerSearchOrder"] = dnsArray;
				outPar = mo.InvokeMethod("SetDNSServerSearchOrder", inPar, null);
			}

			int i = 0;
			// 在这里验证是否设置成功，设置成功才跳出循环
			while (true)
			{
				if (checkSetIPListSuccess(ipArray))
				{
					break;
				}
				Thread.Sleep(100);
				i++;
				if (i >= 200)
				{
					//恢复之前的设置
					inPar = mo.GetMethodParameters("EnableStatic");
					inPar["IPAddress"] = tempIPAst.IpArray;
					inPar["SubnetMask"] = tempIPAst.SubmaskArray;
					mo.InvokeMethod("EnableStatic", inPar, null);
					//Console.WriteLine("超过20S仍未设置成功，已恢复初始设置");
					return false;
				}
			}
			return true;
		}	

		/// <summary>
		/// 辅助方法：检查是否已经成功设置好指定IP列表（看newIPArray是否是系统当前已有的ip列表的子集）
		/// </summary>
		/// <param name="newIPList"></param>
		/// <returns></returns>
		public static bool checkSetIPListSuccess(string[] newIPArray)
		{
			IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
			IList<string> curIPList = new List<string>();
			if (ipe.AddressList != null && ipe.AddressList.Length > 0)
			{
				foreach (IPAddress ip in ipe.AddressList)
				{
					if (ip.AddressFamily == AddressFamily.InterNetwork) //当前ip为ipv4时，才加入到列表中
					{
						curIPList.Add(ip.ToString());
					}
				}
				foreach (string tempIp in newIPArray)
				{
					if (!curIPList.Contains(tempIp))
					{
						return false;
					}
				}
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 辅助方法：由网卡名，获取当前网卡的ManagementObject
		/// </summary>
		/// <param name="carName"></param>
		/// <returns></returns>
		public static ManagementObject GetNetCardMO(string carName)
		{
			ManagementObjectSearcher search = new ManagementObjectSearcher("SELECT * FROM Win32_NetWorkAdapterConfiguration WHERE Description = '" + carName + "'");
			foreach (ManagementObject mo in search.Get())
			{
				if (mo["IPAddress"] != null)
				{
					return mo;
				}
			}
			return null;
		}

		[DllImport("Iphlpapi.dll")]
		private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
		[DllImport("Ws2_32.dll")]
		private static extern Int32 inet_addr(string ip);

		/// <summary>
		/// 获取远程主机MAC
		/// </summary>
		/// <param name="localIP"></param>
		/// <param name="remoteIP"></param>
		/// <returns></returns>
		public static string GetRemoteMac(string localIP, string remoteIP)
		{
			Int32 ldest = inet_addr(remoteIP); //目的ip
			Int32 lhost = inet_addr(localIP); //本地ip

			try
			{
				Int64 macinfo = new Int64();
				Int32 len = 6;
				int res = SendARP(ldest, 0, ref macinfo, ref len);
				return Convert.ToString(macinfo, 16);
			}
			catch (Exception err)
			{
				Console.WriteLine("Error:{0}", err.Message);
				return null;
			}
		}

		/// <summary>
		/// 监测某个IP是否可用（即能设为本地的IP）
		/// </summary>
		/// <param name="localIP"></param>
		/// <param name="remoteIP"></param>
		/// <returns></returns>
		public static bool CheckIPAvailable(string localIP, string remoteIP)
		{
			// 返回值：true为可用（未被占用），false则表示不可用（已被占用）；默认情况下为true，只有ping通或arp通了才会改成false
			bool result = true;
			try
			{
				Ping ping = new Ping();
				IPStatus ipStatus = ping.Send(remoteIP).Status;
				//若能ping通，则返回false
				if (ipStatus == IPStatus.Success)
				{
					result = false;
				}
			}
			//若捕获到异常，则说明还是ping不通,不去改动result的值（默认为true）
			catch (Exception ex)
			{
				Console.WriteLine("ping" + remoteIP + "时出现异常\n" + ex.Message);
			}

			//若ping不通或出现异常（此时result仍为true,即值未被tryCatch内改动过），则检查arp
			if (result)
			{
				string remoteMac = GetRemoteMac(localIP, remoteIP);
				// ARP获取该IP的Mac，若返回值不为空或“0”，则返回false，表示这个IP已被占用不可用
				if (remoteMac != null && !remoteMac.Equals("0"))
				{
					result = false;
				}
			}
			return result;
		}

		/// <summary>
		/// 监测某个IP是否可用（即能设为本地的IP）
		/// </summary>
		/// <param name="localIP"></param>
		/// <param name="remoteIP"></param>
		/// <returns></returns>
		public static bool CheckIPAvailableARPOnly(string localIP, string remoteIP)
		{
			string remoteMac = GetRemoteMac(localIP, remoteIP);
			// ARP获取该IP的Mac，若返回值不为空或“0”，则返回false，表示这个IP已被占用不可用
			if (remoteMac != null && !remoteMac.Equals("0"))
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}
