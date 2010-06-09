namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// If a box contains twenty-one coloured discs, composed of fifteen blue discs and six red discs, and two discs were taken at random, it can be seen that the probability of taking two blue discs, P(BB) = (15/21)×(14/20) = 1/2.
    /// 
    /// The next such arrangement, for which there is exactly 50% chance of taking two blue discs at random, is a box containing eighty-five blue discs and thirty-five red discs.
    /// 
    /// By finding the first arrangement to contain over 10^(12) = 1,000,000,000,000 discs in total, determine the number of blue discs that the box would contain
    /// </summary>
    [Result(Name = "blue", Expected = "756872327473")]
    public class Problem100 : Problem
    {
        public override string Solve(string resource)
        {
            // The probability of taking two blues (Pbb) is:
            // Pbb(t, b) = b/t * (b-1)/(t-1)

            // We are aiming for a probability of exactly 1/2:
            // b/t * (b-1)/(t-1) = 1/2

            // We want to know the total number of blue balls:
            // b = (√(2t^2 - 2t + 1) + 1) / 2

            // So, the numerator of the fraction must be an EVEN INTEGER.
            // This implies that the square-root expression must evaluate to an ODD INTEGER
            //    that is greater than the expression √(2t_min^2 - 2t_min + 1).

            // We can further simplify this into:
            // For an odd perfect square I, if 2*i^2 - 1 is an odd perfect square,
            // b = (i + 1) / 2
            // t = (√(2i^2 - 1) + 1) / 2

            // This can be expressed as the equation:
            // 2i^2 - 1 = p^2
            // for some integers i and p.

            // This can turn into a pell equation:
            // x^2 - ny^2 = -1
            // with n = 2, where x and y are also odd.

            // The primary solution to this equation is x = 1, y = 1
            // Subsequent solutions are found by:
            // x_i = x_1 * x_(i-1) + n * y_1 * y_(i-1)
            // y_i = x_1 * y_(i-1) + y_1 * x_(i-1)

            var minTotal = BigInteger.Pow(10, 12);

            var n = 2;
            BigInteger x1 = 1;
            BigInteger y1 = 1;

            BigInteger xi = x1;
            BigInteger yi = y1;

            while (true)
            {
                var xip1 = x1 * xi + n * y1 * yi;
                var yip1 = x1 * yi + y1 * xi;

                if (!xip1.IsEven && !yip1.IsEven)
                {
                    var total = ((2 * BigInteger.Pow(yip1, 2) - 1).Sqrt() + 1) / 2;

                    if (total > minTotal)
                    {
                        var blue = (yip1 + 1) / 2;

                        return blue.ToString();
                    }
                }

                xi = xip1;
                yi = yip1;
            }
        }
    }
}
