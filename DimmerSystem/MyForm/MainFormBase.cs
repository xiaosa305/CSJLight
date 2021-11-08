using LightController.Ast;
using LightController.Ast.Enum;
using LightController.Common;
using LightController.DAO;
using LightController.Entity;
using LightController.MyForm.LightList;
using LightController.MyForm.Project;
using LightController.MyForm.Step;
using LightEditor.Ast;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Sunny.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LightController.MyForm
{
    public partial class MainFormBase : UIForm
    {
        // 几个全局的辅助控件（导出文件、toolTip提示等）
        //protected FolderBrowserDialog exportFolderBrowserDialog;

        //各类提示
        protected string protocolNotice = "1.选择不同协议，会将场景名更改为该协议的命名；\n" +
            "2.’==...==‘之前的协议为excel表格中的协议，之后的协议为用户自定义协议;\n" +
            "3.如需恢复默认(无场景名)，请选择'==============='。";
        protected string copySceneNotice = "使用本功能，将从其他场景复制到当前场景 或 将当前场景复制到其他场景。";
        protected string keepNotice = "点击此按钮后，当前未选中的其它灯具将会保持它们最后调整时的状态，方便调试。";
        protected string insertNotice = "左键点击此按钮为后插步(即在当前步之后添加新步)，\n右键点击此按钮为前插步(即在当前步之前添加新步)。";
        protected string appendNotice = "右击可追加多步";
        protected string deleteNotice = "右击可删除多步";
        protected string prevStepNotice = "右击可跳转至第一步";
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

        //DOTO DetailMultiAstForm DmaForm
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

        #region 通道信息相关控件

        private Panel[] tdPanels = new Panel[32];
        private Label[] tdNoLabels = new Label[32];
        private Label[] tdNameLabels = new Label[32];
        private TrackBar[] tdTrackBars = new TrackBar[32];
        private NumericUpDown[] tdValueNumericUpDowns = new NumericUpDown[32];
        private ComboBox[] tdCmComboBoxes = new ComboBox[32];
        private NumericUpDown[] tdStNumericUpDowns = new NumericUpDown[32];
        private Panel[] saPanels = new Panel[32];

        #endregion

        public MainFormBase()
        {
            InitializeComponent();

            SoftwareName = InHelper_UTF8.ReadString(Application.StartupPath + @"/GlobalSet.ini", "Show", "softwareName", "");
            SoftwareName += " Dimmer System ";

            string loadexeName = Application.ExecutablePath;
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(loadexeName);
            string appFileVersion = string.Format("{0}.{1}.{2}.{3}", fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart, fileVersionInfo.FileBuildPart, fileVersionInfo.FilePrivatePart);
            SoftwareName += "v" + appFileVersion + " ";

            Text = SoftwareName + projectStr + ConnectStr;

            //从GlobalSet.ini文件读取内容
            SavePath = IniHelper.GetSavePath();
            IsShowTestButton = IniHelper.GetParamBool("testButton");
            IsShowSaPanels = IniHelper.GetParamBool("saPanels");
            IsNoticeUnifyTd = IniHelper.GetParamBool("unifyTd");

            MAX_StTimes = IniHelper.GetSystemCount("maxStTimes", 250);
            MAX_STEP = IniHelper.GetSystemCount("maxStep", 100);
            DefaultSoundCM = IniHelper.GetSystemCount("soundChangeMode", 0);

            LanguageHelper.SetLanguage(IniHelper.GetString("System", "language", "zh-CN").Trim());// 国际化初始化	

            // myToolTips的初始化
            if (LanguageHelper.Language != "zh-CN")
            {
                copySceneNotice = LanguageHelper.TranslateWord(copySceneNotice);
                keepNotice = LanguageHelper.TranslateWord(keepNotice);
                insertNotice = LanguageHelper.TranslateWord(insertNotice);
                appendNotice = LanguageHelper.TranslateWord(appendNotice);
                deleteNotice = LanguageHelper.TranslateWord(deleteNotice);
                prevStepNotice = LanguageHelper.TranslateWord(prevStepNotice);
                nextStepNotice = LanguageHelper.TranslateWord(nextStepNotice);
            }

            // 几个按钮添加提示
            myToolTip.SetToolTip(protocolLabel, protocolNotice);
            myToolTip.SetToolTip(copySceneButton, copySceneNotice);
            myToolTip.SetToolTip(keepButton, keepNotice);
            myToolTip.SetToolTip(insertButton, insertNotice);
            myToolTip.SetToolTip(appendButton, appendNotice);
            myToolTip.SetToolTip(deleteButton, deleteNotice);
            myToolTip.SetToolTip(prevButton, prevStepNotice);
            myToolTip.SetToolTip(nextButton, nextStepNotice);

            // 各通道跳渐变下拉框显示数组的初始化			
            normalCMArray = new object[] {
                LanguageHelper.TranslateWord("跳变"),
                LanguageHelper.TranslateWord("渐变"),
                LanguageHelper.TranslateWord("屏蔽")  };
            soundCMArray = new object[] {
                LanguageHelper.TranslateWord("屏蔽"),
                LanguageHelper.TranslateWord("跳变") };

            // 定义标题栏文字+Icon
            string iconPath = Application.StartupPath + @"\favicon.ico";
            if (File.Exists(iconPath))
            {
                Icon = Icon.ExtractAssociatedIcon(iconPath);
            }

            // 211103 处理 协议列表 和 场景列表
            // 1.由各种配置文件，初始化三个列表（sceneCodeList、protocolList、AllSceneList)                  
            SceneCodeList = TextHelper.Read(Application.StartupPath + @"\Protocol\SceneCode");
            LoadProtocols();
            initSceneList();
            // 2.渲染协议列表：先读取相关的协议index，如果有错误，直接置为-1
            if (Properties.Settings.Default.protocolIndex >= ProtocolList.Count)
            {
                Properties.Settings.Default.protocolIndex = -1;
                Properties.Settings.Default.Save();
            }
            renderProtocolCB(Properties.Settings.Default.protocolIndex);
            // 3. 由最新的协议index，重新渲染 sceneList   
            protocolChanged(Properties.Settings.Default.protocolIndex, false);

            // 渲染通道组件( 包括子属性页等 )
            for (int tdIndex = 0; tdIndex < 32; tdIndex++)
            {
                tdPanels[tdIndex] = new Panel
                {
                    Name = "tdPanel" + (tdIndex + 1),
                    Location = tdPanelDemo.Location,
                    Size = tdPanelDemo.Size,
                    Visible = tdPanelDemo.Visible
                };

                tdNoLabels[tdIndex] = new Label
                {
                    Name = "tdNoLabel" + (tdIndex + 1),
                    Font = tdNoLabelDemo.Font,
                    ForeColor = tdNoLabelDemo.ForeColor,
                    AutoSize = tdNoLabelDemo.AutoSize,
                    Location = tdNoLabelDemo.Location,
                    Size = tdNoLabelDemo.Size,
                };

                tdNameLabels[tdIndex] = new Label
                {
                    Name = "tdNameLabel" + (tdIndex + 1),
                    Font = tdNameLabelDemo.Font,
                    ForeColor = tdNameLabelDemo.ForeColor,
                    Location = tdNameLabelDemo.Location,
                    Size = tdNameLabelDemo.Size,
                    TextAlign = tdNameLabelDemo.TextAlign
                };

                tdTrackBars[tdIndex] = new TrackBar
                {
                    Name = "tdTrackBar" + (tdIndex + 1),
                    AutoSize = tdTrackBarDemo.AutoSize,
                    BackColor = tdTrackBarDemo.BackColor,
                    Location = tdTrackBarDemo.Location,
                    Maximum = tdTrackBarDemo.Maximum,
                    Orientation = tdTrackBarDemo.Orientation,
                    Size = tdTrackBarDemo.Size,
                    TickFrequency = tdTrackBarDemo.TickFrequency,
                    TickStyle = tdTrackBarDemo.TickStyle
                };

                tdValueNumericUpDowns[tdIndex] = new NumericUpDown
                {
                    Name = "tdValueNUD" + (tdIndex + 1),
                    Font = tdValueNUDDemo.Font,
                    Location = tdValueNUDDemo.Location,
                    Maximum = tdValueNUDDemo.Maximum,
                    Size = tdValueNUDDemo.Size,
                    TextAlign = tdValueNUDDemo.TextAlign,
                    Tag = 0,
                };

                tdCmComboBoxes[tdIndex] = new ComboBox
                {
                    Name = "tdCmComboBox" + (tdIndex + 1),
                    FormattingEnabled = tdCmComboBoxDemo.FormattingEnabled,
                    Location = tdCmComboBoxDemo.Location,
                    Size = tdCmComboBoxDemo.Size,
                    DropDownStyle = tdCmComboBoxDemo.DropDownStyle,
                    Tag = 1,
                    Font = tdCmComboBoxDemo.Font
                };
                tdCmComboBoxes[tdIndex].Items.AddRange(new object[] {
                        LanguageHelper.TranslateWord("跳变"),
                        LanguageHelper.TranslateWord("渐变"),
                        LanguageHelper.TranslateWord("屏蔽")
                });

                tdStNumericUpDowns[tdIndex] = new NumericUpDown
                {
                    Name = "tdStNUD" + (tdIndex + 1),
                    Font = tdStNUDDemo.Font,
                    Location = tdStNUDDemo.Location,
                    Size = tdStNUDDemo.Size,
                    TextAlign = tdStNUDDemo.TextAlign,
                    DecimalPlaces = tdStNUDDemo.DecimalPlaces,
                    Tag = 2
                };

                tdPanels[tdIndex].Controls.Add(tdNameLabels[tdIndex]);
                tdPanels[tdIndex].Controls.Add(tdNoLabels[tdIndex]);
                tdPanels[tdIndex].Controls.Add(tdTrackBars[tdIndex]);
                tdPanels[tdIndex].Controls.Add(tdValueNumericUpDowns[tdIndex]);
                tdPanels[tdIndex].Controls.Add(tdCmComboBoxes[tdIndex]);
                tdPanels[tdIndex].Controls.Add(tdStNumericUpDowns[tdIndex]);

                tdTrackBars[tdIndex].MouseEnter += tdTrackBars_MouseEnter;
                tdTrackBars[tdIndex].MouseWheel += tdTrackBars_MouseWheel;
                tdTrackBars[tdIndex].ValueChanged += tdTrackBars_ValueChanged;

                tdValueNumericUpDowns[tdIndex].MouseEnter += tdValueNumericUpDowns_MouseEnter;
                tdValueNumericUpDowns[tdIndex].MouseWheel += tdValueNumericUpDowns_MouseWheel;
                tdValueNumericUpDowns[tdIndex].ValueChanged += tdValueNumericUpDowns_ValueChanged;
                tdValueNumericUpDowns[tdIndex].KeyPress += unifyTd_KeyPress;

                tdCmComboBoxes[tdIndex].SelectedIndexChanged += tdChangeModeSkinComboBoxes_SelectedIndexChanged;
                tdCmComboBoxes[tdIndex].KeyPress += unifyTd_KeyPress;

                tdStNumericUpDowns[tdIndex].MouseEnter += tdStepTimeNumericUpDowns_MouseEnter;
                tdStNumericUpDowns[tdIndex].MouseWheel += tdStepTimeNumericUpDowns_MouseWheel;
                tdStNumericUpDowns[tdIndex].ValueChanged += tdStepTimeNumericUpDowns_ValueChanged;
                tdStNumericUpDowns[tdIndex].KeyPress += unifyTd_KeyPress;

                tdNoLabels[tdIndex].Click += tdNameNumLabels_Click;
                tdNameLabels[tdIndex].Click += tdNameNumLabels_Click;

                tdFlowLayoutPanel.Controls.Add(tdPanels[tdIndex]);

                saPanels[tdIndex] = new Panel
                {
                    Name = "saPanel" + (tdIndex + 1),
                    Location = saPanelDemo.Location,
                    Size = saPanelDemo.Size,
                    Margin = saPanelDemo.Margin,
                    Visible = true,
                };
                tdFlowLayoutPanel.Controls.Add(saPanels[tdIndex]);
            }

            // 渲染lightsListView的灯具图片
            refreshLightImageList();

            //MARK：添加这一句，会去掉其他线程使用本UI控件时弹出异常的问题(权宜之计)。
            CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 辅助方法：在《在全局配置》中改变了时间因子并保存后，mainForm的时间因子变量也跟着改变，同时刷新当前步
        /// </summary>
        public void ChangeEachStepTime(int eachStepTime)
        {
            EachStepTime = eachStepTime / 1000m;
            refreshStep();
        }

        private void MainFormBase_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 辅助方法：传入变动列表，修改数据库及内存中的相关数据（新建工程和改动工程通用）；
        /// </summary>
        public void ReBuildLightList(List<LightsChange> changeList)
        {
            // 若未发生任何变化，则不再往下执行
            if (!LightsChange.IsChanged(changeList))
            {
                return;
            }

            // 只要灯具列表发生变化，很多相关的数据都可能发生变化，故直接重置这些可能发生变化的数据 
            disposeDmaForm();  // DetailMultiAstForm，用以记录之前用户选过的将进行多步联调的通道
            selectedIndex = -1;
            selectedIndexList = new List<int>();

            IList<int> retainList = new List<int>();
            // 新建时，需要初始化这两个List
            if (LightAstList == null)
            {
                LightAstList = new List<LightAst>();
                LightWrapperList = new List<LightWrapper>();
            }
            else
            {
                // 如果之前的灯具列表不为空，则可能存在旧的灯具项，此处先为retainList赋初值，再在后面的遍历中处理这些数据
                for (int lightIndex = 0; lightIndex < LightAstList.Count; lightIndex++)
                {
                    retainList.Add(lightIndex);
                }
            }

            // 遍历处理changeList （分为前后两种情况，但可以写在一起）
            for (int changeIndex = 0; changeIndex < changeList.Count; changeIndex++)
            {
                LightsChange change = changeList[changeIndex];
                // 前 LightAstList.Count项，只可能有DELETE和UPDATE两种情况
                // 删除：先把相关项置为null(最后统一处理，避免遍历过程中index发生变化)，并处理retainList
                if (change.Operation == EnumOperation.DELETE)
                {
                    int delLightId = LightAstList[changeIndex].StartNum;
                    LightAstList[changeIndex] = null;
                    LightWrapperList[changeIndex] = null;
                    channelDAO.DeleteByLightId(delLightId); // 删除数据库相关的数据
                                                            // 不能用RemoveAt（会出现不匹配index的情况） , 而应该用 Remove( retainList中 index 和 value刚好是对应的)
                                                            // 【List,Remove(item)：从 ICollection<T> 中移除特定对象的第一个匹配项。】
                    retainList.Remove(changeIndex);  // 删去的灯具index，在此处直接删掉，这样留下的 【值-键对】可以供新的GroupList使用
                }
                // 修改：相关项内的数据进行变动
                else if (change.Operation == EnumOperation.UPDATE)
                {
                    int editLightId = LightAstList[changeIndex].StartNum;
                    LightAstList[changeIndex].ChangeAddr(change.NewLightAst);
                    // 注意：更改Common项不要直接让StepCommon和TongdaoCommon=新值，这样会让子项和StepTemplate项的引用不再相同！正确的做法应该是改变Common内的属性；
                    LightWrapperList[changeIndex].StepTemplate.StepCommon.StartNum += change.AddNum;
                    for (int tdIndex = 0; tdIndex < LightWrapperList[changeIndex].StepTemplate.TongdaoList.Count; tdIndex++)
                    {
                        LightWrapperList[changeIndex].StepTemplate.TongdaoList[tdIndex].TongdaoCommon.Address += change.AddNum;
                    }
                    channelDAO.UpdateByLightId(editLightId, change.AddNum); // 更新表中此灯具的所有channel数据（注意sql语句中set的写法：改多个列时用逗号而非and）
                }
                // 后面剩下的项，只可能有ADD这种情况
                // 新增：只需新建一个LightAst、LightWrapper；
                if (change.Operation == EnumOperation.ADD)
                {
                    LightAstList.Add(change.NewLightAst);
                    LightWrapperList.Add(new LightWrapper() { StepTemplate = generateStepTemplate(change.NewLightAst) });
                }
            }
            // 最后把null值从各list中去掉，就是新的列表了
            ListHelper.RemoveNull(LightAstList);
            ListHelper.RemoveNull(LightWrapperList);

            // light、fineTune表，直接由当前的LightAst和LightWrapperList生成即可；channel表，则在删除和更新时，直接执行相关的操作
            lightDAO.SaveAll("Light", generateDBLightList());
            fineTuneDAO.SaveAll("FineTune", generateDBFineTuneList());

            // 处理编组列表
            IList<GroupAst> newGroupList = new List<GroupAst>();
            //取出每个编组，并分别进行处理
            foreach (GroupAst group in GroupList)
            {
                // 处理组员,直接用一个新的List来进行存储；
                IList<int> newIndexList = new List<int>();
                foreach (int oldIndex in group.LightIndexList)
                {
                    if (retainList.Contains(oldIndex))
                    { // 若retainList中有此项
                        newIndexList.Add(retainList.IndexOf(oldIndex));   // 则把该项的新索引添加进去
                    }
                }
                if (newIndexList.Count != 0) // 若组内成员已经为空，则此编组直接删掉(不添加到newGroupList中)
                {
                    // 处理组长 : 如果组长还在，则取出其新下标 ; 否则设为0                    
                    group.CaptainIndex = retainList.Contains(group.CaptainIndex) ? retainList.IndexOf(group.CaptainIndex) : 0;
                    group.LightIndexList = newIndexList;
                    newGroupList.Add(group);
                }
            }
            GroupList = newGroupList;
            saveAllGroups();

            //MARK 只开单场景：15.0 BuildLightList时，一定要清空selectedIndex及selectedIndices,否则若删除了该灯具，则一定会出问题！		
            enterSyncMode(false); // 修改了灯具后，一定要退出同步模式
            enableProjectRelative(true);    //ReBuildLightAst内设置
            enableRefreshPic(); //ReBuildLightList
            reBuildLightListView();
            refreshGroupPanels(); // ReBuildLightList() 

            //出现了个Bug：选中灯具后，在灯具列表内删除该灯具（或其他？），则内存内选中的灯和点击追加步之类的灯具可能会不同，故直接帮着选中第一个灯具好了
            if (LightAstList != null && LightAstList.Count > 0)
            {
                selectedIndex = 0;
            }
            generateLightData(); //ReBuildLightList         

        }

        /// <summary>
        /// 辅助方法：保存编组数据（保存工程 、保存场景、更改灯具列表后都需要执行）
        /// </summary>
        private void saveAllGroups()
        {
            try
            {
                GroupAst.SaveGroupIni(groupIniPath, GroupList);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存编组数据出错：\n" + ex.Message);
            }
        }

        #region 由界面数据生成dbXXList数据的方法（这些数据可能只在内存中，和数据库中的数据可能不同，除非保存过）

        /// <summary>
        /// 辅助方法：由lightAstList生成dbLightList（新）;
        /// </summary>
        private IList<DB_Light> generateDBLightList()
        {
            if (LightAstList == null || LightAstList.Count == 0)
            {
                return null;
            }
            IList<DB_Light> dbLightList = new List<DB_Light>();
            foreach (LightAst la in LightAstList)
            {
                dbLightList.Add(new DB_Light(la));
            }
            return dbLightList;
        }

        /// <summary>
        /// 辅助方法：由lightWrapperList生成dbFineTuneList（新）;
        /// </summary>
        private IList<DB_FineTune> generateDBFineTuneList()
        {
            if (LightAstList == null || LightAstList.Count == 0)
            {
                return null;
            }
            IList<DB_FineTune> dbFineTuneList = new List<DB_FineTune>();
            // 遍历lightWrapperList的模板数据，用以读取相关的通道名称，才能加以处理			
            foreach (LightWrapper lightWrapper in LightWrapperList)
            {
                StepWrapper stepTemplate = lightWrapper.StepTemplate;
                if (stepTemplate != null && stepTemplate.TongdaoList != null)
                {
                    int xz = 0, xzwt = 0, xzValue = 0, yz = 0, yzwt = 0, yzValue = 0;
                    foreach (TongdaoWrapper td in stepTemplate.TongdaoList)
                    {
                        switch (td.TongdaoCommon.TongdaoName.Trim())
                        {
                            case "X轴": xz = td.TongdaoCommon.Address; break;
                            case "X轴微调": xzwt = td.TongdaoCommon.Address; xzValue = td.ScrollValue; break;
                            case "Y轴": yz = td.TongdaoCommon.Address; break;
                            case "Y轴微调": yzwt = td.TongdaoCommon.Address; yzValue = td.ScrollValue; break;
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

            return dbFineTuneList;
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

        private void uiImageButton9_Click(object sender, EventArgs e)
        {
            (sender as UIImageButton).Selected = !(sender as UIImageButton).Selected;
        }

        #endregion

        #region 工程相关

        /// <summary>
        /// 事件：点击《新建工程》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newButton_Click(object sender, EventArgs e)
        {
            new NewForm(this).ShowDialog();
        }

        /// <summary>
        /// 事件：点击《打开工程》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openButton_Click(object sender, EventArgs e)
        {
            new OpenForm(this).ShowDialog();
        }

        /// <summary>
        /// 事件：点击《保存工程》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e) { }

        /// <summary>
        /// 事件：点击《保存工程》；根据点击按键的不同，采用不同的处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                saveProjectClick();
            }
            else if (e.Button == MouseButtons.Right)
            {
                exportSourceClick();
            }
        }

        /// <summary>
        /// 辅助方法：保存工程
        /// </summary>
        private void saveProjectClick()
        {
            SetNotice("正在保存工程,请稍候...", false, true);
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
                saveAllChannels();
            }
            saveAllGroups();  // 无论如何，都保存编组列表

            DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforeDT);

            setBusy(false);
            SetNotice(LanguageHelper.TranslateSentence("成功保存工程:")
                + currentProjectName
                + ",耗时: " + ts.TotalSeconds.ToString("#0.00") + " s"
                , true, false);
        }

        /// <summary>
        ///  辅助方法：保存工程源文件（Source.zip）到指定目录
        /// </summary>
        protected void exportSourceClick()
        {
            DialogResult dr = exportSourceBrowserDialog.ShowDialog();
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            string exportPath = exportSourceBrowserDialog.SelectedPath;
            string zipPath = exportPath + @"\Source.zip";
            if (File.Exists(zipPath))
            {
                dr = MessageBox.Show(
                    LanguageHelper.TranslateSentence("检测到该目录已存在一个Source.zip文件，是否覆盖？"),
                    LanguageHelper.TranslateSentence("覆盖文件？"),
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
            }

            setBusy(true);
            SetNotice("正在压缩源文件,请稍候...", false, true);

            if (GenerateSourceZip(zipPath))
            {
                dr = MessageBox.Show(
                        LanguageHelper.TranslateSentence("成功导出当前工程的源文件,是否打开导出文件夹?"),
                        LanguageHelper.TranslateSentence("打开导出文件夹？"),
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    System.Diagnostics.Process.Start(exportPath);
                }
                SetNotice("已成功压缩源文件(Source.zip)。", false, true);
            }
            else
            {
                SetNotice("导出工程源文件失败。", false, true);
            }
            setBusy(false);
        }

        /// <summary>
        /// 辅助方法：导出当前工程的源文件( 工程+灯具+相关Pic)，并压缩为Source.zip；
        /// </summary>
        public bool GenerateSourceZip(string zipPath)
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
                DirectoryHelper.CopyDirectory(currentProjectPath, destPath);

                if (LightAstList != null && LightAstList.Count > 0)
                {
                    string lightLibPath = SavePath + @"\Source\LightLibrary";
                    di = new DirectoryInfo(lightLibPath);
                    di.Create();

                    HashSet<string> lightSet = new HashSet<string>();
                    HashSet<string> dirSet = new HashSet<string>();
                    HashSet<string> picSet = new HashSet<string>();
                    foreach (LightAst la in LightAstList)
                    {
                        dirSet.Add(la.LightName);
                        if (!string.IsNullOrEmpty(la.LightPic))
                        {
                            picSet.Add(la.LightPic);
                        }
                        lightSet.Add(la.LightName + "\\" + la.LightType + ".ini");
                    }

                    foreach (string libDir in dirSet)
                    {
                        di = new DirectoryInfo(SavePath + @"\Source\LightLibrary\" + libDir);
                        di.Create();
                    }
                    foreach (string lightPath in lightSet)
                    {
                        File.Copy(SavePath + @"\LightLibrary\" + lightPath, SavePath + @"\Source\LightLibrary\" + lightPath, true);
                    }
                    // 把灯具图片也保存起来：但如果图片不存在，则跳过不理，避免触发异常（否则整个流程都直接跳出，Source.zip不会生成）
                    di = new DirectoryInfo(SavePath + @"\Source\LightPic");
                    di.Create();
                    foreach (string lightPic in picSet)
                    {
                        string sourcePicPath = SavePath + @"\LightPic\" + lightPic;
                        if (File.Exists(sourcePicPath))
                        {
                            File.Copy(sourcePicPath, SavePath + @"\Source\LightPic\" + lightPic, true);
                        }
                        else
                        {
                            Console.WriteLine("灯具图片(" + sourcePicPath + ")不存在...");
                        }
                    }
                    // 压缩文件直接继承到同一个方法中，成功后把Source工作目录直接删掉；
                    ZipHelper.CompressAllToZip(SavePath + @"\Source", zipPath, 9, null, SavePath + @"\");
                    di = new DirectoryInfo(SavePath + @"\Source");
                    di.Delete(true);
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
        /// 辅助方法：从界面数据实时生成dbFineTuneList，保存到db中
        /// </summary>
        private void saveAllFineTunes()
        {
            if (fineTuneDAO == null)
            {
                fineTuneDAO = new FineTuneDAO(dbFilePath, isEncrypt);
            }
            // 保存数据
            fineTuneDAO.SaveAll("FineTune", generateDBFineTuneList());
        }

        /// <summary>
        /// 辅助方法：从界面数据实时生成dbLightList，保存到db中
        /// </summary>
        private void saveAllLights()
        {
            if (lightDAO == null)
            {
                lightDAO = new LightDAO(dbFilePath, isEncrypt);
            }
            // 将传送所有的DB_Light给DAO,让它进行数据的保存
            lightDAO.SaveAll("Light", generateDBLightList());
        }

        /// <summary>
        /// 辅助方法：保存所有场景的channelList
        /// </summary>
        private void saveAllChannels()
        {
            for (int sceneIndex = 0; sceneIndex < SceneCount; sceneIndex++)
            {
                if (sceneSaveArray[sceneIndex])
                {
                    saveSceneChannels(sceneIndex);
                    if (sceneIndex != CurrentScene)
                    {
                        sceneSaveArray[sceneIndex] = false;
                    }
                }
            }
        }
        /// <summary>
        /// 辅助方法：保存指定场景的channelList
        /// </summary>
        /// <param name="scene">要保存的场景编号，由0开始</param>
        private void saveSceneChannels(int scene)
        {
            if (channelDAO == null)
            {
                channelDAO = new ChannelDAO(dbFilePath, isEncrypt);
            }

            Dictionary<DB_ChannelPK, StringBuilder> channelDict = new Dictionary<DB_ChannelPK, StringBuilder>();

            for (int lightIndex = 0; lightIndex < LightAstList.Count; lightIndex++)
            {
                LightAst la = LightAstList[lightIndex];
                LightStepWrapper[,] allLightStepWrappers = LightWrapperList[lightIndex].LightStepWrapperList;

                //10.17 取出灯具的当前场景（两种模式都要），并将它们保存起来（但若为空，则不保存）
                for (int mode = 0; mode < 2; mode++)
                {
                    LightStepWrapper lswTemp = allLightStepWrappers[scene, mode];

                    //只有不为null且步数>0，才可能有需要保存的数据
                    if (lswTemp != null && lswTemp.TotalStep > 0)
                    {
                        IList<StepWrapper> stepWrapperList = lswTemp.StepWrapperList;

                        for (int stepIndex = 0; stepIndex < stepWrapperList.Count; stepIndex++)
                        {
                            StepWrapper step = stepWrapperList[stepIndex];

                            for (int tongdaoIndex = 0; tongdaoIndex < step.TongdaoList.Count; tongdaoIndex++)
                            {
                                TongdaoWrapper tongdao = step.TongdaoList[tongdaoIndex];

                                DB_ChannelPK pk = new DB_ChannelPK()
                                {
                                    Scene = scene,
                                    Mode = mode,
                                    LightID = la.StartNum,
                                    ChannelID = la.StartNum + tongdaoIndex
                                };

                                string addStr = tongdao.ChangeMode + "-" + tongdao.ScrollValue + "-" + tongdao.StepTime + ",";
                                if (channelDict.ContainsKey(pk))
                                {
                                    channelDict[pk].Append(addStr);
                                }
                                else
                                {
                                    channelDict.Add(pk, new StringBuilder(addStr));
                                }
                            }
                        }
                    }
                }
            }
            channelDAO.SaveSceneChannels(scene, channelDict);
        }

        /// <summary>
        ///  辅助方法: 通过工程名，新建工程; 主要通过调用InitProject(),但在前后加了鼠标特效的处理。（此操作可能耗时较久，故在方法体前后添加鼠标样式的变化）
        /// </summary>
        /// <param name="projectName">工程名</param>
        public void NewProject(string projectName, int sceneIndex)
        {
            Cursor = Cursors.WaitCursor;
            initProject(projectName, sceneIndex, true);
            SetNotice("成功新建工程，请为此工程添加灯具。", true, true);
            Cursor = Cursors.Default;
            editLightList();  //新建工程后，灯具列表一定是空的，故直接弹出《添加灯具》
        }

        /// <summary>
        /// 辅助方法：打开《灯具列表》
        /// </summary>
        protected void editLightList()
        {
            new LightsForm(this).ShowDialog();
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
        public void OpenProject(string savePath, string projectName, int sceneIndex)
        {
            string projDir = savePath + @"\LightProject\" + projectName + @"\";
            if (!File.Exists(projDir + "newData.db3") && File.Exists(projDir + "data.db3"))
            {
                if (DialogResult.No == MessageBox.Show(
                    "您选中的是旧版工程，需进行转化后才可打开。\n是否立刻进行转化（新工程格式将显著提高存取效率）?",
                    "转化为新版工程？",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning))
                {
                    return;
                }
                changeToNewDB(projDir);
            }

            SetNotice("正在打开工程，请稍候...", false, true);
            setBusy(true);

            DateTime beforeDT = System.DateTime.Now;

            //若更改过工作目录，需要刷新软件内置的灯具图片列表；
            if (savePath != SavePath)
            {
                SavePath = savePath;
                refreshLightImageList(); // OpenProject
            }

            // 初始化
            initProject(projectName, sceneIndex, false);

            // 设置listView右键菜单中读取位置配置的可用项	
            autoEnableSLArrange(); //OpenProject

            // 把各数据库表的内容填充进来。
            IList<DB_Light> dbLightList = getDBLightList();

            //如果是空工程(无灯具)，后面的数据无需读取。
            if (dbLightList == null || dbLightList.Count == 0)
            {
                SetNotice(LanguageHelper.TranslateSentence("成功打开空工程：") + projectName, false, false);
                if (DialogResult.OK == MessageBox.Show(
                    LanguageHelper.TranslateSentence("成功打开空工程 , 要为此工程添加灯具吗？"),
                    LanguageHelper.TranslateSentence("为空工程添加灯具"),
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question))
                {
                    new LightsForm(this).ShowDialog();
                }
            }
            //10.17 若非空工程，则继续执行以下代码。
            else
            {
                LightAstList = new List<LightAst>();
                LightWrapperList = new List<LightWrapper>();

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
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("加载工程时发生异常，可能是部分灯库文件已丢失。\n(" + ex.Message + ")");
                    clearAllData();
                    setBusy(false);
                    return;
                }

                enterSyncMode(false); //OpenProject() 需要退出同步模式
                enableProjectRelative(true);    //OpenProject内设置
                enableRefreshPic();  //OpenProject
                reBuildLightListView();  // OpenProject

                // 只加载初始场景    
                generateSceneData(CurrentScene);

                DateTime afterDT = System.DateTime.Now;
                TimeSpan ts = afterDT.Subtract(beforeDT);

                SetNotice(LanguageHelper.TranslateSentence("成功打开工程：")
                    + "【"
                    + projectName + "】"
                    + "，耗时: " + ts.TotalSeconds.ToString("#0.00") + " s" + "。"
                     , true, false);
            }
            setBusy(false);

            // 打开工程后，主动帮用户选择第一个灯具
            if (LightAstList != null && LightAstList.Count > 0)
            {
                selectedIndex = 0;
            }
            generateLightData(); //OpenProject
        }

        /// <summary>
        ///211012 辅助方法：打开工程单场景(两个模式)数据 
        /// </summary>
        /// <param name="scene">场景编号，由0开始计数</param>
        private void generateSceneData(int scene)
        {
            //MARK 重构BuildLightList：generateFrameData()内加dbLightList空值验证
            IList<DB_Light> dbLightList = getDBLightList();
            if (dbLightList == null || dbLightList.Count == 0)
            {
                return;
            }

            if (channelDAO == null)
            {
                channelDAO = new ChannelDAO(dbFilePath, false);
            }

            // 采用多线程方法优化(每个灯开启一个线程)
            Thread[] threadArray = new Thread[dbLightList.Count];
            for (int lightListIndex = 0; lightListIndex < dbLightList.Count; lightListIndex++)
            {
                int tempLightIndex = lightListIndex; // 必须在循环内使用一个临时变量来记录这个index，否则线程运行时lightListIndex会发生变化。
                int tempLightID = dbLightList[tempLightIndex].LightID;   //记录了数据库中灯具的起始地址（不同灯具有1-32个通道，但只要是同个灯，就公用此LightNo)				

                threadArray[tempLightIndex] = new Thread(delegate ()
                {
                    for (int mode = 0; mode < 2; mode++)
                    {
                        LightWrapperList[tempLightIndex].LightStepWrapperList[scene, mode] = new LightStepWrapper();

                        IList<DB_Channel> channelList = channelDAO.GetList(tempLightID, scene, mode);

                        //当找到的stepValueListTemp ①不为空；②通道数量与模板相同 时，才继续往下走，否则不继续运行
                        if (channelList != null && channelList.Count == LightWrapperList[tempLightIndex].StepTemplate.TongdaoList.Count)
                        {
                            StepWrapper.GenerateStepWrapperList(
                                LightWrapperList[tempLightIndex].LightStepWrapperList[scene, mode],
                                LightWrapperList[tempLightIndex].StepTemplate,
                                channelList,
                                mode);
                        }
                    }
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
                    break;
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
            //MARK 只开单场景：07.4 generateFrameData(int)内:从DB生成FrameData后，设frameLoadArray[selectedFrameIndex]=true
            sceneLoadArray[scene] = true;
        }

        #region 由数据库取(get)数据生成dbXXList的几个方法

        protected IList<DB_Light> getDBLightList()
        {
            if (lightDAO == null)
            {
                lightDAO = new LightDAO(dbFilePath, isEncrypt);
            }
            return lightDAO.GetAll();
        }

        protected IList<DB_FineTune> getDBFineTuneList()
        {

            if (fineTuneDAO == null)
            {
                fineTuneDAO = new FineTuneDAO(dbFilePath, isEncrypt);
            }
            return fineTuneDAO.GetAll();
        }

        #endregion

        /// <summary>
        ///  211012 辅助方法：旧版数据库转为新版格式
        /// </summary>
        /// <param name="projDir">工程目录（最后一个字符为”/“无需额外添加）</param>
        private void changeToNewDB(string projDir)
        {
            DateTime beforeDT1 = System.DateTime.Now;

            OldDAO oldDAO = new OldDAO(projDir + "data.db3", false);
            dbFilePath = projDir + "newData.db3";
            lightDAO = new LightDAO(dbFilePath, isEncrypt);
            fineTuneDAO = new FineTuneDAO(dbFilePath, isEncrypt);
            channelDAO = new ChannelDAO(dbFilePath, isEncrypt);

            IList<object[]> oldFineTuneList = oldDAO.GetListBySQL("select * from FineTune");
            List<DB_FineTune> fineTuneList = new List<DB_FineTune>();
            foreach (object[] oldFT in oldFineTuneList)
            {
                fineTuneList.Add(new DB_FineTune()
                {
                    MainIndex = (int)oldFT[0],
                    FineTuneIndex = (int)oldFT[1],
                    MaxValue = (int)oldFT[2]
                });
            }
            fineTuneDAO.SaveAll("FineTune", fineTuneList);

            IList<object[]> oldLightList = oldDAO.GetListBySQL("select LightNo,name,type,pic,count,remark from Light");
            List<DB_Light> lightList = new List<DB_Light>();
            foreach (object[] oldLight in oldLightList)
            {
                lightList.Add(new DB_Light()
                {
                    LightID = (int)oldLight[0],
                    Name = (string)oldLight[1],
                    Type = (string)oldLight[2],
                    Pic = (string)oldLight[3],
                    Count = (int)oldLight[4],
                    Remark = (string)oldLight[5]
                });
            }
            lightDAO.SaveAll("Light", lightList);

            IList<object[]> valueList = oldDAO.GetListBySQL("SELECT  " +
                "lightIndex, lightId, frame, mode,GROUP_CONCAT(changeMode || '-' || scrollValue || '-' || stepTime) as newValue " +
                "FROM value " +
                "group by lightId, frame, mode");
            List<DB_Channel> channelList = new List<DB_Channel>();

            foreach (object[] oldValue in valueList)
            {
                channelList.Add(new DB_Channel()
                {
                    PK = new DB_ChannelPK()
                    {
                        LightID = (int)oldValue[0],
                        ChannelID = (int)oldValue[1],
                        Scene = (int)oldValue[2],
                        Mode = (int)oldValue[3],
                    },
                    Value = (string)oldValue[4],
                });
            }
            channelDAO.SaveAll("Channel", channelList);

            DateTime afterDT1 = System.DateTime.Now;
            TimeSpan ts = afterDT1.Subtract(beforeDT1);

            MessageBox.Show("成功转化为新版工程，耗时: " + ts.TotalSeconds.ToString("#0.00") + " s" + "。");
        }

        /// <summary>
        /// 辅助方法：刷新灯具图片列表(lightImageList)的方法
        /// </summary>
        private void refreshLightImageList()
        {
            lightImageList.Images.Clear();// 清空当前图片
            lightImageList.Images.Add("灯光图.png", global::LightController.Properties.Resources.灯光图);// 添加默认图片
            DirectoryInfo di = new DirectoryInfo(SavePath + @"\LightPic");
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

        /// <summary>
        /// 基类辅助方法InitProject(打开或新建工程会用到)：①ClearAllData()；②设置内部的一些工程路径及变量；③初始化数据库
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="isNew"></param>
        private void initProject(string projectName, int selectedSceneIndex, bool isNew)
        {
            //0.清空所有内存数据及重置控件情况
            clearAllData();

            // 1.全局设置
            currentProjectName = projectName;
            currentProjectPath = SavePath + @"\LightProject\" + projectName;
            GlobalIniPath = currentProjectPath + @"\global.ini";
            projectStr = "(" + LanguageHelper.TranslateSentence(" 当前工程：") + projectName + " )";
            Text = SoftwareName + projectStr + ConnectStr;

            //1.1设置当前工程的 arrange.ini 的地址,以及先把各种可用性屏蔽掉
            arrangeIniPath = currentProjectPath + @"\arrange.ini";

            //1.2 读取时间因子
            IniHelper iniAst = new IniHelper(GlobalIniPath);
            EachStepTime = iniAst.ReadInt("Set", "EachStepTime", 30) / 1000m;
            initStNumericUpDowns();  // InitProject : 更改了时间因子后，需要处理相关的stepTimeNumericUpDown，包括tdPanel内的及unifyPanel内的

            // 1.3 加载groupList : 初始化时检查文件是否存在，不存在，则直接把默认文件拷贝过去；加载到内存后，通过相应的groupList刷新按钮
            groupIniPath = currentProjectPath + @"\groupList.ini";
            if (!File.Exists(groupIniPath))
            {
                File.Copy(Application.StartupPath + @"\Default\groupList.ini", groupIniPath);
            }
            GroupList = GroupAst.GenerateGroupList(groupIniPath);
            refreshGroupPanels(); //initProject()

            // 2.创建数据库:初始化，让所有的DAO指向new xxDAO，避免连接到错误的数据库(已打开过旧的工程的情况下)；			
            dbFilePath = currentProjectPath + @"\newData.db3";
            lightDAO = new LightDAO(dbFilePath, isEncrypt);
            fineTuneDAO = new FineTuneDAO(dbFilePath, isEncrypt);
            channelDAO = new ChannelDAO(dbFilePath, isEncrypt);

            // 若为新建，则初始化db的table(随机使用一个DAO即可初始化）
            if (isNew)
            {
                BaseDAO<object>.CreateSchema(dbFilePath, false);
            }

            // ①修改当前场景；
            // ②初始化sceneSaveArray、sceneLoadArray，除初始场景（selectedSceneIndex）外，其余场景都为false
            //   -->当前打开的场景：因为数据可能发生更改，设为true表示需要保存；
            //   -->当前打开的场景：sceneLoadArray表示已经加载过了，之后如需导出工程等操作，可从内存中直接读取。
            changeCurrentScene(selectedSceneIndex);
            sceneSaveArray = new bool[SceneCount];
            sceneLoadArray = new bool[SceneCount];
            for (int sceneIndex = 0; sceneIndex < SceneCount; sceneIndex++)
            {
                sceneSaveArray[sceneIndex] = sceneIndex == selectedSceneIndex;
                sceneLoadArray[sceneIndex] = sceneIndex == selectedSceneIndex;
            }

            enableProjectRelative(true);    // initProject()时设置，各按键是否可用
        }

        /// <summary>
        /// 辅助方法： 清空相关的所有数据（关闭工程、新建工程、打开工程都会用到）
        /// -- 子类中需有针对该子类内部自己的部分代码（如重置listView或禁用stepPanel等）
        /// </summary>
        protected void clearAllData()
        {
            currentProjectName = null;
            currentProjectPath = null;
            GlobalIniPath = null;
            GroupList = null;

            LightAstList = null;
            LightWrapperList = null;

            selectedIndex = -1;
            selectedIndexList = new List<int>();

            disposeDmaForm();

            tempStep = null;
            TempMaterialAst = null;

            arrangeIniPath = null;
            groupIniPath = null;

            //MARK 只开单场景：03.0 clearAllData()内清空frameSaveArray、frameLoadArray
            sceneSaveArray = null;
            sceneLoadArray = null;

            projectStr = "";
            Text = SoftwareName + projectStr + ConnectStr;

            enterSyncMode(false);  //退出《同步模式》
            EnterMultiMode(null, false); // clearAllData
            autoEnableSLArrange();  // clearAllData
            enableProjectRelative(false);  // clearAllData()内：工程相关的所有按钮，设为不可用
            enableRefreshPic();  //clearAllData()
            refreshGroupPanels(); //clearAllData()

            showStepLabelMore(0, 0); //clearAllData
            showTDPanels(null); // clearAllData
            showLightsInfo(); //clearAllData

            lightsListView.Clear();
            stepPanel.Enabled = false;
        }

        //辅助方法：摧毁DmaForm，同时也将TdDict置为null（供ClearAllData和RebuildLightList调用）
        private void disposeDmaForm()
        {
            //DOTO disposeDmaForm
            //if (DmaForm != null)
            //{
            //	DmaForm.Dispose();
            //	DmaForm = null;
            //	TdDict = null;
            //}
        }

        /// <summary>
        /// 辅助方法：更改32个通道步时间的显示
        /// </summary>
        private void initStNumericUpDowns()
        {
            for (int i = 0; i < 32; i++)
            {
                tdStNumericUpDowns[i].Maximum = EachStepTime * MAX_StTimes;
                tdStNumericUpDowns[i].Increment = EachStepTime;
            }
        }

        private void changeCurrentScene(int sceneIndex)
        {
            CurrentScene = sceneIndex;
            sceneComboBox.SelectedIndexChanged -= sceneComboBox_SelectedIndexChanged;
            sceneComboBox.SelectedIndex = CurrentScene;
            sceneComboBox.SelectedIndexChanged += sceneComboBox_SelectedIndexChanged;
        }

        /// <summary>
        /// 辅助方法：设置是否 同步模式
        /// </summary>
        /// <param name="isSyncMode">进入同步</param>
        private void enterSyncMode(bool isSyncMode)
        {
            this.isSyncMode = isSyncMode;
            syncButton.Text = isSyncMode ? "退出同步" : "进入同步";
            SetNotice(isSyncMode ? "已进入同步模式" : "已退出同步模式", false, true);
        }

        /// <summary>
        /// 辅助方法：进入编组
        ///		1.取出选中的组长，
        ///		2.使用组长数据，替代其他灯具（在该F/M）的所有步数集合。
        /// </summary>
        /// <param name="captainIndex"></param>
        public void EnterMultiMode(GroupAst group, bool isCopyAll)
        {
            if (group == null)
            {
                selectedIndex = -1;
                selectedIndexList = new List<int>();
            }
            else
            {
                selectedIndexList = new List<int>(group.LightIndexList);
                selectedIndex = selectedIndexList[group.CaptainIndex];
            }
            isMultiMode = selectedIndexList.Count > 1;
            if (isMultiMode && isCopyAll)
            {
                LightStepWrapper captainLSWrapper = getSelectedLightStepWrapper(selectedIndex); //取出组长
                foreach (int listIndex in selectedIndexList)
                {
                    if (listIndex != selectedIndex)
                    {
                        //通过组长生成相关的数据,组长自身无需复制(210715优化)；
                        StepWrapper currentStepTemplate = LightWrapperList[listIndex].StepTemplate;
                        LightWrapperList[listIndex].LightStepWrapperList[CurrentScene, CurrentMode] = LightStepWrapper.GenerateLightStepWrapper(captainLSWrapper, currentStepTemplate);
                    }
                }
            }

            showLightsInfo(); //EnterMultiMode
            refreshMultiModeControls(); //EnterMultiMode()
            refreshStep(); //最后刷新步：此处代码用到了模板方法...
        }

        /// <summary>
        ///  辅助方法：《保存|读取灯具位置》按钮是否可用
        /// </summary>
        /// <param name="enable"></param>
        private void autoEnableSLArrange()
        {
            //DOTO autoEnableSLArrange
            //saveArrangeToolStripMenuItem.Enabled = !isAutoArrange;
            //loadArrangeToolStripMenuItem.Enabled = File.Exists(arrangeIniPath);
        }

        /// <summary>
        /// 辅助方法：使能《是否已加载工程》相关的各种按钮及控件
        /// </summary>
        /// <param name="enable">表示按键可以使用</param>
        private void enableProjectRelative(bool enable)
        {
            //常规的四个按钮（保存、关闭、导出都需要当前存在场景）
            saveButton.Enabled = enable;
            saveSceneButton.Enabled = enable;
            exportButton.Enabled = enable && LightAstList != null && LightAstList.Count > 0;
            closeButton.Enabled = enable;

            // 其它相关按键
            copySceneButton.Enabled = enable && LightAstList != null && LightAstList.Count > 0;
            lightListButton.Enabled = enable;
            globalSetButton.Enabled = enable;
        }

        /// <summary>
        ///  辅助方法：使能 《 刷新灯具图片》
        /// </summary>
        protected void enableRefreshPic()
        {
            refreshPicToolStripMenuItem.Enabled = LightAstList != null && LightAstList.Count > 0;
        }

        /// <summary>
        /// MARK 重构BuildLightList：reBuildLightListView() in NewMainForm
        ///辅助方法：根据现有的lightAstList，重新渲染listView
        /// </summary>
        private void reBuildLightListView()
        {
            //listView用BeginUpdate和EndUpdate [能有效的节省一些资源，不用每加一个灯具就重绘一次]
            lightsListView.BeginUpdate();

            lightsListView.Items.Clear();
            for (int i = 0; i < LightAstList.Count; i++)
            {
                // 添加灯具数据到LightsListView中
                lightsListView.Items.Add(new ListViewItem(
                    LightAstList[i].LightType + "\n" +
                        "(" + LightAstList[i].LightAddr + ")\n" +
                        LightAstList[i].Remark,
                    lightImageList.Images.ContainsKey(LightAstList[i].LightPic) ? LightAstList[i].LightPic : "灯光图.png"
                )
                { Tag = LightAstList[i].LightName + ":" + LightAstList[i].LightType });
            }

            lightsListView.EndUpdate();
        }

        /// <summary>
        /// 辅助方法：生成编组按钮组（先清空，再由groupList直接生成新的按钮组）
        /// </summary>
        private void refreshGroupPanels()
        {
            groupFlowLayoutPanel.Controls.Clear();
            groupToolTip.RemoveAll();
            if (GroupList != null && GroupList.Count > 0)
            {
                for (int groupIndex = 0; groupIndex < GroupList.Count; groupIndex++)
                {
                    addGroupPanel(groupIndex, GroupList[groupIndex]);
                }
            }
        }

        /// <summary>
        ///辅助方法：添加编组按钮（一个编组一个Panel，包含两个按钮：使用编组 和 删除编组）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addGroupPanel(int groupIndex, GroupAst ga)
        {
            Panel panel = new Panel();
            Button inButton = new Button();
            Button delButton = new Button();

            panel.Controls.Add(inButton);
            panel.Controls.Add(delButton);
            panel.Location = new System.Drawing.Point(0, 0);
            panel.Name = "groupPanel";
            panel.Size = new System.Drawing.Size(140, 26);
            panel.TabIndex = 56;
            panel.Padding = new Padding(0);
            //panel.BorderStyle = BorderStyle.FixedSingle;

            inButton.BackColor = System.Drawing.Color.White;
            inButton.Enabled = true;
            inButton.Location = new System.Drawing.Point(0, 0);
            inButton.Margin = new System.Windows.Forms.Padding(0);
            inButton.Name = "groupInButton";
            inButton.Size = new System.Drawing.Size(114, 26);
            inButton.TabIndex = 55;
            inButton.Text = ga.GroupName;
            inButton.Tag = groupIndex;
            inButton.UseVisualStyleBackColor = true;
            inButton.Click += new EventHandler(groupInButton_Click);

            delButton.BackColor = System.Drawing.Color.White;
            delButton.Enabled = true;
            delButton.Location = new System.Drawing.Point(118, 0);
            delButton.Margin = new System.Windows.Forms.Padding(0);
            delButton.Name = "groupDelButton";
            delButton.Size = new System.Drawing.Size(20, 26);
            delButton.TabIndex = 55;
            delButton.Text = "-";
            delButton.Tag = groupIndex;
            delButton.UseVisualStyleBackColor = true;
            delButton.Click += new EventHandler(groupDelButton_Click);

            groupFlowLayoutPanel.Controls.Add(panel);
            groupToolTip.SetToolTip(inButton, ga.GroupName + "\n" + StringHelper.MakeIntListToString(ga.LightIndexList, 1, ga.CaptainIndex));
        }

        /// <summary>
        /// 事件：点击《编组名（进入编组）》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupInButton_Click(object sender, EventArgs e)
        {
            if (GroupList == null || GroupList.Count == 0)
            {
                SetNotice("当前工程groupList为空，无法使用编组。", true, true);
                return;
            }
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
            if (groupIndex >= GroupList.Count)
            {
                SetNotice("groupIndex大于groupList的大小，无法使用编组。", true, true);
                return;
            }
            GroupAst group = GroupList[groupIndex];
            if (group.LightIndexList == null || group.LightIndexList.Count < 1)
            {
                SetNotice("选中编组的组员数量小于1，无法使用编组。", true, true);
                return;
            }
            if (!checkIndexAllInLightList(group.LightIndexList))
            {
                SetNotice("编组内的部分灯具索引超过了当前工程的灯具数量，无法使用编组。", true, true);
                return;
            }
            if (!checkSameLightsAndSteps(group.LightIndexList))
            {
                SetNotice("编组内的灯具并非同一类型或步数不一致，无法使用编组。", true, true);
                return;
            }

            EnterMultiMode(group, false);
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
            int firstStepCount = getSelectedLightTotalStep(firstIndex);

            for (int i = 1; i < lightIndexList.Count; i++) // 从第二个选中灯具开始比对
            {
                int tempIndex = lightIndexList[i];
                string tempName = LightAstList[tempIndex].LightName + "：" + LightAstList[tempIndex].LightType;
                int tempStepCount = getSelectedLightTotalStep(tempIndex);

                if (!firstName.Equals(tempName) || firstStepCount != tempStepCount)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 辅助方法：点击《-（删除编组）》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupDelButton_Click(object sender, EventArgs e)
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
        /// 辅助方法：显示步数标签，并判断stepPanel按钮组是否可用
        /// </summary>		
        protected void showStepLabelMore(int currentStep, int totalStep)
        {
            // 1. 设label的Text值					   
            stepLabel.Text = MathHelper.GetFourWidthNumStr(currentStep, true) + "/" + MathHelper.GetFourWidthNumStr(totalStep, false);

            // 2.1 设定《删除步》按钮是否可用
            deleteButton.Enabled = totalStep != 0;

            // 2.2 设定《追加步》、《前插入步》《后插入步》按钮是否可用			
            bool insertEnabled = totalStep < MAX_STEP;
            appendButton.Enabled = insertEnabled;
            insertButton.Enabled = insertEnabled;

            // 2.3 设定《上一步》《下一步》是否可用			
            prevButton.Enabled = totalStep > 1;
            nextButton.Enabled = totalStep > 1;

            //3 设定《复制|粘贴步、保存素材》、《多步复用》等是否可用
            copyButton.Enabled = currentStep > 0;
            pasteButton.Enabled = currentStep > 0 && tempStep != null;
            saveMatButton.Enabled = currentStep > 0;

            // 4.设定统一调整区是否可用
            groupButton.Enabled = (LightAstList != null && lightsListView.SelectedIndices.Count > 0) || isMultiMode; // 只有工程非空（有灯具列表）且选择项不为空才可点击
            groupFlowLayoutPanel.Enabled = LightAstList != null;
            unifyButton.Enabled = totalStep != 0;
            multiButton.Enabled = totalStep != 0;
            multiplexButton.Enabled = currentStep > 0;
            soundListButton.Enabled = !string.IsNullOrEmpty(currentProjectName) && CurrentMode == 1;

            // 5.处理选择步数的框及按钮
            chooseStepNumericUpDown.Enabled = totalStep != 0;
            chooseStepNumericUpDown.Minimum = totalStep != 0 ? 1 : 0;
            chooseStepNumericUpDown.Maximum = totalStep;
            chooseStepButton.Enabled = totalStep != 0;

        }

        /// <summary>
        /// 辅助方法：通过传来的数值，生成通道列表的数据
        /// </summary>
        /// <param name="tongdaoList"></param>
        /// <param name="startNum"></param>
        private void showTDPanels(IList<TongdaoWrapper> tongdaoList)
        {            
            // 1.判断tongdaoList，为null或数量为0时：①隐藏所有通道；②退出此方法
            if (tongdaoList == null || tongdaoList.Count == 0)
            {
                for (int tdIndex = 0; tdIndex < 32; tdIndex++)
                {
                    tdPanels[tdIndex].Hide();
                    saPanels[tdIndex].Hide();
                }
                labelPanel.Hide();
            }
            //2.将dataWrappers的内容渲染到起VScrollBar中
            else
            {
                labelPanel.Show();
                for (int tdIndex = 0; tdIndex < tongdaoList.Count; tdIndex++)
                {
                    tdTrackBars[tdIndex].ValueChanged -= tdTrackBars_ValueChanged;
                    tdValueNumericUpDowns[tdIndex].ValueChanged -= tdValueNumericUpDowns_ValueChanged;
                    tdCmComboBoxes[tdIndex].SelectedIndexChanged -= tdChangeModeSkinComboBoxes_SelectedIndexChanged;
                    tdStNumericUpDowns[tdIndex].ValueChanged -= tdStepTimeNumericUpDowns_ValueChanged;

                    tdNoLabels[tdIndex].Text = LanguageHelper.TranslateWord("通道") + tongdaoList[tdIndex].TongdaoCommon.Address;
                    tdNameLabels[tdIndex].Text = tongdaoList[tdIndex].TongdaoCommon.TongdaoName;
                    myToolTip.SetToolTip(tdNameLabels[tdIndex], tongdaoList[tdIndex].TongdaoCommon.Remark);
                    tdTrackBars[tdIndex].Value = tongdaoList[tdIndex].ScrollValue;
                    tdValueNumericUpDowns[tdIndex].Text = tongdaoList[tdIndex].ScrollValue.ToString();
                    tdCmComboBoxes[tdIndex].SelectedIndex = tongdaoList[tdIndex].ChangeMode;

                    //步时间 NewMainForm：主动 乘以时间因子 后 再展示
                    tdStNumericUpDowns[tdIndex].Text = (tongdaoList[tdIndex].StepTime * EachStepTime).ToString();

                    tdTrackBars[tdIndex].ValueChanged += tdTrackBars_ValueChanged;
                    tdValueNumericUpDowns[tdIndex].ValueChanged += tdValueNumericUpDowns_ValueChanged;
                    tdCmComboBoxes[tdIndex].SelectedIndexChanged += tdChangeModeSkinComboBoxes_SelectedIndexChanged;
                    tdStNumericUpDowns[tdIndex].ValueChanged += tdStepTimeNumericUpDowns_ValueChanged;

                    tdPanels[tdIndex].Show();
                }
                for (int tdIndex = tongdaoList.Count; tdIndex < 32; tdIndex++)
                {
                    tdPanels[tdIndex].Hide();
                }

                if (from0on)
                {
                    // DOTO 	generateSaPanels();
                }
            }
                        
        }

        /// <summary>
        /// 辅助方法：根据传进来的LightAst对象，修改当前灯具内的显示内容
        /// </summary>
        /// <param name="la"></param>
        private void showLightsInfo()
        {
            if (checkNoLightSelected())
            {
                lightPictureBox.Image = null;
                lightNameLabel.Text = null;
                lightTypeLabel.Text = null;
                lightsAddrLabel.Text = null;
                lightRemarkLabel.Text = null;
                return;
            }

            LightAst la = LightAstList[selectedIndex];
            lightPictureBox.Image = lightImageList.Images.ContainsKey(la.LightPic) ? Image.FromFile(SavePath + @"\LightPic\" + la.LightPic) : global::LightController.Properties.Resources.灯光图;
            lightNameLabel.Text = LanguageHelper.TranslateWord("厂商：") + la.LightName;
            lightTypeLabel.Text = LanguageHelper.TranslateWord("型号：") + la.LightType;
            lightRemarkLabel.Text = LanguageHelper.TranslateWord("备注：") + (isMultiMode ? "" : la.Remark);
            lightsAddrLabel.Text = LanguageHelper.TranslateWord("地址：") + generateAddrStr();
        }

        /// <summary>
        /// 辅助方法：检查是否有任意灯具被选中
        /// </summary>
        /// <returns></returns>
        private bool checkNoLightSelected()
        {
            return LightAstList == null || LightAstList.Count == 0 || selectedIndex < 0;
        }

        /// <summary>
        /// 辅助方法：（供refreshMultiModeControls调用）通过一些全局变量，生成灯具地址字符串
        /// </summary>
        /// <returns></returns>
        private string generateAddrStr()
        {
            string lightsAddrStr = "";
            if (isMultiMode)
            {
                foreach (int lightIndex in selectedIndexList)
                {
                    lightsAddrStr += (selectedIndexList.Count > 1 && lightIndex == selectedIndex) ?
                                ("(" + LightAstList[lightIndex].LightAddr + ") ") : (LightAstList[lightIndex].LightAddr + " ");
                }
            }
            else
            {
                lightsAddrStr += LightAstList[selectedIndex].LightAddr;
            }
            return lightsAddrStr;
        }

        /// <summary>
        /// 辅助方法：《进入|退出编组》后的刷新相关控件显示
        /// </summary>
        private void refreshMultiModeControls()
        {
            //MARK 只开单场景：15.1 《灯具列表》是否可用，由单灯模式决定
            lightListButton.Enabled = !isMultiMode;
            lightsListView.Enabled = !isMultiMode;
            sceneComboBox.Enabled = !isMultiMode;
            protocolComboBox.Enabled = !isMultiMode;
            copySceneButton.Enabled = !isMultiMode;
            groupFlowLayoutPanel.Enabled = LightAstList != null; // 只要当前工程有灯具，就可以进入编组（再由按钮点击事件进行进一步确认）
            groupButton.Text = isMultiMode ? "退出编组" : "灯具编组";

            lightsListView.SelectedIndexChanged -= lightsListView_SelectedIndexChanged;
            for (int lightIndex = 0; lightIndex < lightsListView.Items.Count; lightIndex++)
            {
                lightsListView.Items[lightIndex].Selected = selectedIndexList.Contains(lightIndex);
            }
            lightsListView.SelectedIndexChanged += lightsListView_SelectedIndexChanged;
        }

        /// <summary>
        /// 事件：更改lightsListView的选中项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lightsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lightsListView.SelectedIndices.Count > 0)
            {
                selectedIndex = lightsListView.SelectedIndices[0];
                generateLightData(); //lightsListView_SelectedIndexChanged				
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
            if (checkNoLightSelected())
            {
                SetNotice("尚未选择灯具。", false, true);
            }
            else
            {
                // 1.生成子属性Panel			
                // DOTO  	generateSaPanels();

                //2.判断是不是已经有stepTemplate了
                // ①若无，则生成数据，并hideAllTongdao 并设stepLabel为“0/0” --> 因为刚创建，肯定没有步数	
                // ②若有，还需判断该LightData的LightStepWrapperList[frame,mode]是不是为null
                //			若是null，则说明该FM下，并未有步数，hideAllTongdao
                //			若不为null，则说明已有数据，
                LightAst lightAst = LightAstList[selectedIndex];
                LightWrapper lightWrapper = LightWrapperList[selectedIndex];
                if (lightWrapper.StepTemplate == null)
                {
                    lightWrapper.StepTemplate = generateStepTemplate(lightAst);
                }
                stepPanel.Enabled = true;
            }

            //3.更新当前灯具信息、并刷新步
            showLightsInfo(); //generateLightData()
            refreshStep();
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
            try
            {
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
                        IniHelper iniAst = new IniHelper(lightAst.LightPath);
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
                                string saName = InHelper_UTF8.ReadString(lightAst.LightPath, "sa", tdIndex + "_" + saIndex + "_saName", "");
                                int startValue = iniAst.ReadInt("sa", tdIndex + "_" + saIndex + "_saStart", 0);
                                int endValue = iniAst.ReadInt("sa", tdIndex + "_" + saIndex + "_saEnd", 0);
                                remark += saName + " : " + startValue + " - " + endValue + "\n";
                                saList.Add(new SA { SAName = saName, StartValue = startValue, EndValue = endValue });
                            }
                            lightAst.SawList.Add(new SAWrapper() { SaList = saList });

                            tongdaoList.Add(new TongdaoWrapper()
                            {
                                ScrollValue = initNum,
                                StepTime = 50,
                                ChangeMode = -1,
                                TongdaoCommon = new TongdaoWrapperCommon()
                                {
                                    TongdaoName = tongdaoName,
                                    Address = lightAst.StartNum + (address - 1),
                                    Remark = remark
                                }
                            });
                        }
                        return new StepWrapper()
                        {
                            TongdaoList = tongdaoList,
                            StepCommon = new StepWrapperCommon()
                            {
                                LightFullName = lightAst.LightName + "*" + lightAst.LightType, // 使用“*”作为分隔符，这样的字符无法在系统生成文件夹，可有效防止有些灯刚好Name+Type的组合相同
                                StartNum = lightAst.StartNum
                            }
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                SetNotice("生成灯具模板时出错:" + ex.Message, true, false);
                throw ex;
            }
        }

        #endregion

        private void HardwareSetButton_Click(object sender, EventArgs e)
        {
            //new HardwareSetForm(this).ShowDialog();
        }

        /// <summary>
        /// 事件：点击《音频链表》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void soundListButton_Click(object sender, EventArgs e)
        {
            new SKForm(this).ShowDialog();
        }

        /// <summary>
        /// 事件：勾选|取消勾选 《音频模式》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetNotice("正在切换模式", false, true);
            CurrentMode = modeCheckBox.Checked ? 1 : 0;

            for (int tdPanelIndex = 0; tdPanelIndex < 32; tdPanelIndex++)
            {
                tdCmComboBoxes[tdPanelIndex].Items.Clear();
                tdCmComboBoxes[tdPanelIndex].Items.AddRange(CurrentMode == 0 ? normalCMArray : soundCMArray);
                tdStNumericUpDowns[tdPanelIndex].Visible = CurrentMode == 0;
            }
            thirdLabel.Visible = CurrentMode == 0;

            changeSceneMode();
            soundListButton.Enabled = CurrentMode == 1;

            SetNotice("成功切换模式", false, true);
        }

        /// <summary>
        /// 事件：更改《场景》下拉框选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sceneComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //setBusy(true);
            //SetNotice("正在切换场景,请稍候...", false, true);
            //endview(); // sceneSelectedChanged (只要更改了场景，直接结束预览)

            //DialogResult dr = MessageBox.Show(
            //	LanguageHelper.TranslateSentence("切换场景前，是否保存之前场景：") + AllSceneList[CurrentScene] + "?",
            //	LanguageHelper.TranslateSentence("保存场景?"),
            //	MessageBoxButtons.YesNo,
            //	MessageBoxIcon.Question);
            //if (dr == DialogResult.Yes)
            //{
            //	saveSceneClick();
            //	//MARK 只开单场景：06.0.1 切换场景时，若选择保存之前场景，则frameSaveArray设为false，意味着以后不需要再保存了。
            //	sceneSaveArray[CurrentScene] = false;
            //}

            //CurrentScene = newScene;
            ////MARK 只开单场景：06.1.1 更改场景时，只有frameLoadArray为false，才需要从DB中加载相关数据（调用generateFrameData）；若为true，则说明已经加载因而无需重复读取。
            //if (!sceneLoadArray[CurrentScene])
            //{
            //	generateSceneData(CurrentScene);
            //}
            ////MARK 只开单场景：06.2.1 更改场景后，需要将frameSaveArray设为true，表示当前场景需要保存
            //sceneSaveArray[CurrentScene] = true;

            //changeSceneMode();
            //setBusy(false);
            //SetNotice(LanguageHelper.TranslateSentence("成功切换为场景：") + AllSceneList[CurrentScene], false, false);
        }

        /// <summary>
        /// 辅助方法： 改变了模式和场景后的操作		
        /// </summary>
        protected void changeSceneMode()
        {
            //不可让selectedIndex为-1  , 否则会出现数组越界错误
            if (selectedIndex == -1)
            {
                return;
            }

            enterSyncMode(false);   // 复位同步状态为false			
            refreshStep(); // 刷新步

            MAX_STEP = IniHelper.GetSystemCount(CurrentMode == 0 ? "maxStep" : "maxStepSound", 0);
            if (MAX_STEP == 0)
            {
                MAX_STEP = CurrentMode == 0 ? 100 : 300;
            }
        }

        #region 协议相关
        protected HSSFWorkbook xlsWorkbook;  // 通过本对象实现相应的xls文件的映射               
        public IList<string> ProtocolList; // 存储协议名（ xls中协议+分割线+pbin协议)
        public IList<string> SceneCodeList; // 存放读取的《Protocol\SceneCode》文件内生成的1-16场景相应的码值，有多处会用到此List; 

        /// <summary>
        /// 辅助方法：供ToolsForm调用，当新增或更改协议项（另存pbin）时，重新渲染protocolComboBox
        /// </summary>
        public void RenderProtocolCB(string protocolName)
        {
            // 当首页原先选择项，为xls中的协议或未选择时，使用CurrentProtocol作为入参
            if (CurrentProtocol <= xlsWorkbook.NumberOfSheets)
            {
                renderProtocolCB(CurrentProtocol);
            }
            //当首页的原先选择项，为pbin协议时，必须要重新定位，此时传入由旧协议名称生成的新index
            else
            {
                renderProtocolCB(GetIndexByPbinName(protocolName));
            }
        }

        /// <summary>
        /// 辅助公用方法：渲染《协议选择框》
        /// </summary>
        protected void renderProtocolCB(int protocolIndex)
        {
            protocolComboBox.SelectedIndexChanged -= protocolComboBox_SelectedIndexChanged;
            protocolComboBox.Items.Clear();
            foreach (string protocolName in ProtocolList)
            {
                protocolComboBox.Items.Add(protocolName);
            }
            protocolComboBox.SelectedIndex = protocolIndex;
            protocolComboBox.SelectedIndexChanged += protocolComboBox_SelectedIndexChanged;
        }
        protected void renderSceneCB()
        {

            sceneComboBox.SelectedIndexChanged -= sceneComboBox_SelectedIndexChanged;
            sceneComboBox.Items.Clear();
            foreach (string scene in AllSceneList)
            {
                sceneComboBox.Items.Add(scene);
            }
            sceneComboBox.SelectedIndex = CurrentScene;
            sceneComboBox.SelectedIndexChanged += sceneComboBox_SelectedIndexChanged;

        }

        /// <summary>
        ///  辅助方法：加载所有协议，包括xls内的和用户另存为的
        /// </summary>
        public void LoadProtocols()
        {
            try
            {
                ProtocolList = new List<string>();
                // 由xls文件加载协议列表；
                using (FileStream file = new FileStream(Application.StartupPath + @"\Protocol\Controller.xls", FileMode.Open, FileAccess.Read))
                {
                    xlsWorkbook = new HSSFWorkbook(file);
                }
                for (int protocolIndex = 0; protocolIndex < xlsWorkbook.NumberOfSheets; protocolIndex++)
                {
                    ISheet sheet = xlsWorkbook.GetSheetAt(protocolIndex);
                    ProtocolList.Add(sheet.SheetName);
                }
                ProtocolList.Add("===============");
                // 加载所有pbin文件；
                FileInfo[] pbinArray = new DirectoryInfo(Application.StartupPath + @"\Protocol\").GetFiles("*.pbin");
                if (pbinArray.Length > 0)
                {
                    foreach (FileInfo pbin in pbinArray)
                    {
                        ProtocolList.Add(pbin.Name.Substring(0, pbin.Name.LastIndexOf(".pbin")));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void protocolComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            protocolChanged(protocolComboBox.SelectedIndex, true);
        }

        /// <summary>
        /// 辅助方法：更改协议（会更改界面上的前十六个场景名；另外可能需要提示是否设为默认值）
        /// </summary>
        /// <param name="protocolIndex"></param>
        protected void protocolChanged(int protocolIndex, bool isNoticeSave)
        {
            CurrentProtocol = protocolIndex;
            rebuildSceneList(protocolIndex);
            renderSceneCB();

            if (isNoticeSave && DialogResult.Yes == MessageBox.Show(
                "您已更改协议为【" + ProtocolList[protocolIndex] + "】，是否把此协议设为默认值?\n(注：如果选择‘==========’，前16个场景会只有场景编号而无场景名)",
                 "设为默认协议?",
                 MessageBoxButtons.YesNo,
                 MessageBoxIcon.Question))
            {
                Properties.Settings.Default.protocolIndex = (protocolIndex == xlsWorkbook.NumberOfSheets || protocolIndex == -1) ? -1 : protocolIndex;
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// 辅助方法：由协议index，改造或重建AllSceneList;
        /// </summary>
        /// <param name="protocolIndex"></param>
        protected void rebuildSceneList(int protocolIndex)
        {
            // 未选择协议 
            if (protocolIndex == xlsWorkbook.NumberOfSheets ||
                protocolIndex == -1 ||
                SceneCodeList == null ||
                SceneCodeList.Count != 16)
            {
                initSceneList();
                return;
            }

            CCEntity ccEntity = GenerateCCEntity(protocolIndex);
            if (ccEntity != null)
            {
                for (int codeIndex = 0; codeIndex < SceneCodeList.Count; codeIndex++)
                {
                    AllSceneList[codeIndex] = ccEntity.CCDataList[Convert.ToInt32(SceneCodeList[codeIndex], 16) - 1].Function;
                }
            }
        }

        /// <summary>
        /// 辅助方法：由传进来的protocolIndex,生成相应的CCEntity（供rebuildSceneList 和 ToolsForm 调用）
        /// </summary>
        /// <param name="protocolIndex"></param>
        /// <returns></returns>
        public CCEntity GenerateCCEntity(int protocolIndex)
        {

            CCEntity ccEntity = null;

            // 选中xls中协议
            if (protocolIndex < xlsWorkbook.NumberOfSheets)
            {
                ccEntity = new CCEntity();
                ISheet sheet = xlsWorkbook.GetSheetAt(protocolIndex);
                ccEntity.ProtocolName = sheet.SheetName;
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                // 处理通用数据(com0,com1,ps2)
                rows.MoveNext();
                IRow row = (HSSFRow)rows.Current;
                ICell cell = row.GetCell(0);
                ccEntity.Com0 = Convert.ToInt32(cell.ToString().Substring(4));
                rows.MoveNext();
                row = (HSSFRow)rows.Current;
                cell = row.GetCell(0);
                ccEntity.Com1 = Convert.ToInt32(cell.ToString().Substring(4));
                rows.MoveNext();
                row = (HSSFRow)rows.Current;
                cell = row.GetCell(0);
                ccEntity.PS2 = cell.ToString().Equals("PS2=主") ? 0 : 1;
                rows.MoveNext();

                //逐一处理每一行的数据
                int rowIndex = 0;
                while (rows.MoveNext())
                {
                    row = (HSSFRow)rows.Current;

                    CCData ccData = new CCData();
                    cell = row.GetCell(0);
                    ccData.Function = (cell == null ? "" : cell.ToString().Trim());
                    cell = row.GetCell(1);
                    ccData.Code = (cell == null ? "" : cell.ToString().Trim());
                    cell = row.GetCell(2);
                    ccData.Com0Up = (cell == null ? "" : cell.ToString().Trim());
                    cell = row.GetCell(3);
                    ccData.Com0Down = (cell == null ? "" : cell.ToString().Trim());
                    cell = row.GetCell(4);
                    ccData.Com1Up = (cell == null ? "" : cell.ToString().Trim());
                    cell = row.GetCell(5);
                    ccData.Com1Down = (cell == null ? "" : cell.ToString().Trim());
                    cell = row.GetCell(6);
                    ccData.InfraredSend = (cell == null ? "" : cell.ToString().Trim());
                    cell = row.GetCell(7);
                    ccData.InfraredReceive = (cell == null ? "" : cell.ToString().Trim());
                    cell = row.GetCell(8);
                    ccData.PS2Up = (cell == null ? "" : cell.ToString().Trim());
                    cell = row.GetCell(9);
                    ccData.PS2Down = (cell == null ? "" : cell.ToString().Trim());

                    ccEntity.CCDataList.Add(ccData);
                    rowIndex++;
                }
            }
            // 选中本地协议
            else if (protocolIndex > xlsWorkbook.NumberOfSheets)
            {
                try
                {
                    ccEntity = (CCEntity)SerializeUtils.DeserializeToObject(Application.StartupPath + @"\protocol\" + ProtocolList[protocolIndex] + ".pbin");
                }
                catch (Exception)
                {
                    ccEntity = null;
                    MessageBox.Show("用户另存的【" + ProtocolList[protocolIndex] + "】协议损坏，无法调用，请重选协议。");
                }
            }
            return ccEntity;
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

        /// <summary>
        /// 辅助方法：传入pbin文件名，得出其在ProtocolList中的index ;  未找到就返回0（出bug才会有这种情况）
        /// </summary>
        /// <param name="pbinName"></param>
        /// <returns></returns>
        public int GetIndexByPbinName(string pbinName)
        {
            for (int protocolIndex = xlsWorkbook.NumberOfSheets + 1; protocolIndex < ProtocolList.Count; protocolIndex++)
            {
                if (ProtocolList[protocolIndex] == pbinName)
                {
                    return protocolIndex;
                }
            }
            return 0;
        }

        #endregion

        #region  MainFormBase() 获取各种当前（步数、灯具）等的辅助方法 

        /// <summary>
        /// 辅助方法：返回当前工程的当前灯具的具体名称，形式为 "lightName\lightType"
        /// </summary>
        /// <returns></returns>
        public string GetCurrentLightType()
        {
            if (LightAstList != null && LightAstList.Count > 0 && selectedIndex != -1)
            {
                LightAst la = LightAstList[selectedIndex];
                return la.LightName + @"\" + la.LightType;
            }
            else
            {
                return null;
            }
        }

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
        protected LightWrapper getSelectedLightWrapper(int lightIndex)
        {

            // 说明尚未点击任何灯具 或 内存内还没有任何灯具
            if (lightIndex == -1 || LightWrapperList == null || LightWrapperList.Count == 0)
            {
                return null;
            }
            return LightWrapperList[lightIndex];
        }

        /// <summary>
        ///  辅助方法：取出当前灯具(frame、mode)的所有步数集合(LightStepWrapper)
        /// </summary>
        /// <returns></returns>
        protected LightStepWrapper getCurrentLightStepWrapper()
        {
            return getSelectedLightStepWrapper(selectedIndex);
        }

        /// <summary>
        /// 辅助方法：取出选中灯具的当前F/M的所有步数据(LightStepWrapper)
        /// </summary>
        /// <param name="selectedIndex"></param>
        /// <returns></returns>
        protected LightStepWrapper getSelectedLightStepWrapper(int lightIndex)
        {
            if (lightIndex < 0)
            {
                return null;
            }

            if (LightWrapperList == null || LightWrapperList.Count == 0)
            {
                return null;
            }

            LightWrapper lightWrapper = LightWrapperList[lightIndex];
            if (lightWrapper == null)
            {
                return null;
            }
            else
            {
                //若为空，则立刻创建一个
                if (lightWrapper.LightStepWrapperList[CurrentScene, CurrentMode] == null)
                {
                    lightWrapper.LightStepWrapperList[CurrentScene, CurrentMode] = new LightStepWrapper()
                    {
                        StepWrapperList = new List<StepWrapper>()
                    };
                };
                return lightWrapper.LightStepWrapperList[CurrentScene, CurrentMode];
            }
        }

        /// <summary>
        /// 辅助方法：直接取出当前灯在当前F/M的当前步(StepWrapper)
        /// /// </summary>
        /// <returns></returns>
        protected StepWrapper getCurrentStepWrapper()
        {
            return getSelectedLightCurrentStepWrapper(selectedIndex);
        }

        /// <summary>
        ///  辅助方法：取出指定灯在当前F/M的当前步(StepWrapper)
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
            else
            {
                return null;
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
            if (lsWrapper == null)
            {
                return null;
            }
            int totalStep = getCurrentTotalStep();
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
        ///  辅助方法：获取当前灯具的StepTemplate，用于还未生成步数时调用
        /// </summary>
        /// <returns></returns>
        public StepWrapper GetCurrentStepTemplate()
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
        protected int getCurrentTotalStep()
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
        /// 辅助方法：取出指定灯具的当前F/M的步数
        /// </summary>
        /// <param name="selectedIndex"></param>
        /// <returns></returns>
        protected int getSelectedLightTotalStep(int lightIndex)
        {
            if (lightIndex < 0)
            {
                return 0;
            }
            LightStepWrapper lsWrapper = getSelectedLightStepWrapper(lightIndex);
            if (lsWrapper == null)
            {
                return 0;
            }
            else
            {
                return lsWrapper.TotalStep;
            }
        }

        /// <summary>
        /// 辅助方法：当灯具列表为空时，清空数据库内所有数据
        /// </summary>
        private void ClearAllDB()
        {
            lightDAO.Clear();
            fineTuneDAO.Clear();
            channelDAO.Clear();
        }

        #endregion

        #region tdPanel相关（监听事件及辅助方法）

        /// <summary>
        /// 事件:鼠标进入tdTrackBar时，把焦点切换到其对应的tdValueNumericUpDown中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tdTrackBars_MouseEnter(object sender, EventArgs e)
        {
            int tdIndex = MathHelper.GetIndexNum(((TrackBar)sender).Name, -1);
            tdValueNumericUpDowns[tdIndex].Select();
        }

        /// <summary>
        ///  事件：鼠标滚动时，通道值每次只变动一个Increment值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tdTrackBars_MouseWheel(object sender, MouseEventArgs e)
        {
            //Console.WriteLine(	"trackBar_mouseWheel");
            int tdIndex = MathHelper.GetIndexNum(((TrackBar)sender).Name, -1);
            HandledMouseEventArgs hme = e as HandledMouseEventArgs;
            if (hme != null)
            {
                //获取或设置是否应将此事件转发到控件的父容器。
                // public bool Handled { get; set; } ==> 如果鼠标事件应转到父控件，则为 true；否则为 false。
                // Dickov: 实际上就是当Handled设为true时，不再触发本控件的默认相关操作(即屏蔽滚动事件)
                hme.Handled = true;
            }

            // 向上滚
            if (e.Delta > 0)
            {
                decimal dd = tdTrackBars[tdIndex].Value + tdTrackBars[tdIndex].SmallChange;
                if (dd <= tdTrackBars[tdIndex].Maximum)
                {
                    tdTrackBars[tdIndex].Value = decimal.ToInt32(dd);
                }
            }
            // 向下滚
            else if (e.Delta < 0)
            {
                decimal dd = tdTrackBars[tdIndex].Value - tdTrackBars[tdIndex].SmallChange;
                if (dd >= tdTrackBars[tdIndex].Minimum)
                {
                    tdTrackBars[tdIndex].Value = decimal.ToInt32(dd);
                }
            }
        }

        /// <summary>
        ///  事件：TrackBar滚轴值改变时的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tdTrackBars_ValueChanged(object sender, EventArgs e)
        {
            // 1.先找出对应tdSkinTrackBars的index 
            int tongdaoIndex = MathHelper.GetIndexNum(((TrackBar)sender).Name, -1);
            int tdValue = tdTrackBars[tongdaoIndex].Value;

            //2.把滚动条的值赋给tdValueNumericUpDowns
            // 8.28	：在修改时取消其监听事件，修改成功恢复监听；这样就能避免重复触发监听事件
            tdValueNumericUpDowns[tongdaoIndex].ValueChanged -= tdValueNumericUpDowns_ValueChanged;
            tdValueNumericUpDowns[tongdaoIndex].Value = tdValue;
            tdValueNumericUpDowns[tongdaoIndex].ValueChanged += tdValueNumericUpDowns_ValueChanged;

            //3.取出recentStep,使用取出的index，给stepWrapper.TongdaoList[index]赋值；并检查是否实时生成数据进行操作
            changeScrollValue(tongdaoIndex, tdValue);
        }

        /// <summary>
        /// 事件：调节或输入numericUpDown的值后，1.调节通道值 2.调节tongdaoWrapper的相关值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tdValueNumericUpDowns_ValueChanged(object sender, EventArgs e)
        {
            // 1. 找出对应的index
            int tongdaoIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);
            int tdValue = decimal.ToInt32(tdValueNumericUpDowns[tongdaoIndex].Value);

            // 2.调整相应的vScrollBar的数值；
            // 8.28 ：在修改时取消其监听事件，修改成功恢复监听；这样就能避免重复触发监听事件
            tdTrackBars[tongdaoIndex].ValueChanged -= new System.EventHandler(this.tdTrackBars_ValueChanged);
            tdTrackBars[tongdaoIndex].Value = tdValue;
            tdTrackBars[tongdaoIndex].ValueChanged += new System.EventHandler(this.tdTrackBars_ValueChanged);

            //3.取出recentStep,使用取出的index，给stepWrapper.TongdaoList[index]赋值；并检查是否实时生成数据进行操作
            changeScrollValue(tongdaoIndex, tdValue);
        }

        /// <summary>
        /// 事件：鼠标进入通道值输入框时，切换焦点;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tdValueNumericUpDowns_MouseEnter(object sender, EventArgs e)
        {
            int tdIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);
            tdValueNumericUpDowns[tdIndex].Select();
        }

        /// <summary>
        ///  事件：鼠标滚动时，通道值每次只变动一个Increment值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tdValueNumericUpDowns_MouseWheel(object sender, MouseEventArgs e)
        {
            int tdIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);
            HandledMouseEventArgs hme = e as HandledMouseEventArgs;
            if (hme != null)
            {
                //获取或设置是否应将此事件转发到控件的父容器。
                // public bool Handled { get; set; } ==> 如果鼠标事件应转到父控件，则为 true；否则为 false。
                // Dickov: 实际上就是当Handled设为true时，不再触发本控件的默认相关操作(即屏蔽滚动事件)
                hme.Handled = true;
            }
            // 向上滚
            if (e.Delta > 0)
            {
                decimal dd = tdValueNumericUpDowns[tdIndex].Value + tdValueNumericUpDowns[tdIndex].Increment;
                if (dd <= tdValueNumericUpDowns[tdIndex].Maximum)
                {
                    tdValueNumericUpDowns[tdIndex].Value = dd;
                }
            }
            // 向下滚
            else if (e.Delta < 0)
            {
                decimal dd = tdValueNumericUpDowns[tdIndex].Value - tdValueNumericUpDowns[tdIndex].Increment;
                if (dd >= tdValueNumericUpDowns[tdIndex].Minimum)
                {
                    tdValueNumericUpDowns[tdIndex].Value = dd;
                }
            }
        }

        /// <summary>
        ///  事件：每个通道对应的变化模式下拉框，值改变后，对应的tongdaoWrapper也应该设置参数 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tdChangeModeSkinComboBoxes_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1.先找出对应changeModeComboBoxes的index
            int tdIndex = MathHelper.GetIndexNum(((ComboBox)sender).Name, -1);

            //2.取出recentStep，这样就能取出一个步数，使用取出的index，给stepWrapper.TongdaoList[index]赋值
            StepWrapper step = getCurrentStepWrapper();
            int changeMode = tdCmComboBoxes[tdIndex].SelectedIndex;
            step.TongdaoList[tdIndex].ChangeMode = tdCmComboBoxes[tdIndex].SelectedIndex;

            //3.多灯模式下，需要把调整复制到各个灯具去
            if (isMultiMode)
            {
                copyValueToAll(tdIndex, EnumUnifyWhere.CHANGE_MODE, changeMode);
            }
        }

        /// <summary>
        /// 事件：鼠标进入步时间输入框时，切换焦点;
        /// 注意：用MouseEnter事件，而非MouseHover事件;这样才会无延时响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tdStepTimeNumericUpDowns_MouseEnter(object sender, EventArgs e)
        {
            int tdIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);
            tdStNumericUpDowns[tdIndex].Select();
        }

        /// <summary>
        ///  事件：鼠标滚动时，步时间值每次只变动一个Increment值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tdStepTimeNumericUpDowns_MouseWheel(object sender, MouseEventArgs e)
        {
            int tdIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);
            HandledMouseEventArgs hme = e as HandledMouseEventArgs;
            if (hme != null)
            {
                hme.Handled = true;
            }
            if (e.Delta > 0)
            {
                decimal dd = tdStNumericUpDowns[tdIndex].Value + tdStNumericUpDowns[tdIndex].Increment;
                if (dd <= tdStNumericUpDowns[tdIndex].Maximum)
                {
                    tdStNumericUpDowns[tdIndex].Value = dd;
                }
            }
            else if (e.Delta < 0)
            {
                decimal dd = tdStNumericUpDowns[tdIndex].Value - tdStNumericUpDowns[tdIndex].Increment;
                if (dd >= tdStNumericUpDowns[tdIndex].Minimum)
                {
                    tdStNumericUpDowns[tdIndex].Value = dd;
                }
            }
        }

        /// <summary>
        /// 事件： tdStepTimeNumericUpDown值变化时,修改内存中相应Step的tongdaoList的stepTime值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tdStepTimeNumericUpDowns_ValueChanged(object sender, EventArgs e)
        {
            // 1.先找出对应stepNumericUpDowns的index（这个比较麻烦，因为其NumericUpDown的序号是从33开始的 即： name33 = names[0] =>addNum = -33）
            int tdIndex = MathHelper.GetIndexNum(((NumericUpDown)sender).Name, -1);

            //2.取出recentStep，这样就能取出一个步数，使用取出的index，给stepWrapper.TongdaoList[index]赋值
            StepWrapper step = getCurrentStepWrapper();

            // MARK 步时间 NewMainForm：处理为数据库所需数值：将 (显示的步时间* 时间因子)后再放入内存
            int stepTime = decimal.ToInt32(tdStNumericUpDowns[tdIndex].Value / EachStepTime); // 取得的值自动向下取整（即舍去多余的小数位）
            step.TongdaoList[tdIndex].StepTime = stepTime;
            tdStNumericUpDowns[tdIndex].Value = stepTime * EachStepTime; //若与所见到的值有所区别，则将界面控件的值设为处理过的值

            if (isMultiMode)
            {
                copyValueToAll(tdIndex, EnumUnifyWhere.STEP_TIME, stepTime);
            }
        }

        /// <summary>
        /// 事件：点击《tdNameLabels》时，右侧的子属性按钮组，会显示当前通道相关的子属性，其他通道的子属性，则隐藏掉
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tdNameNumLabels_Click(object sender, EventArgs e)
        {
            //DOTO tdNameNumLabels_Click
            //tdNameNumLabelClick(sender);
        }

        /// <summary>
        /// 事件：监听几个通道输入框的键盘点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void unifyTd_KeyPress(object sender, KeyPressEventArgs e)
        {
            unifyTdKeyPress(sender, e);
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

            if (isMultiMode)
            {
                copyValueToAll(tdIndex, EnumUnifyWhere.SCROLL_VALUE, tdValue);
            }

            OneStepPlay(null, null); // changeScrollValue()			
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
                endStep = getCurrentTotalStep();    // 因存在输入框，表示步数一定存在，无需验证空指针
                                                    // 若始末步相同，意味着此操作无意义，直接return
                if (startStep == endStep)
                {
                    return;
                }

                tdIndexList.Add(MathHelper.GetIndexNum(control.Name, -1));
                msg = LanguageHelper.TranslateSentence("要将【当前通道所有步】");
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
                msg = LanguageHelper.TranslateSentence("要将【当前步所有通道】");
            }

            // 定义where，因为下面会多次使用；
            EnumUnifyWhere where = (EnumUnifyWhere)int.Parse(control.Tag.ToString());
            if (where == EnumUnifyWhere.SCROLL_VALUE)
            {
                NumericUpDown valueNUD = control as NumericUpDown;
                unifyValue = decimal.ToInt32(valueNUD.Value);
                msg += LanguageHelper.TranslateSentence("的通道值都设为： ") + unifyValue + " ？";
            }
            else if (where == EnumUnifyWhere.CHANGE_MODE)
            {
                ComboBox cb = control as ComboBox;
                unifyValue = cb.SelectedIndex;
                msg += LanguageHelper.TranslateSentence("的跳渐变都设为：") + cb.Text + " ？";
            }
            else if (where == EnumUnifyWhere.STEP_TIME)
            {
                NumericUpDown stNUD = control as NumericUpDown;
                decimal stepTime = stNUD.Value;
                unifyValue = decimal.ToInt32(stepTime / EachStepTime);
                msg += LanguageHelper.TranslateSentence("的步时间都设为：") + stepTime + " ？";
            }
            else
            {
                SetNotice("错误的Tag。", true, true);
                return;
            }

            // 设置了提示，且用户点击了取消，则return。否则继续往下走
            if (IsNoticeUnifyTd)
            {
                if (DialogResult.Cancel == MessageBox.Show(
                        msg,
                        LanguageHelper.TranslateSentence("通道统一设值"),
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question))
                {
                    return;
                }
            }

            SetMultiStepValues(where, tdIndexList, startStep, endStep, 0, unifyValue);

        }

        /// <summary>
        ///  辅助方法：供《多步(多通道)调节》使用
        /// </summary>
        /// <param name="tdIndexList">要设置的通道Index列表，从0开始</param>
        /// <param name="startStep">开始步</param>
        /// <param name="endStep">结束步</param>
        /// <param name="where">统一设置的属性</param>
        /// <param name="stepPos">全部步0 ;单数步1、双数步2</param>
        /// <param name="unifyValue">统一要设的值，如果是跳渐变则为其索引</param>
        public void SetMultiStepValues(EnumUnifyWhere where, IList<int> tdIndexList, int startStep, int endStep, int stepPos, int unifyValue)
        {
            // 编组模式，将值赋给每个编组的灯具中
            if (isMultiMode)
            {
                foreach (int lightIndex in selectedIndexList)
                {
                    LightStepWrapper lsWrapper = getSelectedLightStepWrapper(lightIndex);
                    for (int stepIndex = startStep - 1; stepIndex < endStep; stepIndex++)
                    {
                        if (stepPos == 0 || (stepPos == 1 && stepIndex % 2 == 0) || (stepPos == 2 && stepIndex % 2 != 0))
                        {
                            lsWrapper.StepWrapperList[stepIndex].MultiChangeValue(where, tdIndexList, unifyValue);
                        }
                    }
                }
            }
            // 单灯模式，则只需更改当前灯具的数据即可。
            else
            {
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
            refreshStep();
        }

        /// <summary>
        /// 辅助方法：编组模式中，利用此方法，将修改不多的组长数据（如部分通道值、渐变方式、步时间等），用此改动较少的方法，赋给所有的组员
        /// </summary>
        /// <param name="groupSelectedIndex"></param>
        protected void copyValueToAll(int tdIndex, EnumUnifyWhere where, int value)
        {
            LightStepWrapper mainLSWrapper = getCurrentLightStepWrapper(); //取出组长
            int currentStep = getCurrentStep();     // 取出组长的当前步
            foreach (int index in selectedIndexList)
            {
                if (getSelectedLightStepWrapper(index).StepWrapperList[currentStep - 1] != null)
                {
                    switch (where)
                    {
                        case EnumUnifyWhere.SCROLL_VALUE:
                            getSelectedLightStepWrapper(index).StepWrapperList[currentStep - 1].TongdaoList[tdIndex].ScrollValue = value; break;
                        case EnumUnifyWhere.CHANGE_MODE:
                            getSelectedLightStepWrapper(index).StepWrapperList[currentStep - 1].TongdaoList[tdIndex].ChangeMode = value; break;
                        case EnumUnifyWhere.STEP_TIME:
                            getSelectedLightStepWrapper(index).StepWrapperList[currentStep - 1].TongdaoList[tdIndex].StepTime = value; break;
                    }
                }
            }
        }

        /// <summary>
        /// 辅助方法：刷新当前步;
        /// </summary>
        protected void refreshStep()
        {
            chooseStep(getCurrentStep());  // RefreshStep
        }

        /// <summary>
        /// 辅助方法：抽象了【选择某一个指定步数后，统一的操作；NextStep和BackStep等应该都使用这个方法】
        /// --所有更换通道数据的操作后，都应该使用这个方法。
        /// </summary>
        protected void chooseStep(int stepNum)
        {
            if (stepNum == 0)
            {
                showTDPanels(null);
                showStepLabelMore(0, 0); // chooseStep(0)
                from0on = true;
            }
            else
            {
                if (getCurrentLightStepWrapper() == null)
                {
                    SetNotice("尚未选中灯具，无法选步。", true, true);
                    return;
                }

                LightStepWrapper lightStepWrapper = getCurrentLightStepWrapper();
                StepWrapper stepWrapper = lightStepWrapper.StepWrapperList[stepNum - 1];
                lightStepWrapper.CurrentStep = stepNum;

                if (isMultiMode)
                {
                    foreach (int lightIndex in selectedIndexList)
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

                showTDPanels(stepWrapper.TongdaoList);
                showStepLabelMore(lightStepWrapper.CurrentStep, lightStepWrapper.TotalStep); //chooseStep
                from0on = false;
            }

            OneStepPlay(null, null); // chooseStep			
        }

        /// <summary>
        /// 辅助方法：单(多)灯单步发送DMX512帧数据
        /// </summary>
        public void OneStepPlay(byte[] stepBytes, MaterialAst material)
        {
            //DOTO OneStepPlay
            //if (IsEnableOneStepPlay())
            //{
            //	string prevStr = "正在调试单步数据 ";
            //	// 当stepBytes为空时，才需要根据material处理，否则直接使用此stepBytes播放即可；
            //	if (stepBytes == null)
            //	{

            //		stepBytes = new byte[512];
            //		int currentStep = getCurrentStep();

            //		if (LightWrapperList != null && LightWrapperList.Count > 0)
            //		{

            //			for (int lightIndex = 0; lightIndex < LightWrapperList.Count; lightIndex++)
            //			{
            //				if (lightIndex == selectedIndex // 当前灯具一定会动
            //					|| isMultiMode && selectedIndexList.Contains(lightIndex)  // 编组模式下，组员也要动
            //					|| isKeepOtherLights  // 保持其它灯状态时，所有灯都要有数据
            //					|| isSyncMode  // 同步状态下，所有灯一起动
            //					)
            //				{
            //					StepWrapper stepWrapper = getSelectedLightCurrentStepWrapper(lightIndex);
            //					if (stepWrapper != null)
            //					{
            //						foreach (TongdaoWrapper td in stepWrapper.TongdaoList)
            //						{
            //							stepBytes[td.TongdaoCommon.Address - 1] = (byte)td.ScrollValue;
            //						}
            //					}
            //				}
            //			}

            //			//MARK : 1221 OneStepPlay添加material后，实时生成(基于现有stepBytes进行处理)。
            //			if (material != null)
            //			{
            //				for (int lightIndex = 0; lightIndex < LightWrapperList.Count; lightIndex++)
            //				{
            //					if (lightIndex == selectedIndex || isMultiMode && selectedIndexList.Contains(lightIndex))
            //					{

            //						IList<TongdaoWrapper> tdList = getSelectedLightStepTemplate(lightIndex).TongdaoList;
            //						foreach (MaterialIndexAst mi in getSameTDIndexList(material.TdNameList, tdList))
            //						{
            //							stepBytes[tdList[mi.CurrentTDIndex].TongdaoCommon.Address - 1] = (byte)material.TongdaoArray[0, mi.MaterialTDIndex].ScrollValue;
            //						}
            //					}
            //				}
            //			}
            //		}

            //		prevStr = LanguageHelper.TranslateWord("正在调试灯具：") + (selectedIndex + 1) + LanguageHelper.TranslateWord("，当前步：") + currentStep;
            //	}

            //	// 打印单步调试时的某些通道值（注意这个方法必须写在这个位置，否则可能直接无数据）
            //	string tdValueStr = "";
            //	if (tdValues != null && tdValues.Count > 0)
            //	{
            //		tdValueStr += "【";
            //		foreach (int tdIndex in tdValues)
            //		{
            //			tdValueStr += stepBytes[tdIndex] + " ";
            //		}
            //		tdValueStr = tdValueStr.TrimEnd(); // 去掉结尾的空格
            //		tdValueStr += "】";
            //	}

            //	// 当使用网络连接设备时，用NetworkPlayTools播放；
            //	if (IsDeviceConnected)
            //	{
            //		networkPlayer.SingleStepPreview(stepBytes, this);
            //	}
            //	// 当DMX512调试线也连接在灯具时，也可调试；（双规并行）
            //	if (IsDMXConnected)
            //	{
            //		SerialPlayer.SingleStepPreview(stepBytes, this);
            //	}

            //	SetNotice(prevStr + tdValueStr, false, false);
            //}
        }

        #endregion

        #region 通用方法

        /// <summary>
        /// 设置提示信息
        /// </summary>
        /// <param name="notice"></param>
        public void SetNotice(string notice, bool msgBoxShow, bool isTranslate)
        {
            if (isTranslate)
            {
                notice = LanguageHelper.TranslateSentence(notice);
            }

            myStatusLabel.Text = notice;
            myStatusStrip.Refresh();
            if (msgBoxShow)
            {
                MessageBox.Show(notice);
            }
        }

        /// <summary>
        /// 设置是否忙时
        /// </summary>
        /// <param name="buzy"></param>
        protected void setBusy(bool busy)
        {
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
            Enabled = !busy;
        }

        #endregion

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

        private void closeButton_Click(object sender, EventArgs e)
        {
            if (!RequestSaveProject(LanguageHelper.TranslateSentence("关闭工程前，是否保存当前工程?")))
            {
                return;
            }

            string tempProjectName = currentProjectName;
            clearAllData();
            SetNotice(LanguageHelper.TranslateSentence("成功关闭工程") + "【" + tempProjectName + "】。", true, false);
        }

        /// <summary>
        /// 辅助方法：请求保存工程
        /// </summary>
        /// <returns>若在本方法之后继续下去，则返回true；若是不再往下执行，则返回false</returns>
        /// <param name="msg">提示保存的消息</param>
        public bool RequestSaveProject(string msg)
        {
            //若frameSaveArray为空，表示当前软件内存内没有工程，不需弹出保存工程。
            if (sceneSaveArray == null)
            {
                return true;
            }

            DialogResult dr = MessageBox.Show(
                msg,
                LanguageHelper.TranslateSentence("保存工程?"),
                MessageBoxButtons.YesNoCancel,
                 MessageBoxIcon.Question
            );

            if (dr == DialogResult.Yes)
            {
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

        private void globalSetButton_Click(object sender, EventArgs e)
        {
            new GlobalSetForm(this).ShowDialog();
        }

        /// <summary>
        /// 事件：点击《上一步》（空方法导航用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void prevButton_Click(object sender, EventArgs e)     {    }

        /// 事件：鼠标（左|右键）按下《上一步》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void prevButton_MouseDown(object sender, MouseEventArgs e) {

            if (e.Button == MouseButtons.Left)
            {
                int currentStep = getCurrentStep();
                chooseStep(currentStep > 1 ? currentStep - 1 : getCurrentTotalStep());  //backStepClick
            }
            else if (e.Button == MouseButtons.Right)
            {
                chooseStep(1); //backStepButton_MouseDown
            }
        }

        /// <summary>
        /// 事件：点击《下一步》（空方法导航用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextButton_Click(object sender, EventArgs e)   {    }

        /// 事件：鼠标（左|右键）按下《下一步》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextStepButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int currentStep = getCurrentStep();
                int totalStep = getCurrentTotalStep();
                chooseStep(currentStep < totalStep ? currentStep + 1 : 1);
            }
            else if (e.Button == MouseButtons.Right)
            {
                chooseStep(getCurrentTotalStep()); //nextStepButton_MouseDown
            }
        }

        /// <summary>
        /// 事件：点击《->（跳转步）》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chooseStepButton_Click(object sender, EventArgs e)
        {
            int stepNum = decimal.ToInt32(chooseStepNumericUpDown.Value);
            if (stepNum == 0)
            {
                MessageBox.Show("不可选择0步");
                return;
            }
            chooseStep(stepNum);     //chooseStepButton_Click
        }

        /// <summary>
        /// 事件：点击《插入步》(空方法导航用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void insertButton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 事件：左右键点击《插入步》(前插或后插)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void insertButton_MouseDown(object sender, MouseEventArgs e)
        {
            // 通用判断语句：非左右键点击时，return
            if (!new MouseButtons[] { MouseButtons.Left, MouseButtons.Right }.Contains(e.Button)) return; 

            LightStepWrapper lsWrapper = getCurrentLightStepWrapper();
            if (lsWrapper == null)
            {
                SetNotice("尚未选中灯具，无法插入步。", true, true);
                return;
            }
            if (lsWrapper.TotalStep >= MAX_STEP)
            {
                SetNotice("总步数已达到上限，无法插入步。", true, true);
                return;
            }

            bool insertBefore = e.Button == MouseButtons.Right && getCurrentStep() != 0;   //当点击右键且当前有步时，才能采取前插；否则一律后插

            int currentStep = lsWrapper.CurrentStep;    // 当前步
            int stepIndex = currentStep - 1;  //插入的位置：InsertStep方法中有针对前后插的判断，无需处理	

            StepWrapper newStep;
            if (insertBefore)
            {
                newStep = StepWrapper.GenerateNewStep(stepIndex == 0 ? GetCurrentStepTemplate() : getSelectedLightSelectedStepWrapper(selectedIndex, stepIndex - 1), CurrentMode);
            }
            else
            {
                newStep = StepWrapper.GenerateNewStep(currentStep == 0 ? GetCurrentStepTemplate() : getCurrentStepWrapper(), CurrentMode);
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
            else if (isMultiMode)
            {
                foreach (int lightIndex in selectedIndexList)
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

            refreshStep();
        }

        /// <summary>
        /// 事件：点击《追加步》(空方法导航用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void appendButton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 事件：左右键点击《追加步》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void appendButton_MouseDown(object sender, MouseEventArgs e)
        {
            // 通用判断语句：非左右键点击时，return
            if (!new MouseButtons[] { MouseButtons.Left, MouseButtons.Right }.Contains(e.Button)) return; 

            LightStepWrapper lsWrapper = getCurrentLightStepWrapper();
            if (lsWrapper == null)
            {
                SetNotice("尚未选中灯具，无法追加步。", true, true);
                return;
            }
            if (lsWrapper.TotalStep >= MAX_STEP)
            {
                SetNotice("总步数已达到上限，无法追加步。", true, true);
                return;
            }

            // 当点击左键时，追加一步，否则弹出AppendStepForm
            if (e.Button == MouseButtons.Left)
            {
                addStep();
                refreshStep();
            }
            else
            {
                new AppendStepsForm(this, MAX_STEP - getCurrentTotalStep()).ShowDialog();
            }
        }

        /// <summary>
        /// 事件：点击《删除步》(空方法导航用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, EventArgs e) { }

        /// <summary>
        /// 辅助方法：左键点击《删除步》
        ///  左键 1.获取当前步，当前步对应的stepIndex
        ///	       2.通过stepIndex，DeleteStep(index);
        ///		   3.获取新步(step删除后会自动生成新的)，并重新渲染stepLabel和vScrollBars
        ///  右键 新建DeleteStepForm并显示
        /// </summary>
        protected void deleteButton_MouseDown(object sender, MouseEventArgs e)
        {
            // 通用判断语句：非左右键点击时，return
            if (!new MouseButtons[] { MouseButtons.Left, MouseButtons.Right }.Contains( e.Button )) return; 
          
            if (getCurrentLightStepWrapper() == null)
            {
                SetNotice("尚未选中灯具，无法删除步。", true, true);
                return;
            }
            if (getCurrentTotalStep() == 0)
            {
                SetNotice("当前灯具没有步数，无法删除步。", true, true);
                return;
            }

            // 左键点击删除一步
            if ( e.Button == MouseButtons.Left)
            {
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
                    else if (isMultiMode)
                    {
                        foreach (int lightIndex in selectedIndexList)
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
                refreshStep();
            }
            // 如果右键点击，则弹出删除多步的窗口
            else
            {
                new DeleteStepsForm(this, getCurrentStep(), getCurrentTotalStep()).ShowDialog();
            }
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
                refreshStep();
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 辅助方法：追加步（供追加一步或追加多步使用）
        /// </summary>
        protected void addStep()
        {
            //1.若当前灯具在本F/M下总步数为0 ，则使用stepTemplate数据，
            //2.否则使用本灯当前最大步的数据			 
            bool addTemplate = getCurrentTotalStep() == 0;
            StepWrapper newStep = StepWrapper.GenerateNewStep(addTemplate ? GetCurrentStepTemplate() : getCurrentLightLastStepWrapper(), CurrentMode);
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
            else if (isMultiMode)
            {
                foreach (int lightIndex in selectedIndexList)
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
        ///  辅助方法：删除指定步（起始步和要删除的总步数）
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
                    else if (isMultiMode)
                    {
                        foreach (int lightIndex in selectedIndexList)
                        {
                            if (lightIndex != selectedIndex)
                            {
                                getSelectedLightStepWrapper(lightIndex).DeleteStep(stepIndex);
                            }
                        }
                    }
                }
                refreshStep();
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 辅助方法：删除所有步（供使用素材时选择覆盖时使用）
        /// </summary>
        private void clearSteps()
        {
            int totalStep = getCurrentTotalStep();
            // 当总步数为0时，无需任何操作; 当总步数超过0时，firstStep一定是1；
            if (totalStep > 0)
            {
                DeleteSteps(1, totalStep);
            }
        }


    }
}
