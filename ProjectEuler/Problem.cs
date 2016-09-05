namespace ProjectEuler
{
    [Result(Name = "result", Expected = "")]
    public abstract class Problem
    {
        public abstract string Solve(string resource);
    }
}
