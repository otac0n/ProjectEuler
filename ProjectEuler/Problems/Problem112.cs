namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Working from left-to-right if no digit is exceeded by the digit to its left it is called an increasing number; for example, 134468.
    /// 
    /// Similarly if no digit is exceeded by the digit to its right it is called a decreasing number; for example, 66420.
    /// 
    /// We shall call a positive integer that is neither increasing nor decreasing a "bouncy" number; for example, 155349.
    /// 
    /// Clearly there cannot be any bouncy numbers below one-hundred, but just over half of the numbers below one-thousand (525) are bouncy. In fact, the least number for which the proportion of bouncy numbers first reaches 50% is 538.
    /// 
    /// Surprisingly, bouncy numbers become more and more common and by the time we reach 21780 the proportion of bouncy numbers is equal to 90%.
    /// 
    /// Find the least number for which the proportion of bouncy numbers is exactly 99%.
    /// </summary>
    [Result(Name = "result", Expected = "")]
    public class Problem112 : Problem
    {
        public override string Solve(string resource)
        {
            Func<int, bool> isBouncy = n =>
            {
                if (n < 100)
                {
                    return false;
                }

                var nStr = n.ToString();

                var diff = nStr[0] - nStr[1];
                var sign = diff == 0 ? 0 : (diff > 0 ? 1 : -1);

                for (int i = 2; i < nStr.Length; i++)
                {
                    diff = nStr[i - 1] - nStr[i];

                    if (sign == 0)
                    {
                        sign = diff;
                        continue;
                    }
                    else if (diff == 0)
                    {
                        continue;
                    }
                    else if ((diff > 0 ? 1 : -1) != sign)
                    {
                        return true;
                    }
                }

                return false;
            };

            if (isBouncy(111)) throw new Exception();
            if (isBouncy(122)) throw new Exception();
            if (isBouncy(1123)) throw new Exception();
            if (isBouncy(33211)) throw new Exception();
            if (!isBouncy(121)) throw new Exception();
            if (!isBouncy(152)) throw new Exception();
            if (!isBouncy(989)) throw new Exception();
            if (!isBouncy(76549)) throw new Exception();

            var bouncy = 0;

            for (int i = 1; ; i++)
            {
                if (isBouncy(i))
                {
                    bouncy++;
                }

                if (bouncy * 100 == i * 90)
                {
                    return i.ToString();
                }
            }
        }
    }
}
