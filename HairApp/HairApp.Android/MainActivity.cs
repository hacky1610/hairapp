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
    [Activity(Label = "HairApp", Icon = "@drawable/icon",Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        App myApp;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            myApp = new App();
            LoadApplication(myApp);

            CheckForNotify();

            App.InitAlarms += App_InitAlarms;
        }

        private void App_InitAlarms(object sender, EventArgs e)
        {
            InitAlarms(DateTime.Now, "Foo", "Bar");
        }

        private void CheckForNotify()
        {
            var id = Intent.GetStringExtra("washday_id");
            if (id != null)
            {
                var day =  App.MainSession.GetWashingDayById(id);
                var contr = new HairAppBl.Controller.WashingDayEditorController(day, App.MainSession.GetAllDefinitions());
                var wdInstance = new HairAppBl.Models.WashingDayInstance(id, Guid.NewGuid().ToString(), DateTime.Now, contr.GetRoutineDefinitions());
                myApp.GetNavigation().PushAsync(new WashDayInstance(wdInstance));
            }
        }


        public void InitAlarms(DateTime dateTime, string title, string message)
        {
            Intent alarmIntent = new Intent(Application.Context, typeof(AlarmReceiver));
            alarmIntent.PutExtra("message", message);
            alarmIntent.PutExtra("title", title);
            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)Application.Context.GetSystemService(Context.AlarmService);

            var s = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day +1 , 8,0,0);
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(s);
            var epochDif = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
            var notifyTimeInInMilliseconds = utcTime.AddSeconds(-epochDif).Ticks / 10000;

            //alarmManager.SetRepeating(AlarmType.RtcWakeup, notifyTimeInInMilliseconds, 60001 * 60 , pendingIntent);
            alarmManager.SetRepeating(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime(), 60001 * 30 , pendingIntent);

        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                HairApp.App.BL.Logger.WriteLine("Backpressed of Popup");
            }
        }
    }
}
