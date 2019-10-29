using LightController.Tools;
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
        private static ArrayList ChannelThreadList = new ArrayList();
        private static ArrayList SceneThreadList = new ArrayList();
        private CSJThreadManager() { }
        private const int CHANNELTHREAMMAX = 100;
        private const int SCENETHREAMMAX = 5;

        ///<summary>
        ///静态方法，开启或唤醒一个线程去执行指定的回调方法
        ///</summary>
        ///<param name="waitCallback">委托实例</param>
        ///<param name="obj">传递给回调方法的参数</param>
        ///<param name="timeOut">当没有可用的线程时的等待时间，
        ///以毫秒为单位</param>
        ///<returns></returns>
        public static bool QueueChannelUserWorkItem(WaitCallback waitCallback, Object obj, int timeOut)
        {
            try
            {
                //如果线程列表为空，填充线程列表
                if (ChannelThreadList.Count == 0)
                {
                    InitChannelThreadList();
                }
                long startTime = DateTime.Now.Ticks;
                do
                {
                    //遍历线程列表，找出可用的线程
                    foreach (CSJThread thread in ChannelThreadList)
                    {
                        if (thread.GetThread == null)
                        {
                            //线程为空，需要创建线程
                            thread.Start(waitCallback, obj, false);
                            //Console.WriteLine("获取空线程");
                            return true;
                        }
                        else if (thread.GetThread.ThreadState == (ThreadState.Background|ThreadState.Suspended))
                        {
                            //线程为挂起状态，唤醒线程
                            thread.Start(waitCallback, obj, true);
                            //Console.WriteLine("获取挂起线程");
                            return true;
                        }
                    }
                    Thread.Sleep(500);
                } while (((DateTime.Now.Ticks - startTime) / 10000) < timeOut);
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
            return false;
        }
        public static bool QueueSceneUserWorkItem(WaitCallback waitCallback, Object obj, int timeOut)
        {
            try
            {
                //如果线程列表为空，填充线程列表
                if (SceneThreadList.Count == 0)
                {
                    InitSceneThreadList();
                }
                long startTime = DateTime.Now.Ticks;
                do
                {
                    //遍历线程列表，找出可用的线程
                    foreach (CSJThread thread in SceneThreadList)
                    {
                        if (thread.GetThread == null)
                        {
                            //线程为空，需要创建线程
                            thread.Start(waitCallback, obj, false);
                            //Console.WriteLine("获取空线程");
                            return true;
                        }
                        else if (thread.GetThread.ThreadState == (ThreadState.Background | ThreadState.Suspended))
                        {
                            //线程为挂起状态，唤醒线程
                            thread.Start(waitCallback, obj, true);
                            //Console.WriteLine("获取挂起线程");
                            return true;
                        }
                    }
                    Thread.Sleep(500);
                } while (((DateTime.Now.Ticks - startTime) / 10000) < timeOut);
            }
            catch (Exception ex)
            {
                CSJLogs.GetInstance().ErrorLog(ex);
            }
            return false;
        }

        private static void InitChannelThreadList()
        {
            ChannelThreadList = new ArrayList();
            for (int i = 0; i < CHANNELTHREAMMAX; i++)
            {
                CSJThread thread = new CSJThread();
                ChannelThreadList.Add(thread);
            }
        }
        private static void InitSceneThreadList()
        {
            SceneThreadList = new ArrayList();
            for (int i = 0; i < SCENETHREAMMAX; i++)
            {
                CSJThread thread = new CSJThread();
                SceneThreadList.Add(thread);
            }
        }
    }
}
