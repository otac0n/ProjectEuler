namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// The smallest number expressible as the sum of a prime square, prime cube, and prime fourth power is 28. In fact, there are exactly four numbers below fifty that can be expressed in such a way:
    /// 
    /// 28 = 2^(2) + 2^(3) + 2^(4)
    /// 33 = 3^(2) + 2^(3) + 2^(4)
    /// 49 = 5^(2) + 2^(3) + 2^(4)
    /// 47 = 2^(2) + 3^(3) + 2^(4)
    /// 
    /// How many numbers below fifty million can be expressed as the sum of a prime square, prime cube, and prime fourth power?
    /// </summary>
    [Result(Name = "result", Expected = "1097343")]
    public class Problem087 : Problem
    {
        public override string Solve(string resource)
        {
            var max = 50000000;
            var maxA = (long)Math.Sqrt(max - (2 * 2 * 2) - (2 * 2 * 2 * 2)) + 1;
            var primes = PrimeMath.GetPrimesBelow(maxA);

            var numbers = new Dictionary<long, bool>();

            for (int a = 0; a < primes.Primes.Count; a++)
            {
                var a_prime = primes.Primes[a];
                var a_squared = a_prime * a_prime;

                for (int b = 0; b < primes.Primes.Count; b++)
                {
                    var b_prime = primes.Primes[b];
                    var b_cubed = b_prime * b_prime * b_prime;

                    for (int c = 0; c < primes.Primes.Count; c++)
                    {
                        var c_prime = primes.Primes[c];
                        var c_hypercubed = c_prime * c_prime * c_prime * c_prime;

                        var sum = a_squared + b_cubed + c_hypercubed;

                        if (sum >= max)
                        {
                            break;
                        }

                        numbers[sum] = true;
                    }                
                }
            }

            return numbers.Count.ToString();
        }
    }
}
