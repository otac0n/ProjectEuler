namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// There are exactly ten ways of selecting three from five, 12345:
    /// 
    /// 123, 124, 125, 134, 135, 145, 234, 235, 245, and 345
    /// 
    /// In combinatorics, we use the notation, 5_C_3 = 10.
    /// 
    /// In general,
    /// n_C_r = (n!)/(r!(n−r)!), where r ≤ n, n! = n×(n−1)×...×3×2×1, and 0! = 1.
    /// 
    /// It is not until n = 23, that a value exceeds one-million: 23_C_10 = 1144066.
    /// 
    /// How many, not necessarily distinct, values of  n_C_r, for 1 ≤ n ≤ 100, are greater than one-million?
    /// </summary>
    [Result(Name = "count", Expected = "4075")]
    public class Problem053 : Problem
    {
        public override string Solve(string resource)
        {
            var factorials = new Dictionary<int, BigInteger>();

            Func<int, BigInteger> lookup = null;
            lookup = num =>
            {
                if (num == 0)
                {
                    return 1;
                }
                else if (factorials.ContainsKey(num))
                {
                    return factorials[num];
                }

                factorials[num] = num * lookup(num - 1);

                return factorials[num];
            };

            Func<int, int, BigInteger> nCr = (n, r) =>
            {
                return lookup(n) / (lookup(r) * lookup(n - r));
            };

            var count = 0;
            for (int n = 1; n <= 100; n++)
            {
                for (int r = 1; r <= n; r++)
                {
                    if (nCr(n, r) > 1000000)
                    {
                        count++;
                    }
                }
            }

            return count.ToString();
        }
    }
}
