using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Globalization;

namespace HairApp.Droid
{
    [Activity(Label = "HairApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            Remind(DateTime.Now, "Foo", "Bar");
        }


        public void Remind(DateTime dateTime, string title, string message)
        {
            Intent alarmIntent = new Intent(Application.Context, typeof(AlarmReceiver));
            alarmIntent.PutExtra("message", message);
            alarmIntent.PutExtra("title", title);
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);

            //alarmManager.SetRepeating(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + 5 * 1000, 5000, pendingIntent);
            alarmManager.SetRepeating(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + 1000 * 20, 60000 * 60 * 6, pendingIntent);
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                HairApp.App.BL.Logger.WriteLine("Backpressed of Popup");
            }
            else
            {
                HairApp.App.BL.Logger.WriteLine("Normal Backpressed");

            }
        }
    }
}