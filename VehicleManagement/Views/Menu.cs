using System;

namespace VehicleManagement.Views
{
    internal class Menu
    {
        public static void ShowMenu()
        {
            Console.WriteLine("Phan mem abcxyz");
            Console.WriteLine("1. Get data from file");
            Console.WriteLine("2. Add new vehicle");
            Console.WriteLine("3. Update vehicle");
            Console.WriteLine("4. Delete Vehicle");
            Console.WriteLine("5. Search vehicle");
            Console.WriteLine("6. Show vehicle list");
            Console.WriteLine("7. Store data to file");
        }
        public static void AddVehicle()
        {

        }
        public static void UpdateVehicle()
        {

        }
        public static void DeleteVehicle()
        {

        }

        public static void SearchVehicleSubmenu()
        {
            Console.WriteLine("5.1. Search by name");
            Console.WriteLine("5.2. Search by id");
        }
        public static void ShowVehicleListSubmenu()
        {
            Console.WriteLine("6.1. Show all");
            Console.WriteLine("6.2. Show all (descending by price)");
        }
        
    }
}
