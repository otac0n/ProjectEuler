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
    [Result(Name = "blue", Expected = "")]
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
            // This implies that the square-root expression must evaluate to the SMALLEST ODD INTEGER
            //    that is greater than the expression √(2t_min^2 - 2t_min + 1).

            BigInteger total = BigInteger.Pow(10, 12);
            var desc = 2 * total * total - 2 * total + 1;

            BigInteger sqrt = 0;
            while (true)
            {
                desc += 4 * total++;

                if (NumberTheory.IsSquare(desc, out sqrt))
                {
                    break;
                }
            }
            
            var blue = (sqrt + 1) / 2;

            return blue.ToString() + "t" + total;
        }
    }
}
