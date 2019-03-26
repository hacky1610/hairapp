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
    class NotificationIntentService: IntentService
{
    protected override void OnHandleIntent(Intent intent)
    {
        ShowLocalNotification(ApplicationContext, intent);

        AlarmReceiver.CompleteWakefulIntent(intent);
    }


    private void ShowLocalNotification(Context context, Intent intent)
    {
        // Do something 
    }

    }
}