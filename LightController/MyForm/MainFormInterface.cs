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

namespace LightController.MyForm
{
	public class MainFormInterface : System.Windows.Forms.Form
	{
		// 辅助的bool变量：	
		protected bool isNew = true;    // 点击新建后 到 点击保存前，这个属性是true；如果是使用打开文件或已经点击了保存按钮，则设为false										
		protected bool isInit = false;  // form都初始化后，才将此变量设为true;为防止某些监听器提前进行监听
		public bool IsCreateSuccess = false; //点击新建后，用这个变量决定是否打开灯具编辑列表

		// 全局配置及数据库连接		
		protected string currentProjectName;  //存放当前工程名，主要作用是防止当前工程被删除（openForm中）
		protected string globalIniPath;  // 存放当前工程《全局配置》、《摇麦设置》的配置文件的路径
		protected string dbFilePath; // 数据库地址：每个工程都有自己的db，所以需要一个可以改变的dbFile字符串，存放数据库连接相关信息
		protected bool isEncrypt = false; //是否加密
		protected int eachStepTime = 30;

		// 数据库DAO
		protected LightDAO lightDAO;
		protected StepCountDAO stepCountDAO;
		protected ValueDAO valueDAO;
		protected FineTuneDAO fineTuneDAO;

		

		// 这几个IList ，存放着所有数据库数据		
		protected IList<DB_Light> dbLightList = new List<DB_Light>();
		protected IList<DB_StepCount> dbStepCountList = new List<DB_StepCount>();

		

		protected IList<DB_Value> dbValueList = new List<DB_Value>();
		protected IList<DB_FineTune> dbFineTuneList = new List<DB_FineTune>();
		
		protected IList<LightAst> lightAstList;	 //与《灯具编辑》通信用的变量 
		protected IList<LightWrapper> lightWrapperList = new List<LightWrapper>(); // 辅助的灯具变量：记录所有（灯具）的（所有场景和模式）的 每一步（通道列表）

		// 通道数据操作时的变量
		protected bool isMultiMode = false;
		protected int selectedIndex = -1; //选择的灯具的index
		protected IList<int> selectedIndices = new List<int>() ; //选择的灯具的index列表（多选情况下）
		protected string selectedLightName = "";
		protected int frame = 0; // 表示场景编号
		protected int mode = 0;  // 0.常规模式； 1.音频模式
		
		protected bool isUseStepTemplate = false ; // 是否勾选了《使用模板生成步》
		protected LightWrapper tempLight = null; // 辅助灯变量，用以复制及粘贴灯 
		protected StepWrapper tempStep = null; //// 辅助步变量：复制及粘贴步时用到
		public MaterialAst TempMaterialAst = null;  // 辅助（复制多步、素材）变量 ， 《复制、粘贴多步》时使用

			   
		// 调试变量
		protected PlayTools playTools; //DMX512灯具操控对象的实例
		protected bool isConnected = false; // 辅助bool值，当选择《连接设备》后，设为true；反之为false
		protected bool isRealtime = false; // 辅助bool值，当选择《实时调试》后，设为true；反之为false			
		protected bool isKeepOtherLights = false ;  // 辅助bool值，当选择《（非调灯具)保持状态》时，设为true；反之为false
		protected string[] comList;  //存储DMX512串口的名称列表，用于comSkinComboBox中
		protected string comName; // 存储打开的DMX512串口名称

		// 将所有场景名称写在此处,并供所有类使用
		public static IList<string> AllFrameList ;
		public static int FrameCount = 0;  //场景数量
		protected string savePath;
		protected bool isShowHardwareUpdateButton = false;

