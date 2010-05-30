namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// n! means n × (n − 1) × ... × 3 × 2 × 1
    /// 
    /// Find the sum of the digits in the number 100!
    /// </summary>
    [Result(Name = "sum", Expected = "648")]
    public class Problem020 : Problem
    {
        public override string Solve(string resource)
        {
            BigInteger value = 1;

            for (var i = 1; i <= 100; i++)
            {
                value = value * i;
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
