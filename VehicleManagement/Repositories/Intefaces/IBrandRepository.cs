using VehicleManagement.Entities;

namespace VehicleManagement.Repositories.Interfaces
{
    internal interface IBrandRepository
    {
        public void AddBrand(string name);
        public Brand GetBrand(string name);
        public Brand CheckBrand(string name);
    }
}
