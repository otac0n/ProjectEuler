namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// The binomial coefficient 10_C_3 = 120.
    /// 120 = 2^(3) × 3 × 5 = 2 × 2 × 2 × 3 × 5, and 2 + 2 + 2 + 3 + 5 = 14.
    /// So the sum of the terms in the prime factorisation of 10_C_3 is 14.
    /// 
    /// Find the sum of the terms in the prime factorisation of 20000000_C_15000000. 
    /// </summary>
    [Result(Name = "sum", Expected = "7526965179680")]
    public class Problem231 : Problem
    {
        public override string Solve(string resource)
        {
            long n = 20000000;
            long r = 15000000;
            var primes = PrimeMath.GetPrimesBelow(n);

            long sum = 0;

            for (int primeIndex = 0; primeIndex < primes.Primes.Count; primeIndex++)
            {
                var prime = primes.Primes[primeIndex];
                if (prime > n)
                {
                    break;
                }

                var value = n;
                long count = 0;

                while (value > 1)
                {
                    var c = value / prime;
                    count += c;
                    value = c;
                }

                value = r;

                while (value > 1)
                {
                    var c = value / prime;
                    count -= c;
                    value = c;
                }

                value = n - r;

                while (value > 1)
                {
                    var c = value / prime;
                    count -= c;
                    value = c;
                }

                sum += prime * count;
            }

            return sum.ToString();
        }
    }
}
