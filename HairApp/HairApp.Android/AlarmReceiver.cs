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
using Android.Support.V4.App;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;
using HairAppBl;
using System.IO;
using HairAppBl.Controller;
using Android.Graphics;

namespace HairApp.Droid
{
    [BroadcastReceiver]
    public class AlarmReceiver : BroadcastReceiver
    {
        static readonly string CHANNEL_ID = "hairapp_notification";
        internal static readonly string WASHDAY_ID = "washday_id";

        public override void OnReceive(Context context, Intent intent)
        {
            CreateNotificationChannel(context);
            Notify(context);

            new Alarm().Init();

        }

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

        void Notify(Context context)
        {
            var fileDb = new FileDB(Constants.SchedulesStorageFile);
            var alarmController = new AlarmController(fileDb);
            var washdays = alarmController.GetTodayWashDays();

            foreach (var wd in washdays)
            {
                
                SendNotify(context, wd.ID, HairAppBl.Resources.AppResource.TimeForHairCare ,    $"{HairAppBl.Resources.AppResource.TodayIs} {wd.Name}");
            }
        }

        private static void SendNotify(Context context,string washDayId,string title, string content)
        {

            try
            {

                // Pass the current button press count value to the next activity:
                var valuesForActivity = new Bundle();
                valuesForActivity.PutString(WASHDAY_ID, washDayId);

                // When the user clicks the notification, SecondActivity will start up.
                var resultIntent = new Intent(context, typeof(MainActivity));

                // Pass some values to SecondActivity:
                resultIntent.PutExtras(valuesForActivity);

                // Construct a back stack for cross-task navigation:
                var stackBuilder = TaskStackBuilder.Create(context);
                stackBuilder.AddNextIntent(resultIntent);

                // Create the PendingIntent with the back stack:
                var resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

                var p = PendingIntent.GetActivity(context, DateTime.Now.Millisecond, resultIntent, PendingIntentFlags.UpdateCurrent);

                // Build the notification:
                var builder = new NotificationCompat.Builder(context, CHANNEL_ID)
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
