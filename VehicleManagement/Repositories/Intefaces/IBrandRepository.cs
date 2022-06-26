using VehicleManagement.Models;

namespace VehicleManagement.Repositories.Interfaces
{
    public interface IBrandRepository
    {
        public Brand GetBrand(string name);
        public Brand CheckBrand(string name);
    }
}
