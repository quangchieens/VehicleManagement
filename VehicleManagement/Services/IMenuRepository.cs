using System;
using VehicleManagement.Entities;

namespace VehicleManagement.Services
{
    internal interface IMenuRepository
    {
        public void ShowMenu();
        public Type GetVehicleType();
        public string GetSearchType();
        public string GetVehicleId(string action);
        public string GetShowType();
    }
}