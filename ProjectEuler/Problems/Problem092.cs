namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A number chain is created by continuously adding the square of the digits in a number to form a new number until it has been seen before.
    /// 
    /// For example,
    /// 
    /// 44 → 32 → 13 → 10 → 1 → 1
    /// 85 → 89 → 145 → 42 → 20 → 4 → 16 → 37 → 58 → 89
    /// 
    /// Therefore any chain that arrives at 1 or 89 will become stuck in an endless loop. What is most amazing is that EVERY starting number will eventually arrive at 1 or 89.
    /// 
    /// How many starting numbers below ten million will arrive at 89?
    /// </summary>
    [Result(Name = "count", Expected = "8581146")]
    public class Problem092 : Problem
    {
        public override string Solve(string resource)
        {
            var count = 0;

            var values = new Dictionary<int, int>();

            Func<int, int> lookup = null;
            lookup = num =>
            {
                if (num == 1 || num == 89)
                {
                    return num;
                }
                
                if (!values.ContainsKey(num))
                {
                    var p = 0;
                    foreach (var c in num.ToString())
                    {
                        var cv = (int)(c - '0');
                        p += cv * cv;
                    }

                    values[num] = lookup(p);
                }

                return values[num];
            };

            for (int n = 1; n < 10000000; n++)
            {
                if (lookup(n) == 89)
                {
                    count++;
                }
            }

            return count.ToString();
        }
    }
}
