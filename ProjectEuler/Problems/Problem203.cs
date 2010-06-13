namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// The binomial coefficients nCk can be arranged in triangular form, Pascal's triangle, like this:
    ///     1	
    ///     1		1	
    ///     1		2		1	
    ///     1		3		3		1	
    ///     1		4		6		4		1	
    ///     1		5		10		10		5		1	
    ///     1		6		15		20		15		6		1	
    ///     1		7		21		35		35		21		7		1
    /// 
    /// It can be seen that the first eight rows of Pascal's triangle contain twelve distinct numbers: 1, 2, 3, 4, 5, 6, 7, 10, 15, 20, 21 and 35.
    /// 
    /// A positive integer n is called squarefree if no square of a prime divides n. Of the twelve distinct numbers in the first eight rows of Pascal's triangle, all except 4 and 20 are squarefree. The sum of the distinct squarefree numbers in the first eight rows is 105.
    /// 
    /// Find the sum of the distinct squarefree numbers in the first 51 rows of Pascal's triangle.
    /// </summary>
    [Result(Name = "sum", Expected = "34029210557338")]
    public class Problem203 : Problem
    {
        public override string Solve(string resource)
        {
            var triangle = new long[51][];
            triangle[0] = new long[] { 1 };
            for (int r = 1; r < triangle.Length; r++)
            {
                var row = new long[r + 1];
                row[0] = 1;
                row[r] = 1;

                for (int c = 1; c < r; c++)
                {
                    row[c] = triangle[r - 1][c - 1] + triangle[r - 1][c];
                }

                triangle[r] = row;
            }

            var distinct = new Dictionary<long, bool>();
            for (int r = 0; r < triangle.Length; r++)
            {
                var row = triangle[r];
                for (int c = 0; c < row.Length; c++)
                {
                    distinct[row[c]] = false;
                }
            }

            var primes = PrimeMath.GetFirstNPrimes(1000);

            long sum = 0;
            foreach (var num in distinct.Keys)
            {
                var factors = PrimeMath.Factor(num, primes);

                if (!(from f in factors
                      where f.Value >= 2
                      select f).Any())
                {
                    sum += num;
                }
            }

            return sum.ToString();
        }
    }
}
