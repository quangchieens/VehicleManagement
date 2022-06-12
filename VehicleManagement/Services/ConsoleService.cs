using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagement.Entities;
using VehicleManagement.Helpers;
using VehicleManagement.Repositories.Interfaces;

namespace VehicleManagement.Services
{
    internal class ConsoleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly FileAccessObject _fileAccessObject;
        public ConsoleService(IVehicleRepository vehicleRepository, FileAccessObject fileAccessObject)
        {
            _vehicleRepository = vehicleRepository;
            _fileAccessObject = fileAccessObject;
        }
        public void LoadDataFromFile()
        {
            _fileAccessObject.LoadDataFromFile();
            Console.WriteLine("Data loaded successfully");
        }
        public void AddVehicle(Vehicle vehicleToAdd)
        {
            if (vehicleToAdd is null)
            {
                Console.WriteLine("You inputted something so wrong");
            }
            else
            {
                _vehicleRepository.AddVehicle(vehicleToAdd);
                Console.WriteLine("Vehicle added");
            }
        }
        public void UpdateVehicle(Vehicle updateData)
        {
            if (updateData is null)
            {
                Console.WriteLine("The data you input is invalid");
                return;
            }

            if (_vehicleRepository.UpdateVehicle(updateData))
            {
                Console.WriteLine("Update successful");
            }
            else
            {
                Console.WriteLine("Update failed");
            }
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
            if (searchType is "name")
            {
                SearchVehicleByName();
            }
            else if (searchType is "id")
            {
                SearchVehicleById();
            }
            else
            {
                Console.WriteLine(searchType);
            }
        }
        private void SearchVehicleByName()
        {
            Console.Write("Input search query: ");
            var name = Console.ReadLine();

            var text = File.ReadAllLines(_fileAccessObject.FilePath);
            foreach (var line in text)
            {
                if (line.Contains(name))
                {
                    line.PrintSearchItems(name);
                }
            }
        }
        private void SearchVehicleById()
        {
            Console.Write("Input search ID: ");
            var id = Console.ReadLine();

            var text = File.ReadAllLines(_fileAccessObject.FilePath);
            foreach (var line in text)
            {
                if (line.Contains(id))
                {
                    line.PrintSearchItems(id);
                    return;
                }
            }
        }
        public void ShowVehicle(string printType)
        {
            if (printType is "0")
            {
                ShowAllVehicle();
            }
            else if (printType is "1")
            {
                ShowVehiclesDescending();
            }
            else
            {
                Console.WriteLine(printType);
            }
        }
        private void ShowAllVehicle()
        {
            var text = File.ReadAllText(_fileAccessObject.FilePath);
            Console.WriteLine(text);
        }
        private void ShowVehiclesDescending()
        {
            var textToPrint = _vehicleRepository.SortVehicles(x => x.Price);

            foreach (var line in textToPrint)
            {
                if (line is Motorbike)
                {
                    Console.WriteLine(line.ToString());
                    ((Motorbike)line).MakeSound();
                }
                else
                {
                    Console.WriteLine(line.ToString());
                }
            }
        }
        public void StoreDataToFile()
        {
            _fileAccessObject.StoreDataToFile();
            Console.WriteLine("Data stored successfully");
        }
    }
}
