using System;
using VehicleManagement.Entities;

namespace VehicleManagement.Services
{
    internal interface IMenuRepository
    {
        public void ShowMenu();
        public Type GetVehicleType();
        public string GetSearchType();
        public string GetVehicleId();
        public string GetShowType();
    }
}