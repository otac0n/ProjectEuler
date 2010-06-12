namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The square root of 2 can be written as an infinite continued fraction.
    /// 
    /// The infinite continued fraction can be written, √2 = [1;(2)], (2) indicates that 2 repeats ad infinitum. In a similar way, √23 = [4;(1,3,1,8)].
    /// 
    /// It turns out that the sequence of partial values of continued fractions for square roots provide the best rational approximations. Let us consider the convergents for √2.
    /// Hence the sequence of the first ten convergents for √2 are:
    /// 1, 3/2, 7/5, 17/12, 41/29, 99/70, 239/169, 577/408, 1393/985, 3363/2378, ...
    /// 
    /// What is most surprising is that the important mathematical constant,
    /// e = [2; 1,2,1, 1,4,1, 1,6,1 , ... , 1,2k,1, ...].
    /// 
    /// The first ten terms in the sequence of convergents for e are:
    /// 2, 3, 8/3, 11/4, 19/7, 87/32, 106/39, 193/71, 1264/465, 1457/536, ...
    /// 
    /// The sum of digits in the numerator of the 10^(th) convergent is 1+4+5+7=17.
    /// 
    /// Find the sum of digits in the numerator of the 100^(th) convergent of the continued fraction for e.
    /// </summary>
    [Result(Name = "digital sum", Expected = "272")]
    public class Problem065 : Problem
    {
        public override string Solve(string resource)
        {
            Func<int, int> eFrac = n =>
            {
                if (n <= 0)
                {
                    return 0;
                }

                if (n == 1)
                {
                    return 2;
                }

                return n % 3 == 0 ? 2 * n / 3 : 1;
            };

            var denom = new BigFraction(1, eFrac(100));

            for (int i = 99; ; i--)
            {
                var addend = eFrac(i);

                if (addend == 0)
                {
                    break;
                }

                denom = new BigFraction(addend * denom.Numerator + denom.Denominator, denom.Numerator);
            }

            var divisor = denom.Numerator.GCD(denom.Denominator);

            var numerator = denom.Numerator / divisor;

            return numerator.DigitalSum().ToString();
        }
    }
}
