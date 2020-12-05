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
            var file = "Input.txt";
            var codes = ProcessInput(file);

            var seatIds = codes.Select(ConvertToBinary)
                                .Select(CalculateSeatId)
                                .OrderBy(s => s);
            var maxSeatId = seatIds.Max();
            Console.WriteLine($"Highest seatId on the list: {maxSeatId}");

            var minSeatId = seatIds.Min();
            var numberOfSeats = maxSeatId - minSeatId;

            var possibleSeats = Enumerable.Range(minSeatId, maxSeatId - minSeatId)
                                .Except(seatIds);

            Console.WriteLine($"Possible seat of yours might be following:");
            foreach (var seat in possibleSeats)
            {
                Console.WriteLine(seat);
            }
            
        }

        private static List<string> ProcessInput(string path)
        {
            return File.ReadAllLines(path).ToList();
        }

        private static string ConvertToBinary(string code)
        {
            return code.Replace('F', '0')
                .Replace('B', '1')
                .Replace('L', '0')
                .Replace('R', '1');
        }

        private static int CalculateSeatId(string code)
        {
            var row = Convert.ToInt32(code.Substring(0,code.Length-3),2);
            var col = Convert.ToInt32(code.Substring(code.Length-3),2);

            return row*8+col;
        }
    }

}
