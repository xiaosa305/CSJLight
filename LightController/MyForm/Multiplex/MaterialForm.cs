using LightController.Ast;
using LightController.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm.Multiplex
{
	public partial class MaterialForm : Form
	{
		private MainFormBase mainForm ;

		private MaterialAst complexMaterial;  // 复合的素材，预览及调用时使用（结合了四个小项素材后的情况）
		//private int stepCount = 0;
		//private int tongdaoCount = 0;

		// 为一些数据设置一些默认值，使得初次进来时，可以渲染各个Tab页 及 其它的输入项（步时间等）
		private int oldMode  = -1; 
		private string oldLightType = "";
		private decimal oldEachStepTime = .0m;

		private StepWrapper stepTemplate;
		
		// 调用素材相关
		private string generalStr = @"\通用\";
		private string specialStr;
		private string materialPath;		

		// 调色素材相关
		private MaterialAst colorMat;           
		private IList<string> colorTdNameList;     
		private string dimmerStr = LanguageHelper.TranslateWord("总调光");
		private string rStr = LanguageHelper.TranslateWord("红");
		private string gStr = LanguageHelper.TranslateWord("绿");
		private string bStr = LanguageHelper.TranslateWord("蓝");
		private int soundStepTime = 10;
		private int colorStepCount = 0;
		private int colorSelectedIndex = -1;				
		private TongdaoWrapper[,] colorTongdaoArray;

		// 动作素材相关
		private MaterialAst actionMaterial ;
		IList<string> actionTdNameList;
		private string xStr = LanguageHelper.TranslateWord("X轴");
		private string yStr = LanguageHelper.TranslateWord("Y轴");		
		private int commonStepTime = 50;		

		public MaterialForm(MainFormBase mainForm)
		{			
			InitializeComponent();
			this.mainForm = mainForm;

			actionTdNameList = new List<string> { xStr, yStr };
			colorTdNameList = new List<string> { dimmerStr, rStr, gStr, bStr }; // 为tdNameList赋值；此列表是固定的
			tgNUD.MouseWheel += someNUD_MouseWheel ;
			tgTrackBar.MouseWheel += someTrackBar_MouseWheel ;
			
		}

		private void MaterialForm_Load(object sender, EventArgs e)
		{
			Location = MousePosition;
			stepTemplate = mainForm.GetCurrentStepTemplate(); 
			string newLightType = mainForm.GetCurrentLightType();		

			// 灯具发生变化时，刷新tdTab(tdCB)
			if (oldLightType != newLightType)
			{
				refreshTdTab();
			}
			// 灯具 或 模式发生变化时，刷新其余的Tab
			if (oldLightType != newLightType || oldMode != mainForm.CurrentMode)
			{
				refreshMaterialTab(newLightType);
				refreshActionTab();
				refreshColorTab();
			}

			// 若mainForm的时间因子发生变化，则相应的步时间也发生变化
			if (mainForm.EachStepTime2 != oldEachStepTime)
			{
				// 动作步时间
				StNumericUpDown.Maximum = MainFormBase.MAX_StTimes * mainForm.EachStepTime2;
				StNumericUpDown.Minimum = mainForm.EachStepTime2; // 步时间如果为0，则毫无意义
				StNumericUpDown.Increment = mainForm.EachStepTime2;
				StNumericUpDown.Value = mainForm.EachStepTime2 * commonStepTime;

				// 颜色步时间	
				for (int panelIndex = 1; panelIndex < colorFLP.Controls.Count; panelIndex++)
				{
					NumericUpDown stNUD = (colorFLP.Controls[panelIndex] as Panel).Controls[0] as NumericUpDown;
					decimal oldValue = stNUD.Value / oldEachStepTime ;

					stNUD.Maximum = MainFormBase.MAX_StTimes * mainForm.EachStepTime2;
					stNUD.Minimum = mainForm.EachStepTime2; // 步时间如果为0，则毫无意义
					stNUD.Increment = mainForm.EachStepTime2;
					stNUD.Value = mainForm.EachStepTime2 * oldValue;
				}				
			}

			oldMode = mainForm.CurrentMode ;
			oldLightType = newLightType ;
			oldEachStepTime = mainForm.EachStepTime2;

			// 显示预览
			materialTreeView.ExpandAll(); // 不论渲染成什么样，都要主动展开树，否则winForm会自己收纳起来。
			previewButton.Visible = mainForm.IsConnected & mainForm.CurrentMode == 0;			

			oneStepPlay(false);  //MaterialForm_Load
		}
			   
		/// <summary>
		///  事件：关闭窗口
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MaterialForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (mainForm.IsConnected)
			{
				if (mainForm.IsPreviewing)
				{
					endView();
				}
				else
				{
					mainForm.OneStepPlay(null);
				}
			}
		}

		/// <summary>
		/// 事件：关闭界面：停止预览
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cancelButton_Click(object sender, EventArgs e)
		{
			Hide();
			mainForm.Activate();
		}


		/// <summary>
		/// 辅助方法：刷新tdTab
		/// </summary>
		private void refreshTdTab() {

			tdComboBox.Items.Clear();
			foreach (TongdaoWrapper td in stepTemplate.TongdaoList)
			{
				tdComboBox.Items.Add(td.TongdaoName);
			}
			tdComboBox.SelectedIndex = 0;
		}

		/// <summary>
		/// 辅助方法：刷新素材列表
		/// </summary>
		private void refreshMaterialTab(string newLightType) {

			materialTreeView.Nodes.Clear();

			materialPath = IniFileHelper.GetSavePath(Application.StartupPath) + @"\LightMaterial\" + (mainForm.CurrentMode == 0 ? "Normal" : "Sound");

			// 添加通用的素材
			string generalPath = materialPath + generalStr;
			if (Directory.Exists(generalPath))
			{
				TreeNode generalTreeNode = new TreeNode(LanguageHelper.TranslateSentence("通用素材"));

				string[] filePaths = Directory.GetFiles(generalPath);
				foreach (string filePath in filePaths)
				{
					FileInfo file = new FileInfo(filePath);
					string fileName = file.Name;
					if (fileName.EndsWith(".ini"))
					{
						TreeNode node = new TreeNode(
							fileName.Substring(0, fileName.IndexOf("."))
						);
						generalTreeNode.Nodes.Add(node);
					}
				}
				materialTreeView.Nodes.Add(generalTreeNode);
			}

			// 添加该灯的素材
			specialStr = @"\"  + newLightType +  @"\";
			string specialPath = materialPath + specialStr;
			if (Directory.Exists(specialPath))
			{
				TreeNode specialTreeNode = new TreeNode(newLightType );
				string[] filePaths = Directory.GetFiles(specialPath);
				foreach (string filePath in filePaths)
				{
					FileInfo file = new FileInfo(filePath);
					string fileName = file.Name;
					if (fileName.EndsWith(".ini"))
					{
						TreeNode node = new TreeNode(
							fileName.Substring(0, fileName.IndexOf("."))
						);
						specialTreeNode.Nodes.Add(node);
					}
				}
				materialTreeView.Nodes.Add(specialTreeNode);
			}			

		}

		/// <summary>
		///  辅助方法：刷新动作Tab:根据mode及XY是否存在隐藏或显示；(步时间的更改不用放在这里)
		/// </summary>
		private void refreshActionTab() {
			Console.WriteLine("refreshActionTab");

			bool actionShow = mainForm.CurrentMode == 0 && StepWrapper.CheckXY(stepTemplate) ; 
			actionTab.Parent = actionShow ? tabControl1 : null ;
			actionCB.Visible = actionShow;
		}

		/// <summary>
		/// 辅助方法：刷新颜色Tab
		/// </summary>
		private void refreshColorTab() {
			
			if (StepWrapper.CheckRGB( stepTemplate) )
			{




				colorTab.Parent = tabControl1;
				colorCB.Visible = true;

				// 当模式发生变化时，需要隐藏|显示 相关控件
				for (int panelIndex = 1; panelIndex < colorFLP.Controls.Count; panelIndex++)
				{
					(colorFLP.Controls[panelIndex] as Panel).Controls[0].Visible = mainForm.CurrentMode == 0;
					(colorFLP.Controls[panelIndex] as Panel).Controls[1].Visible = mainForm.CurrentMode == 0;
				}
				stLabel.Visible = mainForm.CurrentMode == 0;
				modeLabel.Visible = mainForm.CurrentMode == 0;

				// DOTO 考虑是否有更合适的地方可以放（目前是当灯具或当场景发生变化时，才进行处理：实际应在应用颜色时读取，其它地方用不上）
				// 虽然不显示，但应用颜色时，仍需用到这些数据
				if (mainForm.CurrentMode == 1)
				{
					IniFileHelper iniHelper = new IniFileHelper(mainForm.GlobalIniPath);
					soundStepTime = iniHelper.ReadInt("SK", mainForm.CurrentScene + "ST", 10);
				}
			}
			// 当并非RGB灯具时，直接隐藏快速调色功能
			else {
				colorTab.Parent = null;
				colorCB.Visible = false;
			}
		}

		/// <summary>
		/// 辅助方法：已连接且非预览中时,根据当前色块及固定通道，直接在灯具上显示颜色		
		/// </summary>
		private void oneStepPlay(bool colorChanged)
		{
			if (mainForm.IsConnected && !mainForm.IsPreviewing)
			{
				MaterialAst singleMat = null ;
				Dictionary<string, int> tdDict = generateTdDict(); //下列两个方法都会用到此tdDict，直接算出来即可（此tdDict不会为null，下面无需做null值判断）

				if (colorSelectedIndex <= 0)
				{
					// 当没有选中色块时（本Form中等同于没有任何色块），只有当tdDict不为空，才需要单独生成只有固定通道数据的singleMat
					if (tdDict.Count > 0)
					{
						singleMat = MaterialAst.GenerateMaterialAst(mainForm.CurrentMode , tdDict);
					}
				}
				// 当选中了某个色块时，先生成该色块的单步素材，再混合一下tdDict后，进行单步预览
				else {
					// 当色块发生变化时，才重新生成colorMat（避免资源浪费）
					if (colorChanged) { 
						colorTongdaoArray = new TongdaoWrapper[1, colorTdNameList.Count];

						Panel colorPanel = colorFLP.Controls[colorSelectedIndex] as Panel;
						Color bColor = colorPanel.BackColor;
						int stepTime = decimal.ToInt32((colorPanel.Controls[0] as NumericUpDown).Value / oldEachStepTime);

						colorTongdaoArray[0, 0] = new TongdaoWrapper(dimmerStr, tgTrackBar.Value, 50, 0);
						colorTongdaoArray[0, 1] = new TongdaoWrapper(rStr, bColor.R, stepTime, 0);
						colorTongdaoArray[0, 2] = new TongdaoWrapper(gStr, bColor.G, stepTime, 0);
						colorTongdaoArray[0, 3] = new TongdaoWrapper(bStr, bColor.B, stepTime, 0);

						colorMat = new MaterialAst
						{
							StepCount = 1,
							TdNameList = colorTdNameList,
							TongdaoArray = colorTongdaoArray,
						};
					}					

					singleMat = MaterialAst.ProcessMaterialAst(colorMat, tdDict);
				}

				mainForm.OneStepPlay(singleMat);
				previewButton.Text = mainForm.IsPreviewing ? "停止预览" : "预览";
			}
		}

		/// <summary>
		///  事件：点击《预览|停止预览》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void previewButton_Click(object sender, EventArgs e)
		{
			if (!mainForm.IsConnected)
			{
				setNotice("尚未连接设备", true, true);
				return;
			}

			// 如果正在预览中，则停止预览（不需生成material）
			if (mainForm.IsPreviewing)
			{
				endView();
				// 停止预览后，恢复 单灯单步 
				oneStepPlay(false);   //previewButton_Click() 内 停止预览
				setNotice("已停止预览。", false, true);
			}
			else if (generateComplexMaterial())
			{
				mainForm.PreviewButtonClick(complexMaterial);
				previewButton.Text = "停止预览";
				setNotice("正在预览复合素材", false, true);
			}
		}

		/// <summary>
		/// 辅助方法：生成综合素材（1.素材 2.动作、颜色 3.固定通道）
		/// </summary>
		/// <returns></returns>
		private bool generateComplexMaterial()
		{
			// 先设为null，在后面的代码中如果没有任何素材被加入，则可以认定为无效素材
			complexMaterial = null; 

			// 生成选择的素材（保存的）
			if (materialCB.Checked)
			{
				string iniPath = getIniPath();
				if ( iniPath != null )
				{
					complexMaterial = MaterialAst.GenerateMaterialAst( mainForm.CurrentMode,iniPath) ; 
				}				
			}

			// 生成动作素材
			if ( generateActionMaterial() )
			{
				complexMaterial = MaterialAst.ComplexMaterialAst(complexMaterial, actionMaterial);				
			}

			// 生成调色素材（与动作同级，谁先谁后皆可）
			if (  generateColorComplexMaterial() ){
				complexMaterial = MaterialAst.ComplexMaterialAst(complexMaterial, colorMat);				
			}

			if (complexMaterial == null)
			{
				setNotice("请选择至少一个素材来源，且该来源数据非空。", true, true);
				return false;
			}
			else
			{				
				complexMaterial = MaterialAst.ProcessMaterialAst(complexMaterial , generateTdDict() );
				return true;
			}
		}
			
		/// <summary>
		/// 辅助方法：结束预览
		/// </summary>
		private void endView()
		{
			if (mainForm.IsConnected && mainForm.IsPreviewing)
			{
				mainForm.PreviewButtonClick(null);
				previewButton.Text = "预览";
				setNotice("已停止预览", false, true);
			}
		}
		
		/// <summary>
		/// 事件：点击《应用》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void enterButton_Click(object sender, EventArgs e)
		{

		}

		#region 生成外部素材相关

		private void materialTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{

		}

		/// <summary>
		///  辅助方法：供删除及使用素材使用，通过此方法可以直接获取选中项的物理路径
		/// </summary>
		/// <returns></returns>
		private string getIniPath()
		{
			// 1.先验证是否刚删除素材 或 空选
			if (materialTreeView.SelectedNode == null)
			{
				MessageBox.Show("请选择正确的素材名");
				return null;
			}

			//2. 验证是否子节点，父节点不是素材
			if (materialTreeView.SelectedNode.Level == 0)
			{
				MessageBox.Show("请选择素材树的子节点。");
				return null;
			}

			//3.验证素材名是否为空
			string materialName = materialTreeView.SelectedNode.Text;
			if (String.IsNullOrEmpty(materialName))
			{
				MessageBox.Show("素材名不得为空。");
				return null;
			}

			string astPath = materialTreeView.SelectedNode.Parent.Text.Equals(LanguageHelper.TranslateSentence("通用素材")) ? generalStr : specialStr;
			string iniPath = materialPath + astPath + materialName + ".ini";

			return iniPath;
		}
		
		#endregion	

		#region 生成actionMaterial相关

		/// <summary>
		/// 辅助方法：生成动作的素材（供预览和使用动作）
		/// </summary>
		private bool generateActionMaterial()
		{
			if (actionCB.Visible && actionCB.Checked)
			{
				int tabIndex = actionTabControl.SelectedIndex;
				switch (tabIndex)
				{
					case 0: drawLine(); break;
					case 1: drawCircle(); break;
					case 2: drawSemicircle(); break;
					case 3: drawWave(); break;
					case 4: draw8(); break;
				}
				if (actionMaterial == null)
				{
					setNotice("生成动作数据出错，请重试", true, true);
					return false;
				}
				return true;
			}
			else {
				return false;
			}			
		}

		#region 画直线相关的方法：包括验证，生成数据等；

		/// <summary>
		///  辅助方法：制造画圆的图形
		/// </summary>
		private void drawLine()
		{
			int xValue = decimal.ToInt32(lineXNumericUpDown.Value);
			int y1Value = decimal.ToInt32(lineY1NumericUpDown.Value);
			int y2Value = decimal.ToInt32(lineY2NumericUpDown.Value);
			int phase = decimal.ToInt32(linePhaseNumericUpDown.Value) - 1;

			int stepCount = 4;
			int tongdaoCount = 2;

			// 为tongdaoList赋值 
			TongdaoWrapper[,] tongdaoList = new TongdaoWrapper[stepCount, tongdaoCount];

			// X轴值固定，无需处理相位
			tongdaoList[0, 0] = new TongdaoWrapper(xStr, xValue, commonStepTime);
			tongdaoList[1, 0] = new TongdaoWrapper(xStr, xValue, commonStepTime);
			tongdaoList[2, 0] = new TongdaoWrapper(xStr, xValue, commonStepTime);
			tongdaoList[3, 0] = new TongdaoWrapper(xStr, xValue, commonStepTime);

			tongdaoList[(0 + phase) % stepCount, 1] = new TongdaoWrapper(yStr, y1Value, commonStepTime);
			tongdaoList[(1 + phase) % stepCount, 1] = new TongdaoWrapper(yStr, y1Value + y2Value / 2, commonStepTime);
			tongdaoList[(2 + phase) % stepCount, 1] = new TongdaoWrapper(yStr, y1Value + y2Value, commonStepTime);
			tongdaoList[(3 + phase) % stepCount, 1] = new TongdaoWrapper(yStr, y1Value + y2Value / 2, commonStepTime);

			actionMaterial = new MaterialAst
			{
				StepCount = stepCount,
				TdNameList = actionTdNameList,
				TongdaoArray = tongdaoList
			};
		}

		/// <summary>
		/// 事件：当Y1发生变化时，动态调整Y2的最大值（避免超过范围）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lineY1NumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			int y1Value = decimal.ToInt32(lineY1NumericUpDown.Value);
			lineY2NumericUpDown.Maximum = 255 - y1Value;
		}

		/// <summary>
		/// 事件：更改了步时间之后，应该调整相应的stepTime
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lineStNumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			commonStepTime = decimal.ToInt32(StNumericUpDown.Value / mainForm.EachStepTime2);

			// 需要在这里对波浪的倍率项进行限制
			waveTimesNumericUpDown.Maximum = 250 / commonStepTime;

		}

		#endregion

		#region 画圆及半圆相关的方法：包括验证，生成数据等；

		/// <summary>
		/// 辅助方法：生成圆的素材
		/// </summary>
		private void drawCircle()
		{
			int xValue = decimal.ToInt32(circleXNumericUpDown.Value);
			int yValue = decimal.ToInt32(circleYNumericUpDown.Value);
			int phase = decimal.ToInt32(circlePhaseNumericUpDown.Value) - 1;

			int stepCount = 4;
			int tongdaoCount = 2;

			// 为tongdaoList赋值 
			TongdaoWrapper[,] tongdaoList = new TongdaoWrapper[stepCount, tongdaoCount];

			tongdaoList[(0 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, 0, commonStepTime);
			tongdaoList[(1 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, xValue / 2, commonStepTime);
			tongdaoList[(2 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, xValue, commonStepTime);
			tongdaoList[(3 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, xValue / 2, commonStepTime);

			//　Y轴每步都一样，无需处理相位。
			tongdaoList[0, 1] = new TongdaoWrapper(yStr, yValue, commonStepTime);
			tongdaoList[1, 1] = new TongdaoWrapper(yStr, yValue, commonStepTime);
			tongdaoList[2, 1] = new TongdaoWrapper(yStr, yValue, commonStepTime);
			tongdaoList[3, 1] = new TongdaoWrapper(yStr, yValue, commonStepTime);


			actionMaterial = new MaterialAst
			{
				StepCount = stepCount,
				TdNameList = actionTdNameList,
				TongdaoArray = tongdaoList
			};
		}

		/// <summary>
		/// 辅助方法：生成半圆的素材
		/// </summary>
		private void drawSemicircle()
		{
			int xValue = decimal.ToInt32(scXNumericUpDown.Value);
			int yValue = decimal.ToInt32(scYNumericUpDown.Value);
			int phase = decimal.ToInt32(semicirclePhaseNumericUpDown.Value) - 1;

			int stepCount = 4;
			int tongdaoCount = 2;

			// 为tongdaoList赋值 
			TongdaoWrapper[,] tongdaoList = new TongdaoWrapper[stepCount, tongdaoCount];

			// 其实和圆的代码完全相同，区别只在于圆的X轴 转向需要超过170（而半圆必须小于170）
			tongdaoList[(0 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, 0, commonStepTime);
			tongdaoList[(1 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, xValue / 2, commonStepTime);
			tongdaoList[(2 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, xValue, commonStepTime);
			tongdaoList[(3 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, xValue / 2, commonStepTime);

			//Y轴每步都一样，无需处理相位。
			tongdaoList[0, 1] = new TongdaoWrapper(yStr, yValue, commonStepTime);
			tongdaoList[1, 1] = new TongdaoWrapper(yStr, yValue, commonStepTime);
			tongdaoList[2, 1] = new TongdaoWrapper(yStr, yValue, commonStepTime);
			tongdaoList[3, 1] = new TongdaoWrapper(yStr, yValue, commonStepTime);

			actionMaterial = new MaterialAst
			{
				StepCount = stepCount,
				TdNameList = actionTdNameList,
				TongdaoArray = tongdaoList
			};
		}

		#endregion

		#region 画波浪相关的方法：包括验证，生成数据等；

		/// <summary>
		/// 生成波浪的素材
		/// </summary>
		private void drawWave()
		{
			int x1Value = decimal.ToInt32(waveX1NumericUpDown.Value);
			int x2Value = decimal.ToInt32(waveX2NumericUpDown.Value);
			int y1Value = decimal.ToInt32(waveY1NumericUpDown.Value);
			int y2Value = decimal.ToInt32(waveY2NumericUpDown.Value);
			int times = decimal.ToInt32(waveTimesNumericUpDown.Value);

			int stepCount = 2;
			int tongdaoCount = 2;

			// 为tongdaoList赋值 
			TongdaoWrapper[,] tongdaoList = new TongdaoWrapper[stepCount, tongdaoCount];

			tongdaoList[0, 0] = new TongdaoWrapper(xStr, x1Value, commonStepTime * times);
			tongdaoList[1, 0] = new TongdaoWrapper(xStr, x1Value + x2Value, commonStepTime * times);

			tongdaoList[0, 1] = new TongdaoWrapper(yStr, y1Value, commonStepTime);
			tongdaoList[1, 1] = new TongdaoWrapper(yStr, y1Value + y2Value, commonStepTime);

			actionMaterial = new MaterialAst
			{
				StepCount = stepCount,
				TdNameList = actionTdNameList,
				TongdaoArray = tongdaoList
			};

		}

		/// <summary>
		///  当《X轴初始位置》发生改变后，《X轴变化幅度》也要有所限制
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void waveX1NumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			int x1Value = decimal.ToInt32(waveX1NumericUpDown.Value);
			waveX2NumericUpDown.Maximum = 255 - x1Value;
		}

		/// <summary>
		///  当《Y轴初始位置》发生改变后，《Y轴摆动幅度》也要有所限制
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void waveY1NumericUpDown_ValueChanged(object sender, EventArgs e)
		{
			int y1Value = decimal.ToInt32(waveY1NumericUpDown.Value);
			waveY2NumericUpDown.Maximum = 127 - y1Value;
		}

		#endregion

		#region 画8字相关方法：包括验证，生成数据等；

		/// <summary>
		/// 辅助方法：生成8字的素材
		/// </summary>
		private void draw8()
		{
			int yValue = decimal.ToInt32(eightYNumericUpDown.Value);
			int phase = decimal.ToInt32(eightPhaseNumericUpDown.Value) - 1;

			int stepCount = 4;
			int tongdaoCount = 2;

			// 为tongdaoList赋值 
			TongdaoWrapper[,] tongdaoList = new TongdaoWrapper[stepCount, tongdaoCount];

			tongdaoList[(0 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, 0, commonStepTime);
			tongdaoList[(1 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, 85, commonStepTime);
			tongdaoList[(2 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, 85, commonStepTime);
			tongdaoList[(3 + phase) % stepCount, 0] = new TongdaoWrapper(xStr, 0, commonStepTime);

			tongdaoList[(0 + phase) % stepCount, 1] = new TongdaoWrapper(yStr, yValue, commonStepTime);
			tongdaoList[(1 + phase) % stepCount, 1] = new TongdaoWrapper(yStr, yValue, commonStepTime);
			tongdaoList[(2 + phase) % stepCount, 1] = new TongdaoWrapper(yStr, 255 - yValue, commonStepTime);
			tongdaoList[(3 + phase) % stepCount, 1] = new TongdaoWrapper(yStr, 255 - yValue, commonStepTime);

			actionMaterial = new MaterialAst
			{
				StepCount = stepCount,
				TdNameList = actionTdNameList,
				TongdaoArray = tongdaoList
			};

		}

		#endregion

		#endregion

		#region 生成colorMaterial相关

		/// <summary>
		/// 辅助方法：生成所有颜色组合的【素材】
		/// </summary>
		/// <returns></returns>
		private bool generateColorComplexMaterial()
		{
			if (colorCB.Visible && colorCB.Checked)
			{
				if (colorStepCount == 0)
				{
					setNotice("尚未添加颜色块。", true, true);
					return false;
				}
				else
				{
					try
					{
						colorTongdaoArray = new TongdaoWrapper[colorStepCount, colorTdNameList.Count];
						for (int panelIndex = 1; panelIndex <= colorStepCount; panelIndex++)
						{
							Panel colorPanel = colorFLP.Controls[panelIndex] as Panel;

							// 常规模式，取控件内的值；音频模式，取固定值；
							int stepTime = mainForm.CurrentMode == 0 ? decimal.ToInt32((colorPanel.Controls[0] as NumericUpDown).Value / oldEachStepTime) : soundStepTime;
							int changeMode = mainForm.CurrentMode == 0 ? ((colorPanel.Controls[1] as CheckBox).Checked ? 1 : 0) : 1;

							colorTongdaoArray[panelIndex - 1, 0] = new TongdaoWrapper(dimmerStr, tgTrackBar.Value, stepTime, changeMode);
							colorTongdaoArray[panelIndex - 1, 1] = new TongdaoWrapper(rStr, colorPanel.BackColor.R, stepTime, changeMode);
							colorTongdaoArray[panelIndex - 1, 2] = new TongdaoWrapper(gStr, colorPanel.BackColor.G, stepTime, changeMode);
							colorTongdaoArray[panelIndex - 1, 3] = new TongdaoWrapper(bStr, colorPanel.BackColor.B, stepTime, changeMode);

							colorMat = new MaterialAst
							{
								StepCount = colorStepCount,
								TdNameList = colorTdNameList,
								TongdaoArray = colorTongdaoArray,
							};
						}
						return true;
					}
					catch (Exception ex)
					{
						setNotice("生成数据出错：" + ex.Message, true, false);
						return false;
					}
				}
			}
			else {
				return false;
			}	
		}

		/// <summary>
		/// 事件：点击《添加(色块)》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void colorAddButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.Cancel == myColorDialog.ShowDialog())
			{
				return;
			}

			Panel colorPanel = new Panel()
			{
				Size = colorPanelDemo.Size,
				Margin = colorPanelDemo.Margin,
				Visible = true,
				BackColor = myColorDialog.Color,
			};
			colorPanel.Click += colorPanel_Click;

			NumericUpDown stNUD = new NumericUpDown()
			{
				Font = stNUDDemo.Font,
				Location = stNUDDemo.Location,
				Size = stNUDDemo.Size,
				TextAlign = stNUDDemo.TextAlign,
				DecimalPlaces = stNUDDemo.DecimalPlaces,
				Maximum = stNUDDemo.Maximum * mainForm.EachStepTime2,
				Minimum = mainForm.EachStepTime2,
				Increment = mainForm.EachStepTime2,
				Value = mainForm.EachStepTime2 * commonStepTime,
				Visible = mainForm.CurrentMode == 0,
			};
			stNUD.MouseWheel += someNUD_MouseWheel;
			stNUD.KeyPress += stNUD_KeyPress;

			CheckBox cmCB = new CheckBox()
			{
				Font = cmCBDemo.Font,
				Location = cmCBDemo.Location,
				Size = cmCBDemo.Size,
				TextAlign = cmCBDemo.TextAlign,
				Text = cmCBDemo.Text,
				ForeColor = cmCBDemo.ForeColor,
				BackColor = cmCBDemo.BackColor,
				Visible = mainForm.CurrentMode == 0,
			};
			cmCB.KeyPress += cmCheckBox_KeyPress;

			colorPanel.Controls.Add(stNUD);
			colorPanel.Controls.Add(cmCB);

			colorFLP.Controls.Add(colorPanel);
			colorSelectedIndex = colorFLP.Controls.IndexOf(colorPanel);

			selectColorPanel(); //addButton_Click

		}

		/// <summary>
		/// 事件：点击《修改(色块)》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void colorEditButton_Click(object sender, EventArgs e)
		{
			Color selectedColor = colorFLP.Controls[colorSelectedIndex].BackColor;
			myColorDialog.Color = selectedColor;  // 把选中色块的颜色，放给myColorDialog

			if (DialogResult.Cancel == myColorDialog.ShowDialog())
			{
				return;
			}
			colorFLP.Controls[colorSelectedIndex].BackColor = myColorDialog.Color;

			selectColorPanel(); //editButton_Click
		}

		/// <summary>
		/// 事件：点击《删除(色块)》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void colorDeleteButton_Click(object sender, EventArgs e)
		{
			colorFLP.Controls.RemoveAt(colorSelectedIndex);
			colorSelectedIndex = -1;
			selectColorPanel(); //deleteButton_Click
		}

		/// <summary>
		/// 事件：点击《清空(色块)》按键
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void colorClearButton_Click(object sender, EventArgs e)
		{
			for (int panelIndex = colorFLP.Controls.Count - 1; panelIndex > 0; panelIndex--)
			{
				colorFLP.Controls.RemoveAt(panelIndex);
			}
			colorSelectedIndex = -1;
			selectColorPanel(); //clearButton_Click
		}

		/// <summary>
		/// 事件：点击《色块(Panel)》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void colorPanel_Click(object sender, EventArgs e)
		{
			colorSelectedIndex = colorFLP.Controls.IndexOf(sender as Panel);
			selectColorPanel(); //colorPanel_Click
		}

		/// <summary>
		/// 辅助方法：更改selectedPanelIndex后，刷新相应的一些控件；并oneStepPlay(true)
		/// </summary>
		private void selectColorPanel()
		{
			astPanel.BackColor = colorSelectedIndex > 0 ? (colorFLP.Controls[colorSelectedIndex] as Panel).BackColor : Color.MintCream;
			astLabel.Text = colorSelectedIndex > 0 ? colorSelectedIndex + "" : "未选中步";
			editButton.Enabled = colorSelectedIndex > 0;
			colorDeleteButton.Enabled = colorSelectedIndex > 0;

			colorStepCount = colorFLP.Controls.Count - 1;
			clearButton.Enabled = colorStepCount > 0;
			
			oneStepPlay(true); //selectColorPanel
		}
		
		/// <summary>
		/// 事件：(总)调光滑动杆值发生变化
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tgTrackBar_ValueChanged(object sender, EventArgs e)
		{
			TrackBar tBar = sender as TrackBar;

			tgNUD.ValueChanged -= tgNUD_ValueChanged;
			tgNUD.Value = tBar.Value;
			tgNUD.ValueChanged += tgNUD_ValueChanged;

			oneStepPlay(true); //tgTrackBar_ValueChanged
		}

		/// <summary>
		/// 事件：(总)调光nud的值发生变化
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tgNUD_ValueChanged(object sender, EventArgs e)
		{
			NumericUpDown nud = sender as NumericUpDown;
			tgTrackBar.ValueChanged -= tgTrackBar_ValueChanged;
			tgTrackBar.Value = decimal.ToInt32(nud.Value);
			tgTrackBar.ValueChanged += tgTrackBar_ValueChanged;

			oneStepPlay(true); //tgNUD_ValueChanged
		}

		/// <summary>
		/// 事件：《步时间NUD》的键盘点击事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void stNUD_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (colorStepCount > 1 && (e.KeyChar == 'a' || e.KeyChar == 'A'))
			{

				decimal unifySt = (sender as NumericUpDown).Value;

				// 设置了提示，且用户点击了取消，则return。否则继续往下走
				if (mainForm.IsNoticeUnifyTd)
				{
					if (DialogResult.Cancel == MessageBox.Show(
							LanguageHelper.TranslateSentence("确定要将所有步时间都设为") + "【" + unifySt + " S】?",
							LanguageHelper.TranslateSentence("统一步时间"),
							MessageBoxButtons.OKCancel,
							MessageBoxIcon.Question))
					{
						return;
					}
				}

				for (int controlIndex = 1; controlIndex < colorFLP.Controls.Count; controlIndex++)
				{
					((colorFLP.Controls[controlIndex] as Panel).Controls[0] as NumericUpDown).Value = unifySt;
				}
			}
		}

		/// <summary>
		/// 事件：《跳渐变复选框》的键盘点击事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmCheckBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (colorStepCount > 1 && (e.KeyChar == 'a' || e.KeyChar == 'A'))
			{
				bool unifyCM = (sender as CheckBox).Checked;
				string cmStr = unifyCM ? "渐变" : "跳变";

				// 设置了提示，且用户点击了取消，则return。否则继续往下走
				if (mainForm.IsNoticeUnifyTd)
				{
					if (DialogResult.Cancel == MessageBox.Show(
							LanguageHelper.TranslateSentence("确定要将所有跳渐变都设为【" + cmStr + "】吗?"),
							LanguageHelper.TranslateSentence("统一跳渐变"),
							MessageBoxButtons.OKCancel,
							MessageBoxIcon.Question))
					{
						return;
					}
				}

				for (int controlIndex = 1; controlIndex < colorFLP.Controls.Count; controlIndex++)
				{
					((colorFLP.Controls[controlIndex] as Panel).Controls[1] as CheckBox).Checked = unifyCM;
				}
			}
		}

		#endregion

		#region 生成固定通道(tdDict)相关

		/// <summary>
		/// 事件：更改tdCB的选中项（并因此定义右侧《添加》按键是否可用）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			enableTdAddButton();
		}		

		/// <summary>
		/// 事件：点击《添加（固定通道）》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdAddButton_Click(object sender, EventArgs e)
		{
			for (int panelIndex = tdFLP.Controls.Count - 1; panelIndex > 0; panelIndex--)
			{
				if (tdFLP.Controls[panelIndex].Controls[0].Text.Contains(tdComboBox.Text))
				{
					setNotice("此通道已存在，请不要重复添加。", true, true);
					return;
				}
			}

			Panel tdPanel = new Panel
			{
				Location = tdPanelDemo.Location,
				Size = tdPanelDemo.Size,
				BorderStyle = tdPanelDemo.BorderStyle,
			};

			Label tdLabel = new Label
			{
				AutoSize = tdLabelDemo.AutoSize,
				Location = tdLabelDemo.Location,
				Size = tdLabelDemo.Size,
				Text = tdComboBox.Text,
			};

			NumericUpDown tdNUD = new NumericUpDown
			{
				Location = tdNUDDemo.Location,
				Maximum = tdNUDDemo.Maximum,
				Size = tdNUDDemo.Size,
				TextAlign = tdNUDDemo.TextAlign,
				Value = stepTemplate.TongdaoList[tdComboBox.SelectedIndex].ScrollValue,
			};
			tdNUD.ValueChanged += tdNUD_ValueChanged;	

			Button tdDelButton = new Button
			{
				Location = tdDelButtonDemo.Location,
				Size = tdDelButtonDemo.Size,
				Text = "-",
				UseVisualStyleBackColor = tdDelButtonDemo.UseVisualStyleBackColor
			};
			tdDelButton.Click += tdDelButton_Click;

			tdPanel.Controls.Add(tdLabel);
			tdPanel.Controls.Add(tdNUD);
			tdPanel.Controls.Add(tdDelButton);
			tdFLP.Controls.Add(tdPanel);

			enableTdAddButton();
			oneStepPlay(false); // tdAddButton_Click() 
		}

		/// <summary>
		/// 事件：点击《删除（固定通道）》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdDelButton_Click(object sender, EventArgs e)
		{
			tdFLP.Controls.Remove((sender as Button).Parent);
			enableTdAddButton();
			oneStepPlay(false); //tdDelButton_Click
		}

		/// <summary>
		///  事件：点击《清空（固定通道）》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdClearButton_Click(object sender, EventArgs e)
		{
			for (int panelIndex = tdFLP.Controls.Count - 1; panelIndex > 0; panelIndex--)
			{
				tdFLP.Controls.RemoveAt(panelIndex);
			}
			enableTdAddButton(); 
			oneStepPlay(false); //tdClearButton_Click
		}
		
		/// <summary>
		/// 辅助方法：更改了《固定通道》值 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tdNUD_ValueChanged(object sender, EventArgs e)
		{
			oneStepPlay(false); //tdNUD_ValueChanged
		} 

		/// <summary>
		/// 辅助方法：生成固定通道列表;若不存在任何通道，则返回空的Dictionary（非null）
		/// </summary>
		/// <returns></returns>
		private Dictionary<string, int> generateTdDict()
		{
			Dictionary<string, int> tdDict = new Dictionary<string, int>();
			for (int panelIndex = 1; panelIndex < tdFLP.Controls.Count; panelIndex++)
			{
				Panel tdPanel = tdFLP.Controls[panelIndex] as Panel;
				tdDict.Add(
					tdPanel.Controls[0].Text.Trim(),
					decimal.ToInt32((tdPanel.Controls[1] as NumericUpDown).Value)
				);
			}
			return tdDict;
		}

		/// <summary>
		/// 辅助方法：根据tdComboBox的选中项，确定添加按键是否可用
		/// </summary>
		private void enableTdAddButton()
		{
			tdAddButton.Enabled = true;
			for (int panelIndex = tdFLP.Controls.Count - 1; panelIndex > 0; panelIndex--)
			{
				if (tdFLP.Controls[panelIndex].Controls[0].Text.Contains(tdComboBox.Text))
				{
					tdAddButton.Enabled = false;
					return;
				}
			}
		}

		#endregion

		#region 通用方法

		/// <summary>
		/// 辅助方法：设置提醒
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="msgBoxShow"></param>
		private void setNotice(string msg, bool msgBoxShow, bool isTranslate)
		{
			if (isTranslate)
			{
				msg = LanguageHelper.TranslateSentence(msg);
			}

			myStatusLabel.Text = msg;
			if (msgBoxShow)
			{
				MessageBox.Show(msg);
			}
		}

		/// <summary>
		/// 辅助方法：某些控件的文本发生变化时， 需要进行翻译
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someControl_TextChanged(object sender, EventArgs e)
		{
			LanguageHelper.TranslateControl(sender as Control);
		}

		/// <summary>
		/// 验证：对某些NumericUpDown进行鼠标滚轮的验证，避免一次性滚动过多
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someNUD_MouseWheel(object sender, MouseEventArgs e)
		{
			NumericUpDown nud = sender as NumericUpDown;
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true;
			}
			// 向上滚
			if (e.Delta > 0)
			{
				decimal dd = nud.Value + nud.Increment;
				if (dd <= nud.Maximum)
				{
					nud.Value = dd;
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				decimal dd = nud.Value - nud.Increment;
				if (dd >= nud.Minimum)
				{
					nud.Value = dd;
				}
			}
		}

		/// <summary>
		///  验证：对某些TrackBar进行鼠标滚轮的验证，避免一次性滚动过多（与OS设置有关）
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void someTrackBar_MouseWheel(object sender, MouseEventArgs e)
		{

			TrackBar tb = sender as TrackBar;
			HandledMouseEventArgs hme = e as HandledMouseEventArgs;
			if (hme != null)
			{
				hme.Handled = true; //设为true则屏蔽之后系统自行处理的操作（就是原来加3(Win10)之类的操作）
			}
			// 向上滚
			if (e.Delta > 0)
			{
				int dd = tb.Value + tb.SmallChange;
				if (dd <= tb.Maximum)
				{
					tb.Value = dd;
				}
			}
			// 向下滚
			else if (e.Delta < 0)
			{
				int dd = tb.Value - tb.SmallChange;
				if (dd >= tb.Minimum)
				{
					tb.Value = dd;
				}
			}
		}


		#endregion
				
	}
}
