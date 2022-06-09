using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleManagement.Entities
{
    internal class Motorbike : Vehicle
    {
        [Range(0, 200)]
        public int Speed { get; set; }
        public bool? LicenseRequired { get; set; }
        public override string ToString()
        {
            return $"{Id,-40} - {Name,-7} - {Color,-7} - {Price,-15} - {Brand.Name,-7} - {' ',-7} - {' ',-7} - {Speed,-7} - {LicenseRequired,-7}\n";
        }
        public void MakeSound()
        {
            Console.WriteLine("Tin tin tin");
        }
    }
}
