using Android.App.Job;
using Android.Content;

namespace HairApp.Droid
{
    class JobUtil

    {
        //https://www.vogella.com/tutorials/AndroidTaskScheduling/article.html
        //https://devblogs.microsoft.com/xamarin/replacing-services-jobs-android-oreo-8-0/
        public static void scheduleJob(Context context)
        {
            Java.Lang.Class javaClass = Java.Lang.Class.FromType(typeof(AlarmJob));
            ComponentName serviceComponent = new ComponentName(context, javaClass);
            JobInfo.Builder builder = new JobInfo.Builder(0, serviceComponent);
            //builder.SetMinimumLatency(10 * 60 * 1000); // wait at least
            builder.SetPeriodic(JobInfo.MinPeriodMillis);
            builder.SetPersisted(true);
            builder.SetRequiredNetworkType(NetworkType.Any); // device should be idle
            builder.SetRequiresBatteryNotLow(false);
            builder.SetRequiresCharging(false);
            builder.SetRequiresDeviceIdle(false);
            builder.SetRequiresStorageNotLow(false);
            JobScheduler jobScheduler = (JobScheduler)context.GetSystemService(Context.JobSchedulerService);
            jobScheduler.Schedule(builder.Build());
         
        }


}
}