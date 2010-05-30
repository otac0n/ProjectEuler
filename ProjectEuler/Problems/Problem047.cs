namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The first two consecutive numbers to have two distinct prime factors are:
    /// 
    /// 14 = 2 × 7
    /// 15 = 3 × 5
    /// 
    /// The first three consecutive numbers to have three distinct prime factors are:
    /// 
    /// 644 = 2² × 7 × 23
    /// 645 = 3 × 5 × 43
    /// 646 = 2 × 17 × 19.
    /// 
    /// Find the first four consecutive integers to have four distinct primes factors. What is the first of these numbers?
    /// </summary>
    [Result(Name = "first", Expected = "134043")]
    public class Problem047 : Problem
    {
        public override string Solve(string resource)
        {
            var primes = PrimeMath.GetPrimesBelow(1000);

            var num = 4;

            for (long i = 2; ; i++)
            {
                int j;
                var found = true;
                for (j = 0; j < num; j++)
                {
                    var factors = PrimeMath.Factor(i + j, primes);
                    if (factors.Count != num)
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    return i.ToString();
                }
                else
                {
                    i += j;
                }
            }
        }
    }
}
