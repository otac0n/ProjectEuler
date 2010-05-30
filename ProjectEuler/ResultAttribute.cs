using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class ResultAttribute : Attribute
    {
        public ResultAttribute()
        {
        }

        public string Name
        {
            get;
            set;
        }
    }
}
