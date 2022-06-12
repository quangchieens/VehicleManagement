using System;

namespace VehicleManagement.Services
{
    internal class Menu
    {
        public void ShowMenu()
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
        public void AddVehicle()
        {

        }
        public void UpdateVehicle()
        {

        }
        public void DeleteVehicle()
        {

        }

        public void SearchVehicleSubmenu()
        {
            Console.WriteLine("5.1. Search by name");
            Console.WriteLine("5.2. Search by id");
        }
        public void ShowVehicleListSubmenu()
        {
            Console.WriteLine("6.1. Show all");
            Console.WriteLine("6.2. Show all (descending by price)");
        }
        
    }
}
