using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
{
    class Program
    {
        public const string N = "N";
        public const string S = "S";
        public const string E = "E";
        public const string W = "W";
        public const string L = "L";
        public const string R = "R";
        public const string F = "F";

        static void Main(string[] args)
        {
            string file = "Input_Test.txt";
            List<Coordinate> coordinates = ProcessInput(file);
            Spaceship ship = new Spaceship() { Direction = "E", PosX = 0, PosY = 0 };

            foreach(var coord in coordinates)
            {
                ship.Move(coord);
            }

            Console.WriteLine($"Hammington Distance: {ship.HammingtonDistance()}");
        }


        public static List<Coordinate> ProcessInput(string file)
        {
            return File.ReadAllLines(file)
                .Select(s => new Coordinate { Direction = s[0].ToString(), Distance = int.Parse(s[1..]) })
                .ToList();
        }
    }
}
