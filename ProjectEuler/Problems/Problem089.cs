namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The rules for writing Roman numerals allow for many ways of writing each number (see FAQ: Roman Numerals). However, there is always a "best" way of writing a particular number.
    /// 
    /// For example, the following represent all of the legitimate ways of writing the number sixteen:
    /// 
    /// IIIIIIIIIIIIIIII
    /// VIIIIIIIIIII
    /// VVIIIIII
    /// XIIIIII
    /// VVVI
    /// XVI
    /// 
    /// The last example being considered the most efficient, as it uses the least number of numerals.
    /// 
    /// The 11K text file, roman.txt, contains one thousand numbers written in valid, but not necessarily minimal, Roman numerals; that is, they are arranged in descending units and obey the subtractive pair rule (see FAQ for the definitive rules for this problem).
    /// 
    /// Find the number of characters saved by writing each of these in their minimal form.
    /// 
    /// Note: You can assume that all the Roman numerals in the file contain no more than four consecutive identical units.
    /// </summary>
    [ProblemResource("roman")]
    [Result(Name = "count", Expected = "743")]
    public class Problem089 : Problem
    {
        public override string Solve(string resource)
        {
            var numerals = resource.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            var digits = new Dictionary<char, int>
            {
                { 'M', 1000 },
                { 'D', 500 },
                { 'C', 100 },
                { 'L', 50 },
                { 'X', 10 },
                { 'V', 5 },
                { 'I', 1 },
            };

            var remainders = new Dictionary<int, Dictionary<int, string>>
            {
                { 100, new Dictionary<int, string>
                        {
                            { 1, "C" },
                            { 2, "CC" },
                            { 3, "CCC" },
                            { 4, "CD" },
                            { 5, "D" },
                            { 6, "DC" },
                            { 7, "DCC" },
                            { 8, "DCCC" },
                            { 9, "CM" },
                        }},
                { 10, new Dictionary<int, string>
                        {
                            { 1, "X" },
                            { 2, "XX" },
                            { 3, "XXX" },
                            { 4, "XL" },
                            { 5, "L" },
                            { 6, "LX" },
                            { 7, "LXX" },
                            { 8, "LXXX" },
                            { 9, "XC" },
                        }},
                { 1, new Dictionary<int, string>
                        {
                            { 1, "I" },
                            { 2, "II" },
                            { 3, "III" },
                            { 4, "IV" },
                            { 5, "V" },
                            { 6, "VI" },
                            { 7, "VII" },
                            { 8, "VIII" },
                            { 9, "IX" },
                        }},
            };

            Func<string, int> parse = numeral =>
            {
                int value = 0;
                int minvalue = 0;
                for (int i = numeral.Length - 1; i >= 0; i--)
                {
                    var digit = digits[numeral[i]];
                    if (digit >= minvalue)
                    {
                        minvalue = digit;
                        value += digit;
                    }
                    else
                    {
                        value -= digit;
                    }
                }

                return value;
            };

            Func<int, string> toRoman = number =>
            {
                var sb = new StringBuilder();
                while (number >= 1000)
                {
                    sb.Append('M');
                    number -= 1000;
                }

                foreach (var rem in remainders.Keys)
                {
                    if (number >= rem)
                    {
                        var i = number / rem;

                        sb.Append(remainders[rem][i]);
                        number -= rem * i;
                    }
                }

                return sb.ToString();
            };

            var sum = 0;
            foreach (var numeral in numerals)
            {
                var value = parse(numeral);
                var text = toRoman(value);

                sum += numeral.Length - text.Length;
            }

            return sum.ToString();
        }
    }
}
