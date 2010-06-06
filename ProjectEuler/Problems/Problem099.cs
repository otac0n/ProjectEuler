namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Comparing two numbers written in index form like 2^(11) and 3^(7) is not difficult, as any calculator would confirm that 2^(11) = 2048 &lt; 3^(7) = 2187.
    /// 
    /// However, confirming that 632382^(518061) > 519432^(525806) would be much more difficult, as both numbers contain over three million digits.
    /// 
    /// Using base_exp.txt, a 22K text file containing one thousand lines with a base/exponent pair on each line, determine which line number has the greatest numerical value.
    /// </summary>
    [ProblemResource("base_exp")]
    [Result(Name = "line", Expected = "")]
    public class Problem099 : Problem
    {
        public override string Solve(string resource)
        {
            var pairs = from l in resource.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                        let v = l.Split(',')
                        select new
                        {
                            Base = int.Parse(v[0]),
                            Exponent = int.Parse(v[1]),
                        };

            var maxN = 0.0;
            var maxNLine = 0;

            var line = 0;
            foreach (var p in pairs)
            {
                line++;

                var n = p.Exponent * Math.Log10(p.Base);

                if (n > maxN)
                {
                    maxN = n;
                    maxNLine = line;
                }
            }

            return maxNLine.ToString();
        }
    }
}
