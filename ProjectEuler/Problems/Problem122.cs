namespace ProjectEuler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using System.Diagnostics;

    /// <summary>
    /// The most naive way of computing n^(15) requires fourteen multiplications:
    ////
    ////n × n × ... × n = n^(15)
    ////
    ////But using a "binary" method you can compute it in six multiplications:
    ////
    ////n × n = n^(2)
    ////n^(2) × n^(2) = n^(4)
    ////n^(4) × n^(4) = n^(8)
    ////n^(8) × n^(4) = n^(12)
    ////n^(12) × n^(2) = n^(14)
    ////n^(14) × n = n^(15)
    ////
    ////However it is yet possible to compute it in only five multiplications:
    ////
    ////n × n = n^(2)
    ////n^(2) × n = n^(3)
    ////n^(3) × n^(3) = n^(6)
    ////n^(6) × n^(6) = n^(12)
    ////n^(12) × n^(3) = n^(15)
    ////
    ////We shall define m(k) to be the minimum number of multiplications to compute n^(k); for example m(15) = 5.
    ////
    ////For 1 ≤ k ≤ 200, find ∑ m(k).
    /// </summary>
    [ProblemResource("")]
    [Result(Name = "result", Expected = "")]
    public class Problem122 : Problem
    {
        public override string Solve(string resource)
        {
            var cache = new Dictionary<string, int?>();

            Func<int, int[], int?, int?> lookup = null;
            lookup = (target, available, minFound) =>
            {
                if (minFound.HasValue && available.Length >= minFound)
                {
                    return null;
                }

                StringBuilder keyBuilder = new StringBuilder();
                keyBuilder.Append(target);

                var newAvailable = new int[available.Length + 1];
                for (int i = 0; i < available.Length; i++)
                {
                    var val = available[i];

                    if (val == target)
                    {
                        return i;
                    }

                    newAvailable[i] = val;

                    keyBuilder.Append("." + val);
                }

                var key = keyBuilder.ToString();

                if (!cache.ContainsKey(key))
                {
                    var lowest = available[available.Length - 1];

                    int? min = null;

                    for (int i = 0; i < available.Length; i++)
                    {
                        for (int j = i; j < available.Length; j++)
                        {
                            var newValue = available[i] + available[j];

                            if (newValue <= lowest)
                            {
                                continue;
                            }
                            else if (newValue == target)
                            {
                                return available.Length;
                            }
                            else if (newValue > target)
                            {
                                break;
                            }

                            newAvailable[available.Length] = newValue;

                            var newMin = lookup(target, newAvailable, min ?? minFound);

                            if (newMin.HasValue && (!min.HasValue || newMin.Value < min.Value))
                            {
                                min = newMin;
                            }
                        }
                    }

                    cache[key] = min;
                }
                
                return cache[key];
            };

            var sum = 0;

            for (int i = 1; i <= 200; i++)
            {
                var val = lookup(i, new[] { 1 }, null).Value;
                cache.Clear();

                sum += val;
            }

            return sum.ToString();
        }
    }
}
