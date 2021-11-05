using LightController.Common;
using LightController.Entity;
using LightEditor.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.Ast
{
	public class LightAst
	{	
		public string LightPath { get; set; }
		
		// 此四个属性，可由ini文件获取
		public string LightName { get; set; }
		public string LightType { get; set; }
		public string LightPic { get; set; }
		public int Count { get; set; }
		public string Remark { get; set; }

		// 此三个属性，每个灯都不一样，在灯库编辑时添加
		public string LightAddr { get; set; }
		public int StartNum { get; set; }
		public int EndNum { get; set; }

		// 记录子属性列表（包括按键组）（每个通道为List的一个子项）
		public IList<SAWrapper> SawList { get; set; }
		public Dictionary<int, FlowLayoutPanel> saPanelDict { get; set; }

		/// <summary>
		/// 空构造函数
		/// </summary>
		public LightAst() { }

		/// <summary>
		/// 构造函数，用旧对象完全创造一个新的对象
		/// </summary>
		public LightAst(LightAst laOld)
		{
			LightPath = laOld.LightPath;
			LightName = laOld.LightName;
			LightType = laOld.LightType;
			LightPic = laOld.LightPic;
			Count = laOld.Count;
			LightAddr = laOld.LightAddr;
			StartNum = laOld.StartNum;
			EndNum = laOld.EndNum;
			SawList = laOld.SawList;
			Remark = laOld.Remark;			
			saPanelDict = laOld.saPanelDict;
		}

	
		public static LightAst GenerateLightAst(DB_Light newLight, string savePath)
		{
			int endNum = newLight.LightID + newLight.Count - 1;
			string lightAddr = newLight.LightID + "-" + endNum;
			string iniPath = savePath + @"\LightLibrary\" + newLight.Name + @"\" + newLight.Type + ".ini";

			return new LightAst()
			{
				StartNum = newLight.LightID,
				EndNum = endNum,
				LightName = newLight.Name,
				LightType = newLight.Type,
				LightPic = newLight.Pic,
				Count = newLight.Count,
				LightAddr = lightAddr,
				LightPath = iniPath,
				Remark = newLight.Remark
			};
		}

		/// <summary>
		/// 更改地址后，有些内容需要更新
		/// </summary>
		/// <param name="editLa"></param>
		public void ChangeAddr(LightAst editLa)
		{
			StartNum = editLa.StartNum;
			EndNum = editLa.EndNum;
			LightAddr = editLa.LightAddr;
		}

		/// <summary>
		///  辅助方法：重写（实际上并不算，因为形参不同）Equals语句，当地址和灯的全名一致时，基本可认为是同一个灯
		/// </summary>
		/// <param name="l2"></param>
		/// <returns></returns>
		public bool Equals(LightAst l2)
		{
			if (this.LightName == l2.LightName && this.LightType == l2.LightType
				&& this.LightAddr == l2.LightAddr)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		/// <summary>
		/// 辅助方法：输出lightAst对象的主要属性
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return LightName + ":" + LightType + ":" + LightAddr + ":" + LightPic + ":" + Count;
		}

       
    }
}
