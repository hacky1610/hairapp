﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Interfaces
{
    public interface IDataBase
    {
        SQLiteAsyncConnection DB
        {
            get;
            set;
        }
    }
}
