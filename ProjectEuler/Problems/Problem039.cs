namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// If p is the perimeter of a right angle triangle with integral length sides, {a,b,c}, there are exactly three solutions for p = 120.
    /// 
    /// {20,48,52}, {24,45,51}, {30,40,50}
    /// 
    /// For which value of p ≤ 1000, is the number of solutions maximised?
    /// </summary>
    [Result(Name = "perimeter", Expected = "840")]
    public class Problem039 : Problem
    {
        public override string Solve(string resource)
        {
            int maxSolutions = 0;
            int maxSolutionsPerimiter = 0;

            for (int p = 1; p <= 1000; p++)
            {
                int solutions = 0;
                for (int a = 1; a < p; a++)
                {
                    for (int b = a + 1; (p - a - b) > b; b++)
                    {
                        int c = p - a - b;

                        if (a * a + b * b == c * c)
                        {
                            solutions++;
                        }
                    }
                }

                if (solutions > maxSolutions)
                {
                    maxSolutions = solutions;
                    maxSolutionsPerimiter = p;
                }
            }

            return maxSolutionsPerimiter.ToString();
        }
    }
}
