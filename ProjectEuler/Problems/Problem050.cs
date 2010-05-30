namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
    [Result(Name = "longest", Expected = "997651")]
    public class Problem050 : Problem
    {
        public override string Solve(string resource)
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

            return maxLengthPrime.ToString();
        }
    }
}
