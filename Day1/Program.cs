using System;
using System.Linq;
using System.IO;

namespace Day1
{
    class Program
    {
        private const int expenseLimit = 2020;

        static void Main(string[] args)
        {
            var filePath = "./PuzzleInput.txt";
            var processedInput = ProcessFile(filePath);


            var report = new ExpenseReport();
            report.Calculate(processedInput, expenseLimit);
            report.DisplayStatistics();

            var report_extended = new ExtendedExpenseReport();
            report_extended.Calculate(processedInput, expenseLimit);
            report_extended.DisplayStatistics();
        }

        private static int[] ProcessFile(string path)
        {
            return File.ReadAllLines(path)
                    .Select( n => Int32.Parse(n)).ToArray<int>();
        }
    } 
}
