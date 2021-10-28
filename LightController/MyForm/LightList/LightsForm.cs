
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using LightController.Ast;
using LightController.MyForm;
using LightController.Common;
using LightController.Ast.Enum;

namespace LightController
{
    public partial class LightsForm : System.Windows.Forms.Form
    {
        public const int MAX_TD = 512;
        private int minNum = 1; //每次new LightsAstForm的时候，需要填入的最小值；也就是当前所有灯具通道占用的最大值+1
        
        private IList<LightAst> lightAstList = new List<LightAst>();
        private int oldCount;

        private IList<LightAst> lightAstList2 = new List<LightAst>();
        private int newCount;

        private MainFormBase mainForm;
        private List<LightsChange> changeList;  //变动列表
        public LightsForm(MainFormBase mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;

            // 1. 生成左边的灯具列表，树状形式		
            string path = mainForm.SavePath + @"\LightLibrary";
            if (Directory.Exists(path))
            {
                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    DirectoryInfo di = new DirectoryInfo(dir);
                    TreeNode treeNode = new TreeNode(di.Name);

                    //由ini文件生成子节点
                    FileInfo[] files = di.GetFiles();
                    foreach (FileInfo file in files)
                    {
                        string fileName = file.Name;
                        if (fileName.EndsWith(".ini"))
                        {
                            TreeNode node = new TreeNode(
                                fileName.Substring(0, fileName.IndexOf("."))
                            );
                            treeNode.Nodes.Add(node);
                        }
                    }
                    this.skinTreeView1.Nodes.Add(treeNode);
                }
                this.skinTreeView1.ExpandAll();
            }

            // 2.只有加载旧项目（已有LightAst列表）时，才加载lightAstList到右边
            if (mainForm.LightAstList != null && mainForm.LightAstList.Count > 0)
            {
                foreach (LightAst laOld in mainForm.LightAstList)
                {
                    lightAstList.Add(new LightAst(laOld));
                }
                foreach (LightAst la in lightAstList)
                {
                    addListViewItem(la.LightName, la.LightType, la.LightAddr, la.LightPic);
                    minNum = la.EndNum + 1;
                }
            }

            //3. 初始化变动列表
            changeList = new List<LightsChange>();
        }

        /// <summary>
        ///  窗口Load方法：作用是初始化窗体位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LightsForm_Load(object sender, EventArgs e)
        {
            Location = new Point(mainForm.Location.X + 100, mainForm.Location.Y + 100);
            lightsListView.HideSelection = true;
            LanguageHelper.InitForm(this);
            LanguageHelper.TranslateListView(lightsListView);
        }

        #region 添加灯具

        /// <summary>
        ///  事件：点击《添加》按钮：添加新灯具
        ///  1.需选中左边的一个灯具（灯库），点击添加
        ///  2.打开一个NewForm的新实例，在NewForm中填好参数后回调AddListView方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addLightButton_Click(object sender, EventArgs e)
        {
            if (skinTreeView1.SelectedNode == null || skinTreeView1.SelectedNode.Level == 0)
            {
                MessageBox.Show(LanguageHelper.TranslateSentence("请先选择灯具。"));
            }
            else
            {
                if (skinTreeView1.SelectedNode.Parent != null)
                {
                    string fullPath = mainForm.SavePath + @"\LightLibrary\" + skinTreeView1.SelectedNode.FullPath + ".ini";
                    LightsAstForm lightsAstForm = new LightsAstForm(this, fullPath, minNum);
                    lightsAstForm.ShowDialog();
                }
            }
        }

        /// <summary>
        ///  辅助方法：添加数据到ListView中；主要给NewForm回调使用；添加后minNum设成endNum
        /// </summary>
        /// <param name="lightPath"></param>
        /// <param name="lightName"></param>
        /// <param name="lightType"></param>
        /// <param name="lightAddr"></param>
        /// <param name="lightPic"></param>
        /// <param name="startNum"></param>
        /// <param name="endNum"></param>
        /// <param name="lightCount"></param>
        public void AddListViewAndLightAst(String lightPath, string lightName, string lightType,
                    string lightAddr, string lightPic, int startNum, int endNum, int lightCount)
        {
            // 新增时，1.直接往listView加数据，
            addListViewItem(lightName, lightType, lightAddr, lightPic);

            // 2.往lightAstList添加新的数据
            LightAst newLightAst = new LightAst()
            {
                LightAddr = lightAddr,
                LightName = lightName,
                LightType = lightType,
                LightPic = lightPic,
                LightPath = lightPath,
                StartNum = startNum,
                EndNum = endNum,
                Count = lightCount
            };

            lightAstList.Add(newLightAst);

            // 3.设置minNum的值 			
            minNum = endNum + 1;
            if (minNum > 512)
            {
                MessageBox.Show(LanguageHelper.TranslateSentence("当前工程已经到达DMX512地址上限，请谨慎设置！"));
                minNum = 512;
            }

            //DOTO 211028 新增灯具时，记录一个其在list中的index，方便edit和delete时对号入座
            changeList.Add(new LightsChange()
            {
                Operation = EnumOperation.ADD,
                NewLightAst = newLightAst,
                //新增项内设置LightIndex，是为方便修改时读取（但如果先删了后 ，再点击修改，此时的Index就对应不上了啊
                //）
                LightIndex = lightAstList.Count - 1   
            });
        }

