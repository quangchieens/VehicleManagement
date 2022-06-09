using System;
using VehicleManagement.Entities;
using VehicleManagement.Entities.Enums;

namespace VehicleManagement.Services
{
    internal class IOVehicles
    {
        public string GetVehicleType()
        {
            Console.WriteLine("Is your vehicle a Car or a Motorbike? 0: Car, 1: Motorbike");
            var choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 0:
                    return "car";
                case 1:
                    return "motorbike";
                default:
                    return null;
            }
        }

        public Car InputCar(IBrandRepository brandRepository)
        {
            var car = new Car();
            GetVehicleInput(car, brandRepository);

            Console.WriteLine("Enter the year of manufacture: ");
            car.YearOfManufacture = int.Parse(Console.ReadLine());

            Console.WriteLine("Choose the car type: ");
            if (!Enum.TryParse(Console.ReadLine(), out CarType carType))
            {
                throw new ArgumentNullException(nameof(carType));
            }
            car.Type = carType;

            return car;
        }

        public Guid GetId()
        {
            Console.Write("Input the vehicle id: ");
            Guid.TryParse(Console.ReadLine(), out Guid id);

            return id;
        }

        public Motorbike InputMotorbike(IBrandRepository brandRepository)
        {
            var motorbike = new Motorbike();
            GetVehicleInput(motorbike, brandRepository);

            Console.WriteLine("Enter the motorbike's speed");
            motorbike.Speed = int.Parse(Console.ReadLine());

            Console.WriteLine("Is the license required? Y/N");
            motorbike.LicenseRequired = Console.ReadLine() switch
            {
                "y" or "Y" or "yes" or "Yes" => true,
                "n" or "N" or "no" or "No" => false,
                _ => throw new InvalidOperationException()
            };

            return motorbike;
        }

        private Vehicle GetVehicleInput(Vehicle vehicle, IBrandRepository brandRepository)
        {
            Console.Write("Input vehicle name: ");
            vehicle.Name = Console.ReadLine();

            Console.Write("Input vehicle color between Red/Green/Blue: ");
            if (!Enum.TryParse(Console.ReadLine(), out Color color))
            {
                throw new ArgumentNullException(nameof(color));
            }
            vehicle.Color = color;

            Console.Write("Input vehicle Price: ");
            vehicle.Price = decimal.Parse(Console.ReadLine());

            Console.Write("Input vehicle brand: ");
            var brand = Console.ReadLine();
            vehicle.Brand = brandRepository.CheckBrand(brand);

            return vehicle;
        }

        public (string propertyToChange, string newPropertyValue) GetPropertyToChange(Vehicle vehicle)
        {
            if (vehicle is Car)
            {
                Console.WriteLine($"What information do you want to update? Name/Color/Price/Brand/Year of manufacture/Car type");
            }
            else if (vehicle is Motorbike)
            {
                Console.WriteLine($"What information do you want to update? Name/Color/Price/Brand/Speed/Require license?");
            }
            else
            {
                Console.WriteLine($"What information do you want to update? Name/Color/Price/Brand");
            }

            string propertyToChange = Console.ReadLine();
            Console.Write("New value:");
            string newPropertyValue = Console.ReadLine();
            return (propertyToChange, newPropertyValue);
        }

        public string GetShowType()
        {
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

        public string GetSearchType()
        {
            string choice = Console.ReadLine();

            return choice switch
            {
                "name" or "Name" or "NAME" => "name",
                "id" or "Id" or "ID" => "id",
                _ => "Wrong input"
            };
        }
    }
}
