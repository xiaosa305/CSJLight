using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SQLite;

namespace SQLAst
{
	class Program
	{
		SQLiteConnection m_dbConnection;

		static void Main(string[] args)
		{
			Program p = new Program();
		}

		public Program()
		{
			//createNewDatabase();
			connectToDatabase("123456");
			//addPassword();
			//createTable();
			fillTable();
			printHighscores();

			//connectToDatabase("123456");
			//deletePassword();

		}

		//创建一个空的数据库
		void createNewDatabase()
		{
			SQLiteConnection.CreateFile("C:\\sqlite3\\myDB527.sqlite");

		}

		//创建一个连接到指定数据库
		void connectToDatabase(string password)
		{
			string conSql = "Data Source=C:\\sqlite3\\myDB527.sqlite";
			if (password != null && password.Trim().Length > 0) {
				conSql += ";password=" + password;
			}

			m_dbConnection = new SQLiteConnection(conSql);
			m_dbConnection.Open();
		}

		//Dickov: 为数据库加密
		void addPassword() {
			m_dbConnection.ChangePassword("123456");
		}
		void deletePassword() {
			m_dbConnection.ChangePassword("");
		}


		//在指定数据库中创建一个table
		void createTable()
		{
			string sql = "create table highscores (name varchar(20), score int)";
			SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
			command.ExecuteNonQuery();
		}

		//插入一些数据
		void fillTable()
		{
			string sql = "insert into highscores (name, score) values ('Me', 3000)";
			SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
			command.ExecuteNonQuery();

			sql = "insert into highscores (name, score) values ('Myself', 6000)";
			command = new SQLiteCommand(sql, m_dbConnection);
			command.ExecuteNonQuery();

			sql = "insert into highscores (name, score) values ('And I', 9001)";
			command = new SQLiteCommand(sql, m_dbConnection);
			command.ExecuteNonQuery();
		}

		//使用sql查询语句，并显示结果
		void printHighscores()
		{
			string sql = "select * from highscores order by score desc";
			SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
			SQLiteDataReader reader = command.ExecuteReader();
			while (reader.Read())
				Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
			Console.ReadLine();
		}
	}
}

