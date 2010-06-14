namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Drawing;

    /// <summary>
    /// A particular school offers cash rewards to children with good attendance and punctuality. If they are absent for three consecutive days or late on more than one occasion then they forfeit their prize.
    /// 
    /// During an n-day period a trinary string is formed for each child consisting of L's (late), O's (on time), and A's (absent).
    /// 
    /// Although there are eighty-one trinary strings for a 4-day period that can be formed, exactly forty-three strings would lead to a prize:
    /// 
    /// OOOO OOOA OOOL OOAO OOAA OOAL OOLO OOLA OAOO OAOA
    /// OAOL OAAO OAAL OALO OALA OLOO OLOA OLAO OLAA AOOO
    /// AOOA AOOL AOAO AOAA AOAL AOLO AOLA AAOO AAOA AAOL
    /// AALO AALA ALOO ALOA ALAO ALAA LOOO LOOA LOAO LOAA
    /// LAOO LAOA LAAO
    /// 
    /// How many "prize" strings exist over a 30-day period?
    /// </summary>
    [Result(Name = "count", Expected = "1918080160")]
    public class Problem191 : Problem
    {
        public override string Solve(string resource)
        {
            var table = new Dictionary<Point, long>[2];
            table[0] = new Dictionary<Point,long>();
            table[1] = new Dictionary<Point,long>();

            Func<int, int, int, long> lookup = null;
            lookup = (lates, absents, daysLeft) =>
            {
                if (daysLeft == 1)
                {
                    return 1 + (lates < 1 ? 1 : 0) + (absents < 2 ? 1 : 0);
                }
                else
                {
                    var key = new Point(absents, daysLeft);

                    if (!table[lates].ContainsKey(key))
                    {
                        long c = lookup(lates, 0, daysLeft - 1);

                        if (absents < 2)
                        {
                            c += lookup(lates, absents + 1, daysLeft - 1);
                        }

                        if (lates < 1)
                        {
                            c += lookup(lates + 1, 0, daysLeft - 1);
                        }

                        table[lates][key] = c;
                    }

                    return table[lates][key];
                }
            };

            var count = lookup(0, 0, 30);

            return count.ToString();
        }
    }
}
