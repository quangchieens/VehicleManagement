using System.ComponentModel.DataAnnotations;
using VehicleManagement.Models.Enums;

namespace VehicleManagement.Models
{
    public class Car : Vehicle
    {
        [Range(2000, 2022)]
        public int YearOfManufacture { get; set; }
        public CarType Type { get; set; }
        public override string ToString()
        {
            return $"{Id,-40} - {Name,-7} - {Color,-7} - {Price, -15} - {Brand.Name, -7} - {YearOfManufacture,-7} - {Type, -7} - {' ', -7} - {' ', -7}\n";
        }
    }
}
