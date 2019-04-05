﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;
using HairAppBl;
using System.IO;
using HairAppBl.Controller;
using Android.Graphics;

namespace HairApp.Droid
{
    [BroadcastReceiver]
    public class ReminderReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            Notify(context);

            new Alarm().InitReminder();

        }

        void Notify(Context context)
        {
            var fileDb = new FileDB(Constants.SchedulesStorageFile);
            var alarmController = new AlarmController(fileDb);
            var washdays = alarmController.GetTodayWashDays();

            foreach (var wd in washdays)
            {
                SendNotify(context, wd.ID, "Reminder for Hair Care", $"TOmorrow is: {wd.Name}");
            }
        }

        private static void SendNotify(Context context,string washDayId,string title, string content)
        {

            try
            {

                // When the user clicks the notification, SecondActivity will start up.
                var resultIntent = new Intent(context, typeof(MainActivity));


                // Construct a back stack for cross-task navigation:
                var stackBuilder = TaskStackBuilder.Create(context);
                stackBuilder.AddNextIntent(resultIntent);

                // Create the PendingIntent with the back stack:
                var resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

                var p = PendingIntent.GetActivity(context, DateTime.Now.Millisecond, resultIntent, PendingIntentFlags.UpdateCurrent);

                // Build the notification:
                var builder = new NotificationCompat.Builder(context)
                              .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                              .SetContentIntent(p) // Start up this activity when the user clicks the intent.
                              .SetContentTitle(title) // Set the title
                              .SetNumber(1) // Display the count in the Content Info
                              .SetSmallIcon(Resource.Drawable.icon) // This is the icon to display
                              .SetContentText(content)
                              .SetLargeIcon(BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.icon))
                              ; // the message to display.
               

                // Finally, publish the notification:
                var notificationManager = NotificationManagerCompat.From(context);
                notificationManager.Notify(DateTime.Now.Millisecond, builder.Build());
            }
            catch (Exception e)
            {
            }
          
        }

    }
}
