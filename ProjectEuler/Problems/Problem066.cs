namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Drawing;

    /// <summary>
    /// Consider quadratic Diophantine equations of the form:
    /// 
    /// x^(2) – Dy^(2) = 1
    /// 
    /// For example, when D=13, the minimal solution in x is 649^(2) – 13×180^(2) = 1.
    /// 
    /// It can be assumed that there are no solutions in positive integers when D is square.
    /// 
    /// By finding minimal solutions in x for D = {2, 3, 5, 6, 7}, we obtain the following:
    /// 
    /// 3^(2) – 2×2^(2) = 1
    /// 2^(2) – 3×1^(2) = 1
    /// 9^(2) – 5×4^(2) = 1
    /// 5^(2) – 6×2^(2) = 1
    /// 8^(2) – 7×3^(2) = 1
    /// 
    /// Hence, by considering minimal solutions in x for D ≤ 7, the largest x is obtained when D=5.
    /// 
    /// Find the value of D ≤ 1000 in minimal solutions of x for which the largest value of x is obtained.
    /// </summary>
    [Result(Name = "maximized at", Expected = "661")]
    public class Problem066 : Problem
    {
        public override string Solve(string resource)
        {
            BigInteger maxX = 0;
            var maxXD = 0;

            for (int d = 1; d <= 1000; d++)
            {
                var solution = SolveEquation(d);

                if (solution.HasValue && solution.Value.X > maxX)
                {
                    maxX = solution.Value.X;
                    maxXD = d;
                }
            }

            return maxXD.ToString();
        }

        private Solution? SolveEquation(int s)
        {
            long a_0 = 0;

            if (NumberTheory.IsSquare(s, out a_0))
            {
                return null;
            }

            long a_n = a_0;
            long m_n = 0;
            long d_n = 1;
            var prevFrac = new BigFraction(1, 0);
            var frac = new BigFraction(a_0, 1);
            while (true)
            {
                if (frac.Numerator * frac.Numerator - s * frac.Denominator * frac.Denominator == 1)
                {
                    return new Solution(frac.Numerator, frac.Denominator);
                }

                m_n = d_n * a_n - m_n;
                d_n = (s - m_n * m_n) / d_n;
                a_n = (a_0 + m_n) / d_n;

                var newFrac = new BigFraction(a_n * frac.Numerator + prevFrac.Numerator, a_n * frac.Denominator + prevFrac.Denominator);
                prevFrac = frac;
                frac = newFrac;
            }
        }

        private struct Solution
        {
            public Solution(BigInteger x, BigInteger y)
                : this()
            {
                this.X = x;
                this.Y = y;
            }

            public BigInteger X { get; private set; }

            public BigInteger Y { get; private set; }
        }
    }
}
