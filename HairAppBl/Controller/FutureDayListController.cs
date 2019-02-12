using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HairAppBl.Interfaces;
using HairAppBl.Models;
using Newtonsoft.Json;

namespace HairAppBl.Controller
{
    public class FutureDayListController
    {
        Dictionary<DateTime, List<Models.WashingDayDefinition>> mList;

        public FutureDayListController()
        {
            mList = new Dictionary<DateTime, List<WashingDayDefinition>>();
        }

        public void Add(WashingDayDefinition def, List<DateTime> dates)
        {
            foreach(var d in dates)
            {
                if(mList.ContainsKey(d))
                {
                    mList[d].Add(def);
                }
                else
                {
                    mList.Add(d, new List<WashingDayDefinition>() { def});
                }
            }
        }

        public Dictionary<DateTime, List<Models.WashingDayDefinition>> GetAllDays()
        {
            return mList;
        }


    }
}
