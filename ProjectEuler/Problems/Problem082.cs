namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Drawing;

    /// <summary>
    /// NOTE: This problem is a more challenging version of Problem 81.
    /// 
    /// The minimal path sum in the 5 by 5 matrix below, by starting in any cell in the left column and finishing in any cell in the right column, and only moving up, down, and right, is indicated in red and bold; the sum is equal to 994.
    /// 
    /// 
    ///  131	 673 	[234]	[103] 	[ 18]
    /// [201]	[ 96]	[342]	 965 	 150 
    ///  630 	 803 	 746	 422	 111 
    ///  537 	 699 	 497 	 121	 956 
    ///  805 	 732 	 524 	  37	 331
    /// 
    /// 
    /// Find the minimal path sum, in matrix.txt (right click and 'Save Link/Target As...'), a 31K text file containing a 80 by 80 matrix, from the left column to the right column.
    /// </summary>
    [ProblemResource("matrix")]
    [Result(Name = "min total", Expected = "260324")]
    public class Problem082 : Problem
    {
        public override string Solve(string resource)
        {
            var grid = (from l in resource.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                        select (from i in l.Split(',')
                                select int.Parse(i)).ToArray()).ToArray();

            var height = grid.Length;
            var width = grid[0].Length;

            var values = new Dictionary<PointDirection, int>();

            Func<PointDirection, int> lookup = null;
            lookup = point =>
            {
                if (values.ContainsKey(point))
                {
                    return values[point];
                }

                var x = point.Point.X;
                var y = point.Point.Y;

                var sum = grid[y][x];

                if (x == width - 1)
                {
                    return sum;
                }
                else
                {
                    var min = lookup(new PointDirection(new Point(x + 1, y), 0));

                    if ((point.Direction == -1 || point.Direction == 0) && y > 0)
                    {
                        min = Math.Min(min, lookup(new PointDirection(new Point(x, y - 1), -1)));
                    }

                    if ((point.Direction == +1 || point.Direction == 0) && y != height - 1)
                    {
                        min = Math.Min(min, lookup(new PointDirection(new Point(x, y + 1), +1)));
                    }

                    sum += min;
                }

                values[point] = sum;
                return sum;
            };

            int? lowest = null;

            for (int y = 0; y < grid.Length; y++)
            {
                var min = grid[y][0];
                min += lookup(new PointDirection(new Point(1, y), 0));

                if (!lowest.HasValue)
                {
                    lowest = min;
                }
                else
                {
                    lowest = Math.Min(min, lowest.Value);
                }
            }

            return lowest.ToString();
        }

        private struct PointDirection
        {
            private readonly Point point;
            private readonly int direction;

            public PointDirection(Point point, int direction)
            {
                if (direction > 1 || direction < -1)
                {
                    throw new ArgumentOutOfRangeException("direction");
                }

                this.point = point;
                this.direction = direction;
            }

            public Point Point
            {
                get
                {
                    return this.point;
                }
            }

            public int Direction
            {
                get
                {
                    return this.direction;
                }
            }

            public static bool operator ==(PointDirection pd1, PointDirection pd2)
            {
                if (object.ReferenceEquals(pd1, pd2))
                {
                    return true;
                }

                if ((object)pd1 == null || (object)pd2 == null)
                {
                    return false;
                }

                return pd1.direction == pd2.direction && pd1.point == pd2.point;
            }

            public static bool operator !=(PointDirection pd1, PointDirection pd2)
            {
                if (object.ReferenceEquals(pd1, pd2))
                {
                    return false;
                }

                if ((object)pd1 == null || (object)pd2 == null)
                {
                    return true;
                }

                return pd1.direction != pd2.direction || pd1.point != pd2.point;
            }

            public bool Equals(PointDirection other)
            {
                return this == other;
            }

            public override int GetHashCode()
            {
                return this.Point.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj == null || !(obj is PointDirection))
                {
                    return false;
                }

                return this.Equals((PointDirection)obj);
            }
        }
    }
}
