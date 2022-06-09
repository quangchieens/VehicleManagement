using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagement.Entities.Enums;

namespace VehicleManagement.Entities.VehicleTypes
{
    internal class Car : Vehicle
    {
        [Range(2000, 2022)]
        public int YearOfManufacture { get; set; }
        public CarType Type { get; set; }
        public override string ToString()
        {
            return $"{Id,-40} - {Name,-7} - {Color,-7} - {Price, -15} - {Brand, -7} - {YearOfManufacture,-7} - {Type, -7} - {' ', -7} - {' ', -7}\n";
        }
    }
}
