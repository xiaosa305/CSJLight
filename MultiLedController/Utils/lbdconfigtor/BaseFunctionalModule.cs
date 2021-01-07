using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace MultiLedController.utils.lbdconfigtor
{
    public class BaseFunctionalModule
    {
        protected const int TIME_OUT_COUNT = 5000;
        protected System.Timers.Timer TimeOut { get; set; }

        protected void Init()
        {
            this.TimeOut = new Timer(TIME_OUT_COUNT) { AutoReset = false };
            this.TimeOut.Elapsed += this.TimeOutTask;
        }

        protected void StartTimeOutTask()
        {
            this.TimeOut.Start();
        }

        protected void StopTimeOutTask()
        {
            this.TimeOut.Stop();
        }

        protected void TimeOutTask(object sender, ElapsedEventArgs e)
        {

        }

        protected virtual void Send(byte[] data)
        {
            ;
        }

        protected void SendCompleted()
        {

        }

        public void DetectionEquipment()
        {

        }

        protected enum Module
        {
            SearchDevice,
            ReadDeviceId,
            WriteEncrypt,
            UpdateFPGA256,
            UpdataMCU256,
            WriteData,
            WriteParam,
            IsEncrypt,
            SetArtNetSpaceNumber
        }
    }
}