		/// <summary>
		/// 基类辅助方法：①清空所有List；②设置内部的一些工程路径及变量；③初始化数据库
		/// </summary>
		/// <param name="projectName"></param>
		/// <param name="isNew"></param>
		public void InitProject(string projectName, bool isNew)		{

			//0.清空所有list
			clearAllData();

			// 1.全局设置
			currentProjectName = projectName;
			string directoryPath =savePath + @"\LightProject\" + projectName;			
			globalIniPath = directoryPath + "\\global.ini";
			dbFilePath = directoryPath + "\\data.db3";
			this.Text = "智控配置(当前工程:" + projectName + ")";
			this.isNew = isNew;
			// 9.5 读取时间因子
			IniFileAst iniAst = new IniFileAst(globalIniPath);
			eachStepTime = iniAst.ReadInt("Set", "EachStepTime", 30);

			// 2.创建数据库:
			// 因为是初始化，所以先让所有的DAO指向null，避免连接到错误的数据库(已打开过旧的工程的情况下)；
			// --若isNew为true时，为初始化数据库，将lightDAO指向新的对象，然后运行CreateSchema方法
			lightDAO = null;
			stepCountDAO = null;
			valueDAO = null;
			fineTuneDAO = null;

			// 若为新建，则初始化db的table（随机使用一个DAO即可初始化）
			if (isNew)
			{
				lightDAO = new LightDAO(dbFilePath, false);
				lightDAO.CreateSchema(true, true);
			}

			// 设置各按键是否可用
			enableGlobalSet(true);
			enableSave(true);

		}

		/// <summary>
		///  辅助方法，通过打开已有的工程，来加载各种数据到mainForm中
		/// data.db3的载入：把相关内容，放到数据列表中
		///    ①lightList 、stepCountList、valueList
		///    ②lightAstList（由lightList生成）
		///    ③lightWrapperList(由lightAstList生成)
		/// </summary>
		/// <param name="directoryPath"></param>
		public void OpenProject(string projectName)
		{
			// 0.初始化
			InitProject(projectName, false);

			// 把数据库的内容填充进来，并设置好对应的DAO
			dbLightList = getLightList();
			dbStepCountList = getStepCountList();
			dbValueList = getValueList();
			dbFineTuneList = getFineTuneList();

			// 通过lightList填充lightAstList
			lightAstList = reCreateLightAstList(dbLightList);
			AddLightAstList(lightAstList);

			// 针对每个lightWrapper，获取其已有步数的场景和模式
			for (int lightListIndex = 0; lightListIndex < dbLightList.Count; lightListIndex++)
			{
				IList<DB_StepCount> scList = stepCountDAO.getStepCountList(dbLightList[lightListIndex].LightNo);

				if (scList != null && scList.Count > 0)
				{
					// 只要有步数的，优先生成StepMode
					StepWrapper stepTemplate = generateStepTemplate(lightAstList[lightListIndex]);
					lightWrapperList[lightListIndex].StepTemplate = stepTemplate;
					foreach (DB_StepCount sc in scList)
					{
						int frame = sc.PK.Frame;
						int mode = sc.PK.Mode;
						int lightIndex = sc.PK.LightIndex;
						int stepCount = sc.StepCount;

						for (int step = 1; step <= stepCount; step++)
						{
							IList<DB_Value> stepValueList = valueDAO.getStepValueList(lightIndex, frame, mode, step);
							StepWrapper stepWrapper = StepWrapper.GenerateStepWrapper(stepTemplate, stepValueList, mode);
							if (lightWrapperList[lightListIndex].LightStepWrapperList[frame, mode] == null)
							{
								lightWrapperList[lightListIndex].LightStepWrapperList[frame, mode] = new LightStepWrapper();
							}
							lightWrapperList[lightListIndex].LightStepWrapperList[frame, mode].AddStep(stepWrapper);
						}
					}
				}
			}
			// 8.29 统一生成步数模板
			GenerateAllStepTemplates();

			isInit = true;
			MessageBox.Show("成功打开工程：" + projectName);
		}

		#region 几个纯虚（virtual修饰）方法：主要供各种基类方法向子类回调使用		

		protected virtual void enableGlobalSet(bool enable) { }
		protected virtual void enableSave(bool enable) { }

		/// <summary>
		/// 辅助方法（纯虚方法）：选择不同步数时统一使用这个方法（上一步下一步新建步等情况下用）
		/// </summary>
		/// <param name="stepNum"></param>
		protected virtual void chooseStep(int stepNum) { }

