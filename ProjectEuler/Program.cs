using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace ProjectEuler
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Func<IProblemLoader, bool> target = problem =>
            {
                return problem.ProblemName.CompareTo("Toy196") == 0;
                //return problem.ProblemName.CompareTo("Problem070") != 0 && problem.ProblemName.CompareTo("Problem104") != 0;
            };

            var loaders = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => !t.IsAbstract && typeof(Problem).IsAssignableFrom(t))
                .Select(p => typeof(ProblemLoader<>).MakeGenericType(p))
                .Select(l => (IProblemLoader)Activator.CreateInstance(l))
                .ToList();

            var sw = new Stopwatch();
            var previousElapsed = new TimeSpan();

            int targeted = 0;
            foreach (var loader in loaders)
            {
                if (!target(loader))
                {
                    continue;
                }

                targeted++;

                Console.ResetColor();

                var resource = loader.LoadResource();
                var resultInfo = loader.LoadResultInfo();
                var result = string.Empty;

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(loader.ProblemName + ": " + resultInfo.Name.PadRight(11, ' ') + " = ");

                sw.Start();
                var problem = loader.LoadProblem();
                result = problem.Solve(resource);
                sw.Stop();

                Console.ForegroundColor = string.IsNullOrEmpty(resultInfo.Expected) && !(string.IsNullOrEmpty(result) || result == "error") ? ConsoleColor.Yellow : (string.IsNullOrEmpty(result) || result == "error" || result != resultInfo.Expected ? ConsoleColor.Red : ConsoleColor.Green);
                Console.Write((string.IsNullOrEmpty(result) || result == "error" ? "(error)" : result).PadRight(28 - Math.Max(resultInfo.Name.Length, 11)));

                var delta = sw.Elapsed - previousElapsed;
                Console.ForegroundColor = delta > TimeSpan.FromSeconds(0.75) ? ConsoleColor.Red : ConsoleColor.Cyan;
                Console.WriteLine("+" + delta);
                previousElapsed = sw.Elapsed;
            }

            Console.ResetColor();
            Console.WriteLine();
            Console.Write("Solution Found in ");
            Console.ForegroundColor = sw.Elapsed > TimeSpan.FromSeconds(targeted) ? ConsoleColor.Red : ConsoleColor.Cyan;
            Console.Write(sw.Elapsed);
            Console.ResetColor();
            Console.WriteLine(".");
            Console.ReadKey(true);
        }
    }
}
