﻿using System;
using Android.App;
using Android.App.Job;
namespace HairApp.Droid
{
    [Service(Exported = true, Name = "com.companyname.HairApp.Droid.AlarmJob", Permission = "android.permission.BIND_JOB_SERVICE")]
    public class AlarmJob:JobService
    {
        public override bool OnStartJob(JobParameters @params)
        {
            AndroidLog.WriteLog("Job started");
            //Todo:Usa a variable for the language
            var ci = new System.Globalization.CultureInfo("fr");
            var loc = new UsingResxLocalization.Android.Localize();
            loc.SetLocale(ci);

            var currentHour = DateTime.Now.Hour;
            if(currentHour >= 6)
            {
                 new AlarmReceiver().Notify(ApplicationContext);
            }

            if (currentHour >= 14)
            {
                new ReminderReceiver().Notify(ApplicationContext);
            }
           
            return true;
        }

        public override bool OnStopJob(JobParameters @params)
        {
            return true;
        }


}
}