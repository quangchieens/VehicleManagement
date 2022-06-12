
using System;
using VehicleManagement.Repositories.Implementations;
using VehicleManagement.Repositories.Interfaces;
using VehicleManagement.Services;

namespace VehicleManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var brandRepository = new BrandRepository();
            var repo = new InMemoryVehicleRepository(brandRepository);
            var input = new Input(repo, brandRepository);
            var fileAccessObject = new FileAccessObject(repo, brandRepository);
            var consoleService = new ConsoleService(repo, fileAccessObject);
            var menu = new Menu();
            int choice;

            do
            {
                menu.ShowMenu();
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        //Get data from file
                        fileAccessObject.LoadDataFromFile();
                        break;
                    case 2:
                        menu.AddVehicle();
                        var vehicleToAdd = input.GetVehicleToManipulate();
                        consoleService.AddVehicle(vehicleToAdd);
                        break;
                    case 3:
                        menu.UpdateVehicle();
                        var updateData = input.GetUpdateVehicleInformation();
                        consoleService.UpdateVehicle(updateData);
                        break;
                    case 4:
                        menu.DeleteVehicle();
                        var idToDelete = input.GetId();
                        consoleService.DeleteVehicle(idToDelete);
                        break;
                    case 5:
                        menu.SearchVehicleSubmenu();
                        var searchType = input.GetSearchType();
                        consoleService.SearchVehicle(searchType);
                        break;
                    case 6:
                        menu.ShowVehicleListSubmenu();
                        var printType = input.GetPrintType();
                        consoleService.ShowVehicle(printType);
                        break;
                    case 7:
                        fileAccessObject.StoreDataToFile();
                        break;
                }
            } while (true);
        }
    }
}
