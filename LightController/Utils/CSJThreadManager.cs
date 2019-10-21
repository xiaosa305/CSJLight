using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LightController.Utils
{
    public class CSJThreadManager
    {
        public static ArrayList threadList = new ArrayList();
        private CSJThreadManager() { }
        private const int THREAMMAX = 512;

        ///<summary>
        ///静态方法，开启或唤醒一个线程去执行指定的回调方法
        ///</summary>
        ///<param name="waitCallback">委托实例</param>
        ///<param name="obj">传递给回调方法的参数</param>
        ///<param name="timeOut">当没有可用的线程时的等待时间，
        ///以毫秒为单位</param>
        ///<returns></returns>
        public static bool QueueUserWorkItem(WaitCallback waitCallback,Object obj,int timeOut)
        {
            //lock (threadList)
            //{
                try
                {
                    //如果线程列表为空，填充线程列表
                    if (threadList.Count == 0)
                    {
                        InitThreadList();
                    }
                    long startTime = DateTime.Now.Ticks;
                    do
                    {
                        //遍历线程列表，找出可用的线程
                        foreach (CSJThread thread in threadList)
                        {
                            if (thread.GetThread == null)
                            {
                                //线程为空，需要创建线程
                                thread.Start(waitCallback, obj, false);
                                return true;
                            }
                            else if (thread.GetThread.ThreadState == ThreadState.Suspended)
                            {
                                //线程为挂起状态，唤醒线程
                                thread.Start(waitCallback, obj, true);
                                return true;
                            }
                        }
                        //在线程 Sleep 前释放锁
                        //Monitor.PulseAll(threadList);
                        Thread.Sleep(500);
                    } while (((DateTime.Now.Ticks - startTime)/10000)<timeOut);
                }
            finally
            {
                //Monitor.Exit(threadList);
            }
            //}
            return false;
        }

        private static void InitThreadList()
        {
            threadList = new ArrayList();
            for (int i = 0; i < THREAMMAX; i++)
            {
                CSJThread thread = new CSJThread();
                threadList.Add(thread);
            }
        }
    }
}
