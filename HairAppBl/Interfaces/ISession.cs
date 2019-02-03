using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Interfaces
{
    public interface ISession
    {
        void Save();
        void Restore();
    }
}
