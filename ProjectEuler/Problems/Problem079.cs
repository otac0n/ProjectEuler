namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Combinatorics;

    /// <summary>
    /// A common security method used for online banking is to ask the user for three random characters from a passcode. For example, if the passcode was 531278, they may ask for the 2nd, 3rd, and 5th characters; the expected reply would be: 317.
    /// 
    /// The text file, keylog.txt, contains fifty successful login attempts.
    /// 
    /// Given that the three characters are always asked for in order, analyse the file so as to determine the shortest possible secret passcode of unknown length.
    /// </summary>
    [ProblemResource("keylog")]
    [Result(Name = "pin", Expected = "73162890")]
    public class Problem079 : Problem
    {
        public override string Solve(string resource)
        {
            var entries = resource.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();

            var characters = (from e in entries
                              select e).SelectMany(e => e.ToCharArray()).Distinct().OrderBy(e => e).ToList();

            Func<string, string, bool> isMatch = (pin, entry) =>
            {
                var start = 0;
                foreach (var pos in entry)
                {
                    var next = pin.IndexOf(pos, start);
                    if (next < 0)
                    {
                        return false;
                    }

                    start = next + 1;
                }

                return true;
            };

            for (int i = characters.Count; ; i++)
            {
                foreach (var comb in new Variations<char>(characters, i, GenerateOption.WithRepetition))
                {
                    var pin = new string(comb.ToArray());

                    var found = true;
                    foreach (var entry in entries)
                    {
                        if (!isMatch(pin, entry))
                        {
                            found = false;
                            break;
                        }
                    }

                    if (found)
                    {
                        return pin;
                    }
                }
            }
        }
    }
}
