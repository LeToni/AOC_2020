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

            var treesOnPath = EncounteredTreesOnPath(map, 1, 3);

            Console.WriteLine("Part 1: ");
            Console.WriteLine($"Encountered trees on the way: {treesOnPath}");

            var treesOnPath2 = EncounteredTreesOnPath(map, 1,1)
                * EncounteredTreesOnPath(map, 1, 3)
                * EncounteredTreesOnPath(map, 1, 5)
                * EncounteredTreesOnPath(map, 1, 7)
                * EncounteredTreesOnPath(map, 2, 1);
            Console.WriteLine("Part 2: ");
            Console.WriteLine($"Product of encounter trees: {treesOnPath2}");
        }

        private static string[] ProcessInput(string file)
        {
            return File.ReadAllLines(file);
        }

        public static long EncounteredTreesOnPath(string[] map, int downY, int rightX)
        {
            var trees = 0;
            int posX = 0;

            for(int posY = downY; posY < map.Length; posY = posY + downY)
            {
                var area = map[posY];
                posX = posX + rightX;

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
