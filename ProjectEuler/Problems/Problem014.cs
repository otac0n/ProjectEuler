namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The following iterative sequence is defined for the set of positive integers:
    /// 
    /// n → n/2 (n is even)
    /// n → 3n + 1 (n is odd)
    /// 
    /// Using the rule above and starting with 13, we generate the following sequence:
    /// 13 → 40 → 20 → 10 → 5 → 16 → 8 → 4 → 2 → 1
    /// 
    /// It can be seen that this sequence (starting at 13 and finishing at 1) contains 10 terms. Although it has not been proved yet (Collatz Problem), it is thought that all starting numbers finish at 1.
    /// 
    /// Which starting number, under one million, produces the longest chain?
    /// 
    /// NOTE: Once the chain starts the terms are allowed to go above one million.
    /// </summary>
    [Result(Name = "longest", Expected = "837799")]
    public class Problem014 : Problem
    {
        public override string Solve(string resource)
        {
            Func<long, long> getNext = (long n) =>
            {
                return n % 2 == 0 ? n / 2 : 3 * n + 1;
            };

            var lengths = new Dictionary<long, int>
            {
                { 1, 0 },
            };

            Func<long, int> lookup = null;
            lookup = (long n) =>
            {
                if (!lengths.ContainsKey(n))
                {
                    lengths[n] = 1 + lookup(getNext(n));
                }

                return lengths[n];
            };

            var max = 0;
            long maxNum = 1;

            for (long num = 2; num <= 1000000; num++)
            {
                var length = lookup(num);

                if (length > max)
                {
                    maxNum = num;
                    max = length;
                }
            }

            return maxNum.ToString();
        }
    }
}
