using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class ProblemResourceAttribute : Attribute
    {
        readonly string resourceName;

        public ProblemResourceAttribute(string resourceName)
        {
            this.resourceName = resourceName;
        }

        public string ResourceName
        {
            get
            {
                return resourceName;
            }
        }
    }
}
