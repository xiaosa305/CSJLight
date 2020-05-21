using LightController.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Ast
{
	public class GroupAst
	{
		public string GroupName { get; set; }
		public IList <int> LightIndexList { get; set; }

		/// <summary>
		/// 辅助方法：通过文件属性
		/// </summary>
		/// <param name="iniPath"></param>
		public static IList<GroupAst> GenerateGroupList(string iniPath) {		
			
			IniFileHelper iniHelper = new IniFileHelper(iniPath);
			IList<GroupAst> groupList = new List<GroupAst>();
			int groupCount = iniHelper.ReadInt("Common", "Count", 0);
			for (int groupIndex = 0; groupIndex < groupCount; groupIndex++)
			{
				string groupName = iniHelper.ReadString("Data","G"+groupIndex+"Name","");
				string indexListStr = iniHelper.ReadString("Data", "G" + groupIndex + "IndexList", "");
				if (!string.IsNullOrEmpty(groupName) && !string.IsNullOrEmpty(indexListStr)) {
					string[] indexListArray = indexListStr.Split(',');
					IList<int> lightIndexList = new List<int>();
					foreach (string indexStr in indexListArray)
					{
						try
						{
							int index = int.Parse(indexStr);
							lightIndexList.Add(index);
						}
						catch (Exception ex) {
							Console.WriteLine("groupList数据有错:" + ex.Message);							
						}						
					}
					if (lightIndexList != null && lightIndexList.Count > 0) {
						groupList.Add(new GroupAst() {
							GroupName = groupName,
							LightIndexList = lightIndexList
						});						
					}
				}
			} 
			return groupList;
		}


		/// <summary>
		/// 辅助方法：传入groupList，监测其中是否有当前要验证的名字
		/// </summary>
		/// <param name="groupList"></param>
		/// <returns>可用则为true，否则为false</returns>
		public static bool CheckGroupName(IList<GroupAst> groupList,string newName) {
			bool result = true;
			foreach (GroupAst ga in groupList)
			{
				if (ga.GroupName.Equals(newName)) {
					result = false;
				}
			}
			return result;
		}

	}
}
