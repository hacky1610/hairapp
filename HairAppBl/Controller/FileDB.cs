 using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HairAppBl.Interfaces;
using HairAppBl.Models;
using Newtonsoft.Json;

namespace HairAppBl.Controller
{
  public class FileDB:IDataBase
    {
        private readonly String dbFile;

        public FileDB(string file)
        {
            this.dbFile = file;
        }

        public void Save(Object o)
        {
            string json = JsonConvert.SerializeObject(o);
            File.WriteAllText(dbFile, json);
        }


        public T Load<T>()
        {
           
              List<string> wdId = new List<string>();
              var json = File.ReadAllText(dbFile);
              return (T) JsonConvert.DeserializeObject(json, typeof(T));
          
        }
    }
 }
