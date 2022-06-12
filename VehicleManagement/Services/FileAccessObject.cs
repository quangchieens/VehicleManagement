using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleManagement.Helpers;
using VehicleManagement.Repositories.Interfaces;

namespace VehicleManagement.Services
{
    internal class FileAccessObject
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly string _filePath;
        public string FilePath => _filePath;
        public FileAccessObject(IVehicleRepository vehicleRepository, IBrandRepository brandRepository)
        {
            _vehicleRepository = vehicleRepository;
            _brandRepository = brandRepository;
            _filePath = Path.Combine(Constants.FileDirectory + Constants.FileName);
        }
        public void LoadDataFromFile()
        {
            StreamReader data = new StreamReader(FilePath);
            string line;
            while ((line = data.ReadLine()) is not null)
            {
                string[] words = line.Split(" -");
                if (words.Any(w => w.Trim() == "True" || w.Trim() == "False"))
                {
                    _vehicleRepository.AddMotorbike(words, _brandRepository);
                }
                else
                {
                    _vehicleRepository.AddCar(words, _brandRepository);
                }
            }
        }

        public void StoreDataToFile()
        {
            //only append unoccurred instances
            var text = File.ReadAllText(FilePath);
            var vehicles = _vehicleRepository.GetVehicles(v => !text.Contains(v.Id.ToString()));

            foreach (var vehicle in vehicles)
            {
                File.AppendAllText(_filePath, vehicle.ToString());
            }
        }
    }
}
