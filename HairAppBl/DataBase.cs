using SQLite;
using System;
using System.IO;
using System.Threading.Tasks;

namespace HairAppBl
{
    public class DataBase
    {
        static DataBase mInstance;
        public readonly SQLiteAsyncConnection database;

        public DataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
        }

        public static DataBase Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new DataBase(
                      Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "HairDb.db3"));
                }
                return mInstance;
            }
        }


    }


}
