namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
using System.Diagnostics;

    /// <summary>
    /// Consider the fraction, n/d, where n and d are positive integers. If n&ltd  and HCF(n,d)=1, it is called a reduced proper fraction.
    /// 
    /// If we list the set of reduced proper fractions for d ≤ 8 in ascending order of size, we get:
    /// 
    /// 1/8, 1/7, 1/6, 1/5, 1/4, 2/7, 1/3, 3/8, 2/5, 3/7, 1/2, 4/7, 3/5, 5/8, 2/3, 5/7, 3/4, 4/5, 5/6, 6/7, 7/8
    /// 
    /// It can be seen that 2/5 is the fraction immediately to the left of 3/7.
    /// 
    /// By listing the set of reduced proper fractions for d ≤ 1,000,000 in ascending order of size, find the numerator of the fraction immediately to the left of 3/7.
    /// </summary>
    [Result(Name = "numerator", Expected = "")]
    public class Problem071 : Problem
    {
        public override string Solve(string resource)
        {
            var max = new Fraction(3, 7);

            var min = new Fraction(0, 1);

            for (long d = 2; d <= 1000000; d++)
            {
                for (var n = min.Numerator * d / min.Denominator; n < d; n++)
                {
                    var test = new Fraction(n, d);

                    if (test.CompareTo(max) >= 0)
                    {
                        break;
                    }

                    if (test.CompareTo(min) <= 0)
                    {
                        continue;
                    }

                    min = test;
                }
            }

            return min.Numerator.ToString();
        }

        [DebuggerDisplay("{Numerator} / {Denominator}")]
        private class Fraction : IComparable<Fraction>
        {
            public Fraction(long numerator, long denominator)
            {
                this.Numerator = numerator;
                this.Denominator = denominator;
            }

            public long Numerator
            {
                get;
                private set;
            }

            public long Denominator
            {
                get;
                private set;
            }

            public int CompareTo(Fraction other)
            {
                if (other.Denominator == this.Denominator)
                {
                    return this.Numerator.CompareTo(other.Numerator);
                }

                return (this.Numerator * other.Denominator).CompareTo(other.Numerator * this.Denominator);
            }
        }
    }
}
