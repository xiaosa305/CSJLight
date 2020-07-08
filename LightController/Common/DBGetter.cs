using DMX512;
using LightController.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Common
{
	class DBGetter
	{
		private LightDAO lightDAO;
		private StepCountDAO stepCountDAO;
		private ValueDAO valueDAO;
		private FineTuneDAO fineTuneDAO;

		public DBGetter(string dbFile,bool isEncrypt) {
			lightDAO = new LightDAO(dbFile, false);
			stepCountDAO = new StepCountDAO(dbFile, false);
			valueDAO = new ValueDAO(dbFile, false);
			fineTuneDAO = new FineTuneDAO(dbFile, false);
		}

		public DBWrapper getAll() {

			DateTime beforeDT = System.DateTime.Now;

			IList<DB_Light> lightList = lightDAO.GetAll();
			IList<DB_StepCount> stepCountList = stepCountDAO.GetAll();
			IList<DB_Value> valueList = valueDAO.GetAll();
			IList<DB_FineTune> fineTuneList = fineTuneDAO.GetAll();

			DateTime afterDT = System.DateTime.Now;
			TimeSpan ts = afterDT.Subtract(beforeDT);
			Console.WriteLine("已打开所有的数据库文件,耗时: " + ts.TotalSeconds.ToString("#0.00") + " s");

			return new DBWrapper(lightList, stepCountList, valueList, fineTuneList);
		}

	}
}
