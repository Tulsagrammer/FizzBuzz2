/*  FizzBuzz Without A Single Conditional!!!

Eric Chevalier    14-Feb-2015

Given any number X, there are three possible values of X mod 3 and
five possible values of X mod 5. This gives 15 possible combinations
of the two. Three of these combinations indicate a number divisible
by 3 but NOT 5; two of these combinations indicate a number
divisible by 5 but NOT 3 and a single combination indicates a number
divisible by BOTH 3 and 5.

Using this phenomena, we can easily construct a Fizz Buzz program
containing NO conditionals.

*/


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FizzBuzz2
{
    internal delegate void xxx(Tuple<TimeSpan, string> pTuple);

    class Program
    {
        private static int UpperRangeLimit;
        private static int MaxLoops;
        private static readonly List<Tuple<TimeSpan, string>>
            ProcTimes = new List<Tuple<TimeSpan, string>>();


        static void Main(string[] args)
        {
            // Check for presence of command line parameters.
            if (! args.Any())
            {
                Console.WriteLine(@"Yo, hoser! What's the upper range to test?");
                return;
            }
            if (args.Count() < 2)
            {
                Console.WriteLine(@"Yo, hoser! How many iterations of each test?");
                return;
            }

            UpperRangeLimit = Convert.ToInt32(args[0]);
            MaxLoops = Convert.ToInt32(args[1]);

            TestRunner(EricsFineSolution1, "EricsFineSolution1");
            TestRunner(EricsFineSolution2, "EricsFineSolution2");
            TestRunner(MildlyCleverSolution, "MildlyCleverSolution");
            TestRunner(new Program().GrotesquelyOverengineeredSolution,
                        "GrotesquelyOverengineeredSolution");

            Console.WriteLine();
            Console.Error.WriteLine(@"Results:");

            ProcTimes.ForEach(t => Console.Error.WriteLine(@"{0}  {1}", t.Item1, t.Item2));
            Console.Error.WriteLine(@"Each test performed {0} times with max range of {1}.",
                        MaxLoops, UpperRangeLimit);

            Console.Error.WriteLine();
            Console.Error.Write(@"Press any key to continue...");
            Console.ReadKey(true);
        }

        private static void TestRunner(Action<int> testAction, string tag)
        {
            var start = Process.GetCurrentProcess().UserProcessorTime;
            for (var i = 0; i < MaxLoops; i++)
                testAction(UpperRangeLimit);
            var procTime = Process.GetCurrentProcess().UserProcessorTime.Subtract(start);
            ProcTimes.Add(Tuple.Create(procTime, tag));
        }


        #region Eric's Fine Solutions

        private static readonly string[] Tags =
        {
            @"FizzBuzz", @"Fizz", @"Fizz", @"Fizz", @"Fizz",    // V mod 3 = 0
            @"Buzz",     @"",     @"",     @"",     @"",        // V mod 3 = 1
            @"Buzz",     @"",     @"",     @"",     @""         // V mod 3 = 2
        };

        private static readonly byte[,] TagsIndex =
        {
            {3, 1, 1, 1, 1},        // V mod 3 = 0
            {2, 0, 0, 0, 0},        // V mod 3 = 1
            {2, 0, 0, 0, 0}         // V mod 3 = 2
        };

        private static readonly string[] Tags2 = { "", "Fizz", "Buzz", "FizzBuzz" };

        private static void EricsFineSolution1(int upperRange)
        {
            // Index directly into the "Tags" table.
            for (var i = 1; i <= upperRange; i++)
                Console.WriteLine("{0,3} {1}", i, Tags[(i % 3) * 5 + i % 5]);
        }

        private static void EricsFineSolution2(int upperRange)
        {
            // Index into the "Tags2" table via the "TagsIndex" table.
            // Inspired by a suggestion from Sean W.
            Console.WriteLine();
            for (var i = 1; i <= upperRange; i++)
                Console.WriteLine("{0,3} {1}", i, Tags2[TagsIndex[i % 3, i % 5]]);
        }

        #endregion

        #region A Mildly-Clever Solution

        private static string[][] fizzbuzz = new String[][]
        {
            new[] { "FizzBuzz", "Fizz" },
            new[] { "Buzz", "" },
        };

        private static void MildlyCleverSolution(int upperRange)
        {
            // This is another alternative, suggested by Sean W.
            Console.WriteLine();
            for (var i = 1; i <= upperRange; ++i)
            {
                fizzbuzz[1][1] = "" + i;
                var f = (int)Math.Ceiling((i % 3) / (double) 100);
                var b = (int)Math.Ceiling((i % 5) / (double) 100);
                Console.WriteLine(i + ": " + fizzbuzz[f][b]);
            }

        }

        #endregion

        #region Grotesquely Over-Engineered Solution

        private readonly string[] TextArray = new[] { "Beer!!!", "Buzz", "Fizz", "" };

        private void GrotesquelyOverengineeredSolution(int upperRange)
        {
            var foo = Enumerable.Range(1, upperRange);

            foreach (var i in foo)
            {
                TextArray[3] = i.ToString();
                var a = GetArrayIndexes(i, upperRange);
                PrintValue(i, GetText(a));
            };
        }

        public Tuple<int, int> GetArrayIndexes(int i, int upperRange)
        {
            var a = (int)Math.Ceiling((double)i % 3 / 100);
            var b = (int)Math.Ceiling((double)i % 5 / 100) * 2;
            return new Tuple<int, int>(a, b);
        }

        public string GetText(Tuple<int, int> a)
        {
            return TextArray[a.Item1 + a.Item2];
        }

        private void PrintValue(int value, string text)
        {
            Console.WriteLine("{0}", text);
        }

        #endregion
    }
}
