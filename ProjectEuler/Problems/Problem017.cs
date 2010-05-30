namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// If the numbers 1 to 5 are written out in words: one, two, three, four, five, then there are 3 + 3 + 5 + 4 + 4 = 19 letters used in total.
    /// 
    /// If all the numbers from 1 to 1000 (one thousand) inclusive were written out in words, how many letters would be used? 
    /// </summary>
    [Result(Name = "letters", Expected = "21124")]
    public class Problem017 : Problem
    {
        public override string Solve(string resource)
        {
            var ones = new Dictionary<int, string>
            {
                { 1, "one" },
                { 2, "two" },
                { 3, "three" },
                { 4, "four" },
                { 5, "five" },
                { 6, "six" },
                { 7, "seven" },
                { 8, "eight" },
                { 9, "nine" },
            };

            var teens = new Dictionary<int, string>
            {
                { 0, "ten" },
                { 1, "eleven" },
                { 2, "twelve" },
                { 3, "thirteen" },
                { 4, "fourteen" },
                { 5, "fifteen" },
                { 6, "sixteen" },
                { 7, "seventeen" },
                { 8, "eighteen" },
                { 9, "nineteen" },
            };

            var tens = new Dictionary<int, string>
            {
                { 2, "twenty" },
                { 3, "thirty" },
                { 4, "forty" },
                { 5, "fifty" },
                { 6, "sixty" },
                { 7, "seventy" },
                { 8, "eighty" },
                { 9, "ninety" },
            };

            var hundred = "hundred";

            var thousand = "thousand";

            Func<int, string> getName = (int num) =>
            {
                var result = new StringBuilder();

                if (num >= 1000)
                {
                    var thousandsDigit = num / 1000;

                    result.Append(ones[thousandsDigit]);
                    result.Append(thousand);
                    num %= 1000;

                    if (num == 0)
                    {
                        return result.ToString();
                    }
                }

                if (num >= 100)
                {
                    var hundredsDigit = num / 100;

                    result.Append(ones[hundredsDigit]);
                    result.Append(hundred);
                    num %= 100;

                    if (num == 0)
                    {
                        return result.ToString();
                    }
                    else
                    {
                        result.Append("and");
                    }
                }

                var tensDigit = num / 10;
                var onesDigit = num % 10;

                if (tensDigit == 0)
                {
                    result.Append(ones[onesDigit]);
                }
                else if (tensDigit == 1)
                {
                    result.Append(teens[onesDigit]);
                }
                else
                {
                    result.Append(tens[tensDigit]);

                    if (onesDigit != 0)
                    {
                        result.Append(ones[onesDigit]);
                    }
                }

                return result.ToString();
            };

            var sum = 0;
            for (int i = 1; i <= 1000; i++)
            {
                sum += getName(i).Length;
            }

            return sum.ToString();
        }
    }
}
