namespace ProjectEuler
{
    using System;
    using System.Diagnostics;
    using System.Numerics;

    [DebuggerDisplay("{Numerator} / {Denominator}")]
    public class Fraction : IComparable<Fraction>
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

    [DebuggerDisplay("{Numerator} / {Denominator}")]
    public class BigFraction : IComparable<BigFraction>
    {
        public BigFraction(BigInteger numerator, BigInteger denominator)
        {
            this.Numerator = numerator;
            this.Denominator = denominator;
        }

        public BigInteger Numerator
        {
            get;
            private set;
        }

        public BigInteger Denominator
        {
            get;
            private set;
        }

        public int CompareTo(BigFraction other)
        {
            if (other.Denominator == this.Denominator)
            {
                return this.Numerator.CompareTo(other.Numerator);
            }

            return (this.Numerator * other.Denominator).CompareTo(other.Numerator * this.Denominator);
        }
    }
}
