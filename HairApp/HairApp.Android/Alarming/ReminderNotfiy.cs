using Android.Content;

namespace HairApp.Droid
{
    public class ReminderNotfiy : Alarming.Notify
    {
        public ReminderNotfiy() : base()
        {

        }

        public void Notify(Context context)
        {
            CreateNotificationChannel(context);

            var washdays = mAlarmController.GetReminderWashDays();

            if (washdays.Count == 0)
            {
                AndroidLog.WriteLog("Reminder: Tomorrow is no washing day");
                return;
            }

            foreach (var wd in washdays)
            {
                AndroidLog.WriteLog($"Remineder: Tomorrow is washday: {wd.Name} ");
                mAlarmController.SetReminderShown(wd.ID);
                SendNotify(context, wd.ID, HairAppBl.Resources.AppResource.ReminderForHairCare, $"{HairAppBl.Resources.AppResource.TomorrowIs} {wd.Name}");
            }
        }

       
    }
}
