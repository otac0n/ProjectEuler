namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Take the number 192 and multiply it by each of 1, 2, and 3:
    /// 
    ///     192 × 1 = 192
    ///     192 × 2 = 384
    ///     192 × 3 = 576
    /// 
    /// By concatenating each product we get the 1 to 9 pandigital, 192384576. We will call 192384576 the concatenated product of 192 and (1,2,3)
    /// 
    /// The same can be achieved by starting with 9 and multiplying by 1, 2, 3, 4, and 5, giving the pandigital, 918273645, which is the concatenated product of 9 and (1,2,3,4,5).
    /// 
    /// What is the largest 1 to 9 pandigital 9-digit number that can be formed as the concatenated product of an integer with (1,2, ... , n) where n > 1?
    /// </summary>
    [Result(Name = "largest", Expected = "932718654")]
    public class Problem038: Problem
    {
        public override string Solve(string resource)
        {
            var largest = 0;
            for (int i = 1; i <= 9876; i++)
            {
                var digits = new short[10];
                var concat = string.Empty;
                for (int n = 1; n <= 9; n++)
                {
                    var v = (i * n).ToString();
                    foreach (var c in v)
                    {
                        digits[(int)(c - '0')]++;
                    }

                    if (digits[0] != 0)
                    {
                        break;
                    }

                    if (digits[1] > 1 ||
                        digits[2] > 1 ||
                        digits[3] > 1 ||
                        digits[4] > 1 ||
                        digits[5] > 1 ||
                        digits[6] > 1 ||
                        digits[7] > 1 ||
                        digits[8] > 1 ||
                        digits[9] > 1)
                    {
                        break;
                    }

                    concat += v;

                    if (digits[1] == 1 &&
                        digits[2] == 1 &&
                        digits[3] == 1 &&
                        digits[4] == 1 &&
                        digits[5] == 1 &&
                        digits[6] == 1 &&
                        digits[7] == 1 &&
                        digits[8] == 1 &&
                        digits[9] == 1)
                    {
                        if (n > 1)
                        {
                            largest = Math.Max(largest, int.Parse(concat));
                        }

                        break;
                    }
                }
            }

            return largest.ToString();
        }
    }
}
