using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VehicleManagement.Entities;
using VehicleManagement.Entities.Enums;
using VehicleManagement.Helpers;
using VehicleManagement.Repositories.Interfaces;
using VehicleManagement.Services;

namespace VehicleManagement.Repositories.Implementations
{
    internal class InMemoryVehicleRepository : IVehicleRepository
    {
        private readonly List<Vehicle> _vehicles;
        private readonly IBrandRepository _brandRepository;

        public InMemoryVehicleRepository(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
            _vehicles = new List<Vehicle>()
            {
                new Car() { Name = "Khoa", Brand = _brandRepository.GetBrand("Maybach"), Color = Color.Blue, Price = 234.234m, Type = CarType.Sport, YearOfManufacture = 2020 },
                new Motorbike() { Name = "Chien", Brand = _brandRepository.GetBrand("Ford"), Color = Color.Red, Price = 23423414.32m, LicenseRequired = true, Speed = 220 }
            };
        }
        public List<Vehicle> GetVehicles(Func<Vehicle, bool>? predicate)
        {
            return _vehicles.Where(predicate).ToList();
        }
        public Vehicle GetVehicle(Guid id)
        {
            return _vehicles.FirstOrDefault(v => v.Id == id);
        }

        public void AddVehicle(Vehicle vehicleToAdd)
        {
            _vehicles.Add(vehicleToAdd);
        }

        public void DeleteVehicle(Guid id)
        {
            if (!_vehicles.Any(v => v.Id == id))
            {
                Console.WriteLine("Cannot find such id!");
                return;
            }

            var vehicleToRemove = _vehicles.FirstOrDefault(v => v.Id == id);

            _vehicles.Remove(vehicleToRemove);
        }

        public bool UpdateVehicle(Vehicle updateData)
        {
            if (!_vehicles.Any(v => v.Id == updateData.Id))
            {
                return false;
            }

            var vehicleToUpdate = GetVehicle(updateData.Id);

            foreach (var property in updateData.GetType().GetProperties())
            {
                if (property is not null)
                {
                    string value = (string)property.GetValue(updateData, null);
                    vehicleToUpdate.ChangeProperty(property.Name, value, _brandRepository);
                }
            }

            return true;
        }

        private void RemoveFromFile(string id)
        {
            var FilePath = Constants.FileDirectory + Constants.FileName;
            File.WriteAllLines(FilePath, File.ReadAllLines(FilePath).Where(l => !l.Contains(id)));
        }

        public List<Vehicle> GetVehicles()
        {
            return _vehicles;
        }

        public List<Vehicle> SortVehicles<TKey>(Func<Vehicle, TKey> keySelector)
        {
            return _vehicles.OrderByDescending(keySelector).ToList();
        }
    }
}
