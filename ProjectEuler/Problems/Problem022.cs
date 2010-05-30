namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Using names.txt, a 46K text file containing over five-thousand first names, begin by sorting it into alphabetical order. Then working out the alphabetical value for each name, multiply this value by its alphabetical position in the list to obtain a name score.
    /// 
    /// For example, when the list is sorted into alphabetical order, COLIN, which is worth 3 + 15 + 12 + 9 + 14 = 53, is the 938th name in the list. So, COLIN would obtain a score of 938 × 53 = 49714.
    /// 
    /// What is the total of all the name scores in the file?
    /// </summary>
    [ProblemResource("names")]
    [Result(Name = "total", Expected = "871198282")]
    public class Problem022 : Problem
    {
        public override string Solve(string resource)
        {
            var names = from n in resource.Split(',')
                        let name = n.Substring(1, n.Length - 2)
                        orderby name
                        select name;

            long total = 0;

            var offset = 'A' - 1;
            int i = 1;
            foreach (var name in names)
            {
                var sum = 0;
                foreach (var ch in name)
                {
                    sum += ch - offset;
                }

                total += i * sum;
                i++;
            }

            return total.ToString();
        }
    }
}
