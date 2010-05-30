namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class PrimeMath
    {
        public static long ModPow(long @base, long exponent, long modulus)
        {
            long result = 1;

            while (exponent > 0)
            {
                if ((exponent & 1) != 0)
                {
                    result = (result * @base) % modulus;
                }

                exponent >>= 1;
                @base = (@base * @base) % modulus;
            }

            return result;
        }

        public static bool IsPossiblyPrime(long number, int rounds)
        {
            if (rounds <= 1)
            {
                return ModPow(2, number - 1, number) == 1;
            }
            else
            {
                Random r = new Random();
                for (int i = 0; i < rounds; i++)
                {
                    var b = r.Next(2, (int)Math.Min(number, int.MaxValue));
                    if (ModPow(b, number - 1, number) != 1)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public static PrimesList GetFirstNPrimes(long number)
        {
            var primes = new PrimesList
            {
                Primes = new List<long>
                {
                    2,
                },
                LargestValueChecked = 2,
            };

            GetFirstNPrimes(number, primes);

            return primes;
        }

        public static PrimesList GetPrimesBelow(long max)
        {
            var primes = new PrimesList
            {
                Primes = new List<long>
                {
                    2,
                },
                LargestValueChecked = 2,
            };

            GetPrimesBelow(max, primes);

            return primes;
        }

        public static void GetFirstNPrimes(long number, PrimesList currentState)
        {
            var primes = currentState.Primes;
            int primeIndex = primes.Count - 1;
            int compositeIndex = primeIndex + 1;
            long largestAdded = currentState.LargestValueChecked;

            do
            {
                if (primeIndex == primes.Count - 1)
                {
                    if (primes.Count >= number)
                    {
                        break;
                    }

                    for (long num = largestAdded + 1; num <= largestAdded + 1000; num++)
                    {
                        primes.Add(num);
                    }

                    largestAdded = largestAdded + 1000;

                    primeIndex = 0;
                }

                var prime = primes[primeIndex];

                for (int check = compositeIndex; check < primes.Count; )
                {
                    if (primes[check] % prime == 0)
                    {
                        primes.RemoveAt(check);
                    }
                    else
                    {
                        check++;
                    }
                }

                primeIndex++;
                compositeIndex = Math.Max(compositeIndex, primeIndex + 1);
            }
            while (primeIndex < primes.Count);

            currentState.LargestValueChecked = largestAdded;
        }

        public static void GetPrimesBelow(long max, PrimesList currentState)
        {
            var primes = currentState.Primes;
            int primeIndex = primes.Count - 1;
            int compositeIndex = primeIndex + 1;
            long largestAdded = currentState.LargestValueChecked;

            do
            {
                if (primeIndex == primes.Count - 1)
                {
                    if (largestAdded >= max)
                    {
                        break;
                    }

                    for (long num = largestAdded + 1; num <= largestAdded + 1000 && num <= max; num++)
                    {
                        primes.Add(num);
                    }

                    largestAdded = Math.Min(largestAdded + 1000, max);

                    primeIndex = 0;
                }

                var prime = primes[primeIndex];

                for (int check = compositeIndex; check < primes.Count; )
                {
                    if (primes[check] % prime == 0)
                    {
                        primes.RemoveAt(check);
                    }
                    else
                    {
                        check++;
                    }
                }

                primeIndex++;
                compositeIndex = Math.Max(compositeIndex, primeIndex + 1);
            }
            while (primeIndex < primes.Count);

            currentState.LargestValueChecked = largestAdded;
        }
        public static bool IsPrime(long value, PrimesList primes)
        {
            if (primes.LargestValueChecked >= value)
            {
                var l = 0;
                var r = primes.Primes.Count + 1;

                while (true)
                {
                    var p = (r - l) / 2;

                    if (p == 0)
                    {
                        return false;
                    }

                    p += l;

                    var comp = primes.Primes[p - 1].CompareTo(value);

                    if (comp == 0)
                    {
                        return true;
                    }
                    else if (comp < 0)
                    {
                        l = p;
                    }
                    else if (comp > 0)
                    {
                        r = p;
                    }
                }
            }
            else
            {
                var maxFactor = (long)Math.Sqrt(value);

                if (primes.LargestValueChecked < maxFactor)
                {
                    if (primes.LargestValueChecked > 1000 && !PrimeMath.IsPossiblyPrime(value, 3))
                    {
                        return false;
                    }
                }

                int primeIndex = 0;
                var prime = primes.Primes[primeIndex];
                while (prime <= maxFactor)
                {
                    if (value % prime == 0)
                    {
                        return false;
                    }

                    primeIndex++;

                    if (primeIndex >= primes.Primes.Count)
                    {
                        if (primes.LargestValueChecked < maxFactor)
                        {
                            primes = GetPrimesBelow(maxFactor);
                        }
                        else
                        {
                            break;
                        }
                    }

                    prime = primes.Primes[primeIndex];
                }

                return true;
            }
        }

        public static Dictionary<long, int> Factor(long number, PrimesList currentState)
        {
            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException("number");
            }

            var primeFactors = new Dictionary<long, int>();
            int primeIndex = 0;

            while (number != 1)
            {
                for (; primeIndex < currentState.Primes.Count; primeIndex++)
                {
                    var prime = currentState.Primes[primeIndex];

                    while (number % prime == 0)
                    {
                        number /= prime;

                        if (!primeFactors.ContainsKey(prime))
                        {
                            primeFactors[prime] = 0;
                        }

                        primeFactors[prime]++;
                    }
                }

                if (number != 1)
                {
                    GetFirstNPrimes(currentState.Primes.Count + 100, currentState);
                }
            }

            return primeFactors;
        }

        public static List<long> GetAllFactors(Dictionary<long, int> primeFactors)
        {
            return GetSubFactors(primeFactors).ToList();
        }

        public static IEnumerable<long> GetSubFactors(IEnumerable<KeyValuePair<long, int>> primeFactors)
        {
            long product = 1;

            if (primeFactors.Count() == 0)
            {
                yield return product;
                yield break;
            }

            var factor = primeFactors.First();
            for (int i = 0; i <= factor.Value; i++, product *= factor.Key)
            {
                foreach (var otherFactor in GetSubFactors(primeFactors.Skip(1)))
                {
                    yield return otherFactor * product;
                }
            }

            yield break;
        }
    }
}
