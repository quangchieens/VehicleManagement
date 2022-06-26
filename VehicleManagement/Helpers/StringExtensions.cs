using System;
using System.Collections.Generic;
using System.Linq;

namespace VehicleManagement.Helpers
{
    internal static class StringExtensions
    {
        public static void PrintSearchItems(this string line, string searchQuery)
        {
            var searchedTermsIndexes = line.AllIndexesOf(searchQuery).ToArray();
            var length = searchQuery.Length;
            int i = 0, j = 0;
            var defaultColor = Console.ForegroundColor;
            foreach (var index in searchedTermsIndexes)
            {
                Console.Write(line.Substring(i, searchedTermsIndexes[j++] - i));
                i = index + length + 1;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(searchQuery);
                Console.ForegroundColor = defaultColor;
            }

            Console.WriteLine(line.Substring(i - 1, line.Length - i));
        }

        private static IEnumerable<int> AllIndexesOf(this string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    break;
                yield return index;
            }
        }
    }
}
