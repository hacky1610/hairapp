using System;
using Android.Content;
using AndroidX.Work;

namespace HairApp.Droid.Alarming
{
    class AlarmWorker:Worker
    {
        readonly Context mContext;

        public AlarmWorker(Context c, WorkerParameters p):base(c,p)
        {
            AndroidLog.WriteLog("Alarmworker initialized");
            mContext = c;
        }

        public override Result DoWork()
        {
            AndroidLog.WriteLog("Alarmworker works");

            var currentHour = DateTime.Now.Hour;
            if (currentHour >= 6)
            {
                AndroidLog.WriteLog("It is past 6");
                new AlarmNotify().Notify(mContext);
            }

            if (currentHour >= 14)
            {
                AndroidLog.WriteLog("It is past 14");
                new ReminderNotfiy().Notify(mContext);
            }
            return Result.InvokeSuccess();
        }

        public static void Create()
        {
            PeriodicWorkRequest alarmWorker = PeriodicWorkRequest.Builder.From<AlarmWorker>(TimeSpan.FromMinutes(15)).Build();
            WorkManager.Instance.Enqueue(alarmWorker);
        }


    }
}
