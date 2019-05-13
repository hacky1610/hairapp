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

            context.StartService(new Intent(context,typeof(AlarmService)));
        }

    }
}