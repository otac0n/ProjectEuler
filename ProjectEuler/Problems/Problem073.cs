﻿namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Diagnostics;

    /// <summary>
    /// Consider the fraction, n/d, where n and d are positive integers. If n&lt;d  and HCF(n,d)=1, it is called a reduced proper fraction.
    /// 
    /// If we list the set of reduced proper fractions for d ≤ 8 in ascending order of size, we get:
    /// 
    /// 1/8, 1/7, 1/6, 1/5, 1/4, 2/7, 1/3, 3/8, 2/5, 3/7, 1/2, 4/7, 3/5, 5/8, 2/3, 5/7, 3/4, 4/5, 5/6, 6/7, 7/8
    /// 
    /// It can be seen that there are 3 fractions between 1/3 and 1/2.
    /// 
    /// How many fractions lie between 1/3 and 1/2 in the sorted set of reduced proper fractions for d ≤ 12,000?
    /// </summary>
    [Result(Name = "count", Expected = "7295372")]
    public class Problem073 : Problem
    {
        public override string Solve(string resource)
        {
            var min = new Fraction(1, 3);
            var max = new Fraction(1, 2);

            var count = 0;
            for (long d = 2; d <= 12000; d++)
            {
                for (var n = min.Numerator * d / min.Denominator; n < d; n++)
                {
                    var test = new Fraction(n, d);

                    if (test.CompareTo(max) >= 0)
                    {
                        break;
                    }

                    if (test.CompareTo(min) <= 0)
                    {
                        continue;
                    }

                    if (NumberTheory.GCD(n, d) == 1)
                    {
                        count++;
                    }
                }
            }

            return count.ToString();
        }
    }
}
