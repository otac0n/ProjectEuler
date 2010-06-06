namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Euler's Totient function, φ(n) [sometimes called the phi function], is used to determine the number of numbers less than n  which are relatively prime to n. For example, as 1, 2, 4, 5, 7, and 8, are all less than nine and relatively prime to nine, φ(9)=6.
    /// 
    /// It can be seen that n=6 produces a maximum n/φ(n) for n ≤ 10.
    /// 
    /// Find the value of n ≤ 1,000,000 for which n/φ(n) is a maximum.
    /// </summary>
    [Result(Name = "result", Expected = "510510")]
    public class Problem069 : Problem
    {
        public override string Solve(string resource)
        {
            var primes = PrimeMath.GetPrimesBelow(1000);

            var maxRatio = 0.0;
            var maxRatioNum = 0;

            for (int n = 1000000; n > 1; n--)
            {
                long totient = PrimeMath.Totient(n, primes);

                var ratio = (double)n / totient;

                if (ratio > maxRatio)
                {
                    maxRatio = ratio;
                    maxRatioNum = n;
                }
            }

            return maxRatioNum.ToString();
        }
    }
}
