using CCWin.SkinControl;
using LightController.Ast.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LightController.Common
{
	public class LanguageHelper
	{
		private static string language = "zh-CN";
		private static Dictionary<string, string> wordDict;
		private static Dictionary<string, string> sentenceDict;  // 先试着把相关的翻译数据从硬盘中读出来，每次查到新的词，则需要追加到json中；并保存到文件中（实时保存否则文件可能出错）
		private static string sentenceJsonPath; 
		private static int addCount = 0;
		private static string url = "https://api.fanyi.baidu.com/api/trans/vip/translate";
		private static string appid = "20201230000659237"; //荣华
		private static string salt = "2650224"; // 自定义的盐	
		private static string key = "6nwB5RbeKVMQSJ5qboue"; //荣华

		/// <summary>
		///  设置语言，并初始化两个Dictionary
		/// </summary>
		public static void SetLanguage(string lang) {

			if (lang == "zh-CN") {
				return ;
			}

			language = lang;
			wordDict = new Dictionary<string, string>();
			sentenceDict = new Dictionary<string, string>();			
			
			string jsonPath = @"Language\" + language + ".json";
			if (File.Exists(jsonPath)) {
				var content = File.ReadAllText(jsonPath, Encoding.UTF8);
				if (!string.IsNullOrEmpty(content))
				{
					wordDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);					
				}
			}

			sentenceJsonPath = @"Language\sentence_" + language + ".json";
			if (File.Exists(sentenceJsonPath))
			{
				var content = File.ReadAllText(sentenceJsonPath, Encoding.UTF8);
				if (!string.IsNullOrEmpty(content))
				{
					sentenceDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
				}
			}
		}

		// 初始化语言
		public static void InitForm(Form form)
		{
			if (language == "zh-CN")
			{
				return;
			}

			if (wordDict != null && wordDict.Count > 0) {
				TranslateControl(form);
			}
		}

		// 翻译常规Control
		public static void TranslateControl( Control ctrl)
		{
			if (language == "zh-CN" || ctrl == null)
			{
				return;
			}

			if (ctrl.Tag == null && wordDict.ContainsKey(ctrl.Text) )
			{
				ctrl.Text = wordDict[ctrl.Text];
			}

			foreach (Control item in ctrl.Controls)
			{				
				if (item is MenuStrip)
				{						
					foreach (ToolStripMenuItem menuItem in (item as MenuStrip).Items)
					{
						TranslateMenuItem(menuItem);
					}
				}
				else {
					TranslateControl(item);
				}
			}			
		}

		/// <summary>
		/// 辅助方法：翻译listView的列标题
		/// </summary>
		/// <param name="listView"></param>
		public static void InitListView(ListView listView)
		{
			if (language == "zh-CN")
			{
				return;
			}

			foreach (ColumnHeader col in listView.Columns)
			{
				if (col.Tag == null && wordDict.ContainsKey(col.Text))
				{
					col.Text = wordDict[col.Text];
				}
			}
		}

		/// <summary>
		///  翻译菜单
		/// </summary>
		/// <param name="ms"></param>		
		public static void TranslateMenuStrip( ContextMenuStrip ms ) {

			if (language == "zh-CN")
			{
				return;
			}

			if (ms.Tag == null && wordDict.ContainsKey(ms.Text))
			{
				ms.Text = wordDict[ms.Text];
			}

			foreach (var item in ms.Items )
			{
				if (item is ToolStripMenuItem) {
					ToolStripMenuItem mi = item as ToolStripMenuItem;
					TranslateMenuItem(mi);					
				}						
			}
		}
	
		/// <summary>
		/// 翻译菜单项
		/// </summary>
		/// <param name="mi"></param>

		public static void TranslateMenuItem(ToolStripMenuItem mi) {

			if (language == "zh-CN")
			{
				return;
			}

			if (mi.Tag == null && wordDict.ContainsKey(mi.Text))
			{
				mi.Text = wordDict[mi.Text];
			}

			if (mi.DropDownItems.Count > 0)
			{
				foreach (var item in mi.DropDownItems)
				{
					if (item is ToolStripMenuItem)
					{
						TranslateMenuItem(item as ToolStripMenuItem);
					}
				}
			}
		}

		/// <summary>
		///  从wordDict中获取可翻译的word（文件中写死）
		/// </summary>
		/// <param name="word"></param>
		/// <returns></returns>
		public static string TranslateWord(string word) {
			if (language == "zh-CN")
			{
				return word;
			}

			if (wordDict.ContainsKey(word))
			{
				return wordDict[word];
			}
			else {
				return word;
			}			
		}
		
		/// <summary>
		/// 通过百度翻译的API，把传入的中文翻译为英文
		/// </summary>
		/// <param name="word"></param>
		/// <returns></returns>
		public static string TranslateSentence(string sentence) {

			if (language == "zh-CN")
			{
				return sentence;
			}

			if (sentenceDict.ContainsKey(sentence))
			{
				return sentenceDict[sentence];
			}
			else {
				Dictionary<string, string> postParam = new Dictionary<string, string>();
				postParam.Add("q", sentence);
				postParam.Add("from", "zh");
				postParam.Add("to", "en");
				postParam.Add("appid", appid);
				postParam.Add("salt", salt);
				postParam.Add("sign", MD5Helper.MD5_UTF8(appid + sentence + salt + key));
				try
				{					
					string response = HttpHelper.PostUrlWithDict(url, postParam);

					TranslateResult tr = JsonConvert.DeserializeObject<TranslateResult>(response);
					if (tr.trans_result != null)
					{
						string value = tr.trans_result[0]["dst"];
						sentenceDict.Add(sentence, value);
						addCount ++;
						if (addCount % 1 == 0) {
							saveSentenceDict();
						}

						return value;
					}
					else
					{
						return sentence;
					}
				}
				catch (Exception) {
					return sentence;
				}	
			}	
		}
		
		/// <summary>
		///  保存当前的Sentence到硬盘中
		/// </summary>
		private static void saveSentenceDict()
		{
			if ( language == "zh-CN")
			{
				return;
			}
			SortDictionary_Asc(sentenceDict);
			File.WriteAllText( sentenceJsonPath , JsonConvert.SerializeObject( sentenceDict ) );
		}

		/// <summary>
		///  对Dictionary，按照进行升序排序
		/// </summary>
		/// <param name="dic"></param>
		/// <returns></returns>
		private static void SortDictionary_Asc(Dictionary<string, string> dic)
		{
			List<KeyValuePair<string, string>> myList = new List<KeyValuePair<string, string>>(dic);
			myList.Sort(delegate (KeyValuePair<string, string> s1, KeyValuePair<string, string> s2)
			{
				return s1.Key.CompareTo(s2.Key);
			});
			dic.Clear();
			foreach (KeyValuePair<string, string> pair in myList)
			{
				dic.Add(pair.Key, pair.Value);
			}
		}

	}
}
