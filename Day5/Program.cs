using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = "Input.txt";
            var boardingCodes = ProcessInput(path);

            var max = int.MinValue;
            foreach(var boardingCode  in boardingCodes)
            {
                var row = CalculateRow(boardingCode.Substring(0, 7));
                var col = CalculateCol(boardingCode.Substring(7, 3));

                var seatID = row * 8 + col;
                max = Math.Max(max, seatID);
            }

            Console.WriteLine($"Highest SeatID found: {max}");
        }

        private static List<string> ProcessInput(string file)
        {
            var input = File.ReadAllLines(file).ToList();

            return input;
        }

        private static int CalculateRow(string rowCode, int l = 0, int h = 127)
        {
            foreach(var letter in rowCode)
            {
                Partition(letter, ref h, ref l);
            }

            return Math.Min(l, h);
        }

        private static int CalculateCol(string colCode, int l = 0, int h = 7)
        {
            foreach (var letter in colCode)
            {
                Partition(letter, ref h, ref l);
            }

            return Math.Min(l, h);
        }

        private static void Partition(char letter, ref int higher, ref int lower)
        {
            var half = (higher + lower + 1) / 2;
            if (letter == 'F' || letter == 'L')
            {
                higher = half - 1;
            }
            else
            {
                lower = half;
            }
         }
    }
}
