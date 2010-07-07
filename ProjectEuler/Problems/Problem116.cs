namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// A row of five black square tiles is to have a number of its tiles replaced with coloured oblong tiles chosen from red (length two), green (length three), or blue (length four).
    /// 
    /// If red tiles are chosen there are exactly seven ways this can be done.
	/// 		
    /// If green tiles are chosen there are three ways.
    /// 
    /// And if blue tiles are chosen there are two ways.
    /// 
    /// Assuming that colours cannot be mixed there are 7 + 3 + 2 = 12 ways of replacing the black tiles in a row measuring five units in length.
    /// 
    /// How many different ways can the black tiles in a row measuring fifty units in length be replaced if colours cannot be mixed and at least one coloured tile must be used?
    /// </summary>
    [Result(Name = "count", Expected = "20492570929")]
    public class Problem116 : Problem
    {
        public override string Solve(string resource)
        {
            int cells = 50;
            List<int> blocks = null;

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

            blocks = new List<int>() { 2 };
            count += lookup(cells) - 1;

            cache.Clear();
            blocks = new List<int>() { 3 };
            count += lookup(cells) - 1;

            cache.Clear();
            blocks = new List<int>() { 4 };
            count += lookup(cells) - 1;

            return count.ToString();
        }
    }
}
