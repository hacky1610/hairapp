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
            ButtonOnClick(context);
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

        void  ButtonOnClick(Context context)
        {

            var alarmController = new HairAppBl.Controller.AlarmController();
            var washdays = alarmController.GetWashDays();

            if (washdays.Count == 0)
                return;

            var text = $"Routine of today: Conditioning";
            foreach (var wd in washdays)
                SendNotify(context, wd, "Time for Hair Care", text);
        }

        private void SendNotify(Context context,string washDayId,string title, string content)
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

            var props = App.Current.Properties;


            // Build the notification:
            var builder = new NotificationCompat.Builder(context, CHANNEL_ID)
                          .SetAutoCancel(true) // Dismiss the notification from the notification area when the user clicks on it
                          .SetContentIntent(resultPendingIntent) // Start up this activity when the user clicks the intent.
                          .SetContentTitle(title) // Set the title
                          .SetNumber(1) // Display the count in the Content Info
                          .SetSmallIcon(Resource.Drawable.icon) // This is the icon to display
                          .SetContentText(content); // the message to display.

            // Finally, publish the notification:
            var notificationManager = NotificationManagerCompat.From(context);
            notificationManager.Notify(DateTime.Now.Millisecond, builder.Build());
        }

    }
}
