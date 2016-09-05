namespace ProjectEuler
{
    using System;
    using System.Numerics;
    using System.Threading;

    /// <summary>
    ///
    /// </summary>
    [Result(Name = "result", Expected = "")]
    public class Toy196 : Problem
    {
        public override string Solve(string resource)
        {
            BigInteger current = 196;

            var source = new CancellationTokenSource();
            source.CancelAfter(TimeSpan.FromMinutes(1));
            var token = source.Token;

            var iteration = 0;
            while (!token.IsCancellationRequested)
            {
                iteration += 1;

                var reverse = current.Reverse(10);
                if (reverse == current) return $"yes@{iteration}={current}";
                current += reverse;
            }

            return $"unknown@{iteration}";
        }
    }
}
