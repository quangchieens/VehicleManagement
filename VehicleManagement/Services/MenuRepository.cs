using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagement.Entities;
using VehicleManagement.Entities.VehicleTypes;

namespace VehicleManagement.Services
{
    internal class MenuRepository : IMenuRepository
    {
        public Type GetVehicleType()
        {
            Console.WriteLine("Is your vehicle a Car or a Motorbike? 0: Car, 1: Motorbike");
            var choice = int.Parse(Console.ReadLine());
            if (choice == 0)
            {
                return typeof(Car);
            }
            else if (choice == 1)
            {
                return typeof(Motorbike);
            }

            throw new ArgumentNullException(nameof(choice));
        }

        public string GetVehicleId(string action)
        {
            Console.Write($"Input the id of the car you want to {action}: ");
            return Console.ReadLine();
        }

        public string GetSearchType()
        {
            Console.WriteLine("Do you want to search by name or you want to search by id?");
            string choice = Console.ReadLine();

            return choice switch
            {
                "name" or "Name" or "NAME" => "name",
                "id" or "Id" or "ID" => "id",
                _ => "a"
            };
        }

        public void ShowMenu()
        {
            Console.WriteLine("Phan mem abcxyz");
            Console.WriteLine("1. Get data from file");
            Console.WriteLine("2. Add new vehicle");
            Console.WriteLine("3. Update vehicle");
            Console.WriteLine("4. Delete Vehicle");
            Console.WriteLine("5. Search vehicle");
            Console.WriteLine("6. Show vehicle list");
            Console.WriteLine("7. Store data to file");
        }

        public string GetShowType()
        {
            Console.WriteLine("6.1. Show all");
            Console.WriteLine("6.2. Show all (descending by price)");

            string input = Console.ReadLine();

            return input switch
            {
                "6.1" or "6.1." or "Show all" => "0",
                "6.2" or "6.2." or 
                "Show all (descending by price" or 
                "descending by price" or "descending" => "1",
                _ => "Wrong input format"
            };
        }
    }
}
