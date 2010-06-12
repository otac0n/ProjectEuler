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
    /// It can be seen that there are 21 elements in this set.
    /// 
    /// How many elements would be contained in the set of reduced proper fractions for d ≤ 1,000,000?
    /// </summary>
    [Result(Name = "count", Expected = "303963552391")]
    public class Problem072 : Problem
    {
        public override string Solve(string resource)
        {
            Func<int, long, long> f = null;
            f = (x, n) =>
            {
                var a = new long[n + 1];
                for (long q = 0; q < n + 1; q++)
                {
                    a[q] = q * x;
                }

                for (long q = 1; q < n + 1; q++)
                {
                    long m = 2;
                    while (m * q <= n)
                    {
                        a[m * q] -= a[q];
                        m += 1;
                    }
                }
                return a.Sum() - 1;
            };

            long count = f(1, 1000000);

            return count.ToString();
        }
    }
}
