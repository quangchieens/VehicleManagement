using System;
using VehicleManagement.Models;

namespace VehicleManagement.Services.Interfaces
{
    public interface IVehicleService
    {
        public bool AddVehicle(Vehicle vehicleToAdd);
        public void UpdateVehicle(Vehicle updateData);
        public void DeleteVehicle(Guid idToDelete);
        public void SearchVehicle(string searchType);
        public void ShowVehicle(string printType);
    }
}