namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Combinatorics;

    /// <summary>
    /// Consider the following "magic" 3-gon ring, filled with the numbers 1 to 6, and each line adding to nine.
    /// 
    /// Working clockwise, and starting from the group of three with the numerically lowest external node (4,3,2 in this example), each solution can be described uniquely. For example, the above solution can be described by the set: 4,3,2; 6,2,1; 5,1,3.
    /// 
    /// It is possible to complete the ring with four different totals: 9, 10, 11, and 12. There are eight solutions in total.
    /// Total	Solution Set
    /// 9	4,2,3; 5,3,1; 6,1,2
    /// 9	4,3,2; 6,2,1; 5,1,3
    /// 10	2,3,5; 4,5,1; 6,1,3
    /// 10	2,5,3; 6,3,1; 4,1,5
    /// 11	1,4,6; 3,6,2; 5,2,4
    /// 11	1,6,4; 5,4,2; 3,2,6
    /// 12	1,5,6; 2,6,4; 3,4,5
    /// 12	1,6,5; 3,5,4; 2,4,6
    /// 
    /// By concatenating each group it is possible to form 9-digit strings; the maximum string for a 3-gon ring is 432621513.
    /// 
    /// Using the numbers 1 to 10, and depending on arrangements, it is possible to form 16- and 17-digit strings. What is the maximum 16-digit string for a "magic" 5-gon ring?
    /// </summary>
    [Result(Name = "max", Expected = "6531031914842725")]
    public class Problem068 : Problem
    {
        public override string Solve(string resource)
        {
            var maxStr = string.Empty;

            foreach (IList<int> combo in new Permutations<int>(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }))
            {
                if (combo[4] == 10 ||
                    combo[3] == 10 ||
                    combo[2] == 10 ||
                    combo[1] == 10 ||
                    combo[0] == 10)
                {
                    continue;
                }

                if (combo[5] > combo[6] ||
                    combo[5] > combo[7] ||
                    combo[5] > combo[8] ||
                    combo[5] > combo[9])
                {
                    continue;
                }

                var target = combo[5] + combo[0] + combo[1];
                if (combo[6] + combo[1] + combo[2] == target &&
                    combo[7] + combo[2] + combo[3] == target &&
                    combo[8] + combo[3] + combo[4] == target &&
                    combo[9] + combo[4] + combo[0] == target)
                {
                    var str = string.Empty + combo[5] + combo[0] + combo[1] + combo[6] + combo[1] + combo[2] + combo[7] + combo[2] + combo[3] + combo[8] + combo[3] + combo[4] + combo[9] + combo[4] + combo[0];
                    if (str.CompareTo(maxStr) > 0)
                    {
                        maxStr = str;
                    }
                }
            }

            return maxStr;
        }
    }
}
