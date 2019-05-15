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
            builder.SetMinimumLatency(60 * 60 * 1000); // wait at least
            //builder.SetOverrideDeadline(3 * 1000); // maximum delay
            //builder.setRequiredNetworkType(JobInfo.NETWORK_TYPE_UNMETERED); // require unmetered network
            //builder.setRequiresDeviceIdle(true); // device should be idle
            //builder.setRequiresCharging(false); // we don't care if the device is charging or not
            JobScheduler jobScheduler = (JobScheduler)context.GetSystemService(Context.JobSchedulerService);
            jobScheduler.Schedule(builder.Build());
    }
}
}