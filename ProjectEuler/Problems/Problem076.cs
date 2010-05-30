namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Drawing;

    /// <summary>
    /// It is possible to write five as a sum in exactly six different ways:
    /// 
    /// 4 + 1
    /// 3 + 2
    /// 3 + 1 + 1
    /// 2 + 2 + 1
    /// 2 + 1 + 1 + 1
    /// 1 + 1 + 1 + 1 + 1
    /// 
    /// How many different ways can one hundred be written as a sum of at least two positive integers?
    /// </summary>
    [Result(Name = "count", Expected = "190569291")]
    public class Problem076 : Problem
    {
        public override string Solve(string resource)
        {
            var target = 100;

            List<int> coinValues = Enumerable.Range(1, target - 1).OrderByDescending(c => c).ToList();

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

            var count = lookup(new Point(0, target));

            return count.ToString();
        }
    }
}
