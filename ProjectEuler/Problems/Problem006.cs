namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The sum of the squares of the first ten natural numbers is,
    /// 1^2 + 2^2 + ... + 10^2 = 385
    /// 
    /// The square of the sum of the first ten natural numbers is,
    /// (1 + 2 + ... + 10)^2 = 55^2 = 3025
    ///
    /// Hence the difference between the sum of the squares of the first ten natural numbers and the square of the sum is 3025 − 385 = 2640.
    ///
    /// Find the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.
    /// </summary>
    [Result(Name = "difference", Expected = "25164150")]
    public class Problem006 : Problem
    {
        public override string Solve(string resource)
        {
            var sumSquares = 0;
            var sum = 0;
            for (int n = 1; n <= 100; n++)
            {
                sumSquares += n * n;
                sum += n;
            }

            var diff = (sum * sum) - sumSquares;

            return diff.ToString();
        }
    }
}
