using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HairAppBl
{
    public class FileLogger:Interfaces.ILogger
    {
        private string mLogfilePath;

        public FileLogger()
        {
            this.mLogfilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "logger.txt");
        }

        public void WriteLine(object value)
        {
            AddToFile(value);
        }

        private void AddToFile(object value)
        {
            using (var file = File.AppendText(this.mLogfilePath))
            {
                file.WriteLine($"{DateTime.Now.ToLocalTime()}: {value}");
            }
        }


        public void Call(string value)
        {
            AddToFile("Call function: " + value);
        }

        public void Error(string value)
        {
            AddToFile("Error:" + value);
        }

        public string ReadLog()
        {
            return File.ReadAllText(this.mLogfilePath);
        }

        public void Clear()
        {
            File.WriteAllText(this.mLogfilePath, String.Empty);
        }
    }
}
