using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl
{
    public class ConsoleLogger:Interfaces.ILogger
    {
        public void WriteLine(object value)
        {
            System.Diagnostics.Debug.WriteLine(value);
        }

        public void Call(string value)
        {
            System.Diagnostics.Debug.WriteLine("Call function: " + value);
        }

        public void Error(string value)
        {
            System.Diagnostics.Debug.Fail(value);
        }
    }
}
