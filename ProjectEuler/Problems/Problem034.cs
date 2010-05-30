namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 145 is a curious number, as 1! + 4! + 5! = 1 + 24 + 120 = 145.
    /// 
    /// Find the sum of all numbers which are equal to the sum of the factorial of their digits.
    /// 
    /// Note: as 1! = 1 and 2! = 2 are not sums they are not included.
    /// </summary>
    [Result(Name = "sum", Expected = "40730")]
    public class Problem034 : Problem
    {
        public override string Solve(string resource)
        {
            var digits = new Dictionary<char, long>
            {
                { '0', NumberTheory.Factorial(0) },
                { '1', NumberTheory.Factorial(1) },
                { '2', NumberTheory.Factorial(2) },
                { '3', NumberTheory.Factorial(3) },
                { '4', NumberTheory.Factorial(4) },
                { '5', NumberTheory.Factorial(5) },
                { '6', NumberTheory.Factorial(6) },
                { '7', NumberTheory.Factorial(7) },
                { '8', NumberTheory.Factorial(8) },
                { '9', NumberTheory.Factorial(9) }
            };

            var upperBound = digits['9'] * 7;
            long sum = 0;
            for (long i = 10; i <= upperBound; i++)
            {
                var num = i.ToString();

                long digitalSum = 0;

                for (var j = 0; j < num.Length; j++)
                {
                    digitalSum += digits[num[j]];
                }

                if (digitalSum == i)
                {
                    sum += i;
                }
            }

            return sum.ToString();
        }
    }
}
