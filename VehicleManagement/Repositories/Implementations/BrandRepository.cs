using System.Collections.Generic;
using VehicleManagement.Entities;
using VehicleManagement.Repositories.Interfaces;

namespace VehicleManagement.Repositories.Implementations
{
    internal class BrandRepository : IBrandRepository
    {
        private readonly List<Brand> _brands;
        public BrandRepository()
        {
            _brands = new List<Brand>();
            AddBrand("Maybach");
            AddBrand("Ford");
        }
        public void AddBrand(string name)
        {
            _brands.Add(new Brand() { Name = name });
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
