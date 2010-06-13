namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Drawing;

    /// <summary>
    /// How many 20 digit numbers n (without any leading zero) exist such that no three consecutive digits of n have a sum greater than 9?
    /// </summary>
    [Result(Name = "count", Expected = "378158756814587")]
    public class Problem164 : Problem
    {
        public override string Solve(string resource)
        {
            var digits = 20;

            var tables = new Dictionary<Point, long>[digits];
            for (int i = 0; i < digits; i++)
            {
                tables[i] = new Dictionary<Point, long>();
            }

            Func<int, int, int, long> lookup = null;
            lookup = (prevDigit2, prevDigit1, remainingDigits) =>
            {
                if (remainingDigits == 1)
                {
                    return 10 - (prevDigit1 + prevDigit2);
                }
                else
                {
                    var key = new Point(prevDigit2, prevDigit1);
                    if (!tables[remainingDigits].ContainsKey(key))
                    {

                        long count = 0;

                        for (int d = 0; d <= 9; d++)
                        {
                            if (prevDigit2 + prevDigit1 + d > 9)
                            {
                                break;
                            }

                            count += lookup(prevDigit1, d, remainingDigits - 1);
                        }

                        tables[remainingDigits][key] = count;
                    }

                    return tables[remainingDigits][key];
                }
            };

            long sum = 0;

            for (var d = 1; d <= 9; d++)
            {
                sum += lookup(0, d, digits - 1);
            }

            return sum.ToString();
        }
    }
}
