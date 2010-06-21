namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// Euler's Totient function, φ(n) [sometimes called the phi function], is used to determine the number of positive numbers less than or equal to n which are relatively prime to n. For example, as 1, 2, 4, 5, 7, and 8, are all less than nine and relatively prime to nine, φ(9)=6.
    /// The number 1 is considered to be relatively prime to every positive number, so φ(1)=1.
    /// 
    /// Interestingly, φ(87109)=79180, and it can be seen that 87109 is a permutation of 79180.
    /// 
    /// Find the value of n, 1 &lt; n &lt; 10^(7), for which φ(n) is a permutation of n and the ratio n/φ(n) produces a minimum.
    /// </summary>
    [Result(Name = "minimized at", Expected = "8319823")]
    public class Problem070 : Problem
    {
        public override string Solve(string resource)
        {
            Fraction minRatio = null;
            var minRatioN = 0;

            var primes = PrimeMath.GetPrimesBelow(10000000);

            for (int n = 2; n < 10000000; n++)
            {
                var totient = PrimeMath.Totient(n, primes);

                if (NumberTheory.PermutationKey(n) != NumberTheory.PermutationKey(totient))
                {
                    continue;
                }

                var ratio = new Fraction(n, totient);

                if (minRatio == null || ratio < minRatio)
                {
                    minRatio = ratio;
                    minRatioN = n;
                }
            }

            return minRatioN.ToString();
        }
    }
}
