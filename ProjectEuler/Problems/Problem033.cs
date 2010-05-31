namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The fraction 49/98 is a curious fraction, as an inexperienced mathematician in attempting to simplify it may incorrectly believe that 49/98 = 4/8, which is correct, is obtained by cancelling the 9s.
    /// 
    /// We shall consider fractions like, 30/50 = 3/5, to be trivial examples.
    /// 
    /// There are exactly four non-trivial examples of this type of fraction, less than one in value, and containing two digits in the numerator and denominator.
    /// 
    /// If the product of these four fractions is given in its lowest common terms, find the value of the denominator.
    /// </summary>
    [Result(Name = "denominator", Expected = "100")]
    public class Problem033 : Problem
    {
        public override string Solve(string resource)
        {
            var primes = PrimeMath.GetPrimesBelow(1000);

            var productNumerator = 1;
            var productDenominator = 1;

            for (int numerator = 10; numerator <= 98; numerator++)
            {
                if (numerator % 10 == 0)
                {
                    continue;
                }

                //var numeratorFactors = PrimeMath.Factor(numerator, primes);

                for (int denominator = numerator + 1; denominator <= 99; denominator++)
                {
                    if (denominator % 10 == 0)
                    {
                        continue;
                    }

                    var ratio = (double)numerator / (double)denominator;

                    var match = false;
                    if (numerator % 10 == denominator / 10)
                    {
                        var variation = (double)(numerator / 10) / (double)(denominator % 10);

                        if (Math.Abs(variation - ratio) < 0.0001)
                        {
                            match = true;
                        }
                    }

                    if (numerator / 10 == denominator % 10)
                    {
                        var variation = (double)(numerator % 10) / (double)(denominator / 10);

                        if (Math.Abs(variation - ratio) < 0.0001)
                        {
                            match = true;
                        }
                    }

                    if (match)
                    {
                        productNumerator *= numerator;
                        productDenominator *= denominator;
                    }
                }
            }

            var numeratorFactors = PrimeMath.Factor(productNumerator, primes);
            var denominatorFactors = PrimeMath.Factor(productDenominator, primes);

            var result = 1;
            foreach (var factor in denominatorFactors)
            {
                var power = factor.Value;
                power -= numeratorFactors.Where(n => n.Key == factor.Key).Select(n => n.Value).SingleOrDefault();
                for (int i = 0; i < power; i++)
                {
                    result *= (int)factor.Key;
                }
            }

            return result.ToString();
        }
    }
}
