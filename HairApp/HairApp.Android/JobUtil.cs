using Android.App.Job;
using Android.Content;
using System.Threading;

namespace HairApp.Droid
{
    class JobUtil
    {
        //https://www.vogella.com/tutorials/AndroidTaskScheduling/article.html
        //https://devblogs.microsoft.com/xamarin/replacing-services-jobs-android-oreo-8-0/
        public static void scheduleJob(Context context)
        {
            for (int i = 1; i < 12; i++)
            {
                AndroidLog.WriteLog($"Create job {i}");

                CreateJob(context, i);
            }
        }


        private static void CreateJob(Context context,int minutesToWait)
        {
            Java.Lang.Class javaClass = Java.Lang.Class.FromType(typeof(AlarmJob));
            ComponentName serviceComponent = new ComponentName(context, javaClass);
            JobInfo.Builder builder = new JobInfo.Builder(minutesToWait, serviceComponent);
            builder.SetPeriodic(JobInfo.MinPeriodMillis + 60000 * minutesToWait);
            builder.SetPersisted(true);
            builder.SetRequiredNetworkType(NetworkType.Any); // device should be idle
            JobScheduler jobScheduler = (JobScheduler)context.GetSystemService(Context.JobSchedulerService);
            jobScheduler.Schedule(builder.Build());
        }


}
}