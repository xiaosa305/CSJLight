using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data.SQLite;
using DMX512;
using LightController.Ast;
using LightController.MyForm;
using LightController.Common;
using LightController.Tools;
using System.Windows.Forms;
using System.Threading;

namespace LightController.MyForm
{
	public class MainFormInterface : System.Windows.Forms.Form
	{
		// 辅助的bool变量：	
		protected bool isNew = true;  //点击新建后 到 点击保存前，这个属性是true；如果是使用打开文件或已经点击了保存按钮，则设为false
		protected bool isInit = false;// form都初始化后，才将此变量设为true;为防止某些监听器提前进行监听
		public bool IsCreateSuccess = false;  ///点击新建后，用这个变量决定是否打开灯具编辑列表
		public MaterialAst TempMaterialAst = null;  // 辅助（复制多步、素材）变量 ， 《复制、粘贴多步》时使用

		// 全局配置及数据库连接		
		protected string softwareName = "TRANS-JOY Dimmer System";
		protected string currentProjectName;  //存放当前工程名，主要作用是防止当前工程被删除（openForm中）
		protected string globalIniPath;  // 存放当前工程《全局配置》、《摇麦设置》的配置文件的路径
		protected string dbFilePath; // 数据库地址：每个工程都有自己的db，所以需要一个可以改变的dbFile字符串，存放数据库连接相关信息
		protected bool isEncrypt = false; //是否加密
		protected int eachStepTime = 30; // 默认情况下，步时间默认值为30ms

		// 数据库DAO(data access object：数据访问对象）
		protected LightDAO lightDAO;
		protected StepCountDAO stepCountDAO;
		protected ValueDAO valueDAO;
		protected FineTuneDAO fineTuneDAO;

		// 这几个IList ，存放着所有数据库数据		
		protected IList<DB_Light> dbLightList = new List<DB_Light>();
		protected IList<DB_StepCount> dbStepCountList = new List<DB_StepCount>();
		protected IList<DB_Value> dbValueList = new List<DB_Value>();
		protected IList<DB_FineTune> dbFineTuneList = new List<DB_FineTune>();

		protected IList<LightAst> lightAstList;  //与《灯具编辑》通信用的变量；同时也可以供一些辅助form读取相关灯具的简约信息时使用
		protected IList<LightWrapper> lightWrapperList = new List<LightWrapper>(); // 辅助的灯具变量：记录所有灯具（lightWrapper）的（所有场景和模式）的 每一步（通道列表）


		// 通道数据操作时的变量
		protected const int MAX_STEP = 100;
		protected bool isMultiMode = false; //默认情况下是单灯模式；若进入多灯模式，此变量改成true；											
		protected bool isCopyAll = false;   // 11.20 新功能： 多灯模式仍需要一个变量 ，用以设置是否直接用组长的数据替代组员。（默认情况下应该设为false，可以避免误删步数信息）

		protected int selectedIndex = -1; //选择的灯具的index，默认为-1，如有选中灯具，则改成该灯具的index（在lightAstList、lightWrapperList中）
		protected IList<int> selectedIndices = new List<int>(); //选择的灯具的index列表（多选情况下）		

		protected string selectedLightName = ""; //选中的灯具的名字（lightName + lightType）
		protected int frame = 0; // 表示场景编号(selectedIndex )
		protected int mode = 0;  // 表示模式编号（selectedIndex)；0.常规模式； 1.音频模式

		// protected bool isUseStepTemplate = false ; // 是否勾选了《使用模板生成步》 =>11.26 去除此方法，使用中有点多余。
		// protected LightWrapper tempLight = null; // 辅助灯变量，用以复制及粘贴灯 
		protected StepWrapper tempStep = null; //// 辅助步变量：复制及粘贴步时用到
		

		// 调试变量
		protected PlayTools playTools = PlayTools.GetInstance(); //DMX512灯具操控对象的实例
		protected bool isConnected = false; // 辅助bool值，当选择《连接设备》后，设为true；反之为false
		protected bool isRealtime = false; // 辅助bool值，当选择《实时调试》后，设为true；反之为false			
		protected bool isKeepOtherLights = false;  // 辅助bool值，当选择《（非调灯具)保持状态》时，设为true；反之为false

		protected string[] comList;  //存储DMX512串口的名称列表，用于comSkinComboBox中
		protected string comName; // 存储打开的DMX512串口名称

		protected bool isConnectCom = true; //默认情况下，用串口连接设备。
		protected ConnectTools connectTools;
		protected IList<IPAst> ipaList; // 此列表存储所有建立连接的ipAst


		// 动态导入场景到此静态变量中
		public static IList<string> AllFrameList; // 将所有场景名称写在此处,并供所有类使用
		public static int FrameCount = 0;  //场景数量

		protected string savePath; // 动态载入相关的存储目录（开发时放在C:\Temp中；发布时放在应用所在文件夹）

		protected string arrangeIniPath = null;  // 打开工程时 顺便把相关的位置保存ini(arrange.ini) 也读取出来（若有的话）
		protected bool isAutoArrange = true; // 默认情况下，此值为true，代表右键菜单“自动排列”默认情况下是打开的。
		protected string binPath = null; // 此处记录《硬件更新》时，选过的xbin文件路径。
		protected string projectPath = null; //此处记录《工程更新》时，选过的文件夹路径。
		protected bool isSyncMode = false;  // 同步模式为true；异步模式为false(默认）
		protected Dictionary<int, int> lightDictionary = null ;		       

		#region 几个纯虚（virtual修饰）方法：主要供各种基类方法向子类回调使用		

		protected virtual void enableGlobalSet(bool enable) { } // 是否显示《全局设置》等
		protected virtual void enableSave(bool enable) { }  // 是否显示《保存工程》等
		protected virtual void enableSLArrange(bool enableSave, bool enableLoad) { }  //是否显示《 存、取 灯具位置》		
		protected virtual void showPlayPanel(bool visible) { } // 是否显示PlayFlowLayoutPanel
		protected virtual void enableRefreshPic(bool enable) { } // 是否使能《重新加载灯具图片》
		protected virtual void chooseStep(int stepNum) { }  //选步

		protected virtual void setBusy(bool buzy) { } //设置是否忙时
		public virtual void ResetSyncMode() { } // 清空syncStep
		public virtual void SetNotice(string notice) { } //设置提示信息
		public virtual void EnableConnectedButtons(bool connected) { } //设置《连接按钮组》是否可用

		#endregion

		/// <summary>
		/// 辅助方法： 清空相关的所有数据（关闭工程、新建工程、打开工程都会用到）
		/// -- 子类中需有针对该子类内部自己的部分代码（如重置listView或禁用stepPanel等）
		/// </summary>
		protected virtual void clearAllData()
		{
			currentProjectName = null;

			dbLightList = null;
			dbStepCountList = null;
			dbValueList = null;
			dbFineTuneList = null;

			lightAstList = null;
			lightWrapperList = null;
			lightDictionary = null;

			selectedIndex = -1;
			selectedLightName = "";
			selectedIndices = new List<int>();

			tempStep = null;
			TempMaterialAst = null;

			arrangeIniPath = null;
			enableSLArrange(false, false);
			enableSave(false);

			ResetSyncMode();
			AutosetEnabledPlayAndRefreshPic();
		}

		/// <summary>
		/// 基类辅助方法：①ClearAllData()；②设置内部的一些工程路径及变量；③初始化数据库
		/// </summary>
		/// <param name="projectName"></param>
		/// <param name="isNew"></param>
		public void InitProject(string projectName, bool isNew) {

			//0.清空所有list
			clearAllData();

			// 1.全局设置
			currentProjectName = projectName;
			string directoryPath = savePath + @"\LightProject\" + projectName;
			globalIniPath = directoryPath + @"\global.ini";
			dbFilePath = directoryPath + @"\data.db3";
			this.Text = "TRANS-JOY Dimmer System(当前工程:" + projectName + ")";
			this.isNew = isNew;

			//10.9 设置当前工程的 arrange.ini 的地址,以及先把各种可用性屏蔽掉
			arrangeIniPath = directoryPath + @"\arrange.ini";

			// 9.5 读取时间因子
			IniFileAst iniAst = new IniFileAst(globalIniPath);
			eachStepTime = iniAst.ReadInt("Set", "EachStepTime", 30);

			// 2.创建数据库:（10.15修改）
			// 因为是初始化，所以让所有的DAO指向new xxDAO，避免连接到错误的数据库(已打开过旧的工程的情况下)；
			// --若isNew为true时，为初始化数据库，可随即用其中一个DAO运行CreateSchema方法（用实体类建表）
			lightDAO = new LightDAO(dbFilePath, isEncrypt);
			stepCountDAO = new StepCountDAO(dbFilePath, isEncrypt);
			valueDAO = new ValueDAO(dbFilePath, isEncrypt);
			fineTuneDAO = new FineTuneDAO(dbFilePath, isEncrypt);

			// 若为新建，则初始化db的table(随机使用一个DAO即可初始化）
			if (isNew)
			{
				lightDAO.CreateSchema(true, true);
			}

			// 设置各按键是否可用
			enableGlobalSet(true);
			enableSave(true);
		}

		/// <summary>
		///  辅助方法: 通过工程名，新建工程; 主要通过调用InitProject(),但在前后加了鼠标特效的处理。（此操作可能耗时较久，故在方法体前后添加鼠标样式的变化）
		/// </summary>
		/// <param name="projectName">工程名(无需isNew参数)</param>
		public void NewProject(string projectName)
		{
			this.Cursor = Cursors.WaitCursor;
			InitProject(projectName, true);
			this.Cursor = Cursors.Default;
		}

