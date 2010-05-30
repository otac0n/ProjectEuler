namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// The Fibonacci sequence is defined by the recurrence relation:
    /// 
    ///     F_(n) = F_(n−1) + F_(n−2), where F_(1) = 1 and F_(2) = 1.
    /// 
    /// Hence the first 12 terms will be:
    /// 
    ///     F_(1) = 1
    ///     F_(2) = 1
    ///     F_(3) = 2
    ///     F_(4) = 3
    ///     F_(5) = 5
    ///     F_(6) = 8
    ///     F_(7) = 13
    ///     F_(8) = 21
    ///     F_(9) = 34
    ///     F_(10) = 55
    ///     F_(11) = 89
    ///     F_(12) = 144
    /// 
    /// The 12th term, F_(12), is the first term to contain three digits.
    /// 
    /// What is the first term in the Fibonacci sequence to contain 1000 digits?
    /// </summary>
    [Result(Name = "first", Expected = "4782")]
    public class Problem025 : Problem
    {
        public override string Solve(string resource)
        {
            BigInteger thousandDigits = 1;
            for (int i = 1; i < 1000; i++)
            {
                thousandDigits *= 10;
            }

            BigInteger nm1 = 1;
            BigInteger nm2 = 0;

            long term = 2;

            while (true)
            {
                BigInteger n = nm1 + nm2;

                if (n >= thousandDigits)
                {
                    return term.ToString();
                }

                term++;
                nm2 = nm1;
                nm1 = n;
            }
        }
    }
}
