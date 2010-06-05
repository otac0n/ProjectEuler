namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// It is possible to show that the square root of two can be expressed as an infinite continued fraction.
    /// 
    /// √ 2 = 1 + 1/(2 + 1/(2 + 1/(2 + ... ))) = 1.414213...
    /// 
    /// By expanding this for the first four iterations, we get:
    /// 
    /// 1 + 1/2 = 3/2 = 1.5
    /// 1 + 1/(2 + 1/2) = 7/5 = 1.4
    /// 1 + 1/(2 + 1/(2 + 1/2)) = 17/12 = 1.41666...
    /// 1 + 1/(2 + 1/(2 + 1/(2 + 1/2))) = 41/29 = 1.41379...
    /// 
    /// The next three expansions are 99/70, 239/169, and 577/408, but the eighth expansion, 1393/985, is the first example where the number of digits in the numerator exceeds the number of digits in the denominator.
    /// 
    /// In the first one-thousand expansions, how many fractions contain a numerator with more digits than denominator?
    /// </summary>
    [Result(Name = "count", Expected = "153")]
    public class Problem057 : Problem
    {
        public override string Solve(string resource)
        {
            var count = 0;

            for (int i = 1; i <= 1000; i++)
            {
                BigInteger num = 1;
                BigInteger denom = 1;

                for (int j = 0; j < i; j++)
                {
                    num += denom;

                    var swap = num;
                    num = denom;
                    denom = swap;

                    num += denom;
                }

                if (num.ToString().Length > denom.ToString().Length)
                {
                    count++;
                }
            }

            return count.ToString();
        }
    }
}
