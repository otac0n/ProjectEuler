﻿namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Triangle, square, pentagonal, hexagonal, heptagonal, and octagonal numbers are all figurate (polygonal) numbers and are generated by the following formulae:
    /// Triangle 	  	P_(3,n)=n(n+1)/2 	  	1, 3, 6, 10, 15, ...
    /// Square 	  	P_(4,n)=n^(2) 	  	1, 4, 9, 16, 25, ...
    /// Pentagonal 	  	P_(5,n)=n(3n−1)/2 	  	1, 5, 12, 22, 35, ...
    /// Hexagonal 	  	P_(6,n)=n(2n−1) 	  	1, 6, 15, 28, 45, ...
    /// Heptagonal 	  	P_(7,n)=n(5n−3)/2 	  	1, 7, 18, 34, 55, ...
    /// Octagonal 	  	P_(8,n)=n(3n−2) 	  	1, 8, 21, 40, 65, ...
    /// 
    /// The ordered set of three 4-digit numbers: 8128, 2882, 8281, has three interesting properties.
    /// 
    ///    1. The set is cyclic, in that the last two digits of each number is the first two digits of the next number (including the last number with the first).
    ///    2. Each polygonal type: triangle (P_(3,127)=8128), square (P_(4,91)=8281), and pentagonal (P_(5,44)=2882), is represented by a different number in the set.
    ///    3. This is the only set of 4-digit numbers with this property.
    /// 
    /// Find the sum of the only ordered set of six cyclic 4-digit numbers for which each polygonal type: triangle, square, pentagonal, hexagonal, heptagonal, and octagonal, is represented by a different number in the set.
    /// </summary>
    [Result(Name = "sum", Expected = "")]
    public class Problem061 : Problem
    {
        public override string Solve(string resource)
        {
            var minAgon = 3;
            var maxAgon = 8;

            var collation = new Dictionary<int, List<int>>[maxAgon - minAgon + 1];

            for (int i = minAgon; i <= maxAgon; i++)
            {
                collation[i - minAgon] = new Dictionary<int, List<int>>();

                for (int n = 1; ; n++)
                {
                    var num = n * ((i - 2) * n - (i - 4)) / 2;

                    if (num >= 10000)
                    {
                        break;
                    }

                    if (num >= 1000)
                    {
                        var key = num / 100;

                        if (!collation[i - minAgon].ContainsKey(key))
                        {
                            collation[i - minAgon][key] = new List<int>();
                        }

                        collation[i - minAgon][key].Add(num);
                    }
                }
            }

            int cFlag = (1 << (maxAgon - minAgon + 1)) - 1;

            Func<int, int, int, List<int>> lookup = null;
            lookup = (prefix, postfix, flags) =>
            {
                var nFlag = 1;
                for (int n = minAgon; n <= maxAgon; n++, nFlag <<= 1)
                {
                    if ((flags & nFlag) == nFlag)
                    {
                        var candidates = collation[n - minAgon];

                        if (!candidates.ContainsKey(prefix))
                        {
                            continue;
                        }

                        var newFlag = (flags & ~nFlag);

                        if (newFlag == 0)
                        {
                            foreach (var c in candidates[prefix])
                            {
                                if (c % 100 == postfix)
                                {
                                    var result = new List<int>();
                                    result.Add(candidates[prefix][0]);
                                    return result;
                                }
                            }
                        }
                        else
                        {
                            foreach (var c in candidates[prefix])
                            {
                                var result = lookup(c % 100, postfix, newFlag);

                                if (result != null)
                                {
                                    result.Add(c);
                                    return result;
                                }
                            }
                        }
                    }
                }

                return null;
            };

            for (int i = 10; i <= 99; i++)
            {
                var q = lookup(i, i, cFlag);

                if (q != null)
                {
                    return q.Sum().ToString();
                }
            }

            return "error";
        }
    }
}
