namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
    [Result(Name = "longest", Expected = "983")]
    public class Problem026 : Problem
    {
        public override string Solve(string resource)
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

            return maxRepsValue.ToString();
        }
    }
}
