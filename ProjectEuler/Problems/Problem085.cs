namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// By counting carefully it can be seen that a rectangular grid measuring 3 by 2 contains eighteen rectangles:
    /// 
    /// Although there exists no rectangular grid that contains exactly two million rectangles, find the area of the grid with the nearest solution.
    /// </summary>
    [Result(Name = "area", Expected = "2772")]
    public class Problem085 : Problem
    {
        public override string Solve(string resource)
        {
            var primes = PrimeMath.GetFirstNPrimes(100);

            BigInteger minDiff = 2000000;
            var minDiffArea = 0;

            for (int y = 1; ; y++)
            {
                var yCombinations = NumberTheory.nCr(y + 1, 2, primes);

                var x = 0;

                for (x = 1; ; x++)
                {
                    var xCombinations = NumberTheory.nCr(x + 1, 2, primes);

                    var value = yCombinations * xCombinations;
                    var diff = 2000000 - value;

                    if (diff > 0)
                    {
                        if (diff < minDiff)
                        {
                            minDiff = diff;
                            minDiffArea = x * y;
                        }
                    }
                    else
                    {
                        diff = -diff;
                        if (diff < minDiff)
                        {
                            minDiff = diff;
                            minDiffArea = x * y;
                        }

                        break;
                    }
                }

                if (x == 1)
                {
                    break;
                }
            }

            return minDiffArea.ToString();
        }
    }
}
