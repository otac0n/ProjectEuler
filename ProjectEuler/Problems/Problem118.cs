namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Combinatorics;

    /// <summary>
    /// Using all of the digits 1 through 9 and concatenating them freely to form decimal integers, different sets can be formed. Interestingly with the set {2,5,47,89,631}, all of the elements belonging to it are prime.
    /// 
    /// How many distinct sets containing each of the digits one through nine exactly once contain only prime elements?
    /// </summary>
    [Result(Name = "count", Expected = "44680")]
    public class Problem118 : Problem
    {
        public override string Solve(string resource)
        {
            // There are no 9 digit primes with unique digits, because 9 + 8 + 7 + 6 + 5 + 4 + 3 + 2 + 1 = 45, and 45 is divisible by three.
            // Therefore, all numbers that have all of the digits 1-9 must be divisible by 3.

            var primes = PrimeMath.GetDefaultPrimes();

            long count = 0;

            // Count all permutations of 1-9 digit pandigitals as: {abcdefgh, i}, if a and bcdefghi are both prime.
            foreach (IList<int> digits in new Permutations<int>(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, GenerateOption.WithoutRepetition))
            {
                if (!PrimeMath.IsPrime(digits[8], primes))
                {
                    continue;
                }

                var num = 0;
                for (int i = 0; i < 8; i++)
                {
                    num = (num * 10) + digits[i];
                }

                if (PrimeMath.IsPrime(num, primes))
                {
                    count++;
                }
            }

            // Get all primes below the largest 7 digit number with distinct digits.
            PrimeMath.GetPrimesBelow(9876543, primes);

            var counts = new Dictionary<int, int>();

            // Generate counts for all 7-digit-or-less primes.
            long digit = 0;
            int digitMask = 0;
            foreach (var prime in primes.Primes)
            {
                var num = prime;
                int mask = 0;

                var unique = true;
                while (num != 0)
                {
                    num = Math.DivRem(num, 10, out digit);

                    if (digit == 0)
                    {
                        unique = false;
                        break;
                    }

                    digitMask = 1 << ((int)digit - 1);

                    if ((mask & digitMask) != 0)
                    {
                        unique = false;
                        break;
                    }

                    mask |= digitMask;
                }

                if (unique)
                {
                    if (!counts.ContainsKey(mask))
                    {
                        counts[mask] = 0;
                    }

                    counts[mask]++;
                }
            }

            Func<int, long> lookup = null;
            lookup = (mask) =>
            {
                if (mask == 0x1FF)
                {
                    return 1;
                }

                long c = 0;

                foreach (var m in counts.Keys)
                {
                    if (m < mask || (m & mask) != 0)
                    {
                        continue;
                    }

                    var mul = counts[m];

                    c += lookup(m | mask) * mul;
                }

                return c;
            };

            count += lookup(0);

            return count.ToString();
        }
    }
}
