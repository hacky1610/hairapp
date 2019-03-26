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
    class AlarmIntentService: IntentService
    {
        public AlarmIntentService() : base("AlarmIntentService")
        {
        }

        protected override void OnHandleIntent(Android.Content.Intent intent)
        {
            Console.WriteLine("perform some long running work");
            Console.WriteLine("work complete");
        }
    }
}