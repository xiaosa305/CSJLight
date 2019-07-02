using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightController.Tools
{
    public class DeviceException : ApplicationException
    {
        private string Error { get; set; }
        private Exception Exception { get; set; }

        public DeviceException()
        {

        }

        public DeviceException(string msg) : base(msg)
        {
            this.Error = msg;
        }

        public DeviceException(string msg,Exception exception) : base(msg)
        {
            this.Exception = exception;
            this.Error = msg;
        }

        public string GetError()
        {
            return this.Error;
        }
    }
}
