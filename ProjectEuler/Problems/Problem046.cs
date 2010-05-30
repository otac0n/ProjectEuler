namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
    [Result(Name = "smallest", Expected = "5777")]
    public class Problem046 : Problem
    {
        public override string Solve(string resource)
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
                    return num.ToString();
                }
            }
        }
    }
}
