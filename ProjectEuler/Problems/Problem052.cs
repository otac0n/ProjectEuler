namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// It can be seen that the number, 125874, and its double, 251748, contain exactly the same digits, but in a different order.
    /// 
    /// Find the smallest positive integer, x, such that 2x, 3x, 4x, 5x, and 6x, contain the same digits.
    /// </summary>
    [Result(Name = "smallest", Expected = "142857")]
    public class Problem052 : Problem
    {
        public override string Solve(string resource)
        {
            int digit = 1;
            int nextDigit = 10;

            for (int x = 1; ; x++)
            {
                if (x >= nextDigit)
                {
                    digit *= 10;
                    nextDigit *= 10;
                }

                var firstDigit = x / digit;

                if (firstDigit >= 2 && firstDigit <= 4)
                {
                    continue;
                }

                if (NumberTheory.IsAnagram(2 * x, 3 * x) &&
                    NumberTheory.IsAnagram(3 * x, 4 * x) &&
                    NumberTheory.IsAnagram(4 * x, 5 * x) &&
                    NumberTheory.IsAnagram(5 * x, 6 * x))
                {
                    return x.ToString();
                }
            }
        }
    }
}
