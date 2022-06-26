using System;
using System.ComponentModel.DataAnnotations;
using VehicleManagement.Models.Enums;

namespace VehicleManagement.Models
{
    public class Vehicle
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; }
        public Color Color { get; set; }
        [Range(0, 1000000)]
        public decimal Price { get; set; }
        public Brand Brand { get; set; }
    }
}