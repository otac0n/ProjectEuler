namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// The 5-digit number, 16807=7^(5), is also a fifth power. Similarly, the 9-digit number, 134217728=8^(9), is a ninth power.
    /// 
    /// How many n-digit positive integers exist which are also an nth power?
    /// </summary>
    [Result(Name = "count", Expected = "49")]
    public class Problem063 : Problem
    {
        public override string Solve(string resource)
        {
            var count = 0;
            var lastCount = 0;
            for (int n = 1; ; n++)
            {
                for (int a = 1; ; a++)
                {
                    BigInteger p = 1;

                    for (int j = 0; j < n; j++)
                    {
                        p *= a;
                    }

                    var len = p.ToString().Length;

                    if (len == n)
                    {
                        count++;
                    }
                    else if (len > n)
                    {
                        break;
                    }
                }

                if (count == lastCount)
                {
                    break;
                }

                lastCount = count;
            }

            return count.ToString();
        }
    }
}
