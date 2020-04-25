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
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using OtherTools;
using LightEditor.Ast;
using LightController.MyForm.LightList;

namespace LightController.MyForm
{
	public class MainFormBase : System.Windows.Forms.Form,MainFormInterface
	{
		/// <summary>
		/// 枚举类型：《多步(多通道)调节》参数的一种
		/// </summary>
		public enum WHERE
		{
			SCROLL_VALUE,
			CHANGE_MODE,
			STEP_TIME, ALL
		}

		// 全局配置及数据库连接		
		public static int NETWORK_WAITTIME = 1000; //网络搜索时的通用暂停时间
		public string SoftwareName ;	 //动态载入软件名（前半部分）后半部分需自行封装
		public string SavePath; // 动态载入相关的存储目录（开发时放在C:\Temp中；发布时放在应用所在文件夹）
		public bool IsShowTestButton = false;
		public bool IsShowHardwareUpdate = false;
		public bool IsLinkLightEditor = false;
		public bool IsLinkOldTools = false;

		// 几个全局的辅助控件（导出文件、toolTip提示等）
		protected FolderBrowserDialog exportFolderBrowserDialog;
		protected System.ComponentModel.IContainer components;
		protected ToolTip myToolTip;

		// 打开程序时，即需导入的变量
		public static IList<string> AllFrameList; // 将所有场景名称写在此处,并供所有类使用（动态导入场景到此静态变量中）
		public static int FrameCount = 0;  //场景数量
		public static int MAX_StTimes = 254;  //每步 时间因子可乘的 最大倍数 如 0.03s*254= 7.62s ; 应设为常量	-》200331确认为15s=0.03*500	
		protected int MAX_STEP = 100;  //每个场景的最大步数，动态由配置文件在打开软件时读取

		// 辅助的bool变量：	
		protected bool isInit = false;// form都初始化后，才将此变量设为true;为防止某些监听器提前进行监听
		public bool IsCreateSuccess = false;  ///点击新建后，用这个变量决定是否打开灯具编辑列表
		public MaterialAst TempMaterialAst = null;  // 辅助（复制多步、素材）变量 ， 《复制、粘贴多步》时使用

		// 程序运行后，动态变化的变量
		protected string arrangeIniPath = null;  // 打开工程时 顺便把相关的位置保存ini(arrange.ini) 也读取出来（若有的话）
		protected bool isAutoArrange = true; // 默认情况下，此值为true，代表右键菜单“自动排列”默认情况下是打开的。
		protected string binPath = null; // 此处记录《硬件更新》时，选过的xbin文件路径。
		protected string tempProjectPath = null; //此处记录《工程更新》时，选过的文件夹路径。
		protected bool isSyncMode = false;  // 同步模式为true；异步模式为false(默认）	

		protected string currentProjectName;  //存放当前工程名，主要作用是防止当前工程被删除（openForm中）
		protected string projectPath; //存放当前工程所在目录
		public string GlobalIniPath;  // 存放当前工程《全局配置》、《摇麦设置》的配置文件的路径
		protected string dbFilePath; // 数据库地址：每个工程都有自己的db，所以需要一个可以改变的dbFile字符串，存放数据库连接相关信息
		protected bool isEncrypt = false; //是否加密		
		public int eachStepTime = 30; // 默认情况下，步时间默认值为30ms
		public decimal eachStepTime2 = 0.03m; //默认情况下，步时间默认值为0.03s（=30ms）

        //MARK 只开单场景：00.2 ①必须有一个存储所有场景是否需要保存的bool[];②若为true，则说明需要保存
	   	protected bool[] frameSaveArray;
		//MARK 只开单场景：00.3 ①必须有一个存储所有场景数据是否已经由DB载入的bool[];②若为true，则说明不用再从数据库内取数据了
		protected bool[] frameLoadArray;
		//MARK 只开单场景：14.0 必须有一个存储[旧灯具index]的列表，若非列表内的灯具，则应清除相关的DB数据（包括StepCount表及Value表）
		protected IList<int> retainLightIndices ;

		// 数据库DAO(data access object：数据访问对象）
		protected LightDAO lightDAO;
		protected StepCountDAO stepCountDAO;
		protected ValueDAO valueDAO;
		protected FineTuneDAO fineTuneDAO;

		// 这几个IList ，存放着所有数据库数据		
		protected IList<DB_Light> dbLightList;
		protected IList<DB_FineTune> dbFineTuneList;
		protected IList<DB_StepCount> dbStepCountList;
		//protected IList<DB_Value> dbValueList;

		protected IList<LightAst> lightAstList;  //与《灯具编辑》通信用的变量；同时也可以供一些辅助form读取相关灯具的简约信息时使用
		protected IList<LightWrapper> lightWrapperList;// 灯具变量：记录所有灯具（lightWrapper）的（所有场景和模式）的 每一步（通道列表）
		protected Dictionary<int, int> lightDictionary;   //辅助灯具字典，用于通过pk，取出相关灯具的index（供维佳生成数据调用）

		// 通道数据操作时的变量		
		protected bool isMultiMode = false; //默认情况下是单灯模式；若进入多灯模式，此变量改成true；											
		protected bool isCopyAll = false;   // 11.20 新功能：多灯模式仍需要一个变量 ，用以设置是否直接用组长的数据替代组员。（默认情况下应该设为false，可以避免误删步数信息）

		protected int selectedIndex = -1; //选择的灯具的index，默认为-1，如有选中灯具，则改成该灯具的index（在lightAstList、lightWrapperList中）
		protected IList<int> selectedIndices ; //选择的灯具的index列表（多选情况下）
		protected string selectedLightName = ""; //选中的灯具的名字（lightName + lightType）

		protected int currentFrame = 0; // 表示场景编号(selectedIndex )
		protected int currentMode = 0;  // 表示模式编号（selectedIndex)；0.常规模式； 1.音频模式

		protected StepWrapper tempStep = null; //// 辅助步变量：复制及粘贴步时用到		

		// 调试变量
		protected PlayTools playTools = PlayTools.GetInstance(); //DMX512灯具操控对象的实例
		protected bool isConnected = false; // 辅助bool值，当选择《连接设备》后，设为true；反之为false
		protected bool isRealtime = false; // 辅助bool值，当选择《实时调试》后，设为true；反之为false			
		protected bool isKeepOtherLights = false;  // 辅助bool值，当选择《（非调灯具)保持状态》时，设为true；反之为false

		protected string[] comList;  //存储DMX512串口的名称列表，用于comSkinComboBox中
		protected string comName; // 存储打开的DMX512串口名称

		protected bool isConnectCom = true; //默认情况下，用串口连接设备。
		protected ConnectTools connectTools; //连接工具（通用实例：网络及串口皆可用）
		protected IList<IPAst> ipaList; // 此列表存储所有建立连接的ipAst
		protected IPAst selectedIpAst; // 选中的ipast（每个下拉框选中的值）
		protected IList<NetworkDeviceInfo> allNetworkDevices;

		#region 几个纯虚（virtual修饰）方法：主要供各种基类方法向子类回调使用		

		protected virtual void enableProjectRelative(bool enable) { } // 是否显示《保存工程》等
		protected virtual void enableSLArrange(bool enableSave, bool enableLoad) { } //是否显示《 存、取 灯具位置》		
		protected virtual void showPlayPanel(bool visible) { }// 是否显示PlayFlowLayoutPanel
		protected virtual void enableRefreshPic(bool enable) { } // 是否使能《重新加载灯具图片》
		protected virtual void setBusy(bool buzy) { } //设置是否忙时
		protected virtual void editLightInfo(LightAst lightAst) { }  //显示灯具详情到面板中
		protected virtual void enableStepPanel(bool enable) { } //是否使能步数面板
		protected virtual void showTDPanels(IList<TongdaoWrapper> tongdaoList, int startNum) { } //通过传来的数值，生成通道列表的数据
		protected virtual void hideAllTDPanels() { } //隐藏所有通道
		protected virtual void showStepLabel(int currentStep, int totalStep) { } //显示步数标签，并判断stepPanel按钮组是否可用
		protected virtual void connectButtonClick() { }//点击连接按钮，但需子类实现
		protected virtual void initStNumericUpDowns() { }  // 初始化工程时，需要初始化其中的步时间控件的参数值		
		protected virtual void changeCurrentFrame(int frameIndex) { } //MARK 只开单场景：02.0 改变当前Frame
		protected virtual void enableSingleMode(bool enable) { }  //退出多灯模式或单灯模式后的相关操作

		public virtual void EnterSyncMode(bool isSyncMode){} // 设置是否 同步模式
		public virtual void SetNotice(string notice){} //设置提示信息
		public virtual void EnableConnectedButtons(bool connected){} //设置《连接按钮组》是否可用

		#endregion			   

		#region 存储一些供其他Form使用的变量，比如已打开的升级文件、工程文件等

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
			tempProjectPath = projectPath;
		}

		#endregion

		/// <summary>
		/// 辅助方法：以当前打开的工程信息，生成源文件（Source->工程文件夹、LightLibrary）；
		/// </summary>
		public bool GenerateSourceProject()
		{			
			try
			{
				//若存在Source文件夹，则先删除
				DirectoryInfo di = new DirectoryInfo(SavePath + @"\Source");
				if (di.Exists)
				{
					di.Delete(true);
				}			
				// 删除后直接创建下一级目录，并拷贝相关目录
				string destPath = SavePath + @"\Source\LightProject\"+currentProjectName;
				di = new DirectoryInfo(destPath);
				di.Create();
				DirectoryAst.CopyDirectory(projectPath, destPath);

				if (lightAstList != null && lightAstList.Count > 0) {
					string lightLibPath = SavePath + @"\Source\LightLibrary";
					di = new DirectoryInfo(lightLibPath);
					di.Create();

					HashSet<string> lightSet = new HashSet<string>();
					HashSet<string> dirSet = new HashSet<string>();
					foreach (LightAst la in lightAstList)
					{
						dirSet.Add(la.LightName);
						lightSet.Add(la.LightName + "\\" + la.LightType+".ini" );
					}

					foreach (string libDir in dirSet)
					{
						di = new DirectoryInfo(SavePath + @"\Source\LightLibrary\"+libDir);
						di.Create();
					}
					foreach (string lightPath in lightSet) {
						File.Copy(SavePath + @"\LightLibrary\" + lightPath, SavePath + @"\Source\LightLibrary\" + lightPath, true);
					}	
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);				
				return false;
			}		
			return true;
		}

		/// <summary>
		///  辅助方法：判断是否可以显示 playPanel及 刷新图片(主要供《打开工程》和《灯具列表Form》使用），故可以在BuildLightList中使用
		///  --这两个功能都依赖于当前Form中的lightAstList是否为空。
		/// </summary>
		protected void autosetEnabledPlayAndRefreshPic()
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
				lightAstList.Add(LightAst.GenerateLightAst(light,SavePath));
			}
			return lightAstList;
		}

