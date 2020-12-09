using System;
using System.IO;
using System.Linq;

namespace Day09
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "Input.txt";
            long[] numbers = ProcessInput(file);

            var invalidNumber = Filter(numbers, 25);
            Console.WriteLine($"First number that does not have property: {invalidNumber}");

            var sum = FilterForInvalidNumber(numbers, invalidNumber);
            Console.WriteLine($"Encrypten weakness in list of numbers: {sum}");
        }

        public static long FilterForInvalidNumber(long[] numbers, long invalidNumber)
        {
            int min = 0;
            int max = 1;

            while(max < numbers.Count())
            {
                var sum = numbers[min..max].Sum();

                if(sum == invalidNumber)
                {
                    return numbers[min..max].Min() + numbers[min..max].Max();
                }

                if(sum < invalidNumber)
                {
                    max = max + 1;
                }
                else
                {
                    min = min + 1;
                }
            }
            return 0;
        }

        public static long Filter(long[] numbers, int intervalSize)
        {
            int min = 0;
            int max = intervalSize;

            for (int i = 0; intervalSize + i < numbers.Count(); i++)
            {
                long checkInt = numbers[intervalSize + i];
                
                var isSumOfPreamble = IsSumOfPreamble(numbers[min..max], checkInt);

                if (!isSumOfPreamble)
                    return checkInt;
                max = max + 1;
                min = min + 1;
                
            }

            return -1;
        }

        public static bool IsSumOfPreamble(long[] numbers, long target)
        {
            for(int i = 0; i < numbers.Count(); i++)
            {
                for(int j = i+1; j < numbers.Count(); j++)
                {
                    var sum = numbers[i] + numbers[j];

                    if (sum.Equals(target))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static long[] ProcessInput(string file)
        {
            return File.ReadAllLines(file)
                .Select(s => Int64.Parse(s)).ToArray();
        }
    }
}
