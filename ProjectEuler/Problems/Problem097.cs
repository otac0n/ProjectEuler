namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// The first known prime found to exceed one million digits was discovered in 1999, and is a Mersenne prime of the form 2^(6972593)−1; it contains exactly 2,098,960 digits. Subsequently other Mersenne primes, of the form 2^(p)−1, have been found which contain more digits.
    /// 
    /// However, in 2004 there was found a massive non-Mersenne prime which contains 2,357,207 digits: 28433×2^(7830457)+1.
    /// 
    /// Find the last ten digits of this prime number.
    /// </summary>
    [Result(Name = "last", Expected = "8739992577")]
    public class Problem097 : Problem
    {
        public override string Solve(string resource)
        {
            return ((28433 * PrimeMath.ModPow(2, 7830457, new BigInteger(10000000000))) % 10000000000 + 1).ToString();
        }
    }
}
