namespace ProjectEuler
{
    /// <summary>
    /// By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.
    /// 
    /// What is the 10001st prime number?
    /// </summary>
    [Result(Name = "10001st", Expected = "104743")]
    public class Problem007 : Problem
    {
        public override string Solve(string resource)
        {
            var n = 10001;
            var primes = PrimeMath.GetFirstNPrimes(n);
            return primes.Primes[n - 1].ToString();
        }
    }
}
