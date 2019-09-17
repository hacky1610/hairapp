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
            CreateJob(context);
        }


        private static void CreateJob(Context context)
        {
            AndroidLog.WriteLog("Init ServiceRestarter");

            Java.Lang.Class javaClass = Java.Lang.Class.FromType(typeof(ServiceRestarter));
            ComponentName serviceComponent = new ComponentName(context, javaClass);
            JobInfo.Builder builder = new JobInfo.Builder(1, serviceComponent);
            builder.SetPeriodic(1000 * 60 * 60 );
            builder.SetPersisted(true);
            builder.SetRequiredNetworkType(NetworkType.Any); // device should be idle
            JobScheduler jobScheduler = (JobScheduler)context.GetSystemService(Context.JobSchedulerService);
            var jobs = jobScheduler.AllPendingJobs.Count;
            if(jobs == 0)
            {
                AndroidLog.WriteLog("Schedule");
                jobScheduler.Schedule(builder.Build());
            }
            else
            {
                AndroidLog.WriteLog($"NO Schedule. Running jobs {jobs}");
                jobScheduler.Schedule(builder.Build());

            }
        }
    }
}