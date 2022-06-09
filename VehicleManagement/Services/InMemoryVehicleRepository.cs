using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagement.Entities;
using VehicleManagement.Entities.Enums;
using VehicleManagement.Entities.VehicleTypes;
using VehicleManagement.Helpers;

namespace VehicleManagement.Services
{
    internal class InMemoryVehicleRepository : IVehicleRepository
    {
        private readonly List<Vehicle> _vehicles;
        private const string fileDirectory = @"C:\Users\quangchieens\source\repos\VehicleManagement\VehicleManagement\";
        private readonly string fileName;
        private readonly string filePath;

        public InMemoryVehicleRepository(string fileName)
        {
            _vehicles = new List<Vehicle>()
            {
                new Car() { Name = "Khoa", Brand = Brand.Maybach, Color = Color.Blue, Price = 234.234m, Type = CarType.Sport, YearOfManufacture = 2020 },
                new Motorbike() { Name = "Chien", Brand = Brand.Ford, Color = Color.Red, Price = 23423414.32m, LicenseRequired = true, Speed = 220 }
            };

            this.fileName = fileName;
            this.filePath = fileDirectory + fileName;
        }

        private Vehicle GetVehicle(Guid id)
        {
            return _vehicles.FirstOrDefault(v => v.Id == id);
        }

        public void AddVehicle(Type vehicleType)
        {
            if (vehicleType is null)
            {
                throw new ArgumentNullException(nameof(vehicleType));
            }

            if (vehicleType == typeof(Car))
            {
                var car = new Car();
                GetVehicleInput(car);

                Console.WriteLine("Enter the year of manufacture: ");
                car.YearOfManufacture = int.Parse(Console.ReadLine());

                Console.WriteLine("Choose the car type: ");
                if (!Enum.TryParse(Console.ReadLine(), out CarType carType))
                {
                    throw new ArgumentNullException(nameof(carType));
                }
                car.Type = carType;

                _vehicles.Add(car);
            }
            else
            {
                var motorbike = new Motorbike();
                GetVehicleInput(motorbike);

                Console.WriteLine("Enter the motorbike's speed");
                motorbike.Speed = int.Parse(Console.ReadLine());

                Console.WriteLine("Is the license required? Y/N");
                motorbike.LicenseRequired = Console.ReadLine() switch
                {
                    "y" or "Y" or "yes" or "Yes" => true,
                    "n" or "N" or "no" or "No" => false,
                    _ => throw new InvalidOperationException()
                };

                _vehicles.Add(motorbike);
            }
        }

        public void DeleteVehicle(string id)
        {
            if (!_vehicles.Any(v => v.Id == Guid.Parse(id)))
            {
                throw new ArgumentNullException(nameof(id));
            }

            _vehicles.Remove(GetVehicle(Guid.Parse(id)));
            RemoveFromFile(id.ToString());
        }

        private void SearchVehicleByName()
        {
            Console.Write("Input search query: ");
            var name = Console.ReadLine();

            var text = File.ReadAllLines(filePath);
            foreach (var line in text)
            {
                if (line.Contains(name))
                {
                    line.PrintSearchItems(name);
                }
            }
        }

        private void SearchVehicleById()
        {
            Console.Write("Input search ID: ");
            var id = Console.ReadLine();

            var text = File.ReadAllLines(filePath);
            foreach (var line in text)
            {
                if (line.Contains(id))
                {
                    line.PrintSearchItems(id);
                    return;
                }
            }
        }

        public void ShowVehicleList(string showType)
        {
            if (showType is "0")
            {
                ShowAllVehicle();
            }
            else if (showType is "1")
            {
                ShowVehiclesDescending();
            }
            else Console.WriteLine(showType);
        }

        private void ShowAllVehicle()
        {
            var 
        }

        private void ShowVehiclesDescending()
        {

        }

        public void StoreData()
        {
            var text = File.ReadAllText(filePath);
            var vehicles = _vehicles.Where(v => !text.Contains(v.Id.ToString()))
                                    .ToList();

            foreach (var vehicle in vehicles)
            {
                File.AppendAllText(filePath, vehicle.ToString());
            }
        }

        public bool UpdateVehicle(Guid id)
        {
            var vehicle = GetVehicle(id);

            if (vehicle is null)
            {
                Console.WriteLine("The vehicle does not exist");
                return false;
            }

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

            vehicle.ChangeProperty(propertyToChange, newPropertyValue);

            return true;

        }

        private Vehicle GetVehicleInput(Vehicle vehicle)
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
            if (!Enum.TryParse(Console.ReadLine(), out Brand brand))
            {
                throw new ArgumentNullException(nameof(brand));
            }
            vehicle.Brand = brand;

            return vehicle;
        }

        public void LoadData()
        {
            StreamReader data = new StreamReader(filePath);
            string? line;
            while ((line = data.ReadLine()) is not null)
            {
                string[] words = line.Split('-');
                if (words.Any(w => w.Trim() == "YearOfManufacture"))
                {
                    _vehicles.Add(new Car
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
                else
                {
                    _vehicles.Add(new Motorbike
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

        public void SearchVehicle(string searchType)
        {
            if (searchType is "name")
            {
                SearchVehicleByName();
            }
            else if (searchType is "id")
            {
                SearchVehicleById();
            }
            else
            {
                Console.WriteLine("Wrong input");
            }
        }

        private void RemoveFromFile(string id)
        {
            File.WriteAllLines(filePath, File.ReadAllLines(filePath).Where(l => !l.Contains(id)));
        }


            //private string GetVehicleType(Vehicle vehicle)
            //{
            //    if (vehicle.ToString().Contains("YearOfManuFacture"))
            //        return "car";
            //    return "motorbike";
            //}
    }
}
