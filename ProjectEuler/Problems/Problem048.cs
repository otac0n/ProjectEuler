namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// The series, 1^(1) + 2^(2) + 3^(3) + ... + 10^(10) = 10405071317.
    /// 
    /// Find the last ten digits of the series, 1^(1) + 2^(2) + 3^(3) + ... + 1000^(1000).
    /// </summary>
    [Result(Name = "last digits", Expected = "9110846700")]
    public class Problem048 : Problem
    {
        public override string Solve(string resource)
        {
            BigInteger sum = 0;

            for (int i = 1; i <= 1000; i++)
            {
                BigInteger product = 1;

                for (int j = 0; j < i; j++)
                {
                    product *= i;
                }

                sum += product;
            }

            var sumString = sum.ToString();

            return sumString.Substring(sumString.Length - 10, 10);
        }
    }
}
