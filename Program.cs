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

        private static readonly string[] Tags2 = {"", "Fizz", "Buzz", "FizzBuzz"};


        static void Main(string[] args)
        {
            // Index directly into the "Tags" table.
            for (var i = 1; i <= 100; i++)
                Console.WriteLine("{0,3} {1}", i, Tags[(i % 3) * 5 + i % 5]);

            // Index into the "Tags2" table via the "TagsIndex" table.
            // Inspired by a suggestion from Sean W.
            Console.WriteLine();
            for (var i = 1; i <= 100; i++)
                Console.WriteLine("{0,3} {1}", i, Tags2[TagsIndex[i % 3, i % 5]]);

            Console.WriteLine();
            Console.Write(@"Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
