using System.IO;

namespace VehicleManagement.Services
{
    internal class Constants
    {
        public static string FileName { get; } = "vehicles.txt";
        public static string FileDirectory { get; } =
            Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString() + "\\";
    }
}
