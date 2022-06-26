using System;
using System.Collections.Generic;
using System.Linq;
using VehicleManagement.Helpers;
using VehicleManagement.Models;
using VehicleManagement.Models.Enums;
using VehicleManagement.Repositories.Interfaces;

namespace VehicleManagement.Repositories.Implementations
{
    public class InMemoryVehicleRepository : IVehicleRepository
    {
        private readonly List<Vehicle> _vehicles;
        private readonly IBrandRepository _brandRepository;

        public InMemoryVehicleRepository(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
            _vehicles = new List<Vehicle>()
            {
                new Car() { Id = Guid.Parse("5b83df53-a6c6-4e75-a337-8eb9087a2c0e"), Name = "Khoa", Brand = _brandRepository.GetBrand("Maybach"), Color = Color.Blue, Price = 234.234m, Type = CarType.Sport, YearOfManufacture = 2020 },
                new Motorbike() { Name = "Chien", Brand = _brandRepository.GetBrand("Ford"), Color = Color.Red, Price = 23423414.32m, LicenseRequired = true, Speed = 220 }
            };
        }
        public bool AddVehicle(Vehicle vehicleToAdd)
        {
            _vehicles.Add(vehicleToAdd);
            return true;
            //not handle false case because of simple execution
        }
        public bool DeleteVehicle(Guid id)
        {
            if (_vehicles.All(v => v.Id != id))
            {
                Console.WriteLine("Cannot find such id!");
                return false;
            }

            var vehicleToRemove = _vehicles.FirstOrDefault(v => v.Id == id);

            _vehicles.Remove(vehicleToRemove);
            return true;
        }
        public bool UpdateVehicle(Vehicle updateData)
        {
            if (_vehicles.All(v => v.Id != updateData.Id))
            {
                return false;
            }

            var vehicleToUpdate = GetVehicle(updateData.Id);

            foreach (var property in updateData.GetType().GetProperties())
            {
                var value = property.GetValue(updateData, null)?.ToString();
                vehicleToUpdate.ChangeProperty(property.Name, value, _brandRepository);
            }

            return true;
        }
        public Vehicle GetVehicle(Guid id)
        {
            return _vehicles.FirstOrDefault(v => v.Id == id);
        }
        public List<Vehicle> GetVehicles()
        {
            return _vehicles;
        }
        public List<Vehicle> GetVehicles(Func<Vehicle, bool> predicate)
        {
            return _vehicles.Where(predicate).ToList();
        }
        public List<Vehicle> GetSortedVehicles<TKey>(Func<Vehicle, TKey> keySelector)
        {
            return _vehicles.OrderByDescending(keySelector).ToList();
        }
    }
}
