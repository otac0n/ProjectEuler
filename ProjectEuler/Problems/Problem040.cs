namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// An irrational decimal fraction is created by concatenating the positive integers:
    /// 
    /// 0.123456789101112131415161718192021...
    /// 
    /// It can be seen that the 12th digit of the fractional part is 1.
    /// 
    /// If d_n represents the nth digit of the fractional part, find the value of the following expression.
    /// 
    /// d_1 × d_10 × d_100 × d_1000 × d_10000 × d_100000 × d_1000000
    /// </summary>
    [Result(Name = "result", Expected = "210")]
    public class Problem040 : Problem
    {
        public override string Solve(string resource)
        {
            var sb = new StringBuilder(1000100);

            for (var i = 1; sb.Length < 1000000; i++)
            {
                sb.Append(i);
            }

            Func<int, int> d = index =>
            {
                return (int)(sb[index - 1] - '0');
            };

            var product = d(1) * d(10) * d(100) * d(1000) * d(10000) * d(100000) * d(1000000);
            return product.ToString();
        }
    }
}
