using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LightController.Utils
{
    public class CSJThread
    {
        private Thread T { get; set; }
        private WaitCallback W { get; set; }
        private Object O { get; set; }
        public CSJThread()
        {

        }

        ///<summary>

        ///执行回调方法的线程

        ///</summary>

        public Thread GetThread

        {
            get
            {
                return T;
            }

        }
        ///<summary>
        ///开启新线程或唤醒线程，去执行回调方法
        ///</summary>
        ///<param name="w">用回调方法实例化了的委托实例</param>
        ///<param name="o">传递给回调方法的参数值</param>
        ///<param name="isSuspend">true 表示线程为挂起状态，false 则表示线程还没创建</param>

        public void Start(WaitCallback w,Object o,bool isSuspend)
        {
            this.W = w;
            this.O = o;
            if (isSuspend)
            {
                this.T.Resume();
            }
            else
            {
                this.T = new Thread(new ThreadStart(this.ThreadProc))
                {
                    IsBackground = true
                };
                this.T.Start();
            }
        }
        ///<summary>

        ///线程执行的方法

        ///</summary>
        private void ThreadProc()
        {
            //死循环，使线程唤醒后不是退出，而是继续通过委托执行回调方法
            /* while (true)
             {
                 //通过委托执行回调方法
                 if (this.W != null && this.O != null)
                 {
                     this.W(this.O);
                 }
                 //this.T.Suspend();
                 this.W = null;
                 this.O = null;
             }*/

            //通过委托执行回调方法
            if (this.W != null && this.O != null)
            {
                this.W(this.O);
                this.T = null;
            }
        }
    }
}
