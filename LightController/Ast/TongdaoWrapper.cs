using LightController.MyForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightController.Ast
{
	// 通道包装类，记录了相关信息
	public class TongdaoWrapper
	{
		public string TongdaoName { get; set; }  //通道名称
		public int Address { get; set; }	 // 通道地址
		public int ScrollValue { get; set; }// 调节杆的值 --》两种方法改变：1拉杆 2.填值
		public int ChangeMode { get; set; }     // 变化模式： | 常规：跳变0；渐变1；屏蔽2    |  声控：屏蔽0；跳变1；（渐变2）
		public int StepTime { get; set; }    // 步时间：某内部时间因子的倍数
		public string Remark { get; set; } //备注,主要用以显示各个子属性，在渲染时要写进去

		/// <summary>
		/// 构造方法：因有非空入参的构造函数，故需要一个空的构造函数
		/// </summary>
		public TongdaoWrapper() { }

		/// <summary>
		/// 构造方法：主要被《ActionForm》调用，（因有些数据是默认统一的，不需重新添加）
		/// </summary>
		/// <param name="tdName"></param>
		/// <param name="value"></param>
		/// <param name="stepTime"></param>
		public TongdaoWrapper(string tdName, int value, int stepTime)
		{
			TongdaoName = tdName;
			ScrollValue = value;
			StepTime = stepTime;
			ChangeMode = 1;
		}

		/// <summary>
		/// 构造方法：主要被ColorForm调用，需要用到跳渐变。
		/// </summary>
		/// <param name="tdName"></param>
		/// <param name="value"></param>
		/// <param name="stepTime"></param>
		public TongdaoWrapper(string tdName, int value, int stepTime, int changeMode)
		{
			TongdaoName = tdName;
			ScrollValue = value;
			StepTime = stepTime;
			ChangeMode = changeMode;
		}

		/// <summary>
		/// 通过模板的通道数据，生成新的非引用(要摆脱与StepMode的关系)的tongdaoList
		/// </summary>
		/// <param name="oldTongdaoList"></param>
		/// <returns></returns>
		public static IList<TongdaoWrapper> GenerateTongdaoList(IList<TongdaoWrapper> stepTemplateTongdaoList ,int mode)
		{
			IList<TongdaoWrapper> newList = new List<TongdaoWrapper>();
			foreach (TongdaoWrapper td in stepTemplateTongdaoList)
			{
				// 9.7 如果是模板数据，则根据mode来决定changeMode的初值
				newList.Add(new TongdaoWrapper()	{
						StepTime = td.StepTime,
						TongdaoName = td.TongdaoName,
						ScrollValue = td.ScrollValue,
						ChangeMode = td.ChangeMode == -1 ? (mode == 0 ? 1 : MainFormBase.DefaultSoundCM) : td.ChangeMode ,
						Address = td.Address ,
						Remark = td.Remark
					}
				);
			}
			return newList;
		}

		public override string ToString()
		{
			return TongdaoName + " : "+ Address;
		}


		/// <summary>
		///  辅助方法：传入一个TongdaoWrapper的二维数组及两个相关维度，返回其组员，如果数据越界，则返回屏蔽的通道。
		/// </summary>
		/// <param name="tdArray"></param>
		/// <param name="stepIndex"></param>
		/// <param name="tdIndex"></param>
		/// <returns></returns>
		public static TongdaoWrapper GetTongdaoFromArray(TongdaoWrapper[,] tdArray, int stepIndex, int tdIndex, int mode ) {
			try
			{
				TongdaoWrapper td = tdArray[stepIndex, tdIndex];
				return td;
			}
			catch (Exception) {				
				string tdName = tdArray[0, tdIndex].TongdaoName;
				return new TongdaoWrapper( tdName, 0, 50 , mode==0?2:0); 
			}
		}


	}
}
