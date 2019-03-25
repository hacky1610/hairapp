using System;
using System.Collections.Generic;
using System.Text;

namespace HairAppBl.Models
{
    public class TypeNameObject<T>
    {
        public T Type { get; private set; }
        public string Name { get; private set; }

        public TypeNameObject(T type, String name)
        {
            Type = type;
            Name = name;
        }
    }
}
