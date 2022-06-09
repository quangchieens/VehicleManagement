using System;
using VehicleManagement.Entities;

namespace VehicleManagement.Services
{
    internal interface IVehicleRepository
    {
        public void LoadData();
        public void AddVehicle();
        public bool UpdateVehicle(Guid id);
        public void DeleteVehicle(Guid id);
        public void SearchVehicle();
        public void ShowVehicleList();
        public void StoreData();
    }
}