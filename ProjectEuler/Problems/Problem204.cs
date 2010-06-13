namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A Hamming number is a positive number which has no prime factor larger than 5.
    /// So the first few Hamming numbers are 1, 2, 3, 4, 5, 6, 8, 9, 10, 12, 15.
    /// There are 1105 Hamming numbers not exceeding 10^(8).
    /// 
    /// We will call a positive number a generalised Hamming number of type n, if it has no prime factor larger than n.
    /// Hence the Hamming numbers are the generalised Hamming numbers of type 5.
    /// 
    /// How many generalised Hamming numbers of type 100 are there which don't exceed 10^(9)?
    /// </summary>
    [Result(Name = "result", Expected = "")]
    public class Problem204 : Problem
    {
        public override string Solve(string resource)
        {
            var type = 100;
            var max = 1000000000;

            var primes = PrimeMath.GetPrimesBelow(100);
            PrimeMath.GetFirstNPrimes(primes.Primes.Count + 1);

            Func<int, long, long> lookup = null;
            lookup = (primeIndex, product) =>
            {
                var prime = primes.Primes[primeIndex];

                if (prime > type)
                {
                    return 1;
                }
                
                long c = 0;
                for (int p = 0; product <= max; p++)
                {
                    c += lookup(primeIndex + 1, product);
                    product *= prime;
                }

                return c;
            };

            var count = lookup(0, 1);

            return count.ToString();
        }
    }
}