		/// <summary>
		///  辅助方法，通过打开已有的工程，来加载各种数据到mainForm中
		/// data.db3的载入：把相关内容，放到数据列表中
		///    ①lightList 、stepCountList、valueList
		///    ②lightAstList（由lightList生成）
		///    ③lightWrapperList(由lightAstList生成)
		/// （此操作可能耗时较久，故在方法体前后添加鼠标样式的变化）
		/// </summary>
		/// <param name="directoryPath"></param>
		public void OpenProject(string projectName)
		{
			SetNotice("正在打开工程，请稍候...");			
			setBusy(true);		

			DateTime beforDT = System.DateTime.Now;

			// 0.初始化
			InitProject(projectName, false);

			//10.9 设置listView右键菜单中读取配置的可用项					
			if (!isAutoArrange)
			{
				enableSLArrange(true, File.Exists(arrangeIniPath));
			}

			// 把各数据库表的内容填充进来。
			dbLightList = getLightList();
			//10.17 此处添加验证 : 如果是空工程(无任何灯具可认为是空工程)，后面的数据无需读取。
			if (dbLightList == null || dbLightList.Count == 0)
			{
				DialogResult dr = MessageBox.Show("成功打开空工程：" + projectName + "  , 要为此工程添加灯具吗？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
				SetNotice("成功打开工程(" + projectName + ")");
				if (dr == DialogResult.OK)
				{
					new LightsForm(this, null).ShowDialog();
				}				
			}
			//10.17 若非空工程，则继续执行以下代码。
			else
			{
				dbStepCountList = getStepCountList();
				dbFineTuneList = getFineTuneList();
				lightAstList = reCreateLightAstList(dbLightList); // 通过lightList填充lightAstList
				AddLightAstList(lightAstList); // 通过初步lightAstList，生成 最终版的 lightAstList、lightsListView、lightWrapperList的内容
				
				try
				{
					GenerateAllStepTemplates(); // 8.29 统一生成步数模板
				}
				catch (Exception ex) {
					openProjectError(ex.Message);
					return;
				}

				//MARK：MainFormInterface.OpenProject()内 : 针对每个lightWrapper，获取其已有步数的场景和模式；采用多线程优化(每个灯开启一个线程)				
				Thread[] threadArray = new Thread[dbLightList.Count];
				for (int lightListIndex = 0; lightListIndex < dbLightList.Count; lightListIndex++)
				{
					int tempLightIndex = lightListIndex; // 必须在循环内使用一个临时变量来记录这个index，否则线程运行时lightListIndex会发生变化。
					int tempLightNo = dbLightList[tempLightIndex].LightNo;   //记录了数据库中灯具的起始地址（不同灯具有1-32个通道，但只要是同个灯，就公用此LightNo)				

					IList<DB_StepCount> scList = stepCountDAO.getStepCountList(tempLightNo);
					IList<DB_Value> tempDbValueList = null;
					tempDbValueList = valueDAO.GetByLightNo(tempLightNo);
					threadArray[tempLightIndex] = new Thread(delegate ()
					{
						//Console.WriteLine(tempLightIndex + " ++ 线程开始了");
						if (scList != null && scList.Count > 0)
						{
							for (int scIndex = 0; scIndex < scList.Count; scIndex++)
							{
								DB_StepCount sc = scList[scIndex];
								int frame = sc.PK.Frame;
								int mode = sc.PK.Mode;
								int lightIndex = sc.PK.LightIndex;
								int stepCount = sc.StepCount;

								lightWrapperList[tempLightIndex].LightStepWrapperList[frame, mode] = new LightStepWrapper();

								for (int step = 1; step <= stepCount; step++)
								{
									StepWrapper stepWrapper = null;
																	
									var stepValueListTemp = tempDbValueList.Where(t => t.PK.LightIndex == lightIndex && t.PK.Frame == frame && t.PK.Mode == mode && t.PK.Step == step);
									stepWrapper = StepWrapper.GenerateStepWrapper(lightWrapperList[tempLightIndex].StepTemplate, stepValueListTemp.ToList<DB_Value>(), mode);
									lightWrapperList[tempLightIndex].LightStepWrapperList[frame, mode].AddStep(stepWrapper);
								}
							}
						}
						//Console.WriteLine(tempLightIndex + " -- 线程结束了");
					});
					threadArray[tempLightIndex].Start();
				}

				// 下列代码，用以监视所有线程是否已经结束运行。
				while (true)
				{
					int unFinishedCount = 0;
					foreach (var thread in threadArray)
					{
						unFinishedCount += thread.IsAlive ? 1 : 0;
					}

					if (unFinishedCount == 0)
					{
						Console.WriteLine("Dickov:所有线程已结束。");
						break;
					}
					else
					{
						Thread.Sleep(100);
					}
				}

				AutosetEnabledPlayAndRefreshPic();

				DateTime afterDT = System.DateTime.Now;
				TimeSpan ts = afterDT.Subtract(beforDT);

				MessageBox.Show("成功打开工程：" + projectName + ",耗时: " + ts.TotalSeconds.ToString("#0.00") + " s");
				SetNotice("成功打开工程("+ projectName + ")");
			}
			setBusy(false);
		}

		/// <summary>
		/// 辅助方法：提示工程与灯库不匹配，并clearAll
		/// </summary>
		/// <param name="ex"></param>
		private void openProjectError(string exMessage)
		{
			MessageBox.Show("打开工程出错，可能是灯库不匹配。\n异常信息:" + exMessage);
			clearAllData();
			setBusy(false);
			SetNotice("打开工程失败,请验证后重试");
		}

		/// <summary>
		/// 辅助方法：当xbin文件选中后，让mainForm留一个xbin路径的备份，下次重新打开《硬件升级》时可以用到，避免重复打开。
		/// </summary>
		/// <param name="binPath"></param>
		internal void SetBinPath(string binPath)
		{
			this.binPath = binPath;
		}

		/// <summary>
		/// 辅助方法：记录导出工程文件夹，供下载工程使用。
		/// </summary>
		/// <param name="binPath"></param>
		internal void SetProjectPath(string projectPath) {
			this.projectPath = projectPath;
		}

		

		/// <summary>
		///  辅助方法：实时读取（渲染）单灯单步的数据（单线程即可）
		/// </summary>
		protected void MakeCurrentStepWrapperData(int stepNum) {

			//TODO : 10.X （优化计划）从数据库中取单灯单步数据，并渲染进内存。
			if (getCurrentLightStepWrapper().StepWrapperList[stepNum - 1] != null && getCurrentLightStepWrapper().StepWrapperList[stepNum - 1].Flag == 0) {

				LightAst la = lightAstList[selectedIndex];
				int lightIndex = la.StartNum;
				IList<DB_Value> tempValueList = valueDAO.getStepValueList(lightIndex, frame, mode, getCurrentStep());

				if (tempValueList.Count == getCurrentLightStepWrapper().StepWrapperList[stepNum - 1].TongdaoList.Count) {
					for (int tdIndex = 0; tdIndex < tempValueList.Count; tdIndex++)
					{
						DB_Value stepValue = tempValueList[tdIndex];
						getCurrentLightStepWrapper().StepWrapperList[stepNum - 1].TongdaoList[tdIndex].ScrollValue = tempValueList[tdIndex].ScrollValue;
						getCurrentLightStepWrapper().StepWrapperList[stepNum - 1].TongdaoList[tdIndex].StepTime = tempValueList[tdIndex].StepTime;
						getCurrentLightStepWrapper().StepWrapperList[stepNum - 1].TongdaoList[tdIndex].ChangeMode = tempValueList[tdIndex].ChangeMode;
					}
				}
				getCurrentLightStepWrapper().StepWrapperList[stepNum - 1].Flag = 1;
				Console.WriteLine("此步成功渲染。");
			}
			else {
				Console.WriteLine("此步已渲染过了。");
			}
		}


		/// <summary>
		///  辅助方法：判断是否可以显示 playPanel及 刷新图片(主要供《打开工程》和《添加灯具Form》使用）
		///  --这两个功能都依赖于当前Form中的lightAstList是否为空。
		/// </summary>
		public void AutosetEnabledPlayAndRefreshPic()
		{
			bool enable = lightAstList != null && lightAstList.Count > 0;
			showPlayPanel(enable);
			enableRefreshPic(enable);
		}


		/// <summary>
		/// 辅助方法：使用lightList来生成一个新的lightAstList
		/// </summary>
		/// <param name="lightList"></param>
		/// <returns></returns>
		protected IList<LightAst> reCreateLightAstList(IList<DB_Light> lightList)
		{
			IList<LightAst> lightAstList = new List<LightAst>();
			foreach (DB_Light light in lightList)
			{
				lightAstList.Add(LightAst.GenerateLightAst(light));
			}
			return lightAstList;
		}

		/// <summary>
		/// 辅助方法（虚）：添加lightAst列表到主界面内存中,主要供 LightsForm调用（以及OpenProject调用）
		/// </summary>
		public virtual void AddLightAstList(IList<LightAst> lightAstList2)
		{
			List<LightWrapper> lightWrapperList2 = new List<LightWrapper>();
			for (int i = 0; i < lightAstList2.Count; i++)
			{
				// 如果addOld改成true，则说明lighatWrapperList2已添加了旧数据，否则就要新建一个空LightWrapper。
				bool addOld = false;
				if (lightWrapperList != null && lightWrapperList.Count > 0)
				{
					for (int j = 0; j < lightAstList.Count; j++)
					{
						if (j < lightWrapperList.Count
							&& lightAstList2[i].Equals(lightAstList[j])
							&& lightWrapperList[j] != null)
						{
							lightWrapperList2.Add(lightWrapperList[j]);
							addOld = true;
							break;
						}
					}
				}
				if (!addOld)
				{
					Console.WriteLine("Dickov : 添加了一个全新的LightWrapper（还没有生成StepTemplate)");
					lightWrapperList2.Add(new LightWrapper());
				}
			}
			lightAstList = new List<LightAst>(lightAstList2);
			lightWrapperList = new List<LightWrapper>(lightWrapperList2);

			lightDictionary = new Dictionary<int, int>();
			for (int i = 0; i < lightAstList.Count; i++)
			{
				lightDictionary.Add( lightAstList[i].StartNum , i );
			}
		}

		/// <summary>
		/// 辅助方法：在《打开工程》、《添加灯具》时统一生成所有的LightWrapper的StepTemplate（步数模板）
		/// </summary>
		public void GenerateAllStepTemplates() {
			if (lightWrapperList.Count != lightAstList.Count) {
				MessageBox.Show("统一生成步数模板时验证错误：lightWrapperList和lightAstList数量不合！");
				return;
			}
			for (int i = 0; i < lightWrapperList.Count; i++)
			{
				if (lightWrapperList[i].StepTemplate == null) {
					lightWrapperList[i].StepTemplate = generateStepTemplate(lightAstList[i]);
				}
			}
		}

		/// <summary>
		/// 辅助方法：生成模板Step --》 之后每新建一步，都复制模板step的数据。
		/// </summary>
		/// <param name="lightAst"></param>
		/// <param name="lightIndex"></param>
		/// <returns></returns>
		protected StepWrapper generateStepTemplate(LightAst lightAst)
		{
			Console.WriteLine("Dickov : 开始生成模板文件(StepTemplate)：" + lightAst.LightName + ":" + lightAst.LightType + "(" + lightAst.LightAddr + ")");			
			try{
				using (FileStream file = new FileStream(lightAst.LightPath, FileMode.Open))
				{					
					// 可指定编码，默认的用Default，它会读取系统的编码（ANSI-->针对不同地区的系统使用不同编码，中文就是GBK）
					StreamReader reader = new StreamReader(file, Encoding.UTF8);
					string s = "";
					ArrayList lineList = new ArrayList();
					int lineCount = 0;
					while ((s = reader.ReadLine()) != null)
					{
						lineList.Add(s);
						lineCount++;
					}
					// 当行数小于5行时，这个文件肯定是错的;最少需要5行(实际上真实数据至少9行: [set]+4+[data]+3 ）
					if (lineCount < 5)
					{						
						throw new Exception("打开的灯库文件格式有误，无法生成StepTemplate");
						//MessageBox.Show("Dickov：打开的灯库文件格式有误，无法生成StepTemplate！");
						//return null;     //原代码：这样程序会继续下去，是错误的处理方式。
					}
					else
					{
						int tongdaoCount2 = (lineCount - 6) / 3;
						int tongdaoCount = int.Parse(lineList[3].ToString().Substring(6));
						if (tongdaoCount2 < tongdaoCount)
						{							
							throw new Exception("打开的灯库文件的count值与实际值不符合，无法生成StepTemplate");
						}

						List<TongdaoWrapper> tongdaoList = new List<TongdaoWrapper>();						
						for (int i = 0; i < tongdaoCount; i++)
						{
							string tongdaoName = lineList[3 * i + 6].ToString().Substring(4);
							int initNum = int.Parse(lineList[3 * i + 7].ToString().Substring(4));
							int address = int.Parse(lineList[3 * i + 8].ToString().Substring(4));
							tongdaoList.Add(new TongdaoWrapper()
							{
								TongdaoName = tongdaoName,
								ScrollValue = initNum,
								StepTime = 66,
								ChangeMode = -1,
								Address = lightAst.StartNum + (address - 1)
							});
						}
						return new StepWrapper()
						{
							TongdaoList = tongdaoList,
							LightFullName = lightAst.LightName + "*" + lightAst.LightType, // 使用“*”作为分隔符，这样的字符无法在系统生成文件夹，可有效防止有些灯刚好Name+Type的组合相同
							StartNum = lightAst.StartNum							
						};
					}
				}
			}
			catch (Exception ex) {
					throw ex;				
			}
		}

		#region 由数据库取数据的几个方法

		/// <summary>
		///  辅助方法：由dbFilePath，获取lightList
		/// </summary>
		/// <returns></returns>
		protected IList<DB_Light> getLightList()
		{
			if (lightDAO == null)
			{
				lightDAO = new LightDAO(dbFilePath, isEncrypt);
			}
			IList<DB_Light> lightList = lightDAO.GetAll();
			return lightList;
		}

		/// <summary>
		///  辅助方法：由dbFilePath，获取stepCountList
		/// </summary>
		/// <returns></returns>
		protected IList<DB_StepCount> getStepCountList()
		{
			if (stepCountDAO == null)
			{
				stepCountDAO = new StepCountDAO(dbFilePath, isEncrypt);
			}
			IList<DB_StepCount> scList = stepCountDAO.GetAll();
			return scList;
		}

		/// <summary>
		///  辅助方法：由dbFilePath，获取valueList
		/// </summary>
		/// <returns></returns>
		protected IList<DB_Value> getValueList()
		{
			if (valueDAO == null)
			{
				valueDAO = new ValueDAO(dbFilePath, isEncrypt);
			}

			IList<DB_Value> valueList = valueDAO.GetAll();
			return valueList;
		}

		/// <summary>
		///  辅助方法：由dbFilePath，获取fineTuneList
		/// </summary>
		/// <returns></returns>
		protected IList<DB_FineTune> getFineTuneList()
		{
			if (fineTuneDAO == null)
			{
				fineTuneDAO = new FineTuneDAO(dbFilePath, isEncrypt);
			}
			try
			{
				IList<DB_FineTune> ftList = fineTuneDAO.GetAll();
				return ftList;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return null;
			}

		}

		#endregion


		/// <summary>
		///  辅助方法：通过isFromDB属性，来获取内存或数据库中的DBWrapper(三个列表的集合)
		/// </summary>
		/// <returns></returns>
		protected DBWrapper GetDBWrapper(bool isFromDB)
		{
			// 从数据库直接读取的情况
			if (isFromDB)
			{
				DBGetter dbGetter = new DBGetter(dbFilePath, isEncrypt);
				DBWrapper allData = dbGetter.getAll();
				return allData;
			}
			// 由内存几个实时的List实时生成
			else
			{
				// 先生成最新的 dbLightList,dbStepCountList, dbValueList 数据
				generateDBLightList();
				generateDBStepCountList();
				generateDBFineTuneList();

				//generateDBValueList();
				IList<DB_Value> dbValueListTemp = generateDBValueList(frame);

				DBWrapper allData = new DBWrapper(dbLightList, dbStepCountList, dbValueListTemp, dbFineTuneList);
				return allData;
			}
		}

		/// <summary>
		///  保存灯具数据；有几个灯具就保存几个-->先统一删除，再保存
		///  ---（效率比SaveOrUpdate更高，且如果有被删掉的数据，此方法会顺便把该数据删除）
		/// </summary>
		protected void saveAllLights()
		{
			if (lightDAO == null)
			{
				lightDAO = new LightDAO(dbFilePath, isEncrypt);
			}

			// 由lightAstList生成最新的dbLightList
			generateDBLightList();

			// 将传送所有的DB_Light给DAO,让它进行数据的保存
			lightDAO.SaveAll("Light", dbLightList);
		}

		/// <summary>
		/// 辅助方法：由内存的lightAstList生成最新的dbLightList
		/// </summary>
		protected void generateDBLightList() {

			dbLightList = new List<DB_Light>();
			foreach (LightAst la in lightAstList)
			{
				DB_Light light = LightAst.GenerateLight(la);
				dbLightList.Add(light);
			}
		}

		/// <summary>
		///  辅助方法：保存所有的《微调对应键值》列表
		/// </summary>
		protected void saveAllFineTunes()
		{
			if (fineTuneDAO == null)
			{
				fineTuneDAO = new FineTuneDAO(dbFilePath, isEncrypt);
			}
			generateDBFineTuneList();

			// 保存数据
			fineTuneDAO.SaveAll("FineTune", dbFineTuneList);
		}

		/// <summary>
		/// 辅助方法：由lightWrapperList生成最新的dbFineTuneList;
		/// </summary>
		private void generateDBFineTuneList()
		{
			dbFineTuneList = new List<DB_FineTune>(); // 每次更新为最新数据

			// 遍历lightWrapperList的模板数据，用以读取相关的通道名称，才能加以处理			
			foreach (LightWrapper lightWrapper in lightWrapperList) {
				StepWrapper stepTemplate = lightWrapper.StepTemplate;
				if (stepTemplate != null && stepTemplate.TongdaoList != null) {
					int xz = 0, xzwt = 0, xzValue = 0, yz = 0, yzwt = 0, yzValue = 0;
					foreach (TongdaoWrapper td in stepTemplate.TongdaoList)
					{
						switch (td.TongdaoName.Trim()) {
							case "X轴": xz = td.Address; break;
							case "X轴微调": xzwt = td.Address; xzValue = td.ScrollValue; break;
							case "Y轴": yz = td.Address; break;
							case "Y轴微调": yzwt = td.Address; yzValue = td.ScrollValue; break;
						}
					}
					if (xz != 0 && xzwt != 0) {
						dbFineTuneList.Add(new DB_FineTune() { MainIndex = xz, FineTuneIndex = xzwt, MaxValue = xzValue });
					}
					if (yz != 0 && yzwt != 0) {
						dbFineTuneList.Add(new DB_FineTune() { MainIndex = yz, FineTuneIndex = yzwt, MaxValue = yzValue });
					}
				}
			}
		}

		/// <summary>
		/// 保存步数信息：针对每个灯具，保存其相关的步数情况; 
		/// </summary>
		protected void saveAllStepCounts()
		{
			if (stepCountDAO == null)
			{
				stepCountDAO = new StepCountDAO(dbFilePath, isEncrypt);
			}
			// 由lightWrapperList生成最新的dbStepCountList
			generateDBStepCountList();
			// 先删除所有，再保存当前的列表
			stepCountDAO.SaveAll("StepCount", dbStepCountList);
		}

		/// <summary>
		/// 辅助方法：由lightWrapperList生成最新的dbStepCountList，都放在内存中
		/// </summary>
		protected void generateDBStepCountList()
		{
			// 保存所有步骤前，先清空stepCountList
			dbStepCountList = new List<DB_StepCount>();
			// 取出每个灯具的所有【非null】stepCount,填入stepCountList中
			foreach (LightWrapper lightTemp in lightWrapperList)
			{
				DB_Light light = dbLightList[lightWrapperList.IndexOf(lightTemp)];
				LightStepWrapper[,] allLightStepWrappers = lightTemp.LightStepWrapperList;

				// 取出灯具的每个常规场景，并将它们保存起来（但若为空，则不保存）
				for (int frame = 0; frame < FrameCount; frame++)
				{
					for (int mode = 0; mode < 2; mode++)
					{
						LightStepWrapper lsTemp = allLightStepWrappers[frame, mode];
						if (lsTemp != null)
						{
							DB_StepCount stepCount = new DB_StepCount()
							{
								StepCount = lsTemp.TotalStep,
								PK = new DB_StepCountPK()
								{
									Frame = frame,
									Mode = mode,
									LightIndex = light.LightNo
								}
							};
							dbStepCountList.Add(stepCount);
						}
					}
				}
			}
		}

		/// <summary>
		/// 辅助方法：只保存指定场景的stepCount值到数据库
		/// </summary>
		protected void saveFrameSCAndValue(int tempFrame)
		{
			if (stepCountDAO == null)
			{
				stepCountDAO = new StepCountDAO(dbFilePath, isEncrypt);
			}

			if (valueDAO == null)
			{
				valueDAO = new ValueDAO(dbFilePath, isEncrypt);
			}

			IList<DB_Value> frameValueList = new List<DB_Value>();
			foreach (LightWrapper lightTemp in lightWrapperList)
			{
				DB_Light light = dbLightList[lightWrapperList.IndexOf(lightTemp)];
				LightStepWrapper[,] allLightStepWrappers = lightTemp.LightStepWrapperList;

				//10.17 取出灯具的当前场景（两种模式都要），并将它们保存起来（但若为空，则不保存）
				for (int mode = 0; mode < 2; mode++)
				{
					LightStepWrapper lsTemp = allLightStepWrappers[tempFrame, mode];

					// 只有满足所有的这些要求，才能升级这条数据，否则就删除（各种情况的空步）
					if (lsTemp != null && lsTemp.StepWrapperList != null && lsTemp.StepWrapperList.Count > 0)
					{
						DB_StepCount stepCount = new DB_StepCount()
						{
							StepCount = lsTemp.TotalStep,
							PK = new DB_StepCountPK()
							{
								Frame = tempFrame,
								Mode = mode,
								LightIndex = light.LightNo
							}
						};
						stepCountDAO.SaveOrUpdate(stepCount);
					}
					else {
						stepCountDAO.Delete(new DB_StepCount()
						{
							PK = new DB_StepCountPK()
							{
								Frame = tempFrame,
								Mode = mode,
								LightIndex = light.LightNo
							}
						});
					}

					//只有不为null且步数>0，才可能有需要保存的数据
					if (lsTemp != null && lsTemp.TotalStep > 0)
					{
						IList<StepWrapper> stepWrapperList = lsTemp.StepWrapperList;
						foreach (StepWrapper step in stepWrapperList)
						{
							int stepIndex = stepWrapperList.IndexOf(step) + 1;
							for (int tongdaoIndex = 0; tongdaoIndex < step.TongdaoList.Count; tongdaoIndex++)
							{
								TongdaoWrapper tongdao = step.TongdaoList[tongdaoIndex];
								DB_Value valueTemp = new DB_Value()
								{
									ChangeMode = tongdao.ChangeMode,
									ScrollValue = tongdao.ScrollValue,
									StepTime = tongdao.StepTime,
									PK = new DB_ValuePK()
									{
										Frame = tempFrame,
										Mode = mode,
										LightID = light.LightNo + tongdaoIndex,
										LightIndex = light.LightNo,
										Step = stepIndex
									}
								};
								frameValueList.Add(valueTemp);
							}
						}
					}
				}
			}
			valueDAO.SaveFrameValues(tempFrame, frameValueList);
		}

		/// <summary>
		/// 辅助方法：存放所有灯具所有场景的每一步每一通道的值，记录数据到db.Value表中
		/// </summary>
		protected void saveAllValues()
		{
			// 10.24 过时的方法:可能导致内存溢出（数据量过大）
			if (valueDAO == null)
			{
				valueDAO = new ValueDAO(dbFilePath, isEncrypt);
			}
			//由lightWrapperList等内存数据，生成dbValueList
			generateDBValueList();
			// 调用此方法，会先删除之前的表数据，再将当前dbValueList保存到数据库中

			valueDAO.SaveAll("Value", dbValueList);

		}

		/// <summary>
		/// 辅助方法：由lightWrapperList等内存数据，生成dbValueList
		/// </summary>
		protected void generateDBValueList()
		{
			// 需要先清空valueList
			dbValueList = new List<DB_Value>();

			foreach (LightWrapper lightTemp in lightWrapperList)
			{
				DB_Light light = dbLightList[lightWrapperList.IndexOf(lightTemp)];
				LightStepWrapper[,] lswl = lightTemp.LightStepWrapperList;
				for (int frame = 0; frame < FrameCount; frame++)
				{
					for (int mode = 0; mode < 2; mode++)
					{
						LightStepWrapper lightStep = lswl[frame, mode];
						if (lightStep != null && lightStep.TotalStep > 0)
						{  //只有不为null，才可能有需要保存的数据
							IList<StepWrapper> stepWrapperList = lightStep.StepWrapperList;
							foreach (StepWrapper step in stepWrapperList)
							{
								int stepIndex = stepWrapperList.IndexOf(step) + 1;
								for (int tongdaoIndex = 0; tongdaoIndex < step.TongdaoList.Count; tongdaoIndex++)
								{
									TongdaoWrapper tongdao = step.TongdaoList[tongdaoIndex];
									DB_Value valueTemp = new DB_Value()
									{
										ChangeMode = tongdao.ChangeMode,
										ScrollValue = tongdao.ScrollValue,
										StepTime = tongdao.StepTime,
										PK = new DB_ValuePK()
										{
											Frame = frame,
											Mode = mode,
											LightID = light.LightNo + tongdaoIndex,
											LightIndex = light.LightNo,
											Step = stepIndex
										}
									};
									dbValueList.Add(valueTemp);
								}
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// 辅助方法：此处的数据只供预览使用， 因为预览只针对单个场景，没有必要传递所有的value数据。
		/// </summary>
		protected IList<DB_Value> generateDBValueList(int tempFrame)
		{
			// 需要先清空valueList
			IList<DB_Value> result = new List<DB_Value>();

			foreach (LightWrapper lightTemp in lightWrapperList)
			{
				DB_Light light = dbLightList[lightWrapperList.IndexOf(lightTemp)];
				LightStepWrapper[,] lswl = lightTemp.LightStepWrapperList;
				for (int mode = 0; mode < 2; mode++)
				{
					LightStepWrapper lightStep = lswl[tempFrame, mode];
					if (lightStep != null && lightStep.TotalStep > 0)
					{  //只有不为null，才可能有需要保存的数据
						IList<StepWrapper> stepWrapperList = lightStep.StepWrapperList;
						foreach (StepWrapper step in stepWrapperList)
						{
							int stepIndex = stepWrapperList.IndexOf(step) + 1;
							for (int tongdaoIndex = 0; tongdaoIndex < step.TongdaoList.Count; tongdaoIndex++)
							{
								TongdaoWrapper tongdao = step.TongdaoList[tongdaoIndex];
								DB_Value valueTemp = new DB_Value()
								{
									ChangeMode = tongdao.ChangeMode,
									ScrollValue = tongdao.ScrollValue,
									StepTime = tongdao.StepTime,
									PK = new DB_ValuePK()
									{
										Frame = tempFrame,
										Mode = mode,
										LightID = light.LightNo + tongdaoIndex,
										LightIndex = light.LightNo,
										Step = stepIndex
									}
								};
								result.Add(valueTemp);
							}
						}
					}
				}
			}

			return result;
		}

		/// <summary>
		/// 点击《保存工程》按钮
		/// 保存需要进行的操作：
		/// 1.将lightAstList添加到light表中 --> 分新建或打开文件两种情况
		/// 2.将步数、素材、value表的数据都填进各自的表中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void saveAll()
		{
			DateTime beforeDT = System.DateTime.Now;

			// 1.先判断是否有灯具数据；若无，则直接停止
			if (lightAstList == null || lightAstList.Count == 0)
			{
				MessageBox.Show("当前并没有灯具数据，无法保存！");
				return;
			}

			// 2.保存各项数据
			saveAllLights();
			saveAllStepCounts();
			saveAllSCAndValues();
			//saveAllStepCounts();

			DateTime afterDT = System.DateTime.Now;
			TimeSpan ts = afterDT.Subtract(beforeDT);

			MessageBox.Show("成功保存工程:" + currentProjectName + ",耗时: " + ts.TotalSeconds.ToString("#0.00") + " s");
		}


		/// <summary>
		/// 辅助方法：通过遍历的方法，逐一把所有frame的stepCount和values数据写到数据库中
		/// </summary>
		private void saveAllSCAndValues()
		{
			for (int tempFrame = 0; tempFrame < FrameCount; tempFrame++)
			{
				saveFrameSCAndValue(tempFrame);
			}
		}

		/// <summary>
		/// 点击《保存场景》按钮
		/// 保存需要进行的操作：
		/// 1.将lightAstList添加到light表中 --> 分新建或打开文件两种情况
		/// 2.将步数、素材、value表的数据都填进各自的表中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void saveFrame()
		{
			// 1.先判断是否有灯具数据；若无，则直接停止
			if (lightAstList == null || lightAstList.Count == 0)
			{
				MessageBox.Show("当前并没有灯具数据，无法保存！");
				return;
			}

			// 2.保存各项数据，其中保存 灯具、FineTune 是通用的；StepCounts和Values直接用saveOrUpdate方式即可。
			saveAllLights();
			saveAllFineTunes();

			// 只保存当前场景（两种模式）的stepCount和value
			saveFrameSCAndValue(frame);

			MessageBox.Show("成功保存当前场景数据。");
		}

		#region 素材相关

		/// <summary>
		/// MARK：调用素材 或 多灯粘贴 
		/// 辅助方法:调用素材
		/// </summary>
		/// <param name="materialAst"></param>
		/// <param name="method"></param>
		public virtual void InsertOrCoverMaterial(MaterialAst materialAst, InsertMethod method)
		{
			if (materialAst == null) {
				MessageBox.Show("素材调用失败");
				return;
			}

			LightStepWrapper lsWrapper = getCurrentLightStepWrapper();
			int totalStep = lsWrapper.TotalStep;
			int currentStep = lsWrapper.CurrentStep;
			int addStepCount = materialAst.StepCount;

			// 选择《插入》时的操作：后插法（往当前步后加数据）
			// 8.28 当选择《覆盖》但总步数为0时（currentStep也是0），也用插入的方法
			if (method == InsertMethod.INSERT || totalStep == 0)
			{
				int finalStep = totalStep + addStepCount;  // 选择插入多步时，这里的finalStep是指最终的总
				if (finalStep > MAX_STEP)
				{
					MessageBox.Show("素材(或多步)步数超过当前模式剩余步数，无法调用");
					return;
				}

				StepWrapper stepTemplate = getCurrentStepTemplate();
				IList<MaterialIndexAst> sameTDIndexList = getSameTDIndexList(materialAst.TdNameList, stepTemplate.TongdaoList);
				if (sameTDIndexList.Count == 0)
				{
					MessageBox.Show("该素材(或多步)与当前灯具不匹配，无法调用");
					return;
				}

				StepWrapper newStep = null;
				for (int stepIndex = 0; stepIndex < addStepCount; stepIndex++)
				{
					newStep = StepWrapper.GenerateNewStep(stepTemplate, mode);
					// 改造下newStep,将素材值赋给newStep 
					changeStepFromMaterial(materialAst.TongdaoList, stepIndex, sameTDIndexList, newStep);
					// 使用后插法：避免当前无数据的情况下调用素材失败
					lsWrapper.InsertStep(lsWrapper.CurrentStep - 1, newStep, false);
				}

				if (isMultiMode) {
					foreach (int lightIndex in selectedIndices) {
						if (lightIndex != selectedIndex) {
							// 多灯模式下，依然使用上面的步骤来插入素材。
							for (int stepIndex = 0; stepIndex < addStepCount; stepIndex++)
							{
								newStep = StepWrapper.GenerateNewStep(getSelectedLightStepTemplate(lightIndex), mode);
								changeStepFromMaterial(materialAst.TongdaoList, stepIndex, sameTDIndexList, newStep);
								getSelectedLightStepWrapper(lightIndex).InsertStep(getSelectedLightStepWrapper(lightIndex).CurrentStep - 1, newStep, false);
							}
						}
					}
				}

				if (isSyncMode) {
					foreach (int lightIndex in getNotSelectedIndices())
					{
						for (int stepIndex = 0; stepIndex < addStepCount; stepIndex++)
						{
							newStep = StepWrapper.GenerateNewStep(getSelectedLightCurrentStepWrapper(lightIndex), mode);
							getSelectedLightStepWrapper(lightIndex).InsertStep(getSelectedLightStepWrapper(lightIndex).CurrentStep - 1, newStep, false);
						}
					}
				}

				RefreshStep();
			}
			// 选择覆盖时的操作：后插法
			//（当前步也要被覆盖，除非没有当前步-》totalStep == currentStep == 0）
			else
			{
				int finalStep = (currentStep - 1) + addStepCount;// finalStep为覆盖后最后一步的序列，而非所有步的数量

				if (finalStep > MAX_STEP)
				{
					MessageBox.Show("素材步数超过当前模式剩余步数，无法调用；可选择其他位置覆盖");
					return;
				}

				StepWrapper stepTemplate = getCurrentStepTemplate();
				IList<MaterialIndexAst> sameTDIndexList = getSameTDIndexList(materialAst.TdNameList, stepTemplate.TongdaoList);
				if (sameTDIndexList.Count == 0)
				{
					MessageBox.Show("该素材与当前灯具不匹配，无法调用");
					return;
				}

				//覆盖的核心逻辑：
				// 方法1：先找出已有的数据改造之；若没有则添加。实现比较复杂，需考虑多方面情况，不采用。
				// 方法2：①比对 finalStep（currentStep+addStepCount)  和 totalStep值，
				//					若finalStep <=totalStep,无需处理
				//					若finalStep > totalStep,则需addStep，直到totalStep=finalStep
				//				②取出currentStep到finalStep的所有步数（addStepCount数)，用changeStepFromMaterial取代之。
				StepWrapper newStep = null;
				if (finalStep > totalStep)   // （超过总步数的那些步，则需要添加新步,以凑满步数）
				{
					for (int i = 0; i < finalStep - totalStep; i++)
					{
						newStep = StepWrapper.GenerateNewStep(stepTemplate, mode);
						lsWrapper.AddStep(newStep);
					}
				}

				// 在步数都已经存在的情况下，用素材替换掉相关步（相应通道）
				for (int stepIndex = currentStep - 1, materialStepIndex = 0; stepIndex < finalStep; stepIndex++, materialStepIndex++)
				{
					changeStepFromMaterial(materialAst.TongdaoList, materialStepIndex, sameTDIndexList, lsWrapper.StepWrapperList[stepIndex]);
					//newStep = lsWrapper.StepWrapperList[stepIndex];
				}

				if (isMultiMode)
				{
					foreach (int lightIndex in selectedIndices)
					{
						if (lightIndex != selectedIndex)
						{
							if (finalStep > totalStep)   // （超过总步数的那些步，则需要添加新步,以凑满步数）
							{
								for (int i = 0; i < finalStep - totalStep; i++)
								{
									newStep = StepWrapper.GenerateNewStep(getSelectedLightStepTemplate(lightIndex), mode);
									getSelectedLightStepWrapper(lightIndex).AddStep(newStep);
								}
							}
							// 在步数都已经存在的情况下，用素材替换掉相关步（相应通道）
							for (int stepIndex = currentStep - 1, materialStepIndex = 0; stepIndex < finalStep; stepIndex++, materialStepIndex++)
							{
								changeStepFromMaterial(materialAst.TongdaoList, materialStepIndex, sameTDIndexList, getSelectedLightSelectedStepWrapper(lightIndex, stepIndex));
							}
						}
					}
				}


				if (isSyncMode) {
					foreach (int lightIndex in getNotSelectedIndices())
					{
						for (int i = 0; i < finalStep - totalStep; i++)
						{
							// 只有超过了当前步数，才需要addStep,故取当前步或最大步皆可。
							newStep = StepWrapper.GenerateNewStep(getSelectedLightCurrentStepWrapper(lightIndex), mode);
							getSelectedLightStepWrapper(lightIndex).AddStep(newStep);
						}
					}
				}

				chooseStep(finalStep);  // 此处不适用RefreshStep()，因为有些情况下，并没有改变currentStep，此时用refreshStep无效。但相应的，因为计算公式不同，chooseStep反而有效。
			}
		}

		/// <summary>
		///  辅助方法：通过比对tongdaoList 和 素材的所有通道名,获取相应的同名通道的列表(MaterialIndexAst)
		/// </summary>
		/// <param name="materialTDNameList"></param>
		/// <param name="tongdaoList"></param>
		/// <returns></returns>
		protected IList<MaterialIndexAst> getSameTDIndexList(IList<string> materialTDNameList, IList<TongdaoWrapper> tongdaoList)
		{
			IList<MaterialIndexAst> sameTDIndexList = new List<MaterialIndexAst>();
			for (int materialTDIndex = 0; materialTDIndex < materialTDNameList.Count; materialTDIndex++)
			{
				for (int currentTDIndex = 0; currentTDIndex < tongdaoList.Count; currentTDIndex++)
				{
					if (materialTDNameList[materialTDIndex].Equals(tongdaoList[currentTDIndex].TongdaoName))
					{
						sameTDIndexList.Add(new MaterialIndexAst()
						{
							MaterialTDIndex = materialTDIndex,
							CurrentTDIndex = currentTDIndex
						});
					}
				}
			}
			return sameTDIndexList;
		}



		/// <summary>
		/// 辅助方法：用传进来的素材数据，重新包装StepWrapper
		/// </summary>
		/// <param name="materialTongdaoList"></param>
		/// <param name="sameTDIndexList"></param>
		/// <param name="stepWrapper"></param>
		protected void changeStepFromMaterial(TongdaoWrapper[,] materialTongdaoList, int stepIndex,
				IList<MaterialIndexAst> sameTDIndexList, StepWrapper stepWrapper)
		{
			foreach (MaterialIndexAst mia in sameTDIndexList)
			{
				int currentTDIndex = mia.CurrentTDIndex;
				int materialTDIndex = mia.MaterialTDIndex;
				stepWrapper.TongdaoList[currentTDIndex].ScrollValue = materialTongdaoList[stepIndex, materialTDIndex].ScrollValue;
				stepWrapper.TongdaoList[currentTDIndex].ChangeMode = materialTongdaoList[stepIndex, materialTDIndex].ChangeMode;
				stepWrapper.TongdaoList[currentTDIndex].StepTime = materialTongdaoList[stepIndex, materialTDIndex].StepTime;
			}
		}

		#endregion

		/// <summary>
		/// 辅助方法：单(多)灯单步发送DMX512帧数据
		/// </summary>
		protected virtual void oneStepWork()
		{
			// 未连接的情况下，无法发送数据。 
			if (!isConnected)
			{
				MessageBox.Show("请先连接设备");
				return;
			}

			byte[] stepBytes = new byte[512];
			// 若选择了《保持其他灯》状态，只需使用此通用代码即可(遍历所有灯具的当前步，取出其数据，放到数组中）；
			if (isKeepOtherLights || isSyncMode)
			{
				if (lightWrapperList != null)
				{
					for (int lightIndex = 0; lightIndex < lightWrapperList.Count; lightIndex++)
					{
						StepWrapper stepWrapper = getSelectedStepWrapper(lightIndex);
						if (stepWrapper != null)
						{
							foreach (TongdaoWrapper td in stepWrapper.TongdaoList)
							{
								int tongdaoIndex = td.Address - 1;
								stepBytes[tongdaoIndex] = (byte)(td.ScrollValue);
							}
						}
					}
				}
			}
			else {
				// 多灯单步				
				if (isMultiMode)
				{
					int currentStep = getCurrentStep();
					if (currentStep == 0)
					{
						MessageBox.Show("当前多灯编组未选中可用步，无法播放！");
						return;
					}
					foreach (int lightIndex in selectedIndices)
					{
						// 取出所有编组灯具的当前步数据。
						StepWrapper stepWrapper = getSelectedLightStepWrapper(lightIndex).StepWrapperList[currentStep - 1];
						if (stepWrapper != null)
						{
							foreach (TongdaoWrapper td in stepWrapper.TongdaoList)
							{
								int tongdaoIndex = td.Address - 1;
								stepBytes[tongdaoIndex] = (byte)(td.ScrollValue);
							}
						}
					}
				}
				// 单灯单步
				else
				{
					StepWrapper stepWrapper = getCurrentStepWrapper();
					if (stepWrapper == null)
					{
						MessageBox.Show("当前灯具未选中可用步，无法播放！");
						return;
					}
					else
					{
						foreach (TongdaoWrapper td in stepWrapper.TongdaoList)
						{
							int tongdaoIndex = td.Address - 1;
							stepBytes[tongdaoIndex] = (byte)(td.ScrollValue);
						}
					}
				}
			}

			playTools.OLOSView(stepBytes);
		}

		/// <summary>
		/// 辅助方法：改变tdPanel中的通道值之后（改trackBar或者numericUpDown），更改对应的tongdaoList的值；并根据ifRealTime，决定是否实时调试灯具。	
		/// </summary>
		/// <param name="tdIndex"></param>
		protected void changeScrollValue(int tdIndex, int tdValue)
		{
			// 设tongdaoWrapper的值
			StepWrapper step = getCurrentStepWrapper();
			step.TongdaoList[tdIndex].ScrollValue = tdValue;

			if (isMultiMode) {
				copyValueToAll(tdIndex, WHERE.SCROLL_VALUE, tdValue);
			}

			// 是否实时单灯单步
			if (isConnected && isRealtime)
			{
				oneStepWork();
			}
		}


		#region 获取各种当前（步数、灯具）等的辅助方法

		/// <summary>
		///  辅助方法：获取当前选中的LightWrapper（此灯具全部数据）
		/// </summary>
		/// <returns></returns>
		protected LightWrapper getCurrentLightWrapper()
		{
			return getSelectedLightWrapper(selectedIndex);
		}

		/// <summary>
		///  辅助方法：获取指定灯LightWrapper
		/// </summary>
		/// <param name="lightIndex"></param>
		/// <returns></returns>
		protected LightWrapper getSelectedLightWrapper(int lightIndex) {

			// 说明尚未点击任何灯具 或 内存内还没有任何灯具
			if (lightIndex == -1 || lightWrapperList == null || lightWrapperList.Count == 0)
			{
				return null;
			}
			return lightWrapperList[lightIndex];
		}

		/// <summary>
		///  辅助方法：取出当前灯具(frame、mode)的所有步数集合
		/// </summary>
		/// <returns></returns>
		protected LightStepWrapper getCurrentLightStepWrapper()
		{
			return getSelectedLightStepWrapper(selectedIndex);
		}

		/// <summary>
		/// 辅助方法：取出选中灯具的当前F/M的所有步数据
		/// </summary>
		/// <param name="selectedIndex"></param>
		/// <returns></returns>
		protected LightStepWrapper getSelectedLightStepWrapper(int lightIndex)
		{
			if (lightIndex < 0) {
				return null;
			}
			LightWrapper lightWrapper = lightWrapperList[lightIndex];
			if (lightWrapper == null) {
				return null;
			}
			else
			{
				//若为空，则立刻创建一个
				if (lightWrapper.LightStepWrapperList[frame, mode] == null)
				{
					lightWrapper.LightStepWrapperList[frame, mode] = new LightStepWrapper()
					{
						StepWrapperList = new List<StepWrapper>()
					};
				};
				return lightWrapper.LightStepWrapperList[frame, mode];
			}
		}

		/// <summary>
		/// 辅助方法：取出选中灯具的当前F/M的所有步数据
		/// </summary>
		/// <param name="selectedIndex"></param>
		/// <returns></returns>
		protected int getSelectedLightStepCounts(int lightIndex)
		{
			if (lightIndex < 0) {
				return 0;
			}
			LightStepWrapper lsWrapper = getSelectedLightStepWrapper(lightIndex);
			if (lsWrapper == null) {
				return 0;
			}
			else {
				return lsWrapper.TotalStep;
			}
		}

		/// <summary>
		/// 辅助方法：直接取出selectedIndex灯在当前F/M的当前步：筛选条件比较苛刻
		/// </summary>
		/// <returns></returns>
		protected StepWrapper getCurrentStepWrapper()
		{
			return getSelectedLightCurrentStepWrapper(selectedIndex);
		}

		/// <summary>
		///  辅助方法：取出指定灯在当前F/M的当前步
		/// </summary>
		/// <param name="lightIndex"></param>
		/// <returns></returns>
		protected StepWrapper getSelectedLightCurrentStepWrapper(int lightIndex)
		{
			LightStepWrapper lsWrapper = getSelectedLightStepWrapper(lightIndex);
			if (lsWrapper != null
				&& lsWrapper.TotalStep != 0
				&& lsWrapper.CurrentStep != 0
				&& lsWrapper.StepWrapperList != null
				&& lsWrapper.StepWrapperList.Count != 0)
			{
				return lsWrapper.StepWrapperList[lsWrapper.CurrentStep - 1];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 辅助方法：获取指定灯当前F/M, 指定步的数据。
		/// </summary>
		/// <param name="lightIndex"></param>
		/// <param name="stepIndex">从0开始算</param>
		/// <returns></returns>
		protected StepWrapper getSelectedLightSelectedStepWrapper(int lightIndex, int stepIndex)
		{
			LightStepWrapper lsWrapper = getSelectedLightStepWrapper(lightIndex);
			if (lsWrapper != null
				&& lsWrapper.TotalStep != 0
				&& stepIndex < lsWrapper.TotalStep
				&& lsWrapper.StepWrapperList != null
				&& lsWrapper.StepWrapperList.Count != 0)
			{
				return lsWrapper.StepWrapperList[stepIndex];
			}
			else {
				return null;
			}
		}

		/// <summary>
		///  获取当前灯具的StepMode，用于还未生成步数时调用
		/// </summary>
		/// <returns></returns>
		protected StepWrapper getCurrentStepTemplate()
		{
			return getSelectedLightStepTemplate(selectedIndex);
		}

		/// <summary>
		///  11.22
		///  辅助方法：取出指定灯具的StepTemplate
		/// </summary>
		/// <param name="lightIndex"></param>
		/// <returns></returns>
		protected StepWrapper getSelectedLightStepTemplate(int lightIndex)
		{
			LightWrapper lightWrapper = getSelectedLightWrapper(lightIndex);
			if (lightWrapper != null)
			{
				return lightWrapper.StepTemplate; }
			else
			{
				return null;
			}
		}





		/// <summary>
		///  辅助方法：取出当前LightStepWrapper的currentStep值
		/// </summary>
		/// <returns></returns>
		protected int getCurrentStep()
		{
			LightStepWrapper lsWrapper = getCurrentLightStepWrapper();
			if (lsWrapper != null)
			{
				return lsWrapper.CurrentStep;
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		///  辅助方法：取出当前步的totalStep值
		/// </summary>
		/// <returns></returns>
		protected int getTotalStep()
		{
			LightStepWrapper lsWrapper = getCurrentLightStepWrapper();
			if (lsWrapper != null)
			{
				return lsWrapper.TotalStep;
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		///  8.15新增的
		///  辅助方法：取出当前灯在该场景模式下的最后一步数据，（用于追加步）
		/// </summary>
		/// <returns></returns>
		protected StepWrapper getCurrentLightLastStepWrapper()
		{
			LightStepWrapper lsWrapper = getCurrentLightStepWrapper();
			if (lsWrapper == null) {
				return null;
			}
			int totalStep = getTotalStep();
			if (totalStep == 0)
			{
				return null;
			}
			else
			{
				return lsWrapper.StepWrapperList[totalStep - 1];
			}
		}

		/// <summary>
		///  11.22 新增方法 
		///  辅助方法：取出选中灯在该场景模式下的最后一步数据，（用于追加步）
		/// </summary>
		/// <returns></returns>
		protected StepWrapper getSelectedLightLastStepWrapper(int lightIndex)
		{
			LightStepWrapper lsWrapper = getSelectedLightStepWrapper(lightIndex);
			if (lsWrapper == null)
			{
				return null;
			}
			int totalStep = lsWrapper.TotalStep;
			if (totalStep == 0)
			{
				return null;
			}
			else
			{
				return lsWrapper.StepWrapperList[totalStep - 1];
			}
		}

		/// <summary>
		/// 辅助方法：获取指定灯具当前F/M 的当前步数据（需要与totalStep对比进行判断）
		/// </summary>
		/// <param name="light"></param>
		/// <returns></returns>
		protected StepWrapper getSelectedStepWrapper(int lightIndex)
		{
			LightWrapper lightWrapper = lightWrapperList[lightIndex];
			if (lightWrapper == null || lightWrapper.LightStepWrapperList[frame, mode] == null) {
				return null;
			}

			int currentStep = lightWrapper.LightStepWrapperList[frame, mode].CurrentStep;
			int totalStep = lightWrapper.LightStepWrapperList[frame, mode].TotalStep;

			// 当前步或最大步某一种为0的情况下，返回null
			if (currentStep == 0 || totalStep == 0)
			{
				return null;
			}
			else
			{
				return lightWrapper.LightStepWrapperList[frame, mode].StepWrapperList[currentStep - 1];
			}
		}

		#endregion


		#region 窗体相关方法：退出、
		/// <summary>
		///  辅助方法：彻底退出程序
		/// </summary>
		protected void exit()
		{
			System.Environment.Exit(0);
		}

		#endregion


		/// <summary>
		/// 枚举类型：《多步(多通道)调节》参数的一种
		/// </summary>
		public enum WHERE
		{
			SCROLL_VALUE, CHANGE_MODE, STEP_TIME, ALL
		}

		/// <summary>
		///  辅助方法：供《多步(多通道)调节》使用
		/// </summary>
		/// <param name="indexList"></param>
		/// <param name="startStep"></param>
		/// <param name="endStep"></param>
		/// <param name="where"></param>
		/// <param name="commonValue"></param>
		public void SetMultiStepValues(WHERE where, IList<int> tdIndexList, int startStep, int endStep, int commonValue) {

			// 多灯模式，将值赋给每个编组的灯具中
			if (isMultiMode)
			{
				foreach (int lightIndex in selectedIndices)
				{
					LightStepWrapper lsWrapper = getSelectedLightStepWrapper(lightIndex);
					for (int stepIndex = startStep - 1; stepIndex < endStep; stepIndex++)
					{
						lsWrapper.StepWrapperList[stepIndex].MultiChangeValue(where, tdIndexList, commonValue);
					}
				}
			} // 单灯模式，则只需更改当前灯具的数据即可。
			else {
				LightStepWrapper lightStepWrapper = getCurrentLightStepWrapper();
				for (int stepIndex = startStep - 1; stepIndex < endStep; stepIndex++)
				{
					lightStepWrapper.StepWrapperList[stepIndex].MultiChangeValue(where, tdIndexList, commonValue);
				}
			}
			// 刷新当前tdPanels数据。
			RefreshStep();
		}

		//MARK：RefreshStep()
		/// <summary>
		/// 辅助方法：刷新当前步;
		/// MARK：MainFormInterface.RefreshStep():不一定使用chooseStep方法 
		/// </summary>
		public void RefreshStep()
		{
			chooseStep(getCurrentStep());
		}

		/// <summary>
		/// 辅助方法：在《在全局配置》中改变了时间因子并保存后，mainForm的时间因子变量也跟着改变，同时刷新当前步
		/// </summary>
		public void ChangeEachStepTime(int eachStepTime) {
			this.eachStepTime = eachStepTime;
			RefreshStep();
		}

		/// <summary>
		/// 9.16 辅助方法：进入多灯模式
		///		1.取出选中的组长，
		///		2.使用组长数据，替代其他灯具（在该F/M）的所有步数集合。
		/// </summary>
		/// <param name="groupSelectedIndex"></param>
		public virtual void EnterMultiMode(int groupSelectedIndex, bool isCopyAll)
		{
			this.isCopyAll = isCopyAll;
			doMultiMode(groupSelectedIndex);
			RefreshStep();
		}

		/// <summary>
		/// 辅助方法：多灯模式中，利用此方法①设好组长，将selectedIndex设成组长；②若isCopyAll，则将组长所有（当前F/M）数据，赋给所有的组员。
		/// </summary>
		/// <param name="groupSelectedIndex"></param>
		protected void doMultiMode(int groupSelectedIndex) {

			// groupSelectedIndex是几个选中的索引中的顺序;用下列语句取出组长，并设为Form中的selectedIndex
			selectedIndex = selectedIndices[groupSelectedIndex];
			if (isCopyAll) {
				LightStepWrapper mainLSWrapper = getSelectedLightStepWrapper(selectedIndex); //取出组长
				foreach (int index in selectedIndices)
				{
					//通过组长生成相关的数据
					StepWrapper currentStepTemplate = lightWrapperList[index].StepTemplate;
					lightWrapperList[index].LightStepWrapperList[frame, mode] = LightStepWrapper.GenerateLightStepWrapper(mainLSWrapper, currentStepTemplate, mode);
				}
			}
		}

		/// <summary>
		/// 辅助方法：多灯模式中，利用此方法，将修改不多的组长数据（如部分通道值、渐变方式、步时间等），用此改动较少的方法，赋给所有的组员
		/// </summary>
		/// <param name="groupSelectedIndex"></param>
		protected void copyValueToAll(int tdIndex, WHERE where, int value)
		{
			LightStepWrapper mainLSWrapper = getCurrentLightStepWrapper(); //取出组长
			int currentStep = getCurrentStep();     // 取出组长的当前步
			foreach (int index in selectedIndices)
			{
				if (getSelectedLightStepWrapper(index).StepWrapperList[currentStep - 1] != null) {
					switch (where)
					{
						case WHERE.SCROLL_VALUE:
							getSelectedLightStepWrapper(index).StepWrapperList[currentStep - 1].TongdaoList[tdIndex].ScrollValue = value; break;
						case WHERE.CHANGE_MODE:
							getSelectedLightStepWrapper(index).StepWrapperList[currentStep - 1].TongdaoList[tdIndex].ChangeMode = value; break;
						case WHERE.STEP_TIME:
							getSelectedLightStepWrapper(index).StepWrapperList[currentStep - 1].TongdaoList[tdIndex].StepTime = value; break;
					}
				}
			}
		}

		/// <summary>
		///辅助方法：统一设值的辅助方法
		/// </summary>
		/// <param name="where"></param>
		/// <param name="value"></param>
		protected void copyCommonValueToAll(int stepNum, WHERE where, int value) {

			LightStepWrapper mainLSWrapper = getSelectedLightStepWrapper(selectedIndex); //取出组长			
			int tdCount = getCurrentLightWrapper().StepTemplate.TongdaoList.Count;

			foreach (int index in selectedIndices)
			{
				if (getSelectedLightStepWrapper(index).StepWrapperList[stepNum - 1] != null)
				{
					for (int tdIndex = 0; tdIndex < tdCount; tdIndex++)
					{
						switch (where)
						{
							case WHERE.SCROLL_VALUE:
								getSelectedLightStepWrapper(index).StepWrapperList[stepNum - 1].TongdaoList[tdIndex].ScrollValue = value; break;
							case WHERE.CHANGE_MODE:
								getSelectedLightStepWrapper(index).StepWrapperList[stepNum - 1].TongdaoList[tdIndex].ChangeMode = value; break;
							case WHERE.STEP_TIME:
								getSelectedLightStepWrapper(index).StepWrapperList[stepNum - 1].TongdaoList[tdIndex].StepTime = value; break;
						}
					}
				}
			}
		}

		/// <summary>
		/// 辅助方法：多灯模式中，利用此方法，将当前步的一些《统一设置》的scrollValue值，设为多灯的相关步的值。
		/// </summary>
		protected void copyStepToAll(int stepNum, WHERE where) {

			LightStepWrapper mainLSWrapper = getSelectedLightStepWrapper(selectedIndex); //取出组长
			int tdCount = getCurrentLightWrapper().StepTemplate.TongdaoList.Count;

			foreach (int lightIndex in selectedIndices)
			{
				if (getSelectedLightStepWrapper(lightIndex).StepWrapperList[stepNum - 1] != null)
				{
					for (int tdIndex = 0; tdIndex < tdCount; tdIndex++)
					{
						switch (where)
						{
							case WHERE.SCROLL_VALUE:
								getSelectedLightStepWrapper(lightIndex).StepWrapperList[stepNum - 1].TongdaoList[tdIndex].ScrollValue = mainLSWrapper.StepWrapperList[stepNum - 1].TongdaoList[tdIndex].ScrollValue; break;
							case WHERE.CHANGE_MODE:
								getSelectedLightStepWrapper(lightIndex).StepWrapperList[stepNum - 1].TongdaoList[tdIndex].ChangeMode = mainLSWrapper.StepWrapperList[stepNum - 1].TongdaoList[tdIndex].ChangeMode; break;
							case WHERE.STEP_TIME:
								getSelectedLightStepWrapper(lightIndex).StepWrapperList[stepNum - 1].TongdaoList[tdIndex].StepTime = mainLSWrapper.StepWrapperList[stepNum - 1].TongdaoList[tdIndex].StepTime; break;
							case WHERE.ALL:
								getSelectedLightStepWrapper(lightIndex).StepWrapperList[stepNum - 1].TongdaoList[tdIndex].ScrollValue = mainLSWrapper.StepWrapperList[stepNum - 1].TongdaoList[tdIndex].ScrollValue;
								getSelectedLightStepWrapper(lightIndex).StepWrapperList[stepNum - 1].TongdaoList[tdIndex].ChangeMode = mainLSWrapper.StepWrapperList[stepNum - 1].TongdaoList[tdIndex].ChangeMode;
								getSelectedLightStepWrapper(lightIndex).StepWrapperList[stepNum - 1].TongdaoList[tdIndex].StepTime = mainLSWrapper.StepWrapperList[stepNum - 1].TongdaoList[tdIndex].StepTime;
								break;
						}
					}
				}
			}
		}


		/// <summary>
		///  辅助方法：供MultiLightForm使用，检查当前的所有选中灯具的所有步数，是否一致。--》只需都和第一个灯进行对比，稍有不同，即不通过。
		/// </summary>
		/// <returns></returns>
		public bool CheckSameStepCounts() {
			int firstIndex = selectedIndices[0];
			int firstStepCounts = getSelectedLightStepCounts(firstIndex);
			foreach (int index in selectedIndices)
			{
				int tempStepCounts = getSelectedLightStepCounts(index);
				if (tempStepCounts != firstStepCounts) {
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// 辅助方法：检查是否所有灯具的步数都一致，若有不同，直接返回false
		/// </summary>
		protected bool CheckAllSameStepCounts()
		{
			if (lightAstList == null || lightAstList.Count == 0) {
				MessageBox.Show("当前工程无灯具，检查灯具步数是否一致的操作无意义。");
				return false;
			}

			int firstStepCounts = getSelectedLightStepCounts(0);
			for (int lightIndex = 1; lightIndex < lightAstList.Count; lightIndex++)
			{
				int tempStepCounts = getSelectedLightStepCounts(lightIndex);
				if (tempStepCounts != firstStepCounts)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// 辅助方法：显示每个灯具的当前步和最大步 
		/// </summary>
		protected void showAllLightCurrentAndTotalStep()
		{
			foreach (LightWrapper item in lightWrapperList)
			{
				if (item.LightStepWrapperList[frame, mode] != null)
				{
					Console.WriteLine(item.StepTemplate.LightFullName + ":" + item.LightStepWrapperList[frame, mode].CurrentStep + "/" + item.LightStepWrapperList[frame, mode].TotalStep);
				}
			}
		}

		/// <summary>
		/// 辅助方法：获取当前工程未选中灯具=》主动判断是多灯还是单灯模式。
		/// </summary>
		/// <returns></returns>
		protected IList<int> getNotSelectedIndices()
		{
			IList<int> allIndices = new List<int>();
			for (int lightIndex = 0; lightIndex < lightWrapperList.Count; lightIndex++)
			{
				if (isMultiMode)
				{
					if (!selectedIndices.Contains(lightIndex))
						allIndices.Add(lightIndex);
				}
				else {
					if (selectedIndex != lightIndex) {
						allIndices.Add(lightIndex);
					}
				}
			}
			return allIndices;
		}

		/// <summary>
		/// 辅助方法：多步粘贴时，使用此方法;
		/// -- 基本思路与使用素材一样,故直接在其代码基础上进行改动
		/// </summary>
		/// <param name="method"></param>
		public void MultiStepPaste(InsertMethod method)
		{
			InsertOrCoverMaterial(TempMaterialAst, method);
		}

		/// <summary>
		///  辅助方法：调用其他场景
		/// </summary>
		/// <param name="text"></param>
		public void UseOtherForm(int selectedFrameIndex)
		{
			//MessageBox.Show(selectedFrameIndex  + " - " + AllFrameList[selectedFrameIndex]); 
			foreach (LightWrapper lightWrapper in lightWrapperList)
			{
				lightWrapper.LightStepWrapperList[frame, mode] = LightStepWrapper.GenerateLightStepWrapper(lightWrapper.LightStepWrapperList[selectedFrameIndex, mode], lightWrapper.StepTemplate, mode);
			}
			ResetSyncMode();
			RefreshStep();
			MessageBox.Show("成功调用场景:" + AllFrameList[selectedFrameIndex]);
		}

		/// <summary>
		/// 辅助：由内存读取 IList<TongdaoWrapper> 拿出相关通道的TongdaoWrapper
		/// </summary>
		public IList<TongdaoWrapper> GetFMTDList(DB_ValuePK pk){

			int selectedLightIndex = lightDictionary[pk.LightIndex];
			int tdIndex = pk.LightID - pk.LightIndex ;			
			IList<TongdaoWrapper> tdList = new List<TongdaoWrapper>();
			if (lightWrapperList[selectedLightIndex].LightStepWrapperList[pk.Frame, pk.Mode] != null
				&& lightWrapperList[selectedLightIndex].LightStepWrapperList[pk.Frame, pk.Mode].StepWrapperList != null) {

				IList<StepWrapper> stepWrapperList = lightWrapperList[selectedLightIndex].LightStepWrapperList[pk.Frame, pk.Mode].StepWrapperList;
				for (int step = 0; step < stepWrapperList.Count; step++)
				{
					tdList.Add(stepWrapperList[step].TongdaoList[tdIndex]);
				}
			}			
			return tdList;
		}

		/// <summary>
		/// 辅助方法：打开《灯库软件》
		/// </summary>
		protected void openLightEditor() {
			try
			{
				System.Diagnostics.Process.Start(Application.StartupPath + @"\LightEditor.exe");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}



		//TODO：SkinMainForm.MakeFrameData() ， 实时填充某一场景的所有数据（可能在某些操作里需要用到）
		/// <summary>
		///  辅助方法：通过场景编号（0-31），来读取数据库相关数据填入虚假空白数据（NewStep 、Flag==0）中，并将Flag设为1
		/// </summary>
		/// <param name="tempFrame"></param>
		//private void MakeFrameData(int tempFrame)
		//{
		//		Thread[] threadArray = new Thread[dbLightList.Count];
		//		for (int lightListIndex = 0; lightListIndex < dbLightList.Count; lightListIndex++)
		//		{
		//			int tempLightIndex = lightListIndex; // 灯具index（lightAstList和lightWrapperList） ； 必须在循环内使用一个临时变量来记录这个index，否则线程运行时lightListIndex会发生变化。
		//			int tempLightNo = dbLightList[tempLightIndex].LightNo;   //记录了数据库中灯具的起始地址（不同灯具有1-32个通道，但只要是同个灯，就公用此LightNo)		
		//			IList<DB_Value> tempDbValueList = valueDAO.GetByLightNoAndFrame(tempLightNo, tempFrame);

		//			threadArray[tempLightIndex] = new Thread(delegate ()
		//			{
		//				Console.WriteLine(tempLightIndex + " ++ （MakeFrameData）线程开始了");
		//				for (int tempMode = 0; tempMode < 1; tempMode++)
		//				{
		//					for (int step = 1; step <= lightWrapperList[tempLightIndex].LightStepWrapperList[tempFrame, mode].StepWrapperList.Count; step++)
		//					{
		//						if (lightWrapperList[tempLightIndex].LightStepWrapperList[tempFrame, mode].StepWrapperList[step - 1].Flag == 0)
		//						{
		//							for (int tdIndex = 0; tdIndex < lightWrapperList[tempLightIndex].LightStepWrapperList[tempFrame, mode].StepWrapperList[step - 1].TongdaoList.Count; tdIndex++)
		//							{
		//								int lightId = lightWrapperList[tempLightIndex].LightStepWrapperList[tempFrame, mode].StepWrapperList[step - 1].TongdaoList[tdIndex].Address;
		//								DB_Value stepValue = tempDbValueList.SingleOrDefault(t => t.PK.LightID == lightId && t.PK.Frame == tempFrame && t.PK.Mode == tempMode && t.PK.Step == step);
		//								if (stepValue != null)
		//								{
		//									lightWrapperList[tempLightIndex].LightStepWrapperList[tempFrame, mode].StepWrapperList[step - 1].TongdaoList[tdIndex].ScrollValue = stepValue.ScrollValue;
		//									lightWrapperList[tempLightIndex].LightStepWrapperList[tempFrame, mode].StepWrapperList[step - 1].TongdaoList[tdIndex].StepTime = stepValue.StepTime;
		//									lightWrapperList[tempLightIndex].LightStepWrapperList[tempFrame, mode].StepWrapperList[step - 1].TongdaoList[tdIndex].ChangeMode = stepValue.ChangeMode;
		//								}
		//							}
		//							lightWrapperList[tempLightIndex].LightStepWrapperList[tempFrame, mode].StepWrapperList[step - 1].Flag = 1;
		//						}
		//					}
		//				}
		//				Console.WriteLine(tempLightIndex + " -- （MakeFrameData）线程结束了");
		//			});
		//			threadArray[tempLightIndex].Start();

		//	}

		//	// 下列代码，用以监视所有线程是否已经结束运行。
		//	while (true)
		//	{
		//		int unFinishedCount = 0;
		//		foreach (var thread in threadArray)
		//		{
		//			unFinishedCount += thread.IsAlive ? 1 : 0;
		//		}

		//		if (unFinishedCount == 0)
		//		{
		//			Console.WriteLine("Dickov:所有线程(MakeFrameData)已结束。");
		//			break;
		//		}
		//		else
		//		{
		//			Thread.Sleep(100);
		//		}
		//	}
		//}

	}
}