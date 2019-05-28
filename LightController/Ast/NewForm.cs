using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SQLAst;


using System.Security.Cryptography;
using LightController.Ast;
using System.Data.SQLite;

namespace LightController
{
	public partial class NewForm : Form
	{
		private	MainForm mainForm;
		public NewForm(MainForm mainForm)
		{
			this.mainForm = mainForm;
			InitializeComponent();
		}

		private void enterButton_Click(object sender, EventArgs e)
		{
			string s = textBox1.Text;		   
			if (!String.IsNullOrEmpty(s))
			{
				string directoryPath = "C:\\Temp\\LightProject\\" + s;
				DirectoryInfo di = new DirectoryInfo(directoryPath);
				if (di.Exists)
				{
					MessageBox.Show("这个名称已经被使用了，请使用其他名称。");
					return;
				}
				else
				{
					// 1.由新建时取的项目名，来新建相关文件夹
					di.Create();
					// 2.将相关global.ini和data.db3拷贝到文件夹内-->或者新建一个数据库文件	
					string sourcePath = Application.StartupPath;
					string dbFile = directoryPath + @"\data";
					File.Copy(sourcePath + @"\data.db3", directoryPath + @"\data");
					File.Copy(sourcePath + @"\global.ini", directoryPath + @"\global.ini");

					SQLiteHelper sqlHelper = new SQLiteHelper(dbFile);
					sqlHelper.Connect();

					// sqlHelper.ChangePassword(MD5Ast.MD5(dbFile));
					
					//向数据库中user表中插入了一条(name = "马兆瑞"，age = 21)的记录
					string insert_sql = "insert into user(name,age) values(?,?)";        //插入的SQL语句(带参数)
					SQLiteParameter[] para = new SQLiteParameter[2];                        //构造并绑定参数
					para[0] = new SQLiteParameter("name", "马朝旭");
					para[1] = new SQLiteParameter("age", 21); 

					int ret = sqlHelper.ExecuteNonQuery(insert_sql, para); //返回影响的行数

					// 查询并输入表数据
					string select_sql = "select * from user";                            //查询的SQL语句
					DataTable dt = sqlHelper.ExecuteDataTable(select_sql, null);               //执行查询操作,结果存放在dt中
					Console.WriteLine("++++" + dt.ToString());

					MessageBox.Show("成功新建项目");
					this.Dispose();
				}
			}
			else
			{
				MessageBox.Show("请输入项目名");
				return;
			}
		}
		


		private void cancelButton_Click(object sender, EventArgs e)
		{
			this.Dispose();
		}
	}
}
