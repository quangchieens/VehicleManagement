using System;
using VehicleManagement.Entities;

namespace VehicleManagement.Services
{
    internal interface IMenuRepository
    {
        public void ShowMenu();
        public Type AddVehicle();
        public string SearchVehicle();
        public string GetVehicleId();
        public string ShowVehicleList();
    }
}