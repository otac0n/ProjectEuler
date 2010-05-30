namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// An even positive integer N will be called admissible, if it is a power of 2 or its distinct prime factors are consecutive primes.
    /// The first twelve admissible numbers are 2,4,6,8,12,16,18,24,30,32,36,48.
    /// 
    /// If N is admissible, the smallest integer M > 1 such that N+M is prime, will be called the pseudo-Fortunate number for N.
    /// 
    /// For example, N=630 is admissible since it is even and its distinct prime factors are the consecutive primes 2,3,5 and 7.
    /// The next prime number after 631 is 641; hence, the pseudo-Fortunate number for 630 is M=11.
    /// It can also be seen that the pseudo-Fortunate number for 16 is 3.
    /// 
    /// Find the sum of all distinct pseudo-Fortunate numbers for admissible numbers N less than 10^(9).
    /// </summary>
    [Result(Name = "sum", Expected = "2209")]
    public class Problem293 : Problem
    {
        public override string Solve(string resource)
        {
            var primes = PrimeMath.GetFirstNPrimes(20);

            var admissibleNumbers = new List<long>(GenerateAdmissible(1, 0, 1000000000, primes).OrderBy(i => i));

            var sqrtLargest = (long)Math.Sqrt(admissibleNumbers[admissibleNumbers.Count - 1]);
            PrimeMath.GetPrimesBelow((long)(sqrtLargest * 1.1), primes);
            var largestPrime = primes.Primes[primes.Primes.Count - 1];

            var pseudoFortunate = new List<long>();

            var primeIndex = 0;
            long nextLowestPrime = 0;
            foreach (var admissible in admissibleNumbers)
            {
                var minPrime = admissible + 2;
                if (nextLowestPrime >= minPrime)
                {
                }
                else if (largestPrime >= minPrime)
                {
                    for (; ; primeIndex++)
                    {
                        var prime = primes.Primes[primeIndex];
                        if (prime >= minPrime)
                        {
                            nextLowestPrime = prime;
                            break;
                        }
                    }
                }
                else
                {
                    for (long candidate = minPrime; ; candidate++)
                    {
                        var highestPossibleFactor = (long)Math.Sqrt(candidate);

                        if (highestPossibleFactor > largestPrime)
                        {
                            return "error";
                        }

                        bool found = true;
                        for (int i = 0; i < primes.Primes.Count; i++)
                        {
                            var factor = primes.Primes[i];
                            if (candidate % factor == 0)
                            {
                                found = false;
                                break;
                            }
                        }

                        if (found)
                        {
                            nextLowestPrime = candidate;
                            break;
                        }
                    }
                }

                pseudoFortunate.Add(nextLowestPrime - admissible);
            }

            var sum = pseudoFortunate.Distinct().Sum();

            return sum.ToString();
        }

        private static IEnumerable<long> GenerateAdmissible(long currentProduct, int primeIndex, long max, PrimesList primes)
        {
            if (currentProduct < max)
            {
                if (currentProduct != 1)
                {
                    yield return currentProduct;
                }

                var prime = primes.Primes[primeIndex];

                var product = currentProduct * prime;

                while (product < max)
                {
                    foreach (var admissable in GenerateAdmissible(product, primeIndex + 1, max, primes))
                    {
                        yield return admissable;
                    }

                    product *= prime;
                }
            }
        }
    }
}
