using System;
using System.Collections.Generic;
using VehicleManagement.Entities;
using VehicleManagement.Entities.Enums;
using VehicleManagement.Entities.VehicleTypes;

namespace VehicleManagement.Helpers
{
    internal static class VehicleExtensions
    {
        public static void ChangeProperty (this Vehicle vehicle, string propertyToChange, string newPropertyValue)
        {
            vehicle.GetType()
                .GetProperty(propertyToChange)
                .SetValue(vehicle,
                            newPropertyValue switch
                            {
                                "Name" => newPropertyValue,
                                "Color" => Enum.Parse<Color>(newPropertyValue),
                                "Price" => decimal.Parse(newPropertyValue),
                                "Brand" => Enum.Parse<Brand>(newPropertyValue),
                                "Year of manufacture" or "Speed" => int.Parse(newPropertyValue),
                                "Car type" => Enum.Parse<CarType>(newPropertyValue),
                                "Require license" => bool.Parse(newPropertyValue),
                                _ => null
                            },
                            null);
        }

        public static void AddCar(this List<Vehicle> vehicles, string[] words)
        {
            vehicles.Add(new Car
            {
                Id = Guid.Parse(words[0]),
                Name = words[1],
                Color = Enum.Parse<Color>(words[2]),
                Price = decimal.Parse(words[3]),
                Brand = Enum.Parse<Brand>(words[4]),
                YearOfManufacture = int.Parse(words[5]),
                Type = Enum.Parse<CarType>(words[6])
            });
        }

        public static void AddMotorbike(this List<Vehicle> vehicles, string[] words)
        {
            vehicles.Add(new Motorbike
            {
                Id = Guid.Parse(words[0]),
                Name = words[1],
                Color = Enum.Parse<Color>(words[2]),
                Price = decimal.Parse(words[3]),
                Brand = Enum.Parse<Brand>(words[4]),
                Speed = int.Parse(words[5]),
                LicenseRequired = bool.Parse(words[6])
            });
        }

    }
}
