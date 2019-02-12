using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Interfaces
{
    public interface IDataBase
    {
        void Save(Object o);
        T Load<T>();
    }
}
