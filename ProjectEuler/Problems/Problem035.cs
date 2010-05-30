namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The number, 197, is called a circular prime because all rotations of the digits: 197, 971, and 719, are themselves prime.
    /// 
    /// There are thirteen such primes below 100: 2, 3, 5, 7, 11, 13, 17, 31, 37, 71, 73, 79, and 97.
    /// 
    /// How many circular primes are there below one million?
    /// </summary>
    [Result(Name = "count", Expected = "55")]
    public class Problem035 : Problem
    {
        public override string Solve(string resource)
        {
            var primes = PrimeMath.GetPrimesBelow(1000000);

            var candidates = new List<long>(primes.Primes);
            var circular = new List<long>();

            Func<string, string> rotate = num =>
            {
                return num.Length == 1 ? num : num.Substring(1, num.Length - 1) + num.Substring(0, 1);
            };

            while (candidates.Count > 0)
            {
                var candidate = candidates[0];
                candidates.RemoveAt(0);

                var found = new List<long>();
                found.Add(candidate);

                var mismatch = false;

                var next = rotate(candidate.ToString());

                if (next.Contains('0') ||
                    next.Contains('2') ||
                    next.Contains('4') ||
                    next.Contains('5') ||
                    next.Contains('6') ||
                    next.Contains('8'))
                {
                    if (next.Length == 1)
                    {
                        circular.Add(candidate);
                    }

                    continue;
                }

                var nextLong = long.Parse(next);
                while (nextLong != candidate)
                {
                    found.Add(nextLong);
                    var index = candidates.IndexOf(nextLong);

                    if (index < 0)
                    {
                        mismatch = true;
                    }
                    else
                    {
                        candidates.RemoveAt(index);
                    }

                    next = rotate(next);
                    nextLong = long.Parse(next);
                }

                if (!mismatch)
                {
                    circular.AddRange(found);
                }
            }

            var count = circular.Count;

            return count.ToString();
        }
    }
}
