namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Combinatorics;

    /// <summary>
    /// We shall say that an n-digit number is pandigital if it makes use of all the digits 1 to n exactly once. For example, 2143 is a 4-digit pandigital and is also prime.
    ///
    /// What is the largest n-digit pandigital prime that exists?
    /// </summary>
    [Result(Name = "largest", Expected = "7652413")]
    public class Problem041 : Problem
    {
        public override string Solve(string resource)
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

                    if (num > maxPrime && PrimeMath.IsPrime(num, primes))
                    {
                        maxPrime = num;
                    }
                }

                if (maxPrime > 0)
                {
                    return maxPrime.ToString();
                }
            }

            return "error";
        }
    }
}
