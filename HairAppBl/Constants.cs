﻿using System;
using System.IO;

namespace HairAppBl
{
    public static class Constants
    {
        public static string SchedulesStorageFile 
        {
            get => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "schedules.json");
        }
    }
}
