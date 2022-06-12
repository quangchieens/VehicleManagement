using System;
using System.Collections.Generic;
using VehicleManagement.Entities;
using VehicleManagement.Entities.Enums;
using VehicleManagement.Repositories.Interfaces;
using VehicleManagement.Services;

namespace VehicleManagement.Helpers
{
    internal static class VehicleExtensions
    {
        public static void ChangeProperty (this Vehicle vehicle, string propertyToChange, string newPropertyValue, IBrandRepository brandRepository)
        {
            vehicle.GetType()
                .GetProperty(propertyToChange)
                .SetValue(vehicle,
                            newPropertyValue switch
                            {
                                "Name" => newPropertyValue,
                                "Color" => Enum.Parse<Color>(newPropertyValue),
                                "Price" => decimal.Parse(newPropertyValue),
                                "Brand" => brandRepository.CheckBrand(newPropertyValue),
                                "Year of manufacture" or "Speed" => int.Parse(newPropertyValue),
                                "Car type" => Enum.Parse<CarType>(newPropertyValue),
                                "Require license" => bool.Parse(newPropertyValue),
                                _ => null
                            },
                            null);
        }

        public static void AddCar(this IVehicleRepository vehicleRepository, string[] words, IBrandRepository brandRepository)
        {
            vehicleRepository.AddVehicle(new Car
            {
                Id = Guid.Parse(words[0].Trim()),
                Name = words[1].Trim(),
                Color = Enum.Parse<Color>(words[2].Trim()),
                Price = decimal.Parse(words[3].Trim()),
                Brand = brandRepository.CheckBrand(words[4].Trim()),
                YearOfManufacture = int.Parse(words[5].Trim()),
                Type = Enum.Parse<CarType>(words[6].Trim())
            });
        }

        public static void AddMotorbike(this IVehicleRepository vehicleRepository, string[] words, IBrandRepository brandRepository)
        {
            vehicleRepository.AddVehicle(new Motorbike
            {
                Id = Guid.Parse(words[0].Trim()),
                Name = words[1].Trim(),
                Color = Enum.Parse<Color>(words[2].Trim()),
                Price = decimal.Parse(words[3].Trim()),
                Brand = brandRepository.CheckBrand(words[4].Trim()),
                Speed = int.Parse(words[7].Trim()),
                LicenseRequired = bool.Parse(words[8].Trim())
            });
        }

    }
}
