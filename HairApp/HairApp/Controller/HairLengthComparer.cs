using System;
using System.Collections.Generic;
using System.Text;
using HairAppBl.Models;

namespace HairApp.Controller
{
    public class HairLengthComparer : IComparer<HairLength>
    {
        public int Compare(HairLength x, HairLength y)
        {
            return x.Day.CompareTo(y.Day);
        }
    }
}
