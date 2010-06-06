namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The proper divisors of a number are all the divisors excluding the number itself. For example, the proper divisors of 28 are 1, 2, 4, 7, and 14. As the sum of these divisors is equal to 28, we call it a perfect number.
    /// 
    /// Interestingly the sum of the proper divisors of 220 is 284 and the sum of the proper divisors of 284 is 220, forming a chain of two numbers. For this reason, 220 and 284 are called an amicable pair.
    /// 
    /// Perhaps less well known are longer chains. For example, starting with 12496, we form a chain of five numbers:
    /// 
    /// 12496 → 14288 → 15472 → 14536 → 14264 (→ 12496 → ...)
    /// 
    /// Since this chain returns to its starting point, it is called an amicable chain.
    /// 
    /// Find the smallest member of the longest amicable chain with no element exceeding one million.
    /// </summary>
    [Result(Name = "smallest", Expected = "14316")]
    public class Problem095 : Problem
    {
        public override string Solve(string resource)
        {
            var maxLength = 0;
            long maxLengthMin = 0;

            var primes = PrimeMath.GetFirstNPrimes(1000);
            var sums = new Dictionary<long, long>();

            for (int i = 2; i < 1000000; i++)
            {
                var allFactors = PrimeMath.GetAllFactors(PrimeMath.Factor(i, primes));
                allFactors.Remove(i);
                var sum = allFactors.Sum();
                if (sum != i)
                {
                    sums[i] = sum;
                }
            }

            var keys = sums.Keys.ToList();

            foreach (var key in keys)
            {
                var chain = new List<long>();

                var current = key;

                var foundAmicable = false;
                while (true)
                {
                    if (!sums.ContainsKey(current))
                    {
                        break;
                    }

                    if (chain.Contains(current))
                    {
                        foundAmicable = true;
                        break;
                    }

                    chain.Add(current);
                    
                    current = sums[current];
                }

                if (foundAmicable)
                {
                    if (key != current)
                    {
                        var realChain = new List<long>();

                        while (chain.Contains(current))
                        {
                            realChain.Add(current);
                            chain.Remove(current);
                            current = sums[current];
                        }

                        foreach (var item in chain)
                        {
                            sums.Remove(item);
                        }

                        chain = realChain;
                        current = chain.Min();
                    }

                    if (chain.Count > maxLength)
                    {
                        maxLength = chain.Count;
                        maxLengthMin = current;
                    }
                }

                foreach (var item in chain)
                {
                    sums.Remove(item);
                }
            }

            return maxLengthMin.ToString();
        }
    }
}
