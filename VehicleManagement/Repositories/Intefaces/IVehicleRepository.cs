using System;
using System.Collections.Generic;
using VehicleManagement.Models;

namespace VehicleManagement.Repositories.Interfaces
{
    public interface IVehicleRepository
    {
        public bool AddVehicle(Vehicle vehicleToAdd);
        public bool UpdateVehicle(Vehicle updateData);
        public bool DeleteVehicle(Guid id);
        public Vehicle GetVehicle(Guid id);
        public List<Vehicle> GetVehicles();
        public List<Vehicle> GetVehicles(Func<Vehicle, bool> predicate);
        public List<Vehicle> GetSortedVehicles<TKey>(Func<Vehicle, TKey> keySelector);
    }
}