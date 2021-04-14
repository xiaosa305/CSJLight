using LightController.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LightController.Ast
{
	public class GroupAst
	{
		public string GroupName { get; set; }
		public IList <int> LightIndexList { get; set; }		
		// 组长：几个组员组成的列表的索引值
		public int CaptainIndex { get; set; } 

		/// <summary>
		/// 辅助方法：传入groupList，监测其中是否有当前要验证的名字 ; true为可用（不存在）
		/// </summary>
		/// <param name="groupList"></param>
		/// <returns>可用则为true，否则为false</returns>
		public static bool CheckGroupName(IList<GroupAst> groupList, string newName)
		{
			bool result = true;
			foreach (GroupAst ga in groupList)
			{
				if (ga.GroupName.Equals(newName))
				{
					result = false;
				}
			}
			return result;
		}
				
		/// <summary>
		/// 辅助方法：通过传入的ini文件路径，生成并返回相应的groupList
		/// </summary>
		/// <param name="iniPath"></param>
		public static IList<GroupAst> GenerateGroupList(string iniPath) {
			IList<GroupAst> groupList = new List<GroupAst>();
			//只有文件存在，才能继续操作，否则返回空列表；
			if (File.Exists(iniPath)) 
			{
				IniHelper iniHelper = new IniHelper(iniPath);
				int groupCount = iniHelper.ReadInt("Common", "Count", 0);
				for (int groupIndex = 0; groupIndex < groupCount; groupIndex++)
				{
					string groupName = iniHelper.ReadString("Data", "G" + groupIndex + "Name", "");
					string indexListStr = iniHelper.ReadString("Data", "G" + groupIndex + "IndexList", "");
					int captainIndex = iniHelper.ReadInt("Data", "G" + groupIndex + "Captain", 0);
					if (!string.IsNullOrEmpty(groupName) && !string.IsNullOrEmpty(indexListStr))
					{
						string[] indexListArray = indexListStr.Split(',');
						IList<int> lightIndexList = new List<int>();

						foreach (string indexStr in indexListArray)
						{
							try
							{
								int index = int.Parse(indexStr);
								lightIndexList.Add(index);
							}
							catch (Exception ex)
							{
								Console.WriteLine("groupList数据有错:" + ex.Message);
							}
						}
						if (lightIndexList != null && lightIndexList.Count > 0)
						{
							groupList.Add(new GroupAst()
							{
								GroupName = groupName,
								LightIndexList = lightIndexList,
								CaptainIndex = captainIndex
							});
						}
					}
				}
			}
			return groupList;
		}
		
		/// <summary>
		/// 辅助方法：传入iniPath及groupList，将数据写入配置文件中
		/// </summary>
		/// <param name="iniPath"></param>
		/// <param name="groupList"></param>
		public static void SaveGroupIni(string iniPath, IList<GroupAst> groupList)
		{
			try
			{
				IniHelper iniHelper = new IniHelper(iniPath);
				iniHelper.DeleteSection("Data"); //删掉所有Data节内的内容 
				if (groupList == null || groupList.Count == 0)
				{
					iniHelper.WriteInt("Common", "Count", 0);
				}
				else
				{
					iniHelper.WriteInt("Common", "Count", groupList.Count);
					for (int groupIndex = 0; groupIndex < groupList.Count; groupIndex++)
					{
						iniHelper.WriteString("Data", "G" + groupIndex + "Name", groupList[groupIndex].GroupName);
						string indexListStr = StringHelper.MakeIntListToString(groupList[groupIndex].LightIndexList , 0,-1);
						iniHelper.WriteString("Data", "G" + groupIndex + "IndexList", indexListStr);
						iniHelper.WriteInt("Data", "G" + groupIndex + "Captain", groupList[groupIndex].CaptainIndex);
					}
				}
			}
			catch (Exception ex) {
				Console.WriteLine("WriteGroupListIni出现异常" + ex.Message);
				throw ex;
			}			
		}
	}
}
