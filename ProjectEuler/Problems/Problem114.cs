namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// A row measuring seven units in length has red blocks with a minimum length of three units placed on it, such that any two red blocks (which are allowed to be different lengths) are separated by at least one black square. There are exactly seventeen ways of doing this.
    /// 
    /// How many ways can a row measuring fifty units in length be filled?
    /// 
    /// NOTE: Although the example above does not lend itself to the possibility, in general it is permitted to mix block sizes. For example, on a row measuring eight units in length you could use red (3), black (1), and red (4).
    /// </summary>
    [Result(Name = "count", Expected = "16475640049")]
    public class Problem114 : Problem
    {
        public override string Solve(string resource)
        {
            int cells = 50;
            List<int> blocks = new List<int>(Enumerable.Range(1, cells).Where(b => b >= 3));

            var cache = new Dictionary<int, long>();
            Func<int, long> lookup = null;
            lookup = (spaces) =>
            {
                if (!cache.ContainsKey(spaces))
                {
                    long c = 1;
                    foreach (var block in blocks)
                    {
                        if (block <= spaces)
                        {
                            var spacesLeft = spaces - block;
                            for (int i = 0; i <= spacesLeft; i++)
                            {
                                c += lookup(spacesLeft - i - 1);
                            }
                        }
                    }

                    cache[spaces] = c;
                }

                return cache[spaces];
            };

            long count = 0;
            
            count += lookup(cells);

            return count.ToString();
        }
    }
}
