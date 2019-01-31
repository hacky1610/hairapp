using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HairAppBl
{
    public class DbTable<T> where T : new()
    {
        DataBase mDb;

        public DbTable(DataBase db) 
        {
            this.mDb = db;
            CreateTable();
        }

        private void CreateTable() 
        {
            mDb.database.CreateTableAsync<T>().Wait();
        }

        public Task<List<T>> GetItemsAsync()
        {
            return mDb.database.Table<T>().ToListAsync();
        }

        public Task<List<T>> GetItemsNotDoneAsync()
        {
            return mDb.database.QueryAsync<T>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        //public Task<T> GetItemAsync(int id)
        //{
        //    return mDb.database.Table<T>().Where(i => i.ID == id).FirstOrDefaultAsync();
        //}

  

        public Task<int> SaveItemAsync(T item)
        {
           /* if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {*/
                return mDb.database.InsertAsync(item);
            //}
        }

        public Task<int> DeleteItemAsync(T item)
        {
            return mDb.database.DeleteAsync(item);
        }
    }

    public class TodoItem
    {
        public int ID { get; set; }
        public string Text { get; set; }
    }
}
