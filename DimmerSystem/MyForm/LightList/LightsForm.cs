using LightController.Ast;
using LightController.Ast.Enum;
using LightController.Common;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LightController.MyForm.LightList
{
    public partial class LightsForm : UIForm
    {
        public const int MAX_TD = 512;
        public int MinNum = 1; //每次new LightsAstForm的时候，需要填入的最小值；也就是当前所有灯具通道占用的最大值+1        
        private IList<LightAst> lightAstList = new List<LightAst>();
        private MainFormBase mainForm;
        private List<LightsChange> changeList;  //变动列表

        public LightsForm(MainFormBase mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;

            myToolTip.SetToolTip(deleteButton,
               "1.为避免误操作，原工程中的灯具删除后，在右侧灯具列表内仍会保留，只是其\n" +
               "《通道地址》会变成空值；如有需要，可在双击为其添加新地址后恢复；\n" +
               "2.如果删除的是新加的灯具，则会直接删除。");

            // 生成左边的灯具列表，树状形式		
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
                    libTreeView.Nodes.Add(treeNode);
                }
                libTreeView.ExpandAll();
            }

            //初始化变动列表
            changeList = new List<LightsChange>();
            // 2.只有加载旧项目（已有LightAst列表）时，才加载lightAstList到右边
            if (mainForm.LightAstList != null && mainForm.LightAstList.Count > 0)
            {
                foreach (LightAst laOld in mainForm.LightAstList)
                {
                    lightAstList.Add(new LightAst(laOld));
                    changeList.Add(new LightsChange() { Operation = EnumOperation.NOCHANGE });
                    addListViewItem(laOld.LightName, laOld.LightType, laOld.LightAddr, laOld.LightPic);
                    MinNum = laOld.EndNum + 1;
                }
            }
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

            //LanguageHelper.InitForm(this);
            //LanguageHelper.TranslateListView(lightsListView);

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
            if (libTreeView.SelectedNode == null || libTreeView.SelectedNode.Level == 0)
            {
                MessageBox.Show(LanguageHelper.TranslateSentence("请先选择灯具。"));
            }
            else
            {
                if (libTreeView.SelectedNode.Parent != null)
                {
                    string fullPath = mainForm.SavePath + @"\LightLibrary\" + libTreeView.SelectedNode.FullPath + ".ini";
                    LightsAddForm lightsAddForm = new LightsAddForm(this, fullPath);
                    lightsAddForm.ShowDialog();
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
            MinNum = endNum + 1;
            if (MinNum > 512)
            {
                MessageBox.Show(LanguageHelper.TranslateSentence("当前工程已经到达DMX512地址上限，请谨慎设置！"));
                MinNum = 512;
            }

            //211028 新增灯具时，只需把NewLightAst属性与lightAstList中的项进行绑定即可（后续操作中：删除直接全删，修改则无需变动）
            changeList.Add(new LightsChange()
            {
                Operation = EnumOperation.ADD,
                NewLightAst = newLightAst,
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
                // 1.修改lightAstList
                lightAstList[lightIndex].StartNum = startNum;
                lightAstList[lightIndex].EndNum = endNum;
                lightAstList[lightIndex].LightAddr = startNum + "-" + endNum;

                //211028 修改灯具地址时，根据实际情况处理changeList                
                //更改的灯具为旧灯具，才有修改的必要；
                //新加灯具无论怎么改，都是ADD类型的，NewLightAst已指向lightAstList[lightIndex]，无需显式更改changeList内容！
                if (lightIndex < mainForm.LightAstList.Count)
                {
                    int addNum = startNum - mainForm.LightAstList[lightIndex].StartNum;
                    // 当最终改动的值与原列表中的地址一致时，直接还原为NOCHANGE
                    if (addNum == 0)
                    {
                        changeList[lightIndex] = new LightsChange()
                        {
                            Operation = EnumOperation.NOCHANGE,
                        };
                    }
                    // 否则就设为UPDATE，并设置addNum（方便更改数据库）
                    else
                    {
                        changeList[lightIndex] = new LightsChange()
                        {
                            Operation = EnumOperation.UPDATE,
                            LightIndex = lightIndex,
                            NewLightAst = lightAstList[lightIndex],
                            AddNum = addNum,
                        };
                    }
                }

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
                //211028 删除灯具：遍历被删除的灯具，①如果是原来的灯具，则设为空地址；②如果是本次新增的灯具，则直接删除。
                foreach (ListViewItem item in lightsListView.SelectedItems)  //使用遍历item的好处是：其index会自动更新为新的index
                {
                    int delLightIndex = item.Index;    // 关键语句！                
                    if ( mainForm.LightAstList !=null && delLightIndex < mainForm.LightAstList.Count)
                    {
                        lightAstList[delLightIndex].StartNum = 0;
                        lightAstList[delLightIndex].EndNum = 0;
                        lightAstList[delLightIndex].LightAddr = "";
                        lightsListView.Items[delLightIndex].SubItems[2].Text = "";
                        changeList[delLightIndex] = new LightsChange()
                        {
                            Operation = EnumOperation.DELETE,
                            LightIndex = delLightIndex,
                            NewLightAst = null
                        };
                    }
                    else
                    {
                        changeList.RemoveAt(delLightIndex);
                        lightAstList.RemoveAt(delLightIndex);
                        lightsListView.Items.RemoveAt(delLightIndex);
                    }
                }
                lightsListView.Refresh();　// 最后才执行，避免重复刷新
            }
        }

        #endregion

        #region 确认操作
        /// <summary>
        ///  事件：点击《确认》后，添加lightAstList到mainForm去，并进行相关操作
        /// --用此lightAstList替代mainForm中的原lightAstList，并顺便删减lightWrapperList和ListView中的灯具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterButton_Click(object sender, EventArgs e)
        {
            if (LightsChange.IsChanged(changeList))
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

        #region 辅助方法

        /// <summary>
        /// 辅助方法：检测传进来的起始地址和截止地址（同时添加多个灯具时，也会有这两个地址【第一个灯的起始地址和最后一个灯的截止地址】）
        /// ，是否已被当前灯具所占用；
        ///     若传进来的lightIndex==-1；则表示新加灯具，否则为修改旧灯具，先从表中删除旧灯具
        /// </summary>
        /// <param name="startAddr"></param>
        /// <param name="endAddr"></param>
        public bool CheckAddrAvailable(int lightIndex, int startAddr, int endAddr)
        {
            HashSet<int> addrSet = new HashSet<int>();
            for (int tempLightIndex = 0; tempLightIndex < lightAstList.Count; tempLightIndex++)
            {
                if (tempLightIndex != lightIndex)
                {
                    LightAst la = lightAstList[tempLightIndex];
                    for (int j = la.StartNum; j <= la.EndNum; j++)
                    {
                        addrSet.Add(j); //注意：此处0也可能加入到set中
                    }
                }
            }
            for (int addr = startAddr; addr <= endAddr; addr++)
            {
                if (addrSet.Contains(addr))
                {
                    return false;
                }
            }
            return true;
        }

        #endregion
    
    }
}