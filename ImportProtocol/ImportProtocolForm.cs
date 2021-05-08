using ImportProtocol.Common;
using ImportProtocol.Entity;
using NHibernate;
using NHibernate.Cfg;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImportProtocol
{
	public partial class ImportProtocolForm : Form
	{
		private string protocolXlsPath = Application.StartupPath + @"\Protocol\Controller.xls"; //默认的中控配置文件路径
		private HSSFWorkbook xlsWorkbook;  // 通过本对象实现相应的xls文件的映射

		public ImportProtocolForm()
		{
			InitializeComponent();
			loadProtocols();
		}

		private void loadProtocols() {

			try
			{
				protocolComboBox.Items.Clear();
				protocolComboBox.SelectedIndex = -1; //此处不触发保存协议选择的事件
				
				// 由xls文件加载协议列表；
				using (FileStream file = new FileStream(protocolXlsPath, FileMode.Open, FileAccess.Read))
				{
					xlsWorkbook = new HSSFWorkbook(file);
				}
				for (int protocolIndex = 0; protocolIndex < xlsWorkbook.NumberOfSheets; protocolIndex++)
				{
					ISheet sheet = xlsWorkbook.GetSheetAt(protocolIndex);
					protocolComboBox.Items.Add(sheet.SheetName);
				}

				// 加载所有pbin文件；
				FileInfo[] pbinArray = new DirectoryInfo(Application.StartupPath + @"\Protocol\").GetFiles("*.pbin");
				if (pbinArray.Length > 0)
				{
					protocolComboBox.Items.Add("================");
					foreach (FileInfo pbin in pbinArray)
					{
						protocolComboBox.Items.Add(pbin.Name.Substring(0, pbin.Name.LastIndexOf(".pbin")));
					}
				}

				protocolComboBox.SelectedIndex = 0;

				//// 主动选中传入的协议index
				//if (protocolComboBox.Items.Count > selectedProtocolIndex)
				//{
				//	protocolComboBox.SelectedIndex = selectedProtocolIndex;
				//}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		/// 事件：关闭程序时，顺手把SessionFactory关闭
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ImportProtocolForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			DBUtils.CloseFactory();
		}

		/// <summary>
		/// 事件：点击《导入协议》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void importButton_Click(object sender, EventArgs e)
		{
			if (protocolComboBox.SelectedIndex == -1 || protocolComboBox.SelectedIndex == xlsWorkbook.NumberOfSheets)
			{
				setNotice("请选择可用协议（不要选择分隔符========）。", true);
				return;
			}

			setNotice("正在导入("+protocolComboBox.Text+")协议，请稍候...", false);
			setBusy(true);

			CCEntity ccEntity = new CCEntity();
			IList<CCData> ccdList = new List<CCData>();
			int ccIndex;
			string protocolName = "";
			
			// 选中xls中协议
			if (protocolComboBox.SelectedIndex < xlsWorkbook.NumberOfSheets)
			{
				ccIndex = protocolComboBox.SelectedIndex + 101;
				ccEntity.CCIndex = ccIndex ;

				ISheet sheet = xlsWorkbook.GetSheetAt(protocolComboBox.SelectedIndex);
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
				while (rows.MoveNext())
				{
					row = (HSSFRow)rows.Current;

						CCData ccData = new CCData();
						cell = row.GetCell(0);
						ccData.Function = (cell == null ? "" : cell.ToString().Trim());
						cell = row.GetCell(1);
						CCData_PK ccdPK = new CCData_PK
						{
							CCIndex = ccIndex,
							Code = int.Parse( cell.ToString().Trim(), System.Globalization.NumberStyles.HexNumber) 
						};
						ccData.PK = ccdPK;

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

						ccdList.Add(ccData);						
					}
					protocolName = "excel表格中的【" + protocolComboBox.Text + "】协议";
			}
			// 选中本地协议
			else
			{
				try
				{
					ccEntity = (CCEntity)SerializeUtils.DeserializeToObject(Application.StartupPath + @"\protocol\" + protocolComboBox.Text + ".pbin");
					protocolName = "用户另存的【" + protocolComboBox.Text + "】协议";
				}
				catch (Exception)
				{
					ccEntity = null;
					setNotice( "用户另存的【" + protocolComboBox.Text + "】协议损坏，无法生成CC，请重选协议。",  true);					
					setBusy(true);
				}
			}
					   
			if (ccEntity != null)
			{				
				ISession session = DBUtils.GetDBSession();				
				ITransaction transaction = null;

				DateTime beforeDT = System.DateTime.Now;
				try
				{
					// 开启事务
					transaction = session.BeginTransaction();

					session.SaveOrUpdate(ccEntity);
					for (int ccdIndex = 0; ccdIndex < ccdList.Count; ccdIndex++)
					{
						session.SaveOrUpdate(ccdList[ccdIndex]);
					}

					// 提交事务
					transaction.Commit();
				}
				catch (Exception ex)
				{
					setNotice("导入协议发生异常："+ex.Message, true);
					setBusy(true);
					return;
				}
				finally
				{
					if (transaction != null)	{
						transaction.Dispose();
					}
					if (session != null) {
						session.Close();
					}
				}
								
				DateTime afterDT = System.DateTime.Now;
				TimeSpan ts = afterDT.Subtract(beforeDT);
				Text = "导入协议到数据库(本次耗时" + ts.TotalSeconds.ToString("#0.00") + "s)";

			}
			setNotice(  "已将(" + protocolName + ")导入到数据库。",  true);
			setBusy(false);

			//	// MARK3 0420 如果选择了一个可以用的cc，则保存到注册表
			//	Properties.Settings.Default.protocolIndex = protocolComboBox.SelectedIndex;
			//	Properties.Settings.Default.Save();
			
		}

		/// <summary>
		/// 事件：点击《导入全部》
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void importAllButton_Click(object sender, EventArgs e)
		{
			setNotice("即将导入全部" + xlsWorkbook.NumberOfSheets + "个协议(xls)，请稍候...", false);
			setBusy(true);
			DateTime beforeDT = System.DateTime.Now;

			ISession session = DBUtils.GetDBSession();
			ITransaction transaction = null;
			try
			{
				transaction = session.BeginTransaction();

				for (int sheetIndex = 0; sheetIndex < xlsWorkbook.NumberOfSheets; sheetIndex++)
				{
					setNotice("正在导入协议【" + xlsWorkbook.GetSheetName(sheetIndex) + "】，请稍候...", false);					

					CCEntity ccEntity = new CCEntity();
					IList<CCData> ccdList = new List<CCData>();
					int ccIndex = sheetIndex + 101;
					ccEntity.CCIndex = ccIndex;					

					ISheet sheet = xlsWorkbook.GetSheetAt(sheetIndex);
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
					while (rows.MoveNext())
					{
						row = (HSSFRow)rows.Current;

						CCData ccData = new CCData();
						cell = row.GetCell(0);
						ccData.Function = (cell == null ? "" : cell.ToString().Trim());
						cell = row.GetCell(1);
						CCData_PK ccdPK = new CCData_PK
						{
							CCIndex = ccIndex,
							Code = int.Parse(cell.ToString().Trim(), System.Globalization.NumberStyles.HexNumber)
						};
						ccData.PK = ccdPK;

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

						ccdList.Add(ccData);
					}

					// 存到数据库
					session.SaveOrUpdate(ccEntity);
					for (int ccdIndex = 0; ccdIndex < ccdList.Count; ccdIndex++)
					{
						session.SaveOrUpdate(ccdList[ccdIndex]);
					}
				}

				transaction.Commit();
			}
			catch (Exception ex)
			{
				setNotice("导入协议发生异常：" + ex.Message, true);
				setBusy(true);
				return;
			}
			finally
			{
				if (transaction != null) transaction.Dispose();
				if (session != null) session.Close();
			}

			DateTime afterDT = System.DateTime.Now;
			TimeSpan ts = afterDT.Subtract(beforeDT);
			Text = "导入协议到数据库(全部耗时" + ts.TotalSeconds.ToString("#0.00") + "s)";

			setNotice("已将xls内全部协议导入到数据库。", true);
			setBusy(false);
		}
		
		#region 通用方法

		/// <summary>
		/// 辅助方法：按要求显示提示
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="isMsbShow"></param>
		private void setNotice( string msg, bool isMsbShow)
		{
			myStatusLabel.Text = msg;
			Refresh();
			if (isMsbShow) MessageBox.Show(msg);
		}

		private void setBusy(bool busy) {
			Enabled = !busy ;
			Cursor.Current = busy?Cursors.WaitCursor:Cursors.Default;
		}
			   		 
		#endregion

		
	}
}
