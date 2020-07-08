using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace LightDog.tools
{
    public class Constant
    {
        public static readonly int ACTION_TIMEOUT = 500;
        public static readonly int CHECK_TIMEOUT = 100;

        public static readonly string SUPER_PASSWORD = "TRANSJOY";

        public static readonly string ORDER_SET_PASSWORD = "Set_Password";
        public static readonly string ORDER_SET_TIME = "Set_Time";
        public static readonly string ORDER_CHECK_DEVICE = "Check";
        public static readonly string ORDER_LOGIN = "Login";

        public static readonly string RECEIVE_CHECK = "ack\r\n";
        public static readonly string RECEIVE_SET_PASSWORD = "ack\r\n";
        public static readonly string RECEIVE_SET_TIME = "ack\r\n";
        public static readonly string RECEIVE_LOGIN = "ack\r\n";



        /// <summary>
        /// 克隆类对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="RealObject"></param>
        /// <returns></returns>
        public static T Clone<T>(T RealObject)
        {
            using (Stream objStream = new MemoryStream())
            {
                //利用 System.Runtime.Serialization序列化与反序列化完成引用对象的复制
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objStream, RealObject);
                objStream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(objStream);
            }
        }
        /// <summary>
        /// 克隆对象列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="RealObject"></param>
        /// <returns></returns>
        public static List<T> Clone<T>(List<T> RealObject)
        {
            using (Stream objStream = new MemoryStream())
            {
                //利用 System.Runtime.Serialization序列化与反序列化完成引用对象的复制
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objStream, RealObject);
                objStream.Seek(0, SeekOrigin.Begin);
                return (List<T>)formatter.Deserialize(objStream);
            }

        }
    }
}
