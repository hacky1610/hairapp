using System;
using System.Collections.Generic;
using System.Text;
using HairAppBl.Interfaces;

namespace HairAppBl
{
    public class HairAppBl : Interfaces.IHairBl
    {
        public HairAppBl(ILogger logger, IDictionary<string, object> res)
        {
            this.Logger = logger;
            this.Resources = res;
        }

        public ILogger Logger { get; set; }
        public IDictionary<string, object> Resources { get; set; }
    }
 }
