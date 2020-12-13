using System;
namespace Day12
{
    public class Spaceship
    {
        public string Direction { get; set; }
        public int PosX { get; set; }
        public int PosY { get; set; }

        
        public void Move(Coordinate coordinate)
        {
            if(coordinate.Direction == "F")
            {
                Move(Direction, coordinate.Distance);
            }
            else if(coordinate.Direction == "R" || coordinate.Direction == "L")
            {
                Rotate(coordinate);
            }
            else
            {
                Move(coordinate.Direction, coordinate.Distance);
            }
        }

        private void Move(string direction, int distance)
        {
            switch (direction)
            {
                case "E":
                    PosX = PosX + distance;
                    break;
                case "W":
                    PosX = PosX - distance;
                    break;
                case "N":
                    PosY = PosY + distance;
                    break;
                case "S":
                    PosY = PosY - distance;
                    break;
            }
        }

        public void Rotate(Coordinate coordinate)
        {
            int direction = Rotate(coordinate.Direction, coordinate.Distance);

            Direction = direction switch
            {
                0 => "E",
                1 => "S",
                2 => "W",
                _ => "N"
            };
        }

        private int Rotate(string direction, int rotation)
        {
            int shipAngle = Direction switch
            {
                "E" => 0,
                "S" => 1,
                "W" => 2,
                "N" => 3,
                _ => -1
            };

            if(direction == "L")
            {
                 return 4 - (rotation / 90);
            }
            else
            {
                return rotation / 90;
            }

        }

        public int HammingtonDistance()
        {
            return Math.Abs(PosX) + Math.Abs(PosY);
        }
    }
}
