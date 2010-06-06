namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Combinatorics;

    /// <summary>
    /// The number, 1406357289, is a 0 to 9 pandigital number because it is made up of each of the digits 0 to 9 in some order, but it also has a rather interesting sub-string divisibility property.
    /// 
    /// Let d_(1) be the 1^(st) digit, d_(2) be the 2^(nd) digit, and so on. In this way, we note the following:
    /// 
    ///     * d_(2)d_(3)d_(4)=406 is divisible by 2
    ///     * d_(3)d_(4)d_(5)=063 is divisible by 3
    ///     * d_(4)d_(5)d_(6)=635 is divisible by 5
    ///     * d_(5)d_(6)d_(7)=357 is divisible by 7
    ///     * d_(6)d_(7)d_(8)=572 is divisible by 11
    ///     * d_(7)d_(8)d_(9)=728 is divisible by 13
    ///     * d_(8)d_(9)d_(10)=289 is divisible by 17
    /// 
    /// Find the sum of all 0 to 9 pandigital numbers with this property.
    /// </summary>
    [Result(Name = "sum", Expected = "16695334890")]
    public class Problem043 : Problem
    {
        public override string Solve(string resource)
        {
            long sum = 0;
            foreach(IList<int> digits in new Permutations<int>(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, GenerateOption.WithoutRepetition))
            {
                if (digits[0] == 0)
                {
                    continue;
                }

                if (((digits[1] * 100 + digits[2] * 10 + digits[3] * 1) % 02 == 0) &&
                    ((digits[2] * 100 + digits[3] * 10 + digits[4] * 1) % 03 == 0) &&
                    ((digits[3] * 100 + digits[4] * 10 + digits[5] * 1) % 05 == 0) &&
                    ((digits[4] * 100 + digits[5] * 10 + digits[6] * 1) % 07 == 0) &&
                    ((digits[5] * 100 + digits[6] * 10 + digits[7] * 1) % 11 == 0) &&
                    ((digits[6] * 100 + digits[7] * 10 + digits[8] * 1) % 13 == 0) &&
                    ((digits[7] * 100 + digits[8] * 10 + digits[9] * 1) % 17 == 0))
                {
                    sum +=
                        digits[9] * 1L +
                        digits[8] * 10L +
                        digits[7] * 100L +
                        digits[6] * 1000L +
                        digits[5] * 10000L +
                        digits[4] * 100000L +
                        digits[3] * 1000000L +
                        digits[2] * 10000000L +
                        digits[1] * 100000000L +
                        digits[0] * 1000000000L;
                }
            }

            return sum.ToString();
        }
    }
}
