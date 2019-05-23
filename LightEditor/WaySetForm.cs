using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightEditor
{
	public partial class WaySetForm : Form
	{
		// 传入一个对主Form的引用
		public MainForm mainForm;

		public WaySetForm(MainForm mainForm)
		{
			this.mainForm = mainForm;
			InitializeComponent();
			SelfInitializeComponent();
		}

		// 自定义添加辅助组件
		private void SelfInitializeComponent()
		{
			this.textBoxes[0] = _01ztg1TextBox1;
			this.textBoxes[1] = _01ztg1TextBox2;
			this.textBoxes[2] = _02ps1TextBox1;
			this.textBoxes[3] = _02ps1TextBox2;
			this.textBoxes[4] = _03tap1TextBox1;
			this.textBoxes[5] = _03tap1TextBox2;
			this.textBoxes[6] = _04taxz1TextBox1;
			this.textBoxes[7] = _04taxz1TextBox2;
			this.textBoxes[8] = _05ysp1TextBox1;
			this.textBoxes[9] = _05ysp1TextBox2;
			this.textBoxes[10] = _06red1TextBox1;
			this.textBoxes[11] = _06red1TextBox2;
			this.textBoxes[12] = _07green1TextBox1;
			this.textBoxes[13] = _07green1TextBox2;
			this.textBoxes[14] = _08blue1TextBox1;
			this.textBoxes[15] = _08blue1TextBox2;
			this.textBoxes[16] = _09white1TextBox1;
			this.textBoxes[17] = _09white1TextBox2;
			this.textBoxes[18] = _10xTextBox1;
			this.textBoxes[19] = _10xTextBox2;
			this.textBoxes[20] = _11yTextBox1;
			this.textBoxes[21] = _11yTextBox2;
			this.textBoxes[22] = _12xwTextBox1;
			this.textBoxes[23] = _12xwTextBox2;
			this.textBoxes[24] = _13ywTextBox1;
			this.textBoxes[25] = _13ywTextBox2;
			this.textBoxes[26] = _14xyTextBox1;
			this.textBoxes[27] = _14xyTextBox2;
			this.textBoxes[28] = _15gjfdTextBox1;
			this.textBoxes[29] = _15gjfdTextBox2;
			this.textBoxes[30] = _16gjsdTextBox1;
			this.textBoxes[31] = _16gjsdTextBox2;
			this.textBoxes[32] = _17gnTextBox1;
			this.textBoxes[33] = _17gnTextBox2;
			this.textBoxes[34] = _18msTextBox1;
			this.textBoxes[35] = _18msTextBox2;
			this.textBoxes[36] = _19zzTextBox1;
			this.textBoxes[37] = _19zzTextBox2;
			this.textBoxes[38] = _20fwTextBox1;
			this.textBoxes[39] = _20fwTextBox2;
			this.textBoxes[40] = _21xg1TextBox1;
			this.textBoxes[41] = _21xg1TextBox2;
			this.textBoxes[42] = _22xg2TextBox1;
			this.textBoxes[43] = _22xg2TextBox2;
			this.textBoxes[44] = _23ztg2TextBox1;
			this.textBoxes[45] = _23ztg2TextBox2;
			this.textBoxes[46] = _24ps2TextBox1;
			this.textBoxes[47] = _24ps2TextBox2;
			this.textBoxes[48] = _25tap2TextBox1;
			this.textBoxes[49] = _25tap2TextBox2;
			this.textBoxes[50] = _26taxz2TextBox1;
			this.textBoxes[51] = _26taxz2TextBox2;
			this.textBoxes[52] = _27ysp2TextBox1;
			this.textBoxes[53] = _27ysp2TextBox2;
			this.textBoxes[54] = _28red2TextBox1;
			this.textBoxes[55] = _28red2TextBox2;
			this.textBoxes[56] = _29green2TextBox1;
			this.textBoxes[57] = _29green2TextBox2;
			this.textBoxes[58] = _30blue2TextBox1;
			this.textBoxes[59] = _30blue2TextBox2;
			this.textBoxes[60] = _31white2TextBox1;
			this.textBoxes[61] = _31white2TextBox2;
			this.textBoxes[62] = _32ljTextBox1;
			this.textBoxes[63] = _32ljTextBox2;
			this.textBoxes[64] = _33ljxzTextBox1;
			this.textBoxes[65] = _33ljxzTextBox2;
			this.textBoxes[66] = _34whTextBox1;
			this.textBoxes[67] = _34whTextBox2;
			this.textBoxes[68] = _35sfTextBox1;
			this.textBoxes[69] = _35sfTextBox2;
			this.textBoxes[70] = _36jjTextBox1;
			this.textBoxes[71] = _36jjTextBox2;
			this.textBoxes[72] = _37gqTextBox1;
			this.textBoxes[73] = _37gqTextBox2;
			this.textBoxes[74] = _38maxVolumeTextBox1;
			this.textBoxes[75] = _38maxVolumnTextBox2;
			this.textBoxes[76] = _39minVolumeTextBox1;
			this.textBoxes[77] = _39minVolumeTextBox2;
			this.textBoxes[78] = _40maxICTextBox1;
			this.textBoxes[79] = _40maxICTextBox2;
			this.textBoxes[80] = _41speedTextBox1;
			this.textBoxes[81] = _41speedTextBox2;
			this.textBoxes[82] = _42other1TextBox1;
			this.textBoxes[83] = _42other1TextBox2;
			this.textBoxes[84] = _43other2TextBox1;
			this.textBoxes[85] = _43other2TextBox2;
			this.textBoxes[86] = _44other3TextBox1;
			this.textBoxes[87] = _44other3TextBox2;
			
		}



		/// 主要需要验证
		private void confirmButton_Click(object sender, EventArgs e)
		{   
			setDataWrappers();
			mainForm.generateVScrollBars(newTongdaoCount);

			//关闭窗口(隐藏即可)
			this.Hide();
		}

		/// 在新建或更改灯的数据值时，调用这个方法
		private void setDataWrappers()
		{
			for (int i = 0; i < newTongdaoCount; i++)
			{
				for (int j = 0; j < newTongdaoCount; j++)
				{
					if (int.Parse(textBoxes[j * 2].Text) == (i + 1))
					{
						string tongdaoName = textBoxes[j * 2].Tag.ToString();
						int initValue = int.Parse(textBoxes[j * 2 + 1].Text.ToString());
						mainForm.dataWrappers[i] = new DataWrapper(tongdaoName, initValue, i + 1);
						break; //跳出里面的for
					}
				}
			}
		}

		/// 这个方法-->
		/// 1.如果dataWrapper尚未有值(新建灯),则传入tongdaoCount,由wsForm生成默认值，再回调生成dataWrappers
		/// 2.如果dataWrapper已经有值(打开灯.ini),则将该数据填进wsForm的textBoxes
		internal void setTongdaoCount(int tongdaoCount)	{
			// 只有dataWrappers为空时，按照通道Count的数量来生成数据;并生成dataWrappers数据
			newTongdaoCount = tongdaoCount;
			if (mainForm.dataWrappers == null || mainForm.dataWrappers.Length == 0 ) 
			{
				// MessageBox.Show("dataWrappers为空");
				mainForm.dataWrappers = new DataWrapper[tongdaoCount];
				for (int i = 0; i < newTongdaoCount; i++)
				{
					this.textBoxes[i * 2].Text = (i + 1).ToString();
					this.textBoxes[i * 2 + 1].Text = "0";
				}
				setDataWrappers();
			}
			// 若dataWrappers不是为空，则按里面的数据来安排
			else {
				// 清空所有输入框
				foreach (TextBox textBox in textBoxes)
				{
					textBox.Text = "";
				}
				for (int i = 0; i < textBoxes.Length; i = i + 2)
				{
					
					foreach (DataWrapper wrapper in mainForm.dataWrappers)
					{
						// 注意Tag默认不是string，是object
						if (textBoxes[i].Tag.ToString() == wrapper.TongdaoName)
						{
							textBoxes[i].Text = wrapper.Address.ToString();
							textBoxes[i + 1].Text = wrapper.InitNum.ToString();
							//MessageBox.Show("textBoxes["+ i +"].Text = "+wrapper.Address.ToString());
							//MessageBox.Show("textBoxes["+ (i + 1) +"].Text = "+ wrapper.InitNum.ToString());
							break;
						}
						
					}
				}
			}
		}

	

		// 当form真正显示时，此方法才会运行；即new时不允许，Show()时运行
		private void WaySetForm_Load(object sender, EventArgs e)
		{
			// 只有真正渲染form时，才能对这些textBox进行操作（添加事件）
			for (int i = 0; i < 87; i = i + 2)
			{
				this.textBoxes[i].TextChanged += new System.EventHandler(this.LeftTextBox_TextChanged);
				this.textBoxes[i + 1].TextChanged += new System.EventHandler(this.RightTextBox_TextChanged);
			}
		}


		private void LeftTextBox_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			try
			{
				int textBoxNum = int.Parse(textBox.Text); 
				if (textBoxNum < 0)
				{
					MessageBox.Show("通道地址编号不得小于0");
					return;
				}
				if (textBoxNum > newTongdaoCount) {
					MessageBox.Show("通道地址编号不得大于通道数量");
				}
			}
			catch (Exception ex) {

				textBox.Text = "";
				//TODO ：在下方提示该处只能输入数字 
			}
		}
			
		
		private void RightTextBox_TextChanged(object sender, EventArgs e)
		{
			//MessageBox.Show("右值改动");
		}
	}
}
