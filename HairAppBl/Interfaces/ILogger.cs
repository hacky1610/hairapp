using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Interfaces
{
    public interface ILogger
    {
        void Call(string value);
        void WriteLine(object value);
        void Error(string value);
    }
}
