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
        private static void Main(string[] args)
        {
            var loaders = new List<IProblemLoader>
            {
                new ProblemLoader<Problem001>(),
                new ProblemLoader<Problem002>(),
                new ProblemLoader<Problem003>(),
                new ProblemLoader<Problem004>(),
                new ProblemLoader<Problem005>(),
                new ProblemLoader<Problem006>(),
                new ProblemLoader<Problem007>(),
                new ProblemLoader<Problem008>(),
                new ProblemLoader<Problem009>(),
                new ProblemLoader<Problem010>(),
                new ProblemLoader<Problem011>(),
                new ProblemLoader<Problem012>(),
                new ProblemLoader<Problem013>(),
                new ProblemLoader<Problem014>(),
                new ProblemLoader<Problem015>(),
                new ProblemLoader<Problem016>(),
                new ProblemLoader<Problem017>(),
                new ProblemLoader<Problem018>(),
                new ProblemLoader<Problem019>(),
                new ProblemLoader<Problem020>(),
            };

            var sw = new Stopwatch();

            foreach (var loader in loaders)
            {
                Console.ResetColor();
                var resource = loader.LoadResource();
                var resultInfo = loader.LoadResultInfo();
                var result = string.Empty;

                sw.Start();

                var problem = loader.LoadProblem();
                result = problem.Solve(resource);

                sw.Stop();
                if (!string.IsNullOrEmpty(resultInfo.Expected))
                {
                    if (result == resultInfo.Expected)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }

                Console.WriteLine(resultInfo.Name + " = " + result);
            }
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Solution Found in " + sw.Elapsed + ".");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Let d(n) be defined as the sum of proper divisors of n (numbers less than n which divide evenly into n).
        /// If d(a) = b and d(b) = a, where a ≠ b, then a and b are an amicable pair and each of a and b are called amicable numbers.
        /// 
        /// For example, the proper divisors of 220 are 1, 2, 4, 5, 10, 11, 20, 22, 44, 55 and 110; therefore d(220) = 284. The proper divisors of 284 are 1, 2, 4, 71 and 142; so d(284) = 220.
        /// 
        /// Evaluate the sum of all the amicable numbers under 10000.
        /// </summary>
        private static void Problem_021()
        {
            var primes = PrimeMath.GetFirstNPrimes(1000);
            var sums = new Dictionary<long, long>();

            for (int i = 2; i < 10000; i++)
            {
                var allFactors = PrimeMath.GetAllFactors(PrimeMath.Factor(i, primes));
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
        private static void Problem_022()
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
        private static void Problem_023()
        {
            long upperLimit = 28123;

            var primes = PrimeMath.GetPrimesBelow(28123);

            var abundantNumbers = new List<int>();

            for (long i = 1; i <= upperLimit; i++)
            {
                var factors = PrimeMath.GetAllFactors(PrimeMath.Factor(i, primes));
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
        private static void Problem_024()
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
        private static void Problem_025()
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
        private static void Problem_026()
        {
            var primes = PrimeMath.GetFirstNPrimes(10);

            Func<Dictionary<long, int>, long> numFromFactors = (factors) =>
            {
                long product = 1;
                foreach (var factor in factors)
                {
                    for (int i = 0; i < factor.Value; i++)
                    {
                        product *= factor.Key;
                    }
                }

                return product;
            };

            Func<long, int> getRepitition = (long num) =>
            {
                long dividend = 10;

                int reps = 1;
                while (true)
                {
                    var quotient = dividend / num;
                    var remainder = dividend % num;

                    if (remainder == 1)
                    {
                        return reps;
                    }
                    else if (remainder == 0)
                    {
                        return 0;
                    }
                    
                    reps++;
                    dividend = remainder *= 10;
                }
            };

            var maxReps = 0;
            var maxRepsValue = 0;
            for (int i = 2; i < 1000; i++)
            {
                var factors = PrimeMath.Factor(i, primes);
                factors.Remove(2);
                factors.Remove(5);

                if (factors.Count == 0)
                {
                    continue;
                }

                var num = numFromFactors(factors);
                var reps = getRepitition(num);
                if (reps > maxReps)
                {
                    maxReps = reps;
                    maxRepsValue = i;
                }
            }
            
            Console.WriteLine("value = " + maxRepsValue);
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
        ///     n² + an + b, where |a| &lt; 1000 and |b| &lt; 1000
        /// 
        ///     where |n| is the modulus/absolute value of n
        ///     e.g. |11| = 11 and |−4| = 4
        /// 
        /// Find the product of the coefficients, a and b, for the quadratic expression that produces the maximum number of primes for consecutive values of n, starting with n = 0.
        /// </summary>
        private static void Problem_027()
        {
            var primes = PrimeMath.GetPrimesBelow(1000);

            var bValues = primes.Primes.Where(p => p < 1000).ToList();

            int max = 0;
            long maxProduct = 0;
            bool isPrime = false;

            foreach (var b in bValues)
            {
                for (int a = -999; a < 1000; a++)
                {
                    for (int n = 0; ; n++)
                    {
                        var value = n * n + a * n + b;
                        isPrime = PrimeMath.IsPrime(value, primes);

                        if (!isPrime)
                        {
                            if (n - 1 > max)
                            {
                                max = n - 1;
                                maxProduct = a * b;
                            }

                            break;
                        }
                    }
                }
            }

            Console.WriteLine("product = " + maxProduct);
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
        private static void Problem_028()
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
        /// Consider all integer combinations of a^(b) for 2 ≤ a  ≤ 5 and 2 ≤ b  ≤ 5:
        /// 
        ///     2^(2)=4, 2^(3)=8, 2^(4)=16, 2^(5)=32
        ///     3^(2)=9, 3^(3)=27, 3^(4)=81, 3^(5)=243
        ///     4^(2)=16, 4^(3)=64, 4^(4)=256, 4^(5)=1024
        ///     5^(2)=25, 5^(3)=125, 5^(4)=625, 5^(5)=3125
        /// 
        /// If they are then placed in numerical order, with any repeats removed, we get the following sequence of 15 distinct terms:
        /// 
        /// 4, 8, 9, 16, 25, 27, 32, 64, 81, 125, 243, 256, 625, 1024, 3125
        /// 
        /// How many distinct terms are in the sequence generated by a^(b) for 2 ≤ a ≤ 100 and 2 ≤ b ≤ 100?
        /// </summary>
        private static void Problem_029()
        {
            var primes = PrimeMath.GetPrimesBelow(100);

            var distinct = new List<Dictionary<long, int>>();

            for (int a = 2; a <= 100; a++)
            {
                var factorsOfA = PrimeMath.Factor(a, primes);
                
                for (int b = 2; b <= 100; b++)
                {
                    var factors = factorsOfA.ToDictionary(f => f.Key, f => f.Value * b);

                    var exists = (from i in distinct
                                  where !(from f in factors
                                          where !i.ContainsKey(f.Key) || i[f.Key] != f.Value
                                          select f).Any()
                                  where !(from f in i
                                          where !factors.ContainsKey(f.Key)
                                          select f).Any()
                                  select i).Any();

                    if (!exists)
                    {
                        distinct.Add(factors);
                    }
                }
            }

            var count = distinct.Count;

            Console.WriteLine("count = " + count);
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
        private static void Problem_030()
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
        /// In England the currency is made up of pound, £, and pence, p, and there are eight coins in general circulation:
        /// 
        ///     1p, 2p, 5p, 10p, 20p, 50p, £1 (100p) and £2 (200p).
        /// 
        /// It is possible to make £2 in the following way:
        /// 
        ///     1×£1 + 1×50p + 2×20p + 1×5p + 1×2p + 3×1p
        /// 
        /// How many different ways can £2 be made using any number of coins?
        /// </summary>
        private static void Problem_031()
        {
            List<int> coinValues = new List<int>
            {
                200,
                100,
                50,
                20,
                10,
                5,
                2,
                1,
            };

            var counts = new Dictionary<Point, int>();

            Func<Point, int> lookup = null;
            lookup = point =>
            {
                if (counts.ContainsKey(point))
                {
                    return counts[point];
                }
                else
                {
                    var index = point.X;
                    var value = point.Y;
                    var coinValue = coinValues[index];

                    if (index == coinValues.Count - 1)
                    {
                        var sum = value % coinValue == 0 ? 1 : 0;
                        counts[point] = sum;
                        return sum;
                    }
                    else
                    {
                        var sum = lookup(new Point(index + 1, value));
                        while (value >= coinValue)
                        {
                            value -= coinValue;

                            sum += lookup(new Point(index + 1, value));
                        }

                        counts[point] = sum;
                        return sum;
                    }
                }
            };

            var count = lookup(new Point(0, 200));

            Console.WriteLine("count = " + count);
        }

        /// <summary>
        /// 145 is a curious number, as 1! + 4! + 5! = 1 + 24 + 120 = 145.
        /// 
        /// Find the sum of all numbers which are equal to the sum of the factorial of their digits.
        /// 
        /// Note: as 1! = 1 and 2! = 2 are not sums they are not included.
        /// </summary>
        private static void Problem_034()
        {
            var digits = new Dictionary<char, long>
            {
                { '0', NumberTheory.Factorial(0) },
                { '1', NumberTheory.Factorial(1) },
                { '2', NumberTheory.Factorial(2) },
                { '3', NumberTheory.Factorial(3) },
                { '4', NumberTheory.Factorial(4) },
                { '5', NumberTheory.Factorial(5) },
                { '6', NumberTheory.Factorial(6) },
                { '7', NumberTheory.Factorial(7) },
                { '8', NumberTheory.Factorial(8) },
                { '9', NumberTheory.Factorial(9) }
            };

            var upperBound = digits['9'] * 7;
            long sum = 0;
            for (long i = 10; i <= upperBound; i++)
            {
                var num = i.ToString();

                long digitalSum = 0;

                for (var j = 0; j < num.Length; j++)
                {
                    digitalSum += digits[num[j]];
                }

                if (digitalSum == i)
                {
                    sum += i;
                }
            }

            Console.WriteLine("sum = " + sum);
        }

        /// <summary>
        /// The number, 197, is called a circular prime because all rotations of the digits: 197, 971, and 719, are themselves prime.
        /// 
        /// There are thirteen such primes below 100: 2, 3, 5, 7, 11, 13, 17, 31, 37, 71, 73, 79, and 97.
        /// 
        /// How many circular primes are there below one million?
        /// </summary>
        private static void Problem_035()
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

            Console.WriteLine("count = " + count);
        }

        /// <summary>
        /// The decimal number, 585 = 1001001001_(2) (binary), is palindromic in both bases.
        /// 
        /// Find the sum of all numbers, less than one million, which are palindromic in base 10 and base 2.
        /// 
        /// (Please note that the palindromic number, in either base, may not include leading zeros.)
        /// </summary>
        private static void Problem_036()
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
        /// The number 3797 has an interesting property. Being prime itself, it is possible to continuously remove digits from left to right, and remain prime at each stage: 3797, 797, 97, and 7. Similarly we can work from right to left: 3797, 379, 37, and 3.
        /// 
        /// Find the sum of the only eleven primes that are both truncatable from left to right and right to left.
        /// 
        /// NOTE: 2, 3, 5, and 7 are not considered to be truncatable primes.
        /// </summary>
        private static void Problem_037()
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

            Console.WriteLine("sum = " + sum);
        }

        /// <summary>
        /// An irrational decimal fraction is created by concatenating the positive integers:
        /// 
        /// 0.123456789101112131415161718192021...
        /// 
        /// It can be seen that the 12th digit of the fractional part is 1.
        /// 
        /// If d_n represents the nth digit of the fractional part, find the value of the following expression.
        /// 
        /// d_1 × d_10 × d_100 × d_1000 × d_10000 × d_100000 × d_1000000
        /// </summary>
        private static void Problem_040()
        {
            var sb = new StringBuilder(1000100);

            for (var i = 1; sb.Length < 1000000; i++)
            {
                sb.Append(i);
            }

            Func<int, int> d = index =>
            {
                return (int)(sb[index - 1] - '0');
            };

            var product = d(1) * d(10) * d(100) * d(1000) * d(10000) * d(100000) * d(1000000);
            Console.WriteLine("product = " + product);
        }

        /// <summary>
        /// We shall say that an n-digit number is pandigital if it makes use of all the digits 1 to n exactly once. For example, 2143 is a 4-digit pandigital and is also prime.
        ///
        /// What is the largest n-digit pandigital prime that exists?
        /// </summary>
        private static void Problem_041()
        {
            var primes = PrimeMath.GetPrimesBelow((long)Math.Sqrt(987654321));

            long maxPrime = 0;

            for (int i = 9; i >= 1; i--)
            {
                foreach (IList<int> digits in new Permutations<int>(Enumerable.Range(1, i).ToList()))
                {
                    long num = 0;
                    foreach (var d in digits)
                    {
                        num *= 10;
                        num += d;
                    }

                    if (PrimeMath.IsPrime(num, primes))
                    {
                        Console.WriteLine("largest = " + maxPrime);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// The nth term of the sequence of triangle numbers is given by, t(n) = n(n+1)/2; so the first ten triangle numbers are:
        /// 
        /// 1, 3, 6, 10, 15, 21, 28, 36, 45, 55, ...
        /// 
        /// By converting each letter in a word to a number corresponding to its alphabetical position and adding these values we form a word value. For example, the word value for SKY is 19 + 11 + 25 = 55 = t(10). If the word value is a triangle number then we shall call the word a triangle word.
        /// 
        /// Using words.txt, a 16K text file containing nearly two-thousand common English words, how many are triangle words?
        /// </summary>
        private static void Problem_042()
        {
            var text = File.ReadAllText("words.txt");

            var words = from n in text.Split(',')
                        let word = n.Substring(1, n.Length - 2)
                        orderby word
                        select word;

            Func<string, int> getWordValue = word =>
            {
                var sum = 0;
                foreach (var letter in word)
                {
                    sum += (letter - 'A') + 1;
                }

                return sum;
            };

            var count = 0;
            foreach (var word in words)
            {
                if (NumberTheory.IsTriangular(getWordValue(word)))
                {
                    count++;
                }
            }

            Console.WriteLine("count = " + count);
        }

        /// <summary>
        /// Triangle, pentagonal, and hexagonal numbers are generated by the following formulae:
        /// Triangle 	  	T_n=n(n+1)/2 	  	1, 3, 6, 10, 15, ...
        /// Pentagonal 	  	P_n=n(3n−1)/2 	  	1, 5, 12, 22, 35, ...
        /// Hexagonal 	  	H_n=n(2n−1) 	  	1, 6, 15, 28, 45, ...
        /// 
        /// It can be verified that T_285 = P_165 = H_143 = 40755.
        /// 
        /// Find the next triangle number that is also pentagonal and hexagonal.
        /// </summary>
        private static void Problem_045()
        {
            Func<long, long> square = num => num * num;
            Func<long, long> triangle = num => num % 2 == 0 ? (num / 2) * (num + 1) : ((num + 1) / 2) * num;
            Func<long, long> pentagon = num => num % 2 == 0 ? (num / 2) * (3 * num - 1) : ((3 * num - 1) / 2) * num;
            Func<long, long> hexagon = num => num * (2 * num - 1);

            for (long i = 144; ; i++)
            {
                var num = hexagon(i);

                if (NumberTheory.IsPentagonal(num) && NumberTheory.IsTriangular(num))
                {
                    Console.WriteLine("first = " + num);
                    return;
                }
            }
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
        private static void Problem_046()
        {
            long upperBound = 1000;

            var primes = PrimeMath.GetPrimesBelow(upperBound);

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
                    primes = PrimeMath.GetPrimesBelow(upperBound);
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
        private static void Problem_047()
        {
            var primes = PrimeMath.GetPrimesBelow(1000);

            var num = 4;

            for (long i = 2; ; i++)
            {
                int j;
                var found = true;
                for (j = 0; j < num; j++)
                {
                    var factors = PrimeMath.Factor(i + j, primes);
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
        private static void Problem_048()
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
        /// The arithmetic sequence, 1487, 4817, 8147, in which each of the terms increases by 3330, is unusual in two ways: (i) each of the three terms are prime, and, (ii) each of the 4-digit numbers are permutations of one another.
        /// 
        /// There are no arithmetic sequences made up of three 1-, 2-, or 3-digit primes, exhibiting this property, but there is one other 4-digit increasing sequence.
        /// 
        /// What 12-digit number do you form by concatenating the three terms in this sequence?
        /// </summary>
        private static void Problem_049()
        {
            var primes = PrimeMath.GetPrimesBelow(10000);

            for (var aIndex = 169; aIndex < primes.Primes.Count - 2; aIndex++)
            {
                var a = primes.Primes[aIndex];

                if (a == 1487)
                {
                    continue;
                }

                for (var bIndex = aIndex + 1; aIndex < primes.Primes.Count - 1; bIndex++)
                {
                    var b = primes.Primes[bIndex];

                    var c = b - a + b;

                    if (c > 9999)
                    {
                        break;
                    }

                    if (!NumberTheory.IsAnagram(a, b))
                    {
                        continue;
                    }

                    if (!PrimeMath.IsPrime(c, primes))
                    {
                        continue;
                    }

                    if (NumberTheory.IsAnagram(b, c))
                    {
                        Console.WriteLine("composite = " + a.ToString() + b.ToString() + c.ToString());
                        return;
                    }
                }
            }
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
        private static void Problem_050()
        {
            var primes = PrimeMath.GetPrimesBelow(1000000);

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
        /// It can be seen that the number, 125874, and its double, 251748, contain exactly the same digits, but in a different order.
        /// 
        /// Find the smallest positive integer, x, such that 2x, 3x, 4x, 5x, and 6x, contain the same digits.
        /// </summary>
        private static void Problem_052()
        {
            int digit = 1;
            int nextDigit = 10;

            for (int x = 1; ; x++)
            {
                if (x >= nextDigit)
                {
                    digit *= 10;
                    nextDigit *= 10;
                }

                var firstDigit = x / digit;

                if (firstDigit >= 2 && firstDigit <= 4)
                {
                    continue;
                }

                if (NumberTheory.IsAnagram(2 * x, 3 * x) &&
                    NumberTheory.IsAnagram(3 * x, 4 * x) &&
                    NumberTheory.IsAnagram(4 * x, 5 * x) &&
                    NumberTheory.IsAnagram(5 * x, 6 * x))
                {
                    Console.WriteLine("number = " + x);
                    return;
                }
            }
        }

        /// <summary>
        /// In the card game poker, a hand consists of five cards and are ranked, from lowest to highest, in the following way:
        /// 
        ///     * High Card: Highest value card.
        ///     * One Pair: Two cards of the same value.
        ///     * Two Pairs: Two different pairs.
        ///     * Three of a Kind: Three cards of the same value.
        ///     * Straight: All cards are consecutive values.
        ///     * Flush: All cards of the same suit.
        ///     * Full House: Three of a kind and a pair.
        ///     * Four of a Kind: Four cards of the same value.
        ///     * Straight Flush: All cards are consecutive values of same suit.
        ///     * Royal Flush: Ten, Jack, Queen, King, Ace, in same suit.
        /// 
        /// The cards are valued in the order:
        /// 2, 3, 4, 5, 6, 7, 8, 9, 10, Jack, Queen, King, Ace.
        /// 
        /// If two players have the same ranked hands then the rank made up of the highest value wins; for example, a pair of eights beats a pair of fives (see example 1 below). But if two ranks tie, for example, both players have a pair of queens, then highest cards in each hand are compared (see example 4 below); if the highest cards tie then the next highest cards are compared, and so on.
        /// 
        /// Consider the following five hands dealt to two players:
        /// 
        /// Hand    Player 1           Player 2             Winner
        /// 
        /// 1       5H 5C 6S 7S KD     2C 3S 8S 8D TD       Player 2
        ///         Pair of Fives      Pair of Eights
        ///         
        /// 2       5D 8C 9S JS AC     2C 5C 7D 8S QH       Player 1
        ///         Highest card Ace   Highest card Queen
        ///         
        /// 3       2D 9C AS AH AC     3D 6D 7D TD QD       Player 2
        ///         Three Aces         Flush with Diamonds
        ///         
        /// 4	 	4D 6S 9H QH QC     3D 6D 7H QD QS       Player 1
        ///         Pair of Queens     Pair of Queens
        ///         Highest card Nine  Highest card Seven 
        ///         
        /// 5	 	2H 2D 4C 4D 4S     3C 3D 3S 9S 9D       Player 1
        ///         Full House         Full House
        ///         With Three Fours   With Three Threes
        /// 
        /// The file, poker.txt, contains one-thousand random hands dealt to two players. Each line of the file contains ten cards (separated by a single space): the first five are Player 1's cards and the last five are Player 2's cards. You can assume that all hands are valid (no invalid characters or repeated cards), each player's hand is in no specific order, and in each hand there is a clear winner.
        /// 
        /// How many hands does Player 1 win?
        /// </summary>
        private static void Problem_054()
        {
            var deals = (from l in File.ReadLines("poker.txt")
                         let cards = l.Split(' ')
                         let values = from c in cards
                                      let value = c.Substring(0, c.Length - 1)
                                      select new Problem_054_Card
                                      {
                                          Suit = c[c.Length - 1],
                                          Value = value == "A" ? 14 : (value == "K" ? 13 : (value == "Q" ? 12 : (value == "J" ? 11 : (value == "T" ? 10 : int.Parse(value))))),
                                      }
                         select new
                         {
                             Player1 = values.Take(5).OrderByDescending(c => c.Value).ToList(),
                             Player2 = values.Skip(5).OrderByDescending(c => c.Value).ToList(),
                         }).ToList();

            Func<IList<Problem_054_Card>, string> score = hand =>
            {
                var isFlush = hand.Select(s => s.Suit).Distinct().Count() == 1;

                var isStraight = hand[0].Value == hand[1].Value + 1 &&
                                 hand[1].Value == hand[2].Value + 1 &&
                                 hand[2].Value == hand[3].Value + 1 &&
                                 hand[3].Value == hand[4].Value + 1;

                Func<int, string> pad = value => "." + value.ToString().PadLeft(2, '0');

                if (isStraight)
                {
                    return (isFlush ? "08" : "04") + pad(hand[0].Value);
                }
                else if (isFlush)
                {
                    return "05" + pad(hand[0].Value) + pad(hand[1].Value) + pad(hand[2].Value) + pad(hand[3].Value) + pad(hand[4].Value);
                }

                var groups = (from c in hand
                              group hand by c.Value into g
                              orderby g.Key descending
                              orderby g.Count() descending
                              select new
                              {
                                  Value = g.Key,
                                  Count = g.Count(),
                              }).ToList();

                if (groups[0].Count == 4)
                {
                    return "07" + pad(groups[0].Value) + pad(groups[1].Value);
                }
                else if (groups[0].Count == 3)
                {
                    if (groups[1].Count == 2)
                    {
                        return "06" + pad(groups[0].Value) + pad(groups[1].Value);
                    }
                    else
                    {
                        return "03" + pad(groups[0].Value) + pad(groups[1].Value) + pad(groups[2].Value);
                    }
                }
                else if (groups[0].Count == 2)
                {
                    if (groups[1].Count == 2)
                    {
                        return "02" + pad(groups[0].Value) + pad(groups[1].Value) + pad(groups[2].Value);
                    }
                    else
                    {
                        return "01" + pad(groups[0].Value) + pad(groups[1].Value) + pad(groups[2].Value) + pad(groups[3].Value);
                    }
                }

                return "00" + pad(groups[0].Value) + pad(groups[1].Value) + pad(groups[2].Value) + pad(groups[3].Value) + pad(groups[4].Value);
            };

            /// High Card = 00.AA.BB.CC.DD.EE
            /// One Pair = 01.AA.CC.DD.EE
            /// Two Pairs = 02.AA.BB.EE
            /// Three of a Kind = 03.AA.DD.EE
            /// Straight = 04.AA
            /// Flush = 05.AA.BB.CC.DD.EE
            /// Full House = 06.AA.BB
            /// Four of a Kind = 07.AA.EE
            /// Straight\Royal Flush = 08.AA

            var count = 0;
            foreach (var deal in deals)
            {
                var p1 = score(deal.Player1);
                var p2 = score(deal.Player2);

                if (p1.CompareTo(p2) > 0)
                {
                    count++;
                }
            }

            Console.WriteLine("count = " + count);
        }

        [DebuggerDisplay("{Value} of {Suit}")]
        private class Problem_054_Card
        {
            public char Suit { get; set; }
            public int Value { get; set; }
        }

        /// <summary>
        /// Each character on a computer is assigned a unique code and the preferred standard is ASCII (American Standard Code for Information Interchange). For example, uppercase A = 65, asterisk (*) = 42, and lowercase k = 107.
        /// 
        /// A modern encryption method is to take a text file, convert the bytes to ASCII, then XOR each byte with a given value, taken from a secret key. The advantage with the XOR function is that using the same encryption key on the cipher text, restores the plain text; for example, 65 XOR 42 = 107, then 107 XOR 42 = 65.
        /// 
        /// For unbreakable encryption, the key is the same length as the plain text message, and the key is made up of random bytes. The user would keep the encrypted message and the encryption key in different locations, and without both "halves", it is impossible to decrypt the message.
        /// 
        /// Unfortunately, this method is impractical for most users, so the modified method is to use a password as a key. If the password is shorter than the message, which is likely, the key is repeated cyclically throughout the message. The balance for this method is using a sufficiently long password key for security, but short enough to be memorable.
        /// 
        /// Your task has been made easy, as the encryption key consists of three lower case characters. Using cipher1.txt, a file containing the encrypted ASCII codes, and the knowledge that the plain text must contain common English words, decrypt the message and find the sum of the ASCII values in the original text.
        /// </summary>
        private static void Problem_059()
        {
            var bytes = (from l in File.ReadAllText("cipher1.txt").Split(',')
                         select (byte)int.Parse(l)).ToArray();

            var passwords = new Variations<byte>(Enumerable.Range(0, 26).Select(l => (byte)l).ToList(), 3, GenerateOption.WithRepetition);

            var maxLatinCharacters = 0;
            var maxLatinMessage = "";
            foreach (var pass in passwords)
            {
                var passArray = pass.ToArray();
                for (int i = 0; i < passArray.Length; i++)
                {
                    passArray[i] += (byte)'a';
                }

                var sb = new StringBuilder();

                var latinCharacters = 0;
                for (int i = 0; i < bytes.Length; i++)
                {
                    var character = (char)(byte)(passArray[i % passArray.Length] ^ bytes[i]);
                    if ((character >= 'A' && character <= 'Z') ||
                        (character >= 'a' && character <= 'z') ||
                        (character >= '0' && character <= '9') ||
                        (character == ' ') ||
                        (character == '.') ||
                        (character == ','))
                    {
                        latinCharacters++;
                    }

                    sb.Append((char)character);
                }

                if (latinCharacters > maxLatinCharacters)
                {
                    maxLatinCharacters = latinCharacters;
                    maxLatinMessage = sb.ToString();
                }
            }

            var sum = maxLatinMessage.ToCharArray().Select(c => (int)c).Sum();
            Console.WriteLine("sum = " + sum);
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
        /// Find the maximum total from top to bottom in triangle.txt, a 15K text file containing a triangle with one-hundred rows.
        /// </summary>
        private static void Problem_067()
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
        /// It is possible to write five as a sum in exactly six different ways:
        /// 
        /// 4 + 1
        /// 3 + 2
        /// 3 + 1 + 1
        /// 2 + 2 + 1
        /// 2 + 1 + 1 + 1
        /// 1 + 1 + 1 + 1 + 1
        /// 
        /// How many different ways can one hundred be written as a sum of at least two positive integers?
        /// </summary>
        private static void Problem_076()
        {
            var target = 100;

            List<int> coinValues = Enumerable.Range(1, target - 1).OrderByDescending(c => c).ToList();

            var counts = new Dictionary<Point, int>();

            Func<Point, int> lookup = null;
            lookup = point =>
            {
                if (counts.ContainsKey(point))
                {
                    return counts[point];
                }
                else
                {
                    var index = point.X;
                    var value = point.Y;
                    var coinValue = coinValues[index];

                    if (index == coinValues.Count - 1)
                    {
                        var sum = value % coinValue == 0 ? 1 : 0;
                        counts[point] = sum;
                        return sum;
                    }
                    else
                    {
                        var sum = lookup(new Point(index + 1, value));
                        while (value >= coinValue)
                        {
                            value -= coinValue;

                            sum += lookup(new Point(index + 1, value));
                        }

                        counts[point] = sum;
                        return sum;
                    }
                }
            };

            var count = lookup(new Point(0, target));

            Console.WriteLine("count = " + count);
        }

        /// <summary>
        /// A common security method used for online banking is to ask the user for three random characters from a passcode. For example, if the passcode was 531278, they may ask for the 2nd, 3rd, and 5th characters; the expected reply would be: 317.
        /// 
        /// The text file, keylog.txt, contains fifty successful login attempts.
        /// 
        /// Given that the three characters are always asked for in order, analyse the file so as to determine the shortest possible secret passcode of unknown length.
        /// </summary>
        private static void Problem_079()
        {
            var entries = File.ReadAllLines("keylog.txt").Distinct().ToList();

            var characters = (from e in entries
                              select e).SelectMany(e => e.ToCharArray()).Distinct().OrderBy(e => e).ToList();

            Func<string, string, bool> isMatch = (pin, entry) =>
            {
                var start = 0;
                foreach (var pos in entry)
                {
                    var next = pin.IndexOf(pos, start);
                    if (next < 0)
                    {
                        return false;
                    }

                    start = next + 1;
                }

                return true;
            };

            for (int i = characters.Count; ; i++)
            {
                foreach (var comb in new Variations<char>(characters, i, GenerateOption.WithRepetition))
                {
                    var pin = new string(comb.ToArray());

                    var found = true;
                    foreach (var entry in entries)
                    {
                        if (!isMatch(pin, entry))
                        {
                            found = false;
                            break;
                        }
                    }

                    if (found)
                    {
                        Console.WriteLine("pin = " + pin);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// In the 5 by 5 matrix below, the minimal path sum from the top left to the bottom right, by only moving to the right and down, is indicated in bold red and is equal to 2427.
        /// 
        /// [131]	 673 	 234 	 103 	  18
        /// [201]	[ 96] 	[342]	 965 	 150 
        ///  630 	 803 	[746]	[422]	 111 
        ///  537 	 699 	 497 	[121]	 956 
        ///  805 	 732 	 524 	[ 37]	[331]
        /// 
        /// Find the minimal path sum, in matrix.txt, a 31K text file containing a 80 by 80 matrix, from the top left to the bottom right by only moving right and down.
        /// </summary>
        private static void Problem_081()
        {
            var grid = (from l in File.ReadLines("matrix.txt")
                        select (from i in l.Split(',')
                                select int.Parse(i)).ToArray()).ToArray();

            var height = grid.Length;
            var width = grid[0].Length;

            var values = new Dictionary<Point, int>();

            Func<Point, int> lookup = null;
            lookup = point =>
            {
                if (values.ContainsKey(point))
                {
                    return values[point];
                }

                var x = point.X;
                var y = point.Y;

                var sum = grid[y][x];

                if (x == width - 1)
                {
                    if (y == height - 1)
                    {
                        return sum;
                    }

                    sum += lookup(new Point(x, y + 1));
                }
                else if (y == height - 1)
                {
                    sum += lookup(new Point(x + 1, y));
                }
                else
                {
                    sum += Math.Min(
                        lookup(new Point(x + 1, y)),
                        lookup(new Point(x, y + 1)));
                }

                values[point] = sum;
                return sum;
            };

            var min = lookup(new Point(0, 0));

            Console.WriteLine("min = " + min);
        }

        /// <summary>
        /// The rules for writing Roman numerals allow for many ways of writing each number (see FAQ: Roman Numerals). However, there is always a "best" way of writing a particular number.
        /// 
        /// For example, the following represent all of the legitimate ways of writing the number sixteen:
        /// 
        /// IIIIIIIIIIIIIIII
        /// VIIIIIIIIIII
        /// VVIIIIII
        /// XIIIIII
        /// VVVI
        /// XVI
        /// 
        /// The last example being considered the most efficient, as it uses the least number of numerals.
        /// 
        /// The 11K text file, roman.txt, contains one thousand numbers written in valid, but not necessarily minimal, Roman numerals; that is, they are arranged in descending units and obey the subtractive pair rule (see FAQ for the definitive rules for this problem).
        /// 
        /// Find the number of characters saved by writing each of these in their minimal form.
        /// 
        /// Note: You can assume that all the Roman numerals in the file contain no more than four consecutive identical units.
        /// </summary>
        private static void Problem_089()
        {
            var numerals = File.ReadAllLines("roman.txt");

            var digits = new Dictionary<char, int>
            {
                { 'M', 1000 },
                { 'D', 500 },
                { 'C', 100 },
                { 'L', 50 },
                { 'X', 10 },
                { 'V', 5 },
                { 'I', 1 },
            };

            var remainders = new Dictionary<int, Dictionary<int, string>>
            {
                { 100, new Dictionary<int, string>
                        {
                            { 1, "C" },
                            { 2, "CC" },
                            { 3, "CCC" },
                            { 4, "CD" },
                            { 5, "D" },
                            { 6, "DC" },
                            { 7, "DCC" },
                            { 8, "DCCC" },
                            { 9, "CM" },
                        }},
                { 10, new Dictionary<int, string>
                        {
                            { 1, "X" },
                            { 2, "XX" },
                            { 3, "XXX" },
                            { 4, "XL" },
                            { 5, "L" },
                            { 6, "LX" },
                            { 7, "LXX" },
                            { 8, "LXXX" },
                            { 9, "XC" },
                        }},
                { 1, new Dictionary<int, string>
                        {
                            { 1, "I" },
                            { 2, "II" },
                            { 3, "III" },
                            { 4, "IV" },
                            { 5, "V" },
                            { 6, "VI" },
                            { 7, "VII" },
                            { 8, "VIII" },
                            { 9, "IX" },
                        }},
            };

            Func<string, int> parse = numeral =>
            {
                int value = 0;
                int minvalue = 0;
                for (int i = numeral.Length - 1; i >= 0; i--)
                {
                    var digit = digits[numeral[i]];
                    if (digit >= minvalue)
                    {
                        minvalue = digit;
                        value += digit;
                    }
                    else
                    {
                        value -= digit;
                    }
                }

                return value;
            };

            Func<int, string> toRoman = number =>
            {
                var sb = new StringBuilder();
                while (number >= 1000)
                {
                    sb.Append('M');
                    number -= 1000;
                }

                foreach (var rem in remainders.Keys)
                {
                    if (number >= rem)
                    {
                        var i = number / rem;

                        sb.Append(remainders[rem][i]);
                        number -= rem * i;
                    }
                }

                return sb.ToString();
            };

            var sum = 0;
            foreach (var numeral in numerals)
            {
                var value = parse(numeral);
                var text = toRoman(value);

                sum += numeral.Length - text.Length;
            }

            Console.WriteLine("sum = " + sum);
        }

        /// <summary>
        /// Peter has nine four-sided (pyramidal) dice, each with faces numbered 1, 2, 3, 4.
        /// Colin has six six-sided (cubic) dice, each with faces numbered 1, 2, 3, 4, 5, 6.
        /// 
        /// Peter and Colin roll their dice and compare totals: the highest total wins. The result is a draw if the totals are equal.
        /// 
        /// What is the probability that Pyramidal Pete beats Cubic Colin? Give your answer rounded to seven decimal places in the form 0.abcdefg
        /// </summary>
        private static void Problem_205()
        {
            long pete = 0;
            long total = 0;

            var cSums = new Dictionary<int, long>();

            foreach (var cDice in new Variations<int>(new List<int> { 1, 2, 3, 4, 5, 6 }, 6, GenerateOption.WithRepetition))
            {
                var cSum = cDice.Sum();

                if (!cSums.ContainsKey(cSum))
                {
                    cSums[cSum] = 0;
                }

                cSums[cSum]++;
            }

            var pSums = new Dictionary<int, long>();

            foreach (var pDice in new Variations<int>(new List<int> { 1, 2, 3, 4 }, 9, GenerateOption.WithRepetition))
            {
                var pSum = pDice.Sum();

                if (!pSums.ContainsKey(pSum))
                {
                    pSums[pSum] = 0;
                }

                pSums[pSum]++;
            }

            foreach (var cSum in cSums.Keys)
            {
                var cRolls = cSums[cSum];
                foreach (var pSum in pSums.Keys)
                {
                    var pRolls = pSums[pSum];

                    var rolls = cRolls * pRolls;
                    total += rolls;

                    if (pSum > cSum)
                    {
                        pete += rolls;
                    }
                }
            }

            decimal peteRatio = pete;
            peteRatio /= total;
            Console.WriteLine("probability = " + Math.Round(peteRatio, 7));
        }

        /// <summary>
        /// Find the unique positive integer whose square has the form 1_2_3_4_5_6_7_8_9_0, where each “_” is a single digit.
        /// </summary>
        private static void Problem_206()
        {
            // Bounds:
            // 1010101010 < x < 1389026623

            for (var i = 1010101010; i < 1389026623; i++)
            {
                var product = (long)i * i;

                var digit = product % 10;
                if (digit != 0)
                {
                    continue;
                }

                product /= 100;
                digit = product % 10;
                if (digit != 9)
                {
                    continue;
                }

                product /= 100;
                digit = product % 10;
                if (digit != 8)
                {
                    continue;
                }

                product /= 100;
                digit = product % 10;
                if (digit != 7)
                {
                    continue;
                }

                product /= 100;
                digit = product % 10;
                if (digit != 6)
                {
                    continue;
                }

                product /= 100;
                digit = product % 10;
                if (digit != 5)
                {
                    continue;
                }

                product /= 100;
                digit = product % 10;
                if (digit != 4)
                {
                    continue;
                }

                product /= 100;
                digit = product % 10;
                if (digit != 3)
                {
                    continue;
                }

                product /= 100;
                digit = product % 10;
                if (digit != 2)
                {
                    continue;
                }

                product /= 100;
                digit = product % 10;
                if (digit != 1)
                {
                    continue;
                }

                Console.WriteLine("int = " + i);
                return;
            }
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
            var primes = PrimeMath.GetFirstNPrimes(20);

            var admissibleNumbers = new List<long>(Problem_293_GenerateAdmissible(1, 0, 1000000000, primes).OrderBy(i => i));

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