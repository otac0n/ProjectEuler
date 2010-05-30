namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A perfect number is a number for which the sum of its proper divisors is exactly equal to the number. For example, the sum of the proper divisors of 28 would be 1 + 2 + 4 + 7 + 14 = 28, which means that 28 is a perfect number.
    /// 
    /// A number n is called deficient if the sum of its proper divisors is less than n and it is called abundant if this sum exceeds n.
    /// 
    /// As 12 is the smallest abundant number, 1 + 2 + 3 + 4 + 6 = 16, the smallest number that can be written as the sum of two abundant numbers is 24. By mathematical analysis, it can be shown that all integers greater than 28123 can be written as the sum of two abundant numbers. However, this upper limit cannot be reduced any further by analysis even though it is known that the greatest number that cannot be expressed as the sum of two abundant numbers is less than this limit.
    /// 
    /// Find the sum of all the positive integers which cannot be written as the sum of two abundant numbers.
    /// </summary>
    [Result(Name = "sum", Expected = "4179871")]
    public class Problem023 : Problem
    {
        public override string Solve(string resource)
        {
            long upperLimit = 28123;

            Func<int, int> sumOfDivisors = num =>
            {
                var sum = 0;
                for (int i = 1; i < num; i++)
                {
                    if (num % i == 0)
                    {
                        sum += i;
                    }
                }

                return sum;
            };

            var abundantNumbers = new List<int>();
            for (int i = 1; i <= upperLimit; i++)
            {
                if (sumOfDivisors(i) > i)
                {
                    abundantNumbers.Add((int)i);
                }
            }

            var allNumbers = new int[upperLimit];
            for (int i = 1; i <= upperLimit; i++)
            {
                allNumbers[i - 1] = i;
            }

            for (int i = 0; i < abundantNumbers.Count; i++)
            {
                int numI = abundantNumbers[i];

                if (numI > upperLimit)
                {
                    break;
                }

                for (int j = 0; j < abundantNumbers.Count; j++)
                {
                    int numJ = numI + abundantNumbers[j];

                    if (numJ > upperLimit)
                    {
                        break;
                    }

                    allNumbers[numJ - 1] = 0;
                }
            }

            return allNumbers.Sum().ToString();
        }
    }
}
