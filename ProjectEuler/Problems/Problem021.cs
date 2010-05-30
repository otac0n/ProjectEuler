namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Let d(n) be defined as the sum of proper divisors of n (numbers less than n which divide evenly into n).
    /// If d(a) = b and d(b) = a, where a ≠ b, then a and b are an amicable pair and each of a and b are called amicable numbers.
    /// 
    /// For example, the proper divisors of 220 are 1, 2, 4, 5, 10, 11, 20, 22, 44, 55 and 110; therefore d(220) = 284. The proper divisors of 284 are 1, 2, 4, 71 and 142; so d(284) = 220.
    /// 
    /// Evaluate the sum of all the amicable numbers under 10000.
    /// </summary>
    [Result(Name = "sum", Expected = "31626")]
    public class Problem021 : Problem
    {
        public override string Solve(string resource)
        {
            var primes = PrimeMath.GetFirstNPrimes(1000);
            var sums = new Dictionary<long, long>();

            for (int i = 2; i < 10000; i++)
            {
                var allFactors = PrimeMath.GetAllFactors(PrimeMath.Factor(i, primes));
                allFactors.Remove(i);
                sums[i] = allFactors.Sum();
            }

            var amicable = from a in sums
                           where sums.ContainsKey(a.Value)
                           where sums[a.Value] == a.Key
                           where a.Key != a.Value
                           select a.Key;

            var sum = amicable.Sum();

            return sum.ToString();
        }
    }
}
