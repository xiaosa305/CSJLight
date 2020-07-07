using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightDog.tools
{
    public class SerialPortReceiveQueue
    {
        private Queue<byte[]> ReceiveQueue { get; set; }

        public SerialPortReceiveQueue()
        {
            this.ReceiveQueue = new Queue<byte[]>();
        }

        public bool Enqueue(byte[] data)
        {
            try
            {
                this.ReceiveQueue.Enqueue(data);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public byte[] Dequeue()
        {
            try
            {
                if (this.ReceiveQueue.Count != 0)
                {
                    return this.ReceiveQueue.Dequeue();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}
