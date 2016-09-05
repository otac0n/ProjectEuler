namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Some positive integers n have the property that the sum [ n  + reverse(n) ] consists entirely of odd (decimal) digits. For instance, 36 + 63 = 99 and 409 + 904 = 1313. We will call such numbers reversible; so 36, 63, 409, and 904 are reversible. Leading zeroes are not allowed in either n or reverse(n).
    /// 
    /// There are 120 reversible numbers below one-thousand.
    /// 
    /// How many reversible numbers are there below one-billion (10^(9))?
    /// </summary>
    [Result(Name = "count", Expected = "608720")]
    public class Problem145 : Problem
    {
        public override string Solve(string resource)
        {
            var count = 0;

            for (long i = 11; i < 100000000; i += 2)
            {
                if (i % 10 == 0)
                {
                    continue;
                }

                var rev = NumberTheory.Reverse(i, 10);
                var sum = i + rev;

                var odd = true;
                while (sum > 0)
                {
                    if (sum % 2 == 0)
                    {
                        odd = false;
                        break;
                    }

                    sum /= 10;
                }

                if (odd)
                {
                    count += 2;
                }
            }

            return count.ToString();
        }
    }
}
