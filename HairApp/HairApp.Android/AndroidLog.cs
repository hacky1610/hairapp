using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HairApp.Droid
{
    public static class AndroidLog
    {
        public static void WriteLog(string value)
        {
            try
            {
                var mLogfilePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "logger.txt");
                using (var file = File.AppendText(mLogfilePath))
                {
                    file.WriteLine($"{DateTime.Now.ToLocalTime()}: {value}");
                }
            }
            catch(Exception e)
            {

            }
          
        }
    }
}