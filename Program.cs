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
    class Program
    {
        private const int Limit = 20;

        static void Main(string[] args)
        {
            TestRunner(EricsFineSolution, Limit, "EricsFineSolution");
            TestRunner(MildlyCleverSolution, Limit, "MildlyCleverSolution");
            TestRunner(new Program().GrotesquelyOverengineeredSolution,
                        Limit, "GrotesquelyOverengineeredSolution");

            Console.WriteLine();
            Console.Write(@"Press any key to continue...");
            Console.ReadKey(true);
        }

        private static void TestRunner(Action<int> testAction, int upperRange, string tag)
        {
            var start = Process.GetCurrentProcess().UserProcessorTime;
            testAction(upperRange);
            var procTime = Process.GetCurrentProcess().UserProcessorTime.Subtract(start);
            Console.WriteLine(@"ProcTime is {0} for {1}", procTime.ToString(), tag);
        }


        #region Eric's Fine Solution

        private static readonly string[] Tags =
        {
            @"FizzBuzz",
            @"Fizz",
            @"Fizz",
            @"Fizz",
            @"Fizz",
            @"Buzz",
            @"",
            @"",
            @"",
            @"",
            @"Buzz",
            @"",
            @"",
            @"",
            @""
        };

        private static readonly byte[,] TagsIndex =
        {
            {3, 1, 1, 1, 1},        // V mod 3 = 0
            {2, 0, 0, 0, 0},        // V mod 3 = 1
            {2, 0, 0, 0, 0}         // V mod 3 = 2
        };

        private static readonly string[] Tags2 = { "", "Fizz", "Buzz", "FizzBuzz" };

        private static void EricsFineSolution(int upperRange)
        {
            // Index directly into the "Tags" table.
            for (var i = 1; i <= upperRange; i++)
                Console.WriteLine("{0,3} {1}", i, Tags[(i % 3) * 5 + i % 5]);

            // Index into the "Tags2" table via the "TagsIndex" table.
            // Inspired by a suggestion from Sean W.
            Console.WriteLine();
            for (var i = 1; i <= upperRange; i++)
                Console.WriteLine("{0,3} {1}", i, Tags2[TagsIndex[i % 3, i % 5]]);
        }

        #endregion

        #region An Over-Engineered Solution

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

        private string[] TextArray = new[] { "Beer!!!", "Buzz", "Fizz", "" };

        private void GrotesquelyOverengineeredSolution(int upperRange)
        {
            var foo = Enumerable.Range(1, upperRange);

            foreach (var i in foo)
            {
                TextArray[3] = i.ToString();
                var a = GetArrayIndexes(i, upperRange);
                //Console.WriteLine("{0}, {1}, {2}", i, a.Item1, a.Item2);
                PrintValue(i, GetText(a));
            };
        }

        public Tuple<int, int> GetArrayIndexes(int i, int upperRange)
        {
            var t1 = (double) i%3;
            var t2 = t1 / upperRange;
            var t3 = (int) Math.Ceiling(t2);
            var a = (int)Math.Ceiling((double)i % 3 / upperRange);
            var b = (int)Math.Ceiling((double)i % 5 / upperRange) * 2;
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
