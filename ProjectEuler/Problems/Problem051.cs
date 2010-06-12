namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Combinatorics;

    /// <summary>
    /// By replacing the 1st digit of *3, it turns out that six of the nine possible values: 13, 23, 43, 53, 73, and 83, are all prime.
    /// 
    /// By replacing the 3rd and 4th digits of 56**3 with the same digit, this 5-digit number is the first example having seven primes among the ten generated numbers, yielding the family: 56003, 56113, 56333, 56443, 56663, 56773, and 56993. Consequently 56003, being the first member of this family, is the smallest prime with this property.
    /// 
    /// Find the smallest prime which, by replacing part of the number (not necessarily adjacent digits) with the same digit, is part of an eight prime value family.
    /// </summary>
    [Result(Name = "smallest", Expected = "121313")]
    public class Problem051 : Problem
    {
        public override string Solve(string resource)
        {
            var primes = PrimeMath.GetFirstNPrimes(100);

            for (int primeIndex = 0; ; primeIndex++)
            {
                if (primeIndex >= primes.Primes.Count)
                {
                    PrimeMath.GetFirstNPrimes(primes.Primes.Count + 100, primes);
                }

                var prime = primes.Primes[primeIndex];

                var primeStr = prime.ToString();

                var charList = Enumerable.Range(0, primeStr.Length - 1).ToList();

                for (int replacementCount = 1; replacementCount < primeStr.Length - 1; replacementCount++)
                {
                    foreach (IList<int> chars in new Combinations<int>(charList, replacementCount))
                    {
                        var digits = primeStr.ToCharArray();
                        var count = 0;
                        long min = 0;

                        for (int digit = 0; digit <= 9; digit++)
                        {
                            if (digit == 0 && chars[0] == 0)
                            {
                                continue;
                            }

                            foreach (var cd in chars)
                            {
                                digits[cd] = (char)(digit + '0');
                            }

                            var num = long.Parse(new string(digits));

                            if (PrimeMath.IsPrime(num, primes))
                            {
                                if (min == 0)
                                {
                                    min = num;
                                }

                                count++;
                            }
                        }

                        if (count >= 8)
                        {
                            return min.ToString();
                        }
                    }
                }
            }

            return "error";
        }
    }
}
