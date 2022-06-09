using VehicleManagement.Entities;

namespace VehicleManagement.Services
{
    internal interface IBrandRepository
    {
        public void AddBrand(string name);
        public Brand GetBrand(string name);
        public Brand CheckBrand(string name);
    }
}
