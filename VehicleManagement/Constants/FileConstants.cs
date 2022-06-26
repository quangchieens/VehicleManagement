using System.IO;

namespace VehicleManagement.Constants
{
    public class FileConstants
    {
        private static readonly string FileName = "vehicles.txt";
        private static readonly string FileDirectory =
            Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent + "\\";
        public static string FilePath { get; } = FileDirectory + FileName;
    }
}