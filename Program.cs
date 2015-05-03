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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FizzBuzz2
{
    class Program
    {
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

            var upperRangeLimit = Convert.ToInt32(args[0]);
            var loopCount       = Convert.ToInt32(args[1]);

            new ConsoleOutput().Run(upperRangeLimit, loopCount);

            Console.Error.WriteLine();
            Console.Error.Write(@"Press any key to continue...");
            Console.ReadKey(true);
        }
    }

    public class ConsoleOutput : IFizzBuzz
    {
        private int _upperLimit;
        private int _maxLoops;

        public void Run(int upperLimit, int maxLoops)
        {
            _upperLimit = upperLimit;
            _maxLoops = maxLoops;
            new FizzBuzz().Run(upperLimit, maxLoops, this);
        }

        public void TestNumber(int value, string tag)
        {
            Console.WriteLine(tag);
        }

        public void ResultsStart()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(@"Results:");
        }

        public void ResultsFinish()
        {
            Console.WriteLine(@"Each test performed {0} times with max range of {1}.",
                        _maxLoops, _upperLimit);
        }

        public void ResultsItem(TimeSpan timeSpan, string testFunction)
        {
            Console.Error.WriteLine(@"{0}  {1}", timeSpan, testFunction);
        }
    }
}
