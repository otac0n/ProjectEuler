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

        public override int GetHashCode()
        {
            return unchecked((int)this.Numerator ^ (int)this.Denominator);
        }

        public bool Equals(Fraction other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }

            return this.CompareTo(other) == 0;
        }

        public override bool Equals(object other)
        {
            return this.Equals(other as Fraction);
        }

        public static bool operator ==(Fraction lhs, Fraction rhs)
        {
            if (object.ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            return !object.ReferenceEquals(lhs, null) && lhs.Equals(rhs);
        }

        public static bool operator !=(Fraction lhs, Fraction rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator >=(Fraction lhs, Fraction rhs)
        {
            if (object.ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            return lhs.CompareTo(rhs) >= 0;
        }

        public static bool operator <=(Fraction lhs, Fraction rhs)
        {
            if (object.ReferenceEquals(lhs, rhs))
            {
                return true;
            }

            return lhs.CompareTo(rhs) <= 0;
        }

        public static bool operator >(Fraction lhs, Fraction rhs)
        {
            return !(lhs <= rhs);
        }

        public static bool operator <(Fraction lhs, Fraction rhs)
        {
            return !(lhs >= rhs);
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
