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
            var device = adapters.Max() + 3;
            (int jolt1, int jolt3) = CalculateTaskOne(adapters);
            Console.WriteLine($"There are {jolt1} differences of 1 jolt");
            Console.WriteLine($"There are {jolt3} differences of 3 jolt");
            Console.WriteLine($"Final Result 1: {jolt1 * jolt3}");

            var possibleCombinations = CalculatePossibleCombinations(adapters, device);
            Console.WriteLine($"Possible number of combinations: {possibleCombinations}");
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

        public static long CalculatePossibleCombinations(long[] adapters, long device)
        {
            Dictionary<long, long> combinations = new Dictionary<long, long>() { { 0, 1 } };

            for(int i = 0; i <= device; i++)
            {
                if(adapters.Contains(i) || i == device)
                {
                    long j = 0;
                    if (combinations.Keys.Contains(i - 3))
                    {
                        j = j + combinations[i - 3];
                    }
                    if (combinations.Keys.Contains(i - 2))
                    {
                        j = j + combinations[i - 2];
                    }
                    if (combinations.Keys.Contains(i - 1))
                    {
                        j = j + combinations[i - 1];
                    }

                    combinations.Add(i, j);
                }
            }

            return combinations[device];
        }

        private static long[] ProcessInput(string file)
        {
            return File.ReadAllLines(file).Select(s => Int64.Parse(s))
                .OrderBy(l => l)
                .ToArray();
        }
    }
}
