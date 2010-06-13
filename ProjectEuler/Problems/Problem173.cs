namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// We shall define a square lamina to be a square outline with a square "hole" so that the shape possesses vertical and horizontal symmetry. For example, using exactly thirty-two square tiles we can form two different square laminae:
    /// 
    /// With one-hundred tiles, and not necessarily using all of the tiles at one time, it is possible to form forty-one different square laminae.
    /// 
    /// Using up to one million tiles how many different square laminae can be formed?
    /// </summary>
    [Result(Name = "count", Expected = "")]
    public class Problem173 : Problem
    {
        public override string Solve(string resource)
        {
            // Given that the laminae must be square, and the "hole" must be square,
            // the number of laminae that can be formed by N tiles are solutions to the equations:
            // x^2 - y^2 = N
            // Where:
            // x > y (it is not "inside-out")
            // y > 0 (meaning that it has a "hole")

            // Given that the laminae must be symmetrical horizontally and vertically,
            // x ||| y (mod 2)

            var max = 1000000;
            var count = 0;

            for (var y = 1; ; y++)
            {
                var found = false;
                var ySq = (BigInteger)y * y;
                for (var x = y + 2; ; x += 2)
                {
                    var xSq = (BigInteger)x * x;
                    if (xSq - ySq > max)
                    {
                        break;
                    }

                    count++;
                    found = true;
                }

                if (found == false)
                {
                    break;
                }
            }

            return count.ToString();
        }
    }
}
