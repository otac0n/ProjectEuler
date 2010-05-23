using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using System.Numerics;
using System.Drawing;
using System.Combinatorics;

namespace ProjectEuler
{
    public class Program
    {
        private class PrimesList
        {
            public List<long> Primes
            {
                get;
                set;
            }

            public long LargestValueChecked
            {
                get;
                set;
            }
        }

        private static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();

            Problem_26();

            sw.Stop();
            Console.WriteLine();
            Console.WriteLine("Solution Found in " + sw.Elapsed + ".");
            Console.ReadKey(true);
        }

        private static PrimesList GetPrimesBelow(long max)
        {
            return GetPrimesBelow(max, new PrimesList
            {
                Primes = new List<long>
                {
                    2,
                },
                LargestValueChecked = 2,
            });
        }

        private static PrimesList GetPrimesBelow(long max, PrimesList currentState)
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
            return currentState;
        }

        private static PrimesList GetFirstNPrimes(long number)
        {
            return GetFirstNPrimes(number, new PrimesList
            {
                Primes = new List<long>
                {
                    2,
                },
                LargestValueChecked = 2,
            });
        }

        private static PrimesList GetFirstNPrimes(long number, PrimesList currentState)
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
            return currentState;
        }

        private static Dictionary<long, int> Factor(long number, PrimesList currentState)
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
                    currentState = GetFirstNPrimes(currentState.Primes.Count + 100, currentState);
                }
            }

            return primeFactors;
        }

        private static List<long> GetAllFactors(Dictionary<long, int> primeFactors)
        {
            return GetSubFactors(primeFactors).ToList();
        }

        private static IEnumerable<long> GetSubFactors(IEnumerable<KeyValuePair<long, int>> primeFactors)
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

        /// <summary>
        /// If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.
        ///
        /// Find the sum of all the multiples of 3 or 5 below 1000.
        /// </summary>
        private static void Problem_01()
        {
            var numbers = new List<int>();

            for (int i = 1; i < 1000; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    numbers.Add(i);
                    continue;
                }
            }

            Console.WriteLine("sum = " + numbers.Sum());
        }

        /// <summary>
        /// Each new term in the Fibonacci sequence is generated by adding the previous two terms. By starting with 1 and 2, the first 10 terms will be:
        ///
        /// 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, ...
        ///
        /// Find the sum of all the even-valued terms in the sequence which do not exceed four million.
        /// </summary>
        private static void Problem_02()
        {
            long nm1 = 1;
            long nm2 = 0;

            long total = 0;

            while (true)
            {
                long n = nm1 + nm2;

                if (n >= 4000000)
                {
                    break;
                }

                if (n % 2 == 0)
                {
                    total += n;
                }

                nm2 = nm1;
                nm1 = n;
            }

            Console.WriteLine("sum = " + total);
        }

        /// <summary>
        /// The prime factors of 13195 are 5, 7, 13 and 29.
        /// 
        /// What is the largest prime factor of the number 600851475143 ?
        /// </summary>
        private static void Problem_03()
        {
            long number = 600851475143;
            long? greatest = null;

            for (long i = 2; i <= number / 2; i++)
            {
                var isPrime = true;
                for (long j = 2; j <= i / 2; j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (!isPrime)
                {
                    continue;
                }

                if (number % i == 0)
                {
                    greatest = i;

                    do
                    {
                        number /= i;
                    }
                    while (number % i == 0);
                }
            }

            Console.WriteLine("number = " + (greatest.HasValue ? Math.Max(number, greatest.Value) : number));
        }

        /// <summary>
        /// A palindromic number reads the same both ways. The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 × 99.
        /// 
        /// Find the largest palindrome made from the product of two 3-digit numbers.
        /// </summary>
        private static void Problem_04()
        {
            var largest = 0;

            for (var m = 100; m < 1000; m++)
            {
                for (var n = m; n < 1000; n++)
                {
                    var product = m * n;

                    var dec = product.ToString();

                    var isPallindrome = true;
                    for (int i = 0; i < dec.Length; i++)
                    {
                        if (dec[i] != dec[dec.Length - 1 - i])
                        {
                            isPallindrome = false;
                            break;
                        }
                    }

                    if (!isPallindrome)
                    {
                        continue;
                    }

                    largest = Math.Max(largest, product);
                }
            }

            Console.WriteLine("largest = " + largest);
        }

        /// <summary>
        /// 2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.
        /// 
        /// What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?
        /// </summary>
        private static void Problem_05()
        {
            var lessThan = 20;

            var primes = new List<long>();
            for (long i = 2; i < lessThan; i++)
            {
                primes.Add(i);
            }

            for (int i = 0; i < primes.Count; i++)
            {
                var prime = primes[i];
                primes.RemoveAll(k => (k > prime) && (k % prime == 0));
            }

            long result = 1;

            foreach (var prime in primes)
            {
                var max = prime;

                while (max * prime <= lessThan)
                {
                    max *= prime;
                }

                result *= max;
            }

            Console.WriteLine("product = " + result);
        }

        /// <summary>
        /// The sum of the squares of the first ten natural numbers is,
        /// 1^(2) + 2^(2) + ... + 10^(2) = 385
        /// 
        /// The square of the sum of the first ten natural numbers is,
        /// (1 + 2 + ... + 10)^(2) = 55^(2) = 3025
        ///
        /// Hence the difference between the sum of the squares of the first ten natural numbers and the square of the sum is 3025 − 385 = 2640.
        ///
        /// Find the difference between the sum of the squares of the first one hundred natural numbers and the square of the sum.
        /// </summary>
        private static void Problem_06()
        {
            var sumSquares = 0d;
            var sum = 0d;
            foreach (var n in Enumerable.Range(1, 100))
            {
                sumSquares += n * n;
                sum += n;
            }

            var diff = (sum * sum) - sumSquares;

            Console.WriteLine("difference = " + diff);
        }

        /// <summary>
        /// By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.
        /// 
        /// What is the 10001st prime number?
        /// </summary>
        private static void Problem_07()
        {
            var n = 10001;
            var primes = GetFirstNPrimes(n);
            Console.WriteLine(n + "th = " + primes.Primes[n - 1]);
        }

        /// <summary>
        /// Find the greatest product of five consecutive digits in the 1000-digit number.
        ///
        /// 73167176531330624919225119674426574742355349194934
        /// 96983520312774506326239578318016984801869478851843
        /// 85861560789112949495459501737958331952853208805511
        /// 12540698747158523863050715693290963295227443043557
        /// 66896648950445244523161731856403098711121722383113
        /// 62229893423380308135336276614282806444486645238749
        /// 30358907296290491560440772390713810515859307960866
        /// 70172427121883998797908792274921901699720888093776
        /// 65727333001053367881220235421809751254540594752243
        /// 52584907711670556013604839586446706324415722155397
        /// 53697817977846174064955149290862569321978468622482
        /// 83972241375657056057490261407972968652414535100474
        /// 82166370484403199890008895243450658541227588666881
        /// 16427171479924442928230863465674813919123162824586
        /// 17866458359124566529476545682848912883142607690042
        /// 24219022671055626321111109370544217506941658960408
        /// 07198403850962455444362981230987879927244284909188
        /// 84580156166097919133875499200524063689912560717606
        /// 05886116467109405077541002256983155200055935729725
        /// 71636269561882670428252483600823257530420752963450
        /// </summary>
        private static void Problem_08()
        {
            var number =
              @"73167176531330624919225119674426574742355349194934
                96983520312774506326239578318016984801869478851843
                85861560789112949495459501737958331952853208805511
                12540698747158523863050715693290963295227443043557
                66896648950445244523161731856403098711121722383113
                62229893423380308135336276614282806444486645238749
                30358907296290491560440772390713810515859307960866
                70172427121883998797908792274921901699720888093776
                65727333001053367881220235421809751254540594752243
                52584907711670556013604839586446706324415722155397
                53697817977846174064955149290862569321978468622482
                83972241375657056057490261407972968652414535100474
                82166370484403199890008895243450658541227588666881
                16427171479924442928230863465674813919123162824586
                17866458359124566529476545682848912883142607690042
                24219022671055626321111109370544217506941658960408
                07198403850962455444362981230987879927244284909188
                84580156166097919133875499200524063689912560717606
                05886116467109405077541002256983155200055935729725
                71636269561882670428252483600823257530420752963450";

            number = Regex.Replace(number, @"\D", string.Empty);

            var numbers = number.Split("0".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var max = 0;

            var offset = (int)'0';

            foreach (var num in numbers)
            {
                for (int i = 0; i < num.Length - 4; i++)
                {
                    var product = (num[i] - offset) * (num[i + 1] - offset) * (num[i + 2] - offset) * (num[i + 3] - offset) * (num[i + 4] - offset);
                    max = Math.Max(max, product);
                }
            }

            Console.WriteLine("max = " + max);
        }

        /// <summary>
        /// A Pythagorean triplet is a set of three natural numbers, a  &lt; b  &lt; c, for which,
        /// a^(2) + b^(2) = c^(2)
        ///
        /// For example, 3^(2) + 4^(2) = 9 + 16 = 25 = 5^(2).
        ///
        /// There exists exactly one Pythagorean triplet for which a + b + c = 1000.
        /// Find the product abc.
        ///
        /// </summary>
        private static void Problem_09()
        {
            for (var a = 1; a <= 1000; a++)
            {
                for (var b = a + 1; b <= 1000; b++)
                {
                    var c = 1000 - a - b;

                    if (a * a + b * b == c * c)
                    {
                        Console.WriteLine("product = " + (a * b * c));
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.
        /// 
        /// Find the sum of all the primes below two million.
        /// </summary>
        private static void Problem_10()
        {
            var primes = GetPrimesBelow(2000000);
            var sum = primes.Primes.Sum();
            Console.WriteLine("sum = " + sum);
        }

        /// <summary>
        /// In the 20×20 grid below, four numbers along a diagonal line have been marked in red.
        /// 
        /// 08 02 22 97 38 15 00 40 00 75 04 05 07 78 52 12 50 77 91 08
        /// 49 49 99 40 17 81 18 57 60 87 17 40 98 43 69 48 04 56 62 00
        /// 81 49 31 73 55 79 14 29 93 71 40 67 53 88 30 03 49 13 36 65
        /// 52 70 95 23 04 60 11 42 69 24 68 56 01 32 56 71 37 02 36 91
        /// 22 31 16 71 51 67 63 89 41 92 36 54 22 40 40 28 66 33 13 80
        /// 24 47 32 60 99 03 45 02 44 75 33 53 78 36 84 20 35 17 12 50
        /// 32 98 81 28 64 23 67 10 26 38 40 67 59 54 70 66 18 38 64 70
        /// 67 26 20 68 02 62 12 20 95 63 94 39 63 08 40 91 66 49 94 21
        /// 24 55 58 05 66 73 99 26 97 17 78 78 96 83 14 88 34 89 63 72
        /// 21 36 23 09 75 00 76 44 20 45 35 14 00 61 33 97 34 31 33 95
        /// 78 17 53 28 22 75 31 67 15 94 03 80 04 62 16 14 09 53 56 92
        /// 16 39 05 42 96 35 31 47 55 58 88 24 00 17 54 24 36 29 85 57
        /// 86 56 00 48 35 71 89 07 05 44 44 37 44 60 21 58 51 54 17 58
        /// 19 80 81 68 05 94 47 69 28 73 92 13 86 52 17 77 04 89 55 40
        /// 04 52 08 83 97 35 99 16 07 97 57 32 16 26 26 79 33 27 98 66
        /// 88 36 68 87 57 62 20 72 03 46 33 67 46 55 12 32 63 93 53 69
        /// 04 42 16 73 38 25 39 11 24 94 72 18 08 46 29 32 40 62 76 36
        /// 20 69 36 41 72 30 23 88 34 62 99 69 82 67 59 85 74 04 36 16
        /// 20 73 35 29 78 31 90 01 74 31 49 71 48 86 81 16 23 57 05 54
        /// 01 70 54 71 83 51 54 69 16 92 33 48 61 43 52 01 89 19 67 48
        /// 
        /// The product of these numbers is 26 × 63 × 78 × 14 = 1788696.
        /// 
        /// What is the greatest product of four adjacent numbers in any direction (up, down, left, right, or diagonally) in the 20×20 grid?
        /// </summary>
        private static void Problem_11()
        {
            var width = 20;
            var height = 20;

            var grid = new[] {
	            new[] { 08, 02, 22, 97, 38, 15, 00, 40, 00, 75, 04, 05, 07, 78, 52, 12, 50, 77, 91, 08},
	            new[] { 49, 49, 99, 40, 17, 81, 18, 57, 60, 87, 17, 40, 98, 43, 69, 48, 04, 56, 62, 00},
	            new[] { 81, 49, 31, 73, 55, 79, 14, 29, 93, 71, 40, 67, 53, 88, 30, 03, 49, 13, 36, 65},
	            new[] { 52, 70, 95, 23, 04, 60, 11, 42, 69, 24, 68, 56, 01, 32, 56, 71, 37, 02, 36, 91},
	            new[] { 22, 31, 16, 71, 51, 67, 63, 89, 41, 92, 36, 54, 22, 40, 40, 28, 66, 33, 13, 80},
	            new[] { 24, 47, 32, 60, 99, 03, 45, 02, 44, 75, 33, 53, 78, 36, 84, 20, 35, 17, 12, 50},
	            new[] { 32, 98, 81, 28, 64, 23, 67, 10, 26, 38, 40, 67, 59, 54, 70, 66, 18, 38, 64, 70},
	            new[] { 67, 26, 20, 68, 02, 62, 12, 20, 95, 63, 94, 39, 63, 08, 40, 91, 66, 49, 94, 21},
	            new[] { 24, 55, 58, 05, 66, 73, 99, 26, 97, 17, 78, 78, 96, 83, 14, 88, 34, 89, 63, 72},
	            new[] { 21, 36, 23, 09, 75, 00, 76, 44, 20, 45, 35, 14, 00, 61, 33, 97, 34, 31, 33, 95},
	            new[] { 78, 17, 53, 28, 22, 75, 31, 67, 15, 94, 03, 80, 04, 62, 16, 14, 09, 53, 56, 92},
	            new[] { 16, 39, 05, 42, 96, 35, 31, 47, 55, 58, 88, 24, 00, 17, 54, 24, 36, 29, 85, 57},
	            new[] { 86, 56, 00, 48, 35, 71, 89, 07, 05, 44, 44, 37, 44, 60, 21, 58, 51, 54, 17, 58},
	            new[] { 19, 80, 81, 68, 05, 94, 47, 69, 28, 73, 92, 13, 86, 52, 17, 77, 04, 89, 55, 40},
	            new[] { 04, 52, 08, 83, 97, 35, 99, 16, 07, 97, 57, 32, 16, 26, 26, 79, 33, 27, 98, 66},
	            new[] { 88, 36, 68, 87, 57, 62, 20, 72, 03, 46, 33, 67, 46, 55, 12, 32, 63, 93, 53, 69},
	            new[] { 04, 42, 16, 73, 38, 25, 39, 11, 24, 94, 72, 18, 08, 46, 29, 32, 40, 62, 76, 36},
	            new[] { 20, 69, 36, 41, 72, 30, 23, 88, 34, 62, 99, 69, 82, 67, 59, 85, 74, 04, 36, 16},
	            new[] { 20, 73, 35, 29, 78, 31, 90, 01, 74, 31, 49, 71, 48, 86, 81, 16, 23, 57, 05, 54},
	            new[] { 01, 70, 54, 71, 83, 51, 54, 69, 16, 92, 33, 48, 61, 43, 52, 01, 89, 19, 67, 48}
            };

            var length = 4;

            long max = 0;

            for (int x = 0; x <= width - length; x++)
            {
                for (int y = 0; y <= height - 1; y++)
                {
                    long product = 1;

                    for (int i = 0; i < length; i++)
                    {
                        product *= grid[y][x + i];
                    }

                    max = Math.Max(max, product);
                }
            }

            for (int x = 0; x <= width - 1; x++)
            {
                for (int y = 0; y <= height - length; y++)
                {
                    long product = 1;

                    for (int i = 0; i < length; i++)
                    {
                        product *= grid[y + i][x];
                    }

                    max = Math.Max(max, product);
                }
            }

            for (int x = 0; x <= width - length; x++)
            {
                for (int y = 0; y <= height - length; y++)
                {
                    long product = 1;

                    for (int i = 0; i < length; i++)
                    {
                        product *= grid[y + i][x + i];
                    }

                    max = Math.Max(max, product);
                }
            }

            for (int x = length - 1; x <= width - 1; x++)
            {
                for (int y = 0; y <= height - length; y++)
                {
                    long product = 1;

                    for (int i = 0; i < length; i++)
                    {
                        product *= grid[y + i][x - i];
                    }

                    max = Math.Max(max, product);
                }
            }

            Console.WriteLine("max product = " + max);
        }

        /// <summary>
        /// The sequence of triangle numbers is generated by adding the natural numbers. So the 7^(th) triangle number would be 1 + 2 + 3 + 4 + 5 + 6 + 7 = 28. The first ten terms would be:
        /// 
        /// 1, 3, 6, 10, 15, 21, 28, 36, 45, 55, ...
        /// 
        /// Let us list the factors of the first seven triangle numbers:
        /// 
        ///      1: 1
        ///      3: 1,3
        ///      6: 1,2,3,6
        ///     10: 1,2,5,10
        ///     15: 1,3,5,15
        ///     21: 1,3,7,21
        ///     28: 1,2,4,7,14,28
        /// 
        /// We can see that 28 is the first triangle number to have over five divisors.
        /// 
        /// What is the value of the first triangle number to have over five hundred divisors?
        /// </summary>
        private static void Problem_12()
        {
            var primes = GetFirstNPrimes(1000);

            var current = 0;

            for (var i = 1; ; i++)
            {
                current += i;

                var factors = Factor(current, primes);

                var combinations = 1;
                foreach (var factorCount in factors.Values)
                {
                    combinations *= factorCount + 1;
                }

                if (combinations >= 500)
                {
                    Console.WriteLine("first = " + current);
                    return;
                }
            }
        }

        /// <summary>
        /// Work out the first ten digits of the sum of the following one-hundred 50-digit numbers.
        /// 
        /// 37107287533902102798797998220837590246510135740250
        /// 46376937677490009712648124896970078050417018260538
        /// 74324986199524741059474233309513058123726617309629
        /// 91942213363574161572522430563301811072406154908250
        /// 23067588207539346171171980310421047513778063246676
        /// 89261670696623633820136378418383684178734361726757
        /// 28112879812849979408065481931592621691275889832738
        /// 44274228917432520321923589422876796487670272189318
        /// 47451445736001306439091167216856844588711603153276
        /// 70386486105843025439939619828917593665686757934951
        /// 62176457141856560629502157223196586755079324193331
        /// 64906352462741904929101432445813822663347944758178
        /// 92575867718337217661963751590579239728245598838407
        /// 58203565325359399008402633568948830189458628227828
        /// 80181199384826282014278194139940567587151170094390
        /// 35398664372827112653829987240784473053190104293586
        /// 86515506006295864861532075273371959191420517255829
        /// 71693888707715466499115593487603532921714970056938
        /// 54370070576826684624621495650076471787294438377604
        /// 53282654108756828443191190634694037855217779295145
        /// 36123272525000296071075082563815656710885258350721
        /// 45876576172410976447339110607218265236877223636045
        /// 17423706905851860660448207621209813287860733969412
        /// 81142660418086830619328460811191061556940512689692
        /// 51934325451728388641918047049293215058642563049483
        /// 62467221648435076201727918039944693004732956340691
        /// 15732444386908125794514089057706229429197107928209
        /// 55037687525678773091862540744969844508330393682126
        /// 18336384825330154686196124348767681297534375946515
        /// 80386287592878490201521685554828717201219257766954
        /// 78182833757993103614740356856449095527097864797581
        /// 16726320100436897842553539920931837441497806860984
        /// 48403098129077791799088218795327364475675590848030
        /// 87086987551392711854517078544161852424320693150332
        /// 59959406895756536782107074926966537676326235447210
        /// 69793950679652694742597709739166693763042633987085
        /// 41052684708299085211399427365734116182760315001271
        /// 65378607361501080857009149939512557028198746004375
        /// 35829035317434717326932123578154982629742552737307
        /// 94953759765105305946966067683156574377167401875275
        /// 88902802571733229619176668713819931811048770190271
        /// 25267680276078003013678680992525463401061632866526
        /// 36270218540497705585629946580636237993140746255962
        /// 24074486908231174977792365466257246923322810917141
        /// 91430288197103288597806669760892938638285025333403
        /// 34413065578016127815921815005561868836468420090470
        /// 23053081172816430487623791969842487255036638784583
        /// 11487696932154902810424020138335124462181441773470
        /// 63783299490636259666498587618221225225512486764533
        /// 67720186971698544312419572409913959008952310058822
        /// 95548255300263520781532296796249481641953868218774
        /// 76085327132285723110424803456124867697064507995236
        /// 37774242535411291684276865538926205024910326572967
        /// 23701913275725675285653248258265463092207058596522
        /// 29798860272258331913126375147341994889534765745501
        /// 18495701454879288984856827726077713721403798879715
        /// 38298203783031473527721580348144513491373226651381
        /// 34829543829199918180278916522431027392251122869539
        /// 40957953066405232632538044100059654939159879593635
        /// 29746152185502371307642255121183693803580388584903
        /// 41698116222072977186158236678424689157993532961922
        /// 62467957194401269043877107275048102390895523597457
        /// 23189706772547915061505504953922979530901129967519
        /// 86188088225875314529584099251203829009407770775672
        /// 11306739708304724483816533873502340845647058077308
        /// 82959174767140363198008187129011875491310547126581
        /// 97623331044818386269515456334926366572897563400500
        /// 42846280183517070527831839425882145521227251250327
        /// 55121603546981200581762165212827652751691296897789
        /// 32238195734329339946437501907836945765883352399886
        /// 75506164965184775180738168837861091527357929701337
        /// 62177842752192623401942399639168044983993173312731
        /// 32924185707147349566916674687634660915035914677504
        /// 99518671430235219628894890102423325116913619626622
        /// 73267460800591547471830798392868535206946944540724
        /// 76841822524674417161514036427982273348055556214818
        /// 97142617910342598647204516893989422179826088076852
        /// 87783646182799346313767754307809363333018982642090
        /// 10848802521674670883215120185883543223812876952786
        /// 71329612474782464538636993009049310363619763878039
        /// 62184073572399794223406235393808339651327408011116
        /// 66627891981488087797941876876144230030984490851411
        /// 60661826293682836764744779239180335110989069790714
        /// 85786944089552990653640447425576083659976645795096
        /// 66024396409905389607120198219976047599490197230297
        /// 64913982680032973156037120041377903785566085089252
        /// 16730939319872750275468906903707539413042652315011
        /// 94809377245048795150954100921645863754710598436791
        /// 78639167021187492431995700641917969777599028300699
        /// 15368713711936614952811305876380278410754449733078
        /// 40789923115535562561142322423255033685442488917353
        /// 44889911501440648020369068063960672322193204149535
        /// 41503128880339536053299340368006977710650566631954
        /// 81234880673210146739058568557934581403627822703280
        /// 82616570773948327592232845941706525094512325230608
        /// 22918802058777319719839450180888072429661980811197
        /// 77158542502016545090413245809786882778948721859617
        /// 72107838435069186155435662884062257473692284509516
        /// 20849603980134001723930671666823555245252804609722
        /// 53503534226472524250874054075591789781264330331690
        /// </summary>
        private static void Problem_13()
        {
            var text =
              @"37107287533902102798797998220837590246510135740250
                46376937677490009712648124896970078050417018260538
                74324986199524741059474233309513058123726617309629
                91942213363574161572522430563301811072406154908250
                23067588207539346171171980310421047513778063246676
                89261670696623633820136378418383684178734361726757
                28112879812849979408065481931592621691275889832738
                44274228917432520321923589422876796487670272189318
                47451445736001306439091167216856844588711603153276
                70386486105843025439939619828917593665686757934951
                62176457141856560629502157223196586755079324193331
                64906352462741904929101432445813822663347944758178
                92575867718337217661963751590579239728245598838407
                58203565325359399008402633568948830189458628227828
                80181199384826282014278194139940567587151170094390
                35398664372827112653829987240784473053190104293586
                86515506006295864861532075273371959191420517255829
                71693888707715466499115593487603532921714970056938
                54370070576826684624621495650076471787294438377604
                53282654108756828443191190634694037855217779295145
                36123272525000296071075082563815656710885258350721
                45876576172410976447339110607218265236877223636045
                17423706905851860660448207621209813287860733969412
                81142660418086830619328460811191061556940512689692
                51934325451728388641918047049293215058642563049483
                62467221648435076201727918039944693004732956340691
                15732444386908125794514089057706229429197107928209
                55037687525678773091862540744969844508330393682126
                18336384825330154686196124348767681297534375946515
                80386287592878490201521685554828717201219257766954
                78182833757993103614740356856449095527097864797581
                16726320100436897842553539920931837441497806860984
                48403098129077791799088218795327364475675590848030
                87086987551392711854517078544161852424320693150332
                59959406895756536782107074926966537676326235447210
                69793950679652694742597709739166693763042633987085
                41052684708299085211399427365734116182760315001271
                65378607361501080857009149939512557028198746004375
                35829035317434717326932123578154982629742552737307
                94953759765105305946966067683156574377167401875275
                88902802571733229619176668713819931811048770190271
                25267680276078003013678680992525463401061632866526
                36270218540497705585629946580636237993140746255962
                24074486908231174977792365466257246923322810917141
                91430288197103288597806669760892938638285025333403
                34413065578016127815921815005561868836468420090470
                23053081172816430487623791969842487255036638784583
                11487696932154902810424020138335124462181441773470
                63783299490636259666498587618221225225512486764533
                67720186971698544312419572409913959008952310058822
                95548255300263520781532296796249481641953868218774
                76085327132285723110424803456124867697064507995236
                37774242535411291684276865538926205024910326572967
                23701913275725675285653248258265463092207058596522
                29798860272258331913126375147341994889534765745501
                18495701454879288984856827726077713721403798879715
                38298203783031473527721580348144513491373226651381
                34829543829199918180278916522431027392251122869539
                40957953066405232632538044100059654939159879593635
                29746152185502371307642255121183693803580388584903
                41698116222072977186158236678424689157993532961922
                62467957194401269043877107275048102390895523597457
                23189706772547915061505504953922979530901129967519
                86188088225875314529584099251203829009407770775672
                11306739708304724483816533873502340845647058077308
                82959174767140363198008187129011875491310547126581
                97623331044818386269515456334926366572897563400500
                42846280183517070527831839425882145521227251250327
                55121603546981200581762165212827652751691296897789
                32238195734329339946437501907836945765883352399886
                75506164965184775180738168837861091527357929701337
                62177842752192623401942399639168044983993173312731
                32924185707147349566916674687634660915035914677504
                99518671430235219628894890102423325116913619626622
                73267460800591547471830798392868535206946944540724
                76841822524674417161514036427982273348055556214818
                97142617910342598647204516893989422179826088076852
                87783646182799346313767754307809363333018982642090
                10848802521674670883215120185883543223812876952786
                71329612474782464538636993009049310363619763878039
                62184073572399794223406235393808339651327408011116
                66627891981488087797941876876144230030984490851411
                60661826293682836764744779239180335110989069790714
                85786944089552990653640447425576083659976645795096
                66024396409905389607120198219976047599490197230297
                64913982680032973156037120041377903785566085089252
                16730939319872750275468906903707539413042652315011
                94809377245048795150954100921645863754710598436791
                78639167021187492431995700641917969777599028300699
                15368713711936614952811305876380278410754449733078
                40789923115535562561142322423255033685442488917353
                44889911501440648020369068063960672322193204149535
                41503128880339536053299340368006977710650566631954
                81234880673210146739058568557934581403627822703280
                82616570773948327592232845941706525094512325230608
                22918802058777319719839450180888072429661980811197
                77158542502016545090413245809786882778948721859617
                72107838435069186155435662884062257473692284509516
                20849603980134001723930671666823555245252804609722
                53503534226472524250874054075591789781264330331690";

            var numbers = Regex.Replace(text, @"[^\S\r\n]", string.Empty).Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            BigInteger value = 0;

            foreach (var num in numbers)
            {
                value += BigInteger.Parse(num);
            }

            Console.WriteLine("digits = " + value.ToString().Substring(0, 10));
        }

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
        /// 
        /// </summary>
        private static void Problem_14()
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

            Console.WriteLine("num = " + maxNum);
        }

        /// <summary>
        /// Starting in the top left corner of a 2×2 grid, there are 6 routes (without backtracking) to the bottom right corner.
        /// 
        /// How many routes are there through a 20×20 grid?
        /// </summary>
        private static void Problem_15()
        {
            var paths = new Dictionary<Size, long>();

            Func<Size, long> lookup = null;
            lookup = (Size size) =>
            {
                if (size.Width > size.Height)
                {
                    return lookup(new Size(size.Height, size.Width));
                }

                if (size.Width == 0)
                {
                    return 1;
                }

                if (!paths.ContainsKey(size))
                {
                    paths[size] = lookup(new Size(size.Width - 1, size.Height)) + lookup(new Size(size.Width, size.Height - 1));
                }

                return paths[size];
            };

            var count = lookup(new Size(20, 20));

            Console.WriteLine("count = " + count);
        }

        /// <summary>
        /// 2^(15) = 32768 and the sum of its digits is 3 + 2 + 7 + 6 + 8 = 26.
        /// 
        /// What is the sum of the digits of the number 2^(1000)?
        /// </summary>
        private static void Problem_16()
        {
            BigInteger value = 1;

            for (var i = 0; i < 1000; i++)
            {
                value = value * 2;
            }

            var offset = (int)'0';
            var total = 0;
            var num = value.ToString();

            for (int i = 0; i < num.Length; i++)
            {
                total += num[i] - offset;
            }

            Console.WriteLine("sum = " + total);
        }

        /// <summary>
        /// If the numbers 1 to 5 are written out in words: one, two, three, four, five, then there are 3 + 3 + 5 + 4 + 4 = 19 letters used in total.
        /// 
        /// If all the numbers from 1 to 1000 (one thousand) inclusive were written out in words, how many letters would be used? 
        /// </summary>
        private static void Problem_17()
        {
            var ones = new Dictionary<int, string>
            {
                { 1, "one" },
                { 2, "two" },
                { 3, "three" },
                { 4, "four" },
                { 5, "five" },
                { 6, "six" },
                { 7, "seven" },
                { 8, "eight" },
                { 9, "nine" },
            };

            var teens = new Dictionary<int, string>
            {
                { 0, "ten" },
                { 1, "eleven" },
                { 2, "twelve" },
                { 3, "thirteen" },
                { 4, "fourteen" },
                { 5, "fifteen" },
                { 6, "sixteen" },
                { 7, "seventeen" },
                { 8, "eighteen" },
                { 9, "nineteen" },
            };

            var tens = new Dictionary<int, string>
            {
                { 2, "twenty" },
                { 3, "thirty" },
                { 4, "forty" },
                { 5, "fifty" },
                { 6, "sixty" },
                { 7, "seventy" },
                { 8, "eighty" },
                { 9, "ninety" },
            };

            var hundred = "hundred";

            var thousand = "thousand";

            Func<int, string> getName = (int num) =>
            {
                StringBuilder result = new StringBuilder();

                if (num >= 1000)
                {
                    var thousandsDigit = num / 1000;

                    result.Append(ones[thousandsDigit]);
                    result.Append(thousand);
                    num %= 1000;

                    if (num == 0)
                    {
                        return result.ToString();
                    }
                }

                if (num >= 100)
                {
                    var hundredsDigit = num / 100;

                    result.Append(ones[hundredsDigit]);
                    result.Append(hundred);
                    num %= 100;

                    if (num == 0)
                    {
                        return result.ToString();
                    }
                    else
                    {
                        result.Append("and");
                    }
                }

                var tensDigit = num / 10;
                var onesDigit = num % 10;

                if (tensDigit == 0)
                {
                    result.Append(ones[onesDigit]);
                }
                else if (tensDigit == 1)
                {
                    result.Append(teens[onesDigit]);
                }
                else
                {
                    result.Append(tens[tensDigit]);

                    if (onesDigit != 0)
                    {
                        result.Append(ones[onesDigit]);
                    }
                }

                return result.ToString();
            };

            var sum = 0;
            for (int i = 1; i <= 1000; i++)
            {
                sum += getName(i).Length;
            }

            Console.WriteLine("letters = " + sum);
        }

        /// <summary>
        /// By starting at the top of the triangle below and moving to adjacent numbers on the row below, the maximum total from top to bottom is 23.
        /// 
        /// 3
        /// 7 4
        /// 2 4 6
        /// 8 5 9 3
        /// 
        /// That is, 3 + 7 + 4 + 9 = 23.
        /// 
        /// Find the maximum total from top to bottom of the triangle below:
        /// 
        /// 75
        /// 95 64
        /// 17 47 82
        /// 18 35 87 10
        /// 20 04 82 47 65
        /// 19 01 23 75 03 34
        /// 88 02 77 73 07 63 67
        /// 99 65 04 28 06 16 70 92
        /// 41 41 26 56 83 40 80 70 33
        /// 41 48 72 33 47 32 37 16 94 29
        /// 53 71 44 65 25 43 91 52 97 51 14
        /// 70 11 33 28 77 73 17 78 39 68 17 57
        /// 91 71 52 38 17 14 91 43 58 50 27 29 48
        /// 63 66 04 68 89 53 67 30 73 16 69 87 40 31
        /// 04 62 98 27 23 09 70 98 73 93 38 53 60 04 23
        /// </summary>
        private static void Problem_18()
        {
            var text =
               @"75
                 95 64
                 17 47 82
                 18 35 87 10
                 20 04 82 47 65
                 19 01 23 75 03 34
                 88 02 77 73 07 63 67
                 99 65 04 28 06 16 70 92
                 41 41 26 56 83 40 80 70 33
                 41 48 72 33 47 32 37 16 94 29
                 53 71 44 65 25 43 91 52 97 51 14
                 70 11 33 28 77 73 17 78 39 68 17 57
                 91 71 52 38 17 14 91 43 58 50 27 29 48
                 63 66 04 68 89 53 67 30 73 16 69 87 40 31
                 04 62 98 27 23 09 70 98 73 93 38 53 60 04 23";

            var data = (from line in text.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                        select
                            (from entry in line.Split(" \t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                             select int.Parse(entry)).ToArray()).ToArray();

            var values = new Dictionary<Point, long>();

            Func<Point, long> lookup = null;
            lookup = (Point point) =>
            {
                if (point.X >= data.Length)
                {
                    return 0;
                }

                if (!values.ContainsKey(point))
                {
                    var row = point.X;
                    var col = point.Y;

                    values[point] = data[row][col] + Math.Max(lookup(new Point(row + 1, col)), lookup(new Point(row + 1, col + 1)));
                }

                return values[point];
            };

            var max = lookup(new Point(0, 0));

            Console.WriteLine("max = " + max);
        }

        /// <summary>
        /// You are given the following information, but you may prefer to do some research for yourself.
        /// 
        ///     * 1 Jan 1900 was a Monday.
        ///     * Thirty days has September,
        ///       April, June and November.
        ///       All the rest have thirty-one,
        ///       Saving February alone,
        ///       Which has twenty-eight, rain or shine.
        ///       And on leap years, twenty-nine.
        ///     * A leap year occurs on any year evenly divisible by 4, but not on a century unless it is divisible by 400.
        /// 
        /// How many Sundays fell on the first of the month during the twentieth century (1 Jan 1901 to 31 Dec 2000)?
        /// </summary>
        private static void Problem_19()
        {
            int total = 0;
            for (var day = new DateTime(1901, 1, 1); day.Year <= 2000; day = day.AddMonths(1))
            {
                if (day.DayOfWeek == DayOfWeek.Sunday)
                {
                    total++;
                }
            }

            Console.WriteLine("total = " + total);
        }

        /// <summary>
        /// n! means n × (n  − 1) × ... × 3 × 2 × 1
        /// 
        /// Find the sum of the digits in the number 100!
        /// </summary>
        private static void Problem_20()
        {
            BigInteger value = 1;

            for (var i = 1; i <= 100; i++)
            {
                value = value * i;
            }

            var offset = (int)'0';
            var total = 0;
            var num = value.ToString();

            for (int i = 0; i < num.Length; i++)
            {
                total += num[i] - offset;
            }

            Console.WriteLine("sum = " + total);
        }

        /// <summary>
        /// Let d(n) be defined as the sum of proper divisors of n (numbers less than n which divide evenly into n).
        /// If d(a) = b and d(b) = a, where a ≠ b, then a and b are an amicable pair and each of a and b are called amicable numbers.
        /// 
        /// For example, the proper divisors of 220 are 1, 2, 4, 5, 10, 11, 20, 22, 44, 55 and 110; therefore d(220) = 284. The proper divisors of 284 are 1, 2, 4, 71 and 142; so d(284) = 220.
        /// 
        /// Evaluate the sum of all the amicable numbers under 10000.
        /// </summary>
        private static void Problem_21()
        {
            var primes = GetFirstNPrimes(1000);
            var sums = new Dictionary<long, long>();

            for (int i = 2; i < 10000; i++)
            {
                var allFactors = GetAllFactors(Factor(i, primes));
                allFactors.Remove(i);
                sums[i] = allFactors.Sum();
            }

            var amicable = from a in sums
                           where sums.ContainsKey(a.Value)
                           where sums[a.Value] == a.Key
                           where a.Key != a.Value
                           select a.Key;

            var sum = amicable.Sum();

            Console.WriteLine("sum = " + sum);
        }

        /// <summary>
        /// Using names.txt  (right click and 'Save Link/Target As...'), a 46K text file containing over five-thousand first names, begin by sorting it into alphabetical order. Then working out the alphabetical value for each name, multiply this value by its alphabetical position in the list to obtain a name score.
        /// 
        /// For example, when the list is sorted into alphabetical order, COLIN, which is worth 3 + 15 + 12 + 9 + 14 = 53, is the 938th name in the list. So, COLIN would obtain a score of 938 × 53 = 49714.
        /// 
        /// What is the total of all the name scores in the file?
        /// </summary>
        private static void Problem_22()
        {
            var text = File.ReadAllText("names.txt");

            var names = from n in text.Split(',')
                        let name = n.Substring(1, n.Length - 2)
                        orderby name
                        select name;

            long total = 0;

            var offset = 'A' - 1;
            int i = 1;
            foreach (var name in names)
            {
                var sum = 0;
                foreach (var ch in name)
                {
                    sum += ch - offset;
                }

                total += i * sum;
                i++;
            }

            Console.WriteLine("total = " + total);
        }

        /// <summary>
        /// A perfect number is a number for which the sum of its proper divisors is exactly equal to the number. For example, the sum of the proper divisors of 28 would be 1 + 2 + 4 + 7 + 14 = 28, which means that 28 is a perfect number.
        /// 
        /// A number n is called deficient if the sum of its proper divisors is less than n and it is called abundant if this sum exceeds n.
        /// 
        /// As 12 is the smallest abundant number, 1 + 2 + 3 + 4 + 6 = 16, the smallest number that can be written as the sum of two abundant numbers is 24. By mathematical analysis, it can be shown that all integers greater than 28123 can be written as the sum of two abundant numbers. However, this upper limit cannot be reduced any further by analysis even though it is known that the greatest number that cannot be expressed as the sum of two abundant numbers is less than this limit.
        /// 
        /// Find the sum of all the positive integers which cannot be written as the sum of two abundant numbers.
        /// </summary>
        private static void Problem_23()
        {
            long upperLimit = 28123;

            var primes = GetPrimesBelow(28123);

            var abundantNumbers = new List<int>();

            for (long i = 1; i <= upperLimit; i++)
            {
                var factors = GetAllFactors(Factor(i, primes));
                factors.Remove(i);

                if (factors.Sum() > i)
                {
                    abundantNumbers.Add((int)i);
                }
            }

            var allNumbers = new int[upperLimit];
            for (int i = 1; i <= upperLimit; i++)
            {
                allNumbers[i - 1] = i;
            }

            for (int i = 0; i < abundantNumbers.Count; i++)
            {
                int numI = abundantNumbers[i];

                if (numI > upperLimit)
                {
                    break;
                }

                for (int j = 0; j < abundantNumbers.Count; j++)
                {
                    int numJ = numI + abundantNumbers[j];

                    if (numJ > upperLimit)
                    {
                        break;
                    }

                    allNumbers[numJ - 1] = 0;
                }
            }

            var sum = allNumbers.Sum();

            Console.WriteLine("sum = " + sum);
        }

        /// <summary>
        /// A permutation is an ordered arrangement of objects. For example, 3124 is one possible permutation of the digits 1, 2, 3 and 4. If all of the permutations are listed numerically or alphabetically, we call it lexicographic order. The lexicographic permutations of 0, 1 and 2 are:
        /// 
        /// 012   021   102   120   201   210
        /// 
        /// What is the millionth lexicographic permutation of the digits 0, 1, 2, 3, 4, 5, 6, 7, 8 and 9?
        /// </summary>
        private static void Problem_24()
        {
            var ch = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            var permutations = new Permutations<char>(ch);
            IList<char> permutation = permutations.Skip(999999).First();
            var value = new string(permutation.ToArray());
            Console.WriteLine("value = " + value);
        }

        /// <summary>
        /// The Fibonacci sequence is defined by the recurrence relation:
        /// 
        ///     F_(n) = F_(n−1) + F_(n−2), where F_(1) = 1 and F_(2) = 1.
        /// 
        /// Hence the first 12 terms will be:
        /// 
        ///     F_(1) = 1
        ///     F_(2) = 1
        ///     F_(3) = 2
        ///     F_(4) = 3
        ///     F_(5) = 5
        ///     F_(6) = 8
        ///     F_(7) = 13
        ///     F_(8) = 21
        ///     F_(9) = 34
        ///     F_(10) = 55
        ///     F_(11) = 89
        ///     F_(12) = 144
        /// 
        /// The 12th term, F_(12), is the first term to contain three digits.
        /// 
        /// What is the first term in the Fibonacci sequence to contain 1000 digits?
        /// </summary>
        private static void Problem_25()
        {
            BigInteger thousandDigits = 1;
            for (int i = 1; i < 1000; i++)
            {
                thousandDigits *= 10;
            }

            BigInteger nm1 = 1;
            BigInteger nm2 = 0;

            long term = 2;

            while (true)
            {
                BigInteger n = nm1 + nm2;

                if (n >= thousandDigits)
                {
                    Console.WriteLine("first = " + term);
                    return;
                }

                term++;
                nm2 = nm1;
                nm1 = n;
            }
        }

        /// <summary>
        /// A unit fraction contains 1 in the numerator. The decimal representation of the unit fractions with denominators 2 to 10 are given:
        /// 
        ///     1/2  = 	0.5
        ///     1/3  = 	0.(3)
        ///     1/4  = 	0.25
        ///     1/5  = 	0.2
        ///     1/6  = 	0.1(6)
        ///     1/7  = 	0.(142857)
        ///     1/8  = 	0.125
        ///     1/9  = 	0.(1)
        ///     1/10 = 	0.1
        /// 
        /// Where 0.1(6) means 0.166666..., and has a 1-digit recurring cycle. It can be seen that 1/7 has a 6-digit recurring cycle.
        /// 
        /// Find the value of d &lt; 1000 for which 1/d contains the longest recurring cycle in its decimal fraction part.
        /// </summary>
        private static void Problem_26()
        {
            var primes = GetFirstNPrimes(10);

            for (int i = 2; i < 1000; i++)
            {
            }
        }

        /// <summary>
        /// Euler published the remarkable quadratic formula:
        /// 
        /// n² + n + 41
        /// 
        /// It turns out that the formula will produce 40 primes for the consecutive values n = 0 to 39. However, when n = 40, 40^(2) + 40 + 41 = 40(40 + 1) + 41 is divisible by 41, and certainly when n = 41, 41² + 41 + 41 is clearly divisible by 41.
        /// 
        /// Using computers, the incredible formula  n² − 79n + 1601 was discovered, which produces 80 primes for the consecutive values n = 0 to 79. The product of the coefficients, −79 and 1601, is −126479.
        /// 
        /// Considering quadratics of the form:
        /// 
        ///     n² + an + b, where |a| < 1000 and |b| < 1000
        /// 
        ///     where |n| is the modulus/absolute value of n
        ///     e.g. |11| = 11 and |−4| = 4
        /// 
        /// Find the product of the coefficients, a and b, for the quadratic expression that produces the maximum number of primes for consecutive values of n, starting with n = 0.
        /// </summary>
        private static void Problem_27()
        {

        }

        /// <summary>
        /// Starting with the number 1 and moving to the right in a clockwise direction a 5 by 5 spiral is formed as follows:
        /// 
        /// 21 22 23 24 25
        /// 20  7  8  9 10
        /// 19  6  1  2 11
        /// 18  5  4  3 12
        /// 17 16 15 14 13
        /// 
        /// It can be verified that the sum of the numbers on the diagonals is 101.
        /// 
        /// What is the sum of the numbers on the diagonals in a 1001 by 1001 spiral formed in the same way?
        /// </summary>
        private static void Problem_28()
        {
            var size = 1001;

            var max = size * size;

            var total = 0;
            var inc = 2;
            var counter = -1;
            for (int i = 1; i <= max; i += inc)
            {
                total += i;
                counter++;

                if (counter == 4)
                {
                    counter = 0;
                    inc += 2;
                }
            }

            Console.WriteLine("total = " + total);
        }

        /// <summary>
        /// Surprisingly there are only three numbers that can be written as the sum of fourth powers of their digits:
        /// 
        ///     1634 = 1^(4) + 6^(4) + 3^(4) + 4^(4)
        ///     8208 = 8^(4) + 2^(4) + 0^(4) + 8^(4)
        ///     9474 = 9^(4) + 4^(4) + 7^(4) + 4^(4)
        /// 
        /// As 1 = 1^(4) is not a sum it is not included.
        /// 
        /// The sum of these numbers is 1634 + 8208 + 9474 = 19316.
        /// 
        /// Find the sum of all the numbers that can be written as the sum of fifth powers of their digits.
        /// </summary>
        private static void Problem_30()
        {
            long total = 0;

            for (int i = 2; i <= 354294; i++)
            {
                var num = i;
                var dig000001 = num % 10; num /= 10;
                var dig000010 = num % 10; num /= 10;
                var dig000100 = num % 10; num /= 10;
                var dig001000 = num % 10; num /= 10;
                var dig010000 = num % 10; num /= 10;
                var dig100000 = num % 10; num /= 10;

                var digitSum =
                    dig000001 * dig000001 * dig000001 * dig000001 * dig000001 +
                    dig000010 * dig000010 * dig000010 * dig000010 * dig000010 +
                    dig000100 * dig000100 * dig000100 * dig000100 * dig000100 +
                    dig001000 * dig001000 * dig001000 * dig001000 * dig001000 +
                    dig010000 * dig010000 * dig010000 * dig010000 * dig010000 +
                    dig100000 * dig100000 * dig100000 * dig100000 * dig100000;

                if (digitSum == i)
                {
                    total += i;
                }
            }

            Console.WriteLine("total = " + total);
        }

        /// <summary>
        /// The decimal number, 585 = 1001001001_(2) (binary), is palindromic in both bases.
        /// 
        /// Find the sum of all numbers, less than one million, which are palindromic in base 10 and base 2.
        /// 
        /// (Please note that the palindromic number, in either base, may not include leading zeros.)
        /// </summary>
        private static void Problem_36()
        {
            long sum = 0;
            for (int num = 1; num < 1000000; num += 2)
            {
                var dec = Convert.ToString(num, 10);
                var bin = Convert.ToString(num, 2);

                var isPallindrome = true;
                for (int i = 0; i < dec.Length; i++)
                {
                    if (dec[i] != dec[dec.Length - 1 - i])
                    {
                        isPallindrome = false;
                        break;
                    }
                }

                if (!isPallindrome)
                {
                    continue;
                }

                isPallindrome = true;
                for (int i = 0; i < bin.Length; i++)
                {
                    if (bin[i] != bin[bin.Length - 1 - i])
                    {
                        isPallindrome = false;
                        break;
                    }
                }

                if (!isPallindrome)
                {
                    continue;
                }

                sum += num;
            }

            Console.WriteLine("sum = " + sum);
        }

        /// <summary>
        /// It was proposed by Christian Goldbach that every odd composite number can be written as the sum of a prime and twice a square.
        /// 
        /// 9 = 7 + 2×1^(2)
        /// 15 = 7 + 2×2^(2)
        /// 21 = 3 + 2×3^(2)
        /// 25 = 7 + 2×3^(2)
        /// 27 = 19 + 2×2^(2)
        /// 33 = 31 + 2×1^(2)
        /// 
        /// It turns out that the conjecture was false.
        /// 
        /// What is the smallest odd composite that cannot be written as the sum of a prime and twice a square?
        /// </summary>
        private static void Problem_46()
        {
            long upperBound = 1000;

            var primes = GetPrimesBelow(upperBound);

            long lastNumberSquared = 0;
            var doubleSquares = new List<long>();

            Action updateDoubleSquares = () =>
            {
                while (true)
                {
                    lastNumberSquared++;
                    var result = lastNumberSquared * lastNumberSquared * 2;
                    doubleSquares.Add(result);

                    if (result > upperBound)
                    {
                        break;
                    }
                }
            };

            updateDoubleSquares();

            for (long num = 3; ; num += 2)
            {
                while (num >= upperBound)
                {
                    upperBound += 1000;
                    primes = GetPrimesBelow(upperBound);
                    updateDoubleSquares();
                }

                if (primes.Primes.Contains(num))
                {
                    continue;
                }

                bool found = false;

                for (int primeIndex = 0; primeIndex < primes.Primes.Count; primeIndex++)
                {
                    var prime = primes.Primes[primeIndex];
                    if (prime >= num)
                    {
                        break;
                    }

                    for (int doubleSquareIndex = 0; doubleSquareIndex < doubleSquares.Count; doubleSquareIndex++)
                    {
                        var result = doubleSquares[doubleSquareIndex] + prime;

                        if (result == num)
                        {
                            found = true;
                            goto BreakLoops;
                        }
                        else if (result > num)
                        {
                            break;
                        }
                    }
                }

            BreakLoops:
                if (!found)
                {
                    Console.WriteLine("first = " + num);
                    return;
                }
            }
        }

        /// <summary>
        /// The first two consecutive numbers to have two distinct prime factors are:
        /// 
        /// 14 = 2 × 7
        /// 15 = 3 × 5
        /// 
        /// The first three consecutive numbers to have three distinct prime factors are:
        /// 
        /// 644 = 2² × 7 × 23
        /// 645 = 3 × 5 × 43
        /// 646 = 2 × 17 × 19.
        /// 
        /// Find the first four consecutive integers to have four distinct primes factors. What is the first of these numbers?
        /// </summary>
        private static void Problem_47()
        {
            var primes = GetPrimesBelow(1000);

            var num = 4;

            for (long i = 2; ; i++)
            {
                int j;
                var found = true;
                for (j = 0; j < num; j++)
                {
                    var factors = Factor(i + j, primes);
                    if (factors.Count != num)
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    Console.WriteLine("first = " + i);
                    return;
                }
                else
                {
                    i += j;
                }
            }
        }

        /// <summary>
        /// The series, 1^(1) + 2^(2) + 3^(3) + ... + 10^(10) = 10405071317.
        /// 
        /// Find the last ten digits of the series, 1^(1) + 2^(2) + 3^(3) + ... + 1000^(1000).
        /// </summary>
        private static void Problem_48()
        {
            BigInteger sum = 0;

            for (int i = 1; i <= 1000; i++)
            {
                BigInteger product = 1;

                for (int j = 0; j < i; j++)
                {
                    product *= i;
                }

                sum += product;
            }

            var sumString = sum.ToString();

            sumString = sumString.Substring(sumString.Length - 10, 10);

            Console.WriteLine("last digits = " + sumString);
        }

        /// <summary>
        /// The prime 41, can be written as the sum of six consecutive primes:
        /// 41 = 2 + 3 + 5 + 7 + 11 + 13
        /// 
        /// This is the longest sum of consecutive primes that adds to a prime below one-hundred.
        /// 
        /// The longest sum of consecutive primes below one-thousand that adds to a prime, contains 21 terms, and is equal to 953.
        /// 
        /// Which prime, below one-million, can be written as the sum of the most consecutive primes?
        /// </summary>
        private static void Problem_50()
        {
            var primes = GetPrimesBelow(1000000);

            var largesPrime = primes.Primes[primes.Primes.Count - 1];

            int maxLength = 0;
            long maxLengthPrime = 0;

            for (int i = 0; i < primes.Primes.Count; i++)
            {
                for (int num = maxLength + 1; num + i - 1 < primes.Primes.Count; num++)
                {
                    long sum = 0;
                    for (int j = i; j < i + num; j++)
                    {
                        sum += primes.Primes[j];
                    }

                    if (sum >= largesPrime)
                    {
                        break;
                    }

                    if (primes.Primes.Contains(sum) && num > maxLength)
                    {
                        maxLength = num;
                        maxLengthPrime = sum;
                    }
                }
            }

            Console.WriteLine("longest = " + maxLengthPrime);
        }

        /// <summary>
        /// By starting at the top of the triangle below and moving to adjacent numbers on the row below, the maximum total from top to bottom is 23.
        /// 
        /// 3
        /// 7 4
        /// 2 4 6
        /// 8 5 9 3
        /// 
        /// That is, 3 + 7 + 4 + 9 = 23.
        /// 
        /// Find the maximum total from top to bottom in triangle.txt (right click and 'Save Link/Target As...'), a 15K text file containing a triangle with one-hundred rows.
        /// </summary>
        private static void Problem_67()
        {
            var text = File.ReadAllText("triangle.txt");

            var data = (from line in text.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                        select
                            (from entry in line.Split(" \t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                             select int.Parse(entry)).ToArray()).ToArray();

            var values = new Dictionary<Point, long>();

            Func<Point, long> lookup = null;
            lookup = (Point point) =>
            {
                if (point.X >= data.Length)
                {
                    return 0;
                }

                if (!values.ContainsKey(point))
                {
                    var row = point.X;
                    var col = point.Y;

                    values[point] = data[row][col] + Math.Max(lookup(new Point(row + 1, col)), lookup(new Point(row + 1, col + 1)));
                }

                return values[point];
            };

            var max = lookup(new Point(0, 0));

            Console.WriteLine("max = " + max);
        }

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
        private static void Problem_293()
        {
            var primes = GetFirstNPrimes(20);

            var admissibleNumbers = new List<long>(Problem_293_GenerateAdmissible(1, 0, 1000000000, primes).OrderBy(i => i));

            var sqrtLargest = (long)Math.Sqrt(admissibleNumbers[admissibleNumbers.Count - 1]);
            primes = GetPrimesBelow((long)(sqrtLargest * 1.1), primes);
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
                            Console.WriteLine("error");
                            return;
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

            Console.WriteLine("sum = " + sum);
        }

        private static IEnumerable<long> Problem_293_GenerateAdmissible(long currentProduct, int primeIndex, long max, PrimesList primes)
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
                    foreach (var admissable in Problem_293_GenerateAdmissible(product, primeIndex + 1, max, primes))
                    {
                        yield return admissable;
                    }

                    product *= prime;
                }
            }
        }
    }
}