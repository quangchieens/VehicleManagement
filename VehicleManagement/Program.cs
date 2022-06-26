using System;
using VehicleManagement.Repositories.Implementations;
using VehicleManagement.Services.Implementations;
using VehicleManagement.Services.Interfaces;
using VehicleManagement.Views;

namespace VehicleManagement
{
    public class Program
    {
        static void Main(string[] args)
        {
            var brandRepository = new BrandRepository();
            var repo = new InMemoryVehicleRepository(brandRepository);
            IFileService fileService = new FileService(repo, brandRepository);
            IVehicleService vehicleService = new VehicleService(repo);

            do
            {
                Menu.ShowMenu();
                var choice = int.Parse(Console.ReadLine() ?? string.Empty);

                switch (choice)
                {
                    case 1:
                        //Get data from file
                        fileService.LoadDataFromFile();
                        break;
                    case 2:
                        Menu.AddVehicle();
                        var vehicleToAdd = Input.GetVehicleToManipulate(brandRepository);
                        vehicleService.AddVehicle(vehicleToAdd);
                        break;
                    case 3:
                        Menu.UpdateVehicle();
                        var updateData = Input.GetUpdateVehicleInformation(repo, brandRepository);
                        vehicleService.UpdateVehicle(updateData);
                        break;
                    case 4:
                        Menu.DeleteVehicle();
                        var idToDelete = Input.GetId();
                        vehicleService.DeleteVehicle(idToDelete);
                        break;
                    case 5:
                        Menu.SearchVehicleSubmenu();
                        var searchType = Input.GetSearchType();
                        vehicleService.SearchVehicle(searchType);
                        break;
                    case 6:
                        Menu.ShowVehicleListSubmenu();
                        var printType = Input.GetPrintType();
                        vehicleService.ShowVehicle(printType);
                        break;
                    case 7:
                        fileService.StoreDataToFile();
                        break;
                }
            } while (true);
        }
    }
}
