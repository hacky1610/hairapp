using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Interfaces
{
    public interface IHairBl
    {
        ILogger Logger
        {
            get;
            set;
        }
    }
}
