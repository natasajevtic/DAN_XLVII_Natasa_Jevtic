using System;
using System.Diagnostics;
using System.Threading;

namespace Zadatak_1
{
    class Program
    {
        //declaring an array of vehicles
        static Vehicle[] vehicles;
        //delegate for notification
        delegate void Notification();
        //declaring array of threads
        public static Thread[] threadsForVehicle;
        /// <summary>
        /// This method initializes an array of vehicles, creates objects of class Vehicle, and fills the array with them.
        /// </summary>
        static void CreateVehicle()
        {
            Random random = new Random();
            int numberOfVehicle = random.Next(1, 16);
            vehicles = new Vehicle[numberOfVehicle];
            for (int i = 0; i < numberOfVehicle; i++)
            {
                vehicles[i] = new Vehicle(i + 1);
            }
        }
        /// <summary>
        /// This method displays information about vehicles to the user.
        /// </summary>
        static void Notify()
        {
            Console.WriteLine("The total number of vehicles is {0}.", vehicles.Length);
            foreach (Vehicle vehicle in vehicles)
            {
                Console.WriteLine("{0}. vehicle - direction: {1}", vehicle.Number, vehicle.Direction);
            }
        }
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            CreateVehicle();
            //delegate points to the Notify method
            Notification notification = Notify;
            //invoking method Notify
            notification();
            threadsForVehicle = new Thread[vehicles.Length];
            //creating threads for crossing the bridge of every vehicle
            for (int i = 0; i < threadsForVehicle.Length; i++)
            {
                threadsForVehicle[i] = new Thread(vehicles[i].CanCrossTheBridge)
                { Name = vehicles[i].Direction };
            }
            //starting threads
            for (int i = 0; i < threadsForVehicle.Length; i++)
            {
                threadsForVehicle[i].Start();
            }
            for (int i = 0; i < threadsForVehicle.Length; i++)
            {
                threadsForVehicle[i].Join();
            }
            stopWatch.Stop();
            //getting the elapsed time as a TimeSpan value
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("Application duration: {0} ms", ts.TotalMilliseconds);
            Console.ReadKey();
        }
    }
}