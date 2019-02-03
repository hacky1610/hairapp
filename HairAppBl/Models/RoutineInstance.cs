using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class RoutineInstance
    {
        public Boolean Checked { get; set; }
        public string Name { get; set; }

        public RoutineInstance()
        {
            Checked = false;
        }

    }
}
