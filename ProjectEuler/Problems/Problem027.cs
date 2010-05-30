namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
    [Result(Name = "product", Expected = "-59231")]
    public class Problem027 : Problem
    {
        public override string Solve(string resource)
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

            return maxProduct.ToString();
        }
    }
}
