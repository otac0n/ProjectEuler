namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The decimal number, 585 = 1001001001_b2 (binary), is palindromic in both bases.
    /// 
    /// Find the sum of all numbers, less than one million, which are palindromic in base 10 and base 2.
    /// 
    /// (Please note that the palindromic number, in either base, may not include leading zeros.)
    /// </summary>
    [Result(Name = "sum", Expected = "872187")]
    public class Problem036 : Problem
    {
        public override string Solve(string resource)
        {
            long sum = 0;
            for (int num = 1; num < 1000000; num += 2)
            {
                if (NumberTheory.IsPalindrome(num, 10) && NumberTheory.IsPalindrome(num, 2))
                {
                    sum += num;
                }
            }

            return sum.ToString();
        }
    }
}
