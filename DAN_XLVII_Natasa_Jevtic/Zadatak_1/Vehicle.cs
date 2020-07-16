using System;

namespace Zadatak_1
{
    class Vehicle
    {
        public string Direction { get; set; }
        public int Number { get; set; }
        string[] directions = { "south", "north" };
        static Random random = new Random();

        public Vehicle(int number)
        {
            Number = number;
            Direction = directions[random.Next(0, directions.Length)];
        }
    }
}
