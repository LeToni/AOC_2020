using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = "Input.txt";
            var adapters = ProcessInput(file);
            //var device = adapters.Max() + 3;

            (int jolt1, int jolt3) = CalculateTaskOne(adapters);
            Console.WriteLine($"There are {jolt1} differences of 1 jolt");
            Console.WriteLine($"There are {jolt3} differences of 3 jolt");
            Console.WriteLine($"Final Result 1: {jolt1 * jolt3}");
        }

        public static (int,int) CalculateTaskOne(long[] adapters)
        {
            int[] countDiffs = new[] { 0, 0, 0, 0 };

            if(adapters[0] > 3)
            {
                return (0, 0);
            }

            countDiffs[adapters[0]]++;
            for(int i = 0; i + 1 < adapters.Count(); i++)
            {
                var diff = adapters[i + 1] - adapters[i];

                if(diff > 3)
                {
                    break;
                }

                countDiffs[diff] = countDiffs[diff] + 1;
            }

            countDiffs[3] = countDiffs[3] + 1; // Built in adapter is always 3 higher than the highest adapter found
            return (countDiffs[1], countDiffs[3]);
        }

        private static long[] ProcessInput(string file)
        {
            return File.ReadAllLines(file).Select(s => Int64.Parse(s))
                .OrderBy(l => l)
                .ToArray();
        }
    }
}
