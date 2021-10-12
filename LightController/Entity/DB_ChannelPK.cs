namespace LightController.Entity
{
    public class DB_ChannelPK
    {
        public virtual int LightID { get; set; }
        public virtual int ChannelID { get; set; }
        public virtual int Scene { get; set; }
        public virtual int Mode { get; set; }

		/// <summary>
		/// 判断两个对象是否相同，这个方法需要重写
		/// </summary>
		/// <param name="obj">进行比较的对象</param>
		/// <returns>真true或假false</returns>
		public override bool Equals(object obj)
		{
			if (obj is DB_ChannelPK)
			{
				DB_ChannelPK pk = obj as DB_ChannelPK;
				if (LightID == pk.LightID
					&& ChannelID == pk.ChannelID
					&& Scene == pk.Scene 
					&& Mode == pk.Mode)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 联合主键类，这个方法也必须重写，哪怕不改动其内容或只是从base中执行方法
		/// </summary>
		/// <returns>基类的GetHashCode()值</returns>
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

        public override string ToString()
        {
			return LightID + " - " + ChannelID + " - " + Scene + " - " + Mode;
		}
    }
}
