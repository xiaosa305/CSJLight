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
using System.Diagnostics;
using LightController.MyForm.Multiplex;
using LightController.PeripheralDevice;
using System.Drawing;

namespace LightController.MyForm
{
	public class MainFormBase : System.Windows.Forms.Form, MainFormInterface
	{
		/// <summary>
		/// 枚举类型：《多步(多通道)调节》参数的一种
		/// </summary>
		public enum WHERE
		{
			SCROLL_VALUE,
			CHANGE_MODE,
			STEP_TIME,
			ALL
		}

		// 几个全局的辅助控件（导出文件、toolTip提示等）
		protected FolderBrowserDialog exportFolderBrowserDialog;
		private System.ComponentModel.IContainer components;
		protected ToolTip myToolTip;

		//各类提示
		protected string useFrameNotice = "使用本功能，将以选中的场景数据替换当前的场景数据。";
		protected string chooseStepNotice = "使用本功能，将以选中的场景数据替换当前的场景数据。";
		protected string keepNotice = "点击此按钮后，当前未选中的其它灯具将会保持它们最后调整时的状态，方便调试。";
		protected string insertNotice = "左键点击此按钮为后插步(即在当前步之后添加新步)，\n右键点击此按钮为前插步(即在当前步之前添加新步)。";
		protected string backStepNotice = "右击可跳转至第一步";
		protected string nextStepNotice = "右击可跳转至最后一步";		
			   
		// 全局配置及数据库连接		
		public static int NETWORK_WAITTIME; //网络搜索时的通用暂停时间
		public string SoftwareName;  //动态载入软件名（前半部分）后半部分需自行封装
		public string SavePath; // 动态载入相关的存储目录（开发时放在C:\Temp中；发布时放在应用所在文件夹）	

		public bool IsShowTestButton = false;
		public bool IsShowHardwareUpdate = false;
		public bool IsUseSkin = false ;
		public bool IsNoticeUnifyTd = true;

		// 打开程序时，即需导入的变量（全局静态变量，其他form可随时使用）			
		public static IList<string> AllFrameList; // 将所有场景名称写在此处,并供所有类使用（动态导入场景到此静态变量中）
		public static int FrameCount = 0;  //场景数量
		public static int MAX_StTimes = 250;  //每步 时间因子可乘的 最大倍数 如 0.04s*250= 10s ; 应设为常量	-》200331确认为15s=0.03*500	
		public static int MAX_STEP = 100;  //每个场景的最大步数，动态由配置文件在打开软件时读取（换成音频场景时也要发生变化，因为音频模式的步数上限不同）
		public bool IsShowSaPanels = true; // 是否显示 子属性面板
		public static int DefaultSoundCM = 0; // 添加音频步数时，其跳渐变默认值（可由配置文件进行改变）	
		protected List<int> tdValues = null;

		// 辅助的bool变量：	
		protected bool isInit = false;// form都初始化后，才将此变量设为true;为防止某些监听器提前进行监听
		public bool IsCreateSuccess = false;  ///点击新建后，用这个变量决定是否打开灯具编辑列表
		public MaterialAst TempMaterialAst = null;  // 辅助（复制多步、素材）变量 ， 《复制、粘贴多步》时使用		

		// 程序运行后，动态变化的变量
		protected string arrangeIniPath = null;  // 打开工程时 顺便把相关的位置保存ini(arrange.ini) 也读取出来（若有的话）
		protected bool isAutoArrange = true; // 默认情况下，此值为true，代表右键菜单“自动排列”默认情况下是打开的。
		protected string binPath = null; // 此处记录《硬件更新》时，选过的xbin文件路径。
		protected string tempProjectPath = null; //此处记录《工程更新》时，选过的文件夹路径。		
	
		// 工程相关的变量（只在工程载入后才用到的变量）
		protected string currentProjectName;  //存放当前工程名，主要作用是防止当前工程被删除（openForm中）
		protected string projectPath; //存放当前工程所在目录
		public string GlobalIniPath;  // 存放当前工程《全局配置》、《摇麦设置》的配置文件的路径
		protected string dbFilePath; // 数据库地址：每个工程都有自己的db，所以需要一个可以改变的dbFile字符串，存放数据库连接相关信息		
		protected bool isEncrypt = false; //是否加密				
		public int eachStepTime = 30; // 默认情况下，步时间默认值为30ms
		public decimal EachStepTime2 = 0.03m; //默认情况下，步时间默认值为0.03s（=30ms）【不为static的原因是，这个在软件运行时可能会发生改变。】
		protected string groupIniPath; // 存放编组文件存放路径
		public IList<GroupAst> GroupList; // 存放编组	
		protected ActionForm actionForm; //存储一个全局的actionForm（这样可以记录之前使用过的材料）
		protected ColorForm colorForm; // 存储一个全局的colorForm，以便使用之前数据；
		public DetailMultiAstForm DmaForm; //存储一个全局的DetailMultiAstForm，用以记录之前用户选过的将进行多步联调的通道
		public Dictionary<int, List<int>> TdDict; // 存储一个字典，在DmaForm中点击确认后，修改这个数据

		//MARK 只开单场景：00.2 ①必须有一个存储所有场景是否需要保存的bool[];②若为true，则说明需要保存
		protected bool[] frameSaveArray;
		//MARK 只开单场景：00.3 ①必须有一个存储所有场景数据是否已经由DB载入的bool[];②若为true，则说明不用再从数据库内取数据了
		protected bool[] frameLoadArray;
		//MARK 只开单场景：14.0 为处理灯具列表变动，必须有一个存储[保留的旧灯具index]的列表，若非列表内的灯具，则应清除相关的DB数据（包括StepCount表及Value表）
		protected IList<int> retainLightIndices;
		
		// 数据库DAO(data access object：数据访问对象）
		protected LightDAO lightDAO;
		protected StepCountDAO stepCountDAO;
		protected ValueDAO valueDAO;
		protected FineTuneDAO fineTuneDAO;

		// 这几个IList ，存放着所有数据库数据		
		protected DBWrapper dbWrapperTemp;
		protected IList<DB_Light> dbLightList;
		protected IList<DB_FineTune> dbFineTuneList;
		protected IList<DB_StepCount> dbStepCountList;

		public IList<LightAst> LightAstList;  //与《灯具编辑》通信用的变量；同时也可以供一些辅助form读取相关灯具的简约信息时使用 --> 这张表需要给多步联调使用（sawList）
		public IList<LightWrapper> LightWrapperList;   //灯具变量：记录所有灯具（lightWrapper）的（所有场景和模式）的 每一步（通道列表）
		protected Dictionary<int, int> lightDictionary;   //辅助灯具字典，用于通过pk，取出相关灯具的index（供维佳生成数据调用）

		// 通道数据操作时的变量		
		protected bool isSyncMode = false;  // 同步模式为true；异步模式为false(默认）	
		public bool IsMultiMode = false; //默认情况下是单灯模式；若进入多灯模式，此变量改成true；
		protected bool isCopyAll = false;   // 11.20 新功能：多灯模式仍需要一个变量 ，用以设置是否直接用组长的数据替代组员。（默认情况下应该设为false，可以避免误删步数信息）

		protected int selectedIndex = -1; //选择的灯具的index，默认为-1，如有选中灯具，则改成该灯具的index（在lightAstList、lightWrapperList中）
		public IList<int> SelectedIndices; //选择的灯具的index列表（多选情况下）

		public int CurrentFrame = 0; // 表示场景编号(selectedIndex )
		public int CurrentMode = 0;  // 表示模式编号（selectedIndex)；0.常规模式； 1.音频模式

		protected StepWrapper tempStep = null; //// 辅助步变量：复制及粘贴步时用到		
		protected bool from0on = false; // 辅助变量，避免重复渲染子属性按钮组

		// 调试变量
		protected BaseCommunication myConnect;  // 与设备的连接（串口、网口）
		protected PlayTools playTools = PlayTools.GetInstance(); //DMX512灯具操控对象的实例：（20200515）只做预览
		protected bool isConnectCom = true; //默认情况下，用串口连接设备。
		protected IList<NetworkDeviceInfo> networkDeviceList; //记录所有的device列表(包括连接的本地IP和设备信息，故如有多个同网段IP，则同一个设备可能有多个列表值)
		public bool IsConnected = false; // 辅助bool值，当选择《连接设备》后，设为true；反之为false
		protected bool isKeepOtherLights = false;  // 辅助bool值，当选择《（非调灯具)保持状态》时，设为true；反之为false
		public bool IsPreviewing = false; // 是否预览状态中
		protected ImageList lightImageList;
		protected bool generateNow = true; // 是否立即处理（indexSelectedChanged）
			

		#region 几个纯虚（virtual修饰）方法：主要供各种基类方法向子类回调使用				
		protected virtual void enableProjectRelative(bool enable) { } // 是否显示《保存工程》等
		protected virtual void autoEnableSLArrange() { } //自动显示《 存、取 灯具位置》		
		protected virtual void showPlayPanel(bool visible) { }// 是否显示PlayFlowLayoutPanel
		protected virtual void enableRefreshPic(bool enable) { } // 是否使能《重新加载灯具图片》
		protected virtual void setBusy(bool buzy) { } //设置是否忙时
		protected virtual void editLightInfo(LightAst la) { }  //显示灯具详情到面板中		
		protected virtual void generateSaPanels() { } // 实时生成并显示相应的子属性面板
		protected virtual void enableStepPanel(bool enable) { } //是否使能步数面板
		protected virtual void showTDPanels(IList<TongdaoWrapper> tongdaoList, int startNum) { } //通过传来的数值，生成通道列表的数据
		protected virtual void hideAllTDPanels() { } //隐藏所有通道
		protected virtual void showStepLabel(int currentStep, int totalStep) { } //显示步数标签，并判断stepPanel按钮组是否可用		
		protected virtual void initStNumericUpDowns() { }  // 初始化工程时，需要初始化其中的步时间控件的参数值		
		protected virtual void changeCurrentFrame(int frameIndex) { } //MARK 只开单场景：02.0 改变当前Frame
		protected virtual void RefreshMultiModeButtons(bool isMultiMode) { }  //进入或退出多灯模式后的相关操作（设置各个按键的可用性）
		protected virtual void reBuildLightListView() { } //根据现有的lightAstList，重新渲染listView
		protected virtual void refreshGroupPanels() { } // 从groupList重新生成相关的编组列表的panels
		protected virtual void selectLights() { } // 选中列表中的灯具；且必须在这个方法内，跑一次generateLightData或generateSAButtons			
		protected virtual void deviceRefresh() { } //	刷新设备列表
			
		public virtual void SetPreview(bool preview) { }  // 主要供预览失败或成功使用，各子Form更改相应的显示
		protected virtual void setMakeSound(bool makeSound) { } // 点击触发音频后，各子Form更改相应的显示
		public virtual void EnterSyncMode(bool isSyncMode) { } // 设置是否 同步模式
		public virtual void SetNotice(string notice,bool msgBoxShow) { } //设置提示信息（有些重要提示，则需弹窗）
		public virtual void EnableConnectedButtons(bool connected,bool previewing) {
			//Console.WriteLine("EnableConnectedButtons("+connected+","+previewing+")");
			// 是否连接,是否预览中
			IsConnected = connected;
			IsPreviewing = previewing;		
		} //设置《连接按钮组》是否可用	

		protected virtual void enterMultiMode() { }
		protected virtual void exitMultiMode() { }

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

