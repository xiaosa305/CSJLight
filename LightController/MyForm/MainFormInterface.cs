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
		protected int selectedLightIndex = -1; //选择的灯具的index
		protected int frame = 0; // 0-23 表示24种场景
		protected int mode = 0;  // 0.常规模式； 1.音频模式
		protected bool isUseStepTemplate = false ; // 是否勾选了《使用模板生成步》
		protected LightWrapper tempLight = null; // 辅助灯变量，用以复制及粘贴灯
		protected StepWrapper tempStep; //// 辅助步变量：复制及粘贴步时用到

			   
		// 调试变量
		protected PlayTools playTools; //DMX512灯具操控对象的实例
		protected bool isConnect = false; // 辅助bool值，当选择《连接设备》后，设为true；反之为false；
		protected bool isRealtime = false; // 辅助bool值，当选择《实时调试》后，设为true；反之为false			
		protected string[] comList;  //存储DMX512串口的名称列表，用于comSkinComboBox中
		protected string comName; // 存储打开的DMX512串口名称

		// 将所有场景名称写在此处,并供所有类使用
		public static IList<string> allFrameList = new List<string>() {
					"标准",
					"动感",
					"商务",
					"抒情",
					"清洁",
					"柔和",
					"激情",
					"明亮",
					"浪漫",
					"演出",
					"电影",
					"备用1",
					"备用2",
					"备用3",
					"备用4",
					"备用5",
					"备用6",
					"暂停",
					"全关",
					"全开",
					"全开关",
					"摇麦",
					"喝彩",
					"倒彩"
		};

		

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
			string directoryPath = "C:\\Temp\\LightProject\\" + projectName;			
			globalIniPath = directoryPath + "\\global.ini";
			dbFilePath = directoryPath + "\\data.db3";
			this.Text = "智控配置(当前工程:" + projectName + ")";
			this.isNew = isNew;

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

		#region 几个纯虚（virtual修饰）方法：主要供各种基类方法向子类回调使用		

		public virtual void OpenProject(string projectName) {}
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
		/// </summary>
		protected virtual void clearAllData()
		{			
			dbLightList = null;
			dbStepCountList = null;
			dbValueList = null;

			lightAstList = null;
			lightWrapperList = null;

			selectedLightIndex = -1;
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
							StepTime = 10,
							ChangeMode = 0,
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
			IList<DB_FineTune> ftList = fineTuneDAO.GetAll();
			return ftList;
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

				DBWrapper allData = new DBWrapper(dbLightList, dbStepCountList, dbValueList);
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
					int xz = 0, xzwt = 0, yz = 0, yzwt = 0;
					foreach (TongdaoWrapper td in stepTemplate.TongdaoList)
					{
						switch (td.TongdaoName.Trim()) {
							case "X轴": xz = td.Address;break;
							case "X轴微调": xzwt = td.Address; break;
							case "Y轴": yz = td.Address; break;
							case "Y轴微调": yzwt = td.Address; break;
						}						
					}
					if (xz != 0 && xzwt != 0) {
						dbFineTuneList.Add(new DB_FineTune() {	MainIndex = xz , FineTuneIndex = xzwt } );
					}
					if (yz != 0 && yzwt != 0) {
						dbFineTuneList.Add(new DB_FineTune(){	MainIndex = yz,	FineTuneIndex = yzwt	});
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

				// 取出灯具的每个常规场景(24种），并将它们保存起来（但若为空，则不保存）
				for (int frame = 0; frame < 24; frame++)
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
				for (int frame = 0; frame < 24; frame++)
				{
					for (int mode = 0; mode < 2; mode++)
					{
						LightStepWrapper lightStep = lswl[frame, mode];
						if (lightStep != null && lightStep.TotalStep > 0)
						{  //只有不为null，才可能有需要保存的数据
							List<StepWrapper> stepWrapperList = lightStep.StepWrapperList;
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
		public virtual void InsertOrCoverMaterial(MaterialAst materialAst, MaterialUseForm.InsertMethod method)
		{
			LightStepWrapper lsWrapper = getCurrentLightStepWrapper();
			int totalStep = lsWrapper.TotalStep;
			int currentStep = lsWrapper.CurrentStep;
			int addStepCount = materialAst.Step;

			// 选择《插入》时的操作：后插法（往当前步后加数据）
			// 8.28 当选择《覆盖》但总步数为0时（currentStep也是0），也用插入的方法
			if (method == MaterialUseForm.InsertMethod.INSERT || totalStep == 0)
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
			if (!isConnect)
			{
				MessageBox.Show("请先连接设备");
				return;
			}

			StepWrapper step = getCurrentStepWrapper();
			if (step != null)
			{
				List<TongdaoWrapper> tongdaoList = step.TongdaoList;
				byte[] stepBytes = new byte[512];
				foreach (TongdaoWrapper td in tongdaoList)
				{
					int tongdaoIndex = td.Address - 1;
					stepBytes[tongdaoIndex] = (byte)(td.ScrollValue);
				}
				playTools.OLOSView(stepBytes);
			}
			else
			{
				MessageBox.Show("当前未选中可用步，无法播放！");
			}
		}

		/// <summary>
		/// 辅助方法：改变tdPanel中的通道值之后（改trackBar或者numericUpDown），更改对应的tongdaoList的值；并根据ifRealTime，决定是否实时调试灯具。	
		/// </summary>
		/// <param name="tongdaoIndex"></param>
		protected void changeScrollValue(int tongdaoIndex, int tdValue)
		{
			// 1.设tongdaoWrapper的值
			StepWrapper step = getCurrentStepWrapper();
			step.TongdaoList[tongdaoIndex].ScrollValue = tdValue;
			//2.是否实时单灯单步
			if (isConnect && isRealtime)
			{
				oneLightStepWork();
			}
		}

		#region 获取各种当前（步数、灯具）等的辅助方法

		/// <summary>
		///  获取当前选中的LightWrapper
		/// </summary>
		/// <returns></returns>
		protected LightWrapper getCurrentLightWrapper()
		{
			return lightWrapperList[selectedLightIndex];
		}

		/// <summary>
		///  辅助方法：取出选定灯具、Frame、Mode 的 所有步数集合
		/// </summary>
		/// <returns></returns>
		protected LightStepWrapper getCurrentLightStepWrapper()
		{
			// 说明尚未点击任何灯具
			if (selectedLightIndex == -1)
			{
				return null;
			}
			// 说明内存内还没有任何灯具
			if (lightWrapperList == null || lightWrapperList.Count == 0)
			{
				return null;
			}

			LightWrapper light = lightWrapperList[selectedLightIndex];
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
		/// 辅助方法：这个方法直接取出当前步：筛选条件比较苛刻
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
		protected int getCurrentStepValue()
		{
			LightStepWrapper light = getCurrentLightStepWrapper();
			if (light != null)
			{
				return getCurrentLightStepWrapper().CurrentStep;
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
		protected int getTotalStepValue()
		{
			LightStepWrapper light = getCurrentLightStepWrapper();
			if (light != null)
			{
				return getCurrentLightStepWrapper().TotalStep;
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		///  8.15新增的
		///  辅助方法：取出当前灯在该场景模式下的最大步数据，用于追加步
		/// </summary>
		/// <returns></returns>
		protected StepWrapper getCurrentLightMaxStepWrapper()
		{
			LightStepWrapper light = getCurrentLightStepWrapper();
			int totalStep = getTotalStepValue();
			if (light == null || totalStep == 0)
			{
				return null;
			}
			else
			{
				return light.StepWrapperList[getTotalStepValue() - 1];
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
	}
}