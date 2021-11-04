﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Collections;
using LightController.Ast;
using LightController.Common;
using LightController.Tools;
using System.Windows.Forms;
using System.Threading;
using LightController.Tools.CSJ.IMPL;
using LightController.Utils;
using LightEditor.Ast;
using LightController.MyForm.LightList;
using System.Diagnostics;
using LightController.MyForm.Multiplex;
using LightController.PeripheralDevice;
using System.Drawing;
using LightController.MyForm.MainFormAst;
using LightController.MyForm.HardwareSet;
using LightController.MyForm.OtherTools;
using LightController.Xiaosa.Tools;
using LightController.Entity;
using LightController.Ast.Enum;
using LightController.Xiaosa.Preview;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace LightController.MyForm
{
    public class MainFormBase : Form, MainFormInterface
    {
        // 几个全局的辅助控件（导出文件、toolTip提示等）
        protected FolderBrowserDialog exportFolderBrowserDialog;
        private System.ComponentModel.IContainer components;
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

        public bool IsShowTestButton = false;
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
        protected MaterialUseForm materialUseForm = null; // 存储一个materialForm界面的实例，初次使用时新建

        // 程序运行后，动态变化的变量
        protected string arrangeIniPath = null;  // 打开工程时 顺便把相关的位置保存ini(arrange.ini) 也读取出来（若有的话）
        protected bool isAutoArrange = true; // 默认情况下，此值为true，代表右键菜单“自动排列”默认情况下是打开的。		

        // 工程相关的变量（只在工程载入后才用到的变量）
        protected string currentProjectName;  //存放当前工程名，主要作用是防止当前工程被删除（openForm中）
        protected string currentProjectPath; //存放当前工程所在目录
        public string GlobalIniPath;  // 存放当前工程《全局配置》、《摇麦设置》的配置文件的路径
        protected string dbFilePath;  // 211008  新的db存储路径
        private string exportPath; // 导出工程的目录（每次都可能会发生变动）
        protected bool isEncrypt = false; //是否加密				
        public decimal EachStepTime = 0.03m; //默认情况下，步时间默认值为0.03s（=30ms）
        protected string groupIniPath; // 存放编组文件存放路径
        public IList<GroupAst> GroupList; // 存放编组列表（public 因为多步联调等Form会用到）

        public DetailMultiAstForm DmaForm; //存储一个全局的DetailMultiAstForm，用以记录之前用户选过的将进行多步联调的通道
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
        public ConnectForm ConnForm; // 《设备连接》的窗口，只留一个实体即可
        public NetworkConnect MyConnect;  // 与设备的连接
        protected DMX512ConnnectForm dmxConnForm; //《DMX512调试线连接》的窗口，只留一个实体即可	

        protected Player networkPlayer = Player.GetPlayer(); // 通过设备，调试512灯具的对象
        public SerialPortPlayer SerialPlayer = SerialPortPlayer.GetPlayer();     // 通过DMX512调试线直连设备，调试512灯具的对象

        public bool IsDeviceConnected = false; // 辅助bool值，当选择《连接设备》后，设为true；反之为false
        public bool IsDMXConnected = false; // 辅助bool值，当DMX512线已经连接时设为true，反之为false
        protected bool isKeepOtherLights = false;  // 辅助bool值，当选择《（非调灯具)保持状态》时，设为true；反之为false
        public bool IsPreviewing = false; // 是否预览状态中
        public long LastSendTime; // 记录最近一次StartDebug的时间戳，之后如果要发StopPreview，需要等这个时间过2s才进行；		

        #region 几个纯虚（virtual修饰）方法：主要供各种基类方法向子类回调使用

        // 全局
        protected virtual void setBusy(bool buzy) { } //设置是否忙时
        public virtual void SetNotice(string notice, bool msgBoxShow, bool isTranslate) { } //设置提示信息（有些重要提示，则需弹窗）
        protected virtual void initStNumericUpDowns() { }  // 初始化工程时，需要初始化其中的步时间控件的参数值	

        // 工程面板
        protected virtual void enableProjectRelative(bool enable) { } // 是否显示《保存工程》等

        // listView相关
        protected virtual void reBuildLightListView() { } //根据现有的lightAstList，重新渲染listView
        protected virtual void autoEnableSLArrange() { } //自动显示《 存、取 灯具位置》		
        protected virtual void enableRefreshPic(bool enable) { } // 是否使能《重新加载灯具图片》
        protected virtual void enableStepPanel(bool enable) { } //是否使能《步数面板》

        // 步数面板
        protected virtual void renderProtocolCB(int protocolIndex) { }
        public virtual void RenderSceneCB() { } //渲染场景下拉框（外设配置也用得到）
        protected virtual void showStepLabelMore(int currentStep, int totalStep) { } //显示步数标签，并判断stepPanel按钮组是否可用		
        protected virtual void enterSyncMode(bool isSyncMode) { } // 设置是否 同步模式
        protected virtual void changeCurrentScene(int sceneIndex) { } //MARK 只开单场景：02.0 改变当前Frame		

        // 辅助面板
        protected virtual void showLightsInfo() { }  //显示灯具详情到面板中	
        protected virtual void refreshGroupPanels() { } // 从groupList重新生成相关的编组列表的panels		
        protected virtual void refreshMultiModeControls() { }  //进入退出编组模式后的相关操作（让子Form自行控制各个控件的显示等）

        // 通道面板		
        protected virtual void showTDPanels(IList<TongdaoWrapper> tongdaoList) { } //通过传来的数值，生成通道列表的数据		
        protected virtual void generateSaPanels() { } // 实时生成并显示相应的子属性面板							

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

        /// <summary>
		/// 辅助方法：更改协议（会更改界面上的前十六个场景名；另外可能需要提示是否设为默认值）
		/// </summary>
		/// <param name="protocolIndex"></param>
        protected void protocolChanged(int protocolIndex, bool isNoticeSave)
        {
            CurrentProtocol = protocolIndex;
            rebuildSceneList(protocolIndex);
            RenderSceneCB();

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

        // 调试面板
        protected virtual void refreshConnectedControls(bool isDeviceConnected, bool isPreviewing)
        {
            IsDeviceConnected = isDeviceConnected;
            IsPreviewing = isPreviewing;
            ConnectStr = " [ "
                + (IsDeviceConnected ? "设备已连接：" + MyConnect.DeviceName : "设备未连接")
                + (IsDMXConnected ? " #" : "")
                + " ]";
            Text = SoftwareName + projectStr + ConnectStr;
        } //设置《连接按钮组》是否可用
        /// <summary>
        ///  辅助方法：供《DMX512连接Form》使用，使可以更改一些数据
        /// </summary>
        public void RefreshConnectedControls(bool isDMXConnected)
        {
            IsDMXConnected = isDMXConnected;
            refreshConnectedControls(IsDeviceConnected, IsPreviewing);
        }
        public virtual void SetPreview(bool preview) { }  // 主要供预览失败或成功使用，各子Form更改相应的显示
        protected virtual void setMakeSound(bool makeSound) { } // 点击触发音频后，各子Form更改相应的显示			
        public bool IsOneMoreConnected() { return IsDeviceConnected || IsDMXConnected; }
        public bool IsEnableOneStepPlay() { return IsOneMoreConnected() && !IsPreviewing; } // 返回是否可以单步调试（抽象以避免重复）

        #endregion

        #region 存储一些供其他Form使用的变量，比如已打开的升级文件、工程文件等

        /// <summary>
        /// 辅助方法：（供DetailMultiForm等调用）返回当前灯具某个通道的封装类；
        /// </summary>
        /// <returns></returns>
        internal SAWrapper GetSeletecdLightTdSaw(int lightIndex, int tdIndex)
        {

            if (LightAstList == null || LightAstList.Count == 0 || lightIndex == -1)
            {
                return null;
            }

            return LightAstList[lightIndex].SawList[tdIndex];
        }

        #endregion

        /// <summary>
        /// 辅助方法：以当前打开的工程信息，生成源文件（Source->工程文件夹、LightLibrary）；
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
                    // MARK 3.0416 压缩文件直接继承到同一个方法中，过后把考虑Source工作目录直接删掉；
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
        /// 辅助方法：在《在全局配置》中改变了时间因子并保存后，mainForm的时间因子变量也跟着改变，同时刷新当前步
        /// </summary>
        public void ChangeEachStepTime(int eachStepTime)
        {
            EachStepTime = eachStepTime / 1000m;
            refreshStep();
        }

        /// <summary>
        ///  辅助方法：判断是否可以显示 playPanel及 刷新图片(主要供《打开工程》和《灯具列表Form》使用），故可以在BuildLightList中使用
        ///  --这两个功能都依赖于当前Form中的lightAstList是否为空。
        /// </summary>
        protected void autosetEnabledPlayAndRefreshPic()
        {
            bool enable = LightAstList != null && LightAstList.Count > 0;
            enableRefreshPic(enable);
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
            autosetEnabledPlayAndRefreshPic(); //ReBuildLightList
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
            if (DmaForm != null)
            {
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

                            //2110262 修改generateStepTemplate，改为TongdaoCommon
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

        /// <summary>
        /// 辅助方法：此处的数据只供预览使用， 因为预览只针对单个场景，没有必要传递所有的value数据。
        /// </summary>
        //protected IList<DB_Value> generateDBValueList(int tempFrame)
        //{
        //    IList<DB_Value> tempValueList = new List<DB_Value>();
        //    if (LightAstList != null && LightAstList.Count > 0)
        //    {
        //        foreach (LightWrapper lightTemp in LightWrapperList)
        //        {
        //            DB_Light light = dbLightList[LightWrapperList.IndexOf(lightTemp)];
        //            LightStepWrapper[,] lswl = lightTemp.LightStepWrapperList;
        //            for (int mode = 0; mode < 2; mode++)
        //            {
        //                LightStepWrapper lightStep = lswl[tempFrame, mode];
        //                if (lightStep != null && lightStep.TotalStep > 0)
        //                {  //只有不为null，才可能有需要保存的数据
        //                    IList<StepWrapper> stepWrapperList = lightStep.StepWrapperList;
        //                    foreach (StepWrapper step in stepWrapperList)
        //                    {
        //                        int stepIndex = stepWrapperList.IndexOf(step) + 1;
        //                        for (int tongdaoIndex = 0; tongdaoIndex < step.TongdaoList.Count; tongdaoIndex++)
        //                        {
        //                            TongdaoWrapper tongdao = step.TongdaoList[tongdaoIndex];
        //                            DB_Value valueTemp = new DB_Value()
        //                            {
        //                                ChangeMode = tongdao.ChangeMode,
        //                                ScrollValue = tongdao.ScrollValue,
        //                                StepTime = tongdao.StepTime,
        //                                PK = new DB_ValuePK()
        //                                {
        //                                    Frame = tempFrame,
        //                                    Mode = mode,
        //                                    LightID = light.LightNo + tongdaoIndex,
        //                                    LightIndex = light.LightNo,
        //                                    Step = stepIndex
        //                                }
        //                            };
        //                            tempValueList.Add(valueTemp);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return tempValueList;
        //}

        #endregion

        #region 素材相关

        /// <summary>
        /// 辅助方法:调用素材
        /// </summary>
        /// <param name="materialAst"></param>
        /// <param name="insMethod"></param>
        public virtual void UseMaterial(MaterialAst materialAst, InsertMethod insMethod, bool isShieldOthers)
        {
            if (materialAst == null)
            {
                SetNotice("素材调用失败(material为空)", true, true);
                return;
            }

            //21.2.4 添加一个CLEAR_COVER枚举，需要先清空步，再设为插入(覆盖、追加都行，效果一样)。
            if (insMethod == InsertMethod.CLEAR_COVER)
            {
                clearSteps();
                insMethod = InsertMethod.INSERT;
            }

            LightStepWrapper lsWrapper = getCurrentLightStepWrapper();
            int totalStep = lsWrapper.TotalStep;
            int currentStep = lsWrapper.CurrentStep;
            int addStepCount = materialAst.StepCount;

            // 选择《追加》时，不更改核心代码 : 而是先选择最后步(0步则不走这个)，再设置method = insert
            if (insMethod == InsertMethod.APPEND)
            {
                if (totalStep != 0 && currentStep != totalStep)
                {
                    chooseStep(totalStep); //UseMaterial
                }
                insMethod = InsertMethod.INSERT;
            }

            // 选择《插入》时的操作：后插法（往当前步后加数据）
            // 8.28 当选择《覆盖》但总步数为0时（currentStep也是0），也用插入的方法
            if (insMethod == InsertMethod.INSERT || totalStep == 0)
            {
                int finalStep = totalStep + addStepCount;  // 选择插入多步时，这里的finalStep是指最终的总
                if (finalStep > MAX_STEP)
                {
                    SetNotice("素材(或多步)步数超过当前模式剩余步数，无法调用", true, true);
                    return;
                }

                StepWrapper stepTemplate = GetCurrentStepTemplate();
                IList<MaterialIndexAst> sameTDIndexList = getSameTDIndexList(materialAst.TdNameList, stepTemplate.TongdaoList);
                if (sameTDIndexList.Count == 0)
                {
                    SetNotice("该素材(或多步)与当前灯具不匹配，无法调用", true, true);
                    return;
                }

                StepWrapper newStep = null;
                for (int stepIndex = 0; stepIndex < addStepCount; stepIndex++)
                {
                    newStep = StepWrapper.GenerateNewStep(stepTemplate, CurrentMode);
                    // 改造下newStep,将素材值赋给newStep 
                    changeStepFromMaterial(materialAst.TongdaoArray, stepIndex, sameTDIndexList, newStep);
                    // 使用后插法：避免当前无数据的情况下调用素材失败
                    lsWrapper.InsertStep(lsWrapper.CurrentStep - 1, newStep, false);
                }

                if (isMultiMode)
                {
                    foreach (int lightIndex in selectedIndexList)
                    {
                        if (lightIndex != selectedIndex)
                        {
                            // 编组模式下，依然使用上面的步骤来插入素材。
                            for (int stepIndex = 0; stepIndex < addStepCount; stepIndex++)
                            {
                                newStep = StepWrapper.GenerateNewStep(getSelectedLightStepTemplate(lightIndex), CurrentMode);
                                changeStepFromMaterial(materialAst.TongdaoArray, stepIndex, sameTDIndexList, newStep);
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
                            newStep = StepWrapper.GenerateNewStep(getSelectedLightStepTemplate(lightIndex), CurrentMode);
                            getSelectedLightStepWrapper(lightIndex).InsertStep(getSelectedLightStepWrapper(lightIndex).CurrentStep - 1, newStep, false);
                        }
                    }
                }

                refreshStep();
            }
            // 选择覆盖时的操作：后插法
            //（当前步也要被覆盖，除非没有当前步-》totalStep == currentStep == 0）
            else if (insMethod == InsertMethod.COVER)
            {
                int finalStep = (currentStep - 1) + addStepCount;// finalStep为覆盖后最后一步的序列，而非所有步的数量

                if (finalStep > MAX_STEP)
                {
                    SetNotice("素材步数超过当前模式剩余步数，无法调用；可选择其他位置覆盖", true, true);
                    return;
                }

                StepWrapper stepTemplate = GetCurrentStepTemplate();
                IList<MaterialIndexAst> sameTDIndexList = getSameTDIndexList(materialAst.TdNameList, stepTemplate.TongdaoList);
                if (sameTDIndexList.Count == 0)
                {
                    SetNotice("该素材与当前灯具不匹配，无法调用", true, true);
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
                    changeStepFromMaterial(materialAst.TongdaoArray, materialStepIndex, sameTDIndexList, lsWrapper.StepWrapperList[stepIndex]);
                    //newStep = lsWrapper.StepWrapperList[stepIndex];
                }

                if (isMultiMode)
                {
                    foreach (int lightIndex in selectedIndexList)
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
                                changeStepFromMaterial(materialAst.TongdaoArray, materialStepIndex, sameTDIndexList, getSelectedLightSelectedStepWrapper(lightIndex, stepIndex));
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
                    if (materialTDNameList[materialTDIndex].Equals(tongdaoList[currentTDIndex].TongdaoCommon.TongdaoName))
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

        //MARK 0. MainFormBase() 获取各种当前（步数、灯具）等的辅助方法 
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
        ///  辅助方法：供《多步联调》内修改部分步数值时使用；
        /// </summary>
        /// <param name="tdIndex"></param>
        /// <param name="stepIndex"></param>
        public void SetTdStepValue(int selectedLightIndex, int tdIndex, int stepIndex, int stepValue, bool isJumpStep)
        {
            //判断传入的 stepIndex是否在范围内，超过的话，直接return( tdIndex 不需验证：因为是mainForm传过去的)
            LightStepWrapper lsWrapper = getSelectedLightStepWrapper(selectedLightIndex);
            if (stepIndex >= lsWrapper.TotalStep)
            {
                return;
            }

            // 编组模式 且 所选灯具在当前的编组内，将值赋给每个编组的灯具中
            if (isMultiMode && selectedIndexList.Contains(selectedLightIndex))
            {
                foreach (int lightIndex in selectedIndexList)
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
            if (isJumpStep)
            {
                getSelectedLightStepWrapper(selectedLightIndex).CurrentStep = stepIndex + 1;
                if (!isMultiMode && selectedIndex != selectedLightIndex)
                {
                    selectedIndex = selectedLightIndex;
                    generateLightData(); //SetTdStepValue
                    return; //generateLightData()代码中已包含RefreshStep，故如果运行后可直接return；不return的则由最后的RefreshStep()来收尾
                }
            }
            refreshStep();
        }

        /// <summary>
        /// 辅助方法：供《灯具编组Form》使用：成功为true(跳过或真的编组)，失败false（无法编组）
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool MakeGroup(GroupAst group)
        {
            // 当编组名为空时，直接返回true，表示跳过存储编组；
            if (string.IsNullOrWhiteSpace(group.GroupName))
            {
                return true;
            }

            if (GroupList == null)
            {
                MessageBox.Show("GroupList==null");
                return false;
            }
            if (!GroupAst.CheckGroupName(GroupList, group.GroupName))
            {
                MessageBox.Show("编组名已存在");
                return false;
            }

            GroupList.Add(group);
            refreshGroupPanels();
            return true;
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
        ///辅助方法：统一设值的辅助方法
        /// </summary>
        /// <param name="where"></param>
        /// <param name="value"></param>
        protected void copyUnifyValueToAll(int stepNum, EnumUnifyWhere where, int value)
        {

            LightStepWrapper mainLSWrapper = getSelectedLightStepWrapper(selectedIndex); //取出组长			
            int tdCount = getCurrentLightWrapper().StepTemplate.TongdaoList.Count;

            foreach (int index in selectedIndexList)
            {
                if (getSelectedLightStepWrapper(index).StepWrapperList[stepNum - 1] != null)
                {
                    for (int tdIndex = 0; tdIndex < tdCount; tdIndex++)
                    {
                        switch (where)
                        {
                            case EnumUnifyWhere.SCROLL_VALUE:
                                getSelectedLightStepWrapper(index).StepWrapperList[stepNum - 1].TongdaoList[tdIndex].ScrollValue = value; break;
                            case EnumUnifyWhere.CHANGE_MODE:
                                getSelectedLightStepWrapper(index).StepWrapperList[stepNum - 1].TongdaoList[tdIndex].ChangeMode = value; break;
                            case EnumUnifyWhere.STEP_TIME:
                                getSelectedLightStepWrapper(index).StepWrapperList[stepNum - 1].TongdaoList[tdIndex].StepTime = value; break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 辅助方法：编组模式中，利用此方法，将当前步的一些《统一设置》的scrollValue值，设为编组的相关步的值。
        /// </summary>
        protected void copyStepToAll(int stepNum, EnumUnifyWhere where)
        {

            LightStepWrapper mainLSWrapper = getSelectedLightStepWrapper(selectedIndex); //取出组长
            int tdCount = getCurrentLightWrapper().StepTemplate.TongdaoList.Count;

            foreach (int lightIndex in selectedIndexList)
            {
                if (getSelectedLightStepWrapper(lightIndex).StepWrapperList[stepNum - 1] != null)
                {
                    for (int tdIndex = 0; tdIndex < tdCount; tdIndex++)
                    {
                        switch (where)
                        {
                            case EnumUnifyWhere.SCROLL_VALUE:
                                getSelectedLightStepWrapper(lightIndex).StepWrapperList[stepNum - 1].TongdaoList[tdIndex].ScrollValue = mainLSWrapper.StepWrapperList[stepNum - 1].TongdaoList[tdIndex].ScrollValue; break;
                            case EnumUnifyWhere.CHANGE_MODE:
                                getSelectedLightStepWrapper(lightIndex).StepWrapperList[stepNum - 1].TongdaoList[tdIndex].ChangeMode = mainLSWrapper.StepWrapperList[stepNum - 1].TongdaoList[tdIndex].ChangeMode; break;
                            case EnumUnifyWhere.STEP_TIME:
                                getSelectedLightStepWrapper(lightIndex).StepWrapperList[stepNum - 1].TongdaoList[tdIndex].StepTime = mainLSWrapper.StepWrapperList[stepNum - 1].TongdaoList[tdIndex].StepTime; break;
                            case EnumUnifyWhere.ALL:
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
        ///  辅助方法：（供GroupForm调用）检查当前的所有选中灯具的所有步数，是否一致。--》只需都和第一个灯进行对比，稍有不同，即不通过。
        /// </summary>
        /// <returns></returns>
        public bool CheckSameStepCounts()
        {
            int firstIndex = selectedIndexList[0];
            int firstStepCounts = getSelectedLightTotalStep(firstIndex);
            foreach (int index in selectedIndexList)
            {
                int tempStepCounts = getSelectedLightTotalStep(index);
                if (tempStepCounts != firstStepCounts)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 辅助方法：(供同步模式使用)检查是否所有灯具的步数都一致，若有不同，直接返回false
        /// </summary>
        protected bool CheckAllSameStepCounts()
        {
            if (LightAstList == null || LightAstList.Count == 0)
            {
                MessageBox.Show("当前工程无灯具，检查灯具步数是否一致的操作无意义。");
                return false;
            }

            int firstStepCounts = getSelectedLightTotalStep(0);
            for (int lightIndex = 1; lightIndex < LightAstList.Count; lightIndex++)
            {
                int tempStepCounts = getSelectedLightTotalStep(lightIndex);
                if (tempStepCounts != firstStepCounts)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 辅助方法：获取当前工程未选中灯具=》主动判断是编组还是单灯模式。
        /// </summary>
        /// <returns></returns>
        protected IList<int> getNotSelectedIndices()
        {
            IList<int> allIndices = new List<int>();
            for (int lightIndex = 0; lightIndex < LightWrapperList.Count; lightIndex++)
            {
                if (isMultiMode)
                {
                    if (!selectedIndexList.Contains(lightIndex))
                        allIndices.Add(lightIndex);
                }
                else
                {
                    if (selectedIndex != lightIndex)
                    {
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
        /// <param name="insMethod"></param>
        public void MultiStepPaste(InsertMethod insMethod)
        {
            UseMaterial(TempMaterialAst, insMethod, false);
        }

        /// <summary>
        /// 辅助方法：由内存读取 IList<TongdaoWrapper> 拿出相关通道的TongdaoWrapper，取的是 某一场景 某一模式 某一通道 的所有步信息
        /// </summary>
        public IList<TongdaoWrapper> GetSMTDList(DB_ChannelPK pk)
        {
            IList<TongdaoWrapper> tdList = new List<TongdaoWrapper>();
            //MARK 只开单场景：10.1 GetSMTDList() 的实现改动，若是已加载的场景则从内存读数据
            if (sceneLoadArray[pk.Scene])
            {
                int selectedLightIndex = getLightIndex(pk.ChannelID);
                if (selectedLightIndex == -1)
                {
                    return tdList;
                }

                if (LightWrapperList[selectedLightIndex].LightStepWrapperList[pk.Scene, pk.Mode] != null
                        && LightWrapperList[selectedLightIndex].LightStepWrapperList[pk.Scene, pk.Mode].StepWrapperList != null)
                {
                    IList<StepWrapper> stepWrapperList = LightWrapperList[selectedLightIndex].LightStepWrapperList[pk.Scene, pk.Mode].StepWrapperList;

                    for (int step = 0; step < stepWrapperList.Count; step++)
                    {
                        if (stepWrapperList[step].TongdaoList != null && stepWrapperList[step].TongdaoList.Count > 0)
                        {
                            TongdaoWrapper tw = stepWrapperList[step].TongdaoList[pk.ChannelID - LightAstList[selectedLightIndex].StartNum];
                            tdList.Add(tw);
                        }
                    }
                }
            }
            //MARK 只开单场景：10.2 GetSMTDList() 的实现改动：若非已加载的场景则从DB读数据
            else
            {
                DB_Channel channel = channelDAO.GetByPK(pk);
                if (channel != null)
                {
                    string[] stepArray = channel.Value.Split(',');
                    for (int step = 0; step < stepArray.Length; step++)
                    {
                        string[] valueArray = stepArray[step].Split('-');
                        tdList.Add(new TongdaoWrapper()
                        {
                            ChangeMode = int.Parse(valueArray[0]),
                            ScrollValue = int.Parse(valueArray[1]),
                            StepTime = int.Parse(valueArray[2])
                        });
                    }
                }
            }
            return tdList;
        }

        /// <summary>
        /// 遍历LightAstList,找到相关的灯具所在的位置
        /// </summary>
        /// <param name="channelID"></param>
        /// <returns></returns>
        private int getLightIndex(int channelID)
        {
            for (int lightIndex = 0; lightIndex < LightAstList.Count; lightIndex++)
            {
                LightAst la = LightAstList[lightIndex];
                if (la.StartNum <= channelID && la.EndNum >= channelID)
                {
                    return lightIndex;
                }
            }
            return -1;
        }

        #region projectPanel相关

        /// <summary>
        /// 辅助方法：点击《新建工程》
        /// </summary>
        protected void newProjectClick()
        {
            //MARK 只开单场景：17.1 新建工程前，申请保存工程
            if (!RequestSaveProject(LanguageHelper.TranslateSentence("新建工程前，是否保存当前工程？")))
            {
                return;
            }
            new NewForm(this).ShowDialog();
        }

        /// <summary>
        ///  辅助方法: 通过工程名，新建工程; 主要通过调用InitProject(),但在前后加了鼠标特效的处理。（此操作可能耗时较久，故在方法体前后添加鼠标样式的变化）
        /// </summary>
        /// <param name="projectName">工程名</param>
        public void NewProject(string projectName, int sceneIndex)
        {
            Cursor = Cursors.WaitCursor;
            InitProject(projectName, sceneIndex, true);
            //MARK 只开单场景：01.2 NewProject时，要frameLoadArray[selectedFrame]=true；
            sceneLoadArray[sceneIndex] = true;
            SetNotice("成功新建工程，请为此工程添加灯具。", true, true);
            Cursor = Cursors.Default;
            editLightList();  //新建工程后，灯具列表一定是空的，故直接弹出《添加灯具》
        }

        /// <summary>
        /// 基类辅助方法InitProject(打开或新建工程会用到)：①ClearAllData()；②设置内部的一些工程路径及变量；③初始化数据库
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="isNew"></param>
        public void InitProject(string projectName, int selectedSceneIndex, bool isNew)
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
                File.Copy(Application.StartupPath + @"\groupList.ini", groupIniPath);
            }
            GroupList = GroupAst.GenerateGroupList(groupIniPath);
            refreshGroupPanels(); //InitProject()

            // 2.创建数据库:初始化，让所有的DAO指向new xxDAO，避免连接到错误的数据库(已打开过旧的工程的情况下)；
            //211008 newDbFilePath的初始化
            dbFilePath = currentProjectPath + @"\newData.db3";
            lightDAO = new LightDAO(dbFilePath, isEncrypt);
            fineTuneDAO = new FineTuneDAO(dbFilePath, isEncrypt);
            channelDAO = new ChannelDAO(dbFilePath, isEncrypt);

            // 若为新建，则初始化db的table(随机使用一个DAO即可初始化）
            if (isNew)
            {
                BaseDAO<object>.CreateSchema(dbFilePath, false);
            }

            //MARK 只开单场景：04.0 InitProject()内 ①修改当前场景；②初始化frameSaveArray、frameLoadArray
            //   --> 都先设为false;并将frameSaveArray[selectedFrameIndex]为true，因为只要打开了场景（New或Open）其frameSaveArray[selectedFrame]一定要设为true的！
            //   -->（原则：当前打开的场景点击保存时一定要保存，因为在此处可能进行更改数据）
            //MARK 重构BuildLightList：初始化工程（打开或新建工程）时，一定会选一个初始的场景，故可以将frameLoadArray[selectedFrame]也设为true）
            changeCurrentScene(selectedSceneIndex);
            sceneSaveArray = new bool[SceneCount];
            sceneLoadArray = new bool[SceneCount];
            for (int sceneIndex = 0; sceneIndex < SceneCount; sceneIndex++)
            {
                sceneSaveArray[sceneIndex] = sceneIndex == selectedSceneIndex;
                sceneLoadArray[sceneIndex] = sceneIndex == selectedSceneIndex;
            }

            enableProjectRelative(true);    // InitProject()时设置，各按键是否可用
        }

        /// <summary>
        /// 辅助方法： 清空相关的所有数据（关闭工程、新建工程、打开工程都会用到）
        /// -- 子类中需有针对该子类内部自己的部分代码（如重置listView或禁用stepPanel等）
        /// </summary>
        protected virtual void clearAllData()
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
            autosetEnabledPlayAndRefreshPic();  //clearAllData()
            refreshGroupPanels(); //clearAllData()

            showStepLabelMore(0, 0); //clearAllData
            showTDPanels(null); // clearAllData
            showLightsInfo(); //clearAllData
        }


        /// <summary>
        /// 辅助方法：点击《打开工程》
        /// </summary>
        protected void openProjectClick()
        {
            //MARK 只开单场景：17.2 打开工程前，申请保存工程
            if (!RequestSaveProject(LanguageHelper.TranslateSentence("打开工程前，是否保存当前工程？")))
            {
                return;
            }
            new OpenForm(this, CurrentScene, currentProjectName).ShowDialog();
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

            //MARK1124：OpenProject 内当更改过工作目录后，需要刷新软件内置的灯具图片列表；
            if (savePath != SavePath)
            {
                SavePath = savePath;
                RefreshLightImageList(); // OpenProject
            }

            // 0.初始化
            InitProject(projectName, sceneIndex, false);

            // 设置listView右键菜单中读取位置配置的可用项	
            autoEnableSLArrange(); //OpenProject

            // 把各数据库表的内容填充进来。
            IList<DB_Light> dbLightList = getDBLightList();

            //10.17 此处添加验证 : 如果是空工程(无任何灯具可认为是空工程)，后面的数据无需读取。
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
                //MARK 重构BuildLightList：原来OpenProject内用BuildLightList() --> 现把相关代码都放在方法块内
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

                enterSyncMode(false); //需要退出同步模式
                enableProjectRelative(true);    //OpenProject内设置
                autosetEnabledPlayAndRefreshPic();  //OpenProject
                reBuildLightListView();

                //MARK 只开单场景：07.0 generateFrameData():在OpenProject内调用                
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

        /// <summary>
        /// 辅助方法：点击《调用场景》
        /// </summary>
        protected void copySceneClick()
        {
            //MARK 只开单场景：09.0 调用场景前，增加场景数量不够时情况的判断（==1）（0场景的情况软件不会打开，无需考虑）--》不进入useFrameForm；
            if (SceneCount == 1)
            {
                SetNotice("软件中只存在一种场景，无法使用调用场景功能。", true, true);
                return;
            }

            //MARK 只开单场景：09.1 调用场景前，增加当前工程没有灯具数据时，不能使用此功能的校验
            if (LightAstList == null || LightAstList.Count == 0)
            {
                SetNotice("当前工程没有灯具，无法使用调用场景功能。", true, true);
                return;
            }
            new CopySceneForm(this, CurrentScene).ShowDialog();
        }

        /// <summary>
        /// 辅助方法：点击《保存场景》
        /// </summary>
        protected void saveSceneClick()
        {
            SetNotice("正在保存场景,请稍候...", false, true);
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
                saveSceneChannels(CurrentScene);
                saveAllGroups();
            }

            SetNotice(LanguageHelper.TranslateSentence("成功保存场景：") + AllSceneList[CurrentScene], true, false);
            setBusy(false);
        }

        /// <summary>
        /// 辅助方法：点击《保存工程》
        /// </summary>
        protected void saveProjectClick()
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
        /// 辅助方法：保存编组数据（保存工程 、保存场景、更改灯具列表后都需要执行）
        /// </summary>
        protected void saveAllGroups()
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

        /// <summary>
        ///  辅助方法：右键点击《保存工程》将当前工程的相关文件，导出为Source.zip文件
        /// </summary>
        protected void exportSourceClick()
        {
            exportFolderBrowserDialog.Description = LanguageHelper.TranslateSentence("即将为您导出当前工程的源文件，并压缩为Source.zip；请选择源文件保存目录。");
            DialogResult dr = exportFolderBrowserDialog.ShowDialog();
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            string exportPath = exportFolderBrowserDialog.SelectedPath;
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
        /// 辅助方法：点击《导出工程》
        /// </summary>
        protected void exportProjectClick()
        {
            if (LightAstList == null || LightAstList.Count == 0)
            {
                MessageBox.Show(LanguageHelper.TranslateSentence("当前工程没有灯具，无法导出工程。请添加灯具后再使用本功能。"));
                return;
            }
            DialogResult dr = MessageBox.Show(
                LanguageHelper.TranslateSentence("请确保工程已保存后再进行导出，否则可能导出非预期效果。确定现在导出吗？"),
                LanguageHelper.TranslateSentence("导出工程？"),
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question);
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            exportFolderBrowserDialog.Description = LanguageHelper.TranslateSentence("请选择要导出的目录，程序会自动在选中位置创建\"CSJ\"文件夹；并在导出成功后打开该目录。若工程文件过大，导出过程中软件可能会卡住，请稍等片刻即可。");
            dr = exportFolderBrowserDialog.ShowDialog();
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            exportPath = exportFolderBrowserDialog.SelectedPath + @"\CSJ";
            DirectoryInfo di = new DirectoryInfo(exportPath);
            if (di.Exists && (di.GetFiles().Length + di.GetDirectories().Length != 0))
            {
                dr = MessageBox.Show(
                    LanguageHelper.TranslateSentence("检测到目标文件夹不为空，是否覆盖？"),
                    LanguageHelper.TranslateSentence("覆盖工程？"),
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }
            }

            SetNotice("正在导出工程，请稍候...", false, true);
            setBusy(true);

            //211104 导出工程
            CSJProjectBuilder.GetInstance().BuildProjects(this, ExportProjectCompleted, ExportProjectError, ExportProjectProgress);
        }

        /// <summary>
        /// MARK 导出单场景具体实现 0.exportFrameClick()方法体
        /// 辅助方法：右键点击《导出工程》->即导出当前场景
        /// </summary>
        protected void exportSceneClick()
        {
            if (LightAstList == null || LightAstList.Count == 0)
            {
                MessageBox.Show(LanguageHelper.TranslateSentence("当前工程没有灯具，无法导出场景。请添加灯具后再使用本功能。"));
                return;
            }
            //MARK 导出单场景具体实现 1. 修改弹窗的提示
            DialogResult dr = MessageBox.Show(
                    LanguageHelper.TranslateSentence("请确保灯具列表未发生变化，并且与选择的已导出工程相比，只改动了当前场景的数据，否则可能产生错误的效果!\n确定现在导出工程（只修改当前场景数据）吗？"),
                    LanguageHelper.TranslateSentence("导出工程（只修改当前场景数据）？"),
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            //MARK 导出单场景具体实现 2. 修改打开文件夹对话框的提示
            exportFolderBrowserDialog.Description =
                LanguageHelper.TranslateSentence("请选择当前工程以前的导出目录（即CSJ文件夹）。导出工程（只修改当前场景数据）时，"
                                + "程序将只改动C" + (CurrentScene + 1) + ".bin、M" + (CurrentScene + 1) + ".bin、Config.bin及GradientData.bin文件。请稍等片刻即可。");
            dr = exportFolderBrowserDialog.ShowDialog();
            if (dr == DialogResult.Cancel)
            {
                return;
            }

            //MARK 导出单场景具体实现 3. 检测选中的文件夹不为空（数据数量不得为0），若此文件夹为空，则不应导出单场景
            exportPath = exportFolderBrowserDialog.SelectedPath;
            DirectoryInfo di = new DirectoryInfo(exportPath);
            if (!di.Exists || di.GetFiles().Length == 0)
            {
                MessageBox.Show(LanguageHelper.TranslateSentence("检测到目标文件夹为空，说明该文件夹并不存在已导出工程，请选中正确的已导出工程的文件夹（有一些bin文件）！"));
                return;
            }

            SetNotice("正在重新生成已导出工程的当前场景工程文件，请稍候...", false, true);
            setBusy(true);

            exportScene();
        }

        //MARK 导出单场景具体实现 4. 把选中文件夹内的所有数据拷到临时文件夹中（DataCache\Project\CSJ），拷贝前需要先清空目标文件夹；并逐一把所有CX.bin、MX.bin文件都拷贝过去		
        /// <summary>
        /// 辅助方法：当拷贝文件发生错误时，用递归的方法重新操作
        /// </summary>        
        private void exportScene()
        {
            try
            {
                FileUtils.ClearProjectData();
                string destPath = Application.StartupPath + @"\DataCache\Project\CSJ";
                for (int sceneIndex = 1; sceneIndex <= SceneCount; sceneIndex++)
                {
                    FileHelper.CopyFile(exportPath + @"\C" + sceneIndex + ".bin", destPath, true);
                    FileHelper.CopyFile(exportPath + @"\M" + sceneIndex + ".bin", destPath, true);
                }
            }
            catch (Exception ex)
            {
                DialogResult dialogResult = MessageBox.Show("拷贝工程文件到工作目录失败，原因为：\n" + ex.Message + "(可能是在短时间内进行了多次导出操作,系统尚未彻底释放资源)\n请稍等后点击《重试》或《取消》拷贝。",
                        "是否重试？",
                        MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Error);
                if (dialogResult == DialogResult.Retry)
                {
                    exportScene();
                }
                else
                {
                    //若点击取消，则直接把忙时设为false，因为不会再往下走了，没有机会进行更改操作了。
                    setBusy(false);
                    SetNotice("因发生异常，已取消导出工程(只修改当前场景数据)的操作。", false, true);
                }
                return; //只要出现异常，就一定要退出本方法；
            }

            //MARK 导出单场景具体实现 5. 调用维佳的生成单场景方法，将只生成CFrame.bin、MFrame.bin、Config.bin和GradientData.bin；（其余文件都是拷贝两次：先拷到工作目录，调用完成后再拷回导出目录）			
            //DOTO 211019 导出场景
            //DataConvertUtils.GetInstance().SaveSingleFrameFile(
            //    GetDBWrapper(), this, GlobalIniPath, CurrentScene, ExportProjectCompleted, ExportProjectError, ExportProjectProgress);
        }

        /// <summary>
        /// 辅助方法：点击《关闭工程》
        /// </summary>
        protected void closeProjectClick()
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
        /// 辅助方法：拷贝已生成工程到指定目录（并在此期间生成并压缩源文件），在SaveProjectFile成功后回调
        /// </summary>
        /// <param name="exportPath"></param>
        /// <param name="success">SaveProjectFile成功为true，失败为false</param>
        protected void copyProject()
        {
            try
            {
                FileUtils.ExportProjectFile(exportPath);
            }
            catch (Exception ex)
            {
                DialogResult dialogResult =
                    MessageBox.Show("拷贝工程文件失败，原因为：\n" + ex.Message + "\n请《取消(拷贝)》或在处理完成后点击《重试》。",
                    "是否重试？",
                    MessageBoxButtons.RetryCancel,
                    MessageBoxIcon.Error);
                if (dialogResult == DialogResult.Retry)
                {
                    copyProject(); //若点击重试，则再跑一遍本方法						
                }
                else
                {
                    //若点击取消，则直接把忙时设为false，因为不会再往下走了;
                    setBusy(false);
                }
                return; //只要出现异常，就一定要退出本方法；
            }

            SetNotice("正在源文件,请稍候...", false, true);
            // 先生成Source文件夹到工作目录，再把该文件夹压缩到导出文件夹中			
            if (GenerateSourceZip(exportPath + @"\Source.zip"))
            {
                SetNotice("已成功压缩源文件(Source.zip)。", false, true);
            }
            else
            {
                SetNotice("工程源文件生成失败。", false, true);
            }

            DialogResult dr = MessageBox.Show(
                    LanguageHelper.TranslateSentence("导出工程成功,是否打开导出文件夹？"),
                    LanguageHelper.TranslateSentence("打开导出文件夹？"),
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                System.Diagnostics.Process.Start(exportPath);
            }

            setBusy(false);
            SetNotice("导出工程成功", false, true);
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

        public void CopyOtherScene(int sourceScene, int destScene, bool copyNormal, bool copySound)
        {
            if (!copyNormal && !copySound)
            {
                MessageBox.Show("请选择要复制的模式！");
            }

            //MARK 只开单场景：09.2 调用场景时，若调用的是未打开的场景，则需先打开（载入到内存中）
            if (!sceneLoadArray[sourceScene]) generateSceneData(sourceScene);
            if (!sceneLoadArray[destScene]) generateSceneData(destScene);

            //MARK 只开单场景：09.3 调用场景时，把被调用场景的灯具数据，深复制到当前场景中来（只复制当前模式）
            if (LightWrapperList != null && LightWrapperList.Count != 0)
            {
                foreach (LightWrapper lightWrapper in LightWrapperList)
                {
                    if (copyNormal)
                    {
                        lightWrapper.LightStepWrapperList[destScene, (int)EnumMode.NORMAL]
                            = LightStepWrapper.GenerateLightStepWrapper(lightWrapper.LightStepWrapperList[sourceScene, CurrentMode], lightWrapper.StepTemplate);
                    }
                    if (copySound)
                    {
                        lightWrapper.LightStepWrapperList[destScene, (int)EnumMode.NORMAL]
                          = LightStepWrapper.GenerateLightStepWrapper(lightWrapper.LightStepWrapperList[sourceScene, CurrentMode], lightWrapper.StepTemplate);
                    }
                }
            }

            enterSyncMode(false); //UseOtherForm
            refreshStep();
            SetNotice("成功将【" + AllSceneList[sourceScene] + "】复制到【" + AllSceneList[destScene] + "】", true, false);
        }


        /// <summary>
        /// MARK 只开单场景：17.0 请求保存工程的辅助方法：RequestSaveProject（string msg,bool isAfterUpdateLightList)
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

        #endregion

        #region 菜单栏-工程相关

        /// <summary>
        /// 辅助方法：打开《灯具列表》
        /// </summary>
        protected void editLightList()
        {
            new LightsForm(this).ShowDialog();
        }

        /// <summary>
        /// 辅助方法：打开《全局设置》
        /// </summary>
        protected void globalSetClick()
        {
            new GlobalSetForm(this).ShowDialog();
        }

        /// <summary>
        /// 辅助方法：点击《工程升级》
        /// </summary>
        protected void projectDownloadClick()
        {
            // Mark3.0413  projectUpdateClick()-disConnect
            if (IsDeviceConnected)
            {
                stopDebug();
                new ProjectDownloadForm(this).ShowDialog();
            }
        }

        #endregion

        #region 菜单栏-非工程相关

        /// <summary>
        /// 辅助方法：点击《灯库软件》
        /// </summary>
        protected void openLightEditor()
        {
            if (IsPreviewing)
            {
                PreviewButtonClick(null);
            }
            new LightEditor.LightEditorForm(this).ShowDialog();
        }

        /// <summary>
        /// 辅助方法：点击《硬件配置》
        /// </summary>
        protected void hardwareSetButtonClick()
        {
            if (IsDeviceConnected)
            {
                stopDebug();
                new HardwareSetForm(this).ShowDialog();
            }
        }

        /// <summary>
        /// 辅助方法：点击《外设配置》
        /// </summary>
        protected void toolButtonClick()
        {
            if (IsDeviceConnected)
            {
                stopDebug();
                new ToolsForm(this,  (CurrentProtocol==-1 || CurrentProtocol == xlsWorkbook.NumberOfSheets)?0:CurrentProtocol ).ShowDialog();
            }
        }

        /// <summary>
        /// 辅助方法：点击《时序器配置》
        /// </summary>
        protected void sequencerButtonClick()
        {
            if (IsDeviceConnected)
            {
                stopDebug();
                new SequencerForm(this).ShowDialog();
            }
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
        /// 辅助方法：点击《音频链表》
        /// </summary>
        protected void soundListButtonClick()
        {
            new SKForm(this).ShowDialog();
        }

        /// <summary>
        /// 辅助方法：点击《上一步》
        /// </summary>
        protected void backStepClick()
        {
            int currentStep = getCurrentStep();
            chooseStep(currentStep > 1 ? currentStep - 1 : getCurrentTotalStep());  //backStepClick
        }

        /// <summary>
        /// 辅助方法：点击《下一步》
        /// </summary>
        protected void nextStepClick()
        {
            int currentStep = getCurrentStep();
            int totalStep = getCurrentTotalStep();
            chooseStep(currentStep < totalStep ? currentStep + 1 : 1); //nextStepClick
        }

        /// <summary>
        /// 辅助方法：左右键点击《插入步》
        /// </summary>
        /// <param name="mouseButton"></param>
        protected void InsertStepClick(MouseButtons mouseButton)
        {
            if (!new MouseButtons[] { MouseButtons.Left, MouseButtons.Right }.Contains(mouseButton)) return; // 通用判断语句：非左右键点击时，return

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

            bool insertBefore = mouseButton == MouseButtons.Right && getCurrentStep() != 0;   //当点击右键且当前有步时，才能采取前插；否则一律后插

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
        /// 辅助方法：左键点击《追加步》
        /// </summary>
        protected void appendStepClick(MouseButtons mouseButton)
        {
            if (!new MouseButtons[] { MouseButtons.Left, MouseButtons.Right }.Contains(mouseButton)) return; // 通用判断语句：非左右键点击时，return

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
            if (mouseButton == MouseButtons.Left)
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
        /// 辅助方法：左键点击《删除步》
        ///  左键 1.获取当前步，当前步对应的stepIndex
        ///	       2.通过stepIndex，DeleteStep(index);
        ///		   3.获取新步(step删除后会自动生成新的)，并重新渲染stepLabel和vScrollBars
        ///  右键 新建DeleteStepForm并显示
        /// </summary>
        protected void deleteStepClick(MouseButtons mouseButton)
        {
            if (!new MouseButtons[] { MouseButtons.Left, MouseButtons.Right }.Contains(mouseButton)) return; // 通用判断语句：非左右键点击时，return

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
            if (mouseButton == MouseButtons.Left)
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
        /// 辅助方法：删除所有步（根据当前状态来决定删除哪些灯具的数据）
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

        /// <summary>
        /// 辅助方法：点击《复制步》
        /// 1.从项目中选择当前灯的当前步，(若当前步为空，则无法复制），把它赋给tempStep数据。
        /// 2.若复制成功，则《粘贴步》按钮可用
        /// </summary>
        protected void copyStepClick()
        {
            if (getCurrentStepWrapper() == null)
            {
                SetNotice("当前步数据为空，无法复制", true, true);
                return;
            }
            tempStep = getCurrentStepWrapper();
            refreshStep(); // 主要作用是刷新按键（粘贴步可用）
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
                SetNotice("当前步数据为空，无法粘贴步。", true, true);
                return;
            }
            if (CurrentMode != tempStepMode)
            {
                SetNotice("不同模式下无法复制步。", true, true);
                return;
            }
            if (currentStep.StepCommon.LightFullName != tempStep.StepCommon.LightFullName)
            {
                SetNotice("不同类型灯具无法复制步。", true, true);
                return;
            }

            // 2.逐一将TongdaoList的某些数值填入tempStep中，而非粗暴地将currentStep 设为tempStep
            for (int i = 0; i < tempStep.TongdaoList.Count; i++)
            {
                currentStep.TongdaoList[i].ScrollValue = tempStep.TongdaoList[i].ScrollValue;
                currentStep.TongdaoList[i].ChangeMode = tempStep.TongdaoList[i].ChangeMode;
                currentStep.TongdaoList[i].StepTime = tempStep.TongdaoList[i].StepTime;
            }


            //3.如果是编组模式，则需要在复制步之后处理下每个灯具的信息
            if (isMultiMode)
            {
                copyStepToAll(getCurrentStep(), EnumUnifyWhere.ALL);
            }

            //4.刷新当前步
            refreshStep();
        }

        /// <summary>
        /// 辅助方法：点击《保存素材》
        /// </summary>
        protected void saveMaterialClick()
        {
            LightAst la = LightAstList[selectedIndex];
            if (la == null)
            {
                MessageBox.Show(LanguageHelper.TranslateSentence("未选中灯具，无法保存素材。"));
                return;
            }

            MaterialSaveForm materialForm = new MaterialSaveForm(this, getCurrentLightStepWrapper().StepWrapperList, CurrentMode, la, getCurrentStep());
            if (materialForm != null && !materialForm.IsDisposed)
            {
                materialForm.ShowDialog();
            }
        }

        /// <summary>
        ///  辅助方法：点击《使用素材》
        /// </summary>
        protected void useMaterialClick()
        {
            // 若正在预览，则先停止预览
            if (IsPreviewing)
            {
                PreviewButtonClick(null);
            }

            if (materialUseForm == null)
            {
                materialUseForm = new MaterialUseForm(this);
            }
            materialUseForm.ShowDialog();
        }

        /// <summary>
        ///  辅助方法：点击《进入|退出同步》
        /// </summary>
        protected void syncButtonClick()
        {
            // 如果当前已经是同步模式，则退出同步模式，这比较简单，不需要进行任何比较，直接操作即可。
            if (isSyncMode)
            {
                enterSyncMode(false); //syncButtonClick				
            }
            else
            {
                // 异步时，要切换到同步模式，需要先进行检查。
                if (CheckAllSameStepCounts())
                {
                    enterSyncMode(true); //syncButtonClick				
                }
                else
                {
                    SetNotice("当前场景所有灯具步数不一致，无法进入同步模式。", true, true);
                }
            }
        }

        /// <summary>
        ///辅助方法：点击《多步复用》
        /// </summary>
        protected void multiplexButtonClick()
        {
            if (LightWrapperList == null || LightWrapperList.Count == 0)
            {
                MessageBox.Show("当前工程没有灯具，无法使用多步复用功能。");
                return;
            }
            int totalStep = getCurrentTotalStep();
            if (totalStep == 0)
            {
                MessageBox.Show("灯具没有步数，无法使用多步复用功能。");
                return;
            }

            // selectedIndices2 只用在非同步状态时，故可以在同步状态下传入null
            IList<int> selectedIndices2 = null;
            if (!isSyncMode)
            {
                if (!isMultiMode)
                {
                    selectedIndices2 = new List<int>() { selectedIndex };
                }
                else
                {
                    selectedIndices2 = selectedIndexList;
                }
            }

            new MultiplexForm(this, LightAstList, totalStep, isSyncMode, selectedIndices2).ShowDialog();
        }

        /// <summary>
        /// 辅助方法： 改变了模式和场景后的操作		
        /// </summary>
        protected void changeSceneMode()
        {
            // 9.2 不可让selectedIndex为-1  , 否则会出现数组越界错误
            if (selectedIndex == -1)
            {
                return;
            }

            /// 复位同步状态为false
            enterSyncMode(false);

            //最后都要用上RefreshStep()
            refreshStep();

            MAX_STEP = IniHelper.GetSystemCount(CurrentMode == 0 ? "maxStep" : "maxStepSound", 0);
            if (MAX_STEP == 0)
            {
                MAX_STEP = CurrentMode == 0 ? 100 : 300;
            }
        }

        /// <summary>
        /// 事件：切换场景选项
        /// </summary>
        /// <param name="sender"></param>
        protected void sceneChanged(int newScene)
        {
            setBusy(true);
            SetNotice("正在切换场景,请稍候...", false, true);
            endview(); // sceneSelectedChanged (只要更改了场景，直接结束预览)

            DialogResult dr = MessageBox.Show(
                LanguageHelper.TranslateSentence("切换场景前，是否保存之前场景：") + AllSceneList[CurrentScene] + "?",
                LanguageHelper.TranslateSentence("保存场景?"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                saveSceneClick();
                //MARK 只开单场景：06.0.1 切换场景时，若选择保存之前场景，则frameSaveArray设为false，意味着以后不需要再保存了。
                sceneSaveArray[CurrentScene] = false;
            }

            CurrentScene = newScene;
            //MARK 只开单场景：06.1.1 更改场景时，只有frameLoadArray为false，才需要从DB中加载相关数据（调用generateFrameData）；若为true，则说明已经加载因而无需重复读取。
            if (!sceneLoadArray[CurrentScene])
            {
                generateSceneData(CurrentScene);
            }
            //MARK 只开单场景：06.2.1 更改场景后，需要将frameSaveArray设为true，表示当前场景需要保存
            sceneSaveArray[CurrentScene] = true;

            changeSceneMode();
            setBusy(false);
            SetNotice(LanguageHelper.TranslateSentence("成功切换为场景：") + AllSceneList[CurrentScene], false, false);
        }

        /// <summary>
        /// 辅助方法：切换模式选项
        /// </summary>
        /// <param name="sender"></param>
        protected void modeChanged(bool isSoundMode,
            ComboBox[] tdCMComboBoxes,
            NumericUpDown[] tdStepTimeNumericUpDowns,
            Label thirdLabel)
        {
            SetNotice("正在切换模式", false, true);
            CurrentMode = isSoundMode ? 1 : 0;

            for (int tdPanelIndex = 0; tdPanelIndex < 32; tdPanelIndex++)
            {
                tdCMComboBoxes[tdPanelIndex].Items.Clear();
                tdCMComboBoxes[tdPanelIndex].Items.AddRange(CurrentMode == 0 ? normalCMArray : soundCMArray);
                tdStepTimeNumericUpDowns[tdPanelIndex].Visible = CurrentMode == 0;
            }
            thirdLabel.Visible = CurrentMode == 0;

            changeSceneMode();
            SetNotice("成功切换模式", false, true);
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
                generateSaPanels();

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

                enableStepPanel(true);
            }

            //3.更新当前灯具信息、并刷新步
            showLightsInfo(); //generateLightData()
            refreshStep();
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
            string tdName = lw.StepTemplate.TongdaoList[selectedTdIndex].TongdaoCommon.TongdaoName;

            SetNotice("打开【" + lightType + "(" + selectedIndex + ")" + "(" + selectedTdIndex + ":" + tdName + ")】的单通道多步联调窗口。", false, false);

            new DetailSingleForm(
                this,
                selectedIndex,
                selectedTdIndex,
                lw.LightStepWrapperList[CurrentScene, CurrentMode].StepWrapperList
            ).ShowDialog();
        }

        #endregion

        #region astPanel相关

        /// <summary>
        /// 辅助方法：复用多灯多步
        /// </summary>
        /// <param name="selectedIndices"></param>
        /// <param name="startStep"></param>
        /// <param name="endStep"></param>
        /// <param name="times">复用次数</param>
        /// <returns>复用成功返回null，否则返回相应的错误信息</returns>
        public string MultiplexSteps(IList<int> selectedIndices, int startStep, int endStep, int times)
        {
            // 检查剩余步数 是否大于 复用会添加的的步数
            int addStepCount = (endStep - startStep + 1) * times;
            if ((MAX_STEP - getSelectedLightTotalStep(0)) < addStepCount)
            {
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
                    if (isSyncMode)
                    {
                        for (int addStepIndex = 0; addStepIndex < addStepCount; addStepIndex++)
                        {
                            // 210528修复一个bug：下面这两个语句必须写在for循环内，否则会出现多步复用后面的所有步使用的是同一个对象！
                            StepWrapper lastStep = getSelectedLightLastStepWrapper(lightIndex);
                            StepWrapper newStep = StepWrapper.GenerateNewStep(lastStep, CurrentMode);
                            getSelectedLightStepWrapper(lightIndex).AddStep(newStep);
                        }
                    }
                }
            }

            refreshStep();
            return null;
        }

        /// <summary>
        /// 辅助方法：点击《多步调节》
        /// </summary>
        protected void multiButtonClick()
        {
            StepWrapper currentStep = getCurrentStepWrapper();
            if (currentStep == null || currentStep.TongdaoList == null || currentStep.TongdaoList.Count == 0)
            {
                SetNotice("请先选中任意步数，才能进行统一调整！", true, true);
                return;
            }

            LightAst la = LightAstList[selectedIndex];
            LightWrapper lw = LightWrapperList[selectedIndex];

            new MultiStepForm(this,
                getCurrentStep(),
                getCurrentTotalStep(),
                getCurrentStepWrapper(),
                CurrentMode,
                selectedIndex,
                lw.LightStepWrapperList[CurrentScene, CurrentMode].StepWrapperList
            ).ShowDialog();
        }

        /// <summary>
        /// 辅助方法：右键《多步调节》进入多步联调
        /// </summary>
        public void DetailMultiButtonClick(bool isOpenDMF)
        {
            if (getCurrentTotalStep() == 0)
            {
                SetNotice("当前灯具没有步数，无法使用多步联调。", true, true);
                return;
            }

            // 若tdDict不为空（意味着从DmaForm中被回传了），且是右键点击；则直接打开多步联调
            if (TdDict != null && isOpenDMF)
            {
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
        /// 辅助方法：点击《灯具编组|退出编组》
        /// </summary>
        /// <param name="lightsListView"></param>
        protected void groupButtonClick(ListView lightsListView)
        {

            if (isMultiMode)
            {
                GroupAst group = new GroupAst()
                {
                    LightIndexList = new List<int>() { selectedIndex },
                    CaptainIndex = 0,
                };
                EnterMultiMode(group, false); // groupButtonClick内退出编组
            }
            else
            {
                if (lightsListView.SelectedIndices.Count < 1)
                {
                    SetNotice("请选择至少一个灯具，否则无法进行编组。", true, true);
                    return;
                }
                if (!checkSameLights(lightsListView))
                {
                    SetNotice("未选中灯具或选中的灯具并非同一类型，无法进行编组；请再次选择后重试。", true, true);
                    return;
                }

                selectedIndexList = new List<int>();
                foreach (int item in lightsListView.SelectedIndices)
                {
                    selectedIndexList.Add(item);
                }
                new GroupForm(this, LightAstList, selectedIndexList).ShowDialog();
            }
        }

        /// <summary>
        /// 辅助方法：点击《进入编组按键》
        /// </summary>
        /// <param name="sender"></param>
        protected void groupInButtonClick(object sender, ListView lightsListView)
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
        /// 辅助方法：（供refreshMultiModeControls调用）通过一些全局变量，生成灯具地址字符串
        /// </summary>
        /// <returns></returns>
        protected string generateAddrStr()
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
        /// 辅助方法：点击《子属性》
        /// </summary>
        /// <param name="sender"></param>
        protected void saButtonClick(object sender)
        {
            if (getCurrentStepWrapper() == null)
            {
                SetNotice("当前无选中步，不可点击子属性按钮", false, true);
                return;
            }

            Button btn = (Button)sender;
            string[] btnTagArr = btn.Tag.ToString().Split('*');
            int tdIndex = int.Parse(btnTagArr[0]);
            int tdValue = int.Parse(btnTagArr[1]);

            getCurrentStepWrapper().TongdaoList[tdIndex].ScrollValue = tdValue;
            if (isMultiMode)
            {
                copyValueToAll(tdIndex, EnumUnifyWhere.SCROLL_VALUE, tdValue);
            }
            refreshStep();
        }

        #endregion

        #region playPanel相关

        private int clickTime = 0;
        /// <summary>
        /// 辅助方法：点击《设备连接》(左键直接弹出《(网络)设备连接Form》；右键为隐藏功能，单击六下才能触发，弹出《Dmx512连接(直连灯具)Form》)
        /// </summary>
        protected void connectButtonClick(MouseButtons mouseButton)
        {

            // 当点击左键时，直接弹出《网络连接》界面
            if (mouseButton == MouseButtons.Left)
            {
                if (ConnForm == null)
                {
                    ConnForm = new ConnectForm(this);
                }
                ConnForm.ShowDialog();
            }
            // 当点击右键时，1.未使用网络方式连接（暂时取消）；2.点击次数达到6次 ；满足这两个条件才弹出DMX512连接的界面
            else if (mouseButton == MouseButtons.Right)
            {
                //if (MyConnect == null ||  !IsDeviceConnected ) {
                clickTime++;
                if (clickTime == 6)
                {
                    if (dmxConnForm == null)
                    {
                        dmxConnForm = new DMX512ConnnectForm(this);
                    }
                    dmxConnForm.ShowDialog();
                    clickTime = 0;
                }
                //}			
            }
        }

        /// <summary>
        /// 辅助方法：传入相关对象， 进行设备连接；
        /// </summary>
        /// <param name="networkDeviceInfo"></param>
        /// <returns></returns>
        public bool Connect(NetworkDeviceInfo networkDeviceInfo)
        {
            if (MyConnect == null)
            {
                MyConnect = new NetworkConnect();
            }
            if (MyConnect.Connect(networkDeviceInfo))
            {
                refreshConnectedControls(true, IsPreviewing); //Connect() 内连接成功
                return true;
            }
            else
            {
                refreshConnectedControls(false, IsPreviewing); //Connect() 内连接失败
                return false;
            }
        }

        /// <summary>
        /// 辅助方法：断开连接
        /// </summary>
        public void DisConnect()
        {
            MyConnect.DisConnect();
            MyConnect = null;
            refreshConnectedControls(false, IsPreviewing); //DisConnect()
            SetNotice("设备已断开连接。", false, false);
        }

        /// <summary>
        ///  辅助方法：启动调试，基本只有在界面激活时用得到；
        /// </summary>
        protected void startDebug()
        {
            if (IsDeviceConnected)
            {
                SleepBetweenSend("Order : StartDebug", 1);
                networkPlayer.StartDebug(MyConnect, StartPreviewCompleted, StartPreviewError);
            }
        }

        /// <summary>
        /// 辅助方法：关闭调试
        /// </summary>
        protected void stopDebug()
        {
            if (IsDeviceConnected)
            {
                SleepBetweenSend("Order : StopDebug", 1);
                networkPlayer.StopDebug(delegate { }, delegate { });
            }
        }

        /// <summary>
        /// 辅助方法：为硬件发送新命令之前，先检查上次发送的时间，如果时间还不够长，把时间补足；
        /// </summary>
        /// <param name="orderName">命令名，方便调试</param>
        /// <param name="times">暂停时间的倍数（有些命令需要更长的等待时间）</param>
        public void SleepBetweenSend(string orderName, int times)
        {
            long pastTime = (DateTime.Now.ToUniversalTime().Ticks) / 10000 - LastSendTime;
            if (pastTime < ConnectForm.SEND_WAITTIME * times)
            {
                Thread.Sleep(ConnectForm.SEND_WAITTIME * times - (int)pastTime);
            }
            LastSendTime = (DateTime.Now.ToUniversalTime().Ticks) / 10000;
            Console.WriteLine(DateTime.Now + " ：" + orderName);
        }

        /// <summary>
        /// 辅助方法：单(多)灯单步发送DMX512帧数据
        /// </summary>
        public void OneStepPlay(byte[] stepBytes, MaterialAst material)
        {
            if (IsEnableOneStepPlay())
            {
                string prevStr = "正在调试单步数据 ";
                // 当stepBytes为空时，才需要根据material处理，否则直接使用此stepBytes播放即可；
                if (stepBytes == null)
                {

                    stepBytes = new byte[512];
                    int currentStep = getCurrentStep();

                    if (LightWrapperList != null && LightWrapperList.Count > 0)
                    {

                        for (int lightIndex = 0; lightIndex < LightWrapperList.Count; lightIndex++)
                        {
                            if (lightIndex == selectedIndex // 当前灯具一定会动
                                || isMultiMode && selectedIndexList.Contains(lightIndex)  // 编组模式下，组员也要动
                                || isKeepOtherLights  // 保持其它灯状态时，所有灯都要有数据
                                || isSyncMode  // 同步状态下，所有灯一起动
                                )
                            {
                                StepWrapper stepWrapper = getSelectedLightCurrentStepWrapper(lightIndex);
                                if (stepWrapper != null)
                                {
                                    foreach (TongdaoWrapper td in stepWrapper.TongdaoList)
                                    {
                                        stepBytes[td.TongdaoCommon.Address - 1] = (byte)td.ScrollValue;
                                    }
                                }
                            }
                        }

                        //MARK : 1221 OneStepPlay添加material后，实时生成(基于现有stepBytes进行处理)。
                        if (material != null)
                        {
                            for (int lightIndex = 0; lightIndex < LightWrapperList.Count; lightIndex++)
                            {
                                if (lightIndex == selectedIndex || isMultiMode && selectedIndexList.Contains(lightIndex))
                                {

                                    IList<TongdaoWrapper> tdList = getSelectedLightStepTemplate(lightIndex).TongdaoList;
                                    foreach (MaterialIndexAst mi in getSameTDIndexList(material.TdNameList, tdList))
                                    {
                                        stepBytes[tdList[mi.CurrentTDIndex].TongdaoCommon.Address - 1] = (byte)material.TongdaoArray[0, mi.MaterialTDIndex].ScrollValue;
                                    }
                                }
                            }
                        }
                    }

                    prevStr = LanguageHelper.TranslateWord("正在调试灯具：") + (selectedIndex + 1) + LanguageHelper.TranslateWord("，当前步：") + currentStep;
                }

                // 打印单步调试时的某些通道值（注意这个方法必须写在这个位置，否则可能直接无数据）
                string tdValueStr = "";
                if (tdValues != null && tdValues.Count > 0)
                {
                    tdValueStr += "【";
                    foreach (int tdIndex in tdValues)
                    {
                        tdValueStr += stepBytes[tdIndex] + " ";
                    }
                    tdValueStr = tdValueStr.TrimEnd(); // 去掉结尾的空格
                    tdValueStr += "】";
                }

                // 当使用网络连接设备时，用NetworkPlayTools播放；
                if (IsDeviceConnected)
                {
                    networkPlayer.SingleStepPreview(stepBytes, this);
                }
                // 当DMX512调试线也连接在灯具时，也可调试；（双规并行）
                if (IsDMXConnected)
                {
                    SerialPlayer.SingleStepPreview(stepBytes, this);
                }

                SetNotice(prevStr + tdValueStr, false, false);
            }
        }

        /// <summary>
        /// 辅助方法：结束预览
        /// </summary>
        protected void endview()
        {
            // 结束预览时，直接刷新步
            refreshStep();
        }

        /// <summary>
        /// 辅助方法：预览效果|停止预览
        /// </summary>
        public void PreviewButtonClick(MaterialAst material)
        {
            if (!IsOneMoreConnected())
            {
                SetNotice("尚未连接设备（或灯具），无法预览效果（或停止预览）。", true, true);
                refreshConnectedControls(false, false); //PreviewButtonClick()
                return;
            }

            // 停止预览
            if (IsPreviewing)
            {
                endview(); // PreviewButtonClick
                refreshConnectedControls(IsDeviceConnected, false); //PreviewButtonClick
                SetNotice("已结束预览,并恢复到实时调试模式。", false, true);
            }
            // 开始预览
            else
            {
                if (LightAstList == null || LightAstList.Count == 0)
                {
                    SetNotice("当前工程还未添加灯具，无法预览。", true, true);
                    SetPreview(false);
                    return;
                }

                setBusy(true);
                SetPreview(true);

                SetNotice("正在准备预览数据，请稍候...", false, true);
                try
                {
                    generatePreviewParameters();
                    isPreviewMaterial = false;

                    //MARK : 1221 MainFormBase.PreviewButtonClick(material) 给使用动作预览的方法					
                    if (material != null)
                    {
                        isPreviewMaterial = true;                        
                        materialTDListDict = new Dictionary<int, IList<TongdaoWrapper>>();
                        for (int lightIndex = 0; lightIndex < LightWrapperList.Count; lightIndex++)
                        {
                            if (lightIndex == selectedIndex || isMultiMode && selectedIndexList.Contains(lightIndex))
                            {
                                IList<TongdaoWrapper> tdList = getSelectedLightStepTemplate(lightIndex).TongdaoList;
                                for (int stepIndex = 0; stepIndex < material.StepCount; stepIndex++)
                                {
                                    foreach (MaterialIndexAst mi in getSameTDIndexList(material.TdNameList, tdList))
                                    {
                                        int tdAddr = tdList[mi.CurrentTDIndex].TongdaoCommon.Address;
                                        if (!materialTDListDict.ContainsKey(tdAddr))
                                        {
                                            materialTDListDict.Add(tdAddr, new List<TongdaoWrapper>());
                                        }

                                        materialTDListDict[tdAddr].Add(new TongdaoWrapper()
                                        {
                                            ScrollValue = material.TongdaoArray[stepIndex, mi.MaterialTDIndex].ScrollValue,
                                            StepTime = material.TongdaoArray[stepIndex, mi.MaterialTDIndex].StepTime,
                                            ChangeMode = material.TongdaoArray[stepIndex, mi.MaterialTDIndex].ChangeMode,
                                        });
                                    }
                                }
                            }
                        }
                    }

                    SetNotice("正在预览" + (isPreviewMaterial ? "复合素材" : "场景数据") + "...", false, true);
                    refreshConnectedControls(IsDeviceConnected, true); //Preview

                    if (IsDeviceConnected) networkPlayer.Preview(this);
                    if (IsDMXConnected) SerialPlayer.Preview(this);

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
        protected void makeSoundButtonClick()
        {
            if (IsOneMoreConnected() && IsPreviewing)
            {
                if (networkPlayer.MusicControlState() || SerialPlayer.MusicControlState())
                {
                    setMakeSound(true);
                    if (IsDeviceConnected) networkPlayer.TriggerAudio();
                    if (IsDMXConnected) SerialPlayer.TriggerAudio();
                    setMakeSound(false);
                }
                else
                {
                    Console.WriteLine("Dickov :  音频正在触发中或无音频，故此操作不生效...");
                    return;
                }
            }
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
            selectedIndex = lightIndex;
            LightAstList[lightIndex].Remark = remark;
            showLightsInfo(); //EditLightRemark
        }

        /// <summary>
        /// 辅助方法: 确认选中灯具是否否同一种灯具：是则返回true,否则返回false。
        /// 验证方法：取出第一个选中灯具的名字，若后面的灯具的全名（Tag =lightName + ":" + lightType)与它不同，说明不是同种灯具。（只要一个不同即可判断）
        /// </summary>
        /// <returns></returns>
        protected bool checkSameLights(ListView lightsListView)
        {
            if (lightsListView.SelectedItems.Count == 0)
            {
                return false;
            }
            if (lightsListView.SelectedItems.Count == 1)
            {
                return true;
            }

            bool result = true;
            string firstTag = lightsListView.SelectedItems[0].Tag.ToString();
            for (int i = 1; i < lightsListView.SelectedItems.Count; i++) // 从第二个选中灯具开始比对
            {
                string tempTag = lightsListView.SelectedItems[i].Tag.ToString();
                if (!firstTag.Equals(tempTag))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 辅助方法：检查是否有任意灯具被选中
        /// </summary>
        /// <returns></returns>
        protected bool checkNoLightSelected()
        {
            return LightAstList == null || LightAstList.Count == 0 || selectedIndex < 0;
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
        private void MainFormBase_Load(object sender, EventArgs e) { }

        /// <summary>
        /// 辅助方法：在此初始化一些子类都会用到的控件，并需在子类构造函数中优先调用这个方法;以及一些全局变量的取出
        /// </summary>
        protected void initGeneralControls()
        {
            components = new System.ComponentModel.Container();

            // exportFolderBrowserDialog : 导出工程相关
            exportFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            exportFolderBrowserDialog.Description = "请选择要导出的目录，程序会自动在选中位置创建\"CSJ\"文件夹；并在导出成功后打开该目录。若工程文件过大，导出过程中软件可能会卡住，请稍等片刻即可。";
            exportFolderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;

            //// myToolTip：悬停提示,延迟600ms
            myToolTip = new ToolTip(this.components);
            myToolTip.IsBalloon = true;
            myToolTip.InitialDelay = 600;

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

            //MARK : 1218 添加对单步运行时某些步数据是否显示的处理
            try
            {
                string tdStr = IniHelper.GetString("Show", "tdValues", "");
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
            catch (Exception)
            {
                tdValues = null;
            }

            // lightImageList的初始化
            lightImageList = new ImageList();
            lightImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            lightImageList.ImageSize = new System.Drawing.Size(60, 65);
            lightImageList.TransparentColor = System.Drawing.Color.Transparent;

            // myToolTips的初始化
            if (LanguageHelper.Language != "zh-CN")
            {
                copySceneNotice = LanguageHelper.TranslateWord(copySceneNotice);
                keepNotice = LanguageHelper.TranslateWord(keepNotice);
                insertNotice = LanguageHelper.TranslateWord(insertNotice);
                appendNotice = LanguageHelper.TranslateWord(appendNotice);
                deleteNotice = LanguageHelper.TranslateWord(deleteNotice);
                backStepNotice = LanguageHelper.TranslateWord(backStepNotice);
                nextStepNotice = LanguageHelper.TranslateWord(nextStepNotice);
            }

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

            // DOTO 211103 处理 协议列表 和 场景列表
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

            //MARK：添加这一句，会去掉其他线程使用本UI控件时弹出异常的问题(权宜之计)。
            CheckForIllegalCrossThreadCalls = false;
        }

        #endregion

        #region 退出程序相关

        /// <summary>
        /// 辅助方法：点击退出时FormClosing事件；
        /// </summary>
        /// <param name="e"></param>
        protected void formClosing(FormClosingEventArgs e)
        {
            //MARK 只开单场景：17.4 FormClosing时提示保存工程
            if (!RequestSaveProject(LanguageHelper.TranslateSentence("关闭程序前，是否保存当前工程？")))
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
            if (!RequestSaveProject("退出应用前，是否保存当前工程？"))
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
        protected void refreshStep()
        {
            chooseStep(getCurrentStep());  // RefreshStep
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

        #endregion

        #region 委托的成功或方法块 

        /// <summary>
        /// 辅助回调方法：启用调试成功
        /// </summary>
        /// <param name="obj"></param>
        public void StartPreviewCompleted()
        {
            Invoke((EventHandler)delegate
            {
                refreshConnectedControls(IsDeviceConnected, false);  //StartPreviewCompleted
            });
        }

        /// <summary>
        /// 辅助回调方法：启用调试失败
        /// </summary>
        /// <param name="msg">错误提示</param>
        public void StartPreviewError(string msg)
        {
            Invoke((EventHandler)delegate
            {
                DisConnect();
                // 这里先后顺序十分重要：
                // 1. 要在DisConnect()后，弹出提示 ！ 若提示在前，会重新触发Activated-》StartPreview()方法，就会造成跑两次不同msg的StartPreviewError方法！
                // 2.要在ConnForm.ShowDialog()前，弹出提示：否则会出现牛头不对马嘴的情况：即可能都已经重连了再弹出断连的提示！
                SetNotice(msg, true, true);
                ConnForm.ShowDialog();
            });
        }

        /// <summary>
        /// 辅助回调方法：预览数据生成成功
        /// </summary>
        public void PreviewDataGenerateCompleted()
        {
            Invoke((EventHandler)delegate
            {
                SetNotice("预览数据生成成功,即将开始预览。", false, true);
                refreshConnectedControls(IsDeviceConnected, true); //Preview
            });
        }

        /// <summary>
        /// 辅助回调方法：预览数据生成出错
        /// </summary>
        public void PreviewDataGenerateError(string msg)
        {
            Invoke((EventHandler)delegate
            {
                SetPreview(false);
                SetNotice("预览数据生成出错,无法预览,。错误原因为：" + msg, true, false);
            });
        }

        /// <summary>
        /// 辅助回调方法：预览数据生成进度提示
        /// </summary>
        public void PreviewDataGenerateProgress(string name)
        {
            Invoke((EventHandler)delegate
            {
                SetNotice(LanguageHelper.TranslateSentence("预览数据生成中") + "(" + name + ")", false, true);
            });
        }

        /// <summary>
        /// 辅助回调函数：工程导出成功
        /// </summary>
        public void ExportProjectCompleted()
        {
            Invoke((EventHandler)delegate
            {
                SetNotice("工程导出成功，即将拷贝到相关目录...", false, true);
                copyProject();
            });
        }

        /// <summary>
        /// 辅助回调函数：工程导出失败
        /// </summary>
        public void ExportProjectError(string msg)
        {
            Invoke((EventHandler)delegate
            {
                setBusy(false);
                SetNotice("工程导出出错：" + msg, false, true);
            });
        }

        /// <summary>
        /// 辅助回调函数：工程导出进度
        /// </summary>
        public void ExportProjectProgress(string name)
        {
            Invoke((EventHandler)delegate
            {
                SetNotice(LanguageHelper.TranslateSentence("正在生成工程文件") + "(" + name + ")", false, false);
            });
        }

        #endregion

        /// <summary>
        /// 辅助方法：点击测试按键
        /// </summary>
        protected void testButtonClick()
        {
            generatePreviewParameters();
            Console.WriteLine(currentSoundList);
        }

        #region 实现MainFormInterface内定义的一些方法，供维佳调用

        public IList<DB_FineTune> GetFineTunes()
        {
            return generateDBFineTuneList();
        }

        public int GetSceneCount()
        {
            return SceneCount;
        }

        public IList<DB_Light> GetLights()
        {
            return generateDBLightList();
        }

        public string GetConfigPath()
        {
            return GlobalIniPath;
        }

        public IList<int> GetChannelIDList()
        {
            IList<int> channelIDList = new List<int>();

            for (int lightIndex = 0; lightIndex < LightAstList.Count; lightIndex++)
            {
                LightAst la = LightAstList[lightIndex];
                for (int channelID = la.StartNum; channelID <= la.EndNum; channelID++)
                {
                    channelIDList.Add(channelID);
                }
            }

            return channelIDList;
        }

        public IList<int> GetPreviewChannelIDList()
        {
            if (isPreviewMaterial)
            {
                IList<int> channelIDList = new List<int>();
                foreach (int channelID in materialTDListDict.Keys)
                {
                    channelIDList.Add(channelID);
                }
                return channelIDList;
            }
            else
            {
                return GetChannelIDList();
            }
        }

        /// <summary>
        /// 挑出需要导出的场景编号，避免浪费太多时间在导出无步数的场景上；
        /// </summary>
        /// <returns></returns>
        public HashSet<int> GetExportSceneSet()
        {
            HashSet<int> result = new HashSet<int>();
            IList<int> dbSceneList = channelDAO.GetExistSceneList();
            for (int sceneIndex = 0; sceneIndex < sceneLoadArray.Length; sceneIndex++)
            {
                //1.已加载到内存（数据库内可能还没有保存）或 2.数据库中有数据 
                if (sceneLoadArray[sceneIndex] || dbSceneList.Contains(sceneIndex))
                {
                    result.Add(sceneIndex);
                }
            }
            return result;
        }

        private int currenMusicStepTime;
        private int currentMusicWaitTime;
        private List<int> currentSoundList;
        private bool isPreviewMaterial = false;
        private Dictionary<int, IList<TongdaoWrapper>> materialTDListDict;  //音频素材无法预览，故此处只需存常规素材的通道值信息即可：key = channelID , value =  IList<TongdaoWrapper>

        /// <summary>
        /// 每次点击预览前，这些数据要准备好（或者在更改场景或修改音频链表数据后，但这样就太多了，如果不是很耗时，就在预览前跑一下）
        /// </summary>
        private void generatePreviewParameters()
        {
            IniHelper iniAst = new IniHelper(GlobalIniPath);

            currenMusicStepTime = iniAst.ReadInt("SK", CurrentScene + "ST", 0) * (int)EachStepTime;
            currentMusicWaitTime = iniAst.ReadInt("SK", CurrentScene + "JG", 0);
            currentSoundList = new List<int>();
            string lkStr = iniAst.ReadString("SK", CurrentScene + "LK", "");
            foreach (char item in lkStr.ToCharArray())
            {
                currentSoundList.Add(int.Parse(item.ToString()));
            }
        }

        public int GetPreviewMusicControlTime()
        {
            return currenMusicStepTime;
        }

        public int GetPreviewMusicWaitTime()
        {
            return currentMusicWaitTime;
        }

        public List<int> GetPreviewMusicStepList()
        {
            return currentSoundList;
        }

        public int GetPreviewFactoryTime()
        {
            return (int)EachStepTime * 1000;
        }

        public IList<TongdaoWrapper> GetPreviewChannelData(int channelNo, int mode)
        {
            if (isPreviewMaterial)
            {
                return materialTDListDict[channelNo];
            }
            else
            {
                return GetSMTDList(new DB_ChannelPK() { ChannelID = channelNo, Scene = CurrentScene, Mode = mode });
            }
        }

        #endregion
    }


}