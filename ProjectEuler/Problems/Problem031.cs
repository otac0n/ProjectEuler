namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Drawing;

    /// <summary>
    /// In England the currency is made up of pound, £, and pence, p, and there are eight coins in general circulation:
    /// 
    ///     1p, 2p, 5p, 10p, 20p, 50p, £1 (100p) and £2 (200p).
    /// 
    /// It is possible to make £2 in the following way:
    /// 
    ///     1×£1 + 1×50p + 2×20p + 1×5p + 1×2p + 3×1p
    /// 
    /// How many different ways can £2 be made using any number of coins?
    /// </summary>
    [Result(Name = "count", Expected = "73682")]
    public class Problem031 : Problem
    {
        public override string Solve(string resource)
        {
            List<int> coinValues = new List<int>
            {
                200,
                100,
                50,
                20,
                10,
                5,
                2,
                1,
            };

            var counts = new Dictionary<Point, int>();

            Func<Point, int> lookup = null;
            lookup = point =>
            {
                if (counts.ContainsKey(point))
                {
                    return counts[point];
                }
                else
                {
                    var index = point.X;
                    var value = point.Y;
                    var coinValue = coinValues[index];

                    if (index == coinValues.Count - 1)
                    {
                        var sum = value % coinValue == 0 ? 1 : 0;
                        counts[point] = sum;
                        return sum;
                    }
                    else
                    {
                        var sum = lookup(new Point(index + 1, value));
                        while (value >= coinValue)
                        {
                            value -= coinValue;

                            sum += lookup(new Point(index + 1, value));
                        }

                        counts[point] = sum;
                        return sum;
                    }
                }
            };

            var count = lookup(new Point(0, 200));

            return count.ToString();
        }
    }
}
