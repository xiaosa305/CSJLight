using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernateSQLiteDemo.Entities;
using DMX512;

namespace NHibernateSQLiteDemo
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine("Gege");
			Console.WriteLine("Son23433332");
			// 读取配置
			var config = new Configuration().Configure("Database.xml");

			// 创建表结构 --> 其实就是按照映射文件和class来创建新表（如有旧表就覆盖）
			//SchemaMetadataUpdater.QuoteTableAndColumns(config);
			//new SchemaExport(config).Create(false, true);



			// 打开Session
			var sessionFactory = config.BuildSessionFactory();
			using (var session = sessionFactory.OpenSession())
			{
				// 插入
				//var user = new User
				//{
				//	Name = "贼寇在何方2",
				//	Password = "********",
				//	Email = "realh3haha@gmail.com"
				//};

				//session.Save(user);

				//DB_Light light = new DB_Light();
				//light.LightNo = 1;
				//light.Name = "Hello";
				//light.Pic = "DD";
				//light.StartID = 1;
				//light.Type = "EEE";
				//light.Count = 1;
				
				//session.Save(light);
				//session.Flush();
				
				//查询
			 //  var userNow = session.Query<User>().FirstOrDefault();
				//Console.WriteLine(userNow.Name);

				DB_Light l2 = session.Get<DB_Light>(new Guid("E81CF558-A774-4CEC-A16B-C4F06739CC4E"));
				Console.WriteLine(l2.ToString());

				// 修改
				//usernow.name = "贼寇";
				//usernow.email = "dickov";


				//session.Flush();

				// 删除就省了吧
			}

			Console.WriteLine("完成");
			Console.ReadKey();
		}
	}
}
