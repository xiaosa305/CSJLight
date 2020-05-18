using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.multidevice.impl
{
    public class TransactionManager:ITransactionManager
    {
        private static ITransactionManager Instance { get; set; }





        private TransactionManager()
        {

        }
        public static ITransactionManager GetTransactionManager()
        {
            if (Instance == null)
            {
                Instance = new TransactionManager();
            }
            return Instance;
        }





    }
}
