namespace ProjectEuler
{
    using System;
    using System.Diagnostics;

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

}
