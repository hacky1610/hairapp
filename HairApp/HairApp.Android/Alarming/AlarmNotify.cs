using Android.Content;
using HairApp.Droid.Alarming;

namespace HairApp.Droid
{
    public class AlarmNotify : Notify
    {
        public AlarmNotify():base()
        { }

        public void Notify(Context context)
        {
            CreateNotificationChannel(context);

            var washdays = mAlarmController.GetTodayWashDays();

            if (washdays.Count == 0)
            {
                AndroidLog.WriteLog("Today is no washing day");
                return;
            }

            foreach (var wd in washdays)
            {
                AndroidLog.WriteLog($"Today is washday: {wd.Name} ");
                mAlarmController.SetAlarmShown(wd.ID);
                SendNotify(context, wd.ID, HairAppBl.Resources.AppResource.TimeForHairCare ,    $"{HairAppBl.Resources.AppResource.TodayIs} {wd.Name}");
            }
        }

    
    }
}
