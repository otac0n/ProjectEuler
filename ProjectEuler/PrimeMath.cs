namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    public static class PrimeMath
    {
        private static readonly long[] smallPrimes = new long[] {
            2,3,5,7,11,13,17,19,23,29,31,37,41,43,47,53,59,61,67,71,
            73,79,83,89,97,101,103,107,109,113,127,131,137,139,149,151,157,163,167,173,
            179,181,191,193,197,199,211,223,227,229,233,239,241,251,257,263,269,271,277,281,
            283,293,307,311,313,317,331,337,347,349,353,359,367,373,379,383,389,397,401,409,
            419,421,431,433,439,443,449,457,461,463,467,479,487,491,499,503,509,521,523,541,
            547,557,563,569,571,577,587,593,599,601,607,613,617,619,631,641,643,647,653,659,
            661,673,677,683,691,701,709,719,727,733,739,743,751,757,761,769,773,787,797,809,
            811,821,823,827,829,839,853,857,859,863,877,881,883,887,907,911,919,929,937,941,
            947,953,967,971,977,983,991,997,1009,1013,1019,1021,1031,1033,1039,1049,1051,1061,
            1063,1069,1087,1091,1093,1097,1103,1109,1117,1123,1129,1151,1153,1163,1171,1181,1187,
            1193,1201,1213,1217,1223,1229,1231,1237,1249,1259,1277,1279,1283,1289,1291,1297,1301,
            1303,1307,1319,1321,1327,1361,1367,1373,1381,1399,1409,1423,1427,1429,1433,1439,1447,
            1451,1453,1459,1471,1481,1483,1487,1489,1493,1499,1511,1523,1531,1543,1549,1553,1559,
            1567,1571,1579,1583,1597,1601,1607,1609,1613,1619,1621,1627,1637,1657,1663,1667,1669,
            1693,1697,1699,1709,1721,1723,1733,1741,1747,1753,1759,1777,1783,1787,1789,1801,1811,
            1823,1831,1847,1861,1867,1871,1873,1877,1879,1889,1901,1907,1913,1931,1933,1949,1951,
            1973,1979,1987,1993,1997,1999,2003,2011,2017,2027,2029,2039,2053,2063,2069,2081,2083,
            2087,2089,2099,2111,2113,2129,2131,2137,2141,2143,2153,2161,2179,2203,2207,2213,2221,
            2237,2239,2243,2251,2267,2269,2273,2281,2287,2293,2297,2309
        };

        private static readonly long smallPrimesNextPrimeSquared = 2809;

        private static readonly int smallPrimesNextPrimeSquaredIndex = 15;

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

        public static BigInteger ModPow(BigInteger @base, BigInteger exponent, BigInteger modulus)
        {
            BigInteger result = 1;

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

        public static long Totient(long num, PrimesList primes)
        {
            int numPrimes = primes.Primes.Count;

            long totient = num;
            long currentNum = num, temp, p, prevP = 0;
            for (int primeIndex = 0; primeIndex < numPrimes; primeIndex++)
            {
                p = (int)primes.Primes[primeIndex];
                if (p > currentNum) break;
                temp = currentNum / p;
                if (temp * p == currentNum)
                {
                    currentNum = temp;
                    primeIndex--;
                    if (prevP != p)
                    {
                        prevP = p; totient -= (totient / p);
                    }
                }
            }

            return totient;
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

        private static PrimesList CreatePrimesList()
        {
            return new PrimesList
            {
                Primes = smallPrimes.ToList(),
                LargestValueChecked = smallPrimes[smallPrimes.Length - 1],
                NextPrimeSquared = smallPrimesNextPrimeSquared,
                NextPrimeSquaredIndex = smallPrimesNextPrimeSquaredIndex,
            };
        }

        public static PrimesList GetDefaultPrimes()
        {
            return CreatePrimesList();
        }

        public static PrimesList GetFirstNPrimes(long number)
        {
            var primes = CreatePrimesList();
                
            GetFirstNPrimes(number, primes);

            return primes;
        }

        public static PrimesList GetPrimesBelow(long max)
        {
            var primes = CreatePrimesList();

            GetPrimesBelow(max, primes);

            return primes;
        }

        public static void GetFirstNPrimes(long number, PrimesList state)
        {
            var primes = state.Primes;
            var nextPrimeSquared = state.NextPrimeSquared;
            var nextPrimeSquaredIndex = state.NextPrimeSquaredIndex;
            var num = state.LargestValueChecked + 1;

            if (primes.Count < number)
            {
                for ( ; ; num++)
                {
                    if (num == nextPrimeSquared)
                    {
                        nextPrimeSquaredIndex++;
                        nextPrimeSquared = primes[nextPrimeSquaredIndex];
                        nextPrimeSquared *= nextPrimeSquared;

                        continue;
                    }

                    bool prime = true;
                    for (var primeIndex = 0; primeIndex < nextPrimeSquaredIndex; primeIndex++)
                    {
                        if (num % primes[primeIndex] == 0)
                        {
                            prime = false;
                            break;
                        }
                    }

                    if (prime)
                    {
                        primes.Add(num);

                        if (primes.Count >= number)
                        {
                            break;
                        }
                    }
                }
            }

            state.NextPrimeSquared = nextPrimeSquared;
            state.NextPrimeSquaredIndex = nextPrimeSquaredIndex;
            state.LargestValueChecked = num;
        }

        public static void GetPrimesBelow(long max, PrimesList state)
        {
            var primes = state.Primes;
            var nextPrimeSquared = state.NextPrimeSquared;
            var nextPrimeSquaredIndex = state.NextPrimeSquaredIndex;
            var num = state.LargestValueChecked + 1;

            for ( ; num <= max; num++)
            {
                if (num == nextPrimeSquared)
                {
                    nextPrimeSquaredIndex++;
                    nextPrimeSquared = primes[nextPrimeSquaredIndex];
                    nextPrimeSquared *= nextPrimeSquared;

                    continue;
                }

                bool prime = true;
                for (var primeIndex = 0; primeIndex < nextPrimeSquaredIndex; primeIndex++)
                {
                    if (num % primes[primeIndex] == 0)
                    {
                        prime = false;
                        break;
                    }
                }

                if (prime)
                {
                    primes.Add(num);
                }
            }

            state.NextPrimeSquared = nextPrimeSquared;
            state.NextPrimeSquaredIndex = nextPrimeSquaredIndex;
            state.LargestValueChecked = num - 1;
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

                            if (primeIndex >= primes.Primes.Count)
                            {
                                break;
                            }
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

        public static Dictionary<long, int> Factor(long number, PrimesList state)
        {
            if (number <= 0)
            {
                throw new ArgumentOutOfRangeException("number");
            }

            var primeFactors = new Dictionary<long, int>();
            int primeIndex = 0;

            while (number != 1)
            {
                for (; primeIndex < state.Primes.Count; primeIndex++)
                {
                    var prime = state.Primes[primeIndex];

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
                    if (IsPrime(number, state))
                    {
                        primeFactors[number] = 1;
                        break;
                    }
                    else
                    {
                        GetPrimesBelow(number, state);
                    }
                }
            }

            return primeFactors;
        }

        public static Dictionary<long, long> FactorFactorial(long number, PrimesList state)
        {
            GetPrimesBelow(number, state);

            var factors = new Dictionary<long, long>();

            for (int primeIndex = 0; primeIndex < state.Primes.Count; primeIndex++)
            {
                var prime = state.Primes[primeIndex];
                if (prime > number)
                {
                    break;
                }

                var value = number;
                long count = 0;

                while (value > 1)
                {
                    var c = value / prime;
                    count += c;
                    value = c;
                }

                factors[prime] = count;
            }

            return factors;
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
