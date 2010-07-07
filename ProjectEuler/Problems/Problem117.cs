namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// Using a combination of black square tiles and oblong tiles chosen from: red tiles measuring two units, green tiles measuring three units, and blue tiles measuring four units, it is possible to tile a row measuring five units in length in exactly fifteen different ways.
    /// 			
    /// How many ways can a row measuring fifty units in length be tiled?
    /// </summary>
    [Result(Name = "count", Expected = "100808458960497")]
    public class Problem117 : Problem
    {
        public override string Solve(string resource)
        {
            int cells = 50;
            List<int> blocks = new List<int>() { 2, 3, 4 };

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
                                c += lookup(spacesLeft - i);
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
