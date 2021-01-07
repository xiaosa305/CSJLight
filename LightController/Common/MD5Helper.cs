using System.Text;
using System.Security.Cryptography;

namespace LightController.Common
{
	class MD5Helper
	{
		public static string MD5_UTF8(string inputString) {

			MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			byte[] encryptedBytes = md5.ComputeHash(Encoding.UTF8.GetBytes( inputString ));
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < encryptedBytes.Length; i++)
			{
				sb.AppendFormat("{0:x2}", encryptedBytes[i]);
			}
			return sb.ToString();
		}		

	}
}
