using System;
using System.IO;

namespace NLog_AspNet_MVC
{
    public static class AppHelpers
    {
        public static string ReadAppDataFile(string path, bool returnMessageIfNotFound = true)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
            filePath = Path.Combine(filePath, path);
            if (!File.Exists(filePath))
            {
                if (returnMessageIfNotFound)
                {
                    return $"File {filePath} was not found";
                }

                return string.Empty;
            }

            return File.ReadAllText(filePath);
        }
    }
}