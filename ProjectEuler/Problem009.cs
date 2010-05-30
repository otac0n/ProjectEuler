namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A Pythagorean triplet is a set of three natural numbers, a  &lt; b  &lt; c, for which,
    /// a^(2) + b^(2) = c^(2)
    ///
    /// For example, 3^(2) + 4^(2) = 9 + 16 = 25 = 5^(2).
    ///
    /// There exists exactly one Pythagorean triplet for which a + b + c = 1000.
    /// Find the product abc.
    ///
    /// </summary>
    [Result(Name = "product")]
    public class Problem009 : Problem
    {
        public override string Solve(string resource)
        {
            for (var a = 1; a <= 1000; a++)
            {
                for (var b = a + 1; b <= 1000; b++)
                {
                    var c = 1000 - a - b;

                    if (a * a + b * b == c * c)
                    {
                        return (a * b * c).ToString();
                    }
                }
            }

            return "error";
        }
    }
}
