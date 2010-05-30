using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    public class PrimesList
    {
        public List<long> Primes
        {
            get;
            internal set;
        }

        internal long LargestValueChecked
        {
            get;
            set;
        }
    }
}