        /// <summary>
        ///  辅助方法：添加item到ListView中，需要一些参数
        /// </summary>
        /// <param name="lightName"></param>
        /// <param name="lightType"></param>
        /// <param name="lightAddr"></param>
        /// <param name="lightPic"></param>
        private void addListViewItem(string lightName, string lightType, string lightAddr, string lightPic)
        {
            ListViewItem item = new ListViewItem(lightName);
            item.SubItems.Add(lightType);
            item.SubItems.Add(lightAddr);
            item.ImageKey = lightPic;
            lightsListView.Items.Add(item);
        }

        #endregion

        #region 修改灯具

        /// <summary>
        /// 事件：双击灯具进行修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lightsListView_DoubleClick(object sender, EventArgs e)
        {
            int lightIndex = lightsListView.SelectedIndices[0];
            LightsEditForm lightsEditForm = new LightsEditForm(this, lightAstList[lightIndex], lightIndex);
            lightsEditForm.ShowDialog();
        }

        /// <summary>
        ///  辅助方法：供LightsEditForm回调使用
        /// </summary>
        /// <param name="lightIndex"></param>
        /// <param name="startNum"></param>
        public bool UpdateLight(int lightIndex, int startNum)
        {
            int endNum = startNum + lightAstList[lightIndex].Count - 1;
            if (endNum > MAX_TD)
            {
                MessageBox.Show(LanguageHelper.TranslateSentence("设置后的最后地址超过了DMX512的上限值，请重新设置。"));
                return false;
            }
            else
            {
                int oldStartNum = lightAstList[lightIndex].StartNum;
                if (oldStartNum == startNum)
                {
                    MessageBox.Show(LanguageHelper.TranslateSentence("设置地址与原地址相同，请重新设置。"));
                    return false;
                }

                // 1.修改lightAstList
                lightAstList[lightIndex].StartNum = startNum;
                lightAstList[lightIndex].EndNum = endNum;
                lightAstList[lightIndex].LightAddr = startNum + "-" + endNum;

                //DOTO 211028 修改灯具地址时，根据实际情况处理changeList
                int changeIndex = getChangeIndex(lightIndex);
                // 1.存在项且为【修改型】时
                if (changeIndex != -1 && changeList[changeIndex].Operation == EnumOperation.UPDATE)
                {
                    int newAddNum = changeList[changeIndex].AddNum + (startNum - oldStartNum);
                    if (newAddNum == 0)  //若最终算出来的增加值为0，则修改无意义，可直接删除这个变化项
                    {
                        changeList.RemoveAt(changeIndex);
                    }
                    else // 否则就修改AddNum即可（其他几个属性没有修改的必要）
                    {
                        changeList[changeIndex].AddNum = newAddNum;
                    }
                }
                // 2.不存在此项 或 3.存在项但为【删除型】时（此时的lightIndex无法和取到的项对应上，不要去动那个项！）：新建项
                else if(changeIndex == -1 || changeList[changeIndex].Operation == EnumOperation.DELETE)
                {
                    changeList.Add(new LightsChange(){
                            Operation = EnumOperation.UPDATE,
                            LightIndex = lightIndex,
                            NewLightAst = lightAstList[lightIndex],
                            AddNum = startNum - oldStartNum,
                    });                   
                }
                // 4.存在项且为【新建项】时：无需额外处理， 因为此项内有效的数据NewLightAst（->lightAstList[lightIndex]）已经发生了变化
                

                // 2.修改lightListView
                lightsListView.Items[lightIndex].SubItems[2].Text = lightAstList[lightIndex].LightAddr;
                return true;
            }
        }

        #endregion

        #region 删除灯具