		/// <summary>
		/// 辅助方法：（供DetailMultiForm等调用）返回当前灯具某个通道的封装类；
		/// </summary>
		/// <returns></returns>
		internal SAWrapper GetSeletecdLightTdSaw(int lightIndex, int tdIndex) {

			if (LightAstList ==null || LightAstList.Count == 0 || lightIndex == -1) {				
				return null;
			}
			
			return LightAstList[lightIndex].SawList[tdIndex] ;
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
				string destPath = SavePath + @"\Source\LightProject\" + currentProjectName;
				di = new DirectoryInfo(destPath);
				di.Create();
				DirectoryHelper.CopyDirectory(projectPath, destPath);

				if (LightAstList != null && LightAstList.Count > 0) {
					string lightLibPath = SavePath + @"\Source\LightLibrary";
					di = new DirectoryInfo(lightLibPath);
					di.Create();

					HashSet<string> lightSet = new HashSet<string>();
					HashSet<string> dirSet = new HashSet<string>();
					HashSet<string> picSet = new HashSet<string>();
					foreach (LightAst la in LightAstList)
					{
						dirSet.Add(la.LightName);
						if (!string.IsNullOrEmpty(la.LightPic)) {
							picSet.Add(la.LightPic);
						}
						lightSet.Add(la.LightName + "\\" + la.LightType + ".ini");
					}

					foreach (string libDir in dirSet)
					{
						di = new DirectoryInfo(SavePath + @"\Source\LightLibrary\" + libDir);
						di.Create();
					}
					foreach (string lightPath in lightSet) {
						File.Copy(SavePath + @"\LightLibrary\" + lightPath, SavePath + @"\Source\LightLibrary\" + lightPath, true);
					}
					// 1124：连带把灯具图片也保存起来
					di = new DirectoryInfo(SavePath + @"\Source\LightPic");
					di.Create(); 
					foreach (string lightPic in picSet) {
						File.Copy(SavePath + @"\LightPic\" + lightPic, SavePath +  @"\Source\LightPic\" + lightPic, true);
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
		/// 辅助方法：在《在全局配置》中改变了时间因子并保存后，mainForm的时间因子变量也跟着改变，同时刷新当前步
		/// </summary>
		public void ChangeEachStepTime(int eachStepTime)
		{
			this.eachStepTime = eachStepTime;
			this.EachStepTime2 = eachStepTime / 1000m;
			RefreshStep();
		}

		/// <summary>
		///  辅助方法：判断是否可以显示 playPanel及 刷新图片(主要供《打开工程》和《灯具列表Form》使用），故可以在BuildLightList中使用
		///  --这两个功能都依赖于当前Form中的lightAstList是否为空。
		/// </summary>
		protected void autosetEnabledPlayAndRefreshPic()
		{
			bool enable = LightAstList != null && LightAstList.Count > 0;
			showPlayPanel(enable);
			enableRefreshPic(enable);
		}

		/// <summary>
		/// MARK 重构BuildLightList：BuildLightList改名为ReBuildLightList()，并且是完整的方法，不再需要子类完成剩余部分；只是会调用子类的reBuildLightListView()
		/// 辅助方法：添加新的lightAst列表到主界面内存中,只供 LightsForm调用）
		/// </summary>
		public void ReBuildLightList(IList<LightAst> lightAstList2)
		{
			List<LightWrapper> lightWrapperList2 = new List<LightWrapper>();

			// 0907 保留下来的灯具的旧索引（左Key）及新索引（右Value）；
			Dictionary<int, int> retainDict = new Dictionary<int, int>();

			//MARK 只开单场景：14.1 ReBuildLightList()方法体内，对retainLightIndices进行初始化
			retainLightIndices = new List<int>();
			for (int newIndex = 0; newIndex < lightAstList2.Count; newIndex++) {
				// 如果addOld改成true，则说明lighatWrapperList2已添加了旧数据，否则就要新建一个空LightWrapper。
				bool addOld = false;
				if (LightWrapperList != null && LightWrapperList.Count > 0)
				{
					for (int oldIndex = 0; oldIndex < LightAstList.Count; oldIndex++)
					{
						if ( oldIndex < LightWrapperList.Count
							&&  lightAstList2[newIndex].Equals(LightAstList[oldIndex])
							&& LightWrapperList[oldIndex] != null)
						{							
							lightWrapperList2.Add(LightWrapperList[oldIndex]);
							addOld = true;
							//MARK 只开单场景：14.2 ReBuildLightList()方法体内，为retainLightIndices添加旧灯具的数据
							retainLightIndices.Add(lightAstList2[newIndex].StartNum);
							retainDict.Add(oldIndex, newIndex);
							break;
						}
					}
				}
				if (!addOld)
				{
					//Console.WriteLine("Dickov : 添加了一个全新的LightWrapper["  + lightAstList2[i].LightName + ":" + lightAstList2[i].LightType + "(" + lightAstList2[i].LightAddr+ ")]，但还没有生成StepTemplate。");
					lightWrapperList2.Add(new LightWrapper() { StepTemplate = generateStepTemplate(lightAstList2[newIndex]) });
				}
			}

			LightAstList = new List<LightAst>(lightAstList2);
			LightWrapperList = new List<LightWrapper>(lightWrapperList2);
			lightDictionary = new Dictionary<int, int>();
			disposeDmaForm();  // 需要把DmaForm重置，因为灯具列表(可能)发生了变化

			for (int lightIndex = 0; lightIndex < LightAstList.Count; lightIndex++)
			{
				lightDictionary.Add(LightAstList[lightIndex].StartNum, lightIndex);
			}

			//disposeSauForm();
			selectedIndex = -1;
			SelectedIndices = new List<int>();		

			//MARK 只开单场景：15.0 BuildLightList时，一定要清空selectedIndex及selectedIndices,否则若删除了该灯具，则一定会出问题！		
			EnterSyncMode(false); // 修改了灯具后，一定要退出同步模式
			enableProjectRelative(true);    //ReBuildLightAst内设置
			autosetEnabledPlayAndRefreshPic();
			reBuildLightListView();

			//出现了个Bug：选中灯具后，在灯具列表内删除该灯具（或其他？），则内存内选中的灯和点击追加步之类的灯具可能会不同，故直接帮着选中第一个灯具好了
			if (LightAstList != null && LightAstList.Count > 0)
			{
				selectedIndex = 0;
			}
			generateLightData();
			
			// 处理编组列表
			IList<GroupAst> newGroupList = new List<GroupAst>();
			//取出每个编组，并分别进行处理
			foreach (GroupAst group in GroupList)
			{
				// 处理组员,直接用一个新的List来进行存储；
				IList<int> newIndexList = new List<int>();
				foreach ( int oldIndex in group.LightIndexList) {									
					if (retainDict.ContainsKey(oldIndex))
					{
						newIndexList.Add( retainDict[oldIndex]);	
					}
				}
				if (newIndexList.Count != 0) {
					// 处理组长
					if (retainDict.ContainsKey(group.CaptainIndex))
					{
						group.CaptainIndex = retainDict[group.CaptainIndex];
					}
					else
					{
						group.CaptainIndex = retainDict[0];  // 如果组长已经被删了，则直接设为保留下来的第一个灯具
					}
					group.LightIndexList = newIndexList;
					newGroupList.Add(group);
				}				
			}
			// 最后刷新界面显示
			GroupList = newGroupList;
			refreshGroupPanels(); // ReBuildLightList()
		}

		/// <summary>
		/// 辅助方法：刷新 灯具图片的方法
		/// </summary>
		protected void RefreshLightImageList()
		{

			// 清空所有的图片,并添加默认图片
			lightImageList.Images.Clear();
			lightImageList.Images.Add("灯光图.png", global::LightController.Properties.Resources.灯光图);

			string picPath = SavePath + @"\LightPic";
			DirectoryInfo di = new DirectoryInfo(picPath);
			if (di.Exists)
			{
				foreach (FileInfo f in di.GetFiles())
				{
					if (StringHelper.IsPicFile(f.Name))
					{
						lightImageList.Images.Add(f.Name, Image.FromFile(f.FullName));
					}
				}
			}
		}

		//辅助方法：摧毁DmaForm，同时也将TdDict置为null
		private void disposeDmaForm()
		{
			if (DmaForm != null) {
				DmaForm.Dispose();			
				DmaForm = null;
				TdDict = null; 
			}
		}

		/// <summary>
		/// 辅助方法：生成模板Step --》 之后每新建一步，都复制模板step的数据。
		/// --这个方法内集成了生成模板步、子属性列表等数据；
		/// </summary>
		/// <param name="lightAst"></param>
		/// <param name="lightIndex"></param>
		/// <returns></returns>
		protected StepWrapper generateStepTemplate(LightAst lightAst)
		{
			//Console.WriteLine("Dickov :为 " + lightAst.LightName + ":" + lightAst.LightType + "(" + lightAst.LightAddr + ")生成模板文件(StepTemplate)：");
			try {
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
						IniFileHelper iniAst = new IniFileHelper(lightAst.LightPath);
						lightAst.SawList = new List<SAWrapper>();

						for (int tdIndex = 0; tdIndex < tongdaoCount; tdIndex++)
						{
							string tongdaoName = lineList[3 * tdIndex + 6].ToString().Substring(4);
							int initNum = int.Parse(lineList[3 * tdIndex + 7].ToString().Substring(4));
							int address = int.Parse(lineList[3 * tdIndex + 8].ToString().Substring(4));			
							// 备注中加入各子属性的数值							
							string remark = tongdaoName + "\n";
							IList<SA> saList = new List<SA>();
							for (int saIndex = 0; saIndex < iniAst.ReadInt("sa", tdIndex + "_saCount", 0); saIndex++)
							{
								string saName = IniFileHelper_UTF8.ReadString(lightAst.LightPath, "sa", tdIndex + "_" + saIndex + "_saName", "");
								int startValue = iniAst.ReadInt("sa", tdIndex + "_" + saIndex + "_saStart", 0);
								int endValue = iniAst.ReadInt("sa", tdIndex + "_" + saIndex + "_saEnd", 0);
								remark += saName + " : " + startValue + " - " + endValue + "\n";
								saList.Add(new SA{ SAName = saName, StartValue = startValue, EndValue = endValue });
							}
							lightAst.SawList.Add(new SAWrapper() { SaList = saList });

							tongdaoList.Add(new TongdaoWrapper()
							{
								TongdaoName = tongdaoName,
								ScrollValue = initNum,
								StepTime = 50,
								ChangeMode = -1,
								Address = lightAst.StartNum + (address - 1),
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
		public DBWrapper GetDBWrapper(bool isFromDB)
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
				//long time = DateTime.Now.Ticks;
				
				// 先生成最新的 dbLightList,dbStepCountList, dbValueList 数据
				generateDBLightList();
				generateDBFineTuneList();
				//MARK 只开单场景：12.0 GetDBWrapper中，重写generateDBStepCountList(); 【重新生成内存中的dbStepCountList】
				generateDBStepCountList();
				IList<DB_Value> dbValueListTemp = generateDBValueList(CurrentFrame);

				DBWrapper allData = new DBWrapper(dbLightList, dbStepCountList, dbValueListTemp, dbFineTuneList);

				//long useTime = (DateTime.Now.Ticks - time) / 10000 ;
				//Console.WriteLine("GetDBWrapper(false) useTime : " + useTime );

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
			if (LightAstList == null || LightAstList.Count == 0)
			{
				return;
			}

			foreach (LightAst la in LightAstList)
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
			if (LightAstList == null || LightAstList.Count == 0) {
				return;
			}

			// 遍历lightWrapperList的模板数据，用以读取相关的通道名称，才能加以处理			
			foreach (LightWrapper lightWrapper in LightWrapperList)
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
			if (LightAstList == null || LightAstList.Count == 0)
			{
				return;
			}

			//MARK 200908 优化generateDBStepCountList()的速度
			dbStepCountList = stepCountDAO.GetAllWithList(frameLoadArray);

			// 取出每个灯具的所有【非null】stepCount,填入stepCountList中
			foreach (LightWrapper lightTemp in LightWrapperList)
			{
				DB_Light light = dbLightList[LightWrapperList.IndexOf(lightTemp)];
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

						//MARK 只开单场景：12.2 generateDBStepCountLit()重写：加载过的场景用内存数据
						if (frameLoadArray[frameIndex])
						{
							LightStepWrapper lsTemp = allLightStepWrappers[frameIndex, mode];
							//MARK 只开单场景：12.2.1 generateDBStepCountLit()重写：若加载过的场景，此灯具已被选中过，则lsTemp!=null,这时可以添加到dbStepCountList中去；若为null，则不加
							if (lsTemp != null)
							{
								DB_StepCount stepCount = new DB_StepCount()
								{
									StepCount = lsTemp.TotalStep,
									PK = stepCountPK
								};
								dbStepCountList.Add(stepCount);
							}
							#region 弃用（之前有这段代码，是维佳那边若此处为空，程序有个地方出现了Bug，故需进行处理）
							//MARK 只开单场景：12.2.2 (弃用)generateDBStepCountLit()重写：若加载过的场景，此灯具并未被选中过，则其lsTemp为空,需要主动封装一个stepCount=0的DB_StepCount实例并加到dbStepCountList中去
							//else {
							//	DB_StepCount stepCount = new DB_StepCount()
							//	{
							//		StepCount =0,
							//		PK = stepCountPK
							//	};
							//	dbStepCountList.Add(stepCount);
							//}				
							#endregion
						}
						////MARK 只开单场景：12.3 generateDBStepCountLit()重写：未加载过的场景用DB数据
						//else
						//{
						//	DB_StepCount sc = stepCountDAO.GetStepCountByPK(stepCountPK);
						//	if (sc != null)
						//	{
						//		dbStepCountList.Add(sc);
						//	}
						//}
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
			if (LightAstList != null && LightAstList.Count > 0)
			{
				foreach (LightWrapper lightTemp in LightWrapperList)
				{
					DB_Light light = dbLightList[LightWrapperList.IndexOf(lightTemp)];
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

			// 选择《追加》时，不更改核心代码 : 而是先选择最后步(0步则不走这个)，再设置method = insert
			if (method == InsertMethod.APPEND) {
				if (totalStep != 0 && currentStep!=totalStep) {
					chooseStep(totalStep);
				}
				method = InsertMethod.INSERT;
			}

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
					newStep = StepWrapper.GenerateNewStep(stepTemplate, CurrentMode);
					// 改造下newStep,将素材值赋给newStep 
					changeStepFromMaterial(materialAst.TongdaoList, stepIndex, sameTDIndexList, newStep);
					// 使用后插法：避免当前无数据的情况下调用素材失败
					lsWrapper.InsertStep(lsWrapper.CurrentStep - 1, newStep, false);
				}

				if (IsMultiMode)
				{
					foreach (int lightIndex in SelectedIndices)
					{
						if (lightIndex != selectedIndex)
						{
							// 多灯模式下，依然使用上面的步骤来插入素材。
							for (int stepIndex = 0; stepIndex < addStepCount; stepIndex++)
							{
								newStep = StepWrapper.GenerateNewStep(getSelectedLightStepTemplate(lightIndex), CurrentMode);
								changeStepFromMaterial(materialAst.TongdaoList, stepIndex, sameTDIndexList, newStep);
								getSelectedLightStepWrapper(lightIndex).InsertStep(getSelectedLightStepWrapper(lightIndex).CurrentStep - 1, newStep, false);
							}
						}
					}
				}

				if (isSyncMode)
				{
					foreach (int lightIndex in getNotSelectedIndices())
					{
						for (int stepIndex = 0; stepIndex < addStepCount; stepIndex++)
						{
							newStep = StepWrapper.GenerateNewStep( getSelectedLightStepTemplate(lightIndex), CurrentMode);
							getSelectedLightStepWrapper(lightIndex).InsertStep(getSelectedLightStepWrapper(lightIndex).CurrentStep - 1, newStep, false);
						}
					}
				}

				RefreshStep();
			}
			// 选择覆盖时的操作：后插法
			//（当前步也要被覆盖，除非没有当前步-》totalStep == currentStep == 0）
			else if (method == InsertMethod.COVER)
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
						newStep = StepWrapper.GenerateNewStep(stepTemplate, CurrentMode);
						lsWrapper.AddStep(newStep);
					}
				}

				// 在步数都已经存在的情况下，用素材替换掉相关步（相应通道）
				for (int stepIndex = currentStep - 1, materialStepIndex = 0; stepIndex < finalStep; stepIndex++, materialStepIndex++)
				{
					changeStepFromMaterial(materialAst.TongdaoList, materialStepIndex, sameTDIndexList, lsWrapper.StepWrapperList[stepIndex]);
					//newStep = lsWrapper.StepWrapperList[stepIndex];
				}

				if (IsMultiMode)
				{
					foreach (int lightIndex in SelectedIndices)
					{
						if (lightIndex != selectedIndex)
						{
							if (finalStep > totalStep)   // （超过总步数的那些步，则需要添加新步,以凑满步数）
							{
								for (int i = 0; i < finalStep - totalStep; i++)
								{
									newStep = StepWrapper.GenerateNewStep(getSelectedLightStepTemplate(lightIndex), CurrentMode);
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

				if (isSyncMode)
				{
					foreach (int lightIndex in getNotSelectedIndices())
					{
						for (int i = 0; i < finalStep - totalStep; i++)
						{
							// 只有超过了当前步数，才需要addStep,故取当前步或最大步皆可。
							newStep = StepWrapper.GenerateNewStep(getSelectedLightCurrentStepWrapper(lightIndex), CurrentMode);
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
			if (lightIndex == -1 || LightWrapperList == null || LightWrapperList.Count == 0)
			{
				return null;
			}
			return LightWrapperList[lightIndex];
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

			if (LightWrapperList == null || LightWrapperList.Count == 0) {
				return null;
			}

			LightWrapper lightWrapper = LightWrapperList[lightIndex];
			if (lightWrapper == null) {
				return null;
			}
			else
			{
				//若为空，则立刻创建一个
				if (lightWrapper.LightStepWrapperList[CurrentFrame, CurrentMode] == null)
				{
					lightWrapper.LightStepWrapperList[CurrentFrame, CurrentMode] = new LightStepWrapper()
					{
						StepWrapperList = new List<StepWrapper>()
					};
				};
				return lightWrapper.LightStepWrapperList[CurrentFrame, CurrentMode];
			}
		}

		/// <summary>
		/// 辅助方法：取出指定灯具的当前F/M的步数
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
		/// <param name="lightIndex">灯具索引值(从0开始算)</param>
		/// <param name="stepIndex">步数索引值(从0开始算)</param>
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
				return lightWrapper.StepTemplate;
			}
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
		///  辅助方法：取出当前灯在本F/M下的的步数
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
			LightWrapper lightWrapper = LightWrapperList[lightIndex];
			if (lightWrapper == null || lightWrapper.LightStepWrapperList[CurrentFrame, CurrentMode] == null) {
				return null;
			}

			int currentStep = lightWrapper.LightStepWrapperList[CurrentFrame, CurrentMode].CurrentStep;
			int totalStep = lightWrapper.LightStepWrapperList[CurrentFrame, CurrentMode].TotalStep;

			// 当前步或最大步某一种为0的情况下，返回null
			if (currentStep == 0 || totalStep == 0)
			{
				return null;
			}
			else
			{
				return lightWrapper.LightStepWrapperList[CurrentFrame, CurrentMode].StepWrapperList[currentStep - 1];
			}
		}

		#endregion

		/// <summary>
		///  辅助方法：供《多步(多通道)调节》使用
		/// </summary>
		/// <param name="tdIndexList">要设置的通道Index列表，从0开始</param>
		/// <param name="startStep">开始步</param>
		/// <param name="endStep">结束步</param>
		/// <param name="where">统一设置的属性</param>
		/// <param name="stepPos">全部步0 ;单数步1、双数步2</param>
		/// <param name="unifyValue">统一要设的值，如果是跳渐变则为其索引</param>
		public void SetMultiStepValues(WHERE where, IList<int> tdIndexList, int startStep, int endStep, int stepPos, int unifyValue) {

			// 多灯模式，将值赋给每个编组的灯具中
			if (IsMultiMode)
			{
				foreach (int lightIndex in SelectedIndices)
				{
					LightStepWrapper lsWrapper = getSelectedLightStepWrapper(lightIndex);
					for (int stepIndex = startStep - 1; stepIndex < endStep; stepIndex++)
					{
						if (stepPos == 0 || (stepPos == 1 && stepIndex % 2 == 0) || (stepPos == 2 && stepIndex % 2 != 0 ) )
						{
							lsWrapper.StepWrapperList[stepIndex].MultiChangeValue(where, tdIndexList, unifyValue);
						}			
					}
				}
			}
			// 单灯模式，则只需更改当前灯具的数据即可。
			else {
				LightStepWrapper lightStepWrapper = getCurrentLightStepWrapper();
				for (int stepIndex = startStep - 1; stepIndex < endStep; stepIndex++)
				{
					if (stepPos == 0 || (stepPos == 1 && stepIndex % 2 == 0) || (stepPos == 2 && stepIndex % 2 != 0))
					{
						lightStepWrapper.StepWrapperList[stepIndex].MultiChangeValue(where, tdIndexList, unifyValue);
					}
				}
			}
			
			// 改完数值后再刷新步
			RefreshStep();
		}

		/// <summary>
		///  辅助方法：供《多步联调》内修改部分步数值时使用；
		/// </summary>
		/// <param name="tdIndex"></param>
		/// <param name="stepIndex"></param>
		public void SetTdStepValue(int selectedLightIndex, int tdIndex, int stepIndex, int stepValue ,bool isJumpStep)
		{
			//判断传入的 stepIndex是否在范围内，超过的话，直接return( tdIndex 不需验证：因为是mainForm传过去的)
			LightStepWrapper lsWrapper = getSelectedLightStepWrapper(selectedLightIndex);
			if ( stepIndex >= lsWrapper.TotalStep  )  {
				return;
			}

			// 多灯模式 且 所选灯具在当前的多灯组内，将值赋给每个编组的灯具中
			if (IsMultiMode && SelectedIndices.Contains(selectedLightIndex) )
			{
				foreach (int lightIndex in SelectedIndices)
				{
					getSelectedLightStepWrapper(lightIndex).StepWrapperList[stepIndex].TongdaoList[tdIndex].ScrollValue = stepValue;
				}
			} 
			// 单灯模式，则只需更改当前灯具的数据即可。
			else
			{
				getSelectedLightStepWrapper(selectedLightIndex).StepWrapperList[stepIndex].TongdaoList[tdIndex].ScrollValue = stepValue;
			}

			// 不跳步的话，只需刷新当前步数
			if (isJumpStep) {
				getSelectedLightStepWrapper(selectedLightIndex).CurrentStep = stepIndex + 1;
				if (!IsMultiMode && selectedIndex != selectedLightIndex)
				{
					selectedIndex = selectedLightIndex;
					generateLightData();
					return ; //generateLightData()代码中已包含RefreshStep，故如果运行后可直接return；不return的则由最后的RefreshStep()来收尾
				}				
			}					
			RefreshStep();
		}
				
		/// <summary>
		/// 9.16 辅助方法：进入多灯模式
		///		1.取出选中的组长，
		///		2.使用组长数据，替代其他灯具（在该F/M）的所有步数集合。
		/// </summary>
		/// <param name="captainIndex"></param>
		public virtual void EnterMultiMode(int captainIndex, bool isCopyAll)
		{
			this.isCopyAll = isCopyAll;
			// captainIndex 是组长在selectedIndices中的序号，可用之取出组长在[所有灯具]列表中的位置
			selectedIndex = SelectedIndices[captainIndex];
			if (isCopyAll)
			{
				LightStepWrapper mainLSWrapper = getSelectedLightStepWrapper(selectedIndex); //取出组长
				foreach (int index in SelectedIndices)
				{
					//通过组长生成相关的数据
					StepWrapper currentStepTemplate = LightWrapperList[index].StepTemplate;
					LightWrapperList[index].LightStepWrapperList[CurrentFrame, CurrentMode] = LightStepWrapper.GenerateLightStepWrapper(mainLSWrapper, currentStepTemplate, CurrentMode);
				}
			}
			RefreshStep();
		}

		/// <summary>
		/// 辅助方法：多灯模式中，利用此方法，将修改不多的组长数据（如部分通道值、渐变方式、步时间等），用此改动较少的方法，赋给所有的组员
		/// </summary>
		/// <param name="groupSelectedIndex"></param>
		protected void copyValueToAll(int tdIndex, WHERE where, int value)
		{
			LightStepWrapper mainLSWrapper = getCurrentLightStepWrapper(); //取出组长
			int currentStep = getCurrentStep();     // 取出组长的当前步
			foreach (int index in SelectedIndices)
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

			foreach (int index in SelectedIndices)
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

			foreach (int lightIndex in SelectedIndices)
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
			int firstIndex = SelectedIndices[0];
			int firstStepCounts = getSelectedLightStepCounts(firstIndex);
			foreach (int index in SelectedIndices)
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
			if (LightAstList == null || LightAstList.Count == 0) {
				MessageBox.Show("当前工程无灯具，检查灯具步数是否一致的操作无意义。");
				return false;
			}

			int firstStepCounts = getSelectedLightStepCounts(0);
			for (int lightIndex = 1; lightIndex < LightAstList.Count; lightIndex++)
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
		/// 辅助方法：获取当前工程未选中灯具=》主动判断是多灯还是单灯模式。
		/// </summary>
		/// <returns></returns>
		protected IList<int> getNotSelectedIndices()
		{
			IList<int> allIndices = new List<int>();
			for (int lightIndex = 0; lightIndex < LightWrapperList.Count; lightIndex++)
			{
				if (IsMultiMode)
				{
					if (!SelectedIndices.Contains(lightIndex))
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
		/// 辅助方法：由内存读取 IList<TongdaoWrapper> 拿出相关通道的TongdaoWrapper，取的是 某一场景 某一模式 某一通道 的所有步信息
		/// </summary>
		public IList<TongdaoWrapper> GetFMTDList(DB_ValuePK pk) {

			int selectedLightIndex = lightDictionary[pk.LightIndex];
			int tdIndex = pk.LightID - pk.LightIndex;
			IList<TongdaoWrapper> tdList = new List<TongdaoWrapper>();

			//MARK 只开单场景：10.1 GetFMTDList() 的实现改动，添加判断是否已加载的场景，若是则从内存读数据
			if (frameLoadArray[pk.Frame])
			{
				if (LightWrapperList[selectedLightIndex].LightStepWrapperList[pk.Frame, pk.Mode] != null
						&& LightWrapperList[selectedLightIndex].LightStepWrapperList[pk.Frame, pk.Mode].StepWrapperList != null)
				{
					IList<StepWrapper> stepWrapperList = LightWrapperList[selectedLightIndex].LightStepWrapperList[pk.Frame, pk.Mode].StepWrapperList;
					for (int step = 0; step < stepWrapperList.Count; step++)
					{
						if (stepWrapperList[step].TongdaoList != null && stepWrapperList[step].TongdaoList.Count > 0)
						{
							TongdaoWrapper tw = stepWrapperList[step].TongdaoList[tdIndex];
							tdList.Add(tw);
						}
					}
				}
			}
			//MARK 只开单场景：10.2 GetFMTDList() 的实现改动：添加判断是否已加载的场景，若否则从DB读数据
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
			new NewForm(this, CurrentFrame).ShowDialog();

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
		/// 基类辅助方法InitProject(打开或新建工程会用到)：①ClearAllData()；②设置内部的一些工程路径及变量；③初始化数据库
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
			Text = SoftwareName + "(当前工程:" + projectName + ")";

			//1.1设置当前工程的 arrange.ini 的地址,以及先把各种可用性屏蔽掉
			arrangeIniPath = projectPath + @"\arrange.ini";			

			//1.2 读取时间因子
			IniFileHelper iniAst = new IniFileHelper(GlobalIniPath);
			eachStepTime = iniAst.ReadInt("Set", "EachStepTime", 30);
			EachStepTime2 = eachStepTime / 1000m;
			initStNumericUpDowns();  //更改了时间因子后，需要处理相关的stepTimeNumericUpDown，包括tdPanel内的及unifyPanel内的

			// 1.3 加载groupList : 初始化时检查文件是否存在，不存在，则直接把默认文件拷贝过去；加载到内存后，通过相应的groupList刷新按钮
			groupIniPath = projectPath + @"\groupList.ini";
			if (!File.Exists(groupIniPath)) {
				File.Copy(Application.StartupPath + @"\groupList.ini", groupIniPath);
			}
			GroupList = GroupAst.GenerateGroupList(groupIniPath);
			refreshGroupPanels(); //InitProject()

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

			//MARK 只开单场景：04.0 InitProject()内 ①修改当前场景；②初始化frameSaveArray、frameLoadArray
			//   --> 都先设为false;并将frameSaveArray[selectedFrameIndex]为true，因为只要打开了场景（New或Open）其frameSaveArray[selectedFrame]一定要设为true的！
			//   -->（原则：当前打开的场景点击保存时一定要保存，因为在此处可能进行更改数据）
			//MARK 重构BuildLightList：初始化工程（打开或新建工程）时，一定会选一个初始的场景，故可以将frameLoadArray[selectedFrame]也设为true）
			changeCurrentFrame(selectedFrameIndex);
			frameSaveArray = new bool[FrameCount];
			frameLoadArray = new bool[FrameCount];
			for (int frameIndex = 0; frameIndex < FrameCount; frameIndex++)
			{
				frameSaveArray[frameIndex] = frameIndex == selectedFrameIndex;
				frameLoadArray[frameIndex] = frameIndex == selectedFrameIndex;
			}

			enableProjectRelative(true);    // InitProject()时设置，各按键是否可用
		}

		/// <summary>
		/// 辅助方法： 清空相关的所有数据（关闭工程、新建工程、打开工程都会用到）
		/// -- 子类中需有针对该子类内部自己的部分代码（如重置listView或禁用stepPanel等）
		/// </summary>
		protected virtual void clearAllData()
		{	
			disConnect(); //clearAllData() 【原来的代码只是停止预览，用断开连接更完善】

			currentProjectName = null;

			dbLightList = null;
			dbFineTuneList = null;
			dbStepCountList = null;
			GroupList = null;

			LightAstList = null;
			LightWrapperList = null;
			lightDictionary = null;

			selectedIndex = -1;
			SelectedIndices = new List<int>();

			disposeDmaForm();

			tempStep = null;
			TempMaterialAst = null;

			arrangeIniPath = null;
			groupIniPath = null ;

			//clearSaPanelArray();

			//MARK 只开单场景：03.0 clearAllData()内清空frameSaveArray、frameLoadArray
			frameSaveArray = null;
			frameLoadArray = null;

			Text = SoftwareName;

			EnterSyncMode(false);  //退出《同步模式》
			RefreshMultiModeButtons(false); // 刷新为单灯状态的按钮可用性
			autoEnableSLArrange();  // 《保存|读取灯具位置》不可用
			enableProjectRelative(false);  // clearAllData()内：工程相关的所有按钮，设为不可用
			autosetEnabledPlayAndRefreshPic();  //是否可以显示 playPanel及 刷新图片
			refreshGroupPanels(); //clearAllData()

			hideAllTDPanels();
			showStepLabel(0, 0);
		}

		/// <summary>
		/// MARK 只开单场景：14.3 clearRedundantData()方法体：清空不在retainLightIndices内的DB数据，包括StepCount表及Value表
		/// 辅助方法：清空不在retainLightIndices内的DB数据，包括StepCount表及Value表
		/// </summary>
		protected virtual void deleteRedundantData()
		{
			//Console.WriteLine(retainLightIndices);
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
			if (!RequestSaveProject("打开工程前，是否保存当前工程？", false)) {
				return;
			}
			new OpenForm(this, CurrentFrame, currentProjectName).ShowDialog();
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
		public void OpenProject(string savePath , string projectName, int frameIndex)
		{
			SetNotice("正在打开工程，请稍候...",false);
			setBusy(true);

			//MARK1124：OpenProject内当工作
			if (savePath != SavePath) {
				SavePath = savePath;
				RefreshLightImageList();
			}

			DateTime beforeDT = System.DateTime.Now;

			// 0.初始化
			InitProject(projectName, frameIndex, false);

			// 设置listView右键菜单中读取位置配置的可用项	
			autoEnableSLArrange();

			// 把各数据库表的内容填充进来。
			dbLightList = getLightList();
			//10.17 此处添加验证 : 如果是空工程(无任何灯具可认为是空工程)，后面的数据无需读取。
			if (dbLightList == null || dbLightList.Count == 0)
			{
				DialogResult dr = MessageBox.Show("成功打开空工程：" + projectName + "  , 要为此工程添加灯具吗？",
					"",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Question);
				SetNotice("成功打开空工程(" + projectName + ")",false);
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

				LightAstList = new List<LightAst>();
				//MARK 重构BuildLightList：原来OpenProject内用BuildLightList() --> 现把相关代码都放在方法块内
				LightWrapperList = new List<LightWrapper>();
				lightDictionary = new Dictionary<int, int>();

				try
				{
					for (int lightIndex = 0; lightIndex < dbLightList.Count; lightIndex++)
					{
						LightAst la = LightAst.GenerateLightAst(dbLightList[lightIndex], SavePath);
						LightAstList.Add(la);
						LightWrapperList.Add(new LightWrapper()
						{
							StepTemplate = generateStepTemplate(la)
						});
						lightDictionary.Add(la.StartNum, lightIndex);						
						//saFormList.Add(new SAUseForm(this, la));
					}
				}
				catch (Exception ex) {
					MessageBox.Show("加载工程时发生异常，可能是部分灯库文件已丢失。\n(" + ex.Message + ")");
					clearAllData();
					setBusy(false);
					return;
				}
								
				EnterSyncMode(false); //需要退出同步模式
				enableProjectRelative(true);    //OpenProject内设置
				autosetEnabledPlayAndRefreshPic(); 
				reBuildLightListView();

				//MARK 只开单场景：07.0 generateFrameData():在OpenProject内调用
				generateFrameData(CurrentFrame);

				DateTime afterDT = System.DateTime.Now;
				TimeSpan ts = afterDT.Subtract(beforeDT);
								
				SetNotice("成功打开工程：【" + projectName + "】" +
					"，耗时: " + ts.TotalSeconds.ToString("#0.00") + " s" +
					"。", true);
			}
			setBusy(false);

			// 打开工程后，主动帮用户选择第一个灯具
			if (LightAstList != null && LightAstList.Count > 0)
			{
				selectedIndex = 0;
			}
			generateLightData();
		}

		/// <summary>
		/// MARK 只开单场景：07.1 generateFrameData(int):从DB读Frame数据的代码（多线程）
		/// 辅助方法：通过传入frame的值，来读取相关的Frame场景数据（两种mode）
		/// </summary>
		/// <param name="frameIndex"></param>
		protected void generateFrameData(int selectedFrameIndex)
		{
			//MARK 重构BuildLightList：generateFrameData()内加dbLightList空值验证
			if (dbLightList == null || dbLightList.Count == 0) {
				return;
			}

			//MARK：generateFrameData(int)采用多线程方法优化(每个灯开启一个线程)
			Thread[] threadArray = new Thread[dbLightList.Count];
			for (int lightListIndex = 0; lightListIndex < dbLightList.Count; lightListIndex++)
			{
				int tempLightIndex = lightListIndex; // 必须在循环内使用一个临时变量来记录这个index，否则线程运行时lightListIndex会发生变化。
				int tempLightNo = dbLightList[tempLightIndex].LightNo;   //记录了数据库中灯具的起始地址（不同灯具有1-32个通道，但只要是同个灯，就公用此LightNo)				

				//MARK 只开单场景：07.2 generateFrameData(int)内:修改要取的步数（由列表[全部]->列表[当前场景的两个模式]；因为都是IList<DB_StepCount>,故后面的代码无需大改。
				//IList<DB_StepCount> scList = stepCountDAO.GetStepCountList(tempLightNo); //取出数据库内的步数列表		// 旧版弃用代码，取的是该灯具全部的stepCount数据，优化后只取指定场景的stepCount数据
				IList<DB_StepCount> scList = stepCountDAO.GetStepCountListByFrame(tempLightNo, selectedFrameIndex);

				//MARK 只开单场景：07.3 generateFrameData(int)内:取出相应的灯具该场景的所有dbValue数据，
				// IList<DB_Value> tempDbValueList = valueDAO.GetByLightNo(tempLightNo); //旧版弃用代码：取全部，优化后只取指定场景的部分value数据
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

							LightWrapperList[tempLightIndex].LightStepWrapperList[frame, mode] = new LightStepWrapper();

							for (int step = 1; step <= stepCount; step++)
							{
								IList<DB_Value> stepValueListTemp = tempDbValueList.Where(t => t.PK.LightIndex == lightIndex && t.PK.Frame == frame && t.PK.Mode == mode && t.PK.Step == step).ToList<DB_Value>();
								//当找到的stepValueListTemp ①不为空；②通道数量与模板相同 时，才继续往下走，否则不继续运行
								if (stepValueListTemp != null && stepValueListTemp.Count == LightWrapperList[tempLightIndex].StepTemplate.TongdaoList.Count) {
									StepWrapper stepWrapper = StepWrapper.GenerateStepWrapper(LightWrapperList[tempLightIndex].StepTemplate, stepValueListTemp, mode);
									if (stepWrapper != null) {
										LightWrapperList[tempLightIndex].LightStepWrapperList[frame, mode].AddStep(stepWrapper);
									}
								}
							}
						}
					}
					//Console.WriteLine(tempLightIndex + " -- 线程结束了");					
				});
				threadArray[tempLightIndex].Start();
			}

			// 下列代码，用以监视所有线程是否已经结束运行。每隔0.1s，去计算尚存活的线程数量，若数量为0，则说明所有线程已经结束了。
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
			//MARK 只开单场景：09.0 调用场景前，增加场景数量不够时情况的判断（==1）（0场景的情况软件不会打开，无需考虑）--》不进入useFrameForm；
			if (FrameCount == 1)
			{
				MessageBox.Show("软件中只存在一种场景，无法使用调用场景功能。");
				return;
			}

			//MARK 只开单场景：09.1 调用场景前，增加当前工程没有灯具数据时，不能使用此功能的校验
			if (LightAstList == null || LightAstList.Count == 0)
			{
				MessageBox.Show("当前工程没有灯具，无法使用调用场景功能。");
				return;
			}
			new UseFrameForm(this, CurrentFrame).ShowDialog();
		}

		/// <summary>
		/// 辅助方法：点击《保存场景》
		/// </summary>
		protected void saveFrameClick() {

			SetNotice("正在保存场景,请稍候...",false);
			setBusy(true);

			// 1.先判断是否有灯具数据；若无，则清空所有表数据
			if (LightAstList == null || LightAstList.Count == 0)
			{
				ClearAllDB();
			}
			// 2.保存各项数据，其中保存 灯具、FineTune 是通用的；StepCounts和Values直接用saveOrUpdate方式即可。
			else
			{
				saveAllLights();
				saveAllFineTunes();
				// 只保存当前场景（两种模式）的stepCount和value
				saveFrameSCAndValue(CurrentFrame);
				try
				{
					GroupAst.SaveGroupIni(groupIniPath, GroupList);
				}
				catch (Exception ex)
				{
					MessageBox.Show("保存编组数据出错：\n" + ex.Message);
				}
			}
						
			SetNotice("成功保存场景(" + AllFrameList[CurrentFrame] + ")",true);
			setBusy(false);
		}

		/// <summary>
		/// 辅助方法：点击《保存工程》
		/// </summary>
		protected void saveProjectClick() {
						
			SetNotice("正在保存工程,请稍候...",false);
			setBusy(true);

			DateTime beforeDT = System.DateTime.Now;
			// 1.先判断是否有灯具数据；若无，则清空所有表数据
			if (LightAstList == null || LightAstList.Count == 0)
			{
				ClearAllDB();
			}
			// 2.保存各项数据			
			else
			{
				saveAllLights();
				saveAllFineTunes();
				saveAllSCAndValues();
				try
				{
					GroupAst.SaveGroupIni(groupIniPath, GroupList);
				}
				catch (Exception ex) {
					MessageBox.Show("保存编组数据出错：\n" + ex.Message);
				}				
			}

			DateTime afterDT = System.DateTime.Now;
			TimeSpan ts = afterDT.Subtract(beforeDT);

			setBusy(false);
			SetNotice("成功保存工程:" + currentProjectName + ",耗时: " + ts.TotalSeconds.ToString("#0.00") + " s" , true);
		}

		/// <summary>
		///  辅助方法：右键点击《保存工程》将当前工程的相关文件，导出为Source.zip文件
		/// </summary>
		protected void exportSourceClick()
		{
			exportFolderBrowserDialog.Description = "即将为您导出当前工程的源文件（包括工程文件和相关灯具文件），并压缩为Source.zip；请选择源文件保存目录。";
			DialogResult dr = exportFolderBrowserDialog.ShowDialog();
			if (dr == DialogResult.Cancel)
			{
				return;
			}
			string exportPath = exportFolderBrowserDialog.SelectedPath;
			string zipPath = exportPath + @"\Source.zip";
			if (File.Exists(zipPath)) {
				dr =  MessageBox.Show("检测到该目录已存在一个Source.zip文件，是否覆盖？",
					"覆盖文件？",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Question);
				if (dr == DialogResult.Cancel)
				{
					return;
				}
			}
						
			setBusy(true);			

			// 先生成Source文件夹到工作目录，再把该文件夹压缩到导出文件夹中
			if (GenerateSourceProject())
			{
				DateTime beforeDT = System.DateTime.Now;

				SetNotice("正在压缩源文件,请稍候..." ,false);
				string dirPath = SavePath + @"\Source";
				ZipHelper.CompressAllToZip(dirPath, zipPath, 9, null, SavePath + @"\");
				SetNotice("已成功压缩源文件(Source.zip)。", false);

				DateTime afterDT = System.DateTime.Now;
				TimeSpan ts = afterDT.Subtract(beforeDT);
				dr = MessageBox.Show("成功导出工程(" + currentProjectName + ")的源文件,共耗时: " + ts.TotalSeconds.ToString("#0.00") + " s,是否打开导出文件夹?",
							"打开导出文件夹？",
							MessageBoxButtons.OKCancel,
							MessageBoxIcon.Question);
				if (dr == DialogResult.OK)
				{
					System.Diagnostics.Process.Start(exportPath);
				}
				setBusy(false);
				SetNotice("成功导出工程源文件", false);
			}
			else {
				setBusy(false);
				SetNotice("导出工程源文件失败。", false);				
			}			
		}

		/// <summary>
		/// 辅助方法：点击《导出工程》
		/// </summary>
		protected void exportProjectClick()
		{
			if (LightAstList == null || LightAstList.Count == 0)
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
			}
			string exportPath = exportFolderBrowserDialog.SelectedPath + @"\CSJ";
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
						
			SetNotice("正在导出工程，请稍候...", false);
			setBusy(true);

			DataConvertUtils.SaveProjectFile(GetDBWrapper(false), this, GlobalIniPath, new ExportProjectCallBack(this, exportPath));
		}

		/// <summary>
		/// MARK 导出单场景具体实现 0.exportFrameClick()方法体
		/// 辅助方法：右键点击《导出工程》->即导出当前场景
		/// </summary>
		protected void exportFrameClick()
		{
			if (LightAstList == null || LightAstList.Count == 0)
			{
				MessageBox.Show("当前工程没有灯具，无法导出工程。请添加灯具后再使用本功能。");
				return;
			}

			//MARK 导出单场景具体实现 1. 修改弹窗的提示
			DialogResult dr = MessageBox.Show(
					"请确保灯具列表未发生变化，并且与选择的已导出工程相比，只改动了当前场景的数据，否则可能产生错误的效果!\n" +
					"确定现在导出工程（只修改当前场景数据）吗？",
					"导出工程（只修改当前场景数据）？",
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Question);
			if (dr == DialogResult.Cancel)
			{
				return;
			}

			//MARK 导出单场景具体实现 2. 修改打开文件夹对话框的提示
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

			SetNotice("正在重新生成已导出工程的当前场景工程文件，请稍候...", false);
			setBusy(true);

			exportFrame(exportPath);
		}

		//MARK 导出单场景具体实现 4. 把选中文件夹内的所有数据拷到临时文件夹中（DataCache\Project\CSJ），拷贝前需要先清空目标文件夹；并逐一把所有CX.bin、MX.bin文件都拷贝过去		
		/// <summary>
		/// 辅助方法：当拷贝文件发生错误时，用递归的方法重新操作
		/// </summary>
		/// <param name="exportPath"></param>
		private void exportFrame(string exportPath)
		{
			try
			{
				FileUtils.ClearProjectData();
				string destPath = Application.StartupPath + @"\DataCache\Project\CSJ";
				for (int frame = 1; frame <= FrameCount; frame++)
				{
					FileHelper.CopyFile(exportPath + @"\C" + frame + ".bin", destPath, true);
					FileHelper.CopyFile(exportPath + @"\M" + frame + ".bin", destPath, true);
				}
			}
			catch (Exception ex) {
				DialogResult dialogResult = MessageBox.Show("拷贝工程文件到工作目录失败，原因为：\n" + ex.Message + "(可能是在短时间内进行了多次导出操作,系统尚未彻底释放资源)\n请稍等后点击《重试》或《取消》拷贝。",
						"是否重试？",
						MessageBoxButtons.RetryCancel,
						MessageBoxIcon.Error);
				if (dialogResult == DialogResult.Retry)
				{
					exportFrame(exportPath);
				}
				else
				{
					//若点击取消，则直接把忙时设为false，因为不会再往下走了，没有机会进行更改操作了。
					setBusy(false);
					SetNotice("因发生异常，已取消导出工程(只修改当前场景数据)的操作。", false);
				}
				return; //只要出现异常，就一定要退出本方法；
			}

			//MARK 导出单场景具体实现 5. 调用维佳的生成单场景方法，将只生成CFrame.bin、MFrame.bin、Config.bin和GradientData.bin；（其余文件都是拷贝两次：先拷到工作目录，调用完成后再拷回导出目录）
			DataConvertUtils.SaveSingleFrameFile(GetDBWrapper(false), this, GlobalIniPath, new ExportProjectCallBack(this, exportPath), CurrentFrame);
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
			SetNotice("成功关闭工程(" + tempProjectName + ")。", true);
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
					Console.WriteLine("导出结束时间" + DateTime.Now.ToLocalTime().ToString() );
				}
				catch (Exception ex) {
					DialogResult dialogResult = MessageBox.Show("拷贝工程文件失败，原因为：\n" + ex.Message + "\n请在处理完成后点击《重试》或《取消》拷贝。",
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
					SetNotice("正在压缩源文件,请稍候...",false);
					string dirPath = SavePath + @"\Source";
					string zipPath = exportPath + @"\Source.zip";
					ZipHelper.CompressAllToZip(dirPath, zipPath, 9, null, SavePath + @"\");
					SetNotice("已成功压缩源文件(Source.zip)。", false);
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
			SetNotice("导出工程" + (success ? "成功" : "出错"), false);
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
			foreach (LightWrapper lightTemp in LightWrapperList)
			{
				DB_Light light = dbLightList[LightWrapperList.IndexOf(lightTemp)];
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
						for(int stepIndex = 0; stepIndex<stepWrapperList.Count ; stepIndex++ )
						{
							StepWrapper step = stepWrapperList[stepIndex];
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
										Step = stepIndex + 1
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
			//MARK 只开单场景：08.0 保存所有场景数据（两张表），StepCount和Value；需通过frameSaveArray，判断是否要进行保存
			for (int frameIndex = 0; frameIndex < FrameCount; frameIndex++)
			{
				if (frameSaveArray[frameIndex])
				{
					saveFrameSCAndValue(frameIndex);
					//MARK 只开单场景：08.1 如遍历到的frameIndex非当前场景，则frameSaveArray[frameIndex]设为false;意味着之后不需要进行保存了；而当前场景的值仍为true；
					if (frameIndex != CurrentFrame)
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
			//MARK 只开单场景：09.2 调用场景时，若调用的是未打开的场景，则需先打开（载入到内存中）
			if (!frameLoadArray[selectedFrameIndex])
			{
				generateFrameData(selectedFrameIndex);
			}

			//MARK 只开单场景：09.3 调用场景时，把被调用场景的灯具数据，深复制到当前场景中来（只复制当前模式）
			if (LightWrapperList != null && LightWrapperList.Count != 0)
			{
				foreach (LightWrapper lightWrapper in LightWrapperList)
				{
					lightWrapper.LightStepWrapperList[CurrentFrame, CurrentMode]
						= LightStepWrapper.GenerateLightStepWrapper(lightWrapper.LightStepWrapperList[selectedFrameIndex, CurrentMode], lightWrapper.StepTemplate, CurrentMode);
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
			//若frameSaveArray为空，表示当前软件内存内没有工程，不需弹出保存工程。
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
			new LightsForm(this, LightAstList).ShowDialog();
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
			disConnect(); //projectUpdateClick()
			new ProjectUpdateForm(this,  GlobalIniPath, tempProjectPath).ShowDialog();
		}

		#endregion

		#region 菜单栏-非工程相关

		/// <summary>
		/// 辅助方法：点击《灯库软件》
		/// </summary>
		protected void openLightEditor()
		{
			new LightEditor.NewLightEditorForm(this).ShowDialog();			
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
			disConnect(); //newToolClick()
			new NewToolsForm(this).ShowDialog();
		}

		/// <summary>
		/// 辅助方法：点击《使用说明》
		/// </summary>
		protected void helpButtonClick()
		{
			try
			{
				//if (MessageBox.Show("确定打开《使用手册》吗？","" , MessageBoxButtons.OKCancel , MessageBoxIcon.Asterisk) == DialogResult.OK) {
					System.Diagnostics.Process.Start(Application.StartupPath + @"\使用手册.pdf");
				//}				
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 辅助方法：点击《更新日志》
		/// </summary>
		protected void updateLogButtonClick()
		{
			try
			{			
				System.Diagnostics.Process.Start(Application.StartupPath + @"\更新日志\更新日志.txt");								
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
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
			if (lsWrapper == null)
			{
				SetNotice("尚未选中灯具，无法插入步。", true);
				return;
			}

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
				newStep = StepWrapper.GenerateNewStep(stepIndex == 0 ? getCurrentStepTemplate() : getSelectedLightSelectedStepWrapper(selectedIndex, stepIndex - 1), CurrentMode);
			}
			else
			{
				newStep = StepWrapper.GenerateNewStep(currentStep == 0 ? getCurrentStepTemplate() : getCurrentStepWrapper(), CurrentMode);
			}
			lsWrapper.InsertStep(stepIndex, newStep, insertBefore);

			if (isSyncMode)
			{
				for (int lightIndex = 0; lightIndex < LightAstList.Count; lightIndex++)
				{
					if (lightIndex != selectedIndex)
					{
						if (insertBefore)
						{
							newStep = StepWrapper.GenerateNewStep(stepIndex == 0 ? getSelectedLightStepTemplate(lightIndex) : getSelectedLightSelectedStepWrapper(lightIndex, stepIndex - 1), CurrentMode);
						}
						else
						{
							newStep = StepWrapper.GenerateNewStep(currentStep == 0 ? getSelectedLightStepTemplate(lightIndex) : getSelectedLightCurrentStepWrapper(lightIndex), CurrentMode);
						}
						getSelectedLightStepWrapper(lightIndex).InsertStep(stepIndex, newStep, insertBefore);
					}
				}
			}
			else if (IsMultiMode)
			{
				foreach (int lightIndex in SelectedIndices)
				{
					if (lightIndex != selectedIndex)
					{
						if (insertBefore)
						{
							newStep = StepWrapper.GenerateNewStep(stepIndex == 0 ? getSelectedLightStepTemplate(lightIndex) : getSelectedLightSelectedStepWrapper(lightIndex, stepIndex - 1), CurrentMode);
						}
						else
						{
							newStep = StepWrapper.GenerateNewStep(currentStep == 0 ? getSelectedLightStepTemplate(lightIndex) : getSelectedLightCurrentStepWrapper(lightIndex), CurrentMode);
						}
						getSelectedLightStepWrapper(lightIndex).InsertStep(stepIndex, newStep, insertBefore);
					}
				}
			}
						
			RefreshStep();
		}

		/// <summary>
		/// 辅助方法：左键点击《追加步》
		/// </summary>
		protected void addStepClick()
		{
			if (getCurrentLightStepWrapper() == null)
			{
				SetNotice("尚未选中灯具，无法追加步。", true);
				return;
			}

			addStep();
			RefreshStep();
		}

		/// <summary>
		/// 辅助方法：右键点击《追加步》（追加指定数量的步）
		/// </summary>
		protected void addSomeStepClick()
		{
			if (getCurrentLightStepWrapper() == null)
			{
				SetNotice("尚未选中灯具，无法追加步。", true);
				return;
			}
			new AddStepsForm(this, MAX_STEP - getTotalStep() ).ShowDialog();
		}

		/// <summary>
		/// 辅助方法：追加指定数量的步
		/// </summary>
		/// <param name="v"></param>
		/// <returns>成功返回null，失败则返回失败的原因；</returns>
		public string AddSteps(int addStepCount)
		{
			try
			{
				for (int i = 0; i < addStepCount; i++)
				{
					addStep();
				}
				RefreshStep();
				return null;
			}
			catch (Exception ex) {
				return ex.Message;
			}
		}

		/// <summary>
		/// 辅助方法：追加步（供追加一步或追加多步使用）
		/// </summary>
		protected void addStep() {
			
			//1.若当前灯具在本F/M下总步数为0 ，则使用stepTemplate数据，
			//2.否则使用本灯当前最大步的数据			 
			bool addTemplate = getTotalStep() == 0;
			StepWrapper newStep = StepWrapper.GenerateNewStep(addTemplate ? getCurrentStepTemplate() : getCurrentLightLastStepWrapper(), CurrentMode);			
			getCurrentLightStepWrapper().AddStep(newStep);				

			if (isSyncMode)
			{
				for (int lightIndex = 0; lightIndex < LightAstList.Count; lightIndex++)
				{
					if (lightIndex != selectedIndex) //多一层保险...
					{
						newStep = StepWrapper.GenerateNewStep(addTemplate ? getSelectedLightStepTemplate(lightIndex) : getSelectedLightLastStepWrapper(lightIndex), CurrentMode);
						getSelectedLightStepWrapper(lightIndex).AddStep(newStep);
					}
				}
			}
			else if (IsMultiMode)
			{
				foreach (int lightIndex in SelectedIndices)
				{
					if (lightIndex != selectedIndex)
					{
						newStep = StepWrapper.GenerateNewStep(addTemplate ? getSelectedLightStepTemplate(lightIndex) : getSelectedLightLastStepWrapper(lightIndex), CurrentMode);
						getSelectedLightStepWrapper(lightIndex).AddStep(newStep);
					}
				}
			}
		}

		/// <summary>
		/// 辅助方法：左键点击《删除步》
		/// </summary>
		protected void deleteStepClick()
		{
			if (getCurrentLightStepWrapper() == null)
			{
				SetNotice("尚未选中灯具，无法删除步。", true);
				return;
			}

			// 调用包装类内部的方法:删除某一步
			try
			{
				int stepIndex = getCurrentStep() - 1;
				getCurrentLightStepWrapper().DeleteStep(stepIndex);
				if (isSyncMode)
				{
					for (int lightIndex = 0; lightIndex < LightAstList.Count; lightIndex++)
					{
						if (lightIndex != selectedIndex)
						{
							getSelectedLightStepWrapper(lightIndex).DeleteStep(stepIndex);
						}
					}
				}
				else if (IsMultiMode)
				{
					foreach (int lightIndex in SelectedIndices)
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
		/// 辅助方法：右键点击《删除步》（删除指定步）
		/// </summary>
		protected void deleteSomeStepClick()
		{
			if (getCurrentLightStepWrapper() == null)
			{
				SetNotice("尚未选中灯具，无法追加步。", true);
				return;
			}

			if (getTotalStep() == 0) {
				SetNotice("当前灯具没有步数，无法删除步。",true);
				return;
			}
			new DeleteStepsForm(this, getCurrentStep() , getTotalStep() ) .ShowDialog();
		}

		/// <summary>
		///  辅助方法：删除指定步（起始步和总步数）
		/// </summary>
		/// <param name="firstStep"></param>
		/// <param name="stepCount"></param>
		/// <returns></returns>
		public string DeleteSteps(int firstStep, int stepCount)
		{
			try
			{
				int stepIndex = firstStep - 1;
				// 调用包装类内部的方法:删除某一步
				for (int i = 0; i < stepCount; i++)
				{
					getCurrentLightStepWrapper().DeleteStep(stepIndex);
					if (isSyncMode)
					{
						for (int lightIndex = 0; lightIndex < LightAstList.Count; lightIndex++)
						{
							if (lightIndex != selectedIndex)
							{
								getSelectedLightStepWrapper(lightIndex).DeleteStep(stepIndex);
							}
						}
					}
					else if (IsMultiMode)
					{
						foreach (int lightIndex in SelectedIndices)
						{
							if (lightIndex != selectedIndex)
							{
								getSelectedLightStepWrapper(lightIndex).DeleteStep(stepIndex);
							}
						}
					}
				}
				RefreshStep();
				return null;
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}

		/// <summary>
		/// 辅助方法：点击《内置动作》
		/// </summary>
		protected void actionButtonClick()
		{
			//检查是否有X、Y轴；
			if (!checkXY())
			{
				SetNotice("检测到当前灯具无X/Y轴，无法使用内置动作功能。",true);
				return;
			}

			// 检查当前模式（音频模式很少用到动作，更多的是亮灭，故暂时不给音频模式做动作）
			if (CurrentMode != 0) {
				SetNotice("检测到当前模式不是常规模式，无法使用内置动作。",true);
				return;
			}
			
			// 若正在预览，则先停止预览
			if (IsPreviewing)
			{
				PreviewButtonClick(null);
			}

			if (actionForm == null) {
				actionForm = new ActionForm(this);
			}
			actionForm.ShowDialog();
		}

		/// <summary>
		/// 辅助方法：监测当前灯具是否存在X、Y轴
		/// </summary>
		/// <returns></returns>
		protected bool checkXY()
		{
			try
			{
				bool existX = false;
				bool existY = false;
				IList<TongdaoWrapper> tongdaoList = LightWrapperList[selectedIndex].StepTemplate.TongdaoList;
				foreach (TongdaoWrapper td in tongdaoList)
				{
					if (td.TongdaoName == "X轴")
					{
						existX = true;
					}
					else if (td.TongdaoName == "Y轴")
					{
						existY = true;
					}
				}
				return existX && existY;
			}
			catch (Exception ex)
			{
				MessageBox.Show("监测灯具是否有XY轴时出现异常：\n" + ex.Message);
				return false;
			}
		}

		/// <summary>
		/// 辅助方法：右键点击《内置动作》时，进入RGB调整界面
		/// </summary>
		protected void colorButtonClick()
		{
			if (colorForm == null)
			{
				colorForm = new ColorForm(this);
			}
			colorForm.ShowDialog();
		}

		/// <summary>
		/// 辅助方法：点击《复制步》
		/// </summary>
		protected void copyStepClick()
		{
			if (getCurrentStepWrapper() == null)
			{
				MessageBox.Show("当前步数据为空，无法复制");
				return;
			}
			tempStep = getCurrentStepWrapper();
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
			if (IsMultiMode)
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
			LightAst la = LightAstList[selectedIndex];
			if (la == null) {
				MessageBox.Show("未选中灯具，无法复制多步");
				return;
			}

			MultiStepCopyForm mscForm = new MultiStepCopyForm(this, getCurrentLightStepWrapper().StepWrapperList, CurrentMode, la, getCurrentStep());
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
			if (TempMaterialAst.Mode != CurrentMode)
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
			LightAst la = LightAstList[selectedIndex];
			new MaterialUseForm(this, CurrentMode, la.LightName, la.LightType).ShowDialog();
		}

		/// <summary>
		/// 辅助方法：点击《保存素材》
		/// </summary>
		protected void saveMaterial()
		{
			LightAst lightAst = LightAstList[selectedIndex];
			MaterialSaveForm materialForm = new MaterialSaveForm(this, getCurrentLightStepWrapper().StepWrapperList, CurrentMode, lightAst.LightName, lightAst.LightType);
			if (materialForm != null && !materialForm.IsDisposed)
			{
				materialForm.ShowDialog();
			}
		}

		/// <summary>
		///  辅助方法：点击《进入|退出同步》
		/// </summary>
		protected void syncButtonClick()
		{
			// 如果当前已经是同步模式，则退出同步模式，这比较简单，不需要进行任何比较，直接操作即可。
			if (isSyncMode)
			{
				EnterSyncMode(false);
				return;
			}
			else
			{
				// 异步时，要切换到同步模式，需要先进行检查。
				if (!CheckAllSameStepCounts())
				{
					MessageBox.Show("当前场景所有灯具步数不一致，无法进入同步模式。");
					return;
				}
				EnterSyncMode(true);
			}

		}

		/// <summary>
		///辅助方法：点击《多步复用》
		/// </summary>
		protected void multiplexButtonClick()
		{
			if (LightWrapperList == null || LightWrapperList.Count == 0) {
				MessageBox.Show("当前工程没有灯具，无法使用多步复用功能。");
				return;
			}
			int totalStep = getTotalStep();
			if ( totalStep == 0) {
				MessageBox.Show("灯具没有步数，无法使用多步复用功能。");
				return;
			}

			// selectedIndices2 只用在非同步状态时，故可以在同步状态下传入null
			IList<int> selectedIndices2 = null;
			if ( !isSyncMode) {
				if (! IsMultiMode)
				{
					selectedIndices2 = new List<int>() { selectedIndex };
				}
				else
				{
					selectedIndices2 = SelectedIndices;
				}
			}			

			new MultiplexForm(this, LightAstList, totalStep, isSyncMode, selectedIndices2 ).ShowDialog();
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

			MAX_STEP = IniFileHelper.GetSystemCount(Application.StartupPath,  CurrentMode==0?"maxStep":"maxStepSound" ,0);
			if (MAX_STEP == 0) {
				MAX_STEP = CurrentMode == 0 ? 100 : 300;			
			}
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
			if (selectedIndex < 0 || LightAstList == null || LightAstList.Count == 0)
			{
				SetNotice("尚未选择灯具。", false);
				editLightInfo(null);
				RefreshStep();
				return;
			}

			LightAst lightAst = LightAstList[selectedIndex];
			// 1.在右侧灯具信息内显示选中灯具相关信息，并生成子属性Panel
			editLightInfo(lightAst);
			generateSaPanels();
		
			//2.判断是不是已经有stepTemplate了
			// ①若无，则生成数据，并hideAllTongdao 并设stepLabel为“0/0” --> 因为刚创建，肯定没有步数	
			// ②若有，还需判断该LightData的LightStepWrapperList[frame,mode]是不是为null
			//			若是null，则说明该FM下，并未有步数，hideAllTongdao
			//			若不为null，则说明已有数据，

			LightWrapper lightWrapper = LightWrapperList[selectedIndex];
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
				from0on = true;
			}
			else
			{
				if (getCurrentLightStepWrapper() == null)
				{
					SetNotice("尚未选中灯具，无法选步。", true);
					return;
				}

				LightStepWrapper lightStepWrapper = getCurrentLightStepWrapper();
				StepWrapper stepWrapper = lightStepWrapper.StepWrapperList[stepNum - 1];
				lightStepWrapper.CurrentStep = stepNum;

				if (IsMultiMode)
				{
					foreach (int lightIndex in SelectedIndices)
					{
						getSelectedLightStepWrapper(lightIndex).CurrentStep = stepNum;
					}
				}
				//11.27 若是同步状态，则选择步时，将所有灯都设为一致的步数
				if (isSyncMode)
				{
					for (int lightIndex = 0; lightIndex < LightAstList.Count; lightIndex++)
					{
						getSelectedLightStepWrapper(lightIndex).CurrentStep = stepNum;
					}
				}

				showTDPanels(stepWrapper.TongdaoList, stepWrapper.StartNum);
				showStepLabel(lightStepWrapper.CurrentStep, lightStepWrapper.TotalStep);
				from0on = false;
			}
			
			if (IsConnected && !IsPreviewing )
			{
				OneStepPlay(null );
			}
		}

		/// <summary>
		/// 辅助方法：复用多灯多步
		/// </summary>
		/// <param name="selectedIndices"></param>
		/// <param name="startStep"></param>
		/// <param name="endStep"></param>
		/// <param name="times">复用次数</param>
		/// <returns>复用成功返回null，否则返回相应的错误信息</returns>
		public string MultiplexSteps(IList<int> selectedIndices, int startStep, int endStep, int times )
		{
			// 检查剩余步数 是否大于 复用会添加的的步数
			int addStepCount = (endStep - startStep + 1) * times;
			if ((MAX_STEP - getSelectedLightStepCounts(0)) < addStepCount) {
				return "剩余步数小于复用占用步数，无法复用。";
			}		
			
			// 解决方案（两种）：
			// (1)先把所有的步数用新步数填上，再通过SelectedIndices来更改相应的数据 X
			// (2)分开操作，不在列表内的直接加步，在表内的则复用 √
			//		①不在表内的都添加最后一步
			//		②在列表中的使用复制的方法(同步模式才这样选择)

			for (int lightIndex = 0; lightIndex < LightWrapperList.Count; lightIndex++)
			{
				if (selectedIndices.Contains(lightIndex))
				{
					for (int time = 1; time <= times; time++) //循环次数
					{
						for (int copyStepIndex = startStep - 1; copyStepIndex < endStep; copyStepIndex++)
						{
							StepWrapper copyStep = getSelectedLightSelectedStepWrapper(lightIndex, copyStepIndex);
							StepWrapper newStep = StepWrapper.GenerateNewStep(copyStep, CurrentMode);
							getSelectedLightStepWrapper(lightIndex).AddStep(newStep);
						}
					}
				}
				else
				{
					if (isSyncMode) {
						StepWrapper lastStep = getSelectedLightLastStepWrapper(lightIndex);
						StepWrapper newStep = StepWrapper.GenerateNewStep(lastStep, CurrentMode);
						for (int addStepIndex = 0; addStepIndex < addStepCount; addStepIndex++)
						{
							getSelectedLightStepWrapper(lightIndex).AddStep(newStep);
						}
					}
				}					
			}

			RefreshStep();
			return null;
		}

		/// <summary>
		/// 辅助方法：点击通道名，弹出相应的子属性窗口
		/// </summary>
		/// <param name="sender"></param>
		protected void tdNameNumLabelClick(object sender)
		{
			//Dickov:200812 之前的版本，点击通道名时，会弹出该通道的子属性辅助框；但现在的方式（直接全部显示出来）则没有必要了
			//disposeSauForm();
			//LightAst la = lightAstList[selectedIndex];
			//int tdIndex = MathHelper.GetIndexNum(((Label)sender).Name, -1);
			//string tdName = ((Label)sender).Text;
			//if (la.SawList[tdIndex] == null || la.SawList[tdIndex].SaList == null || la.SawList[tdIndex].SaList.Count == 0)
			//{
			//	SetNotice("通道【" + tdName + "】无可用子属性。");
			//	return;
			//}
			//sauForm = new SAUseForm(this, la, tdIndex, tdName);
			//sauForm.Show();
			//SetNotice("已打开通道【" + tdName + "】的子属性小窗口。");


			// Dickov:200812 新的功能：点击通道名，则弹出该通道的多步联调界面（作为所有灯+通道的试验版本）

			LightAst la = LightAstList[selectedIndex];
			LightWrapper lw = LightWrapperList[selectedIndex];

			int selectedTdIndex = MathHelper.GetIndexNum(((Label)sender).Name, -1);

			string lightName = la.LightName;
			string lightType = la.LightType;
			string lightAddr = la.LightAddr;
			string tdName = lw.StepTemplate.TongdaoList[selectedTdIndex].TongdaoName;

			SetNotice("打开【" + lightType+"("+selectedIndex+")" + "("+ selectedTdIndex + ":"+tdName+")】的单通道多步联调窗口。", false);

			new DetailSingleForm(
				this, 				
				selectedIndex,
				selectedTdIndex,
				lw.LightStepWrapperList[CurrentFrame,CurrentMode].StepWrapperList 
			) . ShowDialog();
		}

		#endregion

		#region astPanel相关
		
		/// <summary>
		/// 辅助方法：点击《多步调节》
		/// </summary>
		protected void multiButtonClick()
		{
			StepWrapper currentStep = getCurrentStepWrapper();
			if (currentStep == null || currentStep.TongdaoList == null || currentStep.TongdaoList.Count == 0)
			{
				SetNotice("请先选中任意步数，才能进行统一调整！", true);
				return;
			}
						
			LightAst la = LightAstList[selectedIndex];
			LightWrapper lw = LightWrapperList[selectedIndex];

			new MultiStepForm(this,
				getCurrentStep(),
				getTotalStep(),
				getCurrentStepWrapper(),
				CurrentMode,
				selectedIndex,
				lw.LightStepWrapperList[CurrentFrame, CurrentMode].StepWrapperList
			).ShowDialog();
		}
		
		/// <summary>
		/// 辅助方法：右键《多步调节》进入多步联调
		/// </summary>
		public void DetailMultiButtonClick(bool isOpenDMF)
		{
			if (getTotalStep() == 0)
			{
				SetNotice("当前灯具没有步数，无法使用多步联调。", true);
				return;
			}

			// 若tdDict不为空（意味着从DmaForm中被回传了），且是右键点击；则直接打开多步联调
			if (TdDict != null && isOpenDMF) {
				new DetailMultiPageForm(this, TdDict).ShowDialog();
				return;
			}


			if (DmaForm == null || DmaForm.IsDisposed)
			{
				DmaForm = new DetailMultiAstForm(this);
			}
			DmaForm.ShowDialog();
		}

		/// <summary>
		/// 辅助方法：灯具编组执行方法，成功则返回null；否则返回失败原因（string）
		/// </summary>
		/// <param name="groupName"></param>
		/// <param name="selectedIndices"></param>
		/// <returns></returns>
		public string CreateGroup(string groupName, int captainIndex)
		{
			if (GroupList == null)
			{
				return "尚未生成编组列表，请先创建列表后重试。";
			}
			if (!GroupAst.CheckGroupName(GroupList, groupName))
			{
				return "编组名称已被使用，请使用其他名称。";
			}

			GroupList.Add(new GroupAst()
			{
				GroupName = groupName,
				LightIndexList = SelectedIndices,
				CaptainIndex = captainIndex
			});

			refreshGroupPanels(); // CreateGroup()

			return null;
		}

		/// <summary>
		/// 辅助方法：点击《进入编组按键》
		/// </summary>
		/// <param name="sender"></param>
		protected void groupInButtonClick(object sender)
		{
			int groupIndex;
			try
			{
				groupIndex = int.Parse((sender as Button).Tag.ToString());
			}
			catch (Exception ex)
			{
				MessageBox.Show("按钮的Tag无法转化为groupIndex:\n" + ex.Message);
				return;
			}

			if (GroupList == null || GroupList.Count == 0)
			{
				MessageBox.Show("当前工程groupList为空，无法使用编组。");
				return;
			}
			if (groupIndex >= GroupList.Count)
			{
				MessageBox.Show("groupIndex大于groupList的大小，无法使用编组。");
				return;
			}
			GroupAst group = GroupList[groupIndex];
			if (group.LightIndexList == null || group.LightIndexList.Count < 1)
			{
				MessageBox.Show("选中编组的组员数量小于1，无法使用编组。");
				return;
			}

			if (!checkIndexAllInLightList(group.LightIndexList))
			{
				MessageBox.Show("编组内的部分灯具索引超过了当前工程的灯具数量，无法使用编组。");
				return;
			}
			
			if (!checkSameLightsAndSteps(group.LightIndexList))
			{
				MessageBox.Show("编组内的灯具并非同一类型或步数不一致，无法使用编组。");
				return;
			}	

			SelectedIndices = group.LightIndexList;
			selectLights();

			// 如果灯数超过1，则进入多灯模式
			if (group.LightIndexList.Count > 1)
			{
				EnterMultiMode(group.CaptainIndex, false);
			}
			// 否则退出多灯模式
			else {
				exitMultiMode();
			}			
		}

		/// <summary>
		/// 辅助方法：校验所有列表内索引，是否都在当前工程的灯具列表中
		/// </summary>
		/// <param name="lightIndexList"></param>
		/// <returns>都在则返回true</returns>
		protected bool checkIndexAllInLightList(IList<int> lightIndexList)
		{
			for (int i = 0; i < lightIndexList.Count; i++)
			{
				int lightIndex = lightIndexList[i];
				if (lightIndex >= LightAstList.Count)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// 辅助方法: 确认选中灯具 ①是否同一种灯具 ②所有的选中灯具步数是否一致：都符合则返回true
		/// </summary>
		/// <returns>有个不相同的的项（名或步数），则返回false</returns>
		protected bool checkSameLightsAndSteps(IList<int> lightIndexList)
		{
			bool result = true;
			int firstIndex = lightIndexList[0];
			string firstName = LightAstList[firstIndex].LightName + "：" + LightAstList[firstIndex].LightType;
			int firstStepCount = getSelectedLightStepCounts(firstIndex);

			for (int i = 1; i < lightIndexList.Count; i++) // 从第二个选中灯具开始比对
			{
				int tempIndex = lightIndexList[i];
				string tempName = LightAstList[tempIndex].LightName + "：" + LightAstList[tempIndex].LightType;
				int tempStepCount = getSelectedLightStepCounts(tempIndex);

				if (!firstName.Equals(tempName) || firstStepCount != tempStepCount)
				{
					result = false;
					break;
				}
			}
			return result;
		}

		/// <summary>
		/// 辅助方法：点击《删除编组》
		/// </summary>
		/// <param name="sender"></param>
		protected void groupDelButtonClick(object sender)
		{
			int groupIndex;
			try
			{
				groupIndex = int.Parse((sender as Button).Tag.ToString());
			}
			catch (Exception ex)
			{
				MessageBox.Show("按钮的Tag无法转化为groupIndex:\n" + ex.Message);
				return;
			}

			if (DialogResult.Cancel == MessageBox.Show(
				"确定要删除编组【" + GroupList[groupIndex].GroupName + "】吗？",
				"删除编组?",
				MessageBoxButtons.OKCancel,
				MessageBoxIcon.Warning))
			{
				return;
			}

			GroupList.RemoveAt(groupIndex);
			refreshGroupPanels(); //groupDelButtonClick()
		}
		
		/// <summary>
		/// 辅助方法：点击《子属性》
		/// </summary>
		/// <param name="sender"></param>
		protected void SaButtonClick(object sender)
		{
			if (getCurrentStepWrapper() == null)
			{
				SetNotice("当前无选中步，不可点击子属性按钮", false);
				return;
			}

			Button btn = (Button)sender;
			string[] btnTagArr = btn.Tag.ToString().Split('*');
			int tdIndex = int.Parse(btnTagArr[0]);
			int tdValue = int.Parse(btnTagArr[1]);

			getCurrentStepWrapper().TongdaoList[tdIndex].ScrollValue = tdValue;
			if (IsMultiMode)
			{
				copyValueToAll(tdIndex, WHERE.SCROLL_VALUE, tdValue);
			}
			RefreshStep();
		}
			
		#endregion

		#region playPanel相关

		/// <summary>
		/// 辅助方法：点击《连接设备 | 断开连接》
		/// </summary>
		protected void connectButtonClick(string deviceName , int deviceSelectedIndex)
		{		
			// 如果已连接（按钮显示为“连接设备”)，则关闭连接
			if ( IsConnected)
			{
				disConnect(); //connectButtonClick
			}
			else {
				playTools = PlayTools.GetInstance();
				if (isConnectCom)
				{
					if (String.IsNullOrEmpty(deviceName))
					{
						SetNotice("未选中可用串口，请选中后再点击连接。",true);
						return;
					}
					if (playTools.ConnectDevice(deviceName))
					{
						SetNotice("设备(以串口方式)连接成功,并进入调试模式。",false);
						EnableConnectedButtons(true, false);
					}
					else {
						SetNotice("设备连接失败，请刷新串口列表后重试。",true);
					}
				}
				else
				{
					if ( deviceSelectedIndex < 0)
					{
						SetNotice("未选中可用网络连接，请选中后再点击连接。",true);
						return;
					}
					
					myConnect = new NetworkConnect();					
					if (myConnect.Connect(networkDeviceList[deviceSelectedIndex]))
					{
						playTools.StartInternetPreview( myConnect, ConnectCompleted, ConnectAndDisconnectError, eachStepTime);						
						SetNotice("设备(以网络方式)连接成功,并进入调试模式。",false);						
					}
					else
					{
						SetNotice("设备连接失败，请刷新网络设备列表后重试。",true);
					}
				}
			}			
		}

		/// <summary>
		/// 辅助方法：断开连接
		/// </summary>
		protected void disConnect() {
			if (IsConnected) {
				playTools = PlayTools.GetInstance();
				playTools.ResetDebugDataToEmpty();				
				playTools.StopSend();
				if (isConnectCom)
				{
					playTools.CloseDevice();					
					EnableConnectedButtons(false,false);
				}
				else
				{
					playTools.StopInternetPreview(DisconnectCompleted, ConnectAndDisconnectError);
				}
				SetNotice("已断开连接。",false);
			}
		}

		/// <summary>
		/// 辅助方法：单(多)灯单步发送DMX512帧数据
		/// </summary>
		public void OneStepPlay(MaterialAst material)
		{
			// 未连接的情况下，无法发送数据。 
			if (!IsConnected)
			{
				SetNotice("请先连接设备。", false);
				return;
			}

			if (IsPreviewing)
			{
				SetNotice("正在预览中，无法实时调试。", false);
				return;
			}

			byte[] stepBytes = new byte[512];

			// 若选择了《保持其他灯》状态，只需使用此通用代码即可(遍历所有灯具的当前步，取出其数据，放到数组中）；
			if (isKeepOtherLights || isSyncMode)
			{
				if (LightWrapperList != null)
				{
					for (int lightIndex = 0; lightIndex < LightWrapperList.Count; lightIndex++)
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
			else
			{
				// 多灯单步				
				if (IsMultiMode)
				{
					int currentStep = getCurrentStep();
					if (currentStep == 0)
					{
						SetNotice("当前多灯编组未选中可用步，无法播放。", false);
						return;
					}
					foreach (int lightIndex in SelectedIndices)
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
						SetNotice("当前灯具未选中可用步，无法播放。", false);
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

			//DOTO : 1217 OneStepPlay添加material后，实时生成(基于现有步数据，处理)。
			if (material != null)
			{
				// 多灯单步				
				if (IsMultiMode)
				{
					

				}
				// 单灯单步
				else
				{

				}
			}

			string tdValueStr = "";
			if (tdValues != null && tdValues.Count > 0)
			{
				tdValueStr += "【";
				foreach (int tdIndex in tdValues)
				{
					tdValueStr += stepBytes[tdIndex] + " ";
				}
				tdValueStr += "】";
			}

			playTools.OLOSView(stepBytes);
			SetNotice("正在实时调试(" + (selectedIndex + 1) + ")当前步..." + tdValueStr, false);
		}
		
		/// <summary>
		/// 辅助方法：点击《预览效果》
		/// </summary>
		internal void Preview()
		{			
			SetNotice("预览数据生成成功,即将开始预览。",false);
			EnableConnectedButtons(true,true);

			playTools.PreView(dbWrapperTemp, GlobalIniPath, CurrentFrame);			
		}
		
		/// <summary>
		/// 辅助方法：结束预览
		/// </summary>
		protected void endview()
		{
			playTools.EndView();	
		}

		/// <summary>
		/// 辅助方法：预览效果|停止预览
		/// </summary>
		public void PreviewButtonClick(MaterialAst material)
		{
			if (!IsConnected)
			{
				MessageBox.Show("尚未连接设备，无法预览效果（或停止预览）。");
				EnableConnectedButtons(false, false);
				return;
			}

			// 停止预览
			if (IsPreviewing)
			{
				endview();
				EnableConnectedButtons(true, false);	
				SetNotice("已结束预览,并恢复到实时调试模式。",false);
			}
			// 开始预览
			else
			{
				if (LightAstList == null || LightAstList.Count == 0)
				{
					SetNotice("当前工程还未添加灯具，无法预览。",true);
					SetPreview(false);
					return;
				}

				setBusy(true);
				SetPreview(true);
				SetNotice("正在生成预览数据，请稍候...",false);
				try
				{
					// 给使用动作预览的方法
					if ( material != null) 
					{
						generateDBLightList();
						generateDBFineTuneList();

						IList<DB_Value> valueList = new List<DB_Value>();

						int lightId = dbLightList[selectedIndex].StartID;			
						StepWrapper stepTemplate = getCurrentStepTemplate();
						IList<MaterialIndexAst> sameTDIndexList = getSameTDIndexList( material.TdNameList ,  stepTemplate.TongdaoList);						

						for (int stepIndex = 0; stepIndex < material.StepCount ; stepIndex++)
						{
							for (int tdIndex = 0; tdIndex < sameTDIndexList.Count; tdIndex++)
							{
								int materialTdIndex = sameTDIndexList[tdIndex].MaterialTDIndex;
								int trueTdIndex = sameTDIndexList[tdIndex].CurrentTDIndex + lightId;

								TongdaoWrapper td = material.TongdaoList[stepIndex, materialTdIndex];

								DB_Value value = new DB_Value()
								{
									PK = new DB_ValuePK(){
										 Frame = CurrentFrame,
										 LightIndex = lightId,
										 LightID = trueTdIndex ,
										 Mode = 0,
										 Step = stepIndex+1,
									},
									ScrollValue = td.ScrollValue,
									StepTime = td.StepTime,
									ChangeMode = td.ChangeMode,
								};

								valueList.Add(value);
							}
						}						
						dbWrapperTemp = new DBWrapper(dbLightList,  null, valueList , dbFineTuneList );
					}
					else {
						dbWrapperTemp = GetDBWrapper(false);
					}
					DataConvertUtils.SaveProjectFileByPreviewData( dbWrapperTemp , GlobalIniPath, CurrentFrame, new PreviewCallBack(this));
				}
				catch (Exception ex)
				{
					SetPreview(false);
					MessageBox.Show("生成预览数据时异常：" + ex.Message);
				}
				finally
				{
					setBusy(false);
				}
			}
		}

		/// <summary>
		/// 辅助方法：点击触发音频
		/// </summary>
		protected void makeSoundButtonClick() {
			// 若还在触发状态中，则不再触发
			if (!playTools.GetMusicStatus())
			{
				Console.WriteLine("Dickov:GetMusicStatus==true（触发中，或无音频）,故此操作不生效...");
				return;
			}

			setMakeSound(true);
			playTools.MusicControl();
			setMakeSound(false);
		}

		#endregion

		#region lightListView相关

		/// <summary>
		/// 辅助方法：双击列表中灯具，更改备注
		/// </summary>
		protected void lightsListViewDoubleClick(int lightIndex)
		{
			LightAst la = LightAstList[lightIndex];
			new LightRemarkForm(this, la, lightIndex).ShowDialog();
		}

		/// <summary>
		/// 辅助方法：添加或修改备注
		/// </summary>
		/// <param name="lightIndex"></param>
		/// <param name="remark"></param>
		public virtual void EditLightRemark(int lightIndex, string remark)
		{
			// 内存的lightAstList[lightIndex]要改动相应的值；
			LightAstList[lightIndex].Remark = remark;
			editLightInfo(LightAstList[lightIndex]);
		}

		#endregion

		#region 初始化及窗口相关

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.lightImageList = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// lightImageList
			// 
			this.lightImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.lightImageList.ImageSize = new System.Drawing.Size(60, 65);
			this.lightImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// MainFormBase
			// 
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Name = "MainFormBase";
			this.Load += new System.EventHandler(this.MainFormBase_Load);
			this.ResumeLayout(false);

		}

		/// <summary>
		/// 空Load方法：方便查找InitGeneralControl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainFormBase_Load(object sender, EventArgs e){	}

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
			SoftwareName = IniFileHelper_UTF8.ReadString(Application.StartupPath + @"/GlobalSet.ini", "Show", "softwareName", "");
			SoftwareName += " Dimmer System ";

			string loadexeName = Application.ExecutablePath;
			FileVersionInfo fileVersionInfo =FileVersionInfo.GetVersionInfo(loadexeName);
			string appFileVersion = string.Format("{0}.{1}.{2}.{3}", fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart, fileVersionInfo.FileBuildPart, fileVersionInfo.FilePrivatePart);
			SoftwareName += "v" + appFileVersion + " ";
			
			IniFileHelper iniHelper = new IniFileHelper(Application.StartupPath + @"\GlobalSet.ini");		
			SavePath = iniHelper.GetSavePath();
			IsShowTestButton = iniHelper.GetControlShow( "testButton");
			IsShowHardwareUpdate = iniHelper.GetControlShow( "hardwareUpdateButton");
			IsShowSaPanels = iniHelper.GetControlShow("saPanels");
			IsNoticeUnifyTd = iniHelper.GetIsNotice("unifyTd");

			MAX_StTimes = iniHelper.GetSystemCount( "maxStTimes",250);
			MAX_STEP = iniHelper.GetSystemCount( "maxStep",100);	
			NETWORK_WAITTIME = iniHelper.GetSystemCount( "waitTime",1000);
			DefaultSoundCM = iniHelper.GetSystemCount("soundChangeMode", 0);

			//DOTO : 1218 添加对单步运行时某些步数据是否显示的处理
			try
			{
				string tdStr = iniHelper.ReadString("Show", "tdValues", "");
				if (tdStr.Trim() != "")
				{
					string[] tdValuesStr = tdStr.Split(',');
					tdValues = new List<int>();
					foreach (string valueStr in tdValuesStr)
					{
						tdValues.Add(int.Parse(valueStr) - 1);
					}
				}
			}
			catch (Exception) {
				tdValues = null;
			}			

			// lightImageList的初始化
			this.lightImageList = new ImageList();
			this.lightImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.lightImageList.ImageSize = new System.Drawing.Size(60, 65);
			this.lightImageList.TransparentColor = System.Drawing.Color.Transparent;
		}   
	
		/// <summary>
		/// 辅助方法：点击退出时FormClosing事件；
		/// </summary>
		/// <param name="e"></param>
		protected void formClosing(FormClosingEventArgs e)
		{
			//MARK 只开单场景：17.4 FormClosing时提示保存工程
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

		#region 通用方法

		/// <summary>
		/// 辅助方法：刷新当前步;
		/// </summary>
		public void RefreshStep()
		{
			chooseStep(getCurrentStep());
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

			if (IsMultiMode)
			{
				copyValueToAll(tdIndex, WHERE.SCROLL_VALUE, tdValue);
			}

			// 是否实时单灯单步
			if (IsConnected && !IsPreviewing)
			{
				OneStepPlay(null);
			}
		}

		/// <summary>
		/// 辅助方法：当通道的三个相关输入框，键盘输入某些按键时，可以对通道或步统一调值；
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void unifyTdKeyPress(object sender, KeyPressEventArgs e)
		{
			// 先验证按下的键是否在这些数据中
			if (!"aAsS".ToCharArray().Contains(e.KeyChar))
			{
				return;
			}

			Control control = sender as Control;
			List<int> tdIndexList = new List<int>();
			int startStep, endStep, unifyValue;
			string msg;

			// 按a|A时，作用于当前通道所有步
			if (e.KeyChar == 'a' || e.KeyChar == 'A')
			{
				startStep = 1;
				endStep = getTotalStep();    // 因存在输入框，表示步数一定存在，无需验证空指针
											 // 若始末步相同，意味着此操作无意义，直接return
				if (startStep == endStep)
				{
					return;
				}

				tdIndexList.Add(MathHelper.GetIndexNum(control.Name, -1));
				msg = "要将【当前通道所有步】";
			}
			//按s|S时，把变化作用于当前步的所有通道
			else
			{
				// 因为存在输入框，表示一定存在StepWrapper及TongdaoList，不需要验证空指针
				// 若此灯具小于等于(一般不会)一个通道，则此操作无意义，直接return;
				if (getCurrentStepWrapper().TongdaoList.Count <= 1)
				{
					return;
				}

				for (int tdIndex = 0; tdIndex < getCurrentStepWrapper().TongdaoList.Count; tdIndex++)
				{
					tdIndexList.Add(tdIndex);
				}
				startStep = getCurrentStep();
				endStep = startStep;
				msg = "要将【当前步所有通道】";
			}

			// 定义where，因为下面会多次使用；
			WHERE where = (WHERE)int.Parse(control.Tag.ToString());
			if (where == WHERE.SCROLL_VALUE)
			{
				NumericUpDown valueNUD = control as NumericUpDown;
				unifyValue = decimal.ToInt32(valueNUD.Value);
				msg += "的通道值都设为【" + unifyValue + "】吗？";
			}
			else if (where == WHERE.CHANGE_MODE)
			{
				ComboBox cb = control as ComboBox;
				unifyValue = cb.SelectedIndex;
				msg += "的跳渐变都设为【" + cb.Text + "】吗？";
			}
			else if (where == WHERE.STEP_TIME)
			{
				NumericUpDown stNUD = control as NumericUpDown;
				decimal stepTime = stNUD.Value;
				unifyValue = decimal.ToInt32(stepTime / EachStepTime2);
				msg += "的步时间都设为【" + stepTime + "】吗？";
			}
			else
			{
				SetNotice("错误的Tag。", true);
				return;
			}

			// 设置了提示，且用户点击了取消，则return。否则继续往下走
			if (IsNoticeUnifyTd)
			{
				if (DialogResult.Cancel == MessageBox.Show(
						msg,
						"通道统一设值",
						MessageBoxButtons.OKCancel,
						MessageBoxIcon.Question))
				{
					return;
				}
			}

			SetMultiStepValues(where, tdIndexList, startStep, endStep, 0, unifyValue);

		}

		#endregion

		#region 委托的成功或方法块 

		/// <summary>
		/// 辅助回调方法：设备连接成功
		/// </summary>
		/// <param name="obj"></param>
		public void ConnectCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate {
				EnableConnectedButtons(true,false);	
			});
		}	

		/// <summary>
		/// 辅助回调方法：设备断开成功
		/// </summary>
		/// <param name="obj"></param>
		public void DisconnectCompleted(Object obj, string msg)
		{
			Invoke((EventHandler)delegate {
				EnableConnectedButtons(false,false);				
			});
		}
		
		/// <summary>
		/// 辅助回调方法：设备连接或断开连接出错
		/// </summary>
		/// <param name="obj"></param>
		public void ConnectAndDisconnectError(string msg)
		{
			Invoke((EventHandler)delegate	{
				SetNotice(msg,true);
			});
		}
		
		#endregion	
		
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
			mainForm.EnableConnectedButtons(true,false);
		}

		public void Error(string deviceTag, string errorMessage)
		{
			mainForm.SetNotice("设备(" + deviceTag + ")启动网络调试模式失败。",false);
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
		public void Error(string msg)
		{
            mainForm.SetPreview(false);
            mainForm.SetNotice("预览数据生成出错,无法预览,。错误原因为：" + msg , true);            
		}
		public void UpdateProgress(string name)
		{
			mainForm.SetNotice("预览数据生成中(" + name + ")" , false);
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
		public void Error(string msg)
		{
			mainForm.CopyProject(exportFolder, false);
		}
		public void UpdateProgress(string name)
		{
			mainForm.SetNotice("正在生成工程文件("+name+")" ,false);
		}
	}	

}