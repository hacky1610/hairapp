using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class SettingsModel
    {
        public String Culture { get;  set; }

        public SettingsModel()
        {
            Culture = "fr";
        }
    }
}