        /// <summary>
        /// 事件：点击《删除灯具》的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteLightButton_Click(object sender, EventArgs e)
        {
            if (lightsListView.SelectedIndices.Count == 0)
            {
                MessageBox.Show(LanguageHelper.TranslateSentence("请先选择要删除的灯具。"));
            }
            else
            {
                //DOTO 211028 删除灯具时：(为简化问题，先改成每次只允许删除一个灯具)
                //一、changeList中无相关项，只需新增就好
                //二、changeList中无相关项
                //  1.  新增：因为先后的lightIndex无法确保对上【或者可以确认无法对应】，故只需在记录内新加一项， 到时候按changeList顺序执行就好：只有按顺序运行，index才能对上)
                //  2. 修改：既然项都要被删了，把相应的【修改项】直接删除好了   ！ DOTO（修改项时也可能会碰到index不符合的情况！）
                //  3. 删除，不管这种情况
                int delLightIndex = lightsListView.SelectedIndices[0];
                int changeIndex = getChangeIndex(delLightIndex);

                //当changeList中有这个项的更改(UPDATE)记录时，把该记录删掉；
                if (changeIndex != -1 && changeList[changeIndex].Operation == EnumOperation.UPDATE)  
                {
                    changeList.RemoveAt(changeIndex);
                } 
                changeList.Add(
                     new LightsChange()
                     {
                         Operation = EnumOperation.DELETE,
                         LightIndex = delLightIndex,
                     }
                );

                //lightAstList.RemoveAt(delLightIndex);
                //lightsListView.Items.RemoveAt(delLightIndex);
                //lightAstList.Add();
                

                lightsListView.Refresh();
            }
        }

        #endregion

        /// <summary>
        ///  事件：点击《确认》后，添加lightAstList到mainForm去，并进行相关操作
        /// --用此lightAstList替代mainForm中的原lightAstList，并顺便删减lightWrapperList和ListView中的灯具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterButton_Click(object sender, EventArgs e)
        {
            // 1.当点击确认时，应该将所有的listViewItem 传回到mainForm里。
            //mainForm.ReBuildLightList(lightAstList);

            //DOTO 211026 enterButton_Click() 当修改了灯具列表后，必须保存工程			
            //1.若点了取消，则还保持在当前界面return；
            //2.点了是，则执行操作；
            //3.未修改和点了是(2)之后，统一都要激活mainForm
            if (changeList != null && changeList.Count > 0)
            {
                if (DialogResult.Cancel == MessageBox.Show(
                        LanguageHelper.TranslateSentence("点击《确定》后，会保存工程以使变动生效。是否立刻保存？"),
                        "保存工程？",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question))
                {
                    return;
                }
                mainForm.ReBuildLightList(changeList);
            }
            Dispose();
            mainForm.Activate();
        }

        /// <summary>
        /// 辅助方法：检测传进来的起始地址和截止地址（同时添加多个灯具时，也会有这两个地址【第一个灯的起始地址和最后一个灯的截止地址】）
        /// ，是否已被当前灯具所占用；
        ///     若传进来的lightIndex==-1；则表示新加灯具，否则为修改旧灯具，先从表中删除旧灯具
        /// </summary>
        /// <param name="startAddr"></param>
        /// <param name="endAddr"></param>
        public bool CheckAddrAvailable(int lightIndex, int startAddr, int endAddr)
        {
            List<int> addrList = new List<int>();
            for (int i = 0; i < lightAstList.Count; i++)
            {
                if (i != lightIndex)
                {
                    LightAst la = lightAstList[i];
                    for (int j = la.StartNum; j <= la.EndNum; j++)
                    {
                        addrList.Add(j);
                    }
                }
            }
            for (int addr = startAddr; addr <= endAddr; addr++)
            {
                if (addrList.Contains(addr))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 辅助方法：获取此处变更灯具地址，是否存在于changeList中
        /// </summary>
        /// <param name="lightIndex">修改的灯具在列表中的index</param>
        /// <returns>如果不存在，返回-1；否则返回在changeList中的索引</returns>
        private int getChangeIndex(int lightIndex)
        {
            for (int changeIndex = 0; changeIndex < changeList.Count; changeIndex++)
            {
                if (changeList[changeIndex].LightIndex == lightIndex)
                {
                    return changeIndex;
                }
            }
            return -1;
        }

        #region 测试相关

        /// <summary>
        ///  点击《测试》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void testButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine(changeList);
        }

        #endregion

        #region 退出相关

        /// <summary>
        /// 事件：点击《取消》
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            Dispose();
            mainForm.Activate();
        }
        /// <summary>
        ///  事件：关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LightsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
            mainForm.Activate();
        }

        #endregion

    }
}
