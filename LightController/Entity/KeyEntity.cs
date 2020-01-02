using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Entity
{
	public class KeyEntity
	{
		public string CRC { get; set; }
		public string[] Key0Array { get; set; }
		public string[] Key1Array { get; set; }

		public KeyEntity()
		{
			Key0Array = new string[24];
			Key1Array = new string[24]; 
		}
	}
}
