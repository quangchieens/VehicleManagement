using System;
using VehicleManagement.Entities;
using VehicleManagement.Entities.Enums;

namespace VehicleManagement.Helpers
{
    internal static class VehicleExtension
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
    }
}
