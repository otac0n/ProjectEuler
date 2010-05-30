namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Drawing;

    /// <summary>
    /// By starting at the top of the triangle below and moving to adjacent numbers on the row below, the maximum total from top to bottom is 23.
    /// 
    /// 3
    /// 7 4
    /// 2 4 6
    /// 8 5 9 3
    /// 
    /// That is, 3 + 7 + 4 + 9 = 23.
    /// 
    /// Find the maximum total from top to bottom in triangle.txt, a 15K text file containing a triangle with one-hundred rows.
    /// </summary>
    [ProblemResource("triangle")]
    [Result(Name = "max total", Expected = "7273")]
    public class Problem067 : Problem
    {
        public override string Solve(string resource)
        {
            var data = (from line in resource.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                        select
                            (from entry in line.Split(" \t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                             select int.Parse(entry)).ToArray()).ToArray();

            var values = new Dictionary<Point, long>();

            Func<Point, long> lookup = null;
            lookup = (Point point) =>
            {
                if (point.X >= data.Length)
                {
                    return 0;
                }

                if (!values.ContainsKey(point))
                {
                    var row = point.X;
                    var col = point.Y;

                    values[point] = data[row][col] + Math.Max(lookup(new Point(row + 1, col)), lookup(new Point(row + 1, col + 1)));
                }

                return values[point];
            };

            var max = lookup(new Point(0, 0));

            return max.ToString();
        }
    }
}
