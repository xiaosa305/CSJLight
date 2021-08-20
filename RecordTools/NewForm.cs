using Dickov.Utils;
using RecordTools.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace RecordTools
{
    public partial class NewForm : Form
    {
        private IniFileHelper iniHelper;
        private decimal eachStepTime = .04m;
        private string savePath;
        private int sceneNo = 1;
        private MusicSceneConfig musicSceneConfig;  //维佳的接口

        // 以下变量，为《分页显示》功能必需的变量 
        private int pageSize = 16;    // 如果此项大于tdCount,则应设为tdCount的值
        private int showPage = 1;  // 通道panel显示的页面
        private int singlePage = 1; // 当转换为多选模式时，记录单选的tempPage，便于转回单选模式时调用；        
        private int captainPage = 0; // 多选时选一个组长，每当切到多选模式，就把showPage设为它；
        private SortedSet<int> pageSet = new SortedSet<int>();   // 记录多选的页面集合
        private HashSet<int> tdSet; // 记录使用的通道    

        public NewForm()
        {
            InitializeComponent();

            string loadexeName = Application.ExecutablePath;
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(loadexeName);
            string appFileVersion = string.Format("{0}.{1}.{2}.{3}", fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart, fileVersionInfo.FileBuildPart, fileVersionInfo.FilePrivatePart);
            Text += " v" + appFileVersion;

            // 载入场景
            IList<string> frameList = TextHelper.Read(Application.StartupPath + @"\FrameList.txt");
            frameComboBox.Items.Add("无开机场景");
            for (int frameIndex = 0; frameIndex < frameList.Count; frameIndex++)
            {
                frameComboBox.Items.Add(frameList[frameIndex]);
            }
            frameComboBox.SelectedIndex = sceneNo;

            // 读取各个默认配置
            iniHelper = new IniFileHelper(Application.StartupPath + @"\CommonSet.ini");
            eachStepTime = iniHelper.ReadInt("CommonSet", "EachStepTime", 40) / 1000m;
            int stepTime = iniHelper.ReadInt("CommonSet", "StepTime", 10);

            //添加frameStepTimeNumericUpDown相关初始化及监听事件			
            stepTimeNumericUpDown.Increment = eachStepTime;
            stepTimeNumericUpDown.Maximum = 250 * eachStepTime;
            stepTimeNumericUpDown.Value = eachStepTime * stepTime;
            stepTimeNumericUpDown.MouseWheel += someNUD_MouseWheel;

            jgtNumericUpDown.Value = iniHelper.ReadInt("CommonSet", "JG", 0);
            jgtNumericUpDown.MouseWheel += someNUD_MouseWheel;

            savePath = iniHelper.ReadString("CommonSet", "SavePath", @"C:\Temp\CSJ");
            saveFolderBrowserDialog.SelectedPath = savePath;
            setSavePathLabel();
            sceneNo = iniHelper.ReadInt("CommonSet", "SceneNo", 1);
            if (sceneNo < 1 || sceneNo > 32)
            {
                sceneNo = 1;
            }
            setSceneNo(false);

            sceneNoTextBox.LostFocus += new EventHandler(sceneNoTextBox_LostFocus);

            // 初始化各个组件
            tdSet = new HashSet<int>() { };

            for (int pageIndex = 0; pageIndex < 32; pageIndex++)
            {
                Button pageBtn = new Button
                {
                    Text = (pageIndex + 1) + "\n" + (pageIndex * 16 + 1),
                    Size = tdButtonDemo.Size,
                    ForeColor = tdButtonDemo.ForeColor,
                    Tag = pageIndex + 1,
                    Visible = true,
                };
                pageFLP.Controls.Add(pageBtn);
                pageBtn.Click += pageBtn_Click;
            }

            for (int cbIndex = 0; cbIndex < pageSize; cbIndex++)
            {
                Button tdBtn = new Button
                {
                    Size = tdButtonDemo.Size,
                    ForeColor = tdButtonDemo.ForeColor,
                    Visible = true,
                    Tag = cbIndex + 1
                };
                tdFLP.Controls.Add(tdBtn);
                tdBtn.Click += tdBtn_Click;
            }

            refreshPage();
        }

        private void ButtonForm_Load(object sender, EventArgs e) { }

        #region 通道选择相关

        /// <summary>
        /// 事件：勾选|取消勾选《灯具多选》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lightCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (lightCheckBox.Checked && pageSet.Count == 0)
            {
                pageSet.Add(singlePage);
                captainPage = singlePage;
            }
            refreshPage();
        }

        /// <summary>
        /// 事件：点击《页面按钮》切换页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pageBtn_Click(object sender, EventArgs e)
        {
            int pageNum = (int)(sender as Button).Tag;

            // 多选状态下，反向选择（已在组内的删除，不在组内的添加并设为组长）
            if (lightCheckBox.Checked)
            {
                if (pageSet.Contains(pageNum))
                {
                    if (pageSet.Count == 1)
                    {
                        setNotice("多选项不可少于1！", true);
                        return;
                    }
                    pageSet.Remove(pageNum);
                    captainPage = pageSet.Min; // 若是删除，则把组长指定为组内第一个值
                }
                else
                {
                    pageSet.Add(pageNum);
                    captainPage = pageNum;
                }
            }
            // 单选状态时，直接设singlePage为点击页
            else
            {
                singlePage = pageNum;
            }

            refreshPage();
        }

        /// <summary>
        /// 事件：勾选或取消《512全选》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void allCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (allCheckBox.Checked)
            {
                for (int tdNum = 1; tdNum <= 512; tdNum++)
                {
                    tdSet.Add(tdNum);
                }
            }
            else
            {
                tdSet.Clear();
            }
            refreshPage();
        }

        /// <summary>
        /// 事件：勾选|取消勾选《本页全选》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            for (int addNum = 1; addNum <= pageSize; addNum++)
            {
                unifySetTd( pageCheckBox.Checked , addNum);
            }
            refreshPage();
        }       

        /// <summary>
        /// 事件：点击《通道按钮》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tdBtn_Click(object sender, EventArgs e)
        {
            int addNum = int.Parse( (sender as Button).Tag.ToString()); //Tag不变， 就是在当前页的addNum（或称tdIndex）                       
            unifySetTd( ! tdSet.Contains((showPage - 1) * pageSize + addNum), addNum);         
            refreshPage();
        }

        /// <summary>
        /// 辅助方法：包括更改showPage，刷新页面按键和通道按键；
        /// </summary>
        private void refreshPage()
        {
            // 根据当前状态，处理showPage ;
            showPage = lightCheckBox.Checked ? captainPage : singlePage;

            // 根据多|单状态，刷新页面按钮组（颜色）
            for (int pageIndex = 0; pageIndex < 32; pageIndex++)
            {
                Button pageBtn = pageFLP.Controls[pageIndex] as Button;
                int pageNum = pageIndex + 1;

                if (lightCheckBox.Checked)
                {
                    pageBtn.BackColor = pageSet.Contains(pageNum) ?
                           (pageNum == showPage ? Color.Tomato : Color.DarkSalmon) // 组长颜色略深一些
                            : SystemColors.Control;  // 不在组内的元素，就显示正常色
                }
                else
                {
                    pageBtn.BackColor = pageNum == showPage ? Color.Tomato : SystemColors.Control;
                }
            }

            // 根据showPage，刷新通道按钮组（颜色和文本）            
            for (int btnIndex = 0; btnIndex < pageSize; btnIndex++)
            {
                int tdNum = (showPage - 1) * pageSize + (btnIndex + 1);

                Button tdBtn = tdFLP.Controls[btnIndex] as Button;                
                tdBtn.Text = (btnIndex + 1) + "\n" + tdNum;
                tdBtn.BackColor = tdSet.Contains(tdNum) ? Color.Blue: SystemColors.Control;
                tdBtn.ForeColor = tdSet.Contains(tdNum) ? Color.AliceBlue:SystemColors.ControlText;                    
            }
        }
        
        /// <summary>
        /// 辅助方法：抽象出统一设通道值的操作(供《本页全选》和《点击页面按键》调用)
        /// </summary>
        /// <param name="isAdd"></param>
        /// <param name="addNum"></param>
        private void unifySetTd(bool isAdd, int addNum)
        {
            if (lightCheckBox.Checked)
            {
                foreach (int pageNum in pageSet)
                {
                    tdSetAD(  isAdd , (pageNum - 1) * pageSize + addNum);
                }
            }
            else
            {
                tdSetAD( isAdd , (showPage - 1) * pageSize + addNum);
            }
        }

        /// <summary>
        ///  (策略模式）删除或新增成员
        /// </summary>
        /// <param name="isAdd"></param>
        /// <param name="tdNum"></param>
        private void tdSetAD(bool isAdd, int tdNum)
        {
            if (isAdd)
            {
                tdSet.Add(tdNum);
            }
            else
            {
                tdSet.Remove(tdNum);
            }
        }

        #endregion

        #region 配置相关的一些参数，及监听事件

        /// <summary>
        /// 事件：步时间发生变化时，改变相应的stepTime；
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stepTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            int stepTime = decimal.ToInt32(stepTimeNumericUpDown.Value / eachStepTime);
            stepTimeNumericUpDown.Value = stepTime * eachStepTime;
        }

        /// <summary>
        ///  事件：点击《设置保存目录》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setFilePathButton_Click(object sender, EventArgs e)
        {
            DialogResult dr = saveFolderBrowserDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                savePath = saveFolderBrowserDialog.SelectedPath;
                setSavePathLabel();

                setNotice("已设置存放目录为：" + savePath, false);
            }
        }

        /// <summary>
        /// 辅助方法：根据当前的savePath，设置label及toolTip
        /// </summary>
        private void setSavePathLabel()
        {
            if (savePath != null)
            {
                recordPathLabel.Text = savePath;
                myToolTip.SetToolTip(recordPathLabel, savePath);
            }
        }

        /// <summary>
        /// 事件：《recordTextBox》失去焦点，把文字做相关的转换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sceneNoTextBox_LostFocus(object sender, EventArgs e)
        {
            if (sceneNoTextBox.Text.Length == 0)
            {
                setNotice("文件序号不得为空", true);
                sceneNoTextBox.Text = "1";
            }
            sceneNo = int.Parse(sceneNoTextBox.Text);
            if (sceneNo < 1)
            {
                setNotice("文件序号不得小于1", true);
                sceneNo = 1;
            }
            else if (sceneNo > 32)
            {
                setNotice("文件序号不得大于32", true);
                sceneNo = 32;
            }

            setSceneNo(true);
        }

        /// <summary>
        /// 事件：《场景选择框》更改选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (frameComboBox.SelectedIndex != 0)
            {
                sceneNo = frameComboBox.SelectedIndex;
                setSceneNo(true);
            }
        }

        /// <summary>
        /// 事件：点击《+》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plusButton_Click(object sender, EventArgs e)
        {
            if (sceneNo >= 32)
            {
                setNotice("文件序号不得大于32。", true);
                return;
            }
            sceneNo++;
            setSceneNo(true);
        }

        /// <summary>
        /// 事件：点击《-》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void minusButton_Click(object sender, EventArgs e)
        {
            if (sceneNo <= 1)
            {
                setNotice("文件序号不得小于1。", true);
                return;
            }
            sceneNo--;
            setSceneNo(true);
        }

        /// <summary>
        /// 辅助方法：根据当前的savePath，设置label及toolTip
        /// </summary>
        private void setSceneNo(bool isNotice)
        {
            frameComboBox.SelectedIndexChanged -= frameComboBox_SelectedIndexChanged;
            frameComboBox.SelectedIndex = sceneNo;
            frameComboBox.SelectedIndexChanged += frameComboBox_SelectedIndexChanged;

            sceneNoTextBox.Text = sceneNo.ToString();
            if (isNotice)
            {
                setNotice("已设置文件名为M" + sceneNo + ".bin", false);
            }
        }

        #endregion

        #region 保存或加载配置

        /// <summary>
        /// 事件：点击《打开音频文件》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog.FileName;

                    musicSceneConfig = MusicSceneConfig.ReadFromFile(fileName);
                    stepTimeNumericUpDown.Value = eachStepTime * musicSceneConfig.StepTime;
                    jgtNumericUpDown.Value = musicSceneConfig.StepWaitTIme;
                    mLKTextBox.Text = musicSceneConfig.MusicStepList;
                    tdSet = musicSceneConfig.MusicChannelNoList;
                    refreshPage();

                    //想办法通过文件名，来更改sceneNo的值
                    int tempSceneNo = int.Parse(System.Text.RegularExpressions.Regex.Replace(fileName, @"[^0-9]+", ""));
                    if (tempSceneNo > 0 && tempSceneNo <= 32)
                    {
                        sceneNo = tempSceneNo;
                        setSceneNo(false);
                    }

                    setNotice("已成功打开文件：" + fileName, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开文件异常:\n" + ex.Message);
            }
        }

        /// <summary>
        /// 事件：点击《保存音频文件》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (tdSet == null || tdSet.Count == 0)
            {
                setNotice("请选择至少一个通道！", true);
                return;
            }

            musicSceneConfig = new MusicSceneConfig
            {
                StepTime = decimal.ToInt32(stepTimeNumericUpDown.Value / eachStepTime),
                StepWaitTIme = decimal.ToInt32(jgtNumericUpDown.Value),
                MusicStepList = mLKTextBox.Text.Trim(),
                MusicChannelNoList = tdSet
            };

            if (musicSceneConfig.WriteToFile(savePath, "M" + sceneNo + ".bin"))
            {
                setNotice("成功保存配置文件,路径为：" + savePath + @"\M" + sceneNo + ".bin", true);
            }
            else
            {
                setNotice("保存配置文件失败", true);
            }
        }

        /// <summary>
        /// 事件：点击《保存全局配置》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveConfigButton_Click(object sender, EventArgs e)
        {
            GlobalConfig gc = new GlobalConfig(frameComboBox.SelectedIndex);
            if (gc.WriteToFile(savePath, "Config.bin"))
            {
                setNotice("成功生成全局配置文件", true);
            }
            else
            {
                setNotice("生成全局配置文件失败", true);
            }
        }

        #endregion

        #region 通用方法(这些方法往往只需稍微修改或完全不动，就可以在不同的界面中通用)

        /// <summary>
        /// 辅助方法：设置提醒
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="msgBoxShow"></param>
        private void setNotice(string msg, bool msgBoxShow)
        {
            myStatusLabel.Text = msg;
            if (msgBoxShow)
            {
                MessageBox.Show(msg);
            }
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
        /// 事件：键盘按键点击事件:确保textBox内只能是0-9、及回退键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void someTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            SortedSet<int> tempSS = new SortedSet<int>();
            foreach (int tdNum in tdSet)
            {
                tempSS.Add(tdNum);
            }

            //Console.WriteLine( pageSet + " : " + showPage + " : " + tempSS);
            Console.WriteLine("tdSetCount : " + tempSS.Count);
            foreach (int tdNUm in tempSS)
            {
                Console.Write(tdNUm + " , ");
            }
            Console.WriteLine();
        }
    }
}
