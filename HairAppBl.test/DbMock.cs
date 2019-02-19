 using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HairAppBl.Interfaces;
using HairAppBl.Models;
using Newtonsoft.Json;

namespace HairAppBl.Controller
{
  public class DbMock:IDataBase
    {
        private  String database;

        public DbMock()
        {
        }

        public void Save(Object o)
        {
            database = JsonConvert.SerializeObject(o);
        }


        public T Load<T>()
        {
              return (T) JsonConvert.DeserializeObject(database, typeof(T));          
        }
    }
 }
