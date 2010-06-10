namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// The Fibonacci sequence is defined by the recurrence relation:
    /// 
    ///     F_(n) = F_(n−1) + F_(n−2), where F_(1) = 1 and F_(2) = 1.
    /// 
    /// It turns out that F_(541), which contains 113 digits, is the first Fibonacci number for which the last nine digits are 1-9 pandigital (contain all the digits 1 to 9, but not necessarily in order). And F_(2749), which contains 575 digits, is the first Fibonacci number for which the first nine digits are 1-9 pandigital.
    /// 
    /// Given that F_(k) is the first Fibonacci number for which the first nine digits AND the last nine digits are 1-9 pandigital, find k.
    /// </summary>
    [Result(Name = "result", Expected = "329468")]
    public class Problem104 : Problem
    {
        public override string Solve(string resource)
        {
            BigInteger thousandDigits = 1;
            for (int i = 1; i < 1000; i++)
            {
                thousandDigits *= 10;
            }

            BigInteger nm1 = 1;
            BigInteger nm2 = 0;

            long term = 2;

            while (true)
            {
                BigInteger n = nm1 + nm2;

                var str = n.ToString();

                if (str.Length >= 575)
                {
                    var counts = new int[20];
                    for (int i = 0; i < 9; i++)
                    {
                        counts[(int)(str[i] - '0')]++;
                        counts[(int)(str[str.Length - i - 1] - '0' + 10)]++;
                    }

                    if (counts[00] == 0 &&
                        counts[10] == 0 &&
                        counts[01] == 1 &&
                        counts[11] == 1 &&
                        counts[02] == 1 &&
                        counts[12] == 1 &&
                        counts[03] == 1 &&
                        counts[13] == 1 &&
                        counts[04] == 1 &&
                        counts[14] == 1 &&
                        counts[05] == 1 &&
                        counts[15] == 1 &&
                        counts[06] == 1 &&
                        counts[16] == 1 &&
                        counts[07] == 1 &&
                        counts[17] == 1 &&
                        counts[08] == 1 &&
                        counts[18] == 1 &&
                        counts[09] == 1 &&
                        counts[19] == 1)
                    {
                        return term.ToString();
                    }
                }

                term++;
                nm2 = nm1;
                nm1 = n;
            }
        }
    }
}
