using System;
using System.Collections.Generic;
using System.IO;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = "./Map.txt";
            var map = ProcessInput(file);

            var treesOnPath = EncounteredTreesOnSlope(map, 3, 1);

            Console.WriteLine("Part 1: ");
            Console.WriteLine($"Encountered trees on the way: {treesOnPath}");


            List<Tuple<int, int>> slopes = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(1,1),
                new Tuple<int, int>(3,1),
                new Tuple<int, int>(5,1),
                new Tuple<int, int>(7,1),
                new Tuple<int, int>(1,2),
            };

            long treeProduct = 1;
            foreach(var slope in slopes)
            {
                treeProduct = treeProduct * EncounteredTreesOnSlope(map, slope.Item1, slope.Item2);
            }

            Console.WriteLine("Part 2: ");
            Console.WriteLine($"Product of encountered trees: {treeProduct}");
        }

        private static string[] ProcessInput(string file)
        {
            return File.ReadAllLines(file);
        }

        public static long EncounteredTreesOnSlope(string[] map, int slopeX, int downY)
        {
            var trees = 0;
            var posX = 0;

            for(int posY = downY; posY < map.Length; posY = posY + downY)
            {
                var area = map[posY];
                posX = posX + slopeX;

                if(posX > area.Length - 1)
                {
                    posX = posX - area.Length;
                }
                

                if (area[posX] == '#')
                {
                    trees++;
                }
            }

            return trees;
        }
    }
}
