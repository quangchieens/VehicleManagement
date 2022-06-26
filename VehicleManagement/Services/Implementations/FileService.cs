using System.IO;
using System.Linq;
using VehicleManagement.Helpers;
using VehicleManagement.Constants;
using VehicleManagement.Repositories.Interfaces;
using VehicleManagement.Services.Interfaces;

namespace VehicleManagement.Services.Implementations
{
    public class FileService : IFileService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IBrandRepository _brandRepository;
        public FileService(IVehicleRepository vehicleRepository, IBrandRepository brandRepository)
        {
            _vehicleRepository = vehicleRepository;
            _brandRepository = brandRepository;
        }
        public void LoadDataFromFile()
        {
            var data = new StreamReader(FileConstants.FilePath);
            while (data.ReadLine() is { } line)
            {
                var words = line.Split(" -");
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
            var text = File.ReadAllText(FileConstants.FilePath);
            var vehicles = _vehicleRepository.GetVehicles(v => !text.Contains(v.Id.ToString()));

            foreach (var vehicle in vehicles)
            {
                File.AppendAllText(FileConstants.FilePath, vehicle.ToString());
            }
        }
        private void RemoveFromFile(string id)
        {
            File.WriteAllLines(FileConstants.FilePath, File.ReadAllLines(FileConstants.FilePath).Where(l => !l.Contains(id)));
        }
    }
}
