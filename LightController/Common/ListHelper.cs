using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Common
{
    public class ListHelper
    {
        /// <summary>
        ///  删除IList中的null元素；
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void RemoveNull<T>(IList<T> list)
        {           
            //倒序删除（正序删除时，index会发生变化）
            for (int index = list.Count - 1; index >= 0; index--) {
                if(list[index] == null) {
                    list.RemoveAt(index);
                }            
            }
        }
    }
}
