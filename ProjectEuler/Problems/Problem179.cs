namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// Find the number of integers 1 &lt; n &lt; 10^(7), for which n and n  + 1 have the same number of positive divisors. For example, 14 has the positive divisors 1, 2, 7, 14 while 15 has 1, 3, 5, 15.
    /// </summary>
    [Result(Name = "result", Expected = "")]
    public class Problem179 : Problem
    {
        public override string Solve(string resource)
        {
            var primes = PrimeMath.GetDefaultPrimes();

            var count = 0;

            var lastDiv = 0;
            for (int i = 2; i <= 10000000; i++)
            {
                var div = PrimeMath.GetAllFactors(PrimeMath.Factor(i, primes)).Count;

                if (lastDiv == div)
                {
                    count++;
                }

                lastDiv = div;
            }

            return count.ToString();
        }
    }
}
