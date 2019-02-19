using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HairAppBl.Interfaces;
using HairAppBl.Models;
using Newtonsoft.Json;

namespace HairAppBl.Controller
{
    public class FutureDayListController<T>
    {
        Dictionary<DateTime, List<T>> mList;

        public FutureDayListController()
        {
            mList = new Dictionary<DateTime, List<T>>();
        }

        public void AddMultiple(T val, List<DateTime> dates)
        {
            foreach(var d in dates)
            {
                Add(val, d);
            }
        }

        public void Add(T val, DateTime date)
        {

            if (mList.ContainsKey(date))
            {
                mList[date].Add(val);
            }
            else
            {
                mList.Add(date, new List<T>() { val });
            }
            
        }

        public Dictionary<DateTime, List<T>> GetAllDays()
        {
            return mList;
        }


    }
}
