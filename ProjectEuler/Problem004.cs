namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A palindromic number reads the same both ways. The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 × 99.
    /// 
    /// Find the largest palindrome made from the product of two 3-digit numbers.
    /// </summary>
    [Result(Name = "largest")]
    public class Problem004 : Problem
    {
        public override string Solve(string resource)
        {
            var largest = 0;

            for (var m = 100; m < 1000; m++)
            {
                for (var n = m; n < 1000; n++)
                {
                    var product = m * n;

                    if (NumberTheory.IsPalindrome(product, 10))
                    {
                        largest = Math.Max(largest, product);
                    }
                }
            }

            return largest.ToString();
        }
    }
}
