using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Entity
{
	public class CCEntity
	{
		public string ProtocolName { get; set; }
		public int Com0 { get; set; }
		public int Com1 { get; set; }
		/// <summary>
		/// 主：0 ；从：1
		/// </summary>
		public int PS2 { get; set; }
		public IList<CCData> CCDataList {get;set;}

		public CCEntity() {
			CCDataList = new List<CCData>();
		}

		internal IList<int> SearchIndices(string keyword)
		{
			IList<int> matchIndexList = new List<int>();

			for (int ccDataIndex = 0; ccDataIndex < CCDataList.Count; ccDataIndex++) {
				CCData cd = CCDataList[ccDataIndex];
				if (cd.Function.Contains(keyword) || cd.Code.Contains(keyword) ) {
					matchIndexList.Add(ccDataIndex);
				}				
			}

			return matchIndexList;
		}
	}
}
