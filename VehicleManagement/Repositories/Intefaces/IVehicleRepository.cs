using System;
using System.Collections.Generic;
using VehicleManagement.Entities;

namespace VehicleManagement.Repositories.Interfaces
{
    internal interface IVehicleRepository
    {
        public void AddVehicle(Vehicle vehicleToAdd);
        public bool UpdateVehicle(Vehicle updateData);
        public void DeleteVehicle(Guid id);
        public Vehicle GetVehicle(Guid id);
        public List<Vehicle> GetVehicles();
        public List<Vehicle> GetVehicles(Func<Vehicle, bool> predicate);
        public List<Vehicle> SortVehicles<TKey>(Func<Vehicle, TKey> keySelector);
    }
}