		#endregion


		


		/// <summary>
		/// 辅助方法： 清空相关的所有数据
		/// -- 子类中需有针对该子类内部自己的部分代码（如重置listView或禁用stepPanel等）
		/// </summary>
		protected virtual void clearAllData()
		{			
			dbLightList = null;
			dbStepCountList = null;
			dbValueList = null;
			dbFineTuneList = null;

			lightAstList = null;
			lightWrapperList = null;

			selectedIndex = -1;		
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
		/// 添加lightAst列表到主界面内存中,主要供 LightsForm调用（以及OpenProject调用）
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
						if ((j < lightWrapperList.Count)
							&& lightAstList2[i].Equals(lightAstList[j])
							&& lightWrapperList[j] != null
						)
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
		}

		/// <summary>
		/// 辅助方法：在《打开工程》《添加灯具》时统一生成所有的LightWrapper的StepTemplate（步数模板）
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
		/// 生成模板Step --》 之后每新建一步，都复制模板step的数据。
		/// </summary>
		/// <param name="lightAst"></param>
		/// <param name="lightIndex"></param>
		/// <returns></returns>
		protected StepWrapper generateStepTemplate(LightAst lightAst)
		{
			Console.WriteLine("Dickov : 开始生成模板文件(StepTemplate)：" + lightAst.LightName + lightAst.LightType + "(" + lightAst.LightAddr + ")");
			int startNum = lightAst.StartNum;
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
					MessageBox.Show("Dickov：打开的ini文件格式有误，无法生成StepTemplate！");
					return null;
				}
				else
				{
					int tongdaoCount2 = (lineCount - 6) / 3;
					int tongdaoCount = int.Parse(lineList[3].ToString().Substring(6));
					if (tongdaoCount2 < tongdaoCount)
					{
						MessageBox.Show("Dickov：打开的ini文件的count值与实际值不符合，无法生成StepTemplate！");
						return null;
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
							Address = startNum + (address - 1)
						});
					}


