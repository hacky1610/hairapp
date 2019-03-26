
using System;
using Android.App;
using Android.App.Job;
using Android.Content;
using Android.OS;
using Android.Util;

namespace HairApp.Droid
{
    [Service(Name = "HairApp.Droid.AlarmJobService", Permission = "android.permission.BIND_JOB_SERVICE")]
    public class AlarmJobService : JobService
    {
        public override bool OnStartJob(JobParameters jobParams)
        {
            // Called by the operating system when starting the service.
            // Start up a thread, do work on the thread.
            return true;
        }

        public override bool OnStopJob(JobParameters jobParams)
        {
            // Called by Android when it has to terminate a running service.
            return false; // Don't reschedule the job.
        }
    }
}