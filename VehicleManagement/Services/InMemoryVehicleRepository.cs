using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VehicleManagement.Entities;
using VehicleManagement.Entities.Enums;
using VehicleManagement.Helpers;

namespace VehicleManagement.Services
{
    internal class InMemoryVehicleRepository : IVehicleRepository
    {
        private readonly List<Vehicle> _vehicles;
        private readonly IBrandRepository _brandRepository;
        private readonly IOVehicles _IOVehicles;
        private readonly string fileDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString() + "\\";
        private readonly string fileName;
        private readonly string filePath;

        public InMemoryVehicleRepository(string fileName)
        {

            _brandRepository = new BrandRepository();
            _brandRepository.AddBrand("Maybach");
            _brandRepository.AddBrand("Ford");

            _vehicles = new List<Vehicle>()
            {
                new Car() { Name = "Khoa", Brand = _brandRepository.GetBrand("Maybach"), Color = Color.Blue, Price = 234.234m, Type = CarType.Sport, YearOfManufacture = 2020 },
                new Motorbike() { Name = "Chien", Brand = _brandRepository.GetBrand("Ford"), Color = Color.Red, Price = 23423414.32m, LicenseRequired = true, Speed = 220 }
            };

            _IOVehicles = new IOVehicles();

            this.fileName = fileName;
            this.filePath = fileDirectory + fileName;
        }

        private Vehicle GetVehicle(Guid id)
        {
            return _vehicles.FirstOrDefault(v => v.Id == id);
        }

        public void AddVehicle()
        {
            var vehicleType = _IOVehicles.GetVehicleType();
            if (vehicleType is null)
            {
                Console.WriteLine("You inputted something so wrong");
            }

            if (vehicleType is "car")
            {
                var car = _IOVehicles.InputCar(_brandRepository);
                _vehicles.Add(car);
            }
            else if (vehicleType is "motorbike")
            {
                var motorbike = _IOVehicles.InputMotorbike(_brandRepository);
                _vehicles.Add(motorbike);
            }
            else
            {
                Console.WriteLine("Your object is kinda weird");
            }
        }

        public void DeleteVehicle(Guid id)
        {
            if (!_vehicles.Any(v => v.Id == id))
            {
                Console.WriteLine("Cannot find such id!");
            }

            var vehicleToRemove = _vehicles.FirstOrDefault(v => v.Id == id);

            _vehicles.Remove(vehicleToRemove);
            RemoveFromFile(id.ToString());
        }

        private void SearchVehicleByName()
        {
            Console.Write("Input search query: ");
            var name = Console.ReadLine();

            var text = File.ReadAllLines(filePath);
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

            var text = File.ReadAllLines(filePath);
            foreach (var line in text)
            {
                if (line.Contains(id))
                {
                    line.PrintSearchItems(id);
                    return;
                }
            }
        }

        public void ShowVehicleList()
        {
            var showType = _IOVehicles.GetShowType();
            if (showType is "0")
            {
                ShowAllVehicle();
            }
            else if (showType is "1")
            {
                ShowVehiclesDescending();
            }
            else
            {
                Console.WriteLine(showType);
            }
        }

        private void ShowAllVehicle()
        {
            var text = File.ReadAllText(filePath);
            Console.WriteLine(text);
        }

        private void ShowVehiclesDescending()
        {
            var textToPrint = _vehicles.OrderByDescending(x => x.Price);

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

        public void StoreData()
        {
            //only append unoccurred instances
            var text = File.ReadAllText(filePath);
            var vehicles = _vehicles.Where(v => !text.Contains(v.Id.ToString()))
                                    .ToList();

            foreach (var vehicle in vehicles)
            {
                File.AppendAllText(filePath, vehicle.ToString());
            }
        }

        public bool UpdateVehicle(Guid id)
        {
            if (!_vehicles.Any(v => v.Id == id))
            {
                return false;
            }

            var vehicleToUpdate = GetVehicle(id);
            string propertyToChange, newPropertyValue;

            (propertyToChange, newPropertyValue) = _IOVehicles.GetPropertyToChange(vehicleToUpdate);

            vehicleToUpdate.ChangeProperty(propertyToChange, newPropertyValue, _brandRepository);

            return true;

        }

        public void LoadData()
        {
            StreamReader data = new StreamReader(filePath);
            string line;
            while ((line = data.ReadLine()) is not null)
            {
                string[] words = line.Split(" -");
                if (words.Any(w => w.Trim() == "True" || w.Trim() == "False"))
                {
                    _vehicles.AddMotorbike(words, _brandRepository);
                }
                else
                {
                    _vehicles.AddCar(words, _brandRepository);
                }
            }
        }

        public void SearchVehicle()
        {
            var searchType = _IOVehicles.GetSearchType();
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

        private void RemoveFromFile(string id)
        {
            File.WriteAllLines(filePath, File.ReadAllLines(filePath).Where(l => !l.Contains(id)));
        }
    }
}
