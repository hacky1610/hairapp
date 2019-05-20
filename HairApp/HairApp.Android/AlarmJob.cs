using System;
using Android.App;
using Android.App.Job;
namespace HairApp.Droid
{
    [Service(Exported = true, Name = "com.companyname.HairApp.Droid.AlarmJob", Permission = "android.permission.BIND_JOB_SERVICE")]
    public class AlarmJob:JobService
    {

        public override bool OnStartJob(JobParameters @params)
        {
            AndroidLog.WriteLog("Job started");
            var currentHour = DateTime.Now.Hour;
            if(currentHour > 8)
            {
                 new AlarmReceiver().Notify(ApplicationContext);
            }

            if (currentHour > 18)
            {
                new ReminderReceiver().Notify(ApplicationContext);
            }
           

            JobUtil.scheduleJob(ApplicationContext); // reschedule the job
            return true;
        }

        public override bool OnStopJob(JobParameters @params)
        {
            return true;
        }


}
}