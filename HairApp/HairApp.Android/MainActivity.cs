using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Globalization;
using HairAppBl.Controller;
using HairAppBl;
using Plugin.CurrentActivity;

namespace HairApp.Droid
{
    [Activity(Label = "HairApp", Icon = "@drawable/icon",Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        App myApp;
        Intent mServiceIntent;
        private AlarmService mAlarmService;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironment_UnhandledExceptionRaiser;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            XamForms.Controls.Droid.Calendar.Init();
            OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();

            myApp = new App(Intent.GetStringExtra("washday_id"));
            LoadApplication(myApp);

            //Media
            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            mAlarmService = new AlarmService(this);
            mServiceIntent = new Intent(this, typeof(AlarmService));
            if (!isMyServiceRunning(typeof(AlarmService)))
            {
                StartService(mServiceIntent);
            }

        }

        private bool isMyServiceRunning(Type serviceClass)
        {
            ActivityManager manager = (ActivityManager)GetSystemService(Context.ActivityService);
            
            var servicess = manager.GetRunningServices(Int32.MaxValue);
            foreach(var s in servicess)
            {
                if (s.Service.ClassName.Contains(serviceClass.FullName))
                {
                    AndroidLog.WriteLog("Service is running");

                    return true;
                }
            }
            AndroidLog.WriteLog("Service is not running");

            return false;
        }

        protected override void OnStart()
        {
            base.OnStart();
            myApp.InitException();
        }

        private void AndroidEnvironment_UnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
                myApp.SendException(e.Exception);
        }
  

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                App.BL.Logger.WriteLine("Backpressed of Popup");
            }
        }

        protected override void OnDestroy()
        {
            AndroidLog.WriteLog("MainActivity destroy");

            StopService(mServiceIntent);
            base.OnDestroy();
        }
    }
}
