using System.Collections.Generic;
using VehicleManagement.Models;
using VehicleManagement.Repositories.Interfaces;

namespace VehicleManagement.Repositories.Implementations
{
    public class BrandRepository : IBrandRepository
    {
        private readonly List<Brand> _brands;
        public BrandRepository()
        {
            _brands = new List<Brand>();
            AddBrand("Maybach");
            AddBrand("Ford");
        }
        private bool AddBrand(string name)
        {
            if (name is null) return false;
            _brands.Add(new Brand() { Name = name });
            return true;

        }

        public Brand CheckBrand(string name)
        {
            if (GetBrand(name) == null)
            {
                AddBrand(name);
            }
            return GetBrand(name);
        }

        public Brand GetBrand(string name)
        {
            return _brands.Find(b => b.Name == name);
        }
    }
}
