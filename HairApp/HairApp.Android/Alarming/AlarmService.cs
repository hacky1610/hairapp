using System;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using HairApp.Droid;

[BroadcastReceiver]
public class AlarmService : BroadcastReceiver
{
    public AlarmService(Context applicationContext) : base()
    {
    }

    public AlarmService()
    {
    }

    public override void OnReceive(Context context, Intent intent)
    {
        AndroidLog.WriteLog("AlarmService started");

        var currentHour = DateTime.Now.Hour;
        if (currentHour >= 6)
        {
            AndroidLog.WriteLog("It is past 6");  
            new AlarmNotify().Notify(context);
        }

        if (currentHour >= 14)
        {
            AndroidLog.WriteLog("It is past 14");
            new ReminderNotfiy().Notify(context);
        }

        new Alarm().Init();
    }

}
