namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The arithmetic sequence, 1487, 4817, 8147, in which each of the terms increases by 3330, is unusual in two ways: (i) each of the three terms are prime, and, (ii) each of the 4-digit numbers are permutations of one another.
    /// 
    /// There are no arithmetic sequences made up of three 1-, 2-, or 3-digit primes, exhibiting this property, but there is one other 4-digit increasing sequence.
    /// 
    /// What 12-digit number do you form by concatenating the three terms in this sequence?
    /// </summary>
    [Result(Name = "composite", Expected = "296962999629")]
    public class Problem049 : Problem
    {
        public override string Solve(string resource)
        {
            var primes = PrimeMath.GetPrimesBelow(10000);

            for (var aIndex = 169; aIndex < primes.Primes.Count - 2; aIndex++)
            {
                var a = primes.Primes[aIndex];

                if (a == 1487)
                {
                    continue;
                }

                for (var bIndex = aIndex + 1; aIndex < primes.Primes.Count - 1; bIndex++)
                {
                    var b = primes.Primes[bIndex];

                    var c = b - a + b;

                    if (c > 9999)
                    {
                        break;
                    }

                    if (!NumberTheory.IsAnagram(a, b))
                    {
                        continue;
                    }

                    if (!PrimeMath.IsPrime(c, primes))
                    {
                        continue;
                    }

                    if (NumberTheory.IsAnagram(b, c))
                    {
                        return a.ToString() + b.ToString() + c.ToString();
                    }
                }
            }

            return "error";
        }
    }
}
