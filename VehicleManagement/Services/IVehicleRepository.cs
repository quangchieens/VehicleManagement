using System;
using VehicleManagement.Entities;

namespace VehicleManagement.Services
{
    internal interface IVehicleRepository
    {
        public void LoadData();
        public void AddVehicle(Type vehicleType);
        public bool UpdateVehicle(Guid id);
        public void DeleteVehicle(string id);
        public void SearchVehicle(string searchType);
        public void ShowVehicleList(string showType);
        public void StoreData();
    }
}