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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FizzBuzz2
{
    class Program
    {
        delegate void TestResult(int i, string tag, string testFunctionTitle);

        private static int _upperRangeLimit;
        private static int _maxLoops;
        private static readonly List<Tuple<TimeSpan, string>>
            ProcTimes = new List<Tuple<TimeSpan, string>>();


        static void Main(string[] args)
        {
            // Check for presence of command line parameters.
            if (!args.Any())
            {
                Console.WriteLine(@"Yo, hoser! What's the upper range to test?");
                return;
            }
            if (args.Count() < 2)
            {
                Console.WriteLine(@"Yo, hoser! How many iterations of each test?");
                return;
            }

            _upperRangeLimit = Convert.ToInt32(args[0]);
            _maxLoops = Convert.ToInt32(args[1]);
            Engine(TestResultInText);
            Engine(TestResultInHTML);
        }

        static void TestResultInText(int i, string tag, string testFunctionTitle)
        {
            if (i == 1)
            {
                var tag1 = String.Format(@"{0} tests:", testFunctionTitle);
                var tag2 = new String('=', tag1.Length);
                Console.WriteLine();
                Console.WriteLine(tag1);
                Console.WriteLine(tag2);
            }
            Console.WriteLine(tag);
        }

        static void TestResultInHTML(int i, string tag, string testFunctionTitle)
        {
            if (i == 1)
            {
                Console.WriteLine("<table>");
                Console.WriteLine("  <tr><td>{0}</td></tr>", testFunctionTitle);
            }
            Console.WriteLine("  <tr><td>{0}</td><td>{1}</td><tr>", i, tag);
            if (i == _upperRangeLimit)
                Console.WriteLine("</table>");
        }

        static private void Engine(TestResult testResult)
        {
            TestRunner(EricsFineSolution1,    "EricsFineSolution1", testResult);
            TestRunner(EricsFineSolution2,    "EricsFineSolution2", testResult);
            TestRunner(MildlyCleverSolution1, "MildlyCleverSolution1", testResult);
            TestRunner(MildlyCleverSolution2, "MildlyCleverSolution2", testResult);
            TestRunner(new Program().GrotesquelyOverengineeredSolution,
                        "GrotesquelyOverengineeredSolution", testResult);

            Console.WriteLine();
            Console.WriteLine();
            Console.Error.WriteLine(@"Results:");
            ProcTimes.ForEach(t => Console.Error.WriteLine(@"{0}  {1}", t.Item1, t.Item2));
            Console.Error.WriteLine(@"Each test performed {0} times with max range of {1}.",
                        _maxLoops, _upperRangeLimit);

            Console.Error.WriteLine();
            Console.Error.Write(@"Press any key to continue...");
            Console.ReadKey(true);
        }

        private static void TestRunner(Action<int, TestResult, string> testAction, string testFunctionTitle, TestResult testResult)
        {
            var start = Process.GetCurrentProcess().UserProcessorTime;
            for (var i = 0; i < _maxLoops; i++)
                testAction(_upperRangeLimit, testResult, testFunctionTitle);
            var procTime = Process.GetCurrentProcess().UserProcessorTime.Subtract(start);
            ProcTimes.Add(Tuple.Create(procTime, testFunctionTitle));
        }


        #region Eric's Fine Solutions

        private static readonly string[] Tags =
        {
            @"FizzBuzz", @"Fizz", @"Fizz", @"Fizz", @"Fizz",    // V mod 3 = 0
            @"Buzz",     @"{0}",  @"{0}",  @"{0}",  @"{0}",        // V mod 3 = 1
            @"Buzz",     @"{0}",  @"{0}",  @"{0}",  @"{0}"         // V mod 3 = 2
        };

        private static readonly byte[,] TagsIndex =
        {
            {3, 1, 1, 1, 1},        // V mod 3 = 0
            {2, 0, 0, 0, 0},        // V mod 3 = 1
            {2, 0, 0, 0, 0}         // V mod 3 = 2
        };

        private static readonly string[] Tags2 = { "{0}", "Fizz", "Buzz", "FizzBuzz" };

        private static void EricsFineSolution1(int upperRange, TestResult testResult, string testFunctionTitle)
        {
            // Index directly into the "Tags" table.
            for (var i = 1; i <= upperRange; i++)
                testResult(i, string.Format(Tags[(i % 3) * 5 + i % 5], i), testFunctionTitle);
        }

        private static void EricsFineSolution2(int upperRange, TestResult testResult, string testFunctionTitle)
        {
            // Index into the "Tags2" table via the "TagsIndex" table.
            // Inspired by a suggestion from Sean W.
            for (var i = 1; i <= upperRange; i++)
                testResult(i, string.Format(Tags2[TagsIndex[i % 3, i % 5]], i), testFunctionTitle);
        }

        #endregion

        #region A Mildly-Clever Solution

        private static readonly string[][] Fizzbuzz =
        {
            new[] { "FizzBuzz", "Fizz" },
            new[] { "Buzz", "{0}" }
        };

        private static void MildlyCleverSolution1(int upperRange, TestResult testResult, string testFunctionTitle)
        {
            // This is another alternative, suggested by Sean W.
            for (var i = 1; i <= upperRange; ++i)
            {
                Fizzbuzz[1][1] = "" + i;
                var f = (int)Math.Ceiling((i % 3) / (double) 100);
                var b = (int)Math.Ceiling((i % 5) / (double) 100);
                testResult(i, string.Format(Fizzbuzz[f][b], i), testFunctionTitle);
            }

        }

        #endregion

        #region A Modified Mildly-Clever Solution

        private static readonly string[][] Fizzbuzz2 =
        {
            new[] { @"FizzBuzz", @"Fizz" },
            new[] { @"Buzz",     @"{0}"  }
        };

        private static void MildlyCleverSolution2(int upperRange, TestResult testResult, string testFunctionTitle)
        {
            // This is another alternative, suggested by Sean W.
            for (var i = 1; i <= upperRange; ++i)
            {
                var f = (int)Math.Ceiling((i % 3) / (double)100);
                var b = (int)Math.Ceiling((i % 5) / (double)100);
                testResult(i, string.Format(Fizzbuzz2[f][b], i), testFunctionTitle);
            }

        }

        #endregion

        #region Grotesquely Over-Engineered Solution

        private readonly string[] _textArray = { "Beer!!!", "Buzz", "Fizz", "" };

        private void GrotesquelyOverengineeredSolution(int upperRange, TestResult testResult, string testFunctionTitle)
        {
            var foo = Enumerable.Range(1, upperRange);

            foreach (var i in foo)
            {
                _textArray[3] = i.ToString(CultureInfo.InvariantCulture);
                var a = GetArrayIndexes(i, upperRange);
                PrintValue(i, GetText(a), testResult, testFunctionTitle);
            }
        }

        public Tuple<int, int> GetArrayIndexes(int i, int upperRange)
        {
            var a = (int)Math.Ceiling((double)i % 3 / 100);
            var b = (int)Math.Ceiling((double)i % 5 / 100) * 2;
            return new Tuple<int, int>(a, b);
        }

        private string GetText(Tuple<int, int> a)
        {
            return _textArray[a.Item1 + a.Item2];
        }

        private void PrintValue(int i, string text, TestResult testResult, string testFunctionTitle)
        {
            testResult(i, string.Format("{0}", text), testFunctionTitle);
        }

        #endregion
    }
}
