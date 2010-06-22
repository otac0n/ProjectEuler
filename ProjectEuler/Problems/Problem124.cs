namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// The radical of n, rad(n), is the product of distinct prime factors of n. For example, 504 = 2^(3) × 3^(2) × 7, so rad(504) = 2 × 3 × 7 = 42.
    /// 
    /// If we calculate rad(n) for 1 ≤ n ≤ 10, then sort them on rad(n), and sorting on n if the radical values are equal, we get:
    /// 
    /// [table ommitted]
    /// 
    /// Let E(k) be the kth element in the sorted n column; for example, E(4) = 8 and E(6) = 9.
    /// 
    /// If rad(n) is sorted for 1 ≤ n ≤ 100000, find E(10000).
    /// </summary>
    [Result(Name = "E(10000)", Expected = "21417")]
    public class Problem124 : Problem
    {
        public override string Solve(string resource)
        {
            var primes = PrimeMath.GetDefaultPrimes();

            Func<long, long> rad = k =>
            {
                var factors = PrimeMath.Factor(k, primes);

                long r = 1;
                foreach (var key in factors.Keys)
                {
                    r *= key;
                }

                return r;
            };

            return (from n in Enumerable.Range(1, 100000)
                    orderby n ascending
                    orderby rad(n) ascending
                    select n).Skip(10000 - 1).First().ToString();
        }
    }
}
