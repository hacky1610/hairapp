using System;
using System.IO;

namespace HairAppBl
{
    public static class Constants
    {
        public static string MainSessionStorageFile
        {
            get => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mainsession.json");
        }

        public static string SchedulesStorageFile 
        {
            get => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "schedules.json");
        }

        public static string HistoryStorageFile
        {
            get => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "alarmhistory.json");
        }

        public static string SettingsStorageFile
        {
            get => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "settings.json");
        }
    }
}
