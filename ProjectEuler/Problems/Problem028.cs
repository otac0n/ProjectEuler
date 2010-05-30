namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Starting with the number 1 and moving to the right in a clockwise direction a 5 by 5 spiral is formed as follows:
    /// 
    /// 21 22 23 24 25
    /// 20  7  8  9 10
    /// 19  6  1  2 11
    /// 18  5  4  3 12
    /// 17 16 15 14 13
    /// 
    /// It can be verified that the sum of the numbers on the diagonals is 101.
    /// 
    /// What is the sum of the numbers on the diagonals in a 1001 by 1001 spiral formed in the same way?
    /// </summary>
    [Result(Name = "sum", Expected = "669171001")]
    public class Problem028 : Problem
    {
        public override string Solve(string resource)
        {
            var size = 1001;

            var max = size * size;

            var total = 0;
            var inc = 2;
            var counter = -1;
            for (int i = 1; i <= max; i += inc)
            {
                total += i;
                counter++;

                if (counter == 4)
                {
                    counter = 0;
                    inc += 2;
                }
            }

            return total.ToString();
        }
    }
}
