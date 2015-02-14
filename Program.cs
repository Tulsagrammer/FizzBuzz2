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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz2
{
    class Program
    {
        private static readonly String[] Tags =
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

        static void Main(string[] args)
        {
            for (var i = 1; i <= 100; i++)
            {
                Console.WriteLine("{0,3} {1}", i, Tags[(i % 3) * 5 + i % 5]);
            }

            Console.WriteLine();
            Console.Write(@"Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
