using System;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using HairAppBl.Controller;

namespace HairApp.Droid.Alarming
{
    public class Notify
    {
        #region Members
        protected AlarmController mAlarmController;
        static readonly string CHANNEL_ID = "hairapp_notification";
        public static readonly string WASHDAYID = "washday_id";
        #endregion

        #region Constructor
        public Notify()
        {
            var fileDb = new FileDB(HairAppBl.Constants.SchedulesStorageFile);
            var historyfileDb = new FileDB(HairAppBl.Constants.HistoryStorageFile);
            var settingsDb = new FileDB(HairAppBl.Constants.SettingsStorageFile);
            mAlarmController = new AlarmController(fileDb, historyfileDb, settingsDb);

            SetCulture(mAlarmController.GetCulture().Culture);
        }
        #endregion

        protected void SetCulture(String culture)
        {
            //Todo:Usa a variable for the language
            var ci = new System.Globalization.CultureInfo(culture);
            var loc = new UsingResxLocalization.Android.Localize();
            loc.SetLocale(ci);
        }

        protected void CreateNotificationChannel(Context context)
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
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

        protected static void SendNotify(Context context, string washDayId, string title, string content)
        {
            try
            {
                // When the user clicks the notification, SecondActivity will start up.
                var resultIntent = new Intent(context, typeof(MainActivity));
                resultIntent.PutExtra(WASHDAYID, washDayId);

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