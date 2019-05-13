using System;
using Xamarin.Forms;
using System.Threading;
using System.Globalization;
using HairApp.Interfaces;
using Android.Content;
using Android.App;
using Application = Android.App.Application;
using HairAppBl.Controller;

[assembly: Dependency(typeof(HairApp.Droid.Alarm))]

namespace HairApp.Droid
{
    public class Alarm : IAlarm
    {
        public void Init()
        {

        }

        public void InitReminder()
        {
      
        }


    }
}

