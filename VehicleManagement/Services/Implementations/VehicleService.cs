using System;
using System.IO;
using VehicleManagement.Constants;
using VehicleManagement.Helpers;
using VehicleManagement.Models;
using VehicleManagement.Repositories.Interfaces;
using VehicleManagement.Services.Interfaces;

namespace VehicleManagement.Services.Implementations
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }
        public bool AddVehicle(Vehicle vehicleToAdd)
        {
            if (vehicleToAdd is null)
            {
                Console.WriteLine("You inputted something so wrong");
                return false;
            }

            _vehicleRepository.AddVehicle(vehicleToAdd);
            Console.WriteLine("Vehicle added");
            return true;
        }
        public void UpdateVehicle(Vehicle updateData)
        {
            if (updateData is null)
            {
                Console.WriteLine("The data you input is invalid");
                return;
            }

            Console.WriteLine(_vehicleRepository.UpdateVehicle(updateData) ? "Update successful" : "Update failed");
        }
        public void DeleteVehicle(Guid idToDelete)
        {
            if (idToDelete == Guid.Empty)
            {
                Console.WriteLine("The id you typed in cannot be found");
                return;
            }

            _vehicleRepository.DeleteVehicle(idToDelete);
            Console.WriteLine("Delete successfully");
        }
        public void SearchVehicle(string searchType)
        {
            switch (searchType)
            {
                case "name":
                    SearchVehicleByName();
                    break;
                case "id":
                    SearchVehicleById();
                    break;
                default:
                    Console.WriteLine(searchType);
                    break;
            }
        }
        private void SearchVehicleByName()
        {
            Console.Write("Input search query: ");
            var name = Console.ReadLine();

            var text = File.ReadAllLines(FileConstants.FilePath);
            foreach (var line in text)
            {
                if (name is not null && line.Contains(name))
                {
                    line.PrintSearchItems(name);
                }
            }
        }
        private void SearchVehicleById()
        {
            Console.Write("Input search ID: ");
            string id = Console.ReadLine();

            var text = File.ReadAllLines(FileConstants.FilePath);
            foreach (var line in text)
            {
                if (id != null && line.Contains(id))
                {
                    line.PrintSearchItems(id);
                    return;
                }
            }
        }
        public void ShowVehicle(string printType)
        {
            switch (printType)
            {
                case "0":
                    ShowAllVehicle();
                    break;
                case "1":
                    ShowVehiclesDescending();
                    break;
                default:
                    Console.WriteLine(printType);
                    break;
            }
        }
        private void ShowAllVehicle()
        {
            var text = File.ReadAllText(FileConstants.FilePath);
            Console.WriteLine(text);
        }
        private void ShowVehiclesDescending()
        {
            var textToPrint = _vehicleRepository.GetSortedVehicles(x => x.Price);

            foreach (var line in textToPrint)
            {
                if (line is Motorbike motorbike)
                {
                    Console.WriteLine(motorbike.ToString());
                    motorbike.MakeSound();
                }
                else
                {
                    Console.WriteLine(line.ToString());
                }
            }
        }
    }
}
