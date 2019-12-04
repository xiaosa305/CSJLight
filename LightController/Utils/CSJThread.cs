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
        private bool IsUsedState { get; set; }
        public CSJThread()
        {
            IsUsedState = false;
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
        public void Start(WaitCallback w, Object o)
        {
            this.W = w;
            this.O = o;
            this.T = new Thread(new ThreadStart(this.ThreadProc))
            {
                IsBackground = true
            };
            IsUsedState = true;
            this.T.Start();
        }
        ///<summary>
        ///线程执行的方法
        ///</summary>
        private void ThreadProc()
        {
            if (this.W != null && this.O != null)
            {
                this.W(this.O);
            }
            this.W = null;
            this.O = null;
            IsUsedState = false;
        }

        public bool IsUsed()
        {
            return IsUsedState;
        }

        public void Stop()
        {
            try
            {
                if (this.T != null)
                {
                    this.T.Abort();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("线程" + this.T.ManagedThreadId + "关闭");
            }finally
            {
                this.W = null;
                this.O = null;
                this.T = null;
            }
        }
    }
}
