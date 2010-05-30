namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The number 3797 has an interesting property. Being prime itself, it is possible to continuously remove digits from left to right, and remain prime at each stage: 3797, 797, 97, and 7. Similarly we can work from right to left: 3797, 379, 37, and 3.
    /// 
    /// Find the sum of the only eleven primes that are both truncatable from left to right and right to left.
    /// 
    /// NOTE: 2, 3, 5, and 7 are not considered to be truncatable primes.
    /// </summary>
    [Result(Name = "sum", Expected = "748317")]
    public class Problem037 : Problem
    {
        public override string Solve(string resource)
        {
            var primes = PrimeMath.GetFirstNPrimes(60239);

            Func<long, long> truncRight = num =>
            {
                return num / 10;
            };

            Func<long, long> truncLeft = num =>
            {
                long digit = 1;
                while (digit <= num)
                {
                    digit *= 10;
                }

                digit /= 10;

                return num % digit;
            };

            long sum = 0;
            var count = 0;

            for (var primeIndex = 4; ; primeIndex++)
            {
                if (primeIndex >= primes.Primes.Count)
                {
                    PrimeMath.GetFirstNPrimes(primes.Primes.Count + 1000, primes);
                }

                var prime = primes.Primes[primeIndex];

                var trunc = truncRight(prime);
                bool truncatable = true;
                while (trunc != 0)
                {
                    if (!PrimeMath.IsPrime(trunc, primes))
                    {
                        truncatable = false;
                        break;
                    }

                    trunc = truncRight(trunc);
                }

                if (!truncatable)
                {
                    continue;
                }

                trunc = truncLeft(prime);
                while (trunc != 0)
                {
                    if (!PrimeMath.IsPrime(trunc, primes))
                    {
                        truncatable = false;
                        break;
                    }

                    trunc = truncLeft(trunc);
                }

                if (!truncatable)
                {
                    continue;
                }

                sum += prime;
                count++;
                if (count == 11)
                {
                    break;
                }
            }

            return sum.ToString();
        }
    }
}
