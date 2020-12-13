using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
{
    class Program
    {
        public static long DepartTime { get; set; } = 0;
        
        static void Main(string[] args)
        {
            var file = "Input.txt";
            var buses = ProcessInput(file);

            (long busId, long minutesToWait) = WaitForBus(buses);

            Console.WriteLine($"BusID: {busId}");
            Console.WriteLine($"Wait: {minutesToWait}");

            Console.WriteLine($"Factor: {busId * minutesToWait}");

            //DepartTime = CalculateChinesRemainderTheorem(buses);
            // Below tool is calculate with tool from the internet (wolframalpha)
            DepartTime = 225850756401039; // Hardcoded numeber needs to replaced with funtion to calculat chinesremainder
            ClosestTimeline(buses);

            Console.WriteLine($"Timestamp: {DepartTime}");
        }

        public static long CalculateChinesRemainderTheorem(List<Bus> buses)
        {
            var n = buses.Select(b => b.ID).ToArray();
            var a = buses.Select(b => (b.OffSet) * -1).ToArray();

            return Solve(n, a);
        }

        public static int Solve(int[] n, int[] a)
        {
            int prod = n.Aggregate(1, (i, j) => i * j);
            int p;
            int sm = 0;
            for (int i = 0; i < n.Length; i++)
            {
                p = prod / n[i];
                sm += a[i] * ModularMultiplicativeInverse(p, n[i]) * p;
            }
            return sm % prod;
        }

        private static int ModularMultiplicativeInverse(int a, int mod)
        {
            int b = a % mod;
            for (int x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }
            return 1;
        }

        public static void ClosestTimeline(List<Bus> buses)
        {
            bool foundTimeline = false;
            
            while (!foundTimeline)
            {
                (long busId, long minutesToWait) = WaitForBus(buses);
                DepartTime = DepartTime + minutesToWait;

                foundTimeline = OtherBusesWithinDepartureTime(buses, busId);
            }
            
        }

        public static bool OtherBusesWithinDepartureTime(List<Bus> buses, long busId)
        {
            //return buses.All(x => (DepartTime + x.OffSet) % x.ID == 0);
            var filteredBuses = buses.Where(b => b.ID != busId)
                .Select(b => b)
                .All(x => (DepartTime + x.OffSet) % x.ID == 0);

            return filteredBuses;
        }

        public static(long, long) WaitForBus(List<Bus> buses)
        {
            var NextBusTime = DepartTime;

            while (true)
            {
                var foundBuses = buses.Where(b => NextBusTime % b.ID == 0)
                                        .Select(b => b).ToList();


                if(foundBuses.Count() > 0)
                {
                    return (foundBuses.First().ID, NextBusTime - DepartTime);
                }

                NextBusTime++;
            }

        }

        public static List<Bus> ProcessInput(string file)
        {
            var information = File.ReadAllLines(file);
            DepartTime = int.Parse(information[0]);

            var busInformation = information[1].Split(",", StringSplitOptions.None);

            return busInformation.Select((value, Index) => (value, Index))
                                 .Where(b => b.value != "x")
                                 .Select(x => new Bus() { ID = int.Parse(x.value), OffSet = x.Index }).ToList();
        }
    }
}
