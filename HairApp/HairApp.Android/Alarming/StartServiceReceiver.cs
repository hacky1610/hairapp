
using Android.App;
using Android.Content;

namespace HairApp.Droid
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    class StartServiceReceiver: BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            AndroidLog.WriteLog("Boot completed - start Jobservice");

            JobUtil.scheduleJob(context);
        }
    }
}