namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// The number 145 is well known for the property that the sum of the factorial of its digits is equal to 145:
    /// 
    /// 1! + 4! + 5! = 1 + 24 + 120 = 145
    /// 
    /// Perhaps less well known is 169, in that it produces the longest chain of numbers that link back to 169; it turns out that there are only three such loops that exist:
    /// 
    /// 169 → 363601 → 1454 → 169
    /// 871 → 45361 → 871
    /// 872 → 45362 → 872
    /// 
    /// It is not difficult to prove that EVERY starting number will eventually get stuck in a loop. For example,
    /// 
    /// 69 → 363600 → 1454 → 169 → 363601 (→ 1454)
    /// 78 → 45360 → 871 → 45361 (→ 871)
    /// 540 → 145 (→ 145)
    /// 
    /// Starting with 69 produces a chain of five non-repeating terms, but the longest non-repeating chain with a starting number below one million is sixty terms.
    /// 
    /// How many chains, with a starting number below one million, contain exactly sixty non-repeating terms?
    /// </summary>
    [Result(Name = "result", Expected = "402")]
    public class Problem074 : Problem
    {
        public override string Solve(string resource)
        {
            var data = new Dictionary<long, int>()
            {
                { 169, 3 },  // 169 → 363601 → 1454 (→ 169)
                { 363601, 3 },  // 363601 → 1454 → 169 (→ 363601)
                { 1454, 3 },  // 1454 → 169 → 363601 (→ 1454)

                { 871, 2 },  // 871 → 45361 (→ 871)
                { 45361, 2 },  // 45361 → 871 (→ 45361)

                { 872, 2 },  // 872 → 45362 (→ 872)
                { 45362, 2 },  // 45362 → 872 (→ 45362)
            };

            Func<long, int> lookup = null;
            lookup = (num) =>
            {
                if (!data.ContainsKey(num))
                {
                    long newNum = 0;
                    foreach (var c in num.ToString())
                    {
                        newNum += NumberTheory.Factorial(c - '0');
                    }

                    if (newNum == num)
                    {
                        data[num] = 1;
                    }
                    else
                    {
                        data[num] = lookup(newNum) + 1;
                    }
                }

                return data[num];
            };

            int count = 0;

            for (long num = 1; num < 1000000; num++)
            {
                if (lookup(num) == 60)
                {
                    count++;
                }
            }

            return count.ToString();
        }
    }
}
