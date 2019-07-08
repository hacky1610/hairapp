using System;
using Android.App;
using Android.App.Job;
namespace HairApp.Droid
{
    [Service(Exported = true, Name = "com.companyname.HairApp.Droid.ServiceRestarter", Permission = "android.permission.BIND_JOB_SERVICE")]
    public class ServiceRestarter:JobService
    {
        public override bool OnStartJob(JobParameters @params)
        {
            AndroidLog.WriteLog("ServiceRestarter called");
            new Alarm().Init();
           
            return true;
        }

        public override bool OnStopJob(JobParameters @params)
        {
            return true;
        }


}
}