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
        readonly Interfaces.IDataBase mDb;

        public DbTable(Interfaces.IDataBase db) 
        {
            this.mDb = db;
            CreateTable();
        }

        private void CreateTable() 
        {
            mDb.DB.CreateTableAsync<T>().Wait();
        }

        public Task<List<T>> GetItemsAsync()
        {
            return mDb.DB.Table<T>().ToListAsync();
        }

        public Task<List<T>> GetItemsNotDoneAsync()
        {
            return mDb.DB.QueryAsync<T>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public AsyncTableQuery<T> GetFoo()
        {
            return mDb.DB.Table<T>();
        }

        public Task<List<T>> ExecQuery(string queryString)
        {
            return mDb.DB.QueryAsync<T>(queryString);
        }

   



        public Task<int> SaveItemAsync(T item)
        {
            return mDb.DB.InsertAsync(item);
        }

        public Task<int> SaveItemsAsync(List<T> items)
        {
            return mDb.DB.InsertAllAsync(items);
        }

        public Task<int> DeleteItemAsync(T item)
        {
            return mDb.DB.DeleteAsync(item);
        }
    }

    public class TodoItem
    {
        public int ID { get; set; }
        public string Text { get; set; }
    }
}
