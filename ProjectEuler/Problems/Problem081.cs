namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Drawing;

    /// <summary>
    /// In the 5 by 5 matrix below, the minimal path sum from the top left to the bottom right, by only moving to the right and down, is indicated in bold red and is equal to 2427.
    /// 
    /// [131]	 673 	 234 	 103 	  18
    /// [201]	[ 96] 	[342]	 965 	 150 
    ///  630 	 803 	[746]	[422]	 111 
    ///  537 	 699 	 497 	[121]	 956 
    ///  805 	 732 	 524 	[ 37]	[331]
    /// 
    /// Find the minimal path sum, in matrix.txt, a 31K text file containing a 80 by 80 matrix, from the top left to the bottom right by only moving right and down.
    /// </summary>
    [ProblemResource("matrix")]
    [Result(Name = "min total", Expected = "427337")]
    public class Problem081 : Problem
    {
        public override string Solve(string resource)
        {
            var grid = (from l in resource.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                        select (from i in l.Split(',')
                                select int.Parse(i)).ToArray()).ToArray();

            var height = grid.Length;
            var width = grid[0].Length;

            var values = new Dictionary<Point, int>();

            Func<Point, int> lookup = null;
            lookup = point =>
            {
                if (values.ContainsKey(point))
                {
                    return values[point];
                }

                var x = point.X;
                var y = point.Y;

                var sum = grid[y][x];

                if (x == width - 1)
                {
                    if (y == height - 1)
                    {
                        return sum;
                    }

                    sum += lookup(new Point(x, y + 1));
                }
                else if (y == height - 1)
                {
                    sum += lookup(new Point(x + 1, y));
                }
                else
                {
                    sum += Math.Min(
                        lookup(new Point(x + 1, y)),
                        lookup(new Point(x, y + 1)));
                }

                values[point] = sum;
                return sum;
            };

            var min = lookup(new Point(0, 0));

            return min.ToString();
        }
    }
}
