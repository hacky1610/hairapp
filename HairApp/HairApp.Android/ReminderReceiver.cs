﻿using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using HairAppBl;
using HairAppBl.Controller;
using Android.Graphics;

namespace HairApp.Droid
{
    public class ReminderReceiver 
    {
        static readonly string CHANNEL_ID = "hairapp_reminder_notification";

        void CreateNotificationChannel(Context context)
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var name = "Channel";
            var description = "Foo";
            var channel = new NotificationChannel(CHANNEL_ID, name, NotificationImportance.Default)
            {
                Description = description
            };

            var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        public void Notify(Context context)
        {
            CreateNotificationChannel(context);

            var fileDb = new FileDB(Constants.SchedulesStorageFile);
            var alarmController = new AlarmController(fileDb);
            var washdays = alarmController.GetReminderWashDays();


            if (washdays.Count == 0)
            {
                AndroidLog.WriteLog("Remineder: Tomorrow is no washing day");
                return;
            }

            foreach (var wd in washdays)
            {
                AndroidLog.WriteLog($"Remineder: Tomorrow is washday: {wd.Name} ");
                SendNotify(context, wd.ID, HairAppBl.Resources.AppResource.ReminderForHairCare, $"{HairAppBl.Resources.AppResource.TomorrowIs} {wd.Name}");
            }
        }

        private static void SendNotify(Context context,string washDayId,string title, string content)
        {
            try
            {
                // When the user clicks the notification, SecondActivity will start up.
                var resultIntent = new Intent(context, typeof(MainActivity));
             
                var p = PendingIntent.GetActivity(context, DateTime.Now.Millisecond, resultIntent, PendingIntentFlags.UpdateCurrent);


                // Build the notification:
                var builder = new NotificationCompat.Builder(context, CHANNEL_ID)
                              .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                              .SetContentTitle(title) // Set the title
                             .SetContentIntent(p) // Start up this activity when the user clicks the intent.
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
                AndroidLog.WriteLog($"Error during creation of Alarm Reminder Receiver: {e.StackTrace}");
            }
          
        }

    }
}
