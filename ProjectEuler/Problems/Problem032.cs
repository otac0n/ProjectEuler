namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Combinatorics;

    /// <summary>
    /// We shall say that an n-digit number is pandigital if it makes use of all the digits 1 to n exactly once; for example, the 5-digit number, 15234, is 1 through 5 pandigital.
    /// 
    /// The product 7254 is unusual, as the identity, 39 × 186 = 7254, containing multiplicand, multiplier, and product is 1 through 9 pandigital.
    /// 
    /// Find the sum of all products whose multiplicand/multiplier/product identity can be written as a 1 through 9 pandigital.
    /// 
    /// HINT: Some products can be obtained in more than one way so be sure to only include it once in your sum.
    /// </summary>
    [Result(Name = "sum", Expected = "45228")]
    public class Problem032 : Problem
    {
        public override string Solve(string resource)
        {
            var sum = 0;
            for (int product = 1234; product < 9999; product++)
            {
                var digitCounts = new int[10];

                var pandigital = true;
                var num = product;
                while (num > 0)
                {
                    var digit = num % 10;
                    digitCounts[digit]++;
                    num /= 10;

                    if (digitCounts[digit] > 1)
                    {
                        pandigital = false;
                        break;
                    }
                }

                if (!pandigital || digitCounts[0] > 0)
                {
                    continue;
                }

                for (int a = 1; a <= 98; a++)
                {
                    if (a % 11 == 0 || digitCounts[a / 10] > 0 || digitCounts[a % 10] > 0 || product % a != 0)
                    {
                        continue;
                    }

                    var b = product / a;

                    var digitCountsA = new int[10];
                    if (a > 10)
                    {
                        digitCountsA[a / 10]++;
                    }
                    else if (b < 1000)
                    {
                        continue;
                    }
                    digitCountsA[a % 10]++;

                    if (digitCountsA[0] > 0 || b <= 100)
                    {
                        continue;
                    }

                    pandigital = true;
                    num = b;
                    while (num > 0)
                    {
                        var digit = num % 10;
                        digitCountsA[digit]++;
                        num /= 10;

                        if (digitCounts[digit] + digitCountsA[digit] > 1)
                        {
                            pandigital = false;
                            break;
                        }
                    }

                    if (digitCountsA[0] > 0)
                    {
                        pandigital = false;
                    }

                    if (!pandigital)
                    {
                        continue;
                    }

                    sum += product;
                    break;
                }
            }

            return sum.ToString();
        }
    }
}
