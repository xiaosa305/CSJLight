using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Common
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Net;
	using System.IO;

	public class HttpHelper
		{

			public static string Get(string Url)
			{
				//System.GC.Collect();
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
				request.Proxy = null;
				request.KeepAlive = false;
				request.Method = "GET";
				request.ContentType = "application/json; charset=UTF-8";
				request.AutomaticDecompression = DecompressionMethods.GZip;

				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				Stream myResponseStream = response.GetResponseStream();
				StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
				string retString = myStreamReader.ReadToEnd();

				myStreamReader.Close();
				myResponseStream.Close();

				if (response != null)
				{
					response.Close();
				}
				if (request != null)
				{
					request.Abort();
				}

				return retString;
			}

			public static string Post(string Url, string Data, string Referer)
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
				request.Method = "POST";
				request.Referer = Referer;
				byte[] bytes = Encoding.UTF8.GetBytes(Data);
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = bytes.Length;
				Stream myResponseStream = request.GetRequestStream();
				myResponseStream.Write(bytes, 0, bytes.Length);

				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
				string retString = myStreamReader.ReadToEnd();

				myStreamReader.Close();
				myResponseStream.Close();

				if (response != null)
				{
					response.Close();
				}
				if (request != null)
				{
					request.Abort();
				}
				return retString;
			}
		
			public static string PostUrlWithDict(string url, Dictionary<string,string> paramDict)
			{
				string result = "";
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
				req.Method = "POST";
				req.ContentType = "application/x-www-form-urlencoded";
				#region 添加Post 参数
				StringBuilder builder = new StringBuilder();
				int i = 0;
				foreach (var item in paramDict)
				{
					if (i > 0)
						builder.Append("&");
					builder.AppendFormat("{0}={1}", item.Key, item.Value);
					i++;
				}
				byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
				req.ContentLength = data.Length;
				using (Stream reqStream = req.GetRequestStream())
				{
					reqStream.Write(data, 0, data.Length);
					reqStream.Close();
				}
				#endregion
				HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
				Stream stream = resp.GetResponseStream();
				//获取响应内容
				using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
				{
					result = reader.ReadToEnd();
				}
				return result;

		}

	}
	
}
