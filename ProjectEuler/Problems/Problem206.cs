namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Find the unique positive integer whose square has the form 1_2_3_4_5_6_7_8_9_0, where each “_” is a single digit.
    /// </summary>
    [Result(Name = "value", Expected = "1389019170")]
    public class Problem206 : Problem
    {
        public override string Solve(string resource)
        {
            var lower = (long)Math.Sqrt(1020304050607080900);
            var upper = (long)Math.Sqrt(1929394959697989990);

            for (var i = lower; i < upper; i++)
            {
                var product = (long)i * i;

                var digit = product % 10;
                if (digit != 0)
                {
                    continue;
                }

                product /= 100;
                digit = product % 10;
                if (digit != 9)
                {
                    continue;
                }

                product /= 100;
                digit = product % 10;
                if (digit != 8)
                {
                    continue;
                }

                product /= 100;
                digit = product % 10;
                if (digit != 7)
                {
                    continue;
                }

                product /= 100;
                digit = product % 10;
                if (digit != 6)
                {
                    continue;
                }

                product /= 100;
                digit = product % 10;
                if (digit != 5)
                {
                    continue;
                }

                product /= 100;
                digit = product % 10;
                if (digit != 4)
                {
                    continue;
                }

                product /= 100;
                digit = product % 10;
                if (digit != 3)
                {
                    continue;
                }

                product /= 100;
                digit = product % 10;
                if (digit != 2)
                {
                    continue;
                }

                product /= 100;
                digit = product % 10;
                if (digit != 1)
                {
                    continue;
                }

                return i.ToString();
            }

            return "error";
        }
    }
}
