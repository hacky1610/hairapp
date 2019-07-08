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
            AndroidLog.WriteLog("Trigger new Alarm");
            Intent alarmIntent = new Intent(Application.Context, typeof(AlarmService));
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);

            alarmManager.SetAndAllowWhileIdle(AlarmType.RtcWakeup, AlarmController.GetAlarmTime(), pendingIntent);
        }

    }
}

