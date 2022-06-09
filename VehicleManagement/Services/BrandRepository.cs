using System;
using System.Collections.Generic;
using VehicleManagement.Entities;

namespace VehicleManagement.Services
{
    internal class BrandRepository : IBrandRepository
    {
        private readonly List<Brand> _brandRepository;
        public BrandRepository()
        {
            _brandRepository = new List<Brand>();
        }
        public void AddBrand(string name)
        {
            _brandRepository.Add(new Brand() { Name = name });
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
            return _brandRepository.Find(b => b.Name == name);
        }
    }
}
