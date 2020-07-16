using System;
using System.Threading;
using System.Linq;

namespace Zadatak_1
{
    class Vehicle
    {
        public string Direction { get; set; }
        public int Number { get; set; }
        string[] directions = { "south", "north" };
        static Random random = new Random();
        static string defaultDirection;
        static EventWaitHandle canCross = new ManualResetEvent(false);
        static object locker = new object();
        bool lockTaken = false;

        public Vehicle(int number)
        {
            Number = number;
            Direction = directions[random.Next(0, directions.Length)];
        }
        /// <summary>
        /// This method manages when the CrossTheBridge method can be invoked.
        /// </summary>
        public void CanCrossTheBridge()
        {
            try
            {
                Monitor.TryEnter(locker, ref lockTaken);
                if (lockTaken)
                {
                    defaultDirection = Thread.CurrentThread.Name;
                    CrossTheBridge();
                }
                else
                {
                    canCross.WaitOne();
                    //if vehicle direction different from default direction, waiting for vehicles with default direction to cross the bridge
                    if (Direction != defaultDirection)
                    {
                        Console.WriteLine("{0}. vehicle is waiting for the lane to clear to cross the bridge.", Number);
                        var threads = Program.threadsForVehicle.Where(x => x.Name == defaultDirection);
                        foreach (var item in threads)
                        {
                            item.Join();
                        }
                        CrossTheBridge();
                    }
                    else
                    {
                        CrossTheBridge();
                    }
                }
            }
            finally
            {
                // ensure that the lock is released
                if (lockTaken)
                {
                    Monitor.Exit(locker);
                }
            }
        }
        /// <summary>
        /// This method simulates crossing the bridge of the vehicle.
        /// </summary>
        public void CrossTheBridge()
        {
            Console.WriteLine("{0}. vehicle is crossing the bridge in the direction of the {1}.", Number, Direction);
            canCross.Set();
            Thread.Sleep(500);
            Console.WriteLine("{0}. vehicle has crossed the bridge in the direction of the {1}.", Number, Direction);            
        }
    }
}