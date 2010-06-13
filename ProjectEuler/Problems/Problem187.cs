namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A composite is a number containing at least two prime factors. For example, 15 = 3 × 5; 9 = 3 × 3; 12 = 2 × 2 × 3.
    /// 
    /// There are ten composites below thirty containing precisely two, not necessarily distinct, prime factors: 4, 6, 9, 10, 14, 15, 21, 22, 25, 26.
    /// 
    /// How many composite integers, n &lt; 10^(8), have precisely two, not necessarily distinct, prime factors?
    /// </summary>
    [Result(Name = "result", Expected = "17427258")]
    public class Problem187 : Problem
    {
        public override string Solve(string resource)
        {
            long max = 100000000;

            var primes = PrimeMath.GetPrimesBelow(max / 2);

            var count = 0;

            for (int a = 0; a < primes.Primes.Count; a++)
            {
                var oneFound = false;

                for (int b = a; b < primes.Primes.Count; b++)
                {
                    var val = primes.Primes[a] * primes.Primes[b];

                    if (val > max)
                    {
                        break;
                    }

                    count++;
                    oneFound = true;
                }

                if (!oneFound)
                {
                    break;
                }
            }

            return count.ToString();
        }
    }
}
