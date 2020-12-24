using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordTools.utils
{
    public class EditRecordFile
    {
        private static EditRecordFile Instance { get; set; }
        private EditRecordFile()
        {

        }
        public static EditRecordFile GetInstance()
        {
            if (Instance == null)
            {
                Instance = new EditRecordFile();
            }
            return Instance;
        }
    }
}
