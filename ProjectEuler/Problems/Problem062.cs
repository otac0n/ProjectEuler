namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Combinatorics;

    /// <summary>
    /// The cube, 41063625 (345^(3)), can be permuted to produce two other cubes: 56623104 (384^(3)) and 66430125 (405^(3)). In fact, 41063625 is the smallest cube which has exactly three permutations of its digits which are also cube.
    /// 
    /// Find the smallest cube for which exactly five permutations of its digits are cube.
    /// </summary>
    [Result(Name = "smallest", Expected = "")]
    public class Problem062 : Problem
    {
        public override string Solve(string resource)
        {
            var numbers = new Dictionary<string, List<long>>();

            var digits = 1;
            for (long i = 1; ; i++)
            {
                var cube = i * i * i;

                var cubeStr = cube.ToString();

                if (cubeStr.Length > digits)
                {
                    var perms = (from n in numbers
                                 where n.Value.Count == 5
                                 select n.Value).SelectMany(n => n).ToList();
                    
                    if (perms.Count > 0)
                    {
                        return perms.Min().ToString();
                    }

                    numbers.Clear();

                    digits = cubeStr.Length;
                }

                var key = string.Join(",", from c in cubeStr
                                           group c by c into d
                                           orderby d.Key
                                           select d.Key + "=" + d.Count());
                
                if (!numbers.ContainsKey(key))
                {
                    numbers[key] = new List<long>();
                }

                numbers[key].Add(cube);
            }
        }
    }
}
