namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// A googol (10^(100)) is a massive number: one followed by one-hundred zeros; 100^(100) is almost unimaginably large: one followed by two-hundred zeros. Despite their size, the sum of the digits in each number is only 1.
    /// 
    /// Considering natural numbers of the form, a^(b), where a, b &lt; 100, what is the maximum digital sum?
    /// </summary>
    [Result(Name = "maximum", Expected = "972")]
    public class Problem056 : Problem
    {
        public override string Solve(string resource)
        {
            var max = 0;
            for (int a = 2; a < 100; a++)
            {
                for (int b = 1; b < 100; b++)
                {
                    BigInteger r = a;

                    for (int i = 0; i < b - 1; i++)
                    {
                        r *= a;
                    }

                    var sum = 0;
                    foreach (var c in r.ToString())
                    {
                        sum += (int)(c - '0');
                    }

                    max = Math.Max(max, sum);
                }
            }

            return max.ToString();
        }
    }
}
