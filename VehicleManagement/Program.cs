
using System;
using VehicleManagement.Services;

namespace VehicleManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string file = "vehicles.txt";
            IVehicleRepository repo = new InMemoryVehicleRepository(file);
            IMenuRepository menu = new MenuRepository();
            int choice;

            do
            {
                menu.ShowMenu();
                choice = int.Parse(Console.ReadLine());
                
                switch (choice)
                {
                    case 1:
                        
                        break;
                    case 2:
                        var vehicleType = menu.AddVehicle();
                        repo.AddVehicle(vehicleType);
                        break;
                    case 4:
                        repo.DeleteVehicle(menu.GetVehicleId());
                        break;
                    case 5:
                        var searchType = menu.SearchVehicle();
                        repo.SearchVehicle(searchType);
                        break;
                    case 7:
                        repo.StoreData();
                        break;
                }
            } while (true);
        }
    }
}
