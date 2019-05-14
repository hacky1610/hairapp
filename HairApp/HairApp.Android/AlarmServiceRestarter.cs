using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HairApp.Droid
{
    [BroadcastReceiver]
    public class AlarmServiceRestarter : BroadcastReceiver
    {

        public override void OnReceive(Context context, Intent intent)
        {
            AndroidLog.WriteLog("AlarmServiceRestarter called");

            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                AndroidLog.WriteLog("Start ForegroundService");
                context.StartForegroundService(new Intent(context, typeof(AlarmService)));
            } else {
                AndroidLog.WriteLog("Start BAckroundService");
                context.StartService(new Intent(context, typeof(AlarmService)));
            }


        }

    }
}