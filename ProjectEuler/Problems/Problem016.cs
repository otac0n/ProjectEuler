namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// 2^15 = 32768 and the sum of its digits is 3 + 2 + 7 + 6 + 8 = 26.
    /// 
    /// What is the sum of the digits of the number 2^1000?
    /// </summary>
    [Result(Name = "sum", Expected = "1366")]
    public class Problem016 : Problem
    {
        public override string Solve(string resource)
        {
            BigInteger value = 1;

            for (var i = 0; i < 1000; i++)
            {
                value = value * 2;
            }

            var offset = (int)'0';
            var total = 0;
            var num = value.ToString();

            for (int i = 0; i < num.Length; i++)
            {
                total += num[i] - offset;
            }

            return total.ToString();
        }
    }
}
