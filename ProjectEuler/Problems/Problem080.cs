namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// It is well known that if the square root of a natural number is not an integer, then it is irrational. The decimal expansion of such square roots is infinite without any repeating pattern at all.
    /// 
    /// The square root of two is 1.41421356237309504880..., and the digital sum of the first one hundred decimal digits is 475.
    /// 
    /// For the first one hundred natural numbers, find the total of the digital sums of the first one hundred decimal digits for all the irrational square roots.
    /// </summary>
    [Result(Name = "sum", Expected = "40886")]
    public class Problem080 : Problem
    {
        public override string Solve(string resource)
        {
            var sum = 0;

            for (long i = 2; i <= 99; i++)
            {
                if (!NumberTheory.IsSquare(i))
                {
                    var q = i;
                    BigInteger r = 0;
                    BigInteger p = 0;
                    var count = 0;

                    while (count < 100)
                    {
                        BigInteger c = r * 100 + q;
                        q = 0;
                        
                        BigInteger y = 0;
                        int x;
                        for (x = 0; ; x++)
                        {
                            var y2 = (20 * p + x) * x;
                            if (y2 > c)
                            {
                                x--;
                                break;
                            }

                            y = y2;
                        }

                        sum += x;
                        count++;

                        p = 10 * p + x;
                        r = c - y;
                    }
                }
            }

            return sum.ToString();
        }
    }
}
