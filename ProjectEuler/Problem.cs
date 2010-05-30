using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler
{
    [Result(Name = "result", Expected = "")]
    public abstract class Problem
    {
        public abstract string Solve(string resource);
    }
}