					return new StepWrapper()
					{
						TongdaoList = tongdaoList,
						LightFullName = lightAst.LightName + "*" + lightAst.LightType,
						StartNum = startNum
						// 这里使用“*”作为分隔符，这样的字符无法在系统生成文件夹;
						// 这样就能有效防止有些灯刚好Name+Type的组合相同
					};
				}
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
		///  辅助方法：通过fromDB属性，来获取内存或数据库中的DBWrapper(三个列表的集合)
		/// </summary>
		/// <returns></returns>
		protected DBWrapper GetDBWrapper(bool isFromDB)
		{
			// 从数据库直接读取的情况
			if (isFromDB)
			{
				DBGetter dbGetter = new DBGetter(dbFilePath, false);
				DBWrapper allData = dbGetter.getAll();
				return allData;
			}
			// 由内存几个实时的List实时生成
			else
			{
				// BUG:此处的实际上是上次保存后的数据，这种情况下和 dbGetter.getAll() 没任何区别！
				// -->修改方法：先生成最新的 dbLightList,dbStepCountList, dbValueList 数据

				generateDBLightList();
				generateDBStepCountList();
				generateDBValueList();
				generateDBFineTuneList();

				DBWrapper allData = new DBWrapper(dbLightList, dbStepCountList, dbValueList, dbFineTuneList);
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
				if ( stepTemplate != null  &&  stepTemplate.TongdaoList != null) {
					int xz = 0, xzwt = 0, xzValue=0 , yz = 0, yzwt = 0 ,yzValue=0;
					foreach (TongdaoWrapper td in stepTemplate.TongdaoList)
					{
						switch (td.TongdaoName.Trim()) {
							case "X轴": xz = td.Address;break;
							case "X轴微调": xzwt = td.Address; xzValue = td.ScrollValue; break;
							case "Y轴": yz = td.Address; break;
							case "Y轴微调": yzwt = td.Address; yzValue = td.ScrollValue; break;
						}						
					}
					if (xz != 0 && xzwt != 0) {
						dbFineTuneList.Add(new DB_FineTune() {	MainIndex = xz , FineTuneIndex = xzwt , MaxValue = xzValue  } );
					}
					if (yz != 0 && yzwt != 0) {
						dbFineTuneList.Add(new DB_FineTune(){	MainIndex = yz,	FineTuneIndex = yzwt, MaxValue = yzValue });
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
		/// 存放所有灯具所有场景的每一步每一通道的值，记录数据到db.Value表中
		/// </summary>
		protected void saveAllValues()
		{
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
		/// 点击《保存工程》按钮
		/// 保存需要进行的操作：
		/// 1.将lightAstList添加到light表中 --> 分新建或打开文件两种情况
		/// 2.将步数、素材、value表的数据都填进各自的表中
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void saveAll()
		{
			// 1.先判断是否有灯具数据；若无，则直接停止
			if (lightAstList == null || lightAstList.Count == 0)
			{
				MessageBox.Show("当前并没有灯具数据，无法保存！");
				return;
			}

			// 2.保存各项数据
			saveAllLights();
			saveAllStepCounts();
			saveAllValues();
			saveAllFineTunes();

			MessageBox.Show("成功保存");
		}

		#region 素材相关

		/// <summary>
		/// 辅助方法:调用素材
		/// </summary>
		/// <param name="materialAst"></param>
		/// <param name="method"></param>
		public virtual void InsertOrCoverMaterial(MaterialAst materialAst, InsertMethod method)
		{		
			LightStepWrapper lsWrapper = getCurrentLightStepWrapper();
			int totalStep = lsWrapper.TotalStep;
			int currentStep = lsWrapper.CurrentStep;
			int addStepCount = materialAst.StepCount;

			// 选择《插入》时的操作：后插法（往当前步后加数据）
			// 8.28 当选择《覆盖》但总步数为0时（currentStep也是0），也用插入的方法
			if (method == InsertMethod.INSERT || totalStep == 0)
			{
				int finalStep = totalStep + addStepCount;
				if ((mode == 0 && finalStep > 32) || (mode == 1 && finalStep > 48))
				{
					MessageBox.Show("素材步数超过当前模式剩余步数，无法调用");
					return;
				}

				StepWrapper stepTemplate = getCurrentStepTemplate();
				IList<MaterialIndexAst> sameTDIndexList = getSameTDIndexList(materialAst.TdNameList, stepTemplate.TongdaoList);
				if (sameTDIndexList.Count == 0)
				{
					MessageBox.Show("该素材与当前灯具不匹配，无法调用");
					return;
				}
				else
				{
					StepWrapper newStep = null;
					for (int stepIndex = 0; stepIndex < addStepCount; stepIndex++)
					{
						newStep = StepWrapper.GenerateNewStep(stepTemplate, mode);
						// 改造下newStep,将素材值赋给newStep 
						changeStepFromMaterial(materialAst.TongdaoList, stepIndex, sameTDIndexList, newStep);
						// 使用后插法：避免当前无数据的情况下调用素材失败
						lsWrapper.InsertStep(lsWrapper.CurrentStep - 1, newStep, false);
					}
					if(isMultiMode) {
						copyToAll(0);
					}

					chooseStep(finalStep);
				}
			}
			// 选择覆盖时的操作：后插法
			//（当前步也要被覆盖，除非没有当前步-》totalStep == currentStep == 0）
			else
			{
				int finalStep = (currentStep - 1) + addStepCount;// finalStep为覆盖后最后一步的序列，而非所有步的数量

				if ((mode == 0 && finalStep > 32) || (mode == 1 && finalStep > 48))
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
				else
				{
					StepWrapper newStep = null;
					if (finalStep > totalStep)
					{
						for (int i = 0; i < finalStep - totalStep; i++)
						{
							newStep = StepWrapper.GenerateNewStep(stepTemplate, mode);
							lsWrapper.AddStep(newStep);
						}
					}

					for (int stepIndex = currentStep - 1, materialStepIndex = 0; stepIndex < finalStep; stepIndex++, materialStepIndex++)
					{
						changeStepFromMaterial(materialAst.TongdaoList, materialStepIndex, sameTDIndexList, lsWrapper.StepWrapperList[stepIndex]);
						newStep = lsWrapper.StepWrapperList[stepIndex];
					}
					if (isMultiMode)
					{
						copyToAll(0);
					}

					if (newStep != null)
					{						
						chooseStep(finalStep);
					}
				}
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
		/// <param name="newStep"></param>
		protected void changeStepFromMaterial(TongdaoWrapper[,] materialTongdaoList, int stepIndex,
				IList<MaterialIndexAst> sameTDIndexList, StepWrapper newStep)
		{
			foreach (MaterialIndexAst mia in sameTDIndexList)
			{
				int currentTDIndex = mia.CurrentTDIndex;
				int materialTDIndex = mia.MaterialTDIndex;
				newStep.TongdaoList[currentTDIndex].ScrollValue = materialTongdaoList[stepIndex, materialTDIndex].ScrollValue;
				newStep.TongdaoList[currentTDIndex].ChangeMode = materialTongdaoList[stepIndex, materialTDIndex].ChangeMode;
				newStep.TongdaoList[currentTDIndex].StepTime = materialTongdaoList[stepIndex, materialTDIndex].StepTime;
			}
		}

		#endregion

		/// <summary>
		/// 辅助方法：单灯单步发送DMX512帧数据
		/// </summary>
		protected virtual void oneLightStepWork()
		{
			// 未连接的情况下，无法发送数据。
			if (!isConnected)
			{
				MessageBox.Show("请先连接设备");
				return;
			}

			byte[] stepBytes = new byte[512];
			// 若选择了《保持其他灯》状态，只需使用此通用代码即可(遍历所有灯具的当前步，取出其数据，扔到数组中）；
			if (isKeepOtherLights)
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
						StepWrapper stepWrapper = getSelectedLightStepWrapper(lightIndex).StepWrapperList[currentStep - 1];
						if (stepWrapper != null)
						{
							IList<TongdaoWrapper> tongdaoList = stepWrapper.TongdaoList;
							foreach (TongdaoWrapper td in tongdaoList)
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
						IList<TongdaoWrapper> tongdaoList = stepWrapper.TongdaoList;
						foreach (TongdaoWrapper td in tongdaoList)
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
				copyToAll2(0,tdIndex,WHERE.SCROLL_VALUE,tdValue);
			}

				// 是否实时单灯单步
			if (isConnected && isRealtime)
			{
				oneLightStepWork();
			}
		}


		#region 获取各种当前（步数、灯具）等的辅助方法

		/// <summary>
		///  获取当前选中的LightWrapper（此灯具全部数据）
		/// </summary>
		/// <returns></returns>
		protected LightWrapper getCurrentLightWrapper()
		{
			//Console.WriteLine("currentLight - " + selectedLightIndex);
			// 说明尚未点击任何灯具 或 内存内还没有任何灯具
			if (selectedIndex == -1  || lightWrapperList == null || lightWrapperList.Count == 0)
			{
				return null;
			}			
			return lightWrapperList[selectedIndex];
		}

		/// <summary>
		///  辅助方法：取出选定(灯具、frame、mode))的所有步数集合
		/// </summary>
		/// <returns></returns>
		protected LightStepWrapper getCurrentLightStepWrapper()
		{
			LightWrapper light = getCurrentLightWrapper();
			if (light == null)
			{
				return null;
			}
			else
			{			
				//若为空，则立刻创建一个
				if (light.LightStepWrapperList[frame, mode] == null)
				{
					light.LightStepWrapperList[frame, mode] = new LightStepWrapper()
					{
						StepWrapperList = new List<StepWrapper>()
					};
				};
				return light.LightStepWrapperList[frame, mode];
			}
		}
		
		/// <summary>
		/// 辅助方法：取出选中灯具的当前F/M的所有步数据
		/// </summary>
		/// <param name="selectedIndex"></param>
		/// <returns></returns>
		protected LightStepWrapper getSelectedLightStepWrapper(int selectedIndex)
		{
			LightWrapper light = lightWrapperList[selectedIndex];
			return light.LightStepWrapperList[frame, mode];
		}

		/// <summary>
		/// 辅助方法：直接取出当前（灯、frame、mode 、currentStepValue)步：筛选条件比较苛刻
		/// </summary>
		/// <returns></returns>
		protected StepWrapper getCurrentStepWrapper()
		{
			LightStepWrapper light = getCurrentLightStepWrapper();
			if (light != null
				&& light.TotalStep != 0
				&& light.CurrentStep != 0
				&& light.StepWrapperList != null
				&& light.StepWrapperList.Count != 0)
			{
				return light.StepWrapperList[light.CurrentStep - 1];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		///  获取当前灯具的StepMode，用于还未生成步数时调用
		/// </summary>
		/// <returns></returns>
		protected StepWrapper getCurrentStepTemplate()
		{
			LightStepWrapper light = getCurrentLightStepWrapper();
			if (light != null)
			{
				return getCurrentLightWrapper().StepTemplate;
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
			LightStepWrapper light = getCurrentLightStepWrapper();
			if (light != null)
			{
				return light.CurrentStep;
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
			LightStepWrapper light = getCurrentLightStepWrapper();
			if (light != null)
			{
				return light.TotalStep;
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		///  8.15新增的
		///  辅助方法：取出当前灯在该场景模式下的最大步数据，（用于追加步）
		/// </summary>
		/// <returns></returns>
		protected StepWrapper getCurrentLightMaxStepWrapper()
		{
			LightStepWrapper light = getCurrentLightStepWrapper();
			int totalStep = getTotalStep();
			if (light == null || totalStep == 0)
			{
				return null;
			}
			else
			{
				return light.StepWrapperList[getTotalStep() - 1];
			}
		}

		/// <summary>
		/// 辅助方法：获取指定灯具当前F/M 的当前步数据（需要与totalStep对比进行判断）
		/// </summary>
		/// <param name="light"></param>
		/// <returns></returns>
		protected StepWrapper getSelectedStepWrapper(int lightIndex)
		{
			LightWrapper light = lightWrapperList[lightIndex];
			int currentStep = light.LightStepWrapperList[frame, mode].CurrentStep;
			int totalStep = light.LightStepWrapperList[frame, mode].TotalStep;

			// 当前步或最大步某一种为0的情况下，返回null
			if (currentStep == 0 || totalStep == 0)
			{
				return null;
			}
			else
			{
				return light.LightStepWrapperList[frame, mode].StepWrapperList[currentStep - 1];
			}
		}

		#endregion


		#region 窗体相关方法：退出、
		/// <summary>
		///  辅助方法：彻底退出程序
		/// </summary>
		protected void Exit()
		{
			System.Environment.Exit(0);
		}

		#endregion


		/// <summary>
		/// 枚举类型：《多步(多通道)调节》参数的一种
		/// </summary>
		public enum WHERE
		{
			SCROLL_VALUE , CHANGE_MODE , STEP_TIME , ALL
		}

		/// <summary>
		///  辅助方法：供《多步(多通道)调节》使用
		/// </summary>
		/// <param name="indexList"></param>
		/// <param name="startStep"></param>
		/// <param name="endStep"></param>
		/// <param name="where"></param>
		/// <param name="commonValue"></param>
		public void setMultiStepValues(WHERE where, IList<int> tdIndexList, int startStep, int endStep, int commonValue) {
			
			LightStepWrapper lightStepWrapper = getCurrentLightStepWrapper();
			for (int stepIndex = startStep - 1; stepIndex < endStep; stepIndex++)		{
				StepWrapper stepWrapper = lightStepWrapper.StepWrapperList[stepIndex];
				stepWrapper.MultiChangeValue(where, tdIndexList, commonValue);
			}

			if (isMultiMode) {
				copyToAll(0);
			}

			// 刷新当前tdPanels数据。
			refreshStep();
		}

		/// <summary>
		/// 辅助方法：刷新当前步;
		/// TODO：不一定使用chooseStep方法 
		/// </summary>
		protected void refreshStep()
		{
			chooseStep(getCurrentStep());
		}

		/// <summary>
		/// 辅助方法：在《在全局配置》中改变了时间因子并保存后，mainForm的时间因子变量也跟着改变，同时刷新当前步
		/// </summary>
		public void ChangeEachStepTime(int eachStepTime) {
			this.eachStepTime = eachStepTime;
			refreshStep();
		}

		/// <summary>
		/// 9.16 辅助方法：进入多灯模式
		///		1.取出选中的组长，
		///		2.使用组长数据，替代其他灯具（在该F/M）的所有步数集合。
		/// </summary>
		/// <param name="selectedIndex"></param>
		public virtual void EnterMultiMode(int selectedIndex)
		{
			copyToAll(selectedIndex);
			refreshStep();
		}

		/// <summary>
		/// 辅助方法：多灯模式中，利用此方法将组长所有（当前F/M）数据，赋给所有的组员。
		/// </summary>
		/// <param name="selectedIndex"></param>
		protected void copyToAll(int selectedIndex) {
			
			// selectedIndex是几个选中的索引中的顺序，用chooseIndex才能选到当前ListView中指定的灯具
			int chooseIndex = selectedIndices[selectedIndex];
			LightStepWrapper mainLSWrapper = getSelectedLightStepWrapper(chooseIndex); //取出组长

			foreach (int index in selectedIndices)
			{
				//通过组长生成相关的数据
				StepWrapper currentStepTemplate = lightWrapperList[index].StepTemplate;
				lightWrapperList[index].LightStepWrapperList[frame, mode] = LightStepWrapper.GenerateLightStepWrapper(mainLSWrapper, currentStepTemplate, mode);
			}
		}


		/// <summary>
		/// 辅助方法：多灯模式中，利用此方法，将修改不多的组长数据（如部分通道值、渐变方式、步时间等），用此改动较少的方法，赋给所有的组员
		/// </summary>
		/// <param name="selectedIndex"></param>
		protected void copyToAll2(int selectedIndex , int tdIndex , WHERE where, int value)
		{
			// selectedIndex是几个选中的索引中的顺序，用chooseIndex才能选到当前ListView中指定的灯具
			int chooseIndex = selectedIndices[selectedIndex];
			LightStepWrapper mainLSWrapper = getSelectedLightStepWrapper(chooseIndex); //取出组长
			int currentStep = getCurrentStep();
			foreach (int index in selectedIndices)
			{
				switch (where) {
					case WHERE.SCROLL_VALUE:
						getSelectedLightStepWrapper(index).StepWrapperList[currentStep - 1].TongdaoList[tdIndex].ScrollValue = value; break;
					case WHERE.CHANGE_MODE:
						getSelectedLightStepWrapper(index).StepWrapperList[currentStep - 1].TongdaoList[tdIndex].ChangeMode = value;break;
					case WHERE.STEP_TIME:
						getSelectedLightStepWrapper(index).StepWrapperList[currentStep - 1].TongdaoList[tdIndex].StepTime = value; break;
				}				
			}
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
		/// 辅助方法：获取当前工程未选中灯具（多灯模式下使用）
		/// </summary>
		/// <returns></returns>
		protected IList<int> getNotSelectedIndices()
		{
			IList<int> allIndices = new List<int>();
			for (int i = 0; i < lightWrapperList.Count; i++)
			{
				allIndices.Add(i);
			}
			if (selectedIndices == null || selectedIndices.Count == 0)
			{
				return allIndices;
			}
			else
			{
				return allIndices.Except(selectedIndices).ToList();
			}
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
				lightWrapper.LightStepWrapperList[frame, mode] = LightStepWrapper.GenerateLightStepWrapper(lightWrapper.LightStepWrapperList[selectedFrameIndex, mode], lightWrapper.StepTemplate,   mode) ;				
			}
			refreshStep();
			MessageBox.Show("成功调用场景:"+ AllFrameList[selectedFrameIndex]); 

		}

		
	}
}