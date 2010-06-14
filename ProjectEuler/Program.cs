using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using System.Numerics;
using System.Drawing;
using System.Combinatorics;

namespace ProjectEuler
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Func<IProblemLoader, bool> target = problem =>
            {
                return problem.ProblemName.CompareTo("Problem191") == 0;
                return problem.ProblemName.CompareTo("Problem104") != 0;
            };

            var loaders = new List<IProblemLoader>
            {
                new ProblemLoader<Problem001>(),
                new ProblemLoader<Problem002>(),
                new ProblemLoader<Problem003>(),
                new ProblemLoader<Problem004>(),
                new ProblemLoader<Problem005>(),
                new ProblemLoader<Problem006>(),
                new ProblemLoader<Problem007>(),
                new ProblemLoader<Problem008>(),
                new ProblemLoader<Problem009>(),
                new ProblemLoader<Problem010>(),
                new ProblemLoader<Problem011>(),
                new ProblemLoader<Problem012>(),
                new ProblemLoader<Problem013>(),
                new ProblemLoader<Problem014>(),
                new ProblemLoader<Problem015>(),
                new ProblemLoader<Problem016>(),
                new ProblemLoader<Problem017>(),
                new ProblemLoader<Problem018>(),
                new ProblemLoader<Problem019>(),
                new ProblemLoader<Problem020>(),
                new ProblemLoader<Problem021>(),
                new ProblemLoader<Problem022>(),
                new ProblemLoader<Problem023>(),
                new ProblemLoader<Problem024>(),
                new ProblemLoader<Problem025>(),
                new ProblemLoader<Problem026>(),
                new ProblemLoader<Problem027>(),
                new ProblemLoader<Problem028>(),
                new ProblemLoader<Problem029>(),
                new ProblemLoader<Problem030>(),
                new ProblemLoader<Problem031>(),
                new ProblemLoader<Problem032>(),
                new ProblemLoader<Problem033>(),
                new ProblemLoader<Problem034>(),
                new ProblemLoader<Problem035>(),
                new ProblemLoader<Problem036>(),
                new ProblemLoader<Problem037>(),
                new ProblemLoader<Problem038>(),
                new ProblemLoader<Problem039>(),
                new ProblemLoader<Problem040>(),
                new ProblemLoader<Problem041>(),
                new ProblemLoader<Problem042>(),
                new ProblemLoader<Problem043>(),
                new ProblemLoader<Problem044>(),
                new ProblemLoader<Problem045>(),
                new ProblemLoader<Problem046>(),
                new ProblemLoader<Problem047>(),
                new ProblemLoader<Problem048>(),
                new ProblemLoader<Problem049>(),
                new ProblemLoader<Problem050>(),
                new ProblemLoader<Problem051>(),
                new ProblemLoader<Problem052>(),
                new ProblemLoader<Problem053>(),
                new ProblemLoader<Problem054>(),
                new ProblemLoader<Problem055>(),
                new ProblemLoader<Problem056>(),
                new ProblemLoader<Problem057>(),
                new ProblemLoader<Problem058>(),
                new ProblemLoader<Problem059>(),

                new ProblemLoader<Problem061>(),

                new ProblemLoader<Problem063>(),

                new ProblemLoader<Problem065>(),

                new ProblemLoader<Problem067>(),

                new ProblemLoader<Problem068>(),
                new ProblemLoader<Problem069>(),

                new ProblemLoader<Problem071>(),
                new ProblemLoader<Problem072>(),
                new ProblemLoader<Problem073>(),

                new ProblemLoader<Problem076>(),

                new ProblemLoader<Problem079>(),
                new ProblemLoader<Problem080>(),
                new ProblemLoader<Problem081>(),
                new ProblemLoader<Problem082>(),

                new ProblemLoader<Problem085>(),

                new ProblemLoader<Problem089>(),

                new ProblemLoader<Problem092>(),
                
                new ProblemLoader<Problem095>(),
                new ProblemLoader<Problem096>(),
                new ProblemLoader<Problem097>(),

                new ProblemLoader<Problem099>(),
                new ProblemLoader<Problem100>(),

                new ProblemLoader<Problem102>(),

                new ProblemLoader<Problem104>(),

                new ProblemLoader<Problem112>(),

                new ProblemLoader<Problem145>(),

                new ProblemLoader<Problem164>(),

                new ProblemLoader<Problem173>(),

                new ProblemLoader<Problem187>(),

                new ProblemLoader<Problem191>(),

                new ProblemLoader<Problem203>(),
                new ProblemLoader<Problem204>(),
                new ProblemLoader<Problem205>(),
                new ProblemLoader<Problem206>(),

                new ProblemLoader<Problem231>(),

                new ProblemLoader<Problem293>(),
            };

            var sw = new Stopwatch();
            var previousElapsed = new TimeSpan();
            //Console.SetWindowSize(80, 80);

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