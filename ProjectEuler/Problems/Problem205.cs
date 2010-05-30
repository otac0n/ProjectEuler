namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Combinatorics;

    /// <summary>
    /// Peter has nine four-sided (pyramidal) dice, each with faces numbered 1, 2, 3, 4.
    /// Colin has six six-sided (cubic) dice, each with faces numbered 1, 2, 3, 4, 5, 6.
    /// 
    /// Peter and Colin roll their dice and compare totals: the highest total wins. The result is a draw if the totals are equal.
    /// 
    /// What is the probability that Pyramidal Pete beats Cubic Colin? Give your answer rounded to seven decimal places in the form 0.abcdefg
    /// </summary>
    [Result(Name = "probability", Expected = "0.5731441")]
    public class Problem205 : Problem
    {
        public override string Solve(string resource)
        {
            long pete = 0;
            long total = 0;

            var cSums = new Dictionary<int, long>();

            foreach (var cDice in new Variations<int>(new List<int> { 1, 2, 3, 4, 5, 6 }, 6, GenerateOption.WithRepetition))
            {
                var cSum = cDice.Sum();

                if (!cSums.ContainsKey(cSum))
                {
                    cSums[cSum] = 0;
                }

                cSums[cSum]++;
            }

            var pSums = new Dictionary<int, long>();

            foreach (var pDice in new Variations<int>(new List<int> { 1, 2, 3, 4 }, 9, GenerateOption.WithRepetition))
            {
                var pSum = pDice.Sum();

                if (!pSums.ContainsKey(pSum))
                {
                    pSums[pSum] = 0;
                }

                pSums[pSum]++;
            }

            foreach (var cSum in cSums.Keys)
            {
                var cRolls = cSums[cSum];
                foreach (var pSum in pSums.Keys)
                {
                    var pRolls = pSums[pSum];

                    var rolls = cRolls * pRolls;
                    total += rolls;

                    if (pSum > cSum)
                    {
                        pete += rolls;
                    }
                }
            }

            decimal peteRatio = pete;
            peteRatio /= total;
            return Math.Round(peteRatio, 7).ToString();
        }
    }
}
