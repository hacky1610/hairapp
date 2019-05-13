using System;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using HairApp.Droid;

[Service(Label = "AlarmService", Icon = "@drawable/Icon")]
public class AlarmService : Service
    {
        public int counter = 0;
        private Timer timer;
        public AlarmService(Context applicationContext) : base()
        {
        }

        public AlarmService()
        {
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            AndroidLog.WriteLog("Service started");
            base.OnStartCommand(intent, flags, startId);
            startTimer();
            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            AndroidLog.WriteLog("Service Destroyed");
            base.OnDestroy();
            Intent broadcastIntent = new Intent(this, typeof(AlarmServiceRestarter));

            SendBroadcast(broadcastIntent);
            stoptimertask();
        }

        public void startTimer()
        {
            var oneHour = 1000 * 60 * 60;
            timer = new Timer(Run, null, oneHour, oneHour);

        }

        private void Run(Object stateInfo)
        {
            var currentHour = DateTime.Now.Hour;
            AndroidLog.WriteLog("Timer event");

            if (currentHour == 8)
            {
                AndroidLog.WriteLog("It is 8 o clock - call alarm");

                 var alarm = new AlarmReceiver();
                alarm.Notify(this.ApplicationContext);
            }
            else if (currentHour == 18)
            {
                AndroidLog.WriteLog("It is 18 o clock - call reminder");

                var reminder = new ReminderReceiver();
                reminder.Notify(this.ApplicationContext);
            }
        }

        /**
         * not needed
         */
        public void stoptimertask()
        {
            //stop the timer, if it's not already null
            if (timer != null)
            {
                 timer.Dispose();
                timer = null;
            }
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

    }

