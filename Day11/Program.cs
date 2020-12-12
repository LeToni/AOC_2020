using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day11
{
    class Program
    {
        public const char FlOOR = '.';
        public const char EMPTY_SEAT = 'L';
        public const char OCCUPIED_SEAT = '#';

        static void Main(string[] args)
        {
            var file = "Input_Test.txt";
            List<Seat> seats = ProcessInput(file);
            List<Seat> seats_2 = ProcessInput(file);


            seats = Simulation(seats);
            int occupied = CountOccupiedSeats(seats);
            Console.WriteLine(occupied);

            seats_2 = Advanced_Simulation(seats_2);
            int occupied_2 = CountOccupiedSeats(seats_2);
            Console.WriteLine(occupied_2);
        }

        public static List<Seat> Simulation(List<Seat> seats)
        {
            var seatsChanged = true;
            List<Seat> seatSnapshot = seats;

            while (seatsChanged)
            {
                (seatSnapshot, seatsChanged) = OccupieSeat(seatSnapshot);
                (seatSnapshot, seatsChanged) = LeaveSeat(seatSnapshot);
            }

            return seatSnapshot;
        }

        public static List<Seat> Advanced_Simulation(List<Seat> seats)
        {
            var seatsChanged = true;
            List<Seat> seatSnapshot = seats;

            while (seatsChanged)
            {
                (seatSnapshot, seatsChanged) = OccupieSeatWithinDistance(seatSnapshot);
                (seatSnapshot, seatsChanged) = LeaveSeatWithinDistance(seatSnapshot, 5);
            }

            return seatSnapshot;
        }

        public static (List<Seat>, bool) OccupieSeat(List<Seat> seats)
        {
            bool seatsChanged = false;
            List<Seat> newArrangement = new List<Seat>();

            foreach(var seat in seats)
            {
                if (seat.Occupied == true)
                {
                    newArrangement.Add(seat);
                    continue;
                }
                    
                List<Seat> adjacentSeats = GetAdjacentSeats(seats, seat);
                var numberOccupiedSeats = adjacentSeats.Where(s => s.Occupied == true)
                                                    .Select(s => s)
                                                    .Count();
              
                if (numberOccupiedSeats == 0)
                {
                    Seat newSeat = new Seat() { Occupied = true, PosRow = seat.PosRow, PosCol = seat.PosCol };
                    newArrangement.Add(newSeat);
                    seatsChanged = true;
                }
                else
                {
                    newArrangement.Add(seat);
                }

            }

            return (newArrangement, seatsChanged);
        }

        public static (List<Seat>, bool) OccupieSeatWithinDistance(List<Seat> seats)
        {
            bool seatsChanged = false;
            List<Seat> newArrangement = new List<Seat>();

            foreach (var seat in seats)
            {
                if (seat.Occupied == true)
                {
                    newArrangement.Add(seat);
                    continue;
                }

                List<Seat> adjacentSeats = GetAdjacentSeatsWithinDistance(seats, seat);
                var numberOccupiedSeats = adjacentSeats.Where(s => s.Occupied == true)
                                                    .Select(s => s)
                                                    .Count();

                if (numberOccupiedSeats == 0)
                {
                    Seat newSeat = new Seat() { Occupied = true, PosRow = seat.PosRow, PosCol = seat.PosCol };
                    newArrangement.Add(newSeat);
                    seatsChanged = true;
                }
                else
                {
                    newArrangement.Add(seat);
                }

            }

            return (newArrangement, seatsChanged);
        }
        public static (List<Seat>, bool) LeaveSeat(List<Seat> seats, int numberOccupied = 4)
        {
            bool seatsChanged = false;
            List<Seat> newArrangement = new List<Seat>();

            foreach (var seat in seats)
            {
                if (seat.Occupied == false)
                {
                    newArrangement.Add(seat);
                    continue;
                }

                List<Seat> adjacentSeats = GetAdjacentSeats(seats, seat);
                var numberOccupiedSeats = adjacentSeats.Where(s => s.Occupied)
                                                    .Count();
                if (numberOccupiedSeats >= numberOccupied)
                {
                    Seat newSeat = new Seat() { Occupied = false, PosRow = seat.PosRow, PosCol = seat.PosCol };
                    newArrangement.Add(newSeat);
                    seatsChanged = true;
                }
                else
                {
                    newArrangement.Add(seat);
                }
            }

            return (newArrangement, seatsChanged);
        }

        public static (List<Seat>, bool) LeaveSeatWithinDistance(List<Seat> seats, int numberOccupied = 4)
        {
            bool seatsChanged = false;
            List<Seat> newArrangement = new List<Seat>();

            foreach (var seat in seats)
            {
                if (seat.Occupied == false)
                {
                    newArrangement.Add(seat);
                    continue;
                }

                List<Seat> adjacentSeats = GetAdjacentSeatsWithinDistance(seats, seat);
                var numberOccupiedSeats = adjacentSeats.Where(s => s.Occupied)
                                                    .Count();
                if (numberOccupiedSeats >= numberOccupied)
                {
                    Seat newSeat = new Seat() { Occupied = false, PosRow = seat.PosRow, PosCol = seat.PosCol };
                    newArrangement.Add(newSeat);
                    seatsChanged = true;
                }
                else
                {
                    newArrangement.Add(seat);
                }
            }

            return (newArrangement, seatsChanged);
        }

        public static List<Seat> ProcessInput(string file)
        {
            var input = File.ReadAllLines(file);
            List<Seat> seats = new List<Seat>();
            int i = 0;
            foreach (string seatRow in input)
            {
                for (int j = 0; j < seatRow.Length; j++)
                {
                    if (seatRow[j] == EMPTY_SEAT)
                    {
                        seats.Add(new Seat() { Occupied = false, PosRow = i + 1, PosCol = j + 1 });
                    }
                    if (seatRow[j] == OCCUPIED_SEAT)
                    {
                        seats.Add(new Seat() { Occupied = true, PosRow = i + 1, PosCol = j + 1 });
                    }
                }
                i++;
            }

            return seats;
        }

        public static List<Seat> GetAdjacentSeats(List<Seat> seats, Seat seat)
        {
            return seats.Where(s => IsImmediateAdjacentSeat(s, seat))
                        .Select(s => s)
                        .ToList();
        }

        public static List<Seat> GetAdjacentSeatsWithinDistance(List<Seat> seats, Seat seat)
        {
            return seats.Where(s => IsWithinDistance(s, seat))
                        .Select(s => s)
                        .ToList();
        }

        public static bool IsImmediateAdjacentSeat(Seat seatNeighbor, Seat seat)
        {
            // UP
            if (seatNeighbor.PosRow == seat.PosRow + 1 && seatNeighbor.PosCol == seat.PosCol)
                return true;
            // DOWN
            if (seatNeighbor.PosRow == seat.PosRow - 1 && seatNeighbor.PosCol == seat.PosCol)
                return true;
            // RIGHT
            if (seatNeighbor.PosRow == seat.PosRow && seatNeighbor.PosCol == seat.PosCol + 1)
                return true;
            // LEFT
            if (seatNeighbor.PosRow == seat.PosRow && seatNeighbor.PosCol == seat.PosCol - 1)
                return true;
            // UP RIGHT
            if (seatNeighbor.PosRow  == seat.PosRow + 1  && seatNeighbor.PosCol == seat.PosCol + 1)
                return true;
            // UP LEFT
            if (seatNeighbor.PosRow  == seat.PosRow + 1 && seatNeighbor.PosCol  == seat.PosCol - 1)
                return true;
            // DOWN RIGHT
            if (seatNeighbor.PosRow  == seat.PosRow - 1 && seatNeighbor.PosCol  == seat.PosCol + 1)
                return true;
            // DOWN LEF
            if (seatNeighbor.PosRow == seat.PosRow - 1 && seatNeighbor.PosCol == seat.PosCol - 1)
                return true;

            return false;
        }

        public static bool IsWithinDistance(Seat seatNeighbor, Seat seat)
        {
            for(int i = 1; i <= 8; i++)
            {
                // UP
                if (seatNeighbor.PosRow == seat.PosRow + i && seatNeighbor.PosCol == seat.PosCol)
                    return true;
                // DOWN
                if (seatNeighbor.PosRow == seat.PosRow - i && seatNeighbor.PosCol == seat.PosCol)
                    return true;
                // RIGHT
                if (seatNeighbor.PosRow == seat.PosRow && seatNeighbor.PosCol == seat.PosCol + i)
                    return true;
                // LEFT
                if (seatNeighbor.PosRow == seat.PosRow && seatNeighbor.PosCol == seat.PosCol - i)
                    return true;
                // UP RIGHT
                if (seatNeighbor.PosRow == seat.PosRow + i && seatNeighbor.PosCol == seat.PosCol + i)
                    return true;
                // UP LEFT
                if (seatNeighbor.PosRow == seat.PosRow + i && seatNeighbor.PosCol == seat.PosCol - i)
                    return true;
                // DOWN RIGHT
                if (seatNeighbor.PosRow == seat.PosRow - i && seatNeighbor.PosCol == seat.PosCol + i)
                    return true;
                // DOWN LEF
                if (seatNeighbor.PosRow == seat.PosRow - i && seatNeighbor.PosCol == seat.PosCol - i)
                    return true;
            }

            return false;
        }

        public static int CountOccupiedSeats(List<Seat> seats)
        {
            return seats.Where(s => s.Occupied == true)
                        .Count();
        }
    }
}
