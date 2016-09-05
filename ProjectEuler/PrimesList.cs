using System.Collections.Generic;

namespace ProjectEuler
{
    public class PrimesList
    {
        public List<long> Primes
        {
            get;
            internal set;
        }

        public long LargestValueChecked
        {
            get;
            internal set;
        }

        public long NextPrimeSquared
        {
            get;
            internal set;
        }

        public int NextPrimeSquaredIndex
        {
            get;
            internal set;
        }
    }
}
