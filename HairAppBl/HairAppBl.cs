using System;
using System.Collections.Generic;
using System.Text;
using HairAppBl.Interfaces;

namespace HairAppBl
{
    public class HairAppBl:Interfaces.IHairBl
    {
        public HairAppBl(ILogger logger)
        {
            this.Logger = logger;
        }

        public ILogger Logger { get; set; }


    }
}
