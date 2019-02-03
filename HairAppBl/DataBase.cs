using HairAppBl.Interfaces;
using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HairAppBl
{
    public class DataBase:Interfaces.IDataBase
    {
        static DataBase mInstance;

        public DataBase(string dbPath)
        {
            DB = new SQLiteAsyncConnection(dbPath);
        }

        public static DataBase Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new DataBase(
                      Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HairDbV0.2.db3"));
                }
                return mInstance;
            }
        }

        public SQLiteAsyncConnection DB { get; set; }
    }


}
