using Xamarin.Forms;
using HairApp.Interfaces;

[assembly: Dependency(typeof(HairApp.Droid.Alarm))]
namespace HairApp.Droid
{
    public class Alarm : IAlarm
    {
        public void Init()
        {
            Alarming.AlarmWorker.Create();
        }

    }
}

