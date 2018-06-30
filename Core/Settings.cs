using System;

namespace Core
{
    class Settings
    {
        internal const string API_KEY = "API_KEY";

        internal static object GetAppDataDirectory()
        {
            var currentDirectory = Environment.CurrentDirectory;
            return currentDirectory.Split(new string[] { "bin" }, StringSplitOptions.None)[0] + @"App_Data\";
        }
    }
}