using System;
using System.Reflection;
using VehicleManagement.Entities;
using VehicleManagement.Entities.Enums;
using VehicleManagement.Repositories.Interfaces;

namespace VehicleManagement.Services
{
    internal class Input
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IBrandRepository _brandRepository;
        public Input(IVehicleRepository vehicleRepository, IBrandRepository brandRepository)
        {
            _vehicleRepository = vehicleRepository;
            _brandRepository = brandRepository;
        }
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

        private Car InputCar(Vehicle vehicle)
        {
            var car = new Car() { Id = vehicle.Id, 
                                Brand = vehicle.Brand,
                                Color = vehicle.Color,
                                Name = vehicle.Name,
                                Price = vehicle.Price};
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
            if (Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                return id;
            }
            else
            {
                return Guid.Empty;
            }
        }

        private Motorbike InputMotorbike(Vehicle vehicle)
        {

            var motorbike = new Motorbike()
            {
                Id = vehicle.Id,
                Brand = vehicle.Brand,
                Color = vehicle.Color,
                Name = vehicle.Name,
                Price = vehicle.Price
            };
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

        public Vehicle GetVehicleToManipulate()
        {
            var vehicleType = GetVehicleType();
            var vehicle = new Vehicle();
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
            vehicle.Brand = _brandRepository.CheckBrand(brand);

            if (vehicleType == "car")
            {
                return InputCar(vehicle);
            }
            else if (vehicleType == "motorbike")
            {
                return InputMotorbike(vehicle);
            } 
            else
            {
                Console.WriteLine("Your vehicle should go extinct");
                return null;
            }
        }

        public Vehicle GetUpdateVehicleInformation()
        {
            Console.Write("Input the id for which vehicle you want to update: ");
            if (!Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                return null;
            }

            var vehicle = _vehicleRepository.GetVehicle(id);

            Vehicle vehicleToUpdate;

            if (vehicle is Car)
            {
                vehicleToUpdate = new Car();
            }
            else
            {
                vehicleToUpdate = new Motorbike();
            }

            foreach (var property in vehicleToUpdate.GetType().GetProperties())
            {
                var value = property.GetValue(vehicle, null);
                Console.WriteLine($"New value for {value}: NewValue/No");
                string choice = Console.ReadLine();
                if (choice is "No" or "no" or "NO")
                {
                    continue;
                }
                else
                {
                    property.SetValue(choice, value);
                }   
            }
            return vehicleToUpdate;
        }

        public string GetPrintType()
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
