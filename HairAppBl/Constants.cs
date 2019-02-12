using System;
using System.IO;

namespace HairAppBl
{
    public class Constants
    {
        public static string SchedulesStorageFile 
        {
            get => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "schedules.json");
        }
    }
}
