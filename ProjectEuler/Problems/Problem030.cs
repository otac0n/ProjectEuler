namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Surprisingly there are only three numbers that can be written as the sum of fourth powers of their digits:
    /// 
    ///     1634 = 1^4 + 6^4 + 3^4 + 4^4
    ///     8208 = 8^4 + 2^4 + 0^4 + 8^4
    ///     9474 = 9^4 + 4^4 + 7^4 + 4^4
    /// 
    /// As 1 = 1^4 is not a sum it is not included.
    /// 
    /// The sum of these numbers is 1634 + 8208 + 9474 = 19316.
    /// 
    /// Find the sum of all the numbers that can be written as the sum of fifth powers of their digits.
    /// </summary>
    [Result(Name = "sum", Expected = "443839")]
    public class Problem030 : Problem
    {
        public override string Solve(string resource)
        {
            long total = 0;

            for (int i = 2; i <= 354294; i++)
            {
                var num = i;
                var dig000001 = num % 10; num /= 10;
                var dig000010 = num % 10; num /= 10;
                var dig000100 = num % 10; num /= 10;
                var dig001000 = num % 10; num /= 10;
                var dig010000 = num % 10; num /= 10;
                var dig100000 = num % 10; num /= 10;

                var digitSum =
                    dig000001 * dig000001 * dig000001 * dig000001 * dig000001 +
                    dig000010 * dig000010 * dig000010 * dig000010 * dig000010 +
                    dig000100 * dig000100 * dig000100 * dig000100 * dig000100 +
                    dig001000 * dig001000 * dig001000 * dig001000 * dig001000 +
                    dig010000 * dig010000 * dig010000 * dig010000 * dig010000 +
                    dig100000 * dig100000 * dig100000 * dig100000 * dig100000;

                if (digitSum == i)
                {
                    total += i;
                }
            }

            return total.ToString();
        }
    }
}
