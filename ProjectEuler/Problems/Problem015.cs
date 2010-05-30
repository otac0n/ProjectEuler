namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    /// <summary>
    /// Starting in the top left corner of a 2×2 grid, there are 6 routes (without backtracking) to the bottom right corner.
    /// 
    /// How many routes are there through a 20×20 grid?
    /// </summary>
    [Result(Name = "routes", Expected = "137846528820")]
    public class Problem015 : Problem
    {
        public override string Solve(string resource)
        {
            var paths = new Dictionary<Size, long>();

            Func<Size, long> lookup = null;
            lookup = (Size size) =>
            {
                if (size.Width > size.Height)
                {
                    return lookup(new Size(size.Height, size.Width));
                }

                if (size.Width == 0)
                {
                    return 1;
                }

                if (!paths.ContainsKey(size))
                {
                    paths[size] = lookup(new Size(size.Width - 1, size.Height)) + lookup(new Size(size.Width, size.Height - 1));
                }

                return paths[size];
            };

            var count = lookup(new Size(20, 20));

            return count.ToString();
        }
    }
}
