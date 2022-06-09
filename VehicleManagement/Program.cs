
using System;
using VehicleManagement.Services;

namespace VehicleManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string file = "vehicles.txt";
            IVehicleRepository repo = new InMemoryVehicleRepository(file);
            var IOVehicles = new IOVehicles();
            var menu = new Menu();
            int choice;

            do
            {
                menu.ShowMenu();
                choice = int.Parse(Console.ReadLine());
                
                switch (choice)
                {
                    case 1:
                        repo.LoadData();
                        break;
                    case 2:
                        repo.AddVehicle();
                        break;
                    case 3:
                        Guid idToUpdate = IOVehicles.GetId();
                        repo.UpdateVehicle(idToUpdate);
                        break;
                    case 4:
                        Guid idToDelete = IOVehicles.GetId();
                        repo.DeleteVehicle(idToDelete);
                        break;
                    case 5:
                        menu.SearchVehicleSubmenu();
                        repo.SearchVehicle();
                        break;
                    case 6:
                        menu.ShowVehicleListSubmenu();
                        repo.ShowVehicleList();
                        break;
                    case 7:
                        repo.StoreData();
                        break;
                }
            } while (true);
        }
    }
}
