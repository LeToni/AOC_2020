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

            Dictionary<int, bool> SeatIds = new Dictionary<int, bool>();
            for(int i = 0; i <= 127; i++)
            {
                for(int j= 0; j <= 7; j++)
                {
                    var SeatId = i * 8 + j;
                    SeatIds.Add(SeatId, true);
                }
            }

            var max = int.MinValue;
            foreach (var boardingCode in boardingCodes)
            {
                var row = CalculateRow(boardingCode.Substring(0, 7));
                var col = CalculateCol(boardingCode.Substring(7, 3));

                var seatID = row * 8 + col;
                SeatIds.Remove(seatID);
                max = Math.Max(max, seatID);
            }

            Console.WriteLine($"Highest SeatID found: {max}");

            foreach(var seatId in SeatIds.Keys.ToList())
            {
                bool eval1 = false;
                bool eval2 = false;
                if(SeatIds.TryGetValue(seatId-1, out eval1) && SeatIds.TryGetValue(seatId + 1, out eval2))
                {
                    SeatIds[seatId - 1] = false;
                    SeatIds[seatId] = false;
                    SeatIds[seatId + 1] = false;
                }
            }

            foreach (var seatId in SeatIds.Keys.ToList())
            {
                if(SeatIds[seatId] == false)
                {
                    SeatIds.Remove(seatId);
                }
            }

            foreach (var seatId in SeatIds.Keys.ToList())
            {
                Console.WriteLine($"Your seat should be: {seatId}");
            }
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
