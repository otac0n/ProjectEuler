namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The prime factors of 13195 are 5, 7, 13 and 29.
    /// 
    /// What is the largest prime factor of the number 600851475143 ?
    /// </summary>
    [Result(Name = "largest")]
    public class Problem003 : Problem
    {
        public override string Solve(string resource)
        {
            long number = 600851475143;

            var primes = PrimeMath.GetPrimesBelow(1000);
            var factors = PrimeMath.Factor(number, primes);
            var largest = factors.Keys.Max();

            return largest.ToString();
        }
    }
}
