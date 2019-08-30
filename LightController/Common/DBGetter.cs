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

		public DBGetter(string dbFile,bool addPassword) {
			lightDAO = new LightDAO(dbFile, false);
			stepCountDAO = new StepCountDAO(dbFile, false);
			valueDAO = new ValueDAO(dbFile, false);
			fineTuneDAO = new FineTuneDAO(dbFile, false);
		}

		public DBWrapper getAll() {

			IList<DB_Light> lightList = lightDAO.GetAll();
			IList<DB_StepCount> stepCountList = stepCountDAO.GetAll();
			IList<DB_Value> valueList = valueDAO.GetAll();
			IList<DB_FineTune> fineTuneList = fineTuneDAO.GetAll();

			return new DBWrapper(lightList, stepCountList, valueList, fineTuneList);
		}


	}
}
