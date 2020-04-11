using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace MultiLedController
{
	public partial class TestForm : Form
	{
		public TestForm()
		{
			InitializeComponent();
		}

		private void pingButton_Click(object sender, EventArgs e)
		{
			DateTime beforeDT = System.DateTime.Now;

			Ping ping = new Ping();
			IList<string> enabledIPList = new List<string>();			
			for (int lastStr =14; lastStr <=99; lastStr ++)
			{
				string currentIP = "192.168.31." + lastStr;
				PingReply pr = ping.Send(currentIP);
				IPStatus ipStatus = pr.Status;			
				if (ipStatus != IPStatus.Success) {
					enabledIPList.Add(currentIP);
				}
				if (enabledIPList.Count >= 6) {
					break;
				}
			}

			Console.WriteLine(enabledIPList);

			DateTime afterDT = System.DateTime.Now;
			TimeSpan ts = afterDT.Subtract(beforeDT);

			MessageBox.Show("找到可用ip列表，共耗时" + ts.TotalSeconds.ToString("#0.00") + " s");
		}



		public string GetLocalIP()
		{
			IPAddress localIp = null;
			try
			{
				IPAddress[] ipArray;
				ipArray = Dns.GetHostAddresses(Dns.GetHostName());
				localIp = ipArray.First(ip => ip.AddressFamily == AddressFamily.InterNetwork);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.StackTrace + "\r\n" + ex.Message, "错误", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
				//Log.WriteLog(ex);
			}
			if (localIp == null)
			{
				localIp = IPAddress.Parse("127.0.0.1");
			}
			return localIp.ToString();
		}

	}
}
