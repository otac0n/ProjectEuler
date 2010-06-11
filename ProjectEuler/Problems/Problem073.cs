namespace ProjectEuler
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
            long maxDenom = 12000;

            Func<long, long, long, long, long> f = null;
            f = (a, b, c, d) =>
            {
                if (b + d > maxDenom)
                {
                    return 0;
                }
                else
                {
                    return f(a, b, a + c, b + d) + f(a + c, b + d, c, d) + 1;
                }
            };

            long count = f(1, 3, 1, 2);

            return count.ToString();
        }
    }
}
