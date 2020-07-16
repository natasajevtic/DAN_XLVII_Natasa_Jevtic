using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Program
    {
        //declaring an array of vehicles
        static Vehicle[] vehicles;
        //delegate for notification
        delegate void Notification();
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
            CreateVehicle();
            //delegate points to the Notify method
            Notification notification = Notify;
            //invoking method Notify
            notification();
        }
    }
}
