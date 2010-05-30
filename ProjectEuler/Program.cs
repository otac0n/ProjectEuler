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
                new ProblemLoader<Problem021>(),
                new ProblemLoader<Problem022>(),
                new ProblemLoader<Problem023>(),
                new ProblemLoader<Problem024>(),
                new ProblemLoader<Problem025>(),
                new ProblemLoader<Problem026>(),
                new ProblemLoader<Problem027>(),
                new ProblemLoader<Problem028>(),
                new ProblemLoader<Problem029>(),
                new ProblemLoader<Problem030>(),
                new ProblemLoader<Problem031>(),
                new ProblemLoader<Problem032>(),
                new ProblemLoader<Problem033>(),
                new ProblemLoader<Problem034>(),
                new ProblemLoader<Problem035>(),
                new ProblemLoader<Problem036>(),
                new ProblemLoader<Problem037>(),
                new ProblemLoader<Problem038>(),
                new ProblemLoader<Problem039>(),
                new ProblemLoader<Problem040>(),
            };

            var sw = new Stopwatch();
            var previousElapsed = new TimeSpan();
            Console.SetWindowSize(80, 80);

            int zebra = 0;
            foreach (var loader in loaders)
            {
                Console.ResetColor();
                Console.BackgroundColor = zebra == 0 ? ConsoleColor.Black : ConsoleColor.Black;

                var resource = loader.LoadResource();
                var resultInfo = loader.LoadResultInfo();
                var result = string.Empty;

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(loader.ProblemName + ": " + resultInfo.Name.PadRight(11, ' ') + " = ");

                sw.Start();
                var problem = loader.LoadProblem();
                result = problem.Solve(resource);
                sw.Stop();

                Console.ForegroundColor = string.IsNullOrEmpty(resultInfo.Expected) && !(string.IsNullOrEmpty(result) || result == "error") ? ConsoleColor.Yellow : (string.IsNullOrEmpty(result) || result == "error" || result != resultInfo.Expected ? ConsoleColor.Red : ConsoleColor.Green);
                Console.Write((string.IsNullOrEmpty(result) || result == "error" ? "(error)" : result).PadRight(26 - Math.Max(resultInfo.Name.Length, 11)));

                var delta = sw.Elapsed - previousElapsed;
                Console.ForegroundColor = delta > TimeSpan.FromSeconds(0.75) ? ConsoleColor.Red : ConsoleColor.Cyan;
                Console.WriteLine("+" + delta);
                previousElapsed = sw.Elapsed;
                zebra = 1 - zebra;
            }

            Console.ResetColor();
            Console.WriteLine();
            Console.Write("Solution Found in ");
            Console.ForegroundColor = sw.Elapsed > TimeSpan.FromSeconds(loaders.Count) ? ConsoleColor.Red : ConsoleColor.Cyan;
            Console.Write(sw.Elapsed);
            Console.ResetColor();
            Console.WriteLine(".");
            Console.ReadKey(true);
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