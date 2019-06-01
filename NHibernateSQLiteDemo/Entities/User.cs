using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateSQLiteDemo.Entities
{
    public class User
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }

		public override string ToString()
		{
			return "Id:" + Id + "\n"
				+ "Name:" + Name + "\n"
				+ "psw:" + Password + "\n"
				+ "Email:" + Email + "\n";
		}
	}
}
