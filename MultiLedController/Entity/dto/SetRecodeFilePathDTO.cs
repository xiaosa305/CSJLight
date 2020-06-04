using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.entity.dto
{
    public class SetRecodeFilePathDTO
    {
        public ControlDevice ControlDevice { get; set; }
        public string RecodeFilePath { get; set; }
        public SetRecodeFilePathDTO(ControlDevice controlDevice,string recodeFilePath)
        {
            this.ControlDevice = controlDevice;
            this.RecodeFilePath = recodeFilePath;
        }
        public SetRecodeFilePathDTO()
        {

        }
    }
}