		/// <summary>
		/// 辅助方法（虚）：添加lightAst列表到主界面内存中,主要供 LightsForm调用（以及OpenProject调用）
		/// </summary>
		public virtual void BuildLightList(IList<LightAst> lightAstList2)
		{
			List<LightWrapper> lightWrapperList2 = new List<LightWrapper>();
			//MARK 只开单场景：14.1 AddLightAstList()方法体内，对retainLightIndices进行初始化
			retainLightIndices = new List<int>();

			for (int i = 0; i < lightAstList2.Count; i++)			{
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
							//MARK 只开单场景：14.2 AddLightAstList()方法体内，为retainLightIndices添加旧灯具的数据
							retainLightIndices.Add(lightAstList2[i].StartNum);
							break;
						}
					}
				}
				if (!addOld)
				{
					//Console.WriteLine("Dickov : 添加了一个全新的LightWrapper["  + lightAstList2[i].LightName + ":" + lightAstList2[i].LightType + "(" + lightAstList2[i].LightAddr+ ")]，但还没有生成StepTemplate。");
					lightWrapperList2.Add(new LightWrapper() {	StepTemplate = generateStepTemplate(lightAstList2[i]) });
				}
			}
			
			lightAstList = new List<LightAst>(lightAstList2);
			lightWrapperList = new List<LightWrapper>(lightWrapperList2);
			lightDictionary = new Dictionary<int, int>();
			for (int i = 0; i < lightAstList.Count; i++)
			{
				lightDictionary.Add( lightAstList[i].StartNum , i );				
			}	

			//MARK 只开单场景：15.0 AddLightAstList时，一定要清空selectedIndex及selectedIndices,否则若删除了灯具，则一定会出问题！
			selectedIndex = -1;
			selectedIndices = new List<int>(); 
			RefreshStep();

			EnterSyncMode(false);
			enableProjectRelative(true);			
			autosetEnabledPlayAndRefreshPic();
		}
			   
		/// <summary>
		/// 辅助方法：生成模板Step --》 之后每新建一步，都复制模板step的数据。
		/// </summary>
		/// <param name="lightAst"></param>
		/// <param name="lightIndex"></param>
		/// <returns></returns>
		protected StepWrapper generateStepTemplate(LightAst lightAst)
		{
			Console.WriteLine("Dickov :为 " + lightAst.LightName + ":" + lightAst.LightType + "(" + lightAst.LightAddr + ")生成模板文件(StepTemplate)：" );			
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
					}
					else
					{
						int tongdaoCount = int.Parse(lineList[3].ToString().Substring(6));

						int tongdaoCount2 = (lineCount - 6) / 3;						
						if (tongdaoCount2 < tongdaoCount)
						{							
							throw new Exception("打开的灯库文件的count值与实际值不符合，无法生成StepTemplate");
						}

						List<TongdaoWrapper> tongdaoList = new List<TongdaoWrapper>();
						IniFileAst iniAst = new IniFileAst(lightAst.LightPath);
						lightAst.SawList = new List<SAWrapper>();

						for (int tdIndex = 0; tdIndex < tongdaoCount; tdIndex++)
						{
							string tongdaoName = lineList[3 * tdIndex + 6].ToString().Substring(4);
							int initNum = int.Parse(lineList[3 * tdIndex + 7].ToString().Substring(4));
							int address = int.Parse(lineList[3 * tdIndex + 8].ToString().Substring(4));

							//MARK 200325 生成模板数据时，取出子属性的列表（显示各个子属性数值）					
							string remark = tongdaoName + "\n";
							IList<SA> saList = new List<SA>();
							for (int saIndex = 0; saIndex < iniAst.ReadInt("sa", tdIndex + "_saCount", 0); saIndex++)
							{								
								string saName = IniFileAst_UTF8.ReadString(lightAst.LightPath, "sa", tdIndex + "_" + saIndex + "_saName", "");
								int startValue = iniAst.ReadInt("sa", tdIndex + "_" + saIndex + "_saStart", 0);
								int endValue = iniAst.ReadInt("sa", tdIndex + "_" + saIndex + "_saEnd", 0);								
								remark += saName + " : " + startValue +" - " + endValue +"\n";
								saList.Add(new SA() { SAName = saName, StartValue = startValue, EndValue = endValue });
							}
							lightAst.SawList.Add(new SAWrapper() { SaList = saList });

							tongdaoList.Add(new TongdaoWrapper()
							{
								TongdaoName = tongdaoName,
								ScrollValue = initNum,
								StepTime = 66,
								ChangeMode = -1,
								Address = lightAst.StartNum + (address - 1),
								//MARK 200325 生成模板数据时，加入备注（显示各个子属性数值）
								Remark = remark
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
			// 由内存几个List实时生成
			else
			{
				// 先生成最新的 dbLightList,dbStepCountList, dbValueList 数据
				generateDBLightList();
				generateDBFineTuneList();
				//MARK 只开单场景：12.0 GetDBWrapper中，重写generateDBStepCountList(); 
				generateDBStepCountList();
				IList<DB_Value> dbValueListTemp = generateDBValueList(currentFrame);

				DBWrapper allData = new DBWrapper(dbLightList, dbStepCountList, dbValueListTemp, dbFineTuneList);
				return allData;
			}
		}

		#endregion

		#region 由界面数据生成db数据的方法（但这些db数据也还放在内存中，需要各个save方法才能）

		/// <summary>
		/// 辅助方法：由内存的lightAstList生成最新的dbLightList
		/// </summary>
		protected void generateDBLightList() {
			dbLightList = new List<DB_Light>();
			if (lightAstList == null || lightAstList.Count == 0)
			{
				return;
			}

			foreach (LightAst la in lightAstList)
			{
				DB_Light light = DB_Light.GenerateLight(la);
				dbLightList.Add(light);
			}			
		}

		/// <summary>
		/// 辅助方法：由lightWrapperList生成最新的dbFineTuneList;
		/// </summary>
		private void generateDBFineTuneList()
		{
			dbFineTuneList = new List<DB_FineTune>(); // 每次更新为最新数据
			if (lightAstList == null || lightAstList.Count == 0) {
				return;
			}

			// 遍历lightWrapperList的模板数据，用以读取相关的通道名称，才能加以处理			
			foreach (LightWrapper lightWrapper in lightWrapperList)
			{
				StepWrapper stepTemplate = lightWrapper.StepTemplate;
				if (stepTemplate != null && stepTemplate.TongdaoList != null)
				{
					int xz = 0, xzwt = 0, xzValue = 0, yz = 0, yzwt = 0, yzValue = 0;
					foreach (TongdaoWrapper td in stepTemplate.TongdaoList)
					{
						switch (td.TongdaoName.Trim())
						{
							case "X轴": xz = td.Address; break;
							case "X轴微调": xzwt = td.Address; xzValue = td.ScrollValue; break;
							case "Y轴": yz = td.Address; break;
							case "Y轴微调": yzwt = td.Address; yzValue = td.ScrollValue; break;
						}
					}
					if (xz != 0 && xzwt != 0)
					{
						dbFineTuneList.Add(new DB_FineTune() { MainIndex = xz, FineTuneIndex = xzwt, MaxValue = xzValue });
					}
					if (yz != 0 && yzwt != 0)
					{
						dbFineTuneList.Add(new DB_FineTune() { MainIndex = yz, FineTuneIndex = yzwt, MaxValue = yzValue });
					}
				}
			}
		}

		/// <summary>
		/// 辅助方法：由lightWrapperList生成最新的dbStepCountList，都放在内存中
		/// </summary>
		protected void generateDBStepCountList()
		{
			// 保存所有步骤前，先清空stepCountList
			dbStepCountList = new List<DB_StepCount>();
			if (lightAstList == null || lightAstList.Count == 0)
			{
				return;
			}
			
			// 取出每个灯具的所有【非null】stepCount,填入stepCountList中
			foreach (LightWrapper lightTemp in lightWrapperList)
			{
				DB_Light light = dbLightList[lightWrapperList.IndexOf(lightTemp)];
				LightStepWrapper[,] allLightStepWrappers = lightTemp.LightStepWrapperList;

				//MARK 只开单场景：12.1 generateDBStepCountLit()的重写实现
				// 取出灯具的每个常规场景，并将它们保存起来（但若为空，则不保存）
				for (int frameIndex = 0; frameIndex < FrameCount; frameIndex++)
				{
					for (int mode = 0; mode < 2; mode++)
					{
						DB_StepCountPK stepCountPK = new DB_StepCountPK()
						{
							Frame = frameIndex,
							Mode = mode,
							LightIndex = light.LightNo
						};

						//MARK 只开单场景：12.2 generateDBStepCountLit()重写：加载过的用内存数据
						if (frameLoadArray[frameIndex])
						{
							LightStepWrapper lsTemp = allLightStepWrappers[frameIndex, mode];
							//MARK 只开单场景：12.2.补 generateDBStepCountLit()重写：若加载过的场景，此灯具并未被选中过，则其lsTemp为空！
							//	此时可能最终会传一个Count =0 的 dbStepCountList，而使用这种列表，程序会卡住，故需要处理
							if (lsTemp != null)
							{
								DB_StepCount stepCount = new DB_StepCount()
								{
									StepCount = lsTemp.TotalStep,
									PK = stepCountPK
								};
								dbStepCountList.Add(stepCount);
							}
							//MARK 只开单场景：12.2.补2 generateDBStepCountLit()重写：若加载过的场景，此灯具并未被选中过，则其lsTemp为空,此时不需要添加dbStepCount数据
							//else {
							//	DB_StepCount stepCount = new DB_StepCount()
							//	{
							//		StepCount =0,
							//		PK = stepCountPK
							//	};
							//	dbStepCountList.Add(stepCount);
							//}									
						}
						//MARK 只开单场景：12.3 generateDBStepCountLit()重写：未加载过的用DB数据
						else
						{
							DB_StepCount sc = stepCountDAO.GetStepCountByPK(stepCountPK);
							if (sc != null)
							{
								dbStepCountList.Add(sc);
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
			IList<DB_Value> tempValueList = new List<DB_Value>();
			if (lightAstList != null && lightAstList.Count > 0)
			{
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
									tempValueList.Add(valueTemp);
								}
							}
						}
					}
				}
			}
			return tempValueList;
		}

		#endregion			

		#region 素材相关

		/// <summary>
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
					newStep = StepWrapper.GenerateNewStep(stepTemplate, currentMode);
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
								newStep = StepWrapper.GenerateNewStep(getSelectedLightStepTemplate(lightIndex), currentMode);
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
							newStep = StepWrapper.GenerateNewStep(getSelectedLightCurrentStepWrapper(lightIndex), currentMode);
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
						newStep = StepWrapper.GenerateNewStep(stepTemplate, currentMode);
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
									newStep = StepWrapper.GenerateNewStep(getSelectedLightStepTemplate(lightIndex), currentMode);
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
							newStep = StepWrapper.GenerateNewStep(getSelectedLightCurrentStepWrapper(lightIndex), currentMode);
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
							//Console.WriteLine(td.ScrollValue);
							stepBytes[tongdaoIndex] = (byte)(td.ScrollValue);
						}
					}
				}
			}

			playTools.OLOSView(stepBytes);
			SetNotice("正在实时调试当前步");
		}

		/// <summary>
		/// 辅助方法：改变tdPanel中的通道值之后（改trackBar或者numericUpDown），更改对应的tongdaoList的值；并根据ifRealTime，决定是否实时调试灯具。	
		/// </summary>
		/// <param name="tdIndex"></param>
		protected void changeScrollValue(int tdIndex, int tdValue)
		{
			//Console.WriteLine("changeScrollValue");
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

			if (lightWrapperList == null || lightWrapperList.Count == 0) {
				return null;
			}			

			LightWrapper lightWrapper = lightWrapperList[lightIndex];
			if (lightWrapper == null) {
				return null;
			}
			else
			{
				//若为空，则立刻创建一个
				if (lightWrapper.LightStepWrapperList[currentFrame, currentMode] == null)
				{
					lightWrapper.LightStepWrapperList[currentFrame, currentMode] = new LightStepWrapper()
					{
						StepWrapperList = new List<StepWrapper>()
					};
				};
				return lightWrapper.LightStepWrapperList[currentFrame, currentMode];
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
		///  辅助方法：获取当前灯具的StepMode，用于还未生成步数时调用
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
			if (lightWrapper == null || lightWrapper.LightStepWrapperList[currentFrame, currentMode] == null) {
				return null;
			}

			int currentStep = lightWrapper.LightStepWrapperList[currentFrame, currentMode].CurrentStep;
			int totalStep = lightWrapper.LightStepWrapperList[currentFrame, currentMode].TotalStep;

			// 当前步或最大步某一种为0的情况下，返回null
			if (currentStep == 0 || totalStep == 0)
			{
				return null;
			}
			else
			{
				return lightWrapper.LightStepWrapperList[currentFrame, currentMode].StepWrapperList[currentStep - 1];
			}
		}

		#endregion
		
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
				
		/// <summary>
		/// 辅助方法：刷新当前步;
		/// </summary>
		public void RefreshStep()
		{
			chooseStep(getCurrentStep());
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
					lightWrapperList[index].LightStepWrapperList[currentFrame, currentMode] = LightStepWrapper.GenerateLightStepWrapper(mainLSWrapper, currentStepTemplate, currentMode);
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
		protected void copyUnifyValueToAll(int stepNum, WHERE where, int value) {

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
				if (item.LightStepWrapperList[currentFrame, currentMode] != null)
				{
					Console.WriteLine(item.StepTemplate.LightFullName + ":" + item.LightStepWrapperList[currentFrame, currentMode].CurrentStep + "/" + item.LightStepWrapperList[currentFrame, currentMode].TotalStep);
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
		/// 辅助：由内存读取 IList<TongdaoWrapper> 拿出相关通道的TongdaoWrapper，取的是 某一场景 某一模式 某一通道 的所有步信息
		/// </summary>
		public IList<TongdaoWrapper> GetFMTDList(DB_ValuePK pk) {
			
			int selectedLightIndex = lightDictionary[pk.LightIndex];
			int tdIndex = pk.LightID - pk.LightIndex;
			IList<TongdaoWrapper> tdList = new List<TongdaoWrapper>();

			//MARK 只开单场景：10.1 GetFMTDList() 的实现改动，添加判断
			if (frameLoadArray[pk.Frame])
			{
				if (lightWrapperList[selectedLightIndex].LightStepWrapperList[pk.Frame, pk.Mode] != null
						&& lightWrapperList[selectedLightIndex].LightStepWrapperList[pk.Frame, pk.Mode].StepWrapperList != null)
				{
					IList<StepWrapper> stepWrapperList = lightWrapperList[selectedLightIndex].LightStepWrapperList[pk.Frame, pk.Mode].StepWrapperList;
					for (int step = 0; step < stepWrapperList.Count; step++)
					{
						tdList.Add(stepWrapperList[step].TongdaoList[tdIndex]);
					}
				}
			}
			//MARK 只开单场景：10.2 GetFMTDList() 的实现改动：添加从DB取数据的代码
			else
			{
				IList<DB_Value> valueList = valueDAO.GetTDValueListOrderByStep(pk);				
				foreach (DB_Value value in valueList)
				{
					tdList.Add(new TongdaoWrapper()
					{
						Address = pk.LightID,
						ScrollValue = value.ScrollValue,
						StepTime = value.StepTime,
						ChangeMode = value.ChangeMode
					});
				}
			}
			return tdList;
		}

		#region projectPanel相关

		/// <summary>
		/// 辅助方法：点击《新建工程》
		/// </summary>
		protected void newProjectClick()
		{
			//MARK 只开单场景：17.1 新建工程前，申请保存工程
			if (!RequestSaveProject("新建工程前，是否保存当前工程？", false)) {
				return;
			}		

			//每次打开新建窗口时，先将isCreateSuccess设为false;避免取消新建，仍会打开添加灯。
			IsCreateSuccess = false;
			new NewForm(this,currentFrame).ShowDialog();

			//当IsCreateSuccess==true时(NewForm中确定新建之后会修改IsCreateSuccess值)，打开灯具列表
			if (IsCreateSuccess)
			{
				editLightList();
			}
		}

		/// <summary>
		///  辅助方法: 通过工程名，新建工程; 主要通过调用InitProject(),但在前后加了鼠标特效的处理。（此操作可能耗时较久，故在方法体前后添加鼠标样式的变化）
		/// </summary>
		/// <param name="projectName">工程名</param>
		public void NewProject(string projectName, int selectedFrameIndex)
		{
			this.Cursor = Cursors.WaitCursor;
			InitProject(projectName, selectedFrameIndex, true);
			//MARK 只开单场景：01.2 NewProject时，要frameLoadArray[selectedFrame]=true；
			frameLoadArray[selectedFrameIndex] = true;
			MessageBox.Show("成功新建工程，请为此工程添加灯具。");
			this.Cursor = Cursors.Default;
		}

		/// <summary>
		/// 基类辅助方法：①ClearAllData()；②设置内部的一些工程路径及变量；③初始化数据库
		/// </summary>
		/// <param name="projectName"></param>
		/// <param name="isNew"></param>
		public void InitProject(string projectName, int selectedFrameIndex, bool isNew)
		{
			//0.清空所有内存数据及重置控件情况
			clearAllData();

			// 1.全局设置
			currentProjectName = projectName;
			projectPath = SavePath + @"\LightProject\" + projectName;
			GlobalIniPath = projectPath + @"\global.ini";
			dbFilePath = projectPath + @"\data.db3";
			Text = SoftwareName + " Dimmer System(当前工程:" + projectName + ")";

			//10.9 设置当前工程的 arrange.ini 的地址,以及先把各种可用性屏蔽掉
			arrangeIniPath = projectPath + @"\arrange.ini";

			// 9.5 读取时间因子
			IniFileAst iniAst = new IniFileAst(GlobalIniPath);
			eachStepTime = iniAst.ReadInt("Set", "EachStepTime", 30);
			eachStepTime2 = eachStepTime / 1000m;
			initStNumericUpDowns();  //更改了时间因子后，需要处理相关的stepTimeNumericUpDown，包括tdPanel内的及unifyPanel内的

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

			//MARK 只开单场景：04.0 InitProject()内初始化frameSaveArray、frameLoadArray
			//   --> 都先设为false;并将frameSaveArray[selectedFrameIndex]为true，因为只要打开了工程（New或Open）其选中场景的frameSaveArray一定要设为true的！
			//   -->（原则：当前打开的场景点击保存时一定要保存，因为在此处可能进行更改数据）
			changeCurrentFrame(selectedFrameIndex);
			frameSaveArray = new bool[FrameCount];
			frameLoadArray = new bool[FrameCount];
			for (int frameIndex = 0; frameIndex < FrameCount; frameIndex++)
			{
				frameSaveArray[frameIndex] = frameIndex == selectedFrameIndex;
				frameLoadArray[frameIndex] = false;
			}

			// 设置各按键是否可用			
			enableProjectRelative(true);
		}

		/// <summary>
		/// 辅助方法： 清空相关的所有数据（关闭工程、新建工程、打开工程都会用到）
		/// -- 子类中需有针对该子类内部自己的部分代码（如重置listView或禁用stepPanel等）
		/// </summary>
		protected virtual void clearAllData()
		{
			endview(); // 清空数据时，应该结束预览。

			currentProjectName = null;

			dbLightList = null;
			dbFineTuneList = null;
			dbStepCountList = null;

			lightAstList = null;
			lightWrapperList = null;
			lightDictionary = null;

			selectedIndex = -1;
			selectedLightName = "";
			selectedIndices = new List<int>();

			tempStep = null;
			TempMaterialAst = null;

			arrangeIniPath = null;

			//MARK 只开单场景：03.0 clearAllData()内清空frameSaveArray、frameLoadArray
			frameSaveArray = null;
			frameLoadArray = null;

			Text = SoftwareName + " Dimmer System";

			EnterSyncMode(false);  //退出同步模式
			enableSingleMode(true); // 使用《单灯模式》
			enableSLArrange(false, false);  // 《保存|读取灯具位置》不可用
			enableProjectRelative(false);  // 工程相关的所有按钮，设为不可用
			autosetEnabledPlayAndRefreshPic();  //是否可以显示 playPanel及 刷新图片

			hideAllTDPanels();
			showStepLabel(0, 0);
		}

		/// <summary>
		/// MARK 只开单场景：14.3 clearRedundantData()方法体：清空不在list内的DB数据，包括StepCount表及Value表
		/// 辅助方法：清空不在list内的DB数据，包括StepCount表及Value表
		/// </summary>
		protected virtual void deleteRedundantData()
		{
			Console.WriteLine(retainLightIndices);
			// MARK 只开单场景：14.4 若retainLightIndices为空，说明所有数据皆可删除，因为没有旧灯具
			// （全部是新加的灯具，点《确定》后删掉也无所谓了 - 若新加灯具也是空，则本来无一物何处惹尘埃）
			if (retainLightIndices == null || retainLightIndices.Count == 0)
			{
				stepCountDAO.Clear();
				valueDAO.Clear();
			}
			else
			{
				stepCountDAO.DeleteRedundantData(retainLightIndices);
				valueDAO.DeleteRedundantData(retainLightIndices);
			}
		}

		/// <summary>
		/// 辅助方法：点击《打开工程》
		/// </summary>
		protected void openProjectClick()
		{
			//MARK 只开单场景：17.2 打开工程前，申请保存工程
			if (!RequestSaveProject("打开工程前，是否保存当前工程？",false)) {
				return;
			}
			new OpenForm(this, currentFrame, currentProjectName).ShowDialog();
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
		public void OpenProject(string projectName, int frameIndex)
		{
			SetNotice("正在打开工程，请稍候...");
			Refresh(); //强行刷新显示Notice
			setBusy(true);

			DateTime beforeDT = System.DateTime.Now;

			// 0.初始化
			InitProject(projectName, frameIndex, false);

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
				DialogResult dr = MessageBox.Show("成功打开空工程：" + projectName + "  , 要为此工程添加灯具吗？",
					"",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Question);
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
				BuildLightList(lightAstList); // 通过初步lightAstList，生成 最终版的 lightAstList、lightsListView、lightWrapperList的内容、并生成所有灯具的Template
				Refresh(); //刷新灯具列表放到界面上去，优化显示体验

				//MARK 只开单场景：07.0 generateFrameData():在OpenProject内调用
				generateFrameData(currentFrame);
				autosetEnabledPlayAndRefreshPic();

				DateTime afterDT = System.DateTime.Now;
				TimeSpan ts = afterDT.Subtract(beforeDT);

				MessageBox.Show("成功打开工程：" + projectName + ",耗时: " + ts.TotalSeconds.ToString("#0.00") + " s");
				SetNotice("成功打开工程(" + projectName + ")");
			}
			setBusy(false);
		}	

		/// <summary>
		/// MARK 只开单场景：07.1 generateFrameData(int):抽象从DB读Frame数据的代码（多线程）
		/// 辅助方法：通过传入frame的值，来读取相关的Frame场景数据（两种mode）
		/// </summary>
		/// <param name="frameIndex"></param>
		protected void generateFrameData(int selectedFrameIndex)
		{
			//MARK：generateFrameData(int)采用多线程方法优化(每个灯开启一个线程)
			Thread[] threadArray = new Thread[dbLightList.Count];
			for (int lightListIndex = 0; lightListIndex < dbLightList.Count; lightListIndex++)
			{
				int tempLightIndex = lightListIndex; // 必须在循环内使用一个临时变量来记录这个index，否则线程运行时lightListIndex会发生变化。
				int tempLightNo = dbLightList[tempLightIndex].LightNo;   //记录了数据库中灯具的起始地址（不同灯具有1-32个通道，但只要是同个灯，就公用此LightNo)				

				//MARK 只开单场景：07.2 generateFrameData(int)内:修改要取的步数（由列表[全部]->列表[当前场景的两个模式]；因为都是IList<DB_StepCount>,故后面的代码无需大改。
				//IList<DB_StepCount> scList = stepCountDAO.GetStepCountList(tempLightNo); //取出数据库内的步数列表		
				IList<DB_StepCount> scList = stepCountDAO.GetStepCountListByFrame(tempLightNo, selectedFrameIndex);

				//MARK 只开单场景：07.3 generateFrameData(int)内:此处还有优化的空间 IList<DB_Value> tempDbValueList =valueDAO.GetByLightNo(tempLightNo);
				IList<DB_Value> tempDbValueList = valueDAO.GetByLightIndexAndFrame(tempLightNo, selectedFrameIndex);

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
					//Console.WriteLine("Dickov:所有线程已结束。");
					break;
				}
				else
				{
					Thread.Sleep(100);
				}
			}
			//MARK 只开单场景：07.4 generateFrameData(int)内:从DB生成FrameData后，设frameLoadArray[selectedFrameIndex]=true			
			frameLoadArray[selectedFrameIndex] = true;
			//Console.WriteLine("场景("+AllFrameList[selectedFrameIndex]+")加载完成,其frameLoadArray设为true");
		}			   

		/// <summary>
		/// 辅助方法：点击《调用场景》
		/// </summary>
		protected void useFrameClick()
		{
			//MARK 只开单场景：09.0 调用场景增加只有一个场景（0场景的情况软件不会打开，无需考虑）情况的判断--》不进入useFrameForm；
			if (MainFormBase.AllFrameList.Count == 1)
			{
				MessageBox.Show("软件中只存在一种场景，无法使用调用场景功能。");
				return;
			}

			//MARK 只开单场景：09.1 调用场景增加当前工程没有灯具数据时，不能使用此功能的校验
			if (lightAstList == null || lightAstList.Count == 0)
			{
				MessageBox.Show("当前工程没有灯具，无法使用调用场景功能。");
				return;
			}
			new UseFrameForm(this, currentFrame).ShowDialog();
		}

		/// <summary>
		/// 辅助方法：点击《保存场景》
		/// </summary>
		protected void saveFrameClick() {

			SetNotice("正在保存场景,请稍候...");
			setBusy(true);

			// 1.先判断是否有灯具数据；若无，则清空所有表数据
			if (lightAstList == null || lightAstList.Count == 0)
			{
				ClearAllDB();
			}
			// 2.保存各项数据，其中保存 灯具、FineTune 是通用的；StepCounts和Values直接用saveOrUpdate方式即可。
			else
			{
				saveAllLights();
				saveAllFineTunes();
				// 只保存当前场景（两种模式）的stepCount和value
				saveFrameSCAndValue(currentFrame);
			}

			MessageBox.Show("成功保存当前场景数据。");			
			SetNotice("成功保存场景(" + AllFrameList[currentFrame] + ")");
			setBusy(false);
		}

		/// <summary>
		/// 辅助方法：点击《保存工程》
		/// </summary>
		protected void saveProjectClick() {

			SetNotice("正在保存工程,请稍候...");
			setBusy(true);

			DateTime beforeDT = System.DateTime.Now;
			// 1.先判断是否有灯具数据；若无，则清空所有表数据
			if (lightAstList == null || lightAstList.Count == 0)
			{
				ClearAllDB();
			}
			// 2.保存各项数据			
			else
			{
				saveAllLights();
				saveAllFineTunes();
				saveAllSCAndValues();
			}

			DateTime afterDT = System.DateTime.Now;
			TimeSpan ts = afterDT.Subtract(beforeDT);

			MessageBox.Show("成功保存工程:" + currentProjectName + ",耗时: " + ts.TotalSeconds.ToString("#0.00") + " s");			
			setBusy(false);
			SetNotice("成功保存工程");
		}


		/// <summary>
		/// 辅助方法：点击《导出工程》
		/// </summary>
		protected void exportProjectClick()
		{
			if (lightAstList == null || lightAstList.Count == 0)
			{
				MessageBox.Show("当前工程没有灯具，无法导出工程。请添加灯具后再使用本功能。");
				return;
			}

			DialogResult dr = MessageBox.Show("请确保工程已保存后再进行导出，否则可能导出非预期效果。确定现在导出吗？",
					"导出工程？",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Question);
			if (dr == DialogResult.Cancel)
			{
				return;
			}

			exportFolderBrowserDialog.Description = "请选择要导出的目录，程序会自动在选中位置创建\"CSJ\"文件夹；并在导出成功后打开该目录。若工程文件过大，导出过程中软件可能会卡住，请稍等片刻即可。";
			dr = exportFolderBrowserDialog.ShowDialog();
			if (dr == DialogResult.Cancel)
			{
				return;
			}			string exportPath = exportFolderBrowserDialog.SelectedPath + @"\CSJ";
			DirectoryInfo di = new DirectoryInfo(exportPath);
			if (di.Exists && (di.GetFiles().Length + di.GetDirectories().Length != 0))
			{
				dr = MessageBox.Show("检测到目标文件夹不为空，是否覆盖？",
						"覆盖工程？",
						MessageBoxButtons.OKCancel,
						MessageBoxIcon.Question);
				if (dr == DialogResult.Cancel)
				{
					return;
				}
			}

			SetNotice("正在导出工程，请稍候...");
			setBusy(true);			

			DataConvertUtils.SaveProjectFile(GetDBWrapper(false), this, GlobalIniPath, new ExportProjectCallBack(this, exportPath));
		}


		/// <summary>
		/// MARK 导出单场景具体实现
		/// 辅助方法：右键点击《导出工程》->即导出当前场景
		/// </summary>
		protected void exportFrameClick()
		{
			if (lightAstList == null || lightAstList.Count == 0)
			{
				MessageBox.Show("当前工程没有灯具，无法导出工程。请添加灯具后再使用本功能。");
				return;
			}

			//MARK 导出单场景具体实现 1.修改弹窗的提示
			DialogResult dr = MessageBox.Show("请确保灯具列表未发生变化，并且与选择的已导出工程相比，只改动了当前场景的数据，否则可能产生错误的效果？\n确定现在导出工程（只修改当前场景数据）吗？",
					"导出工程（只修改当前场景数据）？",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Question);
			if (dr == DialogResult.Cancel)
			{
				return;
			}

			//MARK 导出单场景具体实现 2.修改打开文件夹对话框的提示
			exportFolderBrowserDialog.Description = "请选择当前工程之前已导出过的工程文件夹(CSJ文件夹的上一层)，导出工程（只修改当前场景数据）时，程序将只改动当前场景的两个bin文件、Config.bin及GradientData文件，其他文件不会发生变化，请稍等片刻即可。";
			dr = exportFolderBrowserDialog.ShowDialog();
			if (dr == DialogResult.Cancel)
			{
				return;
			}

			//MARK 导出单场景具体实现 3. 检测选中的文件夹不为空（数据数量不得为0），若此文件夹为空，则不应导出单场景
			string exportPath = exportFolderBrowserDialog.SelectedPath + @"\CSJ";
			DirectoryInfo di = new DirectoryInfo(exportPath);
			if (!di.Exists || di.GetFiles().Length == 0)
			{
				MessageBox.Show("检测到目标文件夹为空，说明该文件夹并不存在已导出工程，请选中正确的已导出工程的文件夹（有一些bin文件）！");
				return;
			}

			SetNotice("正在重新生成已导出工程的当前场景工程文件，请稍候...");
			setBusy(true);

			ExportFrame(exportPath);			
		}

		//MARK 导出单场景具体实现 4. 把选中文件夹内的所有数据拷到临时文件夹中（DataCache\Project\CSJ），拷贝前需要先清空目标文件夹；并逐一把所有CX.bin、MX.bin文件都拷贝过去		
		/// <summary>
		/// 辅助方法：当拷贝文件发生错误时，用递归的方法重新操作
		/// </summary>
		/// <param name="exportPath"></param>
		private void ExportFrame(string exportPath)
		{
			try
			{
				FileUtils.ClearProjectData();
				string destPath = Application.StartupPath + @"\DataCache\Project\CSJ";
				for (int frame = 1; frame <= FrameCount; frame++)
				{
					FileAst.CopyFile(exportPath + @"\C" + frame + ".bin", destPath, true);
					FileAst.CopyFile(exportPath + @"\M" + frame + ".bin", destPath, true);
				}
			}
			catch (Exception ex) {
				DialogResult dialogResult = MessageBox.Show("拷贝工程文件到工作目录失败，原因为：\n" + ex.Message + "\n请稍等后点击《重试》或《取消》拷贝。",
						"是否重试？",
						MessageBoxButtons.RetryCancel,
						MessageBoxIcon.Error);
				if (dialogResult == DialogResult.Retry)
				{					
					ExportFrame(exportPath);
				}
				else
				{
					//若点击取消，则直接把忙时设为false，因为不会再往下走了，没有机会进行更改操作了。
					setBusy(false);
					SetNotice("因发生异常，已取消导出工程(只修改当前场景数据)的操作。");
				}				
				return; //只要出现异常，就一定要退出本方法；
			}

			//MARK 导出单场景具体实现 5. 调用维佳的生成单场景方法，将只生成CFrame.bin、MFrame.bin、Config.bin和GradientData.bin；（其余文件都是拷贝两次：先拷到工作目录，调用完成后再拷回导出目录）
			DataConvertUtils.SaveSingleFrameFile(GetDBWrapper(false), this, GlobalIniPath, new ExportProjectCallBack(this, exportPath), currentFrame);
		}

		/// <summary>
		/// 辅助方法：点击《关闭工程》
		/// </summary>
		protected void closeProjectClick()
		{
			if (!RequestSaveProject("关闭工程前，是否保存当前工程?", false))
			{
				return;
			}

			string tempProjectName = currentProjectName;
			clearAllData();
			SetNotice("成功关闭工程("+tempProjectName+")。");
			MessageBox.Show("成功关闭工程(" + tempProjectName + ")。");
		}
		
		/// <summary>
		/// 辅助方法：拷贝已生成工程到指定目录（并在此期间生成并压缩源文件），在SaveProjectFile成功后回调
		/// </summary>
		/// <param name="exportPath"></param>
		/// <param name="success"></param>
		public void CopyProject(string exportPath, bool success)
		{
			if (success)
			{
				try
				{
					FileUtils.ExportProjectFile(exportPath);
				}
				catch (Exception ex) {
					DialogResult dialogResult = MessageBox.Show("拷贝工程文件失败，原因为：\n"+ ex.Message+"\n请在处理完成后点击《重试》或《取消》拷贝。" ,
						"是否重试？",
						MessageBoxButtons.RetryCancel,
						MessageBoxIcon.Error);
					if (dialogResult == DialogResult.Retry)
					{
						CopyProject(exportPath, success); //若点击重试，则再跑一遍本方法						
					}
					else {
						//若点击取消，则直接把忙时设为false，因为不会再往下走了，没有机会进行更改操作了。
						setBusy(false);
					}					
					return; //只要出现异常，就一定要退出本方法；
				}

				// 先生成Source文件夹到工作目录，再把该文件夹压缩到导出文件夹中
				if (GenerateSourceProject())
				{
					SetNotice("正在压缩源文件,请稍候...");
					string dirPath = SavePath + @"\Source";
					string zipPath = exportPath + @"\Source.zip";
					ZipAst.CompressAllToZip(dirPath, zipPath, 9, null, SavePath + @"\");
					SetNotice("已成功压缩源文件(Source.zip)。");
				}

				DialogResult dr = MessageBox.Show("导出工程成功,是否打开导出文件夹?",
						"打开导出文件夹？",
						MessageBoxButtons.OKCancel,
						MessageBoxIcon.Question);
				if (dr == DialogResult.OK)
				{
					System.Diagnostics.Process.Start(exportPath);
				}
			}
			else
			{
				MessageBox.Show("导出工程出错。");
			}

			setBusy(false);
			SetNotice("导出工程" + (success ? "成功" : "出错"));
		}

		/// <summary>
		/// 辅助方法：当灯具列表为空时，清空数据库内所有数据
		/// </summary>
		private void ClearAllDB()
		{
			lightDAO.Clear();
			fineTuneDAO.Clear();
			stepCountDAO.Clear();
			valueDAO.Clear();
		}

		/// <summary>
		///  辅助方法：保存灯具列表；有几个灯具就保存几个-->先统一删除，再保存
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
		///  辅助方法：保存所有的《微调对应键值》列表->一样是先删掉，再保存，避免留下冗余数据
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
		/// 辅助方法：保存指定场景的StepCount和Value值到数据库
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
					LightStepWrapper lswTemp = allLightStepWrappers[tempFrame, mode];

					// 只有满足所有的这些要求，才能升级这条数据，否则就删除（各种情况的空步）
					if (lswTemp != null && lswTemp.StepWrapperList != null && lswTemp.StepWrapperList.Count > 0)
					{
						DB_StepCount stepCount = new DB_StepCount()
						{
							StepCount = lswTemp.TotalStep,
							PK = new DB_StepCountPK()
							{
								Frame = tempFrame,
								Mode = mode,
								LightIndex = light.LightNo
							}
						};
						stepCountDAO.SaveOrUpdate(stepCount);
					}
					else
					{
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
					if (lswTemp != null && lswTemp.TotalStep > 0)
					{
						IList<StepWrapper> stepWrapperList = lswTemp.StepWrapperList;
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
		/// 辅助方法：通过遍历的方法，逐一把所有frame的stepCount和values数据写到数据库中
		/// </summary>
		private void saveAllSCAndValues()
		{
			//MARK 只开单场景：08.0 保存所有场景数据（两张表），StepCount和Value；并通过frameSaveArray，判断是否要进行保存
			for (int frameIndex = 0; frameIndex < FrameCount; frameIndex++)
			{
				if (frameSaveArray[frameIndex])
				{
					saveFrameSCAndValue(frameIndex);
					//MARK 只开单场景：08.1 如遍历到的frameIndex非当前场景，则frameSaveArray[frameIndex]设为false;意味着之后不需要进行保存了;而当前场景的值仍为true；
					if (frameIndex != currentFrame)
					{
						frameSaveArray[frameIndex] = false;
					}
				}
			}
		}

		/// <summary>
		///  辅助方法：调用其他场景
		/// </summary>
		/// <param name="text"></param>
		public void UseOtherForm(int selectedFrameIndex)
		{
			//MARK 只开单场景：09.1 调用场景时，若是已打开的场景，保持原样不动；若是未打开的场景，主动帮着打开
			if (!frameLoadArray[selectedFrameIndex])
			{
				generateFrameData(selectedFrameIndex);
			}

			if (lightWrapperList != null && lightWrapperList.Count != 0)
			{
				foreach (LightWrapper lightWrapper in lightWrapperList)
				{
					lightWrapper.LightStepWrapperList[currentFrame, currentMode]
						= LightStepWrapper.GenerateLightStepWrapper(lightWrapper.LightStepWrapperList[selectedFrameIndex, currentMode], lightWrapper.StepTemplate, currentMode);
				}
			}

			EnterSyncMode(false);
			RefreshStep();
			MessageBox.Show("成功调用场景:" + AllFrameList[selectedFrameIndex]);
		}

		/// <summary>
		/// MARK 只开单场景：17.0 请求保存工程的辅助方法：RequestSaveProject（string msg,bool isAfterUpdateLightList)
		/// 辅助方法：请求保存工程
		/// </summary>
		/// <returns>若在本方法之后继续下去，则返回true；若是不再往下执行，则返回false</returns>
		/// <param name="msg">提示保存的消息</param>
		/// <param name="isAfterUpdateLightList">是否刚刚添加或修改完灯具列表</param>
		public bool RequestSaveProject(string msg, bool isAfterUpdateLightList)
		{
			if (frameSaveArray == null)
			{
				return true;
			}

			DialogResult dr = MessageBox.Show(
				msg,
				"保存工程?",
				MessageBoxButtons.YesNoCancel,
				 MessageBoxIcon.Question
			);

			if (dr == DialogResult.Yes)
			{
				if (isAfterUpdateLightList)
				{
					deleteRedundantData();
				}
				saveProjectClick();
				return true;
			}
			else if (dr == DialogResult.No)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		#endregion

		#region 菜单栏-工程相关

		/// <summary>
		/// 辅助方法：打开《灯具列表》
		/// </summary>
		protected void editLightList()
		{
			new LightsForm(this, lightAstList).ShowDialog();
		}

		/// <summary>
		/// 辅助方法：打开《全局设置》
		/// </summary>
		protected void globalSetClick()
		{
			new GlobalSetForm(this).ShowDialog();
		}

		/// <summary>
		/// 辅助方法：打开《摇麦设置》
		/// </summary>
		protected void ymSetClick()
		{
			new YMSetForm(this).ShowDialog();
		}

		/// <summary>
		/// 辅助方法：点击《工程升级》
		/// </summary>
		protected void projectUpdateClick()
		{
			if (isConnected)
			{
				connectButtonClick();
			}

			new ProjectUpdateForm(this, GetDBWrapper(false), GlobalIniPath, tempProjectPath).ShowDialog();
		}

		#endregion

		#region 菜单栏-非工程相关

		/// <summary>
		/// 辅助方法：点击《灯库软件》
		/// </summary>
		protected void openLightEditor()
		{

			if (IsLinkLightEditor)
			{
				try
				{
					System.Diagnostics.Process.Start(Application.StartupPath + @"\LightEditor.exe");
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
			else
			{
				// 若使用下列语句，则直接把《灯库编辑软件》集成在本Form中
				new LightEditor.LightEditorForm(this).ShowDialog();
			}
		}

		/// <summary>
		/// 辅助方法：点击《硬件升级》
		/// </summary>
		protected void hardwareUpdateClick()
		{
			new HardwareUpdateForm(this, binPath).ShowDialog();
		}

		/// <summary>
		/// 辅助方法：点击《(新)外设配置》
		/// </summary>
		protected void newToolClick()
		{
			// 若要进入《其他工具》，应该先将连接断开
			if (isConnected)
			{
				connectButtonClick();
			}
			new NewToolsForm(this).ShowDialog();
		}

		#endregion

		#region stepPanel相关

		/// <summary>
		/// 辅助方法：点击《上一步》
		/// </summary>
		protected void backStepClick()
		{
			int currentStep = getCurrentStep();
			chooseStep(currentStep > 1 ? currentStep - 1 : getTotalStep());
		}

		/// <summary>
		/// 辅助方法：点击《下一步》
		/// </summary>
		protected void nextStepClick()
		{
			int currentStep = getCurrentStep();
			int totalStep = getTotalStep();
			chooseStep(currentStep < totalStep ? currentStep + 1 : 1);
		}

		/// <summary>
		/// 辅助方法：点击《前插|后插步》
		/// </summary>
		/// <param name="insertBefore"></param>
		protected void insertStepClick(bool insertBefore)
		{
			// 获取当前步与最高值，总步数			
			// 若当前步 <= 总步数，则可以插入，并将之后的步数往后移动
			// 否则报错
			LightStepWrapper lsWrapper = getCurrentLightStepWrapper();
			if (lsWrapper.CurrentStep > lsWrapper.TotalStep)
			{
				MessageBox.Show("Dickov:当前步大于总步数");
				return;
			}


			int currentStep = lsWrapper.CurrentStep;    // 当前步
			int stepIndex = currentStep - 1;  //插入的位置：InsertStep方法中有针对前后插的判断，无需处理

			StepWrapper newStep;
			if (insertBefore)
			{
				newStep = StepWrapper.GenerateNewStep(stepIndex == 0 ? getCurrentStepTemplate() : getSelectedLightSelectedStepWrapper(selectedIndex, stepIndex - 1), currentMode);
			}
			else
			{
				newStep = StepWrapper.GenerateNewStep(currentStep == 0 ? getCurrentStepTemplate() : getCurrentStepWrapper(), currentMode);
			}
			lsWrapper.InsertStep(stepIndex, newStep, insertBefore);

			if (isSyncMode)
			{
				for (int lightIndex = 0; lightIndex < lightAstList.Count; lightIndex++)
				{
					if (lightIndex != selectedIndex)
					{
						if (insertBefore)
						{
							newStep = StepWrapper.GenerateNewStep(stepIndex == 0 ? getSelectedLightStepTemplate(lightIndex) : getSelectedLightSelectedStepWrapper(lightIndex, stepIndex - 1), currentMode);
						}
						else
						{
							newStep = StepWrapper.GenerateNewStep(currentStep == 0 ? getSelectedLightStepTemplate(lightIndex) : getSelectedLightCurrentStepWrapper(lightIndex), currentMode);
						}
						getSelectedLightStepWrapper(lightIndex).InsertStep(stepIndex, newStep, insertBefore);
					}
				}
			}
			else if (isMultiMode)
			{
				foreach (int lightIndex in selectedIndices)
				{
					if (lightIndex != selectedIndex)
					{
						if (insertBefore)
						{
							newStep = StepWrapper.GenerateNewStep(stepIndex == 0 ? getSelectedLightStepTemplate(lightIndex) : getSelectedLightSelectedStepWrapper(lightIndex, stepIndex - 1), currentMode);
						}
						else
						{
							newStep = StepWrapper.GenerateNewStep(currentStep == 0 ? getSelectedLightStepTemplate(lightIndex) : getSelectedLightCurrentStepWrapper(lightIndex), currentMode);
						}
						getSelectedLightStepWrapper(lightIndex).InsertStep(stepIndex, newStep, insertBefore);
					}
				}
			}

			RefreshStep();
		}

		/// <summary>
		/// 辅助方法：点击《追加步》
		/// </summary>
		protected void addStepClick()
		{
			LightStepWrapper lsWrapper = getCurrentLightStepWrapper();

			//1.若当前灯具在本F/M下总步数为0 ，则使用stepTemplate数据，
			//2.否则使用本灯当前最大步的数据			 
			bool addTemplate = getTotalStep() == 0;
			StepWrapper newStep = StepWrapper.GenerateNewStep(addTemplate ? getCurrentStepTemplate() : getCurrentLightLastStepWrapper(), currentMode);
			lsWrapper.AddStep(newStep);
			if (isSyncMode)
			{
				for (int lightIndex = 0; lightIndex < lightAstList.Count; lightIndex++)
				{
					if (lightIndex != selectedIndex) //多一层保险...
					{
						newStep = StepWrapper.GenerateNewStep(addTemplate ? getSelectedLightStepTemplate(lightIndex) : getSelectedLightLastStepWrapper(lightIndex), currentMode);
						getSelectedLightStepWrapper(lightIndex).AddStep(newStep);
					}
				}
			}
			else if (isMultiMode)
			{
				foreach (int lightIndex in selectedIndices)
				{
					if (lightIndex != selectedIndex)
					{
						newStep = StepWrapper.GenerateNewStep(addTemplate ? getSelectedLightStepTemplate(lightIndex) : getSelectedLightLastStepWrapper(lightIndex), currentMode);
						getSelectedLightStepWrapper(lightIndex).AddStep(newStep);
					}
				}
			}
			RefreshStep();
		}

		/// <summary>
		/// 辅助方法：点击《删除步》
		/// </summary>
		protected void deleteStepClick()
		{
			LightStepWrapper lsWrapper = getCurrentLightStepWrapper();
			int stepIndex = getCurrentStep() - 1;

			// 调用包装类内部的方法:删除某一步
			try
			{
				lsWrapper.DeleteStep(stepIndex);
				if (isSyncMode)
				{
					for (int lightIndex = 0; lightIndex < lightAstList.Count; lightIndex++)
					{
						if (lightIndex != selectedIndex)
						{
							getSelectedLightStepWrapper(lightIndex).DeleteStep(stepIndex);
						}
					}
				}
				else if (isMultiMode)
				{
					foreach (int lightIndex in selectedIndices)
					{
						if (lightIndex != selectedIndex)
						{
							getSelectedLightStepWrapper(lightIndex).DeleteStep(stepIndex);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}

			RefreshStep();
		}

		/// <summary>
		/// 辅助方法：点击《粘贴步》
		/// </summary>
		protected void pasteStepClick()
		{
			// 1. 先判断是不是同模式及同一种灯具（非同一个灯具也可以复制，但需类型(同一个灯库内容)一样)
			StepWrapper currentStep = getCurrentStepWrapper();
			if (currentStep == null)
			{
				MessageBox.Show("当前步数据为空，无法粘贴步");
				return;
			}
			if (currentStep.LightMode != tempStep.LightMode)
			{
				MessageBox.Show("不同模式下无法复制步");
				return;
			}
			if (currentStep.LightFullName != tempStep.LightFullName)
			{
				MessageBox.Show("不同类型灯具无法复制步");
				return;
			}

			// 2.逐一将TongdaoList的某些数值填入tempStep中，而非粗暴地将currentStep 设为tempStep
			for (int i = 0; i < tempStep.TongdaoList.Count; i++)
			{
				currentStep.TongdaoList[i].ScrollValue = tempStep.TongdaoList[i].ScrollValue;
				currentStep.TongdaoList[i].ChangeMode = tempStep.TongdaoList[i].ChangeMode;
				currentStep.TongdaoList[i].StepTime = tempStep.TongdaoList[i].StepTime;
			}


			//3.如果是多灯模式，则需要在复制步之后处理下每个灯具的信息
			if (isMultiMode)
			{
				copyStepToAll(getCurrentStep(), WHERE.ALL);
			}

			//4.刷新当前步
			RefreshStep();
		}

		/// <summary>
		/// 辅助方法：点击《复制多步》
		/// </summary>
		protected void multiCopyClick()
		{
			MultiStepCopyForm mscForm = new MultiStepCopyForm(this, getCurrentLightStepWrapper().StepWrapperList, currentMode, selectedLightName, getCurrentStep());
			if (mscForm != null && !mscForm.IsDisposed)
			{
				mscForm.ShowDialog();
			}
		}

		/// <summary>
		/// 辅助方法：点击《粘贴多步》
		/// </summary>
		protected void multiPasteClick()
		{
			if (TempMaterialAst == null)
			{
				MessageBox.Show("还未复制多步，无法粘贴。");
				return;
			}
			if (TempMaterialAst.Mode != currentMode)
			{
				MessageBox.Show("复制的多步与当前模式不同，无法粘贴。");
				return;
			}
			new MultiStepPasteForm(this).ShowDialog();
		}

		/// <summary>
		///  辅助方法：点击《使用素材》
		/// </summary>
		protected void useMaterial()
		{
			LightAst la = lightAstList[selectedIndex];
			new MaterialUseForm(this, currentMode, la.LightName, la.LightType).ShowDialog();
		}

		/// <summary>
		/// 辅助方法：点击《保存素材》
		/// </summary>
		protected void saveMaterial()
		{

			LightAst lightAst = lightAstList[selectedIndex];
			MaterialSaveForm materialForm = new MaterialSaveForm(this, getCurrentLightStepWrapper().StepWrapperList, currentMode, lightAst.LightName, lightAst.LightType);
			if (materialForm != null && !materialForm.IsDisposed)
			{
				materialForm.ShowDialog();
			}
		}

		/// <summary>
		/// 辅助方法： 改变了模式和场景后的操作		
		/// </summary>
		protected void changeFrameMode()
		{
			// 9.2 不可让selectedIndex为-1  , 否则会出现数组越界错误
			if (selectedIndex == -1)
			{
				return;
			}

			/// 复位同步状态为false
			EnterSyncMode(false);

			//最后都要用上RefreshStep()
			RefreshStep();
		}

		/// <summary>
		/// 辅助方法：初始化灯具数据。
		/// 0.先查看当前内存是否已有此数据 
		/// 1.若还未有，则取出相关的ini进行渲染
		/// 2.若内存内 或 数据库内已有相关数据，则使用这个数据。
		/// </summary>
		/// <param name="la"></param>
		protected void generateLightData()
		{
			if (selectedIndex == -1)
			{
				return;
			}
			LightAst lightAst = lightAstList[selectedIndex];

			// 1.在右侧灯具信息内显示选中灯具相关信息
			editLightInfo(lightAst);

			//2.判断是不是已经有stepTemplate了
			// ①若无，则生成数据，并hideAllTongdao 并设stepLabel为“0/0” --> 因为刚创建，肯定没有步数	
			// ②若有，还需判断该LightData的LightStepWrapperList[frame,mode]是不是为null
			//			若是null，则说明该FM下，并未有步数，hideAllTongdao
			//			若不为null，则说明已有数据，
			LightWrapper lightWrapper = lightWrapperList[selectedIndex];
			if (lightWrapper.StepTemplate == null)
			{
				lightWrapper.StepTemplate = generateStepTemplate(lightAst);
			}

			enableStepPanel(true);

			//3.手动刷新当前步信息
			RefreshStep();
		}

		/// <summary>
		/// 辅助方法：抽象了【选择某一个指定步数后，统一的操作；NextStep和BackStep等应该都使用这个方法】
		/// --所有更换通道数据的操作后，都应该使用这个方法。
		/// </summary>
		protected void chooseStep(int stepNum)
		{
			if (stepNum == 0)
			{
				showTDPanels(null, 0);
				showStepLabel(0, 0);
				return;
			}

			LightStepWrapper lightStepWrapper = getCurrentLightStepWrapper();
			StepWrapper stepWrapper = lightStepWrapper.StepWrapperList[stepNum - 1];
			lightStepWrapper.CurrentStep = stepNum;

			if (isMultiMode)
			{
				foreach (int lightIndex in selectedIndices)
				{
					getSelectedLightStepWrapper(lightIndex).CurrentStep = stepNum;
				}
			}
			//11.27 若是同步状态，则选择步时，将所有灯都设为一致的步数
			if (isSyncMode)
			{
				for (int lightIndex = 0; lightIndex < lightAstList.Count; lightIndex++)
				{
					getSelectedLightStepWrapper(lightIndex).CurrentStep = stepNum;
				}
			}

			showTDPanels(stepWrapper.TongdaoList, stepWrapper.StartNum);
			showStepLabel(lightStepWrapper.CurrentStep, lightStepWrapper.TotalStep);

			if (isConnected && isRealtime)
			{
				oneStepWork();
			}
		}

		#endregion

		#region unifyPanel(Or astPanel)相关

		/// <summary>
		/// 辅助方法：点击《全部归零》
		/// </summary>
		protected void zeroButtonClick()
		{
			StepWrapper currentStep = getCurrentStepWrapper();
			if (currentStep == null || currentStep.TongdaoList == null || currentStep.TongdaoList.Count == 0)
			{
				MessageBox.Show("请先选中任意步数，才能进行统一调整！");
				SetNotice("请先选中任意步数，才能进行统一调整！");
				return;
			}

			for (int i = 0; i < currentStep.TongdaoList.Count; i++)
			{
				getCurrentStepWrapper().TongdaoList[i].ScrollValue = 0;
			}

			if (isMultiMode)
			{
				copyUnifyValueToAll(getCurrentStep(), WHERE.SCROLL_VALUE, 0);
			}

			RefreshStep();
		}

		/// <summary>
		/// 辅助方法：点击《设为初值》
		/// </summary>
		protected void initButtonClick()
		{
			StepWrapper currentStep = getCurrentStepWrapper();
			if (currentStep == null || currentStep.TongdaoList == null || currentStep.TongdaoList.Count == 0) {
				MessageBox.Show("请先选中任意步数，才能进行统一调整！");
				SetNotice("请先选中任意步数，才能进行统一调整！");
				return;
			}

			StepWrapper stepMode = getCurrentStepTemplate();
			for (int i = 0; i < currentStep.TongdaoList.Count; i++)
			{
				getCurrentStepWrapper().TongdaoList[i].ScrollValue = stepMode.TongdaoList[i].ScrollValue;
			}
			if (isMultiMode)
			{
				// 全部设为初值（只改变scrollValue，初值里不包括StepTime和ChangeMode）
				copyStepToAll(getCurrentStep(), WHERE.SCROLL_VALUE);
			}
			RefreshStep();
		}

		/// <summary>
		/// 辅助方法：点击《多步调节》
		/// </summary>
		protected void multiButtonClick()
		{
			StepWrapper currentStep = getCurrentStepWrapper();
			if (currentStep == null || currentStep.TongdaoList == null || currentStep.TongdaoList.Count == 0)
			{
				MessageBox.Show("请先选中任意步数，才能进行统一调整！");
				SetNotice("请先选中任意步数，才能进行统一调整！");
				return;
			}

			new MultiStepForm(this, getCurrentStep(), getTotalStep(), getCurrentStepWrapper(), currentMode).ShowDialog();
		}

		#endregion

		#region playPanel相关

		/// <summary>
		/// 辅助方法：点击《预览效果》
		/// </summary>
		internal void Preview()
		{			
			if (!isConnectCom)
			{
				playTools.StartInternetPreview(selectedIpAst.DeviceIP, new NetworkDebugReceiveCallBack(this), eachStepTime);
			}
			SetNotice("预览数据生成成功,即将开始预览。");
			playTools.PreView(GetDBWrapper(false), GlobalIniPath, currentFrame);			
		}

		/// <summary>
		/// 辅助方法：结束预览
		/// </summary>
		protected void endview()
		{
			playTools.EndView();	
		}

		#endregion

		#region lightListView相关
		/// <summary>
		/// 辅助方法：双击列表中灯具，更改备注
		/// </summary>
		protected void lightsListViewDoubleClick(int lightIndex)
		{
			LightAst la = lightAstList[lightIndex];
			new LightRemarkForm(this, la, lightIndex).ShowDialog();
		}

		/// <summary>
		/// MARK 修改备注：EditLightRemark()基类实现
		/// 辅助方法：添加或修改备注
		/// </summary>
		/// <param name="lightIndex"></param>
		/// <param name="remark"></param>
		public virtual void EditLightRemark(int lightIndex, string remark)
		{
			// 内存的lightAstList[lightIndex]要改动相应的值；
			lightAstList[lightIndex].Remark = remark;
			editLightInfo(lightAstList[lightIndex]);
		}

		#endregion

		#region 初始化及窗口相关

		/// <summary>
		/// 辅助方法：在此初始化一些子类都会用到的控件，并需在子类构造函数中优先调用这个方法;以及一些全局变量的取出
		/// </summary>
		protected void initGeneralControls()
		{

			this.components = new System.ComponentModel.Container();

			// exportFolderBrowserDialog : 导出工程相关
			this.exportFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.exportFolderBrowserDialog.Description = "请选择要导出的目录，程序会自动在选中位置创建\"CSJ\"文件夹；并在导出成功后打开该目录。若工程文件过大，导出过程中软件可能会卡住，请稍等片刻即可。";
			this.exportFolderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;

			//// myToolTip：悬停提示,延迟600ms
			this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.myToolTip.IsBalloon = true;
			this.myToolTip.InitialDelay = 600;

			//softwareName =globalSetFileAst.ReadString("Show", "softwareName", "TRANS-JOY");   // 使用这行代码,则中文会乱码			
			SoftwareName = IniFileAst_UTF8.ReadString(Application.StartupPath + @"/GlobalSet.ini", "Show", "softwareName", "TRANS-JOY");
			SavePath = @IniFileAst.GetSavePath(Application.StartupPath);
			IsShowTestButton = IniFileAst.GetControlShow(Application.StartupPath, "testButton");
			IsShowHardwareUpdate = IniFileAst.GetControlShow(Application.StartupPath, "hardwareUpdateButton");
			IsLinkLightEditor = IniFileAst.GetIsLink(Application.StartupPath, "lightEditor");
			IsLinkOldTools = IniFileAst.GetIsLink(Application.StartupPath, "oldTools");
			MAX_StTimes = IniFileAst.GetSystemCount(Application.StartupPath, "maxStTimes");
			MAX_STEP = IniFileAst.GetSystemCount(Application.StartupPath, "maxStep");
		}
			   
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// MainFormBase
			// 
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Name = "MainFormBase";
			this.Load += new System.EventHandler(this.MainFormBase_Load);
			this.ResumeLayout(false);

		}

		/// <summary>
		/// 辅助方法：点击退出时FormClosing事件；
		/// </summary>
		/// <param name="e"></param>
		protected void formClosing(FormClosingEventArgs e)
		{
			//MARK 只开单场景：17.4 FormClosing前提示保存工程
			if (!RequestSaveProject("关闭窗口前，是否保存当前工程？", false))
			{
				e.Cancel = true;
			}
		}

		/// <summary>
		/// 辅助方法：点击《退出程序》
		/// </summary>
		protected void exitClick()
		{
			//MARK 只开单场景：17.3 点击《退出程序》时，申请保存工程
			if (!RequestSaveProject("退出应用前，是否保存当前工程？", false))
			{
				return;
			}
			exit();
		}

		/// <summary>
		///  辅助方法：彻底退出程序
		/// </summary>
		protected void exit()
		{
			System.Environment.Exit(0);
		}

		#endregion

		#region 弃用方法区

		//弃用原因：不再支持可更改时间因子。
		///// <summary>
		///// 辅助方法：在《在全局配置》中改变了时间因子并保存后，mainForm的时间因子变量也跟着改变，同时刷新当前步
		///// </summary>
		//public void ChangeEachStepTime(int eachStepTime)
		//{
		//	this.eachStepTime = eachStepTime;
		//	this.eachStepTime2 = eachStepTime / 1000;
		//	RefreshStep();
		//}

		//SkinMainForm.MakeFrameData() ， 实时填充某一场景的所有数据（可能在某些操作里需要用到）
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

		/// <summary>
		///  辅助方法：实时读取（渲染）单灯单步的数据（单线程即可）
		/// </summary>
		//protected void MakeCurrentStepWrapperData(int stepNum)
		//{
		
		//	if (getCurrentLightStepWrapper().StepWrapperList[stepNum - 1] != null && getCurrentLightStepWrapper().StepWrapperList[stepNum - 1].Flag == 0)
		//	{

		//		LightAst la = lightAstList[selectedIndex];
		//		int lightIndex = la.StartNum;
		//		IList<DB_Value> tempValueList = valueDAO.getStepValueList(lightIndex, frame, mode, getCurrentStep());

		//		if (tempValueList.Count == getCurrentLightStepWrapper().StepWrapperList[stepNum - 1].TongdaoList.Count)
		//		{
		//			for (int tdIndex = 0; tdIndex < tempValueList.Count; tdIndex++)
		//			{
		//				DB_Value stepValue = tempValueList[tdIndex];
		//				getCurrentLightStepWrapper().StepWrapperList[stepNum - 1].TongdaoList[tdIndex].ScrollValue = tempValueList[tdIndex].ScrollValue;
		//				getCurrentLightStepWrapper().StepWrapperList[stepNum - 1].TongdaoList[tdIndex].StepTime = tempValueList[tdIndex].StepTime;
		//				getCurrentLightStepWrapper().StepWrapperList[stepNum - 1].TongdaoList[tdIndex].ChangeMode = tempValueList[tdIndex].ChangeMode;
		//			}
		//		}
		//		getCurrentLightStepWrapper().StepWrapperList[stepNum - 1].Flag = 1;
		//		Console.WriteLine("此步成功渲染。");
		//	}
		//	else
		//	{
		//		Console.WriteLine("此步已渲染过了。");
		//	}
		//}

		/// <summary>		
		/// 辅助方法：保存步数信息：针对每个灯具，保存其相关的步数情况; （弃用原因：有更好的方法替代）
		/// </summary>
		//protected void saveAllStepCounts()
		//{
		//	if (stepCountDAO == null)
		//	{
		//		stepCountDAO = new StepCountDAO(dbFilePath, isEncrypt);
		//	}
		//	// 由lightWrapperList生成最新的dbStepCountList
		//	generateDBStepCountList();
		//	// 先删除所有，再保存当前的列表
		//	stepCountDAO.SaveAll("StepCount", dbStepCountList);
		//}

		/// <summary>
		/// 辅助方法：存放所有灯具所有场景的每一步每一通道的值，记录数据到db.Value表中 （弃用原因：有更好的方法替代；）
		/// </summary>
		//protected void saveAllValues()
		//{
		//	// 10.24 过时的方法:可能导致内存溢出（数据量过大）
		//	if (valueDAO == null)
		//	{
		//		valueDAO = new ValueDAO(dbFilePath, isEncrypt);
		//	}
		//	//由lightWrapperList等内存数据，生成dbValueList
		//	generateDBValueList();
		//	// 调用此方法，会先删除之前的表数据，再将当前dbValueList保存到数据库中

		//	valueDAO.SaveAll("Value", dbValueList);
		//}

		/// <summary>
		///  辅助方法：由dbFilePath，获取valueList（弃用原因：valueList应该从内存中拿，直接从DB拿效率太低）
		/// </summary>
		/// <returns></returns>
		//protected IList<DB_Value> getValueList()
		//{
		//	if (valueDAO == null)
		//	{
		//		valueDAO = new ValueDAO(dbFilePath, isEncrypt);
		//	}

		//	IList<DB_Value> valueList = valueDAO.GetAll();
		//	return valueList;
		//}

		/// <summary>
		/// 辅助方法：由lightWrapperList等内存数据，生成dbValueList
		/// </summary>
		//protected void generateDBValueList()
		//{
		//	需要先清空valueList
		//   dbValueList = new List<DB_Value>();
		//	foreach (LightWrapper lightTemp in lightWrapperList)
		//	{
		//		DB_Light light = dbLightList[lightWrapperList.IndexOf(lightTemp)];
		//		LightStepWrapper[,] lswl = lightTemp.LightStepWrapperList;
		//		for (int frame = 0; frame < FrameCount; frame++)
		//		{
		//			for (int mode = 0; mode < 2; mode++)
		//			{
		//				LightStepWrapper lightStep = lswl[frame, mode];
		//				if (lightStep != null && lightStep.TotalStep > 0)
		//				{  //只有不为null，才可能有需要保存的数据
		//					IList<StepWrapper> stepWrapperList = lightStep.StepWrapperList;
		//					foreach (StepWrapper step in stepWrapperList)
		//					{
		//						int stepIndex = stepWrapperList.IndexOf(step) + 1;
		//						for (int tongdaoIndex = 0; tongdaoIndex < step.TongdaoList.Count; tongdaoIndex++)
		//						{
		//							TongdaoWrapper tongdao = step.TongdaoList[tongdaoIndex];
		//							DB_Value valueTemp = new DB_Value()
		//							{
		//								ChangeMode = tongdao.ChangeMode,
		//								ScrollValue = tongdao.ScrollValue,
		//								StepTime = tongdao.StepTime,
		//								PK = new DB_ValuePK()
		//								{
		//									Frame = frame,
		//									Mode = mode,
		//									LightID = light.LightNo + tongdaoIndex,
		//									LightIndex = light.LightNo,
		//									Step = stepIndex
		//								}
		//							};
		//							dbValueList.Add(valueTemp);
		//						}
		//					}
		//				}
		//			}
		//		}
		//	}
		//}

		/// <summary>
		/// 辅助方法：提示工程与灯库不匹配，并clearAll
		/// </summary>
		/// <param name="ex"></param>
		//private void openProjectError(string exMessage)
		//{
		//	MessageBox.Show("打开工程出错，可能是灯库不匹配。\n异常信息:" + exMessage);
		//	clearAllData();
		//	setBusy(false);
		//	SetNotice("打开工程失败,请验证后重试");
		//}

		#endregion

		private void MainFormBase_Load(object sender, EventArgs e)
		{

		}
		
	}

	public class NetworkDebugReceiveCallBack : ICommunicatorCallBack
	{
		private MainFormBase mainForm;

		public NetworkDebugReceiveCallBack(MainFormBase mainForm)
		{
			this.mainForm = mainForm;
		}

		public void Completed(string deviceTag)
		{
			//mainForm.SetNotice("网络设备(" + deviceTag + ")成功进入网络调试模式。");
			mainForm.EnableConnectedButtons(true);
		}

		public void Error(string deviceTag, string errorMessage)
		{
			mainForm.SetNotice("设备(" + deviceTag + ")启动网络调试模式失败。");
		}

		public void GetParam(CSJ_Hardware hardware)
		{
			throw new NotImplementedException();
		}

		public void UpdateProgress(string deviceTag, string fileName, int newProgress)
		{
			throw new NotImplementedException();
		}
	}

	public class NetworkEndDebugReceiveCallBack : ICommunicatorCallBack
	{
		public void Completed(string deviceTag)
		{
			throw new NotImplementedException();
		}

		public void Error(string deviceTag, string errorMessage)
		{
			throw new NotImplementedException();
		}

		public void GetParam(CSJ_Hardware hardware)
		{
			throw new NotImplementedException();
		}

		public void UpdateProgress(string deviceTag, string fileName, int newProgress)
		{
			throw new NotImplementedException();
		}
	}

	public class PreviewCallBack : ISaveProjectCallBack
	{
		MainFormBase mainForm;
		public PreviewCallBack(MainFormBase mainForm)
		{
			this.mainForm = mainForm;
		}
		public void Completed()
		{			
			mainForm.Preview();
		}
		public void Error()
		{
			mainForm.SetNotice("预览数据生成出错,无法预览。");			
		}
		public void UpdateProgress(string name)
		{
			mainForm.SetNotice("预览数据生成中(" + name + ")");
		}
	}

	public class ExportProjectCallBack : ISaveProjectCallBack
	{
		private MainFormBase mainForm;
		private string exportFolder;
		public ExportProjectCallBack(MainFormBase mainForm, string exportFolder)
		{
			this.mainForm = mainForm;
			this.exportFolder = exportFolder;
		}
		public void Completed()
		{
			mainForm.CopyProject(exportFolder, true);
		}
		public void Error()
		{
			mainForm.CopyProject(exportFolder, false);
		}
		public void UpdateProgress(string name)
		{
			mainForm.SetNotice("正在生成工程文件("+name+")");
		}
	}

}