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

			// 读取配置
			var config = new Configuration().Configure("Database.xml");
			
			// 创建表结构 --> 其实就是按照映射文件和class来创建新表（如有旧表就覆盖）
			//SchemaMetadataUpdater.QuoteTableAndColumns(config);
			//new SchemaExport(config).Create(true, true);  // script:是否输出到控制台; export：是否根据持久类和映射文件先执行删除再执行创建操作

			// 打开Session
			var sessionFactory = config.BuildSessionFactory();
			using (var session = sessionFactory.OpenSession())
			{
				////插入
				//  var user = new User
				//  {
				//   Name = "贼寇在何方2",
				//   Password = "********",
				//   Email = "realh3haha@gmail.com"
				//  };

				//using (var tx = session.BeginTransaction())
				//{

				//	try
				//	{
				//		session.Save(user);

				//		//int b = 0;
				//		//float a = 10 / b;	

				//		tx.Commit();
				//		//Console.WriteLine("事务提交成功");
				//	}
				//	catch (Exception ex)
				//	{
				//		//Console.WriteLine("故意提交事务失败");
				//		tx.Rollback();
				//	}
				//}

				////查询
				//User user2 = session.Query<User>().FirstOrDefault();
				//Console.WriteLine(user2.Id);
				//User user3 = session.Get<User>(user.Id);
				//Console.WriteLine(user3.Id.ToString().ToUpper());

				//// 修改
				//using (var tx = session.BeginTransaction())
				//{

				//	try
				//	{
				//		user3.Name = "贼寇2";
				//		user3.Email = "Dickov@qq.com";
				//		session.Update(user3);
				//		tx.Commit();
				//	}
				//	catch (Exception ex)
				//	{
				//		tx.Rollback();
				//	}
				//}

				////删除
				//using (var trans = session.BeginTransaction())
				//{
				//	session.Delete(user2);
				//	trans.Commit();
				//}


				//Dickov:测试批量读取数据
				IList<User> userList =
					session.CreateQuery("FROM USER u WHERE u.Name like :Name AND Email = :email")
					.SetString("Name","贼寇%")
					.SetString("email", "Dickov20@qq.com")
					//.SetFirstResult(4)
					//.SetMaxResults(2)
					.List<User>();
				Console.WriteLine(userList.Count);

				foreach (User userTemp in userList)
				{
					Console.WriteLine(userTemp.ToString());
				}

				//int userCount = session.Query<User>().Count<User>();
				//Console.WriteLine("当前User数量:" + userCount);
			}


			Console.ReadKey();
		}
	}
}
