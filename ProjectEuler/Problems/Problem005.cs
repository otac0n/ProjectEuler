namespace ProjectEuler
{
    /// <summary>
    /// 2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.
    /// 
    /// What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?
    /// </summary>
    [Result(Name = "smallest", Expected = "232792560")]
    public class Problem005 : Problem
    {
        public override string Solve(string resource)
        {
            var lessThan = 20;

            var primes = PrimeMath.GetPrimesBelow(lessThan);

            long result = 1;

            var primeIndex = 0;
            var prime = primes.Primes[primeIndex];
            var primeCount = primes.Primes.Count;
            while (true)
            {
                var max = prime;

                var next = max * prime;
                while (next <= lessThan)
                {
                    max = next;
                    next *= prime;
                }

                result *= max;

                primeIndex += 1;
                if (primeIndex >= primeCount)
                {
                    break;
                }
                
                prime = primes.Primes[primeIndex];
                if (prime >= 20)
                {
                    break;
                }
            }

            return result.ToString();
        }
    }
}
