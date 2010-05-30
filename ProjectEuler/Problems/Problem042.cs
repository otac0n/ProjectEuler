namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The nth term of the sequence of triangle numbers is given by, t(n) = n(n+1)/2; so the first ten triangle numbers are:
    /// 
    /// 1, 3, 6, 10, 15, 21, 28, 36, 45, 55, ...
    /// 
    /// By converting each letter in a word to a number corresponding to its alphabetical position and adding these values we form a word value. For example, the word value for SKY is 19 + 11 + 25 = 55 = t(10). If the word value is a triangle number then we shall call the word a triangle word.
    /// 
    /// Using words.txt, a 16K text file containing nearly two-thousand common English words, how many are triangle words?
    /// </summary>
    [ProblemResource("words")]
    [Result(Name = "count", Expected = "162")]
    public class Problem042 : Problem
    {
        public override string Solve(string resource)
        {
            var words = from n in resource.Split(',')
                        let word = n.Substring(1, n.Length - 2)
                        select word;

            Func<string, int> getWordValue = word =>
            {
                var sum = 0;
                foreach (var letter in word)
                {
                    sum += (letter - 'A') + 1;
                }

                return sum;
            };

            var count = 0;
            foreach (var word in words)
            {
                if (NumberTheory.IsTriangular(getWordValue(word)))
                {
                    count++;
                }
            }

            return count.ToString();
        }
    }
}
