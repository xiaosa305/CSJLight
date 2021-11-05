using LightController.Ast;
using LightController.Common;
using LightController.DAO;
using LightController.MyForm.Project;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm
{
    public partial class MainFormBase : UIForm
    {
		// 几个全局的辅助控件（导出文件、toolTip提示等）
		protected FolderBrowserDialog exportFolderBrowserDialog;
		protected ToolTip myToolTip;

		//各类提示
		protected string protocolNotice = "1.选择不同协议，会将场景名更改为该协议的命名；\n" +
			"2.’==...==‘之前的协议为excel表格中的协议，之后的协议为用户自定义协议;\n" +
			"3.如需恢复默认(无场景名)，请选择'==============='。";
		protected string copySceneNotice = "使用本功能，将从其他场景复制到当前场景 或 将当前场景复制到其他场景。";
		protected string keepNotice = "点击此按钮后，当前未选中的其它灯具将会保持它们最后调整时的状态，方便调试。";
		protected string insertNotice = "左键点击此按钮为后插步(即在当前步之后添加新步)，\n右键点击此按钮为前插步(即在当前步之前添加新步)。";
		protected string appendNotice = "右击可追加多步";
		protected string deleteNotice = "右击可删除多步";
		protected string backStepNotice = "右击可跳转至第一步";
		protected string nextStepNotice = "右击可跳转至最后一步";

		// 跳渐变
		protected object[] normalCMArray;
		protected object[] soundCMArray;

		// 全局配置及数据库连接				
		public string SoftwareName;  //动态载入软件名（前半部分）后半部分需自行封装
		protected string projectStr;
		public string ConnectStr = " [ 设备未连接 ]";
		public string SavePath; // 动态载入相关的存储目录（开发时放在C:\Temp中；发布时放在应用所在文件夹）	
		
		public bool IsNoticeUnifyTd = true;

		// 打开程序时，即需导入的变量（全局静态变量，其他form可随时使用）	
		public static IList<string> AllSceneList; // 将所有场景名称写在此处,并供所有类使用（动态导入场景到此静态变量中）
		public static int SceneCount = 0;  //场景数量
		public static int MAX_StTimes = 250;  //每步 时间因子可乘的 最大倍数 如 0.04s*250= 10s ; 应设为常量	-》200331确认为15s=0.03*500	
		public static int MAX_STEP = 100;  //每个场景的最大步数，动态由配置文件在打开软件时读取（换成音频场景时也要发生变化，因为音频模式的步数上限不同）
		public bool IsShowSaPanels = true; // 是否显示 子属性面板
		public static int DefaultSoundCM = 0; // 添加音频步数时，其跳渐变默认值（可由配置文件进行改变）	
		protected List<int> tdValues = null;  // 要实时显示单步数据的通道列表		

		// 全局辅助变量
		//protected bool isInit = false;// form都初始化后，才将此变量设为true;为防止某些监听器提前进行监听
		public MaterialAst TempMaterialAst = null;  // 辅助（复制多步、素材）变量 ， 《复制、粘贴多步》时使用		
		//protected MaterialUseForm materialUseForm = null; // 存储一个materialForm界面的实例，初次使用时新建

		// 程序运行后，动态变化的变量
		protected string arrangeIniPath = null;  // 打开工程时 顺便把相关的位置保存ini(arrange.ini) 也读取出来（若有的话）
		protected bool isAutoArrange = true; // 默认情况下，此值为true，代表右键菜单“自动排列”默认情况下是打开的。		

		// 工程相关的变量（只在工程载入后才用到的变量）
		public string currentProjectName;  //存放当前工程名，主要作用是防止当前工程被删除（openForm中）
		protected string currentProjectPath; //存放当前工程所在目录
		public string GlobalIniPath;  // 存放当前工程《全局配置》、《摇麦设置》的配置文件的路径
		protected string dbFilePath;  // 211008  新的db存储路径
		private string exportPath; // 导出工程的目录（每次都可能会发生变动）
		protected bool isEncrypt = false; //是否加密				
		public decimal EachStepTime = 0.03m; //默认情况下，步时间默认值为0.03s（=30ms）
		protected string groupIniPath; // 存放编组文件存放路径
		public IList<GroupAst> GroupList; // 存放编组列表（public 因为多步联调等Form会用到）

		//public DetailMultiAstForm DmaForm; //存储一个全局的DetailMultiAstForm，用以记录之前用户选过的将进行多步联调的通道
		public Dictionary<int, List<int>> TdDict; // 存储一个字典，在DmaForm中点击确认后，修改这个数据

		//MARK 只开单场景：00.2 ①必须有一个存储所有场景是否需要保存的bool[];②若为true，则说明需要保存
		protected bool[] sceneSaveArray;
		//MARK 只开单场景：00.3 ①必须有一个存储所有场景数据是否已经由DB载入的bool[];②若为true，则说明不用再从数据库内取数据了
		protected bool[] sceneLoadArray;

		// 数据库DAO(data access object：数据访问对象）
		protected LightDAO lightDAO;
		protected FineTuneDAO fineTuneDAO;
		protected ChannelDAO channelDAO;

		// 这几个IList ，存放着界面中相关的灯具数据（包括通道数据之类等）
		public IList<LightAst> LightAstList;  //与《灯具编辑》通信用的变量；同时也可以供一些辅助form读取相关灯具的简约信息时使用 --> 这张表需要给多步联调使用（sawList）
		public IList<LightWrapper> LightWrapperList;   //灯具变量：记录所有灯具（lightWrapper）的（所有场景和模式）的 每一步（通道列表）
		protected ImageList lightImageList; //灯具图片列表，从硬盘中读取后放到内存里

		// 通道数据操作时的变量		
		protected bool isSyncMode = false;  // 同步模式为true；异步模式为false(默认）	
		protected bool isMultiMode = false; //默认情况下是单灯模式；若进入编组模式，此变量改成true；	

		protected int selectedIndex = -1; //选择的灯具的index，默认为-1，如有选中灯具，则改成该灯具的index（在lightAstList、lightWrapperList中）
		protected IList<int> selectedIndexList = new List<int>();  //选择的灯具的index列表（多选情况下）

		public int CurrentProtocol = -1; // 表示协议index
		public int CurrentScene = 0; // 表示场景编号(selectedIndex)
		public int CurrentMode = 0;  // 表示模式编号(selectedIndex)；0.常规模式； 1.音频模式

		protected StepWrapper tempStep = null; //// 辅助步变量：复制及粘贴步时用到
		protected int tempStepMode;  // 辅助变量，存储点击《复制步》时的模式
		protected bool from0on = false; // 辅助变量，避免重复渲染子属性按钮组

		// 调试变量
		//public ConnectForm ConnForm; // 《设备连接》的窗口，只留一个实体即可
		//public NetworkConnect MyConnect;  // 与设备的连接
		//protected DMX512ConnnectForm dmxConnForm; //《DMX512调试线连接》的窗口，只留一个实体即可	

		//protected Player networkPlayer = Player.GetPlayer(); // 通过设备，调试512灯具的对象
		//public SerialPortPlayer SerialPlayer = SerialPortPlayer.GetPlayer();     // 通过DMX512调试线直连设备，调试512灯具的对象

		public bool IsDeviceConnected = false; // 辅助bool值，当选择《连接设备》后，设为true；反之为false
		public bool IsDMXConnected = false; // 辅助bool值，当DMX512线已经连接时设为true，反之为false
		protected bool isKeepOtherLights = false;  // 辅助bool值，当选择《（非调灯具)保持状态》时，设为true；反之为false
		public bool IsPreviewing = false; // 是否预览状态中
		public long LastSendTime; // 记录最近一次StartDebug的时间戳，之后如果要发StopPreview，需要等这个时间过2s才进行；		


		public MainFormBase()
        {
            InitializeComponent();
        }

		private void Form1_Load(object sender, EventArgs e)
		{
			// 先把场景都载入
			initSceneList();
		}

		/// <summary>
		/// 由text文件初始化场景名列表，当内容有误时，直接退出软件；
		/// </summary>
		protected void initSceneList()
		{
			AllSceneList = TextHelper.Read(Application.StartupPath + @"\Protocol\SceneList.txt");
			if (AllSceneList == null || AllSceneList.Count == 0)
			{
				MessageBox.Show(LanguageHelper.TranslateSentence("SceneList.txt中的场景不可为空，否则软件无法使用，请修改后重启。"));
				exit();
			}
			SceneCount = AllSceneList.Count;
		}

		private void panel2_Paint(object sender, PaintEventArgs e)
		{

		}

		private void menuPanel_Paint(object sender, PaintEventArgs e)
		{

		}

		private void uiButton1_Click(object sender, EventArgs e)
		{
			//new ConnectForm().ShowDialog();
		}

		private void uiSymbolButton2_Click(object sender, EventArgs e)
		{
			//Console.WriteLine("uiSymbolButton2_Click");

			////uiSymbolButton2.
			//Enabled = false;
			//Thread.Sleep(2000);

			//Application.DoEvents();

			////uiSymbolButton2.
			//Enabled = true;
		}

		private void skinButton5_Click(object sender, EventArgs e)
		{
			Console.WriteLine("skinButton5_Click");
		}

		private void astPanel_Paint(object sender, PaintEventArgs e)
		{

		}

		private void trackBar1_Scroll(object sender, EventArgs e)
		{

		}

		private void uiImageButton9_Click(object sender, EventArgs e)
		{
			(sender as UIImageButton).Selected = !(sender as UIImageButton).Selected;
		}

		private void uiImageButton12_Click(object sender, EventArgs e)
		{
			stepPanel.Visible = !stepPanel.Visible;
		}

		private void uiSymbolButton6_Click(object sender, EventArgs e)
		{
			uiComboBox2.Enabled = !uiComboBox2.Enabled;
		}

		private void panel3_Paint(object sender, PaintEventArgs e)
		{

		}

		private void uiImageButton2_Click(object sender, EventArgs e)
		{
			//new HardwareSetForm(this).ShowDialog();
		}

		private void openButton_Click(object sender, EventArgs e)
		{
			new OpenForm(this).ShowDialog();
		}

		private void uiSymbolButton7_Click(object sender, EventArgs e)
		{
			lgihtsListView.Visible = false;
		}

		private void uiImageButton3_Click(object sender, EventArgs e)
		{
			//new SeqForm().ShowDialog();
		}

		private void uiImageButton4_Click(object sender, EventArgs e)
		{
			//new ToolsForm(this).ShowDialog();
		}

		private void uiImageButton7_Click(object sender, EventArgs e)
		{
			//new GraphicsStudy().ShowDialog();
		}

		private void uiImageButton8_Click(object sender, EventArgs e)
		{
			//new DrawForm2().ShowDialog();
		}

		private void uiImageButton6_Click(object sender, EventArgs e)
		{
			//FiveForm.GetInstance().ShowDialog();
		}

		private void newButton_Click(object sender, EventArgs e)
		{
			new NewForm().ShowDialog();
		}

		private void soundListButton_Click(object sender, EventArgs e)
		{
			//new SKForm().ShowDialog();
		}

		private void modeCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			soundListButton.Enabled = modeCheckBox.Checked;
		}

		#region 退出程序相关

		/// <summary>
		/// 辅助方法：点击退出时FormClosing事件；
		/// </summary>
		/// <param name="e"></param>
		protected void formClosing(FormClosingEventArgs e)
		{
			////MARK 只开单场景：17.4 FormClosing时提示保存工程
			//if (!RequestSaveProject(LanguageHelper.TranslateSentence("关闭程序前，是否保存当前工程？")))
			//{
			//	e.Cancel = true;
			//}
		}

		/// <summary>
		/// 辅助方法：点击《退出程序》
		/// </summary>
		protected void exitClick()
		{
			////MARK 只开单场景：17.3 点击《退出程序》时，申请保存工程
			//if (!RequestSaveProject("退出应用前，是否保存当前工程？"))
			//{
			//	return;
			//}
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

		#region 测试相关 

		public bool IsShowTestButton = false;

		#endregion
	}
}